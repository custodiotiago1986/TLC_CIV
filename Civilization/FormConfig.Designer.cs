namespace CivilizationGame
{
    partial class FormConfig
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            numericUpDownLandWater = new NumericUpDown();
            comboBoxMapSize = new ComboBox();
            btnStartGame = new Button();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDownLandWater).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(67, 93);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(185, 20);
            label1.TabIndex = 0;
            label1.Text = "Land Percentage (0-100%):";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(67, 154);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(73, 20);
            label2.TabIndex = 1;
            label2.Text = "Map Size:";
            // 
            // numericUpDownLandWater
            // 
            numericUpDownLandWater.Location = new Point(333, 91);
            numericUpDownLandWater.Margin = new Padding(4, 5, 4, 5);
            numericUpDownLandWater.Name = "numericUpDownLandWater";
            numericUpDownLandWater.Size = new Size(107, 27);
            numericUpDownLandWater.TabIndex = 2;
            // 
            // comboBoxMapSize
            // 
            comboBoxMapSize.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxMapSize.FormattingEnabled = true;
            comboBoxMapSize.Items.AddRange(new object[] { "36x36", "72x72" });
            comboBoxMapSize.Location = new Point(333, 154);
            comboBoxMapSize.Margin = new Padding(4, 5, 4, 5);
            comboBoxMapSize.Name = "comboBoxMapSize";
            comboBoxMapSize.Size = new Size(132, 28);
            comboBoxMapSize.TabIndex = 3;
            // 
            // btnStartGame
            // 
            btnStartGame.Location = new Point(191, 223);
            btnStartGame.Margin = new Padding(4, 5, 4, 5);
            btnStartGame.Name = "btnStartGame";
            btnStartGame.Size = new Size(133, 46);
            btnStartGame.TabIndex = 4;
            btnStartGame.Text = "Start Game";
            btnStartGame.Click += btnStartGame_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(67, 31);
            label3.Name = "label3";
            label3.Size = new Size(147, 20);
            label3.TabIndex = 5;
            label3.Text = "New Game Settings";
            // 
            // FormConfig
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(533, 313);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(numericUpDownLandWater);
            Controls.Add(comboBoxMapSize);
            Controls.Add(btnStartGame);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FormConfig";
            Text = "TLC CIV";
            ((System.ComponentModel.ISupportInitialize)numericUpDownLandWater).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownLandWater;
        private System.Windows.Forms.ComboBox comboBoxMapSize;
        private System.Windows.Forms.Button btnStartGame;
        private Label label3;
    }
}
