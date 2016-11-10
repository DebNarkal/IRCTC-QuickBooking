namespace IRCTC_QuickBooking
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
            this.button1 = new System.Windows.Forms.Button();
            this.frmStn = new System.Windows.Forms.TextBox();
            this.doj = new System.Windows.Forms.DateTimePicker();
            this.toStn = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.avlTrains = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(809, 243);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmStn
            // 
            this.frmStn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmStn.Location = new System.Drawing.Point(136, 27);
            this.frmStn.Name = "frmStn";
            this.frmStn.Size = new System.Drawing.Size(222, 30);
            this.frmStn.TabIndex = 1;
            this.frmStn.TextChanged += new System.EventHandler(this.frmStn_TextChanged);
            // 
            // doj
            // 
            this.doj.CalendarFont = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.doj.CustomFormat = "";
            this.doj.Location = new System.Drawing.Point(137, 149);
            this.doj.MinDate = new System.DateTime(2016, 10, 27, 0, 0, 0, 0);
            this.doj.Name = "doj";
            this.doj.Size = new System.Drawing.Size(246, 22);
            this.doj.TabIndex = 3;
            this.doj.Value = new System.DateTime(2016, 11, 5, 0, 0, 0, 0);
            // 
            // toStn
            // 
            this.toStn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toStn.Location = new System.Drawing.Point(136, 82);
            this.toStn.Name = "toStn";
            this.toStn.Size = new System.Drawing.Size(222, 30);
            this.toStn.TabIndex = 4;
            this.toStn.TextChanged += new System.EventHandler(this.toStn_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 22);
            this.label1.TabIndex = 5;
            this.label1.Text = "From Station:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 22);
            this.label2.TabIndex = 6;
            this.label2.Text = "To Station:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 22);
            this.label3.TabIndex = 7;
            this.label3.Text = "Journey Date:";
            // 
            // avlTrains
            // 
            this.avlTrains.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.avlTrains.Enabled = false;
            this.avlTrains.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.avlTrains.FormattingEnabled = true;
            this.avlTrains.Location = new System.Drawing.Point(208, 204);
            this.avlTrains.Name = "avlTrains";
            this.avlTrains.Size = new System.Drawing.Size(293, 30);
            this.avlTrains.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(51, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 22);
            this.label4.TabIndex = 9;
            this.label4.Text = "Trains Available:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 451);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.avlTrains);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toStn);
            this.Controls.Add(this.doj);
            this.Controls.Add(this.frmStn);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox frmStn;
        private System.Windows.Forms.DateTimePicker doj;
        private System.Windows.Forms.TextBox toStn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox avlTrains;
        private System.Windows.Forms.Label label4;
    }
}

