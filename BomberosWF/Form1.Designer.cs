namespace BomberosWF {
    partial class Form1 {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose (bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose ( );
            }
            base.Dispose (disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent ( ) {
            this.tipoIncidentesControl1 = new BomberosWF.TipoIncidentesControl();
            this.SuspendLayout();
            // 
            // tipoIncidentesControl1
            // 
            this.tipoIncidentesControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tipoIncidentesControl1.Location = new System.Drawing.Point(0, 0);
            this.tipoIncidentesControl1.Name = "tipoIncidentesControl1";
            this.tipoIncidentesControl1.Size = new System.Drawing.Size(284, 261);
            this.tipoIncidentesControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.tipoIncidentesControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private TipoIncidentesControl tipoIncidentesControl1;
    }
}

