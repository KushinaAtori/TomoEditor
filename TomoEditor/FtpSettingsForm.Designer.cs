using System;
using System.Windows.Forms;

namespace TomoEditor
{
    public partial class FtpSettingsForm : Form
    {

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FtpSettingsForm));
            btnOk = new Guna.UI2.WinForms.Guna2Button();
            btnCancel = new Guna.UI2.WinForms.Guna2Button();
            txtUser = new Guna.UI2.WinForms.Guna2TextBox();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtPass = new Guna.UI2.WinForms.Guna2TextBox();
            txtHost = new Guna.UI2.WinForms.Guna2TextBox();
            txtPort = new Guna.UI2.WinForms.Guna2TextBox();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.CustomizableEdges = customizableEdges13;
            btnOk.DisabledState.BorderColor = Color.DarkGray;
            btnOk.DisabledState.CustomBorderColor = Color.DarkGray;
            btnOk.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnOk.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnOk.Font = new Font("Segoe UI", 9F);
            btnOk.ForeColor = Color.White;
            btnOk.Location = new Point(12, 225);
            btnOk.Name = "btnOk";
            btnOk.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnOk.Size = new Size(116, 45);
            btnOk.TabIndex = 0;
            btnOk.Text = "OK";
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.CustomizableEdges = customizableEdges15;
            btnCancel.DisabledState.BorderColor = Color.DarkGray;
            btnCancel.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancel.Font = new Font("Segoe UI", 9F);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(189, 225);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnCancel.Size = new Size(116, 45);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "CANCEL";
            btnCancel.Click += btnCancel_Click;
            // 
            // txtUser
            // 
            txtUser.CustomizableEdges = customizableEdges17;
            txtUser.DefaultText = "";
            txtUser.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtUser.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtUser.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtUser.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtUser.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtUser.Font = new Font("Segoe UI", 9F);
            txtUser.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtUser.Location = new Point(38, 45);
            txtUser.Name = "txtUser";
            txtUser.PlaceholderText = "";
            txtUser.SelectedText = "";
            txtUser.ShadowDecoration.CustomizableEdges = customizableEdges18;
            txtUser.Size = new Size(102, 36);
            txtUser.TabIndex = 2;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Location = new Point(38, 22);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(56, 17);
            guna2HtmlLabel1.TabIndex = 3;
            guna2HtmlLabel1.Text = "Username";
            // 
            // txtPass
            // 
            txtPass.CustomizableEdges = customizableEdges19;
            txtPass.DefaultText = "";
            txtPass.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtPass.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtPass.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtPass.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtPass.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtPass.Font = new Font("Segoe UI", 9F);
            txtPass.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtPass.Location = new Point(38, 110);
            txtPass.Name = "txtPass";
            txtPass.PlaceholderText = "";
            txtPass.SelectedText = "";
            txtPass.ShadowDecoration.CustomizableEdges = customizableEdges20;
            txtPass.Size = new Size(102, 36);
            txtPass.TabIndex = 4;
            // 
            // txtHost
            // 
            txtHost.CustomizableEdges = customizableEdges21;
            txtHost.DefaultText = "";
            txtHost.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtHost.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtHost.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtHost.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtHost.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtHost.Font = new Font("Segoe UI", 9F);
            txtHost.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtHost.Location = new Point(167, 45);
            txtHost.Name = "txtHost";
            txtHost.PlaceholderText = "";
            txtHost.SelectedText = "";
            txtHost.ShadowDecoration.CustomizableEdges = customizableEdges22;
            txtHost.Size = new Size(95, 36);
            txtHost.TabIndex = 5;
            // 
            // txtPort
            // 
            txtPort.CustomizableEdges = customizableEdges23;
            txtPort.DefaultText = "";
            txtPort.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtPort.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtPort.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtPort.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtPort.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtPort.Font = new Font("Segoe UI", 9F);
            txtPort.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtPort.Location = new Point(167, 110);
            txtPort.Name = "txtPort";
            txtPort.PlaceholderText = "";
            txtPort.SelectedText = "";
            txtPort.ShadowDecoration.CustomizableEdges = customizableEdges24;
            txtPort.Size = new Size(95, 36);
            txtPort.TabIndex = 6;
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Location = new Point(38, 87);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(53, 17);
            guna2HtmlLabel2.TabIndex = 7;
            guna2HtmlLabel2.Text = "Password";
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.BackColor = Color.Transparent;
            guna2HtmlLabel3.Location = new Point(167, 87);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(25, 17);
            guna2HtmlLabel3.TabIndex = 9;
            guna2HtmlLabel3.Text = "Port";
            // 
            // guna2HtmlLabel4
            // 
            guna2HtmlLabel4.BackColor = Color.Transparent;
            guna2HtmlLabel4.Location = new Point(167, 22);
            guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            guna2HtmlLabel4.Size = new Size(58, 17);
            guna2HtmlLabel4.TabIndex = 8;
            guna2HtmlLabel4.Text = "Hostname";
            // 
            // FtpSettingsForm
            // 
            AcceptButton = btnOk;
            CancelButton = btnCancel;
            ClientSize = new Size(317, 282);
            Controls.Add(guna2HtmlLabel3);
            Controls.Add(guna2HtmlLabel4);
            Controls.Add(guna2HtmlLabel2);
            Controls.Add(txtPort);
            Controls.Add(txtHost);
            Controls.Add(txtPass);
            Controls.Add(guna2HtmlLabel1);
            Controls.Add(txtUser);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FtpSettingsForm";
            Text = "FTP Settings";
            ResumeLayout(false);
            PerformLayout();
        }
        private Guna.UI2.WinForms.Guna2Button btnOk;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2TextBox txtUser;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2TextBox txtPass;
        private Guna.UI2.WinForms.Guna2TextBox txtHost;
        private Guna.UI2.WinForms.Guna2TextBox txtPort;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
    }
}
