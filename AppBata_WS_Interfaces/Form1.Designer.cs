namespace AppBata_WS_Interfaces
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
            this.btnhel = new System.Windows.Forms.Button();
            this.btn_ws_update_transaction_guias = new System.Windows.Forms.Button();
            this.btnenvio = new System.Windows.Forms.Button();
            this.btn_servicewin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnhel
            // 
            this.btnhel.Location = new System.Drawing.Point(58, 12);
            this.btnhel.Name = "btnhel";
            this.btnhel.Size = new System.Drawing.Size(75, 23);
            this.btnhel.TabIndex = 0;
            this.btnhel.Text = "hellow";
            this.btnhel.UseVisualStyleBackColor = true;
            this.btnhel.Click += new System.EventHandler(this.btnhel_Click);
            // 
            // btn_ws_update_transaction_guias
            // 
            this.btn_ws_update_transaction_guias.Location = new System.Drawing.Point(182, 22);
            this.btn_ws_update_transaction_guias.Name = "btn_ws_update_transaction_guias";
            this.btn_ws_update_transaction_guias.Size = new System.Drawing.Size(194, 23);
            this.btn_ws_update_transaction_guias.TabIndex = 1;
            this.btn_ws_update_transaction_guias.Text = "ws_update_transaction_guias";
            this.btn_ws_update_transaction_guias.UseVisualStyleBackColor = true;
            this.btn_ws_update_transaction_guias.Click += new System.EventHandler(this.btn_ws_update_transaction_guias_Click);
            // 
            // btnenvio
            // 
            this.btnenvio.Location = new System.Drawing.Point(182, 72);
            this.btnenvio.Name = "btnenvio";
            this.btnenvio.Size = new System.Drawing.Size(308, 75);
            this.btnenvio.TabIndex = 2;
            this.btnenvio.Text = "ws_update_transaction_guias ENVIO DBF";
            this.btnenvio.UseVisualStyleBackColor = true;
            this.btnenvio.Click += new System.EventHandler(this.btnenvio_Click);
            // 
            // btn_servicewin
            // 
            this.btn_servicewin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_servicewin.Location = new System.Drawing.Point(84, 258);
            this.btn_servicewin.Name = "btn_servicewin";
            this.btn_servicewin.Size = new System.Drawing.Size(392, 51);
            this.btn_servicewin.TabIndex = 3;
            this.btn_servicewin.Text = "CAPA DE PRUEBA SERVIVIO WINDOWS";
            this.btn_servicewin.UseVisualStyleBackColor = true;
            this.btn_servicewin.Click += new System.EventHandler(this.btn_servicewin_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 357);
            this.Controls.Add(this.btn_servicewin);
            this.Controls.Add(this.btnenvio);
            this.Controls.Add(this.btn_ws_update_transaction_guias);
            this.Controls.Add(this.btnhel);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WS BATA INTERFACES";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnhel;
        private System.Windows.Forms.Button btn_ws_update_transaction_guias;
        private System.Windows.Forms.Button btnenvio;
        private System.Windows.Forms.Button btn_servicewin;
    }
}

