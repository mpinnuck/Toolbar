namespace Toolbar
{
    partial class ToolbarFrm
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
            listBox = new ListBox();
            SuspendLayout();
            // 
            // listBox
            // 
            listBox.FormattingEnabled = true;
            listBox.Location = new Point(3, 1);
            listBox.Name = "listBox";
            listBox.Size = new Size(167, 68);
            listBox.TabIndex = 0;
            // 
            // ToolbarFrm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(232, 73);
            Controls.Add(listBox);
            Name = "ToolbarFrm";
            Text = "Toolbar";
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox;
    }
}
