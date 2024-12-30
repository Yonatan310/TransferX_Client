using TransferX_GUI.Server;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using Renci.SshNet.Common;
namespace TransferX_GUI
{
    public partial class ConnectWindow : Form
    {
        public ConnectWindow()
        {
            InitializeComponent();
            this.Icon = new System.Drawing.Icon("ico.ico");
            Main main = new Main();
            main.Show();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void LogMessage(string message)
        {
            LogsBox.AppendText($"{DateTime.Now}: {message}{Environment.NewLine}");
        }
        private async void Connect_Click(object sender, EventArgs e)
        {
            ConnectDetails connectDetails = new ConnectDetails
            (
                IpAddressBox.Text,
                PortBox.Text,
                UsernameBox.Text,
                PasswordBox.Text,
                SerialKeyBox.Text,
                ProtocolBox.Text
            );
            if (ProtocolBox == null || string.IsNullOrEmpty(ProtocolBox.Text) || IpAddressBox == null || string.IsNullOrEmpty(IpAddressBox.Text) || PortBox == null || string.IsNullOrEmpty(PortBox.Text) || UsernameBox == null || string.IsNullOrEmpty(UsernameBox.Text) || PasswordBox == null || string.IsNullOrEmpty(PasswordBox.Text) || SerialKeyBox == null || string.IsNullOrEmpty(SerialKeyBox.Text))
            {

                MessageBox.Show("Some Of The Boxes Are Empty Or Null, Check Them Before Connect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            LogMessage("Checking serial key...");
            await Task.Delay(2000);
            bool isValidSerialKey = ConnectDetails.ValidSerialKey(connectDetails.GetSerialKey());
            if (isValidSerialKey == false)
            {
                LogMessage("Invalid serial key. Please check if you put your Serial Key Correctly!.");
                MessageBox.Show("Invalid serial key!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            LogMessage("Serial key is valid.");
            await Task.Delay(1000);
            LogMessage("Connecting To The Server...");
            LogMessage($"Using Protocol: {connectDetails.GetProtocol()}");
            await Task.Delay(2000);


            bool isValidUser = ConnectDetails.ValidSftpCredentials(connectDetails.GetIpAddress(), int.Parse(connectDetails.GetPort()), connectDetails.GetUsername(), connectDetails.GetPassword());
            if (isValidUser == true)
            {
                LogMessage("User authentication successful!");
                await Task.Delay(1000);
                this.Hide();
                Main main = new Main();
                main.Show();
            }
            else
            {
                LogMessage("User authentication failed. Check If Your Details Are Correct!");
            }
        }
    }
}
