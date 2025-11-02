using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using BombermanGame.Network;

namespace BombermanGame
{
    // Lobby form for hosting or joining multiplayer games
    public partial class MultiplayerLobby : Form
    {
        private TextBox playerNameTextBox;
        private TextBox serverAddressTextBox;
        private TextBox serverPortTextBox;
        private Button hostButton;
        private Button joinButton;
        private Button cancelButton;
        private Label statusLabel;
        private Label playerCountLabel;
        
        private GameServer? server;
        private GameClient? client;
        private bool isHosting = false;
        
        public string PlayerName { get; private set; } = "";
        public string ServerAddress { get; private set; } = "";
        public int ServerPort { get; private set; }
        public bool IsHosting { get; private set; }
        public GameClient? Client { get; private set; }
        public GameServer? Server { get; private set; }
        public bool Connected { get; private set; }
        
        public MultiplayerLobby()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            this.Text = "Bomberman Multiplayer Lobby";
            this.Size = new System.Drawing.Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            // Player name
            Label nameLabel = new Label
            {
                Text = "Player Name:",
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(100, 20)
            };
            this.Controls.Add(nameLabel);
            
            playerNameTextBox = new TextBox
            {
                Text = "Player" + new Random().Next(1000, 9999),
                Location = new System.Drawing.Point(130, 18),
                Size = new System.Drawing.Size(230, 20)
            };
            this.Controls.Add(playerNameTextBox);
            
            // Server address
            Label addressLabel = new Label
            {
                Text = "Server IP:",
                Location = new System.Drawing.Point(20, 50),
                Size = new System.Drawing.Size(100, 20)
            };
            this.Controls.Add(addressLabel);
            
            serverAddressTextBox = new TextBox
            {
                Text = "localhost",
                Location = new System.Drawing.Point(130, 48),
                Size = new System.Drawing.Size(230, 20)
            };
            this.Controls.Add(serverAddressTextBox);
            
            // Server port
            Label portLabel = new Label
            {
                Text = "Port:",
                Location = new System.Drawing.Point(20, 80),
                Size = new System.Drawing.Size(100, 20)
            };
            this.Controls.Add(portLabel);
            
            serverPortTextBox = new TextBox
            {
                Text = "8888",
                Location = new System.Drawing.Point(130, 78),
                Size = new System.Drawing.Size(230, 20)
            };
            this.Controls.Add(serverPortTextBox);
            
            // Buttons
            hostButton = new Button
            {
                Text = "Host Game",
                Location = new System.Drawing.Point(20, 120),
                Size = new System.Drawing.Size(160, 30)
            };
            hostButton.Click += HostButton_Click;
            this.Controls.Add(hostButton);
            
            joinButton = new Button
            {
                Text = "Join Game",
                Location = new System.Drawing.Point(200, 120),
                Size = new System.Drawing.Size(160, 30)
            };
            joinButton.Click += JoinButton_Click;
            this.Controls.Add(joinButton);
            
            cancelButton = new Button
            {
                Text = "Cancel",
                Location = new System.Drawing.Point(140, 200),
                Size = new System.Drawing.Size(100, 30)
            };
            cancelButton.Click += CancelButton_Click;
            this.Controls.Add(cancelButton);
            
            // Status label
            statusLabel = new Label
            {
                Text = "",
                Location = new System.Drawing.Point(20, 160),
                Size = new System.Drawing.Size(340, 20),
                ForeColor = System.Drawing.Color.Blue
            };
            this.Controls.Add(statusLabel);
            
            playerCountLabel = new Label
            {
                Text = "",
                Location = new System.Drawing.Point(20, 180),
                Size = new System.Drawing.Size(340, 20),
                ForeColor = System.Drawing.Color.Green
            };
            this.Controls.Add(playerCountLabel);
        }
        
        private async void HostButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(playerNameTextBox.Text))
            {
                MessageBox.Show("Please enter a player name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            // Disable buttons during connection
            hostButton.Enabled = false;
            joinButton.Enabled = false;
            
            try
            {
                server = new GameServer();
                server.OnPlayerJoined += (name) =>
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() => UpdateStatus($"Player {name} joined!")));
                        this.Invoke(new Action(() => UpdatePlayerCount()));
                    }
                };
                
                server.OnPlayerLeft += (id) =>
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() => UpdateStatus("A player left.")));
                        this.Invoke(new Action(() => UpdatePlayerCount()));
                    }
                };
                
                UpdateStatus("Starting server...");
                server.Start();
                
                // Give server a moment to start listening
                await Task.Delay(500);
                
                UpdateStatus("Connecting to server...");
                
                // Connect as client to own server
                client = new GameClient();
                bool connectedSuccessfully = false;
                
                client.OnConnected += () =>
                {
                    connectedSuccessfully = true;
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            PlayerName = playerNameTextBox.Text;
                            ServerAddress = "localhost";
                            ServerPort = int.Parse(serverPortTextBox.Text);
                            IsHosting = true;
                            Connected = true;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }));
                    }
                };
                
                client.OnError += (error) =>
                {
                    if (this.InvokeRequired && !connectedSuccessfully)
                    {
                        this.Invoke(new Action(() => 
                        {
                            UpdateStatus($"Error: {error}");
                            hostButton.Enabled = true;
                            joinButton.Enabled = true;
                        }));
                    }
                };
                
                bool connectResult = await client.ConnectAsync("localhost", int.Parse(serverPortTextBox.Text), playerNameTextBox.Text);
                
                // Wait for connection confirmation (PlayerJoined message) with timeout
                int timeout = 5000; // 5 seconds
                int elapsed = 0;
                int checkInterval = 100; // Check every 100ms
                
                while (!connectedSuccessfully && elapsed < timeout)
                {
                    await Task.Delay(checkInterval);
                    elapsed += checkInterval;
                }
                
                if (connectedSuccessfully)
                {
                    Server = server;
                    Client = client;
                }
                else
                {
                    UpdateStatus("Connection timeout. Server may not be responding.");
                    server?.Stop();
                    client?.Disconnect();
                    client = null;
                    hostButton.Enabled = true;
                    joinButton.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to host game: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server?.Stop();
                hostButton.Enabled = true;
                joinButton.Enabled = true;
                UpdateStatus("Ready. Click Host Game or Join Game.");
            }
        }
        
        private async void JoinButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(playerNameTextBox.Text))
            {
                MessageBox.Show("Please enter a player name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (string.IsNullOrWhiteSpace(serverAddressTextBox.Text))
            {
                MessageBox.Show("Please enter a server address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (!int.TryParse(serverPortTextBox.Text, out int port))
            {
                MessageBox.Show("Please enter a valid port number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            try
            {
                client = new GameClient();
                
                client.OnConnected += () =>
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            PlayerName = playerNameTextBox.Text;
                            ServerAddress = serverAddressTextBox.Text;
                            ServerPort = port;
                            IsHosting = false;
                            Connected = true;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }));
                    }
                };
                
                client.OnError += (error) =>
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() => UpdateStatus($"Error: {error}")));
                    }
                };
                
                client.OnPlayerJoined += (name) =>
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() => UpdateStatus($"Connected! Players: {name}")));
                    }
                };
                
                UpdateStatus("Connecting to server...");
                bool connected = await client.ConnectAsync(serverAddressTextBox.Text, port, playerNameTextBox.Text);
                
                if (connected)
                {
                    Client = client;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to connect: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus("Connection failed.");
            }
        }
        
        private void CancelButton_Click(object sender, EventArgs e)
        {
            client?.Disconnect();
            server?.Stop();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        private void UpdateStatus(string message)
        {
            statusLabel.Text = message;
        }
        
        private void UpdatePlayerCount()
        {
            if (server != null)
            {
                playerCountLabel.Text = $"Players: {server.GetPlayerCount()}";
            }
        }
        
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !Connected)
            {
                client?.Disconnect();
                server?.Stop();
            }
            base.OnFormClosing(e);
        }
    }
}

