namespace TPWinForm_Presentacion
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
            this.dgvArticulo = new System.Windows.Forms.DataGridView();
            this.fpImagen = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEleminar = new System.Windows.Forms.Button();
<<<<<<< HEAD
            this.btnVerDetalle = new System.Windows.Forms.Button();
=======
            this.btnAgregar = new System.Windows.Forms.Button();
>>>>>>> b8daf4f393344c5ba27515e85f819b72136d50ba
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticulo)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvArticulo
            // 
            this.dgvArticulo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArticulo.Location = new System.Drawing.Point(60, 135);
            this.dgvArticulo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvArticulo.Name = "dgvArticulo";
            this.dgvArticulo.RowHeadersWidth = 51;
            this.dgvArticulo.Size = new System.Drawing.Size(617, 279);
            this.dgvArticulo.TabIndex = 0;
            this.dgvArticulo.SelectionChanged += new System.EventHandler(this.dgvArticulo_SelectionChanged);
            // 
            // fpImagen
            // 
            this.fpImagen.AutoScroll = true;
            this.fpImagen.Location = new System.Drawing.Point(721, 135);
            this.fpImagen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.fpImagen.Name = "fpImagen";
            this.fpImagen.Size = new System.Drawing.Size(197, 279);
            this.fpImagen.TabIndex = 1;
            this.fpImagen.Paint += new System.Windows.Forms.PaintEventHandler(this.fpImagen_Paint);
            // 
            // btnEleminar
            // 
            this.btnEleminar.Location = new System.Drawing.Point(60, 442);
            this.btnEleminar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEleminar.Name = "btnEleminar";
            this.btnEleminar.Size = new System.Drawing.Size(100, 28);
            this.btnEleminar.TabIndex = 2;
            this.btnEleminar.Text = "Elimnar";
            this.btnEleminar.UseVisualStyleBackColor = true;
            this.btnEleminar.Click += new System.EventHandler(this.btnEleminar_Click);
            // 
<<<<<<< HEAD
            // btnVerDetalle
            // 
            this.btnVerDetalle.Location = new System.Drawing.Point(167, 442);
            this.btnVerDetalle.Name = "btnVerDetalle";
            this.btnVerDetalle.Size = new System.Drawing.Size(107, 28);
            this.btnVerDetalle.TabIndex = 3;
            this.btnVerDetalle.Text = "Ver Detalle";
            this.btnVerDetalle.UseVisualStyleBackColor = true;
            this.btnVerDetalle.Click += new System.EventHandler(this.btnVerDetalle_Click);
=======
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(404, 364);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 3;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
>>>>>>> b8daf4f393344c5ba27515e85f819b72136d50ba
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
<<<<<<< HEAD
            this.ClientSize = new System.Drawing.Size(977, 585);
            this.Controls.Add(this.btnVerDetalle);
=======
            this.ClientSize = new System.Drawing.Size(733, 475);
            this.Controls.Add(this.btnAgregar);
>>>>>>> b8daf4f393344c5ba27515e85f819b72136d50ba
            this.Controls.Add(this.btnEleminar);
            this.Controls.Add(this.fpImagen);
            this.Controls.Add(this.dgvArticulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticulo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvArticulo;
        private System.Windows.Forms.FlowLayoutPanel fpImagen;
        private System.Windows.Forms.Button btnEleminar;
<<<<<<< HEAD
        private System.Windows.Forms.Button btnVerDetalle;
=======
        private System.Windows.Forms.Button btnAgregar;
>>>>>>> b8daf4f393344c5ba27515e85f819b72136d50ba
    }
}

