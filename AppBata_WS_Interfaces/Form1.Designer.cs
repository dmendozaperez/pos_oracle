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
            this.btnupload = new System.Windows.Forms.Button();
            this.ws_get_time_servicetrans = new System.Windows.Forms.Button();
            this.ws_envia_stock_tda = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ws_get_stk_tda = new System.Windows.Forms.Button();
            this.ws_transmision_ingreso_nube = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
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
            this.btnenvio.Location = new System.Drawing.Point(299, 51);
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
            this.btn_servicewin.Location = new System.Drawing.Point(-4, 132);
            this.btn_servicewin.Name = "btn_servicewin";
            this.btn_servicewin.Size = new System.Drawing.Size(392, 51);
            this.btn_servicewin.TabIndex = 3;
            this.btn_servicewin.Text = "CAPA DE PRUEBA SERVIVIO WINDOWS";
            this.btn_servicewin.UseVisualStyleBackColor = true;
            this.btn_servicewin.Click += new System.EventHandler(this.btn_servicewin_Click);
            // 
            // btnupload
            // 
            this.btnupload.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnupload.Location = new System.Drawing.Point(12, 211);
            this.btnupload.Name = "btnupload";
            this.btnupload.Size = new System.Drawing.Size(320, 41);
            this.btnupload.TabIndex = 4;
            this.btnupload.Text = "UPLOAD FILE";
            this.btnupload.UseVisualStyleBackColor = true;
            this.btnupload.Click += new System.EventHandler(this.btnupload_Click);
            // 
            // ws_get_time_servicetrans
            // 
            this.ws_get_time_servicetrans.Location = new System.Drawing.Point(12, 62);
            this.ws_get_time_servicetrans.Name = "ws_get_time_servicetrans";
            this.ws_get_time_servicetrans.Size = new System.Drawing.Size(187, 26);
            this.ws_get_time_servicetrans.TabIndex = 5;
            this.ws_get_time_servicetrans.Text = "ws_get_time_servicetrans";
            this.ws_get_time_servicetrans.UseVisualStyleBackColor = true;
            this.ws_get_time_servicetrans.Click += new System.EventHandler(this.ws_get_time_servicetrans_Click);
            // 
            // ws_envia_stock_tda
            // 
            this.ws_envia_stock_tda.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ws_envia_stock_tda.Location = new System.Drawing.Point(654, 22);
            this.ws_envia_stock_tda.Name = "ws_envia_stock_tda";
            this.ws_envia_stock_tda.Size = new System.Drawing.Size(191, 32);
            this.ws_envia_stock_tda.TabIndex = 6;
            this.ws_envia_stock_tda.Text = "ws_envia_stock_tda";
            this.ws_envia_stock_tda.UseVisualStyleBackColor = true;
            this.ws_envia_stock_tda.Click += new System.EventHandler(this.ws_envia_stock_tda_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Khaki;
            this.groupBox1.Controls.Add(this.ws_get_stk_tda);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 269);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(685, 84);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "WEB SERVICE ECCOMERCE";
            // 
            // ws_get_stk_tda
            // 
            this.ws_get_stk_tda.Location = new System.Drawing.Point(55, 37);
            this.ws_get_stk_tda.Name = "ws_get_stk_tda";
            this.ws_get_stk_tda.Size = new System.Drawing.Size(131, 23);
            this.ws_get_stk_tda.TabIndex = 0;
            this.ws_get_stk_tda.Text = "ws_get_stk_tda";
            this.ws_get_stk_tda.UseVisualStyleBackColor = true;
            this.ws_get_stk_tda.Click += new System.EventHandler(this.ws_get_stk_tda_Click);
            // 
            // ws_transmision_ingreso_nube
            // 
            this.ws_transmision_ingreso_nube.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ws_transmision_ingreso_nube.Location = new System.Drawing.Point(369, 217);
            this.ws_transmision_ingreso_nube.Name = "ws_transmision_ingreso_nube";
            this.ws_transmision_ingreso_nube.Size = new System.Drawing.Size(238, 31);
            this.ws_transmision_ingreso_nube.TabIndex = 8;
            this.ws_transmision_ingreso_nube.Text = "ws_transmision_ingreso_nube";
            this.ws_transmision_ingreso_nube.UseVisualStyleBackColor = true;
            this.ws_transmision_ingreso_nube.Click += new System.EventHandler(this.ws_transmision_ingreso_nube_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 357);
            this.Controls.Add(this.ws_transmision_ingreso_nube);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ws_envia_stock_tda);
            this.Controls.Add(this.ws_get_time_servicetrans);
            this.Controls.Add(this.btnupload);
            this.Controls.Add(this.btn_servicewin);
            this.Controls.Add(this.btnenvio);
            this.Controls.Add(this.btn_ws_update_transaction_guias);
            this.Controls.Add(this.btnhel);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WS BATA INTERFACES";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnhel;
        private System.Windows.Forms.Button btn_ws_update_transaction_guias;
        private System.Windows.Forms.Button btnenvio;
        private System.Windows.Forms.Button btn_servicewin;
        private System.Windows.Forms.Button btnupload;
        private System.Windows.Forms.Button ws_get_time_servicetrans;
        private System.Windows.Forms.Button ws_envia_stock_tda;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ws_get_stk_tda;
        private System.Windows.Forms.Button ws_transmision_ingreso_nube;
    }
}

