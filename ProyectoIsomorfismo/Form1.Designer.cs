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
            this.groupBox1.SuspendLayout();
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
            this.btnComprobar.Location = new System.Drawing.Point(243, 44);
            this.btnComprobar.Name = "btnComprobar";
            this.btnComprobar.Size = new System.Drawing.Size(129, 23);
            this.btnComprobar.TabIndex = 2;
            this.btnComprobar.Text = "Comprobar Isomorfismo";
            this.btnComprobar.UseVisualStyleBackColor = true;
            this.btnComprobar.Click += new System.EventHandler(this.btnComprobar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 261);
            this.Controls.Add(this.btnComprobar);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGrafo1;
        private System.Windows.Forms.Button btnGrafo2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnComprobar;
    }
}

