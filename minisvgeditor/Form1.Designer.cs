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
            button5 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(26, 19);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(104, 44);
            button1.TabIndex = 2;
            button1.Text = "Vonal";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(137, 19);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new Size(104, 44);
            button2.TabIndex = 3;
            button2.Text = "Téglalap";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(248, 19);
            button3.Margin = new Padding(3, 4, 3, 4);
            button3.Name = "button3";
            button3.Size = new Size(104, 44);
            button3.TabIndex = 4;
            button3.Text = "Ellipszis";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = SystemColors.ControlLight;
            button4.Location = new Point(651, 616);
            button4.Margin = new Padding(3, 4, 3, 4);
            button4.Name = "button4";
            button4.Size = new Size(104, 44);
            button4.TabIndex = 5;
            button4.Text = "Mentés (SVG)";
            button4.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLightLight;
            panel1.Location = new Point(18, 87);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(914, 800);
            panel1.TabIndex = 6;
            panel1.Paint += panel1_Paint;
            panel1.MouseDown += panel_MouseDown;
            panel1.MouseMove += panel_MouseMove;
            panel1.MouseUp += panel1_MouseUp;
            // 
            // colorvalaszto
            // 
            colorvalaszto.Location = new Point(421, 21);
            colorvalaszto.Margin = new Padding(3, 4, 3, 4);
            colorvalaszto.Name = "colorvalaszto";
            colorvalaszto.Size = new Size(161, 41);
            colorvalaszto.TabIndex = 7;
            colorvalaszto.Text = "Szin";
            colorvalaszto.UseVisualStyleBackColor = true;
            colorvalaszto.Click += colorvalaszto_Click;
            // 
            // button_mentes
            // 
            button_mentes.Location = new Point(866, 895);
            button_mentes.Margin = new Padding(3, 4, 3, 4);
            button_mentes.Name = "button_mentes";
            button_mentes.Size = new Size(115, 48);
            button_mentes.TabIndex = 8;
            button_mentes.Text = "Mentés (SVG)";
            button_mentes.UseVisualStyleBackColor = true;
            button_mentes.Click += button_mentes_Click;
            // 
            // button5
            // 
            button5.Location = new Point(710, 12);
            button5.Name = "button5";
            button5.Size = new Size(135, 50);
            button5.TabIndex = 0;
            button5.Text = "SVG betöltés";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(995, 969);
            Controls.Add(button5);
            Controls.Add(button_mentes);
            Controls.Add(colorvalaszto);
            Controls.Add(panel1);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            DoubleBuffered = true;
            Margin = new Padding(3, 4, 3, 4);
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
        private Button button5;
    }
}
