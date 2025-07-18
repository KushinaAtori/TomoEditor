using Newtonsoft.Json;
using System.Net;

namespace TomoEditor
{
    public partial class Form1 : Form
    {
        private string ftpHost;
        private int ftpPort = 21;
        private string ftpUser;
        private string ftpPass;
        private string currentFtpDirectory = "/";

        private string savedataArcPath = "";
        private long expectedFileLength = 0;
        private string region = "";

        private PendingChanges changes = new PendingChanges();

        public class ConnectionDetails
        {
            public required string Server { get; set; }
            public int Port { get; set; }
            public required string Username { get; set; }
            public required string Password { get; set; }
        }
        private class PendingChanges
        {
            public int? Money { get; set; }
            public bool RemoveTimePenalty { get; set; } = false;
            public byte? ClothesValue { get; set; }
            public byte? SSClothesValue { get; set; }
            public bool GiveAllHats { get; set; } = false;

            public bool HasChanges =>
                Money.HasValue || RemoveTimePenalty ||
                (ClothesValue.HasValue && SSClothesValue.HasValue) || GiveAllHats;
        }

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e) => PromptForSaveFile();

        private void btnLoadSave_Click(object sender, EventArgs e) => PromptForSaveFile();

        private void PromptForSaveFile()
        {
            using var openFileDialog =
                new OpenFileDialog
                {
                    Filter = "Save Files (*.txt)|*.txt",
                    Title = "Select a Save File"
                };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                savedataArcPath = openFileDialog.FileName;
                expectedFileLength = new FileInfo(savedataArcPath).Length;

                region = expectedFileLength switch
                {
                    1359208 => "JP",
                    1985688 => "USA",
                    _ => string.Empty
                };

                if (string.IsNullOrEmpty(region))
                {
                    MessageBox.Show("Unknown region. Cannot modify the file.");
                    return;
                }

                MessageBox.Show(
                    $"Save file loaded.\nRegion: {region}\nSize: {expectedFileLength} bytes.");
            }
        }

        private string BuildFtpUrl(string path = "/")
        {
            if (string.IsNullOrWhiteSpace(ftpHost))
                throw new InvalidOperationException("FTP host is not set.");

            if (ftpPort <= 0 || ftpPort > 65535)
                throw new InvalidOperationException("Invalid FTP port.");

            path = path.StartsWith("/") ? path : "/" + path;
            return $"ftp://{ftpHost}:{ftpPort}{path}";
        }

        private void LoadConnectionFromFile()
        {
            string path =
                Path.Combine(Application.StartupPath, "connectionDetails.txt");
            if (!File.Exists(path))
            {
                Invoke(() => MessageBox.Show("No saved connection found."));
                return;
            }

            var connection = JsonConvert.DeserializeObject<ConnectionDetails>(
                File.ReadAllText(path));
            ftpHost = connection.Server?.Replace("ftp://", "").Trim();
            ftpPort = connection.Port > 0 ? connection.Port : 21;
            ftpUser = connection.Username;
            ftpPass = connection.Password;

            Invoke(() => {
                MessageBox.Show(
                    $"[DEBUG]\nHost: {ftpHost}\nPort: {ftpPort}\nUser: {ftpUser}");
                LoadFtpDirectory("/");
            });
        }

        private void DownloadFileWithProgress(string remotePath, string localPath)
        {
            string ftpUrl = BuildFtpUrl(remotePath);

            var request = (FtpWebRequest)WebRequest.Create(ftpUrl);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(ftpUser, ftpPass);

            using var response = (FtpWebResponse)request.GetResponse();
            using var ftpStream = response.GetResponseStream();
            using var fileStream = new FileStream(localPath, FileMode.Create);

            byte[] buffer = new byte[8192];
            int bytesRead;
            long totalRead = 0;

            progressBar1.Value = 0;
            progressBar1.Maximum = 100;

            long totalBytes = GetFtpFileSize(remotePath);

            while ((bytesRead = ftpStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                fileStream.Write(buffer, 0, bytesRead);
                totalRead += bytesRead;
                if (totalBytes > 0)
                {
                    int percent = (int)((totalRead * 100) / totalBytes);
                    progressBar1.Value = Math.Min(percent, 100);
                }
                Application.DoEvents();
            }

            progressBar1.Value = 100;
            MessageBox.Show("Download complete!");
        }

        private long GetFtpFileSize(string remotePath)
        {
            var request = (FtpWebRequest)WebRequest.Create(BuildFtpUrl(remotePath));
            request.Method = WebRequestMethods.Ftp.GetFileSize;
            request.Credentials = new NetworkCredential(ftpUser, ftpPass);

            using var response = (FtpWebResponse)request.GetResponse();
            return response.ContentLength;
        }

        private void LoadFtpDirectory(string path)
        {
            try
            {
                string url = BuildFtpUrl(path);
                var request = (FtpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(ftpUser, ftpPass);

                using var response = (FtpWebResponse)request.GetResponse();
                using var stream = response.GetResponseStream();
                using var reader = new StreamReader(stream);

                lstDirectory.Items.Clear();
                while (!reader.EndOfStream) lstDirectory.Items.Add(reader.ReadLine());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load directory: {ex.Message}");
            }
        }

        private void lstDirectory_MouseDoubleClick(object sender,
                                                   MouseEventArgs e)
        {
            if (lstDirectory.SelectedItems.Count == 0)
                return;

            string selectedName = lstDirectory.SelectedItems[0].Text;
            string previousDirectory = currentFtpDirectory;

            try
            {
                currentFtpDirectory =
                    (currentFtpDirectory == "/")
                        ? selectedName
                        : currentFtpDirectory.TrimEnd('/') + "/" + selectedName;
                LoadFtpDirectory(currentFtpDirectory);
            }
            catch
            {
                currentFtpDirectory = previousDirectory;
                MessageBox.Show($"'{selectedName}' does not appear to be a folder.");
            }
        }

        private void CreateBackup()
        {
            if (string.IsNullOrEmpty(savedataArcPath) ||
                !File.Exists(savedataArcPath))
            {
                MessageBox.Show("No save file loaded to backup.");
                return;
            }

            string dir = Path.GetDirectoryName(savedataArcPath)!;
            string name = $"savedataarc_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt";
            File.Copy(savedataArcPath, Path.Combine(dir, name), overwrite: false);
        }

        private void btnSaveConnection_Click(object sender, EventArgs e)
        {
            try
            {
                var connection =
                    new ConnectionDetails
                    {
                        Server = ftpHost,
                        Port = ftpPort,
                        Username = ftpUser,
                        Password = ftpPass
                    };
                string path =
                    Path.Combine(Application.StartupPath, "connectionDetails.txt");
                File.WriteAllText(
                    path, JsonConvert.SerializeObject(connection, Formatting.Indented));
                MessageBox.Show("Connection saved to connectionDetails.txt.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save connection: {ex.Message}");
            }
        }

        private void btnNewConnection_Click(object sender, EventArgs e)
        {
            using var ftpForm = new FtpSettingsForm();
            if (ftpForm.ShowDialog() == DialogResult.OK)
            {
                ftpHost = ftpForm.FtpHost.Replace("ftp://", "").Trim();
                ftpPort = ftpForm.FtpPort;
                ftpUser = ftpForm.FtpUser;
                ftpPass = ftpForm.FtpPass;
                currentFtpDirectory = "/";

                MessageBox.Show($"Connected to {ftpHost}:{ftpPort} as {ftpUser}.");
                LoadFtpDirectory(currentFtpDirectory);
            }
            else
            {
                MessageBox.Show("Connection cancelled.");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(currentFtpDirectory) ||
                currentFtpDirectory == "/")
                return;

            int lastSlash = currentFtpDirectory.LastIndexOf('/');
            currentFtpDirectory =
                lastSlash > 0 ? currentFtpDirectory[..lastSlash] : "/";
            LoadFtpDirectory(currentFtpDirectory);
        }

        private void btnRoot_Click(object sender, EventArgs e)
        {
            currentFtpDirectory = "/";
            LoadFtpDirectory(currentFtpDirectory);
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (lstDirectory.SelectedItems.Count == 0)
                return;

            string selectedFile = lstDirectory.SelectedItems[0].Text;
            string remoteFile =
                (currentFtpDirectory == "/" ? "" : currentFtpDirectory.TrimEnd('/')) +
                "/" + selectedFile;

            using var saveFileDialog =
                new SaveFileDialog
                {
                    FileName = selectedFile,
                    DefaultExt = Path.GetExtension(selectedFile),
                    Filter = "All Files (*.*)|*.*"
                };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                DownloadFileWithProgress(remoteFile, saveFileDialog.FileName);
        }
        private async void btnLoadConnection_Click(object sender, EventArgs e)
        {
            btnLoadConnection.Enabled = false;
            try
            {
                await Task.Run(() => LoadConnectionFromFile());
            }
            finally
            {
                btnLoadConnection.Enabled = true;
            }
        }
        private void btnDisableTimePenalty_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(region))
            {
                MessageBox.Show("Region not loaded. Please load a save file.");
                return;
            }

            changes.RemoveTimePenalty = true;
            MessageBox.Show(
                "Time penalty removal queued. Click 'Save File' to apply.");
        }
        private void btnApplyMoney_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtBoxMoneyInput.Text, out int moneyValue))
            {
                MessageBox.Show("Invalid input for money.");
                return;
            }

            changes.Money = moneyValue;
            MessageBox.Show(
                $"Money change queued: {moneyValue}. Click 'Save File' to apply.");
        }
        private void btnWriteClothes_Click(object sender, EventArgs e)
        {
            byte clothesValue = 3;
            byte ssClothesValue = 3;

            changes.ClothesValue = clothesValue;
            changes.SSClothesValue = ssClothesValue;

            MessageBox.Show("Clothing changes queued. Click 'Save File' to apply.");
        }
        private void btnGiveAllHats_Click(object sender, EventArgs e)
        {
            if (region != "USA")
            {
                MessageBox.Show(
                    "Currently only the USA region is supported for hat modifications.");
                return;
            }

            changes.GiveAllHats = true;
            MessageBox.Show("Hat item changes queued. Click 'Save File' to apply.");
        }
        private void btnApplyChanges_Click(object sender, EventArgs e)
        {
            if (!changes.HasChanges)
            {
                MessageBox.Show("No changes to apply.");
                return;
            }

            if (string.IsNullOrEmpty(savedataArcPath) ||
                !File.Exists(savedataArcPath))
            {
                MessageBox.Show("Save file not loaded.");
                return;
            }

            CreateBackup();

            using var fs =
                new FileStream(savedataArcPath, FileMode.Open, FileAccess.Write);

            if (changes.Money.HasValue)
                SaveDataModifier.ApplyMoneyChange(fs, changes.Money.Value, region);

            if (changes.RemoveTimePenalty)
                SaveDataModifier.ApplyTimePenaltyPatch(fs, region);

            if (changes.ClothesValue.HasValue && changes.SSClothesValue.HasValue)
                SaveDataModifier.ApplyClothesChange(fs, changes.ClothesValue.Value, changes.SSClothesValue.Value);

            if (changes.GiveAllHats)
                SaveDataModifier.ApplyHatChanges(fs);

            MessageBox.Show("Changes successfully applied.");
            changes = new PendingChanges();
        }
    }
}
