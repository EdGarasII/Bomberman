using System;
using System.Drawing;
using System.Windows.Forms;

namespace BombermanGame
{
    // Main menu to choose game mode
    public partial class MainMenuForm : Form
    {
        private Button singlePlayerButton;
        private Button multiplayerButton;
        private Button exitButton;
        
        public bool IsMultiplayer { get; private set; }
        
        public MainMenuForm()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            this.Text = "Bomberman - Main Menu";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            Label titleLabel = new Label
            {
                Text = "BOMBERMAN",
                Font = new Font("Arial", 24, FontStyle.Bold),
                Location = new Point(80, 30),
                Size = new Size(240, 50),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(titleLabel);
            
            singlePlayerButton = new Button
            {
                Text = "Single Player",
                Location = new Point(100, 100),
                Size = new Size(200, 40),
                Font = new Font("Arial", 12)
            };
            singlePlayerButton.Click += SinglePlayerButton_Click;
            this.Controls.Add(singlePlayerButton);
            
            multiplayerButton = new Button
            {
                Text = "Multiplayer (Online)",
                Location = new Point(100, 150),
                Size = new Size(200, 40),
                Font = new Font("Arial", 12)
            };
            multiplayerButton.Click += MultiplayerButton_Click;
            this.Controls.Add(multiplayerButton);
            
            exitButton = new Button
            {
                Text = "Exit",
                Location = new Point(100, 220),
                Size = new Size(200, 30)
            };
            exitButton.Click += ExitButton_Click;
            this.Controls.Add(exitButton);
        }
        
        private void SinglePlayerButton_Click(object sender, EventArgs e)
        {
            IsMultiplayer = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        
        private void MultiplayerButton_Click(object sender, EventArgs e)
        {
            IsMultiplayer = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}


