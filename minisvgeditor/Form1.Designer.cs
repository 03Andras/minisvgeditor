namespace minisvgeditor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            colorDialog1 = new ColorDialog();
            panel1 = new Panel();
            colorvalaszto = new Button();
            button_mentes = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(23, 14);
            button1.Name = "button1";
            button1.Size = new Size(91, 33);
            button1.TabIndex = 2;
            button1.Text = "Vonal";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(120, 14);
            button2.Name = "button2";
            button2.Size = new Size(91, 33);
            button2.TabIndex = 3;
            button2.Text = "Téglalap";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(217, 14);
            button3.Name = "button3";
            button3.Size = new Size(91, 33);
            button3.TabIndex = 4;
            button3.Text = "Ellipszis";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = SystemColors.ControlLight;
            button4.Location = new Point(570, 462);
            button4.Name = "button4";
            button4.Size = new Size(91, 33);
            button4.TabIndex = 5;
            button4.Text = "Mentés (SVG)";
            button4.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLightLight;
            panel1.Location = new Point(16, 65);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 600);
            panel1.TabIndex = 6;
            panel1.Paint += panel1_Paint;
            panel1.MouseDown += panel_MouseDown;
            panel1.MouseMove += panel_MouseMove;
            panel1.MouseUp += panel1_MouseUp;
            // 
            // colorvalaszto
            // 
            colorvalaszto.Location = new Point(368, 16);
            colorvalaszto.Name = "colorvalaszto";
            colorvalaszto.Size = new Size(141, 31);
            colorvalaszto.TabIndex = 7;
            colorvalaszto.Text = "Szin";
            colorvalaszto.UseVisualStyleBackColor = true;
            colorvalaszto.Click += colorvalaszto_Click;
            // 
            // button_mentes
            // 
            button_mentes.Location = new Point(758, 671);
            button_mentes.Name = "button_mentes";
            button_mentes.Size = new Size(101, 36);
            button_mentes.TabIndex = 8;
            button_mentes.Text = "Mentés (SVG)";
            button_mentes.UseVisualStyleBackColor = true;
            button_mentes.Click += button_mentes_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(871, 727);
            Controls.Add(button_mentes);
            Controls.Add(colorvalaszto);
            Controls.Add(panel1);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            DoubleBuffered = true;
            Name = "Form1";
            Text = "Mini SVG Editor";
            ResumeLayout(false);
        }

        #endregion
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private ColorDialog colorDialog1;
        private Panel panel1;
        private Button colorvalaszto;
        private Button button_mentes;
    }
}
