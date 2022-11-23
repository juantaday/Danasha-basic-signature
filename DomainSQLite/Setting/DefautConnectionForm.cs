using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Threading;
using System.Data.SqlClient;

using System.Diagnostics;
using DomainSQLite.Models;
using DomainSQLite.Funtions;

namespace DomainSQLite.Setting
{
    public partial class DefautConnectionForm : Form
    {
        private Conection _cnn;
        private readonly string _connectionStringFile;  
        
        public DefautConnectionForm()
        {
            InitializeComponent();


            _connectionStringFile = @"Data Source = (localdb)\MSSQLLocalDB;
            AttachDbFilename = C:\SQL_Data\Basic\JSofwareCommerceDB_MDF.mdf;
            Integrated Security = True;
            Connect Timeout = 30;";

            GetLoadData();

        }

        private void Close_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void GetLoadData() {
            try
            {
              this._cnn =await FunctionSQLite.GetDefaultConectionInLine();
            }
            catch (Exception ex)
            {

                System.Diagnostics .Debug.WriteLine (ex.Message);
            }
        }
        private void DefautConnectionForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            panelFilepath.Location = new Point(x: panelUserIdentity.Location.X, y: panelUserIdentity.Location.Y);
            panelFilepath.Visible = false;
            this.Width = 420;
            ViewData();
        }

        private async void ViewData() {
            try
            {
               await Task.Run(() => {

                    while (this._cnn == null)
                    {
                        Thread.Sleep(200);
                    }

                    this.Invoke(new MethodInvoker(() => {
                        this.DataServerText.Text = this._cnn.IpConection;
                        this.NameDataBaseText.Text = this._cnn.NameDatabase;
                        this.UserIdText.Text = this._cnn.UserId;
                        this.PasswordText.Text = this._cnn.Password;
                        if (string.IsNullOrEmpty(this.NameDataBaseText.Text))
                            this.NameDataBaseText.Text = "JSofwareCommerceDB";

                        if (string.IsNullOrEmpty(this.UserIdText.Text))
                            this.UserIdText.Text = "JsofUserAdmin";

                        if (string.IsNullOrEmpty(this.PasswordText.Text))
                            this.PasswordText.Text = "1234567890";

                        if (string.IsNullOrEmpty(this.filePathTextBox.Text))
                            this.filePathTextBox.Text = @"Data Source=.\;AttachDbFilename=|DataDirectory|\DataBaseMirror\JSofwareCommerceDB.mdf;" +
                                   " Integrated Security=True;" +
                                   " Connect Timeout=30;";

                        //filePathTextBox
                    }));
                });
               
            }
            catch (Exception ex)
            {

                Interaction.MsgBox (ex.Message  + "\n" + ex.StackTrace ,MsgBoxStyle.Critical,"Error");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "Está seguro de guardar los cambio.?";
            sql = sql + "\n\nLe recomiendo que primero haga un text de coneccion..";
            sql = sql + "\nDe todas maneras desea guardar.?";

            if (Interaction.MsgBox(sql, MsgBoxStyle.Question | MsgBoxStyle.DefaultButton2 | MsgBoxStyle.YesNo) != MsgBoxResult.Yes)
            {
                return;
            }
            var cnn = new Conection()
            {
                IpConection = this.DataServerText.Text.Trim(),
                NameDatabase = this.NameDataBaseText.Text.Trim(),
                UserId = this.UserIdText.Text.Trim(),
                Password = this.PasswordText.Text.Trim(),
                FilePath = this.filePathTextBox.Text.Trim(),
            };

            if (!panelFilepath.Visible)
                cnn.FilePath = string.Empty;


            this.Cursor = Cursors.WaitCursor;
            try
            {
                var result = Task.Run(async () => {
                    return await FunctionSQLite.SaveItemContectionAsync(cnn);
                }).GetAwaiter().GetResult();

                this.Cursor = Cursors.Default;
                if (result)
                {

                    Interaction.MsgBox("Guardada exitosamente..!\nSe reiniciará la aplicación.",
                        MsgBoxStyle.Exclamation, "Alert..");

                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace, MsgBoxStyle.Critical, "Error");
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                bool result = false;
                string stringConnection = string.Empty;

                if (panelFilepath.Visible) {
                    stringConnection = filePathTextBox.Text.Trim();
                } else if (panelUserIdentity.Visible ) {
                    stringConnection = string.Format(
                                "Data Source={0};" +
                                "Initial Catalog={1};Persist Security Info=True;" +
                                "User ID={2};Password={3};",
                                this.DataServerText.Text,
                                this.NameDataBaseText.Text,
                                this.UserIdText.Text,
                                this.PasswordText.Text);
                } 
                else
                {
                    Interaction.MsgBox("Algo salio mal , no se pudo connfigurar");
                    return;
                }
 

                this.Cursor = Cursors.WaitCursor;
               
                using (var cnn = new SqlConnection(stringConnection))
                {
                    cnn.Open();
                    result = true;
                }

                this.Cursor = Cursors.Default;
                if (result) {
                    Interaction.MsgBox("Configuració exitosa....");
                }

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Interaction.MsgBox (ex.Message + "\n" + ex.StackTrace ,MsgBoxStyle.Critical,"Error");
            }

            
        }

        private void Userbutton_Click(object sender, EventArgs e)
        {
            HeaderButton.Location = new Point(x: Userbutton.Location.X, y: HeaderButton.Location.Y);
            panelUserIdentity.Visible = true;
            panelFilepath.Visible = false;
        }

        private void Filebutton_Click(object sender, EventArgs e)
        {
            HeaderButton.Location = new Point(x: Filebutton.Location.X, y: HeaderButton.Location.Y);
            panelUserIdentity.Visible = false;
            panelFilepath.Visible = true;

            filePathTextBox.Text = this._cnn?.FilePath?? "";
        }

        private void filePathTextBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            filePathTextBox.Text = this._connectionStringFile;
        }


    }
}
