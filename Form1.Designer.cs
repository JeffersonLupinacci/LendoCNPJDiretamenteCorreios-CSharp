namespace LeituraWeb
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.memoEdit2 = new DevExpress.XtraEditors.MemoEdit();
            this.button1 = new System.Windows.Forms.Button();
            this.edCAPTCHA = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.edCNPJ = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edCAPTCHA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edCNPJ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 36);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(239, 55);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // memoEdit2
            // 
            this.memoEdit2.EditValue = resources.GetString("memoEdit2.EditValue");
            this.memoEdit2.Location = new System.Drawing.Point(12, 142);
            this.memoEdit2.Name = "memoEdit2";
            this.memoEdit2.Size = new System.Drawing.Size(537, 179);
            this.memoEdit2.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(257, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 25);
            this.button1.TabIndex = 6;
            this.button1.Text = "Reload";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // edCAPTCHA
            // 
            this.edCAPTCHA.Location = new System.Drawing.Point(141, 105);
            this.edCAPTCHA.Name = "edCAPTCHA";
            this.edCAPTCHA.Size = new System.Drawing.Size(86, 20);
            this.edCAPTCHA.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Digite a frase da imagem";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(233, 101);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 27);
            this.button2.TabIndex = 9;
            this.button2.Text = "Confirmar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // edCNPJ
            // 
            this.edCNPJ.Location = new System.Drawing.Point(52, 10);
            this.edCNPJ.Name = "edCNPJ";
            this.edCNPJ.Size = new System.Drawing.Size(245, 20);
            this.edCNPJ.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "CNPJ";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(347, 102);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // memoEdit1
            // 
            this.memoEdit1.Location = new System.Drawing.Point(15, 338);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new System.Drawing.Size(534, 149);
            this.memoEdit1.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 499);
            this.Controls.Add(this.memoEdit1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.edCNPJ);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.edCAPTCHA);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.memoEdit2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edCAPTCHA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edCNPJ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.MemoEdit memoEdit2;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraEditors.TextEdit edCAPTCHA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private DevExpress.XtraEditors.TextEdit edCNPJ;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
    }
}

