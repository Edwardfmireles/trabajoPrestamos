namespace Prestamos
{
    partial class buscarCliente
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bcDataGridView = new System.Windows.Forms.DataGridView();
            this.bcBusqueda = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.eccancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bcDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // bcDataGridView
            // 
            this.bcDataGridView.AllowUserToAddRows = false;
            this.bcDataGridView.AllowUserToDeleteRows = false;
            this.bcDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bcDataGridView.Location = new System.Drawing.Point(32, 65);
            this.bcDataGridView.MultiSelect = false;
            this.bcDataGridView.Name = "bcDataGridView";
            this.bcDataGridView.ReadOnly = true;
            this.bcDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.bcDataGridView.Size = new System.Drawing.Size(528, 150);
            this.bcDataGridView.TabIndex = 7;
            this.bcDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.bcDataGridView_CellDoubleClick);
            // 
            // bcBusqueda
            // 
            this.bcBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bcBusqueda.Location = new System.Drawing.Point(144, 28);
            this.bcBusqueda.Name = "bcBusqueda";
            this.bcBusqueda.Size = new System.Drawing.Size(178, 22);
            this.bcBusqueda.TabIndex = 5;
            this.bcBusqueda.TextChanged += new System.EventHandler(this.bcBusqueda_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Buscar cliente";
            // 
            // eccancelar
            // 
            this.eccancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eccancelar.Location = new System.Drawing.Point(328, 28);
            this.eccancelar.Name = "eccancelar";
            this.eccancelar.Size = new System.Drawing.Size(76, 22);
            this.eccancelar.TabIndex = 8;
            this.eccancelar.Text = "Insertar";
            this.eccancelar.UseVisualStyleBackColor = true;
            this.eccancelar.Click += new System.EventHandler(this.eccancelar_Click);
            // 
            // buscarCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 242);
            this.Controls.Add(this.eccancelar);
            this.Controls.Add(this.bcDataGridView);
            this.Controls.Add(this.bcBusqueda);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "buscarCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buscar cliente";
            this.Load += new System.EventHandler(this.buscarCliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bcDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox bcBusqueda;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button eccancelar;
        public System.Windows.Forms.DataGridView bcDataGridView;
    }
}