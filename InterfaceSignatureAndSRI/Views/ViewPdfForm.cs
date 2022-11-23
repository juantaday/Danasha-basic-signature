using CrystalDecisions.CrystalReports.Engine;
using InterfaceSignatureAndSRI.Data;
using InterfaceSignatureAndSRI.Data.Enums;
using InterfaceSignatureAndSRI.Models;
using InterfaceSignatureAndSRI.Reports;
using java.security;
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace InterfaceSignatureAndSRI.Views
{
    public partial class ViewPdfForm : Form
    {
        private int _VoucheID;
        private readonly ViewTypePDFEnum _operation;
        private FacturaViewModels viewModel;
        private byte[] _logo;

        ReportDocument rpt;

    
        public ViewPdfForm(ViewTypePDFEnum operation,int idVoucher = 0)
        {
            InitializeComponent();

            this._operation = operation;
            _VoucheID = idVoucher;

            if (_operation == ViewTypePDFEnum.PDF)
                this.rpt = new FacturaRideRpt();
            else if (_operation == ViewTypePDFEnum.TICKET) 
            {
                Load_logo();
                this.rpt = new FacturaTicketRpt();
            }
                
        }

        public ViewPdfForm(ViewTypePDFEnum operation, FacturaViewModels viewModel)
        {
            InitializeComponent();

            this._operation = operation;
            this.viewModel = viewModel;

            if (_operation == ViewTypePDFEnum.PDF)
                this.rpt = new FacturaRideRpt();
            else if (_operation == ViewTypePDFEnum.TICKET)
            {
                Load_logo();
                this.rpt = new FacturaTicketRpt();
            }
        }

        private async  void Load_logo()
        {
            try
            {
                _logo = Domain.Funtions.Funtion.GetLogoTicketByte();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
              }
        }

      


        private void Form1_Load(object sender, EventArgs e)
        {
    
            DataSet dsSource = new DataSet();

            try
            {
                    if (_operation == ViewTypePDFEnum.TICKET && this.viewModel != null)
                    this.viewModel.logoByte =_logo;

                    if (this.viewModel != null) 
                       dsSource = CRUD_database.GetDataSetWithModel(this.viewModel);
                    if (this._VoucheID >0 )
                        dsSource = CRUD_database.GetDataSetWitID(this._VoucheID);

                rpt.SetDataSource(dsSource);

                //System.IO.StreamWriter writer = new System.IO.StreamWriter("Customers.xsd");
                //dsSource.WriteXmlSchema(writer);
                //writer.Close();

                rptViewer.ReportSource = rpt;
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

        private void ViewPdfForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(this.rpt != null )
                rpt.Dispose();
        }
    }

}
