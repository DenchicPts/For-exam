namespace Apgerbs
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.manRadioBut = new System.Windows.Forms.RadioButton();
            this.femaleRadioBut = new System.Windows.Forms.RadioButton();
            this.BrandName = new System.Windows.Forms.ComboBox();
            this.ClothType = new System.Windows.Forms.ComboBox();
            this.CheckButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // manRadioBut
            // 
            this.manRadioBut.AutoSize = true;
            this.manRadioBut.BackColor = System.Drawing.SystemColors.Control;
            this.manRadioBut.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.manRadioBut.Location = new System.Drawing.Point(173, 25);
            this.manRadioBut.Name = "manRadioBut";
            this.manRadioBut.Size = new System.Drawing.Size(77, 29);
            this.manRadioBut.TabIndex = 0;
            this.manRadioBut.TabStop = true;
            this.manRadioBut.Text = "Male";
            this.manRadioBut.UseVisualStyleBackColor = false;
            // 
            // femaleRadioBut
            // 
            this.femaleRadioBut.AutoSize = true;
            this.femaleRadioBut.BackColor = System.Drawing.SystemColors.Control;
            this.femaleRadioBut.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.femaleRadioBut.Location = new System.Drawing.Point(173, 93);
            this.femaleRadioBut.Name = "femaleRadioBut";
            this.femaleRadioBut.Size = new System.Drawing.Size(101, 29);
            this.femaleRadioBut.TabIndex = 2;
            this.femaleRadioBut.TabStop = true;
            this.femaleRadioBut.Text = "Female";
            this.femaleRadioBut.UseVisualStyleBackColor = false;
            // 
            // BrandName
            // 
            this.BrandName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BrandName.FormattingEnabled = true;
            this.BrandName.Location = new System.Drawing.Point(12, 25);
            this.BrandName.Name = "BrandName";
            this.BrandName.Size = new System.Drawing.Size(121, 33);
            this.BrandName.TabIndex = 3;
            this.BrandName.SelectedIndexChanged += new System.EventHandler(this.BrandName_SelectedIndexChanged);
            // 
            // ClothType
            // 
            this.ClothType.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClothType.FormattingEnabled = true;
            this.ClothType.Location = new System.Drawing.Point(12, 101);
            this.ClothType.Name = "ClothType";
            this.ClothType.Size = new System.Drawing.Size(121, 33);
            this.ClothType.TabIndex = 4;
            this.ClothType.SelectedIndexChanged += new System.EventHandler(this.ClothType_SelectedIndexChanged);
            // 
            // CheckButton
            // 
            this.CheckButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CheckButton.Location = new System.Drawing.Point(72, 250);
            this.CheckButton.Name = "CheckButton";
            this.CheckButton.Size = new System.Drawing.Size(142, 49);
            this.CheckButton.TabIndex = 5;
            this.CheckButton.Text = "Check";
            this.CheckButton.UseVisualStyleBackColor = true;
            this.CheckButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Brand name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Type of clothing";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CheckButton);
            this.Controls.Add(this.ClothType);
            this.Controls.Add(this.BrandName);
            this.Controls.Add(this.femaleRadioBut);
            this.Controls.Add(this.manRadioBut);
            this.Name = "Form1";
            this.Text = "Apgerbs";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton manRadioBut;
        private System.Windows.Forms.RadioButton femaleRadioBut;
        private System.Windows.Forms.ComboBox BrandName;
        private System.Windows.Forms.ComboBox ClothType;
        private System.Windows.Forms.Button CheckButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

