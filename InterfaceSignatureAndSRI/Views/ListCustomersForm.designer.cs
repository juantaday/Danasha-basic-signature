namespace InterfaceSignatureAndSRI.Views
{
    partial class ListCustomersForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.Findbutton = new System.Windows.Forms.Button();
            this.FindtextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TypeFindComboBox = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtGrid = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.OK_button = new System.Windows.Forms.Button();
            this.Cancel_button = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrid)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Findbutton);
            this.panel1.Controls.Add(this.FindtextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TypeFindComboBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 44);
            this.panel1.TabIndex = 0;
            // 
            // Findbutton
            // 
            this.Findbutton.Image = global::InterfaceSignatureAndSRI.Properties.Resources.zoom_Grin_24;
            this.Findbutton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Findbutton.Location = new System.Drawing.Point(578, 1);
            this.Findbutton.Name = "Findbutton";
            this.Findbutton.Size = new System.Drawing.Size(74, 38);
            this.Findbutton.TabIndex = 3;
            this.Findbutton.Text = "Buscar";
            this.Findbutton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Findbutton.UseVisualStyleBackColor = true;
            this.Findbutton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // FindtextBox
            // 
            this.FindtextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FindtextBox.Location = new System.Drawing.Point(316, 12);
            this.FindtextBox.Name = "FindtextBox";
            this.FindtextBox.Size = new System.Drawing.Size(254, 23);
            this.FindtextBox.TabIndex = 2;
            this.FindtextBox.TextChanged += new System.EventHandler(this.FindtextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tipo de busqueda..";
            // 
            // TypeFindComboBox
            // 
            this.TypeFindComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.TypeFindComboBox.FormattingEnabled = true;
            this.TypeFindComboBox.Items.AddRange(new object[] {
            "Apellido y nombre",
            "Ruc / C.I ",
            "Código del cliente"});
            this.TypeFindComboBox.Location = new System.Drawing.Point(113, 14);
            this.TypeFindComboBox.Name = "TypeFindComboBox";
            this.TypeFindComboBox.Size = new System.Drawing.Size(180, 21);
            this.TypeFindComboBox.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtGrid);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 44);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(784, 280);
            this.panel2.TabIndex = 1;
            // 
            // dtGrid
            // 
            this.dtGrid.AllowUserToAddRows = false;
            this.dtGrid.AllowUserToDeleteRows = false;
            this.dtGrid.BackgroundColor = System.Drawing.Color.White;
            this.dtGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGrid.Location = new System.Drawing.Point(3, 3);
            this.dtGrid.Name = "dtGrid";
            this.dtGrid.ReadOnly = true;
            this.dtGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGrid.Size = new System.Drawing.Size(778, 274);
            this.dtGrid.TabIndex = 4;
            this.dtGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGrid_CellContentDoubleClick);
            this.dtGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGrid_RowEnter);
            this.dtGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtGrid_KeyDown);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.OK_button);
            this.panel3.Controls.Add(this.Cancel_button);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 324);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(2);
            this.panel3.Size = new System.Drawing.Size(784, 36);
            this.panel3.TabIndex = 2;
            // 
            // OK_button
            // 
            this.OK_button.Dock = System.Windows.Forms.DockStyle.Right;
            this.OK_button.Location = new System.Drawing.Point(601, 2);
            this.OK_button.Name = "OK_button";
            this.OK_button.Size = new System.Drawing.Size(101, 32);
            this.OK_button.TabIndex = 1;
            this.OK_button.Text = "Seleccionar";
            this.OK_button.UseVisualStyleBackColor = true;
            this.OK_button.Click += new System.EventHandler(this.OK_button_Click);
            // 
            // Cancel_button
            // 
            this.Cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_button.Dock = System.Windows.Forms.DockStyle.Right;
            this.Cancel_button.Location = new System.Drawing.Point(702, 2);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.Size = new System.Drawing.Size(80, 32);
            this.Cancel_button.TabIndex = 0;
            this.Cancel_button.Text = "Cancelar..";
            this.Cancel_button.UseVisualStyleBackColor = true;
            this.Cancel_button.Click += new System.EventHandler(this.Cancel_button_Click);
            // 
            // ListCustomersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.CancelButton = this.Cancel_button;
            this.ClientSize = new System.Drawing.Size(784, 360);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ListCustomersForm";
            this.Text = "Listado de clientes...";
            this.Load += new System.EventHandler(this.ListCustomersForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGrid)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Findbutton;
        private System.Windows.Forms.TextBox FindtextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox TypeFindComboBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button OK_button;
        private System.Windows.Forms.Button Cancel_button;
        protected internal System.Windows.Forms.DataGridView dtGrid;
    }
}