namespace Scheduler_NEA
{
    partial class UserControlWeekDay
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
            SuspendLayout();
            // 
            // Day
            // 
            Day.AutoSize = true;
            Day.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Day.Location = new Point(3, 0);
            Day.Name = "Day";
            Day.Size = new Size(27, 20);
            Day.TabIndex = 0;
            Day.Text = "00";
            // 
            // UserControlWeekDay
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(Day);
            Name = "UserControlWeekDay";
            Size = new Size(176, 768);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Day;
    }
}
