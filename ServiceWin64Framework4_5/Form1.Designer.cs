namespace ServiceWin64Framework4_5
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
            this.btn_ejecutar_genera_file_xstore_auto = new System.Windows.Forms.Button();
            this.btnejecutar_genera_interface_xstore = new System.Windows.Forms.Button();
            this.btn_guias = new System.Windows.Forms.Button();
            this.btn_enviosftp = new System.Windows.Forms.Button();
            this.btnstk_ec = new System.Windows.Forms.Button();
            this.btnupdate_bataweb = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_ejecutar_genera_file_xstore_auto
            // 
            this.btn_ejecutar_genera_file_xstore_auto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ejecutar_genera_file_xstore_auto.Location = new System.Drawing.Point(12, 12);
            this.btn_ejecutar_genera_file_xstore_auto.Name = "btn_ejecutar_genera_file_xstore_auto";
            this.btn_ejecutar_genera_file_xstore_auto.Size = new System.Drawing.Size(334, 51);
            this.btn_ejecutar_genera_file_xstore_auto.TabIndex = 0;
            this.btn_ejecutar_genera_file_xstore_auto.Text = "GENERA INTERFACE AUTOMATICA PROGRAMADO";
            this.btn_ejecutar_genera_file_xstore_auto.UseVisualStyleBackColor = true;
            this.btn_ejecutar_genera_file_xstore_auto.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnejecutar_genera_interface_xstore
            // 
            this.btnejecutar_genera_interface_xstore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnejecutar_genera_interface_xstore.Location = new System.Drawing.Point(12, 75);
            this.btnejecutar_genera_interface_xstore.Name = "btnejecutar_genera_interface_xstore";
            this.btnejecutar_genera_interface_xstore.Size = new System.Drawing.Size(334, 51);
            this.btnejecutar_genera_interface_xstore.TabIndex = 1;
            this.btnejecutar_genera_interface_xstore.Text = "GENERA INTERFACE AUTOMATICA DESDE APLICACION";
            this.btnejecutar_genera_interface_xstore.UseVisualStyleBackColor = true;
            this.btnejecutar_genera_interface_xstore.Click += new System.EventHandler(this.btnejecutar_genera_interface_xstore_Click);
            // 
            // btn_guias
            // 
            this.btn_guias.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_guias.Location = new System.Drawing.Point(12, 141);
            this.btn_guias.Name = "btn_guias";
            this.btn_guias.Size = new System.Drawing.Size(334, 51);
            this.btn_guias.TabIndex = 2;
            this.btn_guias.Text = "GENERA INTERFACE GUIAS";
            this.btn_guias.UseVisualStyleBackColor = true;
            this.btn_guias.Click += new System.EventHandler(this.btn_guias_Click);
            // 
            // btn_enviosftp
            // 
            this.btn_enviosftp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_enviosftp.Location = new System.Drawing.Point(12, 204);
            this.btn_enviosftp.Name = "btn_enviosftp";
            this.btn_enviosftp.Size = new System.Drawing.Size(334, 51);
            this.btn_enviosftp.TabIndex = 3;
            this.btn_enviosftp.Text = "ENVIO DE SFTP";
            this.btn_enviosftp.UseVisualStyleBackColor = true;
            this.btn_enviosftp.Click += new System.EventHandler(this.btn_enviosftp_Click);
            // 
            // btnstk_ec
            // 
            this.btnstk_ec.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnstk_ec.Location = new System.Drawing.Point(12, 265);
            this.btnstk_ec.Name = "btnstk_ec";
            this.btnstk_ec.Size = new System.Drawing.Size(334, 41);
            this.btnstk_ec.TabIndex = 4;
            this.btnstk_ec.Text = "Generar Stock E-COMMERCE";
            this.btnstk_ec.UseVisualStyleBackColor = true;
            this.btnstk_ec.Click += new System.EventHandler(this.btnstk_ec_Click);
            // 
            // btnupdate_bataweb
            // 
            this.btnupdate_bataweb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnupdate_bataweb.Location = new System.Drawing.Point(369, 12);
            this.btnupdate_bataweb.Name = "btnupdate_bataweb";
            this.btnupdate_bataweb.Size = new System.Drawing.Size(334, 51);
            this.btnupdate_bataweb.TabIndex = 5;
            this.btnupdate_bataweb.Text = "ACTUALIZAR BATAWEB DLL PRODUCCION";
            this.btnupdate_bataweb.UseVisualStyleBackColor = true;
            this.btnupdate_bataweb.Click += new System.EventHandler(this.btnupdate_bataweb_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 318);
            this.Controls.Add(this.btnupdate_bataweb);
            this.Controls.Add(this.btnstk_ec);
            this.Controls.Add(this.btn_enviosftp);
            this.Controls.Add(this.btn_guias);
            this.Controls.Add(this.btnejecutar_genera_interface_xstore);
            this.Controls.Add(this.btn_ejecutar_genera_file_xstore_auto);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_ejecutar_genera_file_xstore_auto;
        private System.Windows.Forms.Button btnejecutar_genera_interface_xstore;
        private System.Windows.Forms.Button btn_guias;
        private System.Windows.Forms.Button btn_enviosftp;
        private System.Windows.Forms.Button btnstk_ec;
        private System.Windows.Forms.Button btnupdate_bataweb;
    }
}

