namespace TelegramFutures
{
    partial class TelegramReader
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
            this.txtRead = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtRead
            // 
            this.txtRead.Location = new System.Drawing.Point(0, 0);
            this.txtRead.Multiline = true;
            this.txtRead.Name = "txtRead";
            this.txtRead.Size = new System.Drawing.Size(398, 323);
            this.txtRead.TabIndex = 0;
            // 
            // TelegramReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtRead);
            this.Name = "TelegramReader";
            this.Size = new System.Drawing.Size(398, 356);
            this.Load += new System.EventHandler(this.TelegramReader_Load);
            this.Leave += new System.EventHandler(this.TelegramReader_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRead;
    }
}
