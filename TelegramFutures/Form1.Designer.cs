namespace TelegramFutures
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
            this.telegramReader1 = new TelegramFutures.TelegramReader();
            this.telegramReader2 = new TelegramFutures.TelegramReader();
            this.telegramReader3 = new TelegramFutures.TelegramReader();
            this.btnEnviarOrdenBinance = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // telegramReader1
            // 
            this.telegramReader1.Location = new System.Drawing.Point(0, 0);
            this.telegramReader1.Name = "telegramReader1";
            this.telegramReader1.Size = new System.Drawing.Size(0, 0);
            this.telegramReader1.TabIndex = 0;
            // 
            // telegramReader2
            // 
            this.telegramReader2.Location = new System.Drawing.Point(0, 0);
            this.telegramReader2.Name = "telegramReader2";
            this.telegramReader2.Size = new System.Drawing.Size(0, 0);
            this.telegramReader2.TabIndex = 1;
            // 
            // telegramReader3
            // 
            this.telegramReader3.Location = new System.Drawing.Point(12, 12);
            this.telegramReader3.Name = "telegramReader3";
            this.telegramReader3.Size = new System.Drawing.Size(410, 328);
            this.telegramReader3.TabIndex = 2;
            // 
            // btnEnviarOrdenBinance
            // 
            this.btnEnviarOrdenBinance.Location = new System.Drawing.Point(501, 12);
            this.btnEnviarOrdenBinance.Name = "btnEnviarOrdenBinance";
            this.btnEnviarOrdenBinance.Size = new System.Drawing.Size(166, 23);
            this.btnEnviarOrdenBinance.TabIndex = 3;
            this.btnEnviarOrdenBinance.Text = "Enviar Orden Binance";
            this.btnEnviarOrdenBinance.UseVisualStyleBackColor = true;
            this.btnEnviarOrdenBinance.Click += new System.EventHandler(this.btnEnviarOrdenBinance_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1253, 450);
            this.Controls.Add(this.btnEnviarOrdenBinance);
            this.Controls.Add(this.telegramReader3);
            this.Controls.Add(this.telegramReader2);
            this.Controls.Add(this.telegramReader1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private TelegramReader telegramReader1;
        private TelegramReader telegramReader2;
        private TelegramReader telegramReader3;
        private System.Windows.Forms.Button btnEnviarOrdenBinance;
    }
}

