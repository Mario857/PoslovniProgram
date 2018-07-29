namespace Poslovni
{
    partial class Korisnici
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
            this.update = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.adduser = new System.Windows.Forms.Button();
            this.remuser = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.korisnici_grid = new System.Windows.Forms.DataGridView();
            this.isAdmin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OIB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PREZIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.korisnici_grid)).BeginInit();
            this.SuspendLayout();
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(26, 216);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(170, 49);
            this.update.TabIndex = 1;
            this.update.Text = "UPDATE";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(96, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(96, 40);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(96, 66);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 4;
            this.textBox3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox3_KeyDown);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(96, 92);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 5;
            this.textBox4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox4_KeyDown);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(96, 118);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 6;
            this.textBox5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox5_KeyDown);
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(96, 144);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 20);
            this.textBox6.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "IME :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "PREZIME :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "OIB :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "username :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "password";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "ID :";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(96, 180);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox1.Size = new System.Drawing.Size(65, 17);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "is Admin";
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // adduser
            // 
            this.adduser.Location = new System.Drawing.Point(26, 292);
            this.adduser.Name = "adduser";
            this.adduser.Size = new System.Drawing.Size(170, 30);
            this.adduser.TabIndex = 16;
            this.adduser.Text = "ADD";
            this.adduser.UseVisualStyleBackColor = true;
            this.adduser.Click += new System.EventHandler(this.adduser_Click);
            // 
            // remuser
            // 
            this.remuser.Location = new System.Drawing.Point(26, 328);
            this.remuser.Name = "remuser";
            this.remuser.Size = new System.Drawing.Size(170, 30);
            this.remuser.TabIndex = 17;
            this.remuser.Text = "REMOVE";
            this.remuser.UseVisualStyleBackColor = true;
            this.remuser.Click += new System.EventHandler(this.remuser_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.remuser);
            this.panel2.Controls.Add(this.update);
            this.panel2.Controls.Add(this.adduser);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.checkBox1);
            this.panel2.Controls.Add(this.textBox3);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.textBox4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.textBox5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.textBox6);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(12, 15);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(232, 478);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.korisnici_grid);
            this.panel1.Location = new System.Drawing.Point(259, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(755, 449);
            this.panel1.TabIndex = 18;
            // 
            // korisnici_grid
            // 
            this.korisnici_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.korisnici_grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.IME,
            this.PREZIME,
            this.OIB,
            this.username,
            this.password,
            this.isAdmin});
            this.korisnici_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.korisnici_grid.Location = new System.Drawing.Point(0, 0);
            this.korisnici_grid.Name = "korisnici_grid";
            this.korisnici_grid.ReadOnly = true;
            this.korisnici_grid.Size = new System.Drawing.Size(755, 449);
            this.korisnici_grid.TabIndex = 0;
            this.korisnici_grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.korisnici_grid_CellContentClick);
            this.korisnici_grid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.korisnici_grid_CellEndEdit);
            this.korisnici_grid.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.korisnici_grid_CellLeave);
            this.korisnici_grid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.korisnici_grid_CellMouseClick);
            this.korisnici_grid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.korisnici_grid_CellValueChanged);
            this.korisnici_grid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.korisnici_grid_RowEnter);
            this.korisnici_grid.SelectionChanged += new System.EventHandler(this.korisnici_grid_SelectionChanged);
            this.korisnici_grid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.korisnici_grid_KeyDown);
            // 
            // isAdmin
            // 
            this.isAdmin.HeaderText = "isAdmin";
            this.isAdmin.Name = "isAdmin";
            this.isAdmin.ReadOnly = true;
            this.isAdmin.Width = 103;
            // 
            // password
            // 
            this.password.HeaderText = "password";
            this.password.Name = "password";
            this.password.ReadOnly = true;
            this.password.Width = 96;
            // 
            // username
            // 
            this.username.HeaderText = "username";
            this.username.Name = "username";
            this.username.ReadOnly = true;
            this.username.Width = 101;
            // 
            // OIB
            // 
            this.OIB.HeaderText = "OIB";
            this.OIB.Name = "OIB";
            this.OIB.ReadOnly = true;
            this.OIB.Width = 102;
            // 
            // PREZIME
            // 
            this.PREZIME.HeaderText = "PREZIME";
            this.PREZIME.Name = "PREZIME";
            this.PREZIME.ReadOnly = true;
            this.PREZIME.Width = 101;
            // 
            // IME
            // 
            this.IME.HeaderText = "IME";
            this.IME.Name = "IME";
            this.IME.ReadOnly = true;
            this.IME.Width = 102;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 101;
            // 
            // Korisnici
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 482);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.MinimumSize = new System.Drawing.Size(1015, 497);
            this.Name = "Korisnici";
            this.Text = "Korisnici";
            this.Load += new System.EventHandler(this.Korisnici_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.korisnici_grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button adduser;
        private System.Windows.Forms.Button remuser;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView korisnici_grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn IME;
        private System.Windows.Forms.DataGridViewTextBoxColumn PREZIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn OIB;
        private System.Windows.Forms.DataGridViewTextBoxColumn username;
        private System.Windows.Forms.DataGridViewTextBoxColumn password;
        private System.Windows.Forms.DataGridViewTextBoxColumn isAdmin;
    }
}