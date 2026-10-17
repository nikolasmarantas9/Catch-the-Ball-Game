namespace SAEK_Game_Project
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // --- Δήλωση όλων των controls ---
            this.picPlayer   = new System.Windows.Forms.PictureBox();
            this.picBall     = new System.Windows.Forms.PictureBox();
            this.lblScore    = new System.Windows.Forms.Label();
            this.lblLives    = new System.Windows.Forms.Label();
            this.lblLevel    = new System.Windows.Forms.Label();
            this.lblTitle    = new System.Windows.Forms.Label();
            this.btnPause    = new System.Windows.Forms.Button();
            this.gameTimer   = new System.Windows.Forms.Timer(this.components);
            this.panelHUD    = new System.Windows.Forms.Panel();

            ((System.ComponentModel.ISupportInitialize)(this.picPlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBall)).BeginInit();
            this.panelHUD.SuspendLayout();
            this.SuspendLayout();

            // ============================
            // panelHUD (Μπάρα πληροφοριών)
            // ============================
            this.panelHUD.BackColor = System.Drawing.Color.FromArgb(12, 12, 30);
            this.panelHUD.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHUD.Height = 65;
            this.panelHUD.Controls.Add(this.lblTitle);
            this.panelHUD.Controls.Add(this.lblScore);
            this.panelHUD.Controls.Add(this.lblLives);
            this.panelHUD.Controls.Add(this.lblLevel);
            this.panelHUD.Controls.Add(this.btnPause);

            // ============================
            // lblTitle
            // ============================
            this.lblTitle.Text = "🎮 CATCH THE BALL";
            this.lblTitle.Font = new System.Drawing.Font( "Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor =System.Drawing.Color.FromArgb(0, 229, 255);
            this.lblTitle.Location = new System.Drawing.Point(10, 17);
            this.lblTitle.Size = new System.Drawing.Size(260, 30);
            this.lblTitle.AutoSize = false;

            // ============================
            // lblScore
            // ============================
            this.lblScore.Name = "lblScore";
            this.lblScore.Text = "Score: 0";
            this.lblScore.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblScore.ForeColor = System.Drawing.Color.FromArgb(0, 255, 170);
            this.lblScore.Location = new System.Drawing.Point(280, 17);
            this.lblScore.Size = new System.Drawing.Size(130, 28);
            this.lblScore.AutoSize = false;

            // ============================
            // lblLives
            // ============================
            this.lblLives.Name = "lblLives";
            this.lblLives.Text = "Lives: 3";
            this.lblLives.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLives.ForeColor =System.Drawing.Color.FromArgb(255, 60, 172);;
            this.lblLives.Location = new System.Drawing.Point(420, 17);
            this.lblLives.Size = new System.Drawing.Size(120, 28);
            this.lblLives.AutoSize = false;

            // ============================
            // lblLevel
            // ============================
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Text = "Level: 1";
            this.lblLevel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLevel.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblLevel.Location = new System.Drawing.Point(550, 17);
            this.lblLevel.Size = new System.Drawing.Size(120, 28);
            this.lblLevel.AutoSize = false;

            // ============================
            // btnPause (Start/Pause)
            // ============================
            this.btnPause.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
this.btnPause.BackColor =System.Drawing.Color.FromArgb(25, 20, 55);
this.btnPause.ForeColor = System.Drawing.Color.FromArgb(0, 229, 255);
this.btnPause.FlatStyle =System.Windows.Forms.FlatStyle.Flat;
this.btnPause.FlatAppearance.BorderColor =System.Drawing.Color.FromArgb(168, 85, 247);
this.btnPause.FlatAppearance.BorderSize = 2;
this.btnPause.Location = new System.Drawing.Point(690, 12);
this.btnPause.Size = new System.Drawing.Size(120, 38);
this.btnPause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);

            // ============================
            // picPlayer (Το καλάθι/παίκτης)
            // ============================
            this.picPlayer.Name = "picPlayer";
            this.picPlayer.BackColor =System.Drawing.Color.FromArgb(0, 180, 255);
            this.picPlayer.Size = new System.Drawing.Size(100, 20);
            this.picPlayer.Location = new System.Drawing.Point(300, 540);
            this.picPlayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            // ============================
            // picBall (Η μπάλα/φρούτο)
            // ============================
            this.picBall.Name = "picBall";
            this.picBall.BackColor =System.Drawing.Color.FromArgb(190, 70, 255);
            this.picBall.Size = new System.Drawing.Size(30, 30);
            this.picBall.Location = new System.Drawing.Point(200, -30);
            this.picBall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            // ============================
            // gameTimer
            // ============================
            this.gameTimer.Interval = 20;
            this.gameTimer.Enabled = false;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);

            // ============================
            // Form1 (Κύρια φόρμα)
            // ============================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            // Φόντο παιχνιδιού
            this.BackColor = System.Drawing.Color.FromArgb(8, 8, 24);
            this.ClientSize = new System.Drawing.Size(830, 600);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Text = "SAEK Arcade - Catch the Ball";
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Controls.Add(this.panelHUD);
            this.Controls.Add(this.picPlayer);
            this.Controls.Add(this.picBall);

            this.Load  += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);

            ((System.ComponentModel.ISupportInitialize)(this.picPlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBall)).EndInit();
            this.panelHUD.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // --- Δηλώσεις πεδίων ---
        private System.Windows.Forms.PictureBox picPlayer;
        private System.Windows.Forms.PictureBox picBall;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblLives;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Panel panelHUD;
    }
}
