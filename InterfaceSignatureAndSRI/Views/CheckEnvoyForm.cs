
using CADsisVenta;
using CADsisVenta.Funtions;
using CADsisVenta.Statics;
using CrystalDecisions.CrystalReports.Engine;
using ec.gob.sri.comprobantes;
using ec.gob.sri.comprobantes.Enum;
using ec.gob.sri.comprobantes.Net;
using ec.gob.sri.comprobantes.Utils;
using ec.gob.sri.Xml;
using InterfaceSignatureAndSRI.Data;
using InterfaceSignatureAndSRI.GeneratedXML;
using InterfaceSignatureAndSRI.Models;
using InterfaceSignatureAndSRI.SendMail;
using InterfaceSignatureAndSRI.SigningXML;
using InterfaceSignatureAndSRI.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using XmlGridViewSample;

namespace InterfaceSignatureAndSRI.Views
{
    public partial class CheckEnvoyForm : Form
    {
        private XmlGridView xmlGridView;
        DataClassesDBDataContext dbReflexion = new DataClassesDBDataContext(DomainSQLite.Setting.Configuration.ConectionString);
        private static string _NameDatabase = Properties.Settings.Default.NameDataBse;
        private bool isLoated;
        private bool _isChangCheck;
        private int LastIemIndex;
        internal DataGridView DataGridEcommerce;
        internal DataGridView DataGridSingned;

        private DateTime _fechaEmision;
        private DateTime _fechaAutoriza;
        private string _NumeroAutoriza;
        private string claveAcceso;
        private string codDoc;
        private string ambiente;
        private List<autorizacion> autorizacions;


        private int idFactura;
        private string _CodTerminal;
        private int _VoucherID;
        private XmlDocument doc;
        private Label LabelError;
        private string xmlFirmado;

        private FacturaViewModels viewModel;

        private string _FilterState;
        private string _FilterDate;
        private string _Email;
        ReportDocument rpt;

        public CheckEnvoyForm(string codTerminal)
        {
            InitializeComponent();
            //

            this.rpt = new rptElectronicInvoice();
            LastIemIndex = -1;
            _CodTerminal = codTerminal;
            DataGridEcommerce = new DataGridView();
            DataGridEcommerce.Dock = DockStyle.Fill;
            DataGridEcommerce.AllowUserToAddRows = false;
            DataGridEcommerce.AllowUserToDeleteRows = false;
            DataGridEcommerce.ReadOnly = true;
            DataGridEcommerce.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridEcommerce.BackgroundColor = Color.White;
            DataGridEcommerce.Name = "DataGridEcommerce";

            DataGridSingned = new DataGridView();
            DataGridSingned.Dock = DockStyle.Fill;
            DataGridSingned.AllowUserToAddRows = false;
            DataGridSingned.AllowUserToDeleteRows = false;
            DataGridSingned.ReadOnly = true;
            DataGridSingned.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridSingned.BackgroundColor = Color.White;
            DataGridSingned.Name = "DataGridSingned";
            this.DataGridSingned.RowEnter += new DataGridViewCellEventHandler(this.dataGridView1_RowEnter);

            LabelError = new Label
            {
                AutoSize = true,
                Location = new Point { X = 20, Y = 50 },
                ForeColor = Color.Red,
            };
            panelError.Controls.Add(LabelError);
        }

        private void CheckEnvoyForm_Load(object sender, EventArgs e)
        {
            xmlGridView = new XmlGridView();
            xmlGridView.Dock = DockStyle.Fill;
            panelViewXml.Controls.Add(xmlGridView);
            comboBox1.DataSource = ec.gob.sri.comprobantes.Enum.TipoAmbienteEnum.values().ToList();
            comboBox1.DisplayMember = "TypeName";
            comboBox1.ValueMember = "IntId";
            isLoated = true;

            checkedListBox1.SelectedIndex = 0;


        }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LastIemIndex == checkedListBox1.SelectedIndex)
            {
                CheckenItem();
                return;
            }

            CheckenItem();

            panelView.Controls.Clear();

            if (checkedListBox1.GetItemChecked(0) == true)
            {
                if (rptViewer.Dock != DockStyle.Fill)
                {
                    rptViewer.Dock = DockStyle.Fill;
                    rptViewer.BringToFront();
                }
                panelSigMnager.Visible = false;
                PanelECommerce.Visible = false;
                panelResum.Visible = true;
            }
            else if (checkedListBox1.GetItemChecked(1) == true)
            {
                panelSigMnager.BringToFront();
                panelSigMnager.Visible = true;
                panelResum.Visible = false;
                EnterClavepanel.Visible = false;
                FilterMunuSignedPanel.Visible = true;
                PanelECommerce.Visible = false;
                GeneratedXMlButton.Enabled = false;
                SignedButton.Enabled = true;
                SubmitSRIButton.Enabled = true;
                ConsultSRIButton.Enabled = true;
                panelView.Controls.Add(DataGridSingned);
                ViewPdfButton.Enabled = true;
                ViewXmlButton.Enabled = true;
            }
            else if (checkedListBox1.GetItemChecked(2) == true)
            {
                panelResum.Visible = false;
                EnterClavepanel.Visible = true;
                FilterMunuSignedPanel.Visible = false;
                PanelECommerce.Visible = false;
                GeneratedXMlButton.Enabled = false;
                SignedButton.Enabled = false;
                SubmitSRIButton.Enabled = false;
                ConsultSRIButton.Enabled = true;
                ViewPdfButton.Enabled = false;
                ViewXmlButton.Enabled = false;
                EnterClaveAcceso();

            }
            else if (checkedListBox1.GetItemChecked(3) == true)
            {
                panelResum.Visible = false;
                EnterClavepanel.Visible = false;
                FilterMunuSignedPanel.Visible = false;
                PanelECommerce.Visible = true;
                GeneratedXMlButton.Enabled = true;
                SignedButton.Enabled = false;
                SubmitSRIButton.Enabled = false;
                ConsultSRIButton.Enabled = false;
                panelView.Controls.Add(DataGridEcommerce);
                ViewPdfButton.Enabled = false;
                ViewXmlButton.Enabled = false;
                GeneratedXMLWithDataBase();
            }
        }

        private void GeneratedXMLWithDataBase()
        {


        }
        private void CheckenItem()
        {
            if (!isLoated)
            {
                return;
            }
            for (int i = 0; i <= checkedListBox1.Items.Count - 1; i++)
            {
                if (checkedListBox1.Items[i] == checkedListBox1.Items[checkedListBox1.SelectedIndex])
                {
                    checkedListBox1.SetItemChecked(i, true);
                    LastIemIndex = i;
                }
                else
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
            }

        }

        #region Region Get Datas XML

        private async Task GetDataListAsync()
        {
            string errorMessage = string.Empty;

            DataGridSingned.DataSource = null;

            await Task.Run (() => {
                try
                {
                     // Limpia la grilla antes de cargar datos

                    using (var db = new DataContextReflex())
                    {
                        var dt = db.Voucher
                            .Select(v => new
                            {
                                v.VoucherID,
                                v.IDRelationData,
                                v.ClaveAcceso,
                                v.Estado,
                                v.FechaEmision,
                                v.FechaAutorizacion,
                                v.ErrorMesage
                            })
                            .Take(300)
                            .ToList(); // Evita bloqueos en el UI thread

                        // Actualiza la UI en el hilo principal
                        this.Invoke(new MethodInvoker(() =>
                        {
                            var bindingSource = new BindingSource { DataSource = dt };
                            DataGridSingned.DataSource = bindingSource;
                            DataGridSingned.Columns[0].Visible = false;
                            DataGridSingned.Columns[1].HeaderText = "IDFacturacion";
                        }));
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message + "\n" + ex.StackTrace;
                }
            });    

            if (!string.IsNullOrEmpty(errorMessage))
            {
                Interaction.MsgBox(errorMessage, MsgBoxStyle.Critical, "Error");
            }   

        }



        private async void GetDataListByID(int idFacture)
        {
            DataGridSingned.DataSource = false;
            await Task.Factory.StartNew(() =>
            {
                using (DataContextReflex db = new DataContextReflex())
                {
                    var dt = (from v in db.Voucher
                              where v.IDRelationData == idFacture
                              select (
                              new
                              {
                                  v.VoucherID,
                                  v.IDRelationData,
                                  v.ClaveAcceso,
                                  v.Estado,
                                  v.FechaEmision,
                                  v.FechaAutorizacion,
                                  v.ErrorMesage
                              }));

                    this.Invoke(new MethodInvoker(() =>
                    {
                        DataGridSingned.DataSource = dt.ToList();
                        DataGridSingned.Columns[0].Visible = false;
                        DataGridSingned.Columns[1].HeaderText = "IDFacturacion";
                    }));
                }
            });
        }
        private async void FilterMyCliente(int idCliente)
        {
            ClienteFindButton.Enabled = false;
            string stringConnection = "";
            string nameDataBase = "";
            try
            {
                Cursor = Cursors.WaitCursor;

                await Task.Factory.StartNew(() =>
                {
                    using (DataContextReflex db = new DataContextReflex())
                    {
                        stringConnection = db.Connection.ConnectionString;
                        nameDataBase = db.Connection.Database;
                    }
                    using (SqlConnection cnn = new SqlConnection(stringConnection))
                    {
                        cnn.Open();
                        using (SqlCommand cmd = new SqlCommand("GetListWidByIDCliente", cnn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@idCliente", SqlDbType.Int);
                            cmd.Parameters.Add("@codTerminal", SqlDbType.VarChar, 8);
                            cmd.Parameters.Add("@DataBaseName", SqlDbType.VarChar, 50);

                            cmd.Parameters["@idCliente"].Value = idCliente;
                            cmd.Parameters["@codTerminal"].Value = this._CodTerminal;

                            cmd.Parameters["@DataBaseName"].Value = dbReflexion.Connection.Database;
                            cmd.ExecuteNonQuery();
                            using (SqlDataAdapter dat = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                dat.Fill(dt);
                                this.Invoke(new MethodInvoker(() =>
                                {
                                    DataGridSingned.DataSource = dt;
                                }));
                            }
                        }

                    }

                });
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace,
                    MsgBoxStyle.Critical, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
                ClienteFindButton.Enabled = true;
            }
        }
        private async void GetDataListByState(string stado)
        {
            DataGridSingned.DataSource = false;
            await Task.Factory.StartNew(() =>
            {
                using (DataContextReflex db = new DataContextReflex())
                {
                    var dt = (from v in db.Voucher
                              where v.Estado.ToUpper() == stado.ToUpper()
                              select (
                              new
                              {
                                  v.VoucherID,
                                  v.IDRelationData,
                                  v.ClaveAcceso,
                                  v.Estado,
                                  v.FechaEmision,
                                  v.FechaAutorizacion,
                                  v.ErrorMesage
                              })).Take(300);

                    this.Invoke(new MethodInvoker(() =>
                    {
                        DataGridSingned.DataSource = dt.ToList();
                        DataGridSingned.Columns[0].Visible = false;
                        DataGridSingned.Columns[1].HeaderText = "IDFacturacion";
                    }));
                }
            });
        }

        private async void GetDataListByStateAndDate(string stado, DateTime date)
        {
            DataGridSingned.DataSource = false;

            DateTime star = StardateTimePicker1.Value;
            DateTime end = EndDateTimePicker.Value;

            if (DesdelinkLabel.Visible)
            {
                star = StardateTimePicker1.Value.Date;
                end = EndDateTimePicker.Value.Date.AddDays(1);
            }

            await Task.Factory.StartNew(() =>
            {
                using (DataContextReflex db = new DataContextReflex())
                {
                    var dt = (from v in db.Voucher
                              where (v.FechaEmision >= star &&
                              v.FechaEmision < end &&
                              v.Estado.ToUpper().Trim() == stado.ToUpper())
                              select (
                              new
                              {
                                  v.VoucherID,
                                  v.IDRelationData,
                                  v.ClaveAcceso,
                                  v.Estado,
                                  v.FechaEmision,
                                  v.FechaAutorizacion,
                                  v.ErrorMesage
                              })).Take(300);

                    this.Invoke(new MethodInvoker(() =>
                    {
                        DataGridSingned.DataSource = dt.ToList();
                        DataGridSingned.Columns[0].Visible = false;
                        DataGridSingned.Columns[1].HeaderText = "IDFacturacion";
                    }));
                }
            });
        }
        private async void GetDataListByDate()
        {
            DataGridSingned.DataSource = false;

            DateTime star = StardateTimePicker1.Value;
            DateTime end = EndDateTimePicker.Value;

            if (DesdelinkLabel.Visible)
            {
                star = StardateTimePicker1.Value.Date;
                end = EndDateTimePicker.Value.Date.AddDays(1);
            }

            await Task.Factory.StartNew(() =>
            {
                using (DataContextReflex db = new DataContextReflex())
                {
                    var dt = (from v in db.Voucher
                              where (v.FechaEmision >= star &&
                              v.FechaEmision < end)
                              select (
                              new
                              {
                                  v.VoucherID,
                                  v.IDRelationData,
                                  v.ClaveAcceso,
                                  v.Estado,
                                  v.FechaEmision,
                                  v.FechaAutorizacion,
                                  v.ErrorMesage
                              })).Take(500);

                    this.Invoke(new MethodInvoker(() =>
                    {
                        DataGridSingned.DataSource = dt.ToList();
                        DataGridSingned.Columns[0].Visible = false;
                        DataGridSingned.Columns[1].HeaderText = "IDFacturacion";
                    }));
                }
            });
        }
        private void EnterClaveAcceso()
        {

        }
        #endregion

        private void ConsultButton_Click(object sender, EventArgs e)
        {

            try
            {

                if (!CheckConnectivity.IsInternetAvailable())
                {
                    Interaction.MsgBox("No hay conección a internet..", MsgBoxStyle.Exclamation, "Aviso");
                    return;
                }

                ConsultSRIButton.Enabled = false;
                Cursor = Cursors.WaitCursor;

                if (EnterClavepanel.Visible)
                {
                    if (string.IsNullOrEmpty(ClaveAccesoTextBox.Text))
                    {
                        Microsoft.VisualBasic.
                            Interaction.MsgBox("Ingese la clave de acceso a consultar...", Title: "Importante..");
                        return;
                    }
                    string mesa = "Esta bien el tipo de ambiente en el que desea consultar..?";
                    if (Microsoft.VisualBasic.
                          Interaction.MsgBox(mesa, Title: "Responda",
                          Buttons: Microsoft.VisualBasic.MsgBoxStyle.YesNo) == Microsoft.VisualBasic.MsgBoxResult.Yes)
                    {
                        ConsultWithEnterClaveAcc(ClaveAccesoTextBox.Text, comboBox1.SelectedValue.ToString());
                    }

                    return;
                }
                if (DataGridSingned.Visible && DataGridSingned.SelectedRows.Count != 1)
                {
                    return;
                }
                _VoucherID = (int)DataGridSingned.SelectedRows[0].Cells[DataGridSingned.Columns["VoucherID"].Index].Value;
                //get String Connection

                FacturaViewModels viewmodel =
                    new FacturaViewModels(dbReflexion.Connection.ConnectionString);
                viewmodel.GetInformation(_VoucherID);
                comboBox1.SelectedValue = Convert.ToByte(viewmodel.ambiente);

                ConsultWithEnterClaveAcc(viewmodel.ClaveAcceso, comboBox1.SelectedValue.ToString());
                CRUD_database.UpdateAutorize(_VoucherID, this.viewModel.Data, estadoLabel.Text,
                      this.viewModel.fechaAutoriza.Value, LabelError.Text);
                FindXamlButton.PerformClick();
            }
            catch (Exception ex)
            {
                ConsultSRIButton.Enabled = true;
                Cursor = Cursors.Default;
                Microsoft.VisualBasic.Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace);

            }
            finally
            {
                ConsultSRIButton.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void ConsultWithEnterClaveAcc(string claveAcceso, string codAmbiente)
        {
            DeleteErrorButton.PerformClick();
            string message = string.Empty;
            SRI sRI = new SRI(Notifycation: true);
            try
            {
                xmlGridView.DataFilePath = string.Empty;

                message = sRI.ConsultarComprobante(claveAcceso, codAmbiente);
                var data = sRI.GetAutorizacions();
                if (data == null || data.Count == 0)
                {
                    this.claveAcceso = string.Empty;
                    this.estadoLabel.Text = "No se obtubo información";
                    this.ClaveAccesoLabel.Text = this.claveAcceso;
                    message = "No se obtubo informacíon con la clave de acceso:" + "\n" +
                        ClaveAccesoTextBox.Text + "\n" +
                        "Ambiente: " + comboBox1.Text;

                    return;
                }


                xmlFirmado = XMLSerializers.Serialize(data[0], "");
                if (xmlFirmado.Length == 0)
                {
                    return;
                }
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlFirmado);
                doc.Save(GetPathMyDoc());
                xmlGridView.ViewMode = XmlGridView.VIEW_MODE.XML;
                xmlGridView.DataFilePath = GetPathMyDoc();
                estadoLabel.Text = sRI.GetEstado();

                this._fechaAutoriza = sRI.GetFechaAutoriza();
                this._NumeroAutoriza = sRI.GetNumAutoriza();

                autorizacions = new List<autorizacion>();
                autorizacions = sRI.GetAutorizacions();
                this.viewModel = new FacturaViewModels("");
                this.viewModel.SetInformatio(xmlFirmado, null);

            }
            catch (Exception ex)
            {

                Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace, MsgBoxStyle.Critical, "Error");
            }
            finally
            {
                sRI = null;
                if (!string.IsNullOrEmpty(message))
                {
                    panelError.Height = 200;
                    LabelError.Text = message;
                    LabelError.ForeColor = Color.Red;
                    panelError.AutoScroll = true;
                }
                else
                {
                    DeleteErrorButton.PerformClick();
                }
            }

        }

        private string GetPathMyDoc()
        {
            string filePath = System.Environment.GetFolderPath(
                        System.Environment.SpecialFolder.UserProfile);
            return string.Format("{0}\\View1235.xml", filePath);
        }



        private void TipoSatetButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ContextMenuStrip menu = this.MenuEstadoFilter;
                menu.Show(Cursor.Position);
            }

        }

        private void MenuFechaEmisButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ContextMenuStrip menu = this.MenuFechaEmisiFilter;
                menu.Show(Cursor.Position);
            }
        }

        private void CmbOptionSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AcceptButton = null;
            string sql = string.Empty;
            if (CmbOptionSelect.SelectedIndex > -1)
            {
                if (CmbOptionSelect.Text.Contains("No Generadas"))
                {
                    txtbuscar.Enabled = false;
                    bntBuscar.Enabled = false;

                    if (MostarFactura_NoGenered(sql))
                    {

                    }

                }
                else if (CmbOptionSelect.Text.Contains("Fecha del documento"))
                {
                    txtbuscar.Visible = false;
                    bntBuscar.Enabled = true;
                    PanelImputDate.Visible = true;
                }
                else
                {
                    txtbuscar.Enabled = true;
                    txtbuscar.Visible = true;
                    bntBuscar.Enabled = true;
                    PanelImputDate.Visible = false;
                }
            }
        }

        private bool MostarFactura_NoGenered(string sql)
        {
            sql = @"Select Top(200) 
                    fv.idFactVenta,fv.Num_Factu,c.Nombres As[Cliente], fv.fechaDesde, fv.fechaHasta, 
                    FV.Base00Iva, FV.Base12Iva, FV.Iva, cast(FV.OtroValor + FV.Total As Decimal(18, 2)) as [Total] 
                    ,td.Nom_Docu as [Tipo_Documento]  
                    from FacturaVenta  as fv
                    INNER Join [dbo].[ClienteName] AS c ON fv.idCliente = c.idCliente
                    INNER Join [stm].[FormaPago] AS fp ON fv.idFormaPago = fp.idformaPago
                    INNER Join [stm].[TypoDocumento] AS td on td.idTypoDocu = fv.idTypoDocument
                    left outer join [ElectronicBillingDB].[dbo].[Voucher] as s on fv.idFactVenta = s.IDRelationData
                    where(( s.TypesVoucherID is null) and  (fv.idTypoDocument =1))
                    order by fv.idFactVenta desc";

            try
            {
                DataGridEcommerce.DataSource = null;
                panelView.Controls.Clear();
                using (SqlConnection cnn = new SqlConnection(dbReflexion.Connection.ConnectionString))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.ExecuteNonQuery();
                        using (SqlDataAdapter dat = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            dat.Fill(dt);
                            DataGridEcommerce.DataSource = dt;
                            panelView.Controls.Add(DataGridEcommerce);
                            return true;
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                Microsoft.VisualBasic.Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace);
                return false;
            }
        }

        private bool MostarFactura_Select(string SrtWhere)
        {
            this.isLoated = false;
            string sql = string.Empty;
            sql = "Select Top(200) fv.idFactVenta,fv.Num_Factu,c.Nombres As[Cliente], fv.fechaDesde, fv.fechaHasta,  ";
            sql = sql + "FV.Base00Iva, FV.Base12Iva, FV.Iva, cast(FV.OtroValor + FV.Total As Decimal(18, 2))  as [Total] ";
            sql = sql + ", td.Nom_Docu as [Tipo_Documento]";
            sql = sql + "From [dbo].[FacturaVenta] AS fv ";
            sql = sql + "INNER Join [dbo].[ClienteName] AS c ON fv.idCliente = c.idCliente ";
            sql = sql + "INNER Join [stm].[FormaPago] AS fp ON fv.idFormaPago = fp.idformaPago ";
            sql = sql + "INNER Join [stm].[TypoDocumento] AS td on td.idTypoDocu = fv.idTypoDocument ";
            sql = sql + SrtWhere;

            try
            {
                DataGridEcommerce.DataSource = null;
                panelView.Controls.Clear();
                using (SqlConnection cnn = new SqlConnection(dbReflexion.Connection.ConnectionString))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.ExecuteNonQuery();
                        using (SqlDataAdapter dat = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            dat.Fill(dt);
                            DataGridEcommerce.DataSource = dt;
                            panelView.Controls.Add(DataGridEcommerce);
                            return true;
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                Microsoft.VisualBasic.Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace);
                return false;
            }
        }

        private void bntBuscar_Click(object sender, EventArgs e)
        {
            isLoated = false;
            try
            {
                string sql = "";
                switch (CmbOptionSelect.Text)
                {
                    case "Número de Factura":
                        sql = "WHERE (fv.Num_Factu Like '%" + txtbuscar.Text + "%') ";
                        break;
                    case "Cliente":
                        sql = FilterToName();
                        break;
                    case "Ruc (o) C.I":
                        sql = "WHERE (c.Ruc_Ci LIKE '%" + txtbuscar.Text + "%') ";
                        break;
                    case "No Impresas":
                        sql = "WHERE (fv.Impreso = 0)";
                        break;
                    case "Fecha del documento":
                        DateTime dateStar = DateTimePickerStar.Value.Date;
                        DateTime dateFind = DateTimePickerEnd.Value.Date.AddDays(1);

                        sql = "WHERE (fv.fechaDesde >= '" + dateStar.ToString("yyyy/MM/dd") + "') AND (fv.fechaDesde < '" + dateFind.ToString("yyyy/MM/dd") + "') ";
                        break;
                    case "ID":
                        sql = "WHERE (fv.idFactVenta >= " + txtbuscar.Text + ") AND (fv.idFactVenta <= " + txtbuscar.Text + ") ";
                        break;
                    default:
                        break;
                }
                if (sql.Length > 0)
                {
                    if (MostarFactura_Select(sql))
                    {

                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Microsoft.VisualBasic.Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                isLoated = true;
            }

        }

        private string FilterToName()
        {
            ResponseSpliter mySpliter = Funciones.GenerateSpliter(txtbuscar.Text);
            string sql = "";
            switch (mySpliter.Spliter.Count())
            {
                case 1:
                    sql = "WHERE (c.Nombres LIKE '%" + mySpliter.Spliter[0] + "%') ";
                    break;
                case 2:
                    sql = "WHERE (c.Nombres LIKE '%" + mySpliter.Spliter[0] + "%') and (c.Nombres LIKE '%" + mySpliter.Spliter[1] + "%')  ";
                    break;
                case 3:
                    sql = "WHERE (c.Nombres LIKE '%" + mySpliter.Spliter[0] + "%') and (c.Nombres LIKE '%" + mySpliter.Spliter[1] + "%') and (c.Nombres LIKE '%" + mySpliter.Spliter[2] + "%') ";
                    break;
                case 4:
                    sql = "WHERE (c.Nombres LIKE '%" + mySpliter.Spliter[0] + "%') and (c.Nombres LIKE '%" + mySpliter.Spliter[1] + "%') and (c.Nombres LIKE '%" + mySpliter.Spliter[2] + "%') ";
                    break;
                default:
                    break;
            }
            return sql;
        }
        private void FindXamlButton_Click(object sender, EventArgs e)
        {
            int idFactur;
            try
            {
                Cursor = Cursors.WaitCursor;

                if (panelResum.Visible)
                {

                    return;
                }

                if (CodInternoTextBox.Visible)
                {
                    if (int.TryParse(CodInternoTextBox.Text, out idFactur))
                    {
                        GetDataListByID(idFactur);
                    };
                    return;
                }

                if (!string.IsNullOrWhiteSpace(ClienteEspecifLabel.Text))
                {
                    var idString = ClienteEspecifLabel.Tag.ToString();
                    if (int.TryParse((string)idString, out idFactur))
                    {
                        FilterMyCliente(idFactur);
                    };
                    return;
                }


                if (!string.IsNullOrEmpty(_FilterState) && !string.IsNullOrEmpty(_FilterDate))
                {
                    GetDataListByStateAndDate(_FilterState, new DateTime());
                }
                else if (!string.IsNullOrEmpty(_FilterDate))
                {
                    GetDataListByDate();
                }
                else if (!string.IsNullOrEmpty(_FilterState))
                {
                    GetDataListByState(_FilterState);
                }
                else
                {
                    GetDataListAsync();
                }


            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                Interaction.MsgBox(ex.Message + "\n" +
                    ex.StackTrace, MsgBoxStyle.Critical, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }

        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtbuscar.Text.Length >= 4)
            {
                this.AcceptButton = bntBuscar;
            }
            else
            {
                this.AcceptButton = null;
            }
        }

        private void DateTimePickerStar_ValueChanged(object sender, EventArgs e)
        {
            this.AcceptButton = bntBuscar;
        }

        private void DateTimePickerEnd_ValueChanged(object sender, EventArgs e)
        {
            this.AcceptButton = bntBuscar;
        }

        private void GeneratedXMlButton_EnabledChanged(object sender, EventArgs e)
        {
            panel3.Visible = GeneratedXMlButton.Enabled;
        }

        private void DetermFactButton_Click(object sender, EventArgs e)
        {
            if (DataGridEcommerce.SelectedRows.Count != 1)
            {
                return;
            }
            try
            {

                int idFac = (int)DataGridEcommerce.SelectedRows[0].Cells[0].Value;

                if (Interaction.MsgBox("Esta seguro de cambiar a factura",
                    Buttons: MsgBoxStyle.YesNo, Title: "Responda..") != MsgBoxResult.Yes)
                {
                    return;
                }


                using (SqlConnection cnn = new SqlConnection(dbReflexion.Connection.ConnectionString))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("", cnn))
                    {
                        cmd.CommandText = "Update [dbo].[FacturaVenta]" + "\n" +
                                "set idTypoDocument = 1" + "\n" +
                                "Where (idFactVenta = " + idFac + ")";
                        cmd.ExecuteNonQuery();
                    }
                }


            }
            catch (Exception ex)
            {

                Microsoft.VisualBasic.Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace);

            }
        }

        private async void GeneratedXMlButton_Click(object sender, EventArgs e)
        {
            idFactura = 0;
            if (DataGridEcommerce.SelectedRows.Count != 1)
            {
                return;
            }

            try
            {
                GeneratedXMlButton.Enabled = false;
                ClaveAccesoLabel.Text = string.Empty;

                DeleteErrorButton.PerformClick();

                idFactura = (int)DataGridEcommerce.SelectedRows[0].Cells[0].Value;
                string tipoDocument = (string)DataGridEcommerce.SelectedRows[0].Cells["Tipo_Documento"].Value;
                if (!tipoDocument.Contains("Factura"))
                {
                    Interaction.MsgBox("El documento debe ser de tipo factura.");
                    return;
                }
                Cursor = Cursors.WaitCursor;
                GeneratedXMlButton.Enabled = false;
                if (this.comboBox1.SelectedValue == null)
                {
                    Interaction.MsgBox("Seleccione el tipo de mabiente..");
                    return;
                }
                if (Interaction.MsgBox(
                 "Está seguro de generara en tipo de ambiente: " + comboBox1.Text
                 , MsgBoxStyle.YesNo, "Responda..") != MsgBoxResult.Yes)
                {
                    return;
                }

                using (FacturaXMLWithId generaFac = new FacturaXMLWithId(this.comboBox1.SelectedValue.ToString(),
                    idFactura, SettingObject.EcommerceActive.CommerceId))
                {
                    xmlFirmado = await NewMethod(generaFac);
                    _fechaEmision = generaFac.GetFechaEmision();
                    doc = new XmlDocument();
                    doc.LoadXml(xmlFirmado);
                    claveAcceso = doc.GetElementsByTagName("claveAcceso")[0].InnerText;
                    ClaveAccesoLabel.Text = claveAcceso;
                    codDoc = doc.GetElementsByTagName("codDoc")[0].InnerText;
                    ambiente = doc.GetElementsByTagName("ambiente")[0].InnerText;
                    if (string.IsNullOrEmpty(claveAcceso))
                    {
                        return;
                    }
                    doc.Save(GetPathMyDoc());
                    xmlGridView.DataFilePath = GetPathMyDoc();
                    estadoLabel.Text = "Generado";
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                GeneratedXMlButton.Enabled = true;
                Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                Cursor = Cursors.Default;
                GeneratedXMlButton.Enabled = true;
            }

        }

        private async Task<string> NewMethod(FacturaXMLWithId generaFac)
        {
            return await generaFac.GetXmlFactura();
        }

        private void ViewFileButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "XML Files (*.xml)|*.xml|PDF Files (*.pdf)|*.pdf";
                fileDialog.Title = "Seleccione el archivo.";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    idFactura = 0;
                    xmlGridView.DataFilePath = fileDialog.FileName.ToString();
                }
            }
        }

        private void GetNumFactButton_Click(object sender, EventArgs e)
        {
            if (DataGridEcommerce.SelectedRows.Count != 1)
            {
                return;
            }

            idFactura = (int)DataGridEcommerce.SelectedRows[0].Cells[0].Value;
            string Numfactur = OntengoNumero();
            if (string.IsNullOrEmpty(Numfactur))
            {
                return;
            }
            if (Interaction.MsgBox(
                "Se genero el siguiente numero de documento:" + "\n" + Numfactur + "\n" + "\n" +
                "Desea guardar este numero en base de datos..?"
                , MsgBoxStyle.YesNo, "Responda..") == MsgBoxResult.Yes)
            {
                if (SaveNroDucumento(idFactura, Numfactur))
                {
                    bntBuscar.PerformClick();
                }
            }

        }

        private bool SaveNroDucumento(int _idFactura, string num_Factur)
        {
            string sql = string.Empty;
            try
            {
                using (SqlConnection cnn = new SqlConnection(dbReflexion.Connection.ConnectionString))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("", cnn))
                    {
                        sql = @"update [dbo].[FacturaVenta]
                                set [Num_Factu] =@Num_Factu
                                where idFactVenta =@idFactVenta";

                        cmd.Parameters.Add("@idFactVenta", SqlDbType.Int);
                        cmd.Parameters.Add("@Num_Factu", SqlDbType.VarChar);

                        cmd.Parameters["@idFactVenta"].Value = _idFactura;
                        cmd.Parameters["@Num_Factu"].Value = num_Factur;

                        cmd.CommandText = sql;
                        int excec = cmd.ExecuteNonQuery();

                        if (excec > 0)
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace);
                return false;
            }
        }

        private string OntengoNumero()
        {
            string sql = string.Empty;
            try
            {
                using (SqlConnection cnn = new SqlConnection(dbReflexion.Connection.ConnectionString))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("[dbo].[prcReturnNumFactury]", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@codTerminal", SqlDbType.Char, 8);
                        cmd.Parameters.Add("@NameDocumento", SqlDbType.VarChar);

                        cmd.Parameters["@codTerminal"].Value = _CodTerminal;
                        cmd.Parameters["@NameDocumento"].Value = "Factura";

                        int excec = cmd.ExecuteNonQuery();
                        sql = @" select tc.NumRetur
                              from  [stm].[TerminalConfi] as tc
		                             inner join [stm].[Terminal] as t on t.idTerminal  = tc.idTerminal
		                             inner join [stm].[TypoDocumento] as d on tc.idTypoDocumento  = d.idTypoDocu
                              where ((t.codTerminal=@codTerminal) and (d.Nom_Docu=@NameDocumento))";

                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        using (SqlDataAdapter dat = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            dat.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                return (string)dt.Rows[0]["NumRetur"];
                            }
                            return string.Empty;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace);
                return string.Empty;
            }
        }

        private void CkeckClaveInDataDaseButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.claveAcceso))
            {
                Interaction.MsgBox("No se obtubo la clave para revisar..");
                return;
            }
            if (CheckClaveAcceso(this.claveAcceso))
            {
                SaveXMLButton.Enabled = false;
                Interaction.MsgBox("Ya está registrada esta clave..", MsgBoxStyle.Exclamation, Title: "Importante");
            }
            else
            {
                SaveXMLButton.Enabled = true;
                Interaction.MsgBox("No esta registrada esta clave de acceso..", MsgBoxStyle.Information, "Aviso");
            }

        }

        private bool CheckClaveAcceso(string claveAcceso)
        {
            string sql = string.Empty;
            try
            {
                using (SqlConnection cnn = new SqlConnection(dbReflexion.Connection.ConnectionString))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("", cnn))
                    {
                        cmd.CommandType = CommandType.Text;

                        sql = "Select top(1) VoucherID from [" + _NameDatabase + "].[dbo].[Voucher]" + "\n" +
                               " where ClaveAcceso =@ClaveAcceso";

                        cmd.Parameters.Add("@ClaveAcceso", SqlDbType.VarChar);

                        cmd.Parameters["@ClaveAcceso"].Value = claveAcceso;

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        using (SqlDataAdapter dat = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                dat.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    this._VoucherID = (int)dt.Rows[0]["VoucherID"];
                                }
                                return dt.Rows.Count > 0;
                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace);
                return false;
            }
        }

        private void ClaveAccesoTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ClaveAccesoTextBox.Text.Length >= 49)
            {
                this.AcceptButton = ConsultSRIButton;
            }
            else
            {
                this.AcceptButton = null;
            }
        }

        private void SaveXMLButton_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(xmlFirmado))
            {
                Interaction.MsgBox("El documento esta vacio..", MsgBoxStyle.Information, "Aviso");
                return;
            }

            if (Interaction.MsgBox("Esta seguro de guardar en base de datos..",
                MsgBoxStyle.YesNo, "Responda") != MsgBoxResult.Yes)
            {
                return;
            }
            bool result = false;
            string nameDtg = panelView.Controls[0].Name;
            try
            {
                Cursor = Cursors.WaitCursor;

                if (estadoLabel.Text.Contains("Generado") || nameDtg.Contains("DataGridEcommerce"))
                {
                    if (string.IsNullOrEmpty(ClaveAccesoLabel.Text))
                    {
                        Interaction.MsgBox("No se ha determinado la clave de acceso..", MsgBoxStyle.Information, "Aviso");
                        return;
                    }

                    if (CheckClaveAcceso(ClaveAccesoLabel.Text))
                    {
                        Interaction.MsgBox("Esta clave de acceso ya esta registra.", MsgBoxStyle.Information, "Aviso");
                        return;
                    }


                    DateTime dtStar = (DateTime)DataGridEcommerce.SelectedRows[0]
                       .Cells[DataGridEcommerce.Columns["fechaDesde"].Index].Value;
                    if (CRUD_database.InsertGenerated(this.claveAcceso, doc,
                        this.idFactura, "01", dtStar, estadoLabel.Text) > 0)
                    {
                        result = true;
                    };
                }
                else if (estadoLabel.Text.Contains("Firmado"))
                {
                    result = CRUD_database.UpdateSingned(_VoucherID, xmlFirmado);

                }
                else if (estadoLabel.Text.Contains("AUTORIZADO") || estadoLabel.Text.Contains("NO AUTORIZADO"))
                {

                }
                if (result)
                {
                    Interaction.MsgBox("Operación exitosa..", MsgBoxStyle.Information, "Aviso");
                    FindXamlButton.PerformClick();
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace, MsgBoxStyle.Critical, "Error");
            }

            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void SignedButton_Click(object sender, EventArgs e)
        {
            if (DataGridSingned.SelectedRows.Count != 1)
            {
                return;
            }
            try
            {
                string estado = (string)DataGridSingned.SelectedRows[0]
                    .Cells[DataGridSingned.Columns["Estado"].Index].Value;

                if (!estado.Contains("Generado"))
                {
                    Interaction.MsgBox("Debe estar es estado generado",
                        MsgBoxStyle.Critical, "Error");
                    return;
                }

                _VoucherID = (int)DataGridSingned.SelectedRows[0].Cells[0].Value;
                Cursor = Cursors.WaitCursor;
                SignedButton.Enabled = false;
                FacturaViewModels viewmodel =
                new FacturaViewModels(dbReflexion.Connection.ConnectionString);
                viewmodel.GetInformation(_VoucherID);
                this.claveAcceso = viewmodel.ClaveAcceso;
                this.ambiente = viewmodel.ambiente;

                ClaveAccesoLabel.Text = this.claveAcceso;


                TokensValidos token = null;
                try
                {
                    if (String.IsNullOrWhiteSpace(SettingObject.SignatureOptios.TOKEN))
                    {
                        Interaction.MsgBox("Debe configurar elgùn token kalido para firma electronica..");
                        return;
                    }
                    token = TokensValidos.obtenerToken(SettingObject.SignatureOptios.TOKEN, SettingObject.SignatureOptios.THUMBPRINT);
                }
                catch (Exception ex)
                {

                    Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace, MsgBoxStyle.Critical, "Error");
                    return;
                }


                xmlFirmado = SigningProcess.GeneratedSignigWithPlaneText(viewmodel.InnerXml, token);
                if (!string.IsNullOrEmpty(xmlFirmado))
                {
                    CRUD_database.UpdateSingned(_VoucherID, xmlFirmado);
                    doc = new XmlDocument();
                    doc.LoadXml(xmlFirmado);
                    doc.Save(GetPathMyDoc());
                    xmlGridView.DataFilePath = "";
                    xmlGridView.DataFilePath = GetPathMyDoc();
                    estadoLabel.Text = "Firmado";
                    FindXamlButton.PerformClick();
                }
                else
                {
                    estadoLabel.Text = "No firmado";
                }

            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace, MsgBoxStyle.Critical, "Error");
            }
            finally
            {

                Cursor = Cursors.Default;
                SignedButton.Enabled = true;
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            GeneratedXMlButton.Enabled = false;
            SignedButton.Enabled = false;
            SubmitSRIButton.Enabled = false;
            // ViewXmlButton.Enabled = false;
            //ViewPdfButton.Enabled = false;
            ConsultSRIButton.Enabled = false;
            DeleteNoAutorizeButton.Enabled = false;
            SendMailButton.Enabled = false;
            if (DataGridSingned.SelectedRows.Count != 1)
            {
                return;
            }

            string estado = (string)DataGridSingned.SelectedRows[0]
                .Cells[DataGridSingned.Columns["Estado"].Index].Value;
            switch (estado)
            {
                case "Generado":
                    SignedButton.Enabled = true;
                    break;
                case "Firmado":
                    SubmitSRIButton.Enabled = true;
                    break;
                case "Enviado":
                case "EN PROCESO":
                    ConsultSRIButton.Enabled = true;
                    break;
                case "AUTORIZADO":
                    DeleteNoAutorizeButton.Enabled = false;
                    SendMailButton.Enabled = true;
                    break;
                case "DEVUELTA":
                case "NO AUTORIZADO":
                    DeleteNoAutorizeButton.Enabled = true;
                    //ViewXmlButton.Enabled = true;
                    // ViewPdfButton.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        private void SubmitSRIButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckConnectivity.IsInternetAvailable())
                {
                    Interaction.MsgBox("No hay conección a internet..", MsgBoxStyle.Exclamation, "Aviso");
                    return;
                }

                if (DataGridSingned.SelectedRows.Count != 1)
                {
                    return;
                }
                try
                {
                    string estado = (string)DataGridSingned.SelectedRows[0]
                        .Cells[DataGridSingned.Columns["Estado"].Index].Value;

                    if (!estado.Contains("Firmado"))
                    {
                        Interaction.MsgBox("El documento no  esta firmado",
                            MsgBoxStyle.Critical, "Error");
                        return;
                    }

                    _VoucherID = (int)DataGridSingned.SelectedRows[0].Cells[0].Value;
                    Cursor = Cursors.WaitCursor;
                    SubmitSRIButton.Enabled = false;

                    FacturaViewModels viewmodel =
                    new FacturaViewModels(dbReflexion.Connection.ConnectionString);
                    viewmodel.GetInformation(_VoucherID);

                    this.claveAcceso = viewmodel.ClaveAcceso;
                    this.ambiente = viewmodel.ambiente;
                    this.comboBox1.SelectedValue = Convert.ToByte(this.ambiente);

                    xmlFirmado = "<?xml version=" + "\"1.0\"" + " encoding=" + "\"UTF-8\"" + "?>" + viewmodel.Data;
                    ClaveAccesoLabel.Text = this.claveAcceso;
                    doc = new XmlDocument();
                    doc.LoadXml(xmlFirmado);
                    doc.Save(GetPathMyDoc());

                    SRI sRI = new SRI(Save: false, Notifycation: true, files: null);


                    string result = sRI.EnviarComprobante(xmlFirmado, this.ambiente);

                    if (sRI.GetEstado().Contains("RECIBIDA"))
                    {
                        CRUD_database.UpdateEnter(_VoucherID);
                        xmlGridView.DataFilePath = "";
                        estadoLabel.Text = "Enviado";
                        panelError.Height = 100;
                        LabelError.Text = " ☻  Enviado..→";
                        LabelError.ForeColor = Color.Green;
                        FindXamlButton.PerformClick();
                    }
                    else if (sRI.GetEstado().Contains("DEVUELTA"))
                    {
                        estadoLabel.Text = "DEVUELTA";
                        CRUD_database.UpdateRejected(_VoucherID, result);
                        xmlGridView.DataFilePath = "";

                        panelError.Height = 100;
                        panelError.AutoScroll = true;
                        LabelError.Text = " ๏̯͡๏﴿  DEVUELTA !!" + "\n" + result;
                        LabelError.ForeColor = Color.Red;
                        FindXamlButton.PerformClick();
                    }
                    else if (!string.IsNullOrWhiteSpace(result))
                    {
                        panelError.Height = 200;
                        LabelError.Text = result;
                        LabelError.ForeColor = Color.Red;
                    }
                    sRI = null;
                }
                catch (Exception ex)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace,
                       icon: MessageBoxIcon.Error,
                       buttons: MessageBoxButtons.OK,
                       caption: "Error");

                }
                finally
                {
                    SubmitSRIButton.Enabled = true;
                    Cursor = Cursors.Default;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace,
                                icon: MessageBoxIcon.Error,
                                buttons: MessageBoxButtons.OK,
                                caption: "Error");
            }
        }

        private void DeleteErrorButton_Click(object sender, EventArgs e)
        {
            panelError.Height = 30;
            panelError.AutoScroll = false;
            LabelError.Text = string.Empty;
        }
        private void GeneratedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TipoStateButton.Text = GeneratedToolStripMenuItem.Text;
        }

        private void QuitFilterButton_Click(object sender, EventArgs e)
        {
            FilterMunuSignedPanel.Height = 30;
            _FilterDate = string.Empty;
            _FilterState = string.Empty;
        }

        private void SingnedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TipoStateButton.Text = SingnedToolStripMenuItem.Text;
        }

        private void SubmidToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TipoStateButton.Text = SubmidToolStripMenuItem1.Text;
        }

        private void autorizadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TipoStateButton.Text = autorizadosToolStripMenuItem.Text;
        }

        private void noAutorizadosToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            TipoStateButton.Text = noAutorizadosToolStripMenuItem2.Text;
        }

        private void enProcesoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TipoStateButton.Text = enProcesoToolStripMenuItem.Text;
        }

        private void CodInternoButton_Click(object sender, EventArgs e)
        {
            if (CodInternoButton.Text.Contains("Cualquier código"))
            {
                CodInternoTextBox.Visible = true;
                CodInternoButton.Text = "Código específico";
                cualuierClienteToolStripMenuItem.PerformClick();
                todasToolStripMenuItem.PerformClick();
                cualquierFechaToolStripMenuItem.PerformClick();
                ClienteFindButton.Enabled = false;
                TipoStateButton.Enabled = false;
                FechaEmisButton.Enabled = false;
                FilterMunuSignedPanel.Height = 60;
            }
            else
            {
                CodInternoTextBox.Visible = false;
                CodInternoButton.Text = "Cualquier código";
                ClienteFindButton.Enabled = true;
                TipoStateButton.Enabled = true;
                FechaEmisButton.Enabled = true;
            }
            if (CodInternoTextBox.Visible && FilterMunuSignedPanel.Height == 35)
            {
                FilterMunuSignedPanel.Height = 60;
                CodInternoTextBox.Select();
            }
            else if (!CodInternoTextBox.Visible && FilterMunuSignedPanel.Height == 60)
            {
                FilterMunuSignedPanel.Height = 35;
            }
        }

        private void DesdelinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StardateTimePicker1.Open();
        }

        private void StardateTimePicker1_Enter(object sender, EventArgs e)
        {

        }

        private void StardateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (isLoated)
            {
                DesdelinkLabel.Text = StardateTimePicker1.Value.ToString("dd/MM/yyyy");
            }
        }

        private void EndDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (isLoated)
            {
                HastalinkLabel.Text = EndDateTimePicker.Value.ToString("dd/MM/yyyy");
            }
        }

        private void HastalinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EndDateTimePicker.Open();
        }


        private void cualquierFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FechaEmisButton.Text = cualquierFechaToolStripMenuItem.Text;
            OcultarFiltroFecha();
        }

        private void OcultarFiltroFecha(bool activate = false)
        {
            if (activate)
            {
                FilterMunuSignedPanel.Height = 70;
                DesdeLabel.Visible = true;
                DesdelinkLabel.Visible = true;
                HastaLabel.Visible = true;
                HastalinkLabel.Visible = true;
            }
            else
            {
                DesdeLabel.Visible = false;
                DesdelinkLabel.Visible = false;
                HastaLabel.Visible = false;
                HastalinkLabel.Visible = false;
            }

        }

        private void HoyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FechaEmisButton.Text = HoyToolStripMenuItem1.Text;
            OcultarFiltroFecha();
            StardateTimePicker1.Value = DateTime.Now.Date;
            EndDateTimePicker.Value = DateTime.Now.Date.AddDays(1);
        }
        private void MenuFechaEmisiFilter_Opening(object sender, CancelEventArgs e)
        {
            foreach (ToolStripItem item in MenuFechaEmisiFilter.Items)
            {
                if (item.Text == FechaEmisButton.Text)
                {
                    item.Image = Properties.Resources.ok_12;
                    item.ImageScaling = ToolStripItemImageScaling.None;
                }
                else
                {
                    item.Image = null;
                }
            }
        }

        private void ThisWeekStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                FechaEmisButton.Text = ThisWeekStripMenuItem.Text;
                OcultarFiltroFecha();

                DateTime date = DateTime.Now.Date;

                int dia = GetDayIdOfWeek(date);
                dia = dia - 1;
                DateTime inicio = date.AddDays(dia * -1);
                StardateTimePicker1.Value = inicio;
                EndDateTimePicker.Value = inicio.AddDays(7);
            }
            catch (Exception ex)
            {

                Interaction.MsgBox(ex.Message);
            }

        }


        private void LastWeekStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                FechaEmisButton.Text = LastWeekStripMenuItem3.Text;
                OcultarFiltroFecha();
                DateTime date = DateTime.Now.Date;

                int dia = GetDayIdOfWeek(date);
                dia = dia - 1;
                DateTime inicio = date.AddDays(dia * -1);
                StardateTimePicker1.Value = inicio.AddDays(-7);
                EndDateTimePicker.Value = StardateTimePicker1.Value.AddDays(7);
            }
            catch (Exception ex)
            {

                Interaction.MsgBox(ex.Message);
            }


        }

        private void LastMonthStripMenuItem4_Click(object sender, EventArgs e)
        {
            try
            {

                FechaEmisButton.Text = LastMonthStripMenuItem4.Text;
                OcultarFiltroFecha();
                DateTime date = DateTime.Now.Date;
                int year = date.Year;
                int month = date.Month;

                if (date.Month == 1)
                {
                    year = year - 1;
                    month = 12;
                }

                StardateTimePicker1.Value = new DateTime(year, month - 1, 1);

                //Y de la siguiente forma obtenemos el ultimo dia del mes
                //agregamos 1 mes al objeto anterior y restamos 1 día.
                EndDateTimePicker.Value = StardateTimePicker1.Value.AddMonths(1);
            }
            catch (Exception ex)
            {

                Interaction.MsgBox(ex.Message);
            }


        }

        private void esteAñoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FechaEmisButton.Text = esteAñoToolStripMenuItem.Text;
                OcultarFiltroFecha();
                DateTime date = DateTime.Now.Date;

                StardateTimePicker1.Value = new DateTime(date.Year, 1, 1);

                //Y de la siguiente forma obtenemos el ultimo dia del mes
                //agregamos 1 mes al objeto anterior y restamos 1 día.
                EndDateTimePicker.Value = StardateTimePicker1.Value.AddMonths(12);

            }
            catch (Exception ex)
            {

                Interaction.MsgBox(ex.Message);
            }

        }

        private void determinarPeriodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FechaEmisButton.Text = determinarPeriodoToolStripMenuItem.Text;
            OcultarFiltroFecha(true);
        }

        private int GetDayIdOfWeek(DateTime date)
        {
            int dayIndex = 0;
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    dayIndex = 7;
                    break;
                case DayOfWeek.Monday:
                    dayIndex = 1;
                    break;
                case DayOfWeek.Tuesday:
                    dayIndex = 2;
                    break;
                case DayOfWeek.Wednesday:
                    dayIndex = 3;
                    break;
                case DayOfWeek.Thursday:
                    dayIndex = 4;
                    break;
                case DayOfWeek.Friday:
                    dayIndex = 5;
                    break;
                case DayOfWeek.Saturday:
                    dayIndex = 6;
                    break;
                default:
                    break;
            }
            return dayIndex;
        }
        private void MenuEstadoFilter_Opening(object sender, CancelEventArgs e)
        {
            foreach (ToolStripItem item in MenuEstadoFilter.Items)
            {
                if (item.Text == TipoStateButton.Text)
                {
                    item.Image = Properties.Resources.ok_12;
                    item.ImageScaling = ToolStripItemImageScaling.None;
                }
                else
                {
                    item.Image = null;
                }
            }
        }

        private void todasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TipoStateButton.Text = todasToolStripMenuItem.Text;
        }

        private void clienteEspecificoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClienteFindButton.Text = clienteEspecificoToolStripMenuItem.Text;
            try
            {
                using (ListCustomersForm viewList = new ListCustomersForm(dbReflexion.Connection.ConnectionString))
                {
                    viewList.ShowDialog();
                    if (viewList.DialogResult == DialogResult.OK)
                    {
                        int idclient = (int)viewList.dtGrid.SelectedCells[viewList.dtGrid.Columns["idCliente"].Index].Value;
                        _Email = (string)viewList.dtGrid.SelectedCells[viewList.dtGrid.Columns["mail"].Index].Value;
                        ClienteEspecifLabel.Text = (string)
                            viewList.dtGrid.SelectedCells[viewList.dtGrid.Columns["Nombres"].Index].Value + "\n" +
                            "E-mail: " + _Email;

                        ClienteEspecifLabel.Tag = idclient;
                        FilterMyCliente(idclient);
                        cualquierFechaToolStripMenuItem.PerformClick();
                        TipoStateButton.Enabled = false;
                        FechaEmisButton.Enabled = false;
                        CodInternoButton.Enabled = false;

                        FilterMunuSignedPanel.Height = 70;
                    }
                }
            }
            catch (Exception ex)
            {

                Interaction.MsgBox(ex.Message + "\n0" +
                ex.StackTrace, MsgBoxStyle.Critical, "Error"); ;
            }

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            foreach (ToolStripItem item in ClienteMenuStrip1.Items)
            {
                if (item.Text == ClienteFindButton.Text)
                {
                    item.Image = Properties.Resources.ok_12;
                    item.ImageScaling = ToolStripItemImageScaling.None;
                }
                else
                {
                    item.Image = null;
                }
            }
        }

        private void cualuierClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClienteFindButton.Text = cualuierClienteToolStripMenuItem.Text;
            ClienteEspecifLabel.Text = "";
            TipoStateButton.Enabled = true;
            FechaEmisButton.Enabled = true;
            CodInternoButton.Enabled = true;
            ClienteEspecifLabel.Tag = 0;
            _Email = string.Empty;

        }

        private void ClienteFindButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ContextMenuStrip menu = this.ClienteMenuStrip1;
                menu.Show(Cursor.Position);
            }
        }

        private void ViewPdfButton_Click(object sender, EventArgs e)
        {
            if (DataGridSingned.SelectedRows.Count != 1)
            {
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                int idVouche = (int)DataGridSingned.SelectedRows[0].Cells[0].Value;
                using (ViewPdfForm vieForm = new ViewPdfForm(Data.Enums.ViewTypePDFEnum.PDF, idVouche))
                {
                    vieForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace
                    , icon: MessageBoxIcon.Exclamation,
                   buttons: MessageBoxButtons.OK, caption: "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }

        }

        private void ViewXmlButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridSingned.SelectedRows.Count != 1)
                {
                    return;
                }
                try
                {
                    _VoucherID = (int)DataGridSingned.SelectedRows[0].Cells[0].Value;
                    Cursor = Cursors.WaitCursor;
                    ViewXmlButton.Enabled = false;

                    viewModel =
                    new FacturaViewModels(dbReflexion.Connection.ConnectionString);
                    viewModel.GetInformation(_VoucherID);

                    this.claveAcceso = viewModel.ClaveAcceso;
                    this.ambiente = viewModel.ambiente;
                    this.comboBox1.SelectedValue = Convert.ToByte(this.ambiente);

                    xmlFirmado = "<?xml version=" + "\"1.0\"" + " encoding=" + "\"UTF-8\"" + "?>" + viewModel.Data;
                    ClaveAccesoLabel.Text = this.claveAcceso;
                    doc = new XmlDocument();
                    doc.LoadXml(xmlFirmado);
                    doc.Save(GetPathMyDoc());
                    xmlGridView.DataFilePath = "";

                    xmlGridView.DataFilePath = GetPathMyDoc();


                }
                catch (Exception ex)
                {
                    Cursor = Cursors.Default;
                    Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace, MsgBoxStyle.Critical, "Error");

                }
                finally
                {
                    ViewXmlButton.Enabled = true;
                    Cursor = Cursors.Default;
                }

            }
            catch (Exception ex)
            {

                Interaction.MsgBox(ex.Message + "\n" +
                    ex.StackTrace, MsgBoxStyle.Critical, "Error");
            }
        }

        private void ViewPDFWitModelbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (viewModel != null)
                {
                    using (ViewPdfForm viewRide = new ViewPdfForm(Data.Enums.ViewTypePDFEnum.PDF, viewModel))
                    {
                        viewRide.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {

                Interaction.MsgBox(ex.Message + "\n" +
                    ex.StackTrace, MsgBoxStyle.Critical, "Error");
            }

        }

        private void ViewSingnedList_Click(object sender, EventArgs e)
        {
            try
            {
                X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly | OpenFlags.MaxAllowed);

                X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                store.Close();
                //seleccion solo los token validos segun  fecha de expiration
                string title = "Listado de firmas..";
                string message = "Se muestra solo firmas validas segun su fecha de caducidad..";
                IntPtr windowHandle = this.Handle;

                X509Certificate2Collection fcollection = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(fcollection, title, message, X509SelectionFlag.MultiSelection, windowHandle);

            }
            catch (Exception ex)
            {

                Interaction.MsgBox(ex.Message + "\n" +
                    ex.StackTrace, MsgBoxStyle.Critical, "Error");
            }
        }

        private void DeleteNoAutorizeButton_Click(object sender, EventArgs e)
        {
            if (DataGridSingned.SelectedRows.Count != 1)
            {
                return;
            }
            try
            {
                if (Interaction.MsgBox("Está seguro de borra?",
                MsgBoxStyle.YesNo, "Responda") != MsgBoxResult.Yes)
                {
                    return;
                }
                Cursor = Cursors.WaitCursor;
                int idVouche = (int)DataGridSingned.SelectedRows[0].Cells[0].Value;
                if (CRUD_database.DeleteSingned(idVouche))
                {
                    FindXamlButton.PerformClick();
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                Interaction.MsgBox(ex.Message + "\n" +
                    ex.StackTrace, MsgBoxStyle.Critical, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void CodInternoTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CodInternoTextBox.Text))
            {
                this.AcceptButton = FindXamlButton;
            }
            else
            {
                this.AcceptButton = null;
            }
        }

        private void TipoStateButton_TextChanged(object sender, EventArgs e)
        {
            if (TipoStateButton.Text.Contains("Todas"))
            {
                _FilterState = string.Empty;
            }
            else
            {
                _FilterState = TipoStateButton.Text;
            }
            FindXamlButton.PerformClick();
        }

        private void FechaEmisButton_TextChanged(object sender, EventArgs e)
        {
            if (FechaEmisButton.Text.Contains("Cualquier fecha"))
            {
                _FilterDate = string.Empty;
            }
            else
            {
                _FilterDate = FechaEmisButton.Text;
            }
        }

        private void esteMesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FechaEmisButton.Text = esteMesToolStripMenuItem.Text;
            OcultarFiltroFecha();

            DateTime date = DateTime.Now.Date;

            StardateTimePicker1.Value = new DateTime(date.Year, date.Month, 1);

            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            EndDateTimePicker.Value = StardateTimePicker1.Value.AddMonths(1);

        }
        private void SendMailButton_Click(object sender, EventArgs e)
        {

            if (DataGridSingned.SelectedRows.Count != 1)
            {
                return;
            }
            if (string.IsNullOrEmpty(this._Email))
            {
                MessageBox.Show("No existe email para enviar");
                return;
            }
            List<string> listMail = new List<string>();

            string[] mails = this._Email.Split(';');
            for (int i = 0; i < mails.Length; i++)
            {
                listMail.Add(mails[i]);
            }
            try
            {
                if (listMail == null || listMail.Count == 0)
                {
                    MessageBox.Show("No existe destinatarios...");
                    return;
                }
                Cursor = Cursors.WaitCursor;
                SendMailButton.Enabled = false;

                _VoucherID = (int)DataGridSingned.SelectedRows[0].Cells[0].Value;

                FacturaViewModels viewmodel =
                new FacturaViewModels(dbReflexion.Connection.ConnectionString);
                viewmodel.GetInformation(_VoucherID);

                this.claveAcceso = viewmodel.ClaveAcceso;
                this.ambiente = viewmodel.ambiente;
                this.comboBox1.SelectedValue = Convert.ToByte(this.ambiente);
                this.ClaveAccesoLabel.Text = this.claveAcceso;

                xmlFirmado = "<?xml version=" + "\"1.0\"" + " encoding=" + "\"UTF-8\"" + "?>" + viewmodel.Data;

                ToolsMail.SendMailDefault(Domain.Data.Enums.OwnerEnum.Customer, listMail, xmlFirmado, fechaEmision: viewmodel.fechaEmision);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "" + ex.StackTrace,
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Exclamation, caption: "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
                SendMailButton.Enabled = true;
            }
        }

        private void devueltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TipoStateButton.Text = devueltaToolStripMenuItem.Text;
        }

        private void rjRadioButton1_CheckedChanged(object sender, EventArgs e)
        {

            if (rjRadioButton1.Checked)
                panelFilterDateResum.Visible = false;

        }

        private void rjRadioButton2_CheckedChanged(object sender, EventArgs e)
        {


            if (rjRadioButton2.Checked)
                panelFilterDateResum.Visible = true;


        }

        private void rjRadioButton1_MouseClick(object sender, MouseEventArgs e)
        {
            rjRadioButton1.Checked = true;
        }

        private void rjRadioButton2_Click(object sender, EventArgs e)
        {
            rjRadioButton2.Checked = true;
        }

        private async void GetResumeButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                rptViewer.ReportSource = null;
                lblRegitros.Text = String.Empty;


                rptViewer.BringToFront();
                rptViewer.Visible = true;
                rptViewer.Dock = DockStyle.Fill;

                DataTable dsSource = await StoreProcedure.GetElectronicInvoice(dateTimePicker1.Value, dateTimePicker2.Value, rjRadioButton2.Checked);
                rpt.SetDataSource(dsSource);

                rptViewer.ReportSource = rpt;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace, MsgBoxStyle.Critical, "Error");
            }
            finally { this.Cursor = Cursors.Default; }

        }

        private async void GetDetalleElectroButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                fastObjectListView1.DataSource = null;

                DataTable data_All = await StoreProcedure.GetElectronicInvoiceDeatil(dateTimePicker1.Value, dateTimePicker2.Value, rjRadioButton2.Checked);
                fastObjectListView1.BringToFront();
                fastObjectListView1.Visible = true;
                fastObjectListView1.Dock = DockStyle.Fill;



                if (data_All == null || data_All.Rows.Count == 0)
                {
                    fastObjectListView1.DataSource = null;
                    fastObjectListView1.EmptyListMsg = "NO EXISTE INFORMACION..";
                    fastObjectListView1.ClearObjects();
                    lblRegitros.Text = "NO EXISTE INFORMACION";

                    //astObjectListView1.Columns.Clear();
                }
                else
                {
                    lblRegitros.Text = $"Registro extraido {data_All.Rows.Count} visibilidad maxima de : 10000";
                    fastObjectListView1.DataSource = data_All.AsEnumerable().Take(1000).CopyToDataTable();
                    fastObjectListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                }



                data_All.Dispose();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace, MsgBoxStyle.Critical, "Error");
            }
            finally { this.Cursor = Cursors.Default; }


        }

        private async void ExportDetailExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = "";

                string messague = "Escoja una de las sigientes opciones..\n"
                    + "\n1.- Visualizar en excel.."
                    + "\n2.- Enviar a la carpera de descarga."
                    + "\n3.- Cancelar.";

                var result = Interaction.InputBox(messague, "Responda", "1");

                if (!(result.Equals("1") || result.Equals("2")))
                    return;

                this.Cursor = Cursors.WaitCursor;

                if (result == "2")
                {
                    string download = Environment.GetEnvironmentVariable("USERPROFILE") + @"\" + "Downloads";

                    string nameDocument = "Doc Elect " + DateTime.Now.ToString("dd-MM-yyyy HH_mm");
                    filePath = String.Format("{0}\\{1}.xlsx", download, nameDocument);
                }


                DataTable data_All = await StoreProcedure.GetElectronicInvoiceDeatil(dateTimePicker1.Value, dateTimePicker2.Value, rjRadioButton2.Checked);

                data_All.ExportToExcel(filePath.Length > 0 ? filePath : "");
                this.Cursor = Cursors.Default;
                Interaction.MsgBox("Fin de proceso");

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace, MsgBoxStyle.Critical, "Error");
            }
        }


    }



    public static class Extensions
    {
        private const uint WM_SYSKEYDOWN = 0x104;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        public static void Open(this DateTimePicker obj)
        {
            SendMessage(obj.Handle, WM_SYSKEYDOWN, (int)Keys.Down, 0);
        }
    }

}





