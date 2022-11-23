namespace DomainSQLite.Setting
{
    partial class DefautConnectionForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefautConnectionForm));
            this.label1 = new System.Windows.Forms.Label();
            this.DataServerText = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.Close_Button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.NameDataBaseText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.UserIdText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PasswordText = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Userbutton = new System.Windows.Forms.Button();
            this.Filebutton = new System.Windows.Forms.Button();
            this.Integridadbutton = new System.Windows.Forms.Button();
            this.panelUserIdentity = new System.Windows.Forms.Panel();
            this.HeaderButton = new System.Windows.Forms.Panel();
            this.panelFilepath = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.panelUserIdentity.SuspendLayout();
            this.panelFilepath.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data Server:";
            // 
            // DataServerText
            // 
            this.DataServerText.Location = new System.Drawing.Point(15, 37);
            this.DataServerText.Name = "DataServerText";
            this.DataServerText.Size = new System.Drawing.Size(291, 24);
            this.DataServerText.TabIndex = 1;
            // 
            // SaveButton
            // 
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.Location = new System.Drawing.Point(34, 293);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(290, 37);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "Save";
            this.toolTip1.SetToolTip(this.SaveButton, "Save the conection");
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // Close_Button
            // 
            this.Close_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Close_Button.Image = ((System.Drawing.Image)(resources.GetObject("Close_Button.Image")));
            this.Close_Button.Location = new System.Drawing.Point(367, 2);
            this.Close_Button.Name = "Close_Button";
            this.Close_Button.Size = new System.Drawing.Size(41, 33);
            this.Close_Button.TabIndex = 3;
            this.toolTip1.SetToolTip(this.Close_Button, "Close the panel");
            this.Close_Button.UseVisualStyleBackColor = true;
            this.Close_Button.Click += new System.EventHandler(this.Close_Button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Data base:";
            // 
            // NameDataBaseText
            // 
            this.NameDataBaseText.Location = new System.Drawing.Point(15, 91);
            this.NameDataBaseText.Name = "NameDataBaseText";
            this.NameDataBaseText.Size = new System.Drawing.Size(291, 24);
            this.NameDataBaseText.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 122);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "User Id";
            // 
            // UserIdText
            // 
            this.UserIdText.Location = new System.Drawing.Point(15, 144);
            this.UserIdText.Name = "UserIdText";
            this.UserIdText.Size = new System.Drawing.Size(291, 24);
            this.UserIdText.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 177);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "Password:";
            // 
            // PasswordText
            // 
            this.PasswordText.Location = new System.Drawing.Point(15, 198);
            this.PasswordText.Name = "PasswordText";
            this.PasswordText.PasswordChar = '*';
            this.PasswordText.Size = new System.Drawing.Size(291, 24);
            this.PasswordText.TabIndex = 1;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(71, 338);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(139, 19);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Text connection.....";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Userbutton
            // 
            this.Userbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Userbutton.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Userbutton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Userbutton.Location = new System.Drawing.Point(12, 2);
            this.Userbutton.Name = "Userbutton";
            this.Userbutton.Size = new System.Drawing.Size(113, 27);
            this.Userbutton.TabIndex = 6;
            this.Userbutton.Text = "Identificacion";
            this.toolTip1.SetToolTip(this.Userbutton, "Con identificacion de usurio");
            this.Userbutton.UseVisualStyleBackColor = true;
            this.Userbutton.Click += new System.EventHandler(this.Userbutton_Click);
            // 
            // Filebutton
            // 
            this.Filebutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Filebutton.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Filebutton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Filebutton.Location = new System.Drawing.Point(127, 2);
            this.Filebutton.Name = "Filebutton";
            this.Filebutton.Size = new System.Drawing.Size(113, 27);
            this.Filebutton.TabIndex = 7;
            this.Filebutton.Text = "Con archivo DB";
            this.toolTip1.SetToolTip(this.Filebutton, "Con direccion de un archivo");
            this.Filebutton.UseVisualStyleBackColor = true;
            this.Filebutton.Click += new System.EventHandler(this.Filebutton_Click);
            // 
            // Integridadbutton
            // 
            this.Integridadbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Integridadbutton.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Integridadbutton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Integridadbutton.Location = new System.Drawing.Point(242, 2);
            this.Integridadbutton.Name = "Integridadbutton";
            this.Integridadbutton.Size = new System.Drawing.Size(113, 27);
            this.Integridadbutton.TabIndex = 7;
            this.Integridadbutton.Text = "Integridad";
            this.toolTip1.SetToolTip(this.Integridadbutton, "Con seguridad integrada.");
            this.Integridadbutton.UseVisualStyleBackColor = true;
            // 
            // panelUserIdentity
            // 
            this.panelUserIdentity.Controls.Add(this.PasswordText);
            this.panelUserIdentity.Controls.Add(this.label1);
            this.panelUserIdentity.Controls.Add(this.DataServerText);
            this.panelUserIdentity.Controls.Add(this.label2);
            this.panelUserIdentity.Controls.Add(this.NameDataBaseText);
            this.panelUserIdentity.Controls.Add(this.label4);
            this.panelUserIdentity.Controls.Add(this.label3);
            this.panelUserIdentity.Controls.Add(this.UserIdText);
            this.panelUserIdentity.Location = new System.Drawing.Point(12, 44);
            this.panelUserIdentity.Name = "panelUserIdentity";
            this.panelUserIdentity.Size = new System.Drawing.Size(345, 238);
            this.panelUserIdentity.TabIndex = 5;
            // 
            // HeaderButton
            // 
            this.HeaderButton.BackColor = System.Drawing.Color.Red;
            this.HeaderButton.Location = new System.Drawing.Point(12, 30);
            this.HeaderButton.Name = "HeaderButton";
            this.HeaderButton.Size = new System.Drawing.Size(113, 3);
            this.HeaderButton.TabIndex = 8;
            // 
            // panelFilepath
            // 
            this.panelFilepath.Controls.Add(this.label5);
            this.panelFilepath.Controls.Add(this.filePathTextBox);
            this.panelFilepath.Location = new System.Drawing.Point(372, 44);
            this.panelFilepath.Name = "panelFilepath";
            this.panelFilepath.Size = new System.Drawing.Size(345, 238);
            this.panelFilepath.TabIndex = 6;
            this.panelFilepath.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 15);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "File path:";
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.Location = new System.Drawing.Point(15, 37);
            this.filePathTextBox.Multiline = true;
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.Size = new System.Drawing.Size(313, 185);
            this.filePathTextBox.TabIndex = 1;
            this.filePathTextBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.filePathTextBox_MouseDoubleClick);
            // 
            // DefautConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(67)))), ((int)(((byte)(31)))));
            this.CancelButton = this.Close_Button;
            this.ClientSize = new System.Drawing.Size(766, 367);
            this.ControlBox = false;
            this.Controls.Add(this.panelFilepath);
            this.Controls.Add(this.HeaderButton);
            this.Controls.Add(this.Close_Button);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.Integridadbutton);
            this.Controls.Add(this.Filebutton);
            this.Controls.Add(this.Userbutton);
            this.Controls.Add(this.panelUserIdentity);
            this.Controls.Add(this.linkLabel1);
            this.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "DefautConnectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DefautConnectionForm";
            this.Load += new System.EventHandler(this.DefautConnectionForm_Load);
            this.panelUserIdentity.ResumeLayout(false);
            this.panelUserIdentity.PerformLayout();
            this.panelFilepath.ResumeLayout(false);
            this.panelFilepath.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DataServerText;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button Close_Button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NameDataBaseText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox UserIdText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PasswordText;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panelUserIdentity;
        private System.Windows.Forms.Button Userbutton;
        private System.Windows.Forms.Button Filebutton;
        private System.Windows.Forms.Button Integridadbutton;
        private System.Windows.Forms.Panel HeaderButton;
        private System.Windows.Forms.Panel panelFilepath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox filePathTextBox;
    }
}