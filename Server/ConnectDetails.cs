using MySql.Data.MySqlClient;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferX_GUI.Server
{
    internal class ConnectDetails
    {


        public string IpAddress;
        public string Port;
        public string Username;
        public string Password;
        public string SerialKey;
        public string Protocol;


        public ConnectDetails(string ipAddress, string port, string username, string password, string serialkey, string protocol)
        {
            IpAddress = ipAddress;
            Port = port;
            Username = username;
            Password = password;
            SerialKey = serialkey;
            Protocol = protocol;
        }
        public string GetIpAddress() { return IpAddress; }
        public string GetPort() { return Port; }
        public string GetUsername() { return Username; }
        public string GetPassword() { return Password; }
        public string GetSerialKey() { return SerialKey; }
        public string GetProtocol() { return Protocol; }

        public void SetIpAddress(string ipAddress) { IpAddress = ipAddress; }
        public void SetPort(string port) { Port = port; }
        public void SetUsername(string username) { Username = username; }
        public void SetPassword(string password) { Password = password; }
        public void SetSerialKey(string serialkey) { SerialKey = serialkey; }
        public void SetProtocol(string protocol) { Protocol = protocol; }



        public static bool ValidSerialKey(string serialKey)
        {
            string connectionString = "Server=127.0.0.1;Port=3306;Database=transferx;Uid=root;Pwd=;";
            string CheckValidSerialKeyQuery = "SELECT COUNT(1) FROM users WHERE SerialKey = '" + serialKey + "'";
            try
            {
                using (MySqlConnection transferxsqlconnection = new MySqlConnection(connectionString))
                {
                    transferxsqlconnection.Open();
                    using (MySqlCommand command = new MySqlCommand(CheckValidSerialKeyQuery, transferxsqlconnection))
                    {




                        return Convert.ToBoolean(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool ValidSftpCredentials(string ipAddress, int port, string username, string password)
        {
            try
            {

                using (var transferxclient = new SftpClient(ipAddress, port, username, password))
                {
                    transferxclient.Connect();
                    if (transferxclient.IsConnected)
                    {
                        return true;
                    }
                }
            }
            catch (FormatException ex)
            {

                MessageBox.Show($"Invalid input format: {ex.Message}", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {

                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

    }

}
