namespace WinWS_Paperless
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnpaperless = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnpaperless
            // 
            this.btnpaperless.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnpaperless.Location = new System.Drawing.Point(10, 111);
            this.btnpaperless.Name = "btnpaperless";
            this.btnpaperless.Size = new System.Drawing.Size(264, 39);
            this.btnpaperless.TabIndex = 23;
            this.btnpaperless.Text = "CONSUMIR WS PAPERLESS";
            this.btnpaperless.UseVisualStyleBackColor = true;
            this.btnpaperless.Click += new System.EventHandler(this.btnpaperless_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnpaperless);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnpaperless;
    }
}

