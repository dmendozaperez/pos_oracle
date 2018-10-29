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
            this.button1 = new System.Windows.Forms.Button();
            this.btnbarra = new System.Windows.Forms.Button();
            this.btnenvio_nov = new System.Windows.Forms.Button();
            this.btnenviog = new System.Windows.Forms.Button();
            this.btnposlog = new System.Windows.Forms.Button();
            this.procesar_fcacb_SQL = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_servicewin
            // 
            this.btn_servicewin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_servicewin.Location = new System.Drawing.Point(12, 32);
            this.btn_servicewin.Name = "btn_servicewin";
            this.btn_servicewin.Size = new System.Drawing.Size(392, 51);
            this.btn_servicewin.TabIndex = 4;
            this.btn_servicewin.Text = "CAPA DE PRUEBA SERVIVIO WINDOWS";
            this.btn_servicewin.UseVisualStyleBackColor = true;
            this.btn_servicewin.Click += new System.EventHandler(this.btn_servicewin_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(43, 139);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnbarra
            // 
            this.btnbarra.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnbarra.Location = new System.Drawing.Point(290, 165);
            this.btnbarra.Name = "btnbarra";
            this.btnbarra.Size = new System.Drawing.Size(172, 31);
            this.btnbarra.TabIndex = 6;
            this.btnbarra.Text = "COD BARRA";
            this.btnbarra.UseVisualStyleBackColor = true;
            this.btnbarra.Click += new System.EventHandler(this.btnbarra_Click);
            // 
            // btnenvio_nov
            // 
            this.btnenvio_nov.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnenvio_nov.Location = new System.Drawing.Point(43, 212);
            this.btnenvio_nov.Name = "btnenvio_nov";
            this.btnenvio_nov.Size = new System.Drawing.Size(185, 37);
            this.btnenvio_nov.TabIndex = 7;
            this.btnenvio_nov.Text = "ENVIO NOVELL";
            this.btnenvio_nov.UseVisualStyleBackColor = true;
            this.btnenvio_nov.Click += new System.EventHandler(this.btnenvio_nov_Click);
            // 
            // btnenviog
            // 
            this.btnenviog.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnenviog.Location = new System.Drawing.Point(214, 110);
            this.btnenviog.Name = "btnenviog";
            this.btnenviog.Size = new System.Drawing.Size(210, 34);
            this.btnenviog.TabIndex = 8;
            this.btnenviog.Text = "ENVIO DE GUIAS";
            this.btnenviog.UseVisualStyleBackColor = true;
            this.btnenviog.Click += new System.EventHandler(this.btnenviog_Click);
            // 
            // btnposlog
            // 
            this.btnposlog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnposlog.Location = new System.Drawing.Point(530, 32);
            this.btnposlog.Name = "btnposlog";
            this.btnposlog.Size = new System.Drawing.Size(166, 23);
            this.btnposlog.TabIndex = 9;
            this.btnposlog.Text = "ENVIO POS LOG";
            this.btnposlog.UseVisualStyleBackColor = true;
            this.btnposlog.Click += new System.EventHandler(this.btnposlog_Click);
            // 
            // procesar_fcacb_SQL
            // 
            this.procesar_fcacb_SQL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.procesar_fcacb_SQL.Location = new System.Drawing.Point(505, 200);
            this.procesar_fcacb_SQL.Name = "procesar_fcacb_SQL";
            this.procesar_fcacb_SQL.Size = new System.Drawing.Size(207, 37);
            this.procesar_fcacb_SQL.TabIndex = 10;
            this.procesar_fcacb_SQL.Text = "procesar_fcacb_SQL";
            this.procesar_fcacb_SQL.UseVisualStyleBackColor = true;
            this.procesar_fcacb_SQL.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(530, 95);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(152, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 261);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.procesar_fcacb_SQL);
            this.Controls.Add(this.btnposlog);
            this.Controls.Add(this.btnenviog);
            this.Controls.Add(this.btnenvio_nov);
            this.Controls.Add(this.btnbarra);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_servicewin);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_servicewin;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnbarra;
        private System.Windows.Forms.Button btnenvio_nov;
        private System.Windows.Forms.Button btnenviog;
        private System.Windows.Forms.Button btnposlog;
        private System.Windows.Forms.Button procesar_fcacb_SQL;
        private System.Windows.Forms.Button button3;
    }
}

