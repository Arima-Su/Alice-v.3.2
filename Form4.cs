﻿using RconSharp;
using Alice_v._3._1;

namespace Alice_v._3._2
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string message = textBox1.Text;

            string executablePath = AppDomain.CurrentDomain.BaseDirectory;

            if (message == null)
            {
                MessageBox.Show("Please input a command first");
                return;
            }

            if (Form1.selectedVersion != null)
            {
                try
                {
                    string filePath = Path.Combine(executablePath, "versions", Form1.selectedVersion, "server.properties");
                    if (File.Exists(filePath))
                    {
                        string[] lines = File.ReadAllLines(filePath);
                        string serverctxIp = Form1.GetServerIp(lines);

                        if (!string.IsNullOrEmpty(serverctxIp))
                        {
                            string serverIp = $"{serverctxIp}"; // Replace with your server IP
                            int serverPort = 25575; // Replace with your server RCON port
                                                    //string rconPassword = ""; // Replace with your RCON password

                            var client = RconClient.Create($"{serverIp}", serverPort);

                            // Open the connection
                            await client.ConnectAsync();

                            // Send a RCON packet with type AUTH and the RCON password for the target server
                            var authenticated = await client.AuthenticateAsync("727");
                            if (authenticated)
                            {
                                await client.ExecuteCommandAsync($"{message}");
                            }
                        }
                        else
                        {
                            // The server IP value was not found in the file
                            MessageBox.Show("Server IP value not found in server.properties");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Server Properties file not found in {filePath}");
                    }
                }
                catch
                {
                    MessageBox.Show("Well that failed, you can try again tho..");
                }
            }
            else
            {
                MessageBox.Show("Please pick a version first");
            }
        }
    }
}
