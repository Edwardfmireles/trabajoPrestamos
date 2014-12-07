namespace Prestamos
{
    partial class login
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
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
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.busuario = new System.Windows.Forms.TextBox();
            this.bcontrasena = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(42, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario";
            // 
            // busuario
            // 
            this.busuario.BackColor = System.Drawing.SystemColors.Window;
            this.busuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.busuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.busuario.Location = new System.Drawing.Point(160, 32);
            this.busuario.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.busuario.Name = "busuario";
            this.busuario.Size = new System.Drawing.Size(183, 26);
            this.busuario.TabIndex = 0;
            this.busuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.busuario_KeyPress);
            // 
            // bcontrasena
            // 
            this.bcontrasena.BackColor = System.Drawing.SystemColors.Window;
            this.bcontrasena.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bcontrasena.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bcontrasena.Location = new System.Drawing.Point(160, 66);
            this.bcontrasena.Margin = new System.Windows.Forms.Padding(6);
            this.bcontrasena.Name = "bcontrasena";
            this.bcontrasena.Size = new System.Drawing.Size(183, 26);
            this.bcontrasena.TabIndex = 1;
            this.bcontrasena.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.bcontrasena_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(42, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Contraseña";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(385, 121);
            this.Controls.Add(this.bcontrasena);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.busuario);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Form1";
            this.Text = "Autenticación";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox busuario;
        private System.Windows.Forms.TextBox bcontrasena;
        private System.Windows.Forms.Label label2;
    }
}
