namespace CivilizationGame
{
    partial class FormGame
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
            panelMap = new Panel();
            lblYear = new Label();
            btnNextTurn = new Button();
            btnTechTree = new Button();
            lblTechChoiced = new Label();
            SuspendLayout();
            // 
            // panelMap
            // 
            panelMap.AutoScroll = true;
            panelMap.BorderStyle = BorderStyle.FixedSingle;
            panelMap.Location = new Point(26, 68);
            panelMap.Margin = new Padding(4, 5, 4, 5);
            panelMap.Name = "panelMap";
            panelMap.Size = new Size(850, 484);
            panelMap.TabIndex = 0;
            // 
            // lblYear
            // 
            lblYear.AutoSize = true;
            lblYear.Location = new Point(26, 26);
            lblYear.Name = "lblYear";
            lblYear.Size = new Size(41, 20);
            lblYear.TabIndex = 1;
            lblYear.Text = "4000";
            // 
            // btnNextTurn
            // 
            btnNextTurn.Location = new Point(792, 22);
            btnNextTurn.Name = "btnNextTurn";
            btnNextTurn.Size = new Size(84, 29);
            btnNextTurn.TabIndex = 2;
            btnNextTurn.Text = "Next Turn";
            btnNextTurn.UseVisualStyleBackColor = true;
            btnNextTurn.Click += BtnNextTurn_Click;
            // 
            // btnTechTree
            // 
            btnTechTree.Location = new Point(637, 22);
            btnTechTree.Name = "btnTechTree";
            btnTechTree.Size = new Size(94, 29);
            btnTechTree.TabIndex = 3;
            btnTechTree.Text = "Tech Tree";
            btnTechTree.UseVisualStyleBackColor = true;
            btnTechTree.Click += btnTechTree_Click;
            // 
            // lblTechChoiced
            // 
            lblTechChoiced.AutoSize = true;
            lblTechChoiced.Location = new Point(180, 26);
            lblTechChoiced.Name = "lblTechChoiced";
            lblTechChoiced.Size = new Size(0, 20);
            lblTechChoiced.TabIndex = 4;
            // 
            // FormGame
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 584);
            Controls.Add(lblTechChoiced);
            Controls.Add(btnTechTree);
            Controls.Add(btnNextTurn);
            Controls.Add(lblYear);
            Controls.Add(panelMap);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FormGame";
            Text = "TLC CIV";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Panel panelMap;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Button btnNextTurn;
        private Button btnTechTree;
        private Label lblTechChoiced;
    }
}
