
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SAEK_Game_Project
{
    public partial class Form1 : Form
    {
        // ============================
        // ΜΕΤΑΒΛΗΤΕΣ ΠΑΙΧΝΙΔΙΟΥ
        // ============================
        private int score = 0;
        private int lives = 3;
        private int ballSpeed = 5;
        private readonly Random rnd = new Random();
        private bool gameRunning = false;
        // Αρχική οθόνη και Game Over
private Panel menuPanel;
private Label menuTitle;
private Label menuSubtitle;
private Button btnStartGame;
private Label gameOverTitle;
private Label gameOverScore;
private Button btnRestart;
private bool gameOver = false;

        private readonly HashSet<Keys> pressedKeys = new HashSet<Keys>();

        // Neon παλέτα
        private readonly Color neonBlue = Color.FromArgb(0, 210, 255);
        private readonly Color neonPurple = Color.FromArgb(190, 60, 255);
        private readonly Color deepBackground = Color.FromArgb(8, 8, 24);

        public Form1()
        {
            InitializeComponent();

            DoubleBuffered = true;
            KeyPreview = true;

            // Αφήνουμε το custom drawing να σχεδιάσει
            // τη μπάλα και την πλατφόρμα.
            picBall.BackColor = Color.Transparent;
            picPlayer.BackColor = Color.Transparent;

            picBall.Size = new Size(38, 38);
            picPlayer.Size = new Size(110, 22);

           picBall.Paint += PicBall_Paint;
picPlayer.Paint += PicPlayer_Paint;

BackColor = deepBackground;
Paint += Form1_Paint;
}

private void Form1_Paint(object? sender, PaintEventArgs e)
{
    Graphics g = e.Graphics;
    g.SmoothingMode = SmoothingMode.AntiAlias;
    
    // Neon arcade background grid
    int top = panelHUD.Bottom;
    int gridSize = 40;

    using (var gridPen = new Pen(Color.FromArgb(28, 0, 210, 255), 1))
    {
        for (int x = 0; x < ClientSize.Width; x += gridSize)
        {
            g.DrawLine(gridPen, x, top, x, ClientSize.Height);
        }

        for (int y = top; y < ClientSize.Height; y += gridSize)
        {
            g.DrawLine(gridPen, 0, y, ClientSize.Width, y);
        }
    }

    // Μωβ φωτισμός στο κάτω μέρος
    using (var glowBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
        new Rectangle(0, top, ClientSize.Width, ClientSize.Height - top),
        Color.FromArgb(0, 190, 60, 255),
        Color.FromArgb(35, 8, 8, 24),
        System.Drawing.Drawing2D.LinearGradientMode.Vertical))
    {
        g.FillRectangle(
            glowBrush,
            0,
            top,
            ClientSize.Width,
            ClientSize.Height - top
        );
    }
    using (Pen blueGrid = new Pen(
        Color.FromArgb(22, 0, 210, 255), 1))
    using (Pen purpleGrid = new Pen(
        Color.FromArgb(15, 190, 60, 255), 1))
    {
        // Κάθετες γραμμές neon.
        for (int x = 0; x < ClientSize.Width; x += gridSize)
        {
            g.DrawLine(blueGrid, x, top, x, ClientSize.Height);
        }

        // Οριζόντιες γραμμές neon.
        for (int y = top + gridSize; y < ClientSize.Height; y += gridSize)
        {
            g.DrawLine(purpleGrid, 0, y, ClientSize.Width, y);
        }
    }

    // Διακριτικό περίγραμμα στο πεδίο παιχνιδιού.
    using (Pen border = new Pen(
        Color.FromArgb(65, 0, 210, 255), 1))
    {
        g.DrawRectangle(
            border,
            1,
            top,
            ClientSize.Width - 3,
            ClientSize.Height - top - 2
        );
    }
}



        // ============================
        // ΣΧΕΔΙΑΣΗ NEON ΜΠΑΛΑΣ
        // ============================
        private void PicBall_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            float w = picBall.ClientSize.Width;
            float h = picBall.ClientSize.Height;

            // Εξωτερική μωβ λάμψη
            using (var glow1 = new SolidBrush(Color.FromArgb(35, 190, 60, 255)))
                g.FillEllipse(glow1, 1, 1, w - 2, h - 2);

            using (var glow2 = new Pen(Color.FromArgb(100, 190, 60, 255), 2))
                g.DrawEllipse(glow2, 3, 3, w - 6, h - 6);

            // Φωτεινός πυρήνας
            RectangleF core = new RectangleF(7, 7, w - 14, h - 14);

            using (var brush = new LinearGradientBrush(
                core,
                Color.FromArgb(245, 180, 100, 255),
                Color.FromArgb(100, 20, 220, 255),
                LinearGradientMode.ForwardDiagonal))
            {
                g.FillEllipse(brush, core);
            }

            // Λευκή αντανάκλαση
            using (var shine = new SolidBrush(Color.FromArgb(210, 255, 255, 255)))
                g.FillEllipse(shine, 11, 10, 7, 5);

            using (var border = new Pen(Color.FromArgb(230, 225, 190, 255), 1.5f))
                g.DrawEllipse(border, core);
        }

        // ============================
        // ΣΧΕΔΙΑΣΗ NEON ΠΛΑΤΦΟΡΜΑΣ
        // ============================
        private void PicPlayer_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            float w = picPlayer.ClientSize.Width;
            float h = picPlayer.ClientSize.Height;

            RectangleF body = new RectangleF(5, 5, w - 10, h - 10);

            // Εξωτερικό glow
            using (var glow = new Pen(Color.FromArgb(90, 0, 210, 255), 5))
            using (var path = RoundedRectangle(body, 7))
                g.DrawPath(glow, path);

            // Σώμα πλατφόρμας
            using (var path = RoundedRectangle(body, 7))
            using (var fill = new LinearGradientBrush(
                body,
                Color.FromArgb(0, 230, 255),
                Color.FromArgb(35, 90, 255),
                LinearGradientMode.Horizontal))
            {
                g.FillPath(fill, path);

                using (var border = new Pen(Color.FromArgb(220, 220, 255, 255), 1.5f))
                    g.DrawPath(border, path);
            }

            // Φωτεινή γραμμή στην κορυφή
            using (var highlight = new Pen(Color.FromArgb(220, 255, 255, 255), 1.2f))
                g.DrawLine(highlight, 12, 7, w - 12, 7);
        }

        // Βοηθητική μέθοδος για στρογγυλεμένα ορθογώνια
        private GraphicsPath RoundedRectangle(RectangleF rect, float radius)
        {
            float d = radius * 2;
            GraphicsPath path = new GraphicsPath();

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }

        // ============================
        // ΠΛΗΚΤΡΟΛΟΓΙΟ
        // ============================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left)
            {
                pressedKeys.Add(Keys.Left);
                return true;
            }

            if (keyData == Keys.Right)
            {
                pressedKeys.Add(Keys.Right);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        // ============================
        // ΕΚΚΙΝΗΣΗ
        // ============================
        private void Form1_Load(object sender, EventArgs e)
{
    ResetGame();
    UpdateUI();
    gameTimer.Stop();

    gameRunning = false;
    CreateStartScreen();
}

        // ============================
        // ΚΥΡΙΟΣ ΒΡΟΧΟΣ ΠΑΙΧΝΙΔΙΟΥ
        // ============================
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            int playerSpeed = 18;

            if (pressedKeys.Contains(Keys.Left) && picPlayer.Left > 0)
                picPlayer.Left -= playerSpeed;

            if (pressedKeys.Contains(Keys.Right) &&
                picPlayer.Right < ClientSize.Width)
                picPlayer.Left += playerSpeed;

            pressedKeys.Clear();

            // Κίνηση μπάλας
            picBall.Top += ballSpeed;

            // Έλεγχος σύλληψης
            if (picPlayer.Bounds.IntersectsWith(picBall.Bounds))
            {
                score++;
                lblScore.Text = "Score: " + score;

                ResetBall();

                if (score % 5 == 0)
                {
                    ballSpeed += 2;
                    lblLevel.Text = "Level: " + (score / 5 + 1);
                }
            }

            // Έλεγχος απώλειας μπάλας
            if (picBall.Top > ClientSize.Height)
            {
                lives--;
                lblLives.Text = "Lives: " + lives;

                ResetBall();

                if (lives <= 0)
                {
                    gameTimer.Stop();
                    gameRunning = false;
                    btnPause.Text = "▶ Start";

                    MessageBox.Show(
                        $"💀 Game Over!\n\nΤελικό Σκορ: {score}\n" +
                        $"Επίπεδο που έφτασες: {score / 5 + 1}",
                        "Τέλος Παιχνιδιού",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    ShowGameOverScreen();
                    return;
                }
            }
        }

        // ============================
        // ΠΛΗΚΤΡΟ ESCAPE
        // ============================
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                gameTimer.Stop();
                gameRunning = false;
                btnPause.Text = "Resume";
                ActiveControl = null;
            }
        }

        // ============================
        // START / PAUSE
        // ============================
        private void btnPause_Click(object sender, EventArgs e)
        {
            gameRunning = !gameRunning;
            gameTimer.Enabled = gameRunning;

            btnPause.Text = gameRunning ? "Pause" : "Resume";

            ActiveControl = null;
        }

        // ============================
        // ΕΠΑΝΑΦΟΡΑ ΜΠΑΛΑΣ
        // ============================
        private void ResetBall()
        {
            picBall.Top = -picBall.Height;
            picBall.Left = rnd.Next(
                10,
                Math.Max(11, ClientSize.Width - picBall.Width - 10)
            );
        }

        // ============================
        // ΕΠΑΝΑΦΟΡΑ ΠΑΙΧΝΙΔΙΟΥ
        // ============================
        private void ResetGame()
        {
            score = 0;
            lives = 3;
            ballSpeed = 5;

            picPlayer.Left = (ClientSize.Width - picPlayer.Width) / 2;
            picPlayer.Top = ClientSize.Height - picPlayer.Height - 20;

            ResetBall();
        }

        // ============================
        // ΕΝΗΜΕΡΩΣΗ ΕΝΔΕΙΞΕΩΝ
        // ============================
        private void UpdateUI()
        {
            lblScore.Text = "Score: 0";
            lblLives.Text = "Lives: 3";
            lblLevel.Text = "Level: 1";
            btnPause.Text = "▶ Start";
        }
    
private void CreateStartScreen()
{
    menuPanel = new Panel();
    menuPanel.Name = "menuPanel";
    menuPanel.Dock = DockStyle.Fill;
    menuPanel.BackColor = Color.FromArgb(8, 8, 24);

    menuTitle = new Label();
    menuTitle.Text = "CATCH THE BALL";
    menuTitle.Font = new Font("Segoe UI", 30F, FontStyle.Bold);
    menuTitle.ForeColor = Color.FromArgb(0, 210, 255);
    menuTitle.TextAlign = ContentAlignment.MiddleCenter;
    menuTitle.Dock = DockStyle.Top;
    menuTitle.Height = 115;

    menuSubtitle = new Label();
    menuSubtitle.Text = "Catch it. Score higher. Beat your record.";
    menuSubtitle.Font = new Font("Segoe UI", 12F);
    menuSubtitle.ForeColor = Color.FromArgb(190, 190, 230);
    menuSubtitle.TextAlign = ContentAlignment.MiddleCenter;
    menuSubtitle.Dock = DockStyle.Top;
    menuSubtitle.Height = 55;

    btnStartGame = new Button();
    btnStartGame.Text = "▶  START GAME";
    btnStartGame.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
    btnStartGame.ForeColor = Color.White;
    btnStartGame.BackColor = Color.FromArgb(120, 35, 190);
    btnStartGame.FlatStyle = FlatStyle.Flat;
    btnStartGame.FlatAppearance.BorderColor =
        Color.FromArgb(0, 210, 255);
    btnStartGame.FlatAppearance.BorderSize = 2;
    btnStartGame.Size = new Size(230, 58);
    btnStartGame.Location = new Point(
        (ClientSize.Width - btnStartGame.Width) / 2,
        300
    );
    btnStartGame.Cursor = Cursors.Hand;
    btnStartGame.Click += (s, e) => StartNewGame();

    menuPanel.Controls.Add(btnStartGame);
    menuPanel.Controls.Add(menuSubtitle);
    menuPanel.Controls.Add(menuTitle);

    Controls.Add(menuPanel);
    menuPanel.BringToFront();
}

private void StartNewGame()
{
    if (menuPanel != null)
        Controls.Remove(menuPanel);

    if (gameOverTitle != null)
    {
        Controls.Remove(gameOverTitle);
        gameOverTitle.Dispose();
        gameOverTitle = null;
    }

    if (gameOverScore != null)
    {
        Controls.Remove(gameOverScore);
        gameOverScore.Dispose();
        gameOverScore = null;
    }

    if (btnRestart != null)
    {
        Controls.Remove(btnRestart);
        btnRestart.Dispose();
        btnRestart = null;
    }

    gameOver = false;
    gameRunning = true;

    ResetGame();
    UpdateUI();

    gameTimer.Start();
    ActiveControl = null;
}

private void ShowGameOverScreen()
{
    gameTimer.Stop();
    gameRunning = false;
    gameOver = true;
    pressedKeys.Clear();

    gameOverTitle = new Label();
    gameOverTitle.Text = "GAME OVER";
    gameOverTitle.Font = new Font("Segoe UI", 30F, FontStyle.Bold);
    gameOverTitle.ForeColor = Color.FromArgb(255, 60, 172);
    gameOverTitle.BackColor = Color.FromArgb(8, 8, 24);
    gameOverTitle.TextAlign = ContentAlignment.MiddleCenter;
    gameOverTitle.Size = new Size(500, 75);
    gameOverTitle.Location = new Point(
        (ClientSize.Width - 500) / 2,
        210
    );

    gameOverScore = new Label();
    gameOverScore.Text = $"FINAL SCORE: {score}\nLEVEL REACHED: {score / 5 + 1}";
    gameOverScore.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
    gameOverScore.ForeColor = Color.FromArgb(0, 210, 255);
    gameOverScore.BackColor = Color.FromArgb(8, 8, 24);
    gameOverScore.TextAlign = ContentAlignment.MiddleCenter;
    gameOverScore.Size = new Size(400, 75);
    gameOverScore.Location = new Point(
        (ClientSize.Width - 400) / 2,
        285
    );

    btnRestart = new Button();
    btnRestart.Text = "↻  PLAY AGAIN";
    btnRestart.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
    btnRestart.ForeColor = Color.White;
    btnRestart.BackColor = Color.FromArgb(120, 35, 190);
    btnRestart.FlatStyle = FlatStyle.Flat;
    btnRestart.FlatAppearance.BorderColor =
        Color.FromArgb(0, 210, 255);
    btnRestart.FlatAppearance.BorderSize = 2;
    btnRestart.Size = new Size(220, 55);
    btnRestart.Location = new Point(
        (ClientSize.Width - 220) / 2,
        385
    );
    btnRestart.Cursor = Cursors.Hand;
    btnRestart.Click += (s, e) => StartNewGame();

    Controls.Add(gameOverTitle);
    Controls.Add(gameOverScore);
    Controls.Add(btnRestart);

    gameOverTitle.BringToFront();
    gameOverScore.BringToFront();
    btnRestart.BringToFront();

    ActiveControl = null;
}
    }
}