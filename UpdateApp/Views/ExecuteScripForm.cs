using BrightIdeasSoftware;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using UpdateApp.Models;

namespace UpdateApp.Views
{
    public partial class ExecuteScripForm : Form
    {
        private readonly string _codeUserNamager;
        private readonly string cnn;
        private List<FileObjectSelect> _FileObjects;
        private PictureBox _imageRunning;

        public ExecuteScripForm(string conection)
        {
            InitializeComponent();

            _imageRunning = new PictureBox();
            _imageRunning.Visible = false;
            this.cnn = conection;

        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            string initialPth = string.Empty;
            try
            {
                initialPth = Path.Combine(Application.CommonAppDataPath, "Updates");
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.Write(ex.Message);
            }

            try
            {
                using (var fileDialog = new OpenFileDialog())
                {
                    fileDialog.InitialDirectory = initialPth;
                    fileDialog.Filter = "Files sql|*.sql";
                    fileDialog.Multiselect = true;

                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (this._FileObjects == null)
                            this._FileObjects = new List<FileObjectSelect>();

                        this._FileObjects.Clear();
                        this.objectListView1.UpdateObjects(this._FileObjects);

                        for (int i = 0; i < fileDialog.FileNames.Length; i++)
                        {
                            var info = new System.IO.FileInfo(fileDialog.FileNames[i]);

                            this._FileObjects.Add(new FileObjectSelect
                            {
                                Extencion = info.Extension,
                                PathFile = info.FullName,
                                NameFile = info.Name,
                                Size = info.Length,
                                IsPrepared = true,
                            });

                        }


                        this.objectListView1.UpdateObjects(this._FileObjects);
                    }
                };

            }
            catch (Exception ex)
            {

                Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace, MsgBoxStyle.Critical, "Error");
            }

        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            if (_imageRunning.Visible)
                return;

            if (this.objectListView1.CheckedObjects.Count == 0)
            {
                Interaction.MsgBox("Seleccione las versiones a ejecutar..");
                return;
            }

            if (Interaction.MsgBox("Esta seguro de ejecutar actualizaciones en lista.?",
                MsgBoxStyle.Question | MsgBoxStyle.YesNo | MsgBoxStyle.DefaultButton2) != MsgBoxResult.Yes)
                return;


            if (_imageRunning.Image == null)
                _imageRunning.Size = new Size(20, 20);
            _imageRunning.Image = Image.FromFile(".\\AnimatedGifs\\Runnig.gif");
            _imageRunning.SizeMode = PictureBoxSizeMode.StretchImage;

            if (_imageRunning.Image != null && rjButton2.Controls.OfType<PictureBox>().FirstOrDefault() == null)
            {
                rjButton2.Controls.Add(_imageRunning);
                _imageRunning.Location = new Point(2, 15);
                _imageRunning.Visible = true;
            }
            else
            {
                _imageRunning.Visible = true;
            }

            this.Cursor = Cursors.WaitCursor;

            Execute_Scrip();
        }
        private async void Execute_Scrip()
        {
            string error = "";
            try
            {
                await Task.Delay(200);
                //Server.MapPath("~/sql-scripts/")
                foreach (FileObjectSelect model in this.objectListView1.CheckedObjects)
                {

                    string script = File.ReadAllText(model.PathFile);

                    string nameNotExte = model.NameFile.Replace(".sql", "");

                    string versioNum = nameNotExte.Replace(".", "");

                     string verSimple = versioNum.Replace("scrip v", "");

                    int versionReal = 0;
                    if (!int.TryParse(verSimple, out versionReal))
                        throw new Exception("No se puede reconover version..");


         
                    IEnumerable<string> commandStrings = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                   
                    using (var cnn = new SqlConnection(this.cnn))
                    {

                        if (cnn.State != System.Data.ConnectionState.Open)
                        {
                            await cnn.OpenAsync();
                        }

                        var dt = new   DataTable ();
                        using (var command = new SqlCommand(stringGetVrtsio(), cnn))
                        {

                            command.ExecuteNonQuery();
                            using (var tap = new SqlDataAdapter(command)) 
                            {
                                tap.Fill(dt);
                            }
                        }

                        if (dt == null || dt.Rows.Count == 0)
                            throw new Exception("No se optuvo el historial de versiones..");

                        if (dt.Rows[0].Field <int>("ProductValue")> versionReal )
                            throw new Exception("La version "  + nameNotExte  + " ya existe..");


                        foreach (var commandString in commandStrings)
                        {
                            if (!string.IsNullOrWhiteSpace(commandString.Trim()))
                            {
                                using (var command = new SqlCommand(commandString, cnn))
                                {
                                    command.ExecuteNonQuery();
                                }
                            }

                        }

                        string updateVer = @"insert into [__MigrationHistory] (MigrationId,ProductVersion,ProductValue)" +
                            "\n\r"+$"Values ('{model.NameFile}','{model.NameFile}',{versionReal});";

                        using (var command = new SqlCommand(updateVer, cnn))
                        {
      
                            command.ExecuteNonQuery();
                        }


                    }

                    model.ExcuteSuccess = true;
                    this.objectListView1.Invoke(new MethodInvoker(() => {
                        this.objectListView1.UpdateObject(model);
                    }));
                }


            }
            catch (Exception ex)
            {
                error = ex.Message + "\n" + ex.StackTrace;
            }

            finally
            {

                this.Invoke(new MethodInvoker(() => {
                    if (!string.IsNullOrEmpty(error))
                    {
                        Interaction.MsgBox(error, MsgBoxStyle.Critical, "Error");
                    }
                    this.Cursor = Cursors.Default;
                }));

                this.rjButton2.Invoke(new MethodInvoker(() => {
                    this.rjButton2.Enabled = true;
                }));

                _imageRunning.Invoke(new MethodInvoker(() => {
                    _imageRunning.Visible = false;
                }));
            }
        }

        private string stringGetVrtsio() {
                        return @"if exists (SELECT  1
            FROM INFORMATION_SCHEMA.TABLES
            WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = '__MigrationHistory') begin 
            -- exsiste la tabla pero no existe el campo ProductValue destruyo y crea nuevamenete
            print 'Existe tabla __MigrationHistory';
            IF not exists
               (
	            SELECT 1
	            FROM INFORMATION_SCHEMA.COLUMNS
	            WHERE COLUMN_NAME = 'ProductValue' AND TABLE_NAME = '__MigrationHistory'
	            )
	            BEGIN
	              print 'No existe columna ProductValue';
                  drop table dbo.__MigrationHistory;
	  
	              create table dbo.__MigrationHistory (
	              [MigrationId] varchar (150) primary key not null,
	              [ProductVersion] varchar (30) not null,
	              [ProductValue] int not null);

	              declare  @ParmDefinition nvarchar(255);
	              declare @qry nvarchar (max) =N'
	              Insert into dbo.__MigrationHistory (MigrationId,ProductValue,ProductVersion)
	              values (@nameVersion,@valueVersion,0);'
	              SET @ParmDefinition = N'@nameVersion varchar(15), @valueVersion int';  
	              EXECUTE sp_executesql @qry,@ParmDefinition,@nameVersion ='StartinSystem',@valueVersion =0;
	             select 0 as ProductValue;
	              END
               else begin  --Existe la table __MigrationHistory y el campo ProductValue
                  print 'Existe la columna  ProductValue';
	              DECLARE @valueVersion int;  

	              declare @qry1 nvarchar (max) =N'
		            select  top (1) @valueVersionOUT = ProductValue  
		            from __MigrationHistory order by ProductValue desc;';

	             SET @ParmDefinition = N'@valueVersionOUT int OUTPUT';  

	             EXECUTE sp_executesql @qry1, @ParmDefinition, @valueVersionOUT=@valueVersion OUTPUT;

	             if (@valueVersion is null ) begin 
	               select 0 as ProductValue;
	               end
                 else begin 
	               select @valueVersion AS ProductValue;
	             end
	            end
            end
            else  begin  -- si no existe la tabla __MigrationHistory la CREA y retorna 0
               create table dbo.__MigrationHistory (
	              [MigrationId] varchar (150) primary key not null,
	              [ProductVersion] varchar (30) not null,
	              [ProductValue] int not null);
	             select 0 as ProductValue;
            end 
            ";
        }

        private void objectListView1_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            FileObjectSelect p = (FileObjectSelect)e.Model;
            if (p is null)
                return;
            if (e.ColumnIndex == 0)
            {
                if (p.ExcuteSuccess)
                {
                    e.SubItem.Decoration = new ImageDecoration(Properties.Resources.ok_16, 100);
                }
                else
                {
                    e.SubItem.Decoration = null;
                }
            }
        }
    }
}
