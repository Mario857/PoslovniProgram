namespace Poslovni
{
    partial class ZakljuciRacun
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
            this.zakljuci_racun_button = new System.Windows.Forms.Button();
            this.otp_partneru = new System.Windows.Forms.CheckBox();
            this.otp_partneru_naziv = new System.Windows.Forms.TextBox();
            this.naziv_partnera = new System.Windows.Forms.TextBox();
            this.R1_racun = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.adresa_partnera = new System.Windows.Forms.TextBox();
            this.oib_partnera = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gotovina_iznos = new System.Windows.Forms.TextBox();
            this.kartica_iznos = new System.Windows.Forms.TextBox();
            this.karticar_odarbir = new System.Windows.Forms.TextBox();
            this.post_odabir = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // zakljuci_racun_button
            // 
            this.zakljuci_racun_button.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zakljuci_racun_button.Location = new System.Drawing.Point(13, 474);
            this.zakljuci_racun_button.Name = "zakljuci_racun_button";
            this.zakljuci_racun_button.Size = new System.Drawing.Size(420, 41);
            this.zakljuci_racun_button.TabIndex = 0;
            this.zakljuci_racun_button.Text = "Zaključi (F12)";
            this.zakljuci_racun_button.UseVisualStyleBackColor = true;
            // 
            // otp_partneru
            // 
            this.otp_partneru.AutoSize = true;
            this.otp_partneru.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.otp_partneru.Location = new System.Drawing.Point(28, 27);
            this.otp_partneru.Name = "otp_partneru";
            this.otp_partneru.Size = new System.Drawing.Size(161, 22);
            this.otp_partneru.TabIndex = 1;
            this.otp_partneru.Text = "Otpremnica partneru";
            this.otp_partneru.UseVisualStyleBackColor = true;
            this.otp_partneru.CheckedChanged += new System.EventHandler(this.otp_partneru_CheckedChanged);
            // 
            // otp_partneru_naziv
            // 
            this.otp_partneru_naziv.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.otp_partneru_naziv.Location = new System.Drawing.Point(195, 27);
            this.otp_partneru_naziv.Name = "otp_partneru_naziv";
            this.otp_partneru_naziv.Size = new System.Drawing.Size(223, 25);
            this.otp_partneru_naziv.TabIndex = 2;
            // 
            // naziv_partnera
            // 
            this.naziv_partnera.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.naziv_partnera.Location = new System.Drawing.Point(187, 58);
            this.naziv_partnera.Name = "naziv_partnera";
            this.naziv_partnera.Size = new System.Drawing.Size(185, 25);
            this.naziv_partnera.TabIndex = 3;
            // 
            // R1_racun
            // 
            this.R1_racun.AutoSize = true;
            this.R1_racun.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.R1_racun.Location = new System.Drawing.Point(187, 12);
            this.R1_racun.Name = "R1_racun";
            this.R1_racun.Size = new System.Drawing.Size(92, 22);
            this.R1_racun.TabIndex = 4;
            this.R1_racun.Text = "R1 Račun";
            this.R1_racun.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.post_odabir);
            this.panel1.Controls.Add(this.karticar_odarbir);
            this.panel1.Controls.Add(this.kartica_iznos);
            this.panel1.Controls.Add(this.gotovina_iznos);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.oib_partnera);
            this.panel1.Controls.Add(this.adresa_partnera);
            this.panel1.Controls.Add(this.R1_racun);
            this.panel1.Controls.Add(this.naziv_partnera);
            this.panel1.Location = new System.Drawing.Point(28, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(390, 410);
            this.panel1.TabIndex = 5;
            // 
            // adresa_partnera
            // 
            this.adresa_partnera.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adresa_partnera.Location = new System.Drawing.Point(187, 107);
            this.adresa_partnera.Name = "adresa_partnera";
            this.adresa_partnera.Size = new System.Drawing.Size(185, 25);
            this.adresa_partnera.TabIndex = 5;
            // 
            // oib_partnera
            // 
            this.oib_partnera.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oib_partnera.Location = new System.Drawing.Point(187, 162);
            this.oib_partnera.Name = "oib_partnera";
            this.oib_partnera.Size = new System.Drawing.Size(185, 25);
            this.oib_partnera.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(184, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "Naziv";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(184, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Adresa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(184, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 18);
            this.label3.TabIndex = 9;
            this.label3.Text = "OIB";
            // 
            // gotovina_iznos
            // 
            this.gotovina_iznos.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gotovina_iznos.Location = new System.Drawing.Point(22, 229);
            this.gotovina_iznos.Name = "gotovina_iznos";
            this.gotovina_iznos.Size = new System.Drawing.Size(149, 25);
            this.gotovina_iznos.TabIndex = 10;
            // 
            // kartica_iznos
            // 
            this.kartica_iznos.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kartica_iznos.Location = new System.Drawing.Point(22, 284);
            this.kartica_iznos.Name = "kartica_iznos";
            this.kartica_iznos.Size = new System.Drawing.Size(149, 25);
            this.kartica_iznos.TabIndex = 11;
            // 
            // karticar_odarbir
            // 
            this.karticar_odarbir.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.karticar_odarbir.Location = new System.Drawing.Point(205, 284);
            this.karticar_odarbir.Name = "karticar_odarbir";
            this.karticar_odarbir.Size = new System.Drawing.Size(147, 25);
            this.karticar_odarbir.TabIndex = 12;
            // 
            // post_odabir
            // 
            this.post_odabir.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.post_odabir.Location = new System.Drawing.Point(205, 331);
            this.post_odabir.Name = "post_odabir";
            this.post_odabir.Size = new System.Drawing.Size(147, 25);
            this.post_odabir.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(202, 263);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 18);
            this.label4.TabIndex = 14;
            this.label4.Text = "Kartičar";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(202, 312);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 18);
            this.label5.TabIndex = 15;
            this.label5.Text = "POS";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(19, 208);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 18);
            this.label6.TabIndex = 16;
            this.label6.Text = "Gotovina IZNOS";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(19, 263);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 18);
            this.label7.TabIndex = 17;
            this.label7.Text = "Kartica IZNOS";
            // 
            // ZakljuciRacun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 527);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.otp_partneru_naziv);
            this.Controls.Add(this.otp_partneru);
            this.Controls.Add(this.zakljuci_racun_button);
            this.Name = "ZakljuciRacun";
            this.Text = "ZakljuciRacun";
            this.Load += new System.EventHandler(this.ZakljuciRacun_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button zakljuci_racun_button;
        private System.Windows.Forms.CheckBox otp_partneru;
        private System.Windows.Forms.TextBox otp_partneru_naziv;
        private System.Windows.Forms.TextBox naziv_partnera;
        private System.Windows.Forms.CheckBox R1_racun;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox oib_partnera;
        private System.Windows.Forms.TextBox adresa_partnera;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox post_odabir;
        private System.Windows.Forms.TextBox karticar_odarbir;
        private System.Windows.Forms.TextBox kartica_iznos;
        private System.Windows.Forms.TextBox gotovina_iznos;
    }
}