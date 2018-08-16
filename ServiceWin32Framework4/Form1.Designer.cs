namespace ServiceWin32Framework4
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
            this.btn_servicewin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_servicewin
            // 
            this.btn_servicewin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_servicewin.Location = new System.Drawing.Point(79, 73);
            this.btn_servicewin.Name = "btn_servicewin";
            this.btn_servicewin.Size = new System.Drawing.Size(392, 51);
            this.btn_servicewin.TabIndex = 4;
            this.btn_servicewin.Text = "CAPA DE PRUEBA SERVIVIO WINDOWS";
            this.btn_servicewin.UseVisualStyleBackColor = true;
            this.btn_servicewin.Click += new System.EventHandler(this.btn_servicewin_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 261);
            this.Controls.Add(this.btn_servicewin);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_servicewin;
    }
}

