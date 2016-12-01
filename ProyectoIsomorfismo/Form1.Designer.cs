namespace ProyectoIsomorfismo
{
    partial class Form1
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
            this.btnGrafo1 = new System.Windows.Forms.Button();
            this.btnGrafo2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnComprobar = new System.Windows.Forms.Button();
            this.Grafos = new System.Windows.Forms.GroupBox();
            this.btnDibujarGrafos = new System.Windows.Forms.Button();
            this.pbGrafo1 = new System.Windows.Forms.PictureBox();
            this.pbGrafo2 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.Grafos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGrafo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGrafo2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGrafo1
            // 
            this.btnGrafo1.Location = new System.Drawing.Point(10, 32);
            this.btnGrafo1.Name = "btnGrafo1";
            this.btnGrafo1.Size = new System.Drawing.Size(75, 23);
            this.btnGrafo1.TabIndex = 0;
            this.btnGrafo1.Text = "Grafo 1";
            this.btnGrafo1.UseVisualStyleBackColor = true;
            this.btnGrafo1.Click += new System.EventHandler(this.btnGrafo1_Click);
            // 
            // btnGrafo2
            // 
            this.btnGrafo2.Location = new System.Drawing.Point(91, 32);
            this.btnGrafo2.Name = "btnGrafo2";
            this.btnGrafo2.Size = new System.Drawing.Size(75, 23);
            this.btnGrafo2.TabIndex = 1;
            this.btnGrafo2.Text = "Grafo 2";
            this.btnGrafo2.UseVisualStyleBackColor = true;
            this.btnGrafo2.Click += new System.EventHandler(this.btnGrafo2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGrafo2);
            this.groupBox1.Controls.Add(this.btnGrafo1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(181, 62);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Carga de grafos";
            // 
            // btnComprobar
            // 
            this.btnComprobar.Enabled = false;
            this.btnComprobar.Location = new System.Drawing.Point(233, 44);
            this.btnComprobar.Name = "btnComprobar";
            this.btnComprobar.Size = new System.Drawing.Size(129, 23);
            this.btnComprobar.TabIndex = 2;
            this.btnComprobar.Text = "Comprobar Isomorfismo";
            this.btnComprobar.UseVisualStyleBackColor = true;
            this.btnComprobar.Click += new System.EventHandler(this.btnComprobar_Click);
            // 
            // Grafos
            // 
            this.Grafos.Controls.Add(this.pbGrafo2);
            this.Grafos.Controls.Add(this.pbGrafo1);
            this.Grafos.Location = new System.Drawing.Point(22, 93);
            this.Grafos.Name = "Grafos";
            this.Grafos.Size = new System.Drawing.Size(556, 239);
            this.Grafos.TabIndex = 3;
            this.Grafos.TabStop = false;
            this.Grafos.Text = "Grafos";
            // 
            // btnDibujarGrafos
            // 
            this.btnDibujarGrafos.Location = new System.Drawing.Point(368, 44);
            this.btnDibujarGrafos.Name = "btnDibujarGrafos";
            this.btnDibujarGrafos.Size = new System.Drawing.Size(131, 23);
            this.btnDibujarGrafos.TabIndex = 4;
            this.btnDibujarGrafos.Text = "Ver Grafos";
            this.btnDibujarGrafos.UseVisualStyleBackColor = true;
            // 
            // pbGrafo1
            // 
            this.pbGrafo1.Location = new System.Drawing.Point(20, 19);
            this.pbGrafo1.Name = "pbGrafo1";
            this.pbGrafo1.Size = new System.Drawing.Size(250, 200);
            this.pbGrafo1.TabIndex = 0;
            this.pbGrafo1.TabStop = false;
            // 
            // pbGrafo2
            // 
            this.pbGrafo2.Location = new System.Drawing.Point(288, 19);
            this.pbGrafo2.Name = "pbGrafo2";
            this.pbGrafo2.Size = new System.Drawing.Size(250, 200);
            this.pbGrafo2.TabIndex = 1;
            this.pbGrafo2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 349);
            this.Controls.Add(this.btnDibujarGrafos);
            this.Controls.Add(this.Grafos);
            this.Controls.Add(this.btnComprobar);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.Grafos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbGrafo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGrafo2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGrafo1;
        private System.Windows.Forms.Button btnGrafo2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnComprobar;
        private System.Windows.Forms.GroupBox Grafos;
        private System.Windows.Forms.Button btnDibujarGrafos;
        private System.Windows.Forms.PictureBox pbGrafo2;
        private System.Windows.Forms.PictureBox pbGrafo1;
    }
}

