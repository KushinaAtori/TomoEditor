using System;
using System.Windows.Forms;

namespace TomoEditor
{
    public partial class FtpSettingsForm : Form
    {
        public string FtpHost { get; private set; }
        public string FtpUser { get; private set; }
        public string FtpPass { get; private set; }
        public int FtpPort { get; private set; }


        public FtpSettingsForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            FtpHost = txtHost.Text;
            FtpUser = txtUser.Text;
            FtpPass = txtPass.Text;
            FtpPort = int.TryParse(txtPort.Text, out int p) ? p : 21; // default to 21
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
