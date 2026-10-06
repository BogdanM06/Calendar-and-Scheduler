namespace Scheduler_NEA
{
    partial class UserControlDays
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Day = new Label();
            Event1 = new Label();
            Event3 = new Label();
            Event2 = new Label();
            Event5 = new Label();
            Event4 = new Label();
            SuspendLayout();
            // 
            // Day
            // 
            Day.AutoSize = true;
            Day.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Day.Location = new Point(3, 0);
            Day.Name = "Day";
            Day.Size = new Size(30, 18);
            Day.TabIndex = 0;
            Day.Text = "00";
            // 
            // Event1
            // 
            Event1.BackColor = Color.Red;
            Event1.Font = new Font("Segoe UI", 8F);
            Event1.Location = new Point(0, 18);
            Event1.Name = "Event1";
            Event1.Size = new Size(172, 20);
            Event1.TabIndex = 1;
            Event1.Visible = false;
            // 
            // Event3
            // 
            Event3.BackColor = Color.FromArgb(128, 255, 128);
            Event3.Font = new Font("Segoe UI", 8F);
            Event3.Location = new Point(0, 58);
            Event3.Name = "Event3";
            Event3.Size = new Size(172, 20);
            Event3.TabIndex = 3;
            Event3.Visible = false;
            // 
            // Event2
            // 
            Event2.BackColor = Color.FromArgb(128, 128, 255);
            Event2.Font = new Font("Segoe UI", 8F);
            Event2.Location = new Point(0, 38);
            Event2.Name = "Event2";
            Event2.Size = new Size(172, 20);
            Event2.TabIndex = 4;
            Event2.Visible = false;
            // 
            // Event5
            // 
            Event5.BackColor = Color.FromArgb(255, 128, 255);
            Event5.Location = new Point(0, 98);
            Event5.Name = "Event5";
            Event5.Size = new Size(172, 20);
            Event5.TabIndex = 7;
            Event5.Visible = false;
            // 
            // Event4
            // 
            Event4.BackColor = Color.FromArgb(255, 128, 0);
            Event4.Font = new Font("Segoe UI", 8F);
            Event4.Location = new Point(0, 78);
            Event4.Name = "Event4";
            Event4.Size = new Size(172, 20);
            Event4.TabIndex = 5;
            Event4.Visible = false;
            // 
            // UserControlDays
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(Event5);
            Controls.Add(Event4);
            Controls.Add(Event2);
            Controls.Add(Event3);
            Controls.Add(Event1);
            Controls.Add(Day);
            Name = "UserControlDays";
            Size = new Size(172, 117);
            Load += UserControlDays_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Day;
        private Label Event1;
        private Label Event3;
        private Label Event2;
        private Label Event5;
        private Label Event4;
    }
}
