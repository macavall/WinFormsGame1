using System;
using System.Drawing;
using System.Windows.Forms;

namespace RectangleMover
{
    public partial class Form1 : Form
    {
        private int rectX = 100; // Initial X position
        private int rectY = 100; // Initial Y position
        private const int rectSize = 50; // Rectangle size
        private const int moveSpeed = 1; // Pixels to move per tick (adjust for speed)

        private bool leftPressed = false;
        private bool rightPressed = false;
        private bool upPressed = false;
        private bool downPressed = false;

        private System.Windows.Forms.Timer movementTimer;

        public Form1()
        {
            InitializeComponent(); // Call designer initialization

            // Set up the form (overrides designer settings if needed)
            this.Text = "Smooth Rectangle Movement with Arrow Keys";
            this.Size = new Size(400, 400);
            this.DoubleBuffered = true; // Reduce flicker
            this.KeyPreview = true; // Ensure form captures key events
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
            this.Paint += Form1_Paint;

            // Set up the timer for smooth movement
            movementTimer = new System.Windows.Forms.Timer();
            movementTimer.Interval = 4; // ~60 FPS (adjust lower for faster updates, higher for slower)
            movementTimer.Tick += MovementTimer_Tick;
            movementTimer.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Draw the rectangle
            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.Red, rectX, rectY, rectSize, rectSize);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Set flags for pressed keys
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftPressed = true;
                    break;
                case Keys.Right:
                    rightPressed = true;
                    break;
                case Keys.Up:
                    upPressed = true;
                    break;
                case Keys.Down:
                    downPressed = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // Reset flags for released keys
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftPressed = false;
                    break;
                case Keys.Right:
                    rightPressed = false;
                    break;
                case Keys.Up:
                    upPressed = false;
                    break;
                case Keys.Down:
                    downPressed = false;
                    break;
            }
        }

        private void MovementTimer_Tick(object sender, EventArgs e)
        {
            // Update position based on pressed keys
            if (leftPressed) rectX -= moveSpeed;
            if (rightPressed) rectX += moveSpeed;
            if (upPressed) rectY -= moveSpeed;
            if (downPressed) rectY += moveSpeed;

            // Ensure rectangle stays within form bounds
            rectX = Math.Max(0, Math.Min(rectX, this.ClientSize.Width - rectSize));
            rectY = Math.Max(0, Math.Min(rectY, this.ClientSize.Height - rectSize));

            // Redraw if any movement occurred
            if (leftPressed || rightPressed || upPressed || downPressed)
            {
                this.Invalidate();
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}