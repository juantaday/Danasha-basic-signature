using InterfaceSignatureAndSRI.Utils;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterfaceSignatureAndSRI.Views
{
    public partial class ListCustomersForm : Form
    {
        private string StringConnection;
        private bool isLoated;
        public ListCustomersForm(string sTringConnection)
        {
            InitializeComponent();
            StringConnection = sTringConnection;
        }

        private void ListCustomersForm_Load(object sender, EventArgs e)
        {
            GetListCustomers();
            isLoated = true;
            TypeFindComboBox.SelectedIndex = 0 ;
        }

        private async void GetListCustomers() {
            await Task.Factory.StartNew(() =>
            {
                  using (SqlConnection cnn  = new SqlConnection(StringConnection)) {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("",cnn))
                    {
                        string sql = @"select top(300) * from [dbo].[ClienteName] as c";
                        cmd.CommandText = sql;
                        using (SqlDataAdapter dat = new SqlDataAdapter (cmd)) {
                            DataTable dt = new DataTable();
                            dat.Fill(dt);
                            this.Invoke(new MethodInvoker(() => {
                                dtGrid.DataSource = dt;
                                SettingColumns();
                            }));

                        }
                    }

                }
            });
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            if (!isLoated)
            {
                return;
            }
            if (TypeFindComboBox.SelectedIndex == -1)
            {
                Interaction.MsgBox("Seleccione el tipo de busqueda..", Title: "Importate..");
                TypeFindComboBox.Select();
                return;
            }

            if (FindtextBox.Text.Length < 3)
            {
                Interaction.MsgBox("Determine los que busca.", Title: "Importate..");
                FindtextBox.Select();
                return;
            }
            string whereFilte = string.Empty;
            switch (TypeFindComboBox.SelectedIndex)
            {
                case 0:
                    whereFilte = FilterToName();
                    break;
                case 1:
                    whereFilte = "WHERE c.Ruc_Ci like '" + FindtextBox.Text + "%'";
                    break;
                case 2:
                    whereFilte = "WHERE c.idCliente = " + FindtextBox.Text;
                    break;
                default:
                    break;
            }
            try
            {
                FilterCustomers(whereFilte);
            }

            catch (Exception ex)
            {

                Interaction.MsgBox (ex.Message + "\n"+ 
                    ex.StackTrace , MsgBoxStyle.Critical,"Error");
            }
        }

        private  void FilterCustomers(string whereFilte)
        {
     
                using (SqlConnection cnn = new SqlConnection(StringConnection))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("", cnn))
                    {
                        string sql = @"select * from [dbo].[ClienteName] as c" + "\n";
                        sql = sql  + whereFilte;

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        using (SqlDataAdapter dat = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            dat.Fill(dt);
                            this.Invoke(new MethodInvoker(() => {
                                dtGrid.DataSource = dt;
                                SettingColumns();
                            }));

                        }
                    }
                }
        }

        private void SettingColumns()
        {
            try
            {
                if (dtGrid.Columns.Count > 0)
                {
                    dtGrid.Columns["credito"].Visible = false;
                    dtGrid.Columns["monto_Max"].Visible = false;
                    dtGrid.Columns["Nombres"].Width = 250;
                    dtGrid.Columns["Ruc_Ci"].Width = 100;
                }
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex);
            }
           
        }

        private string FilterToName()
        {
            ResponseSpliter mySpliter = Funciones.GenerateSpliter(FindtextBox.Text);
            string sql ="";
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

        private void FindtextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(FindtextBox.Text))
            {
                this.AcceptButton = Findbutton;
            }
            else {
                this.AcceptButton = null;
            }
        }

        private void dtGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dtGrid.SelectedRows.Count == 1)
            {
                this.AcceptButton = OK_button;
            }
            else {
                this.AcceptButton = null;
            }
        }
        private void OK_button_Click(object sender, EventArgs e)
        {
            if  (dtGrid.SelectedRows.Count != 1)
            {
                Interaction.MsgBox("Seleccion un cliente de la lista..", Title: "Importante..");
                return;
            }
            this.DialogResult = DialogResult.OK;

        }

        private void dtGrid_KeyDown(object sender, KeyEventArgs e)
        {
            try
                {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    OK_button.PerformClick();
                } else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Tab) {
                    FindtextBox.Select();
                }

              }
        catch (Exception ex){
                Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical, "Error");
       }
        }

        private void dtGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OK_button.PerformClick();
        }
    }
}
