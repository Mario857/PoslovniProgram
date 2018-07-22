namespace Poslovni
{
    partial class Maloprodaja
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.maloprodajaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noviRacunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.izbrisiTekućiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pregledFinancijaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zbrojiUtržakToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pregledUtrškaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centifikatFiksalizacijeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(1133, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 73);
            this.label1.TabIndex = 0;
            this.label1.Text = "0,00kn";
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 29;
            this.listBox1.Location = new System.Drawing.Point(12, 105);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(94, 555);
            this.listBox1.TabIndex = 1;
            this.listBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox1_DrawItem);
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Modern No. 20", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.Location = new System.Drawing.Point(128, 105);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1211, 563);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellEndEdit);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.EditControlShowing_event);
            this.dataGridView1.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_Sinkroniziraj);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.maloprodajaToolStripMenuItem,
            this.pregledFinancijaToolStripMenuItem,
            this.centifikatFiksalizacijeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1365, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // maloprodajaToolStripMenuItem
            // 
            this.maloprodajaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noviRacunToolStripMenuItem,
            this.izbrisiTekućiToolStripMenuItem});
            this.maloprodajaToolStripMenuItem.Name = "maloprodajaToolStripMenuItem";
            this.maloprodajaToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.maloprodajaToolStripMenuItem.Text = "Maloprodaja";
            // 
            // noviRacunToolStripMenuItem
            // 
            this.noviRacunToolStripMenuItem.Name = "noviRacunToolStripMenuItem";
            this.noviRacunToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.noviRacunToolStripMenuItem.Text = "Novi racun";
            this.noviRacunToolStripMenuItem.Click += new System.EventHandler(this.NoviRacunToolStripMenuItem_Click);
            // 
            // izbrisiTekućiToolStripMenuItem
            // 
            this.izbrisiTekućiToolStripMenuItem.Name = "izbrisiTekućiToolStripMenuItem";
            this.izbrisiTekućiToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.izbrisiTekućiToolStripMenuItem.Text = "Izbrisi tekući";
            this.izbrisiTekućiToolStripMenuItem.Click += new System.EventHandler(this.izbrisiTekućiToolStripMenuItem_Click);
            // 
            // pregledFinancijaToolStripMenuItem
            // 
            this.pregledFinancijaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zbrojiUtržakToolStripMenuItem,
            this.pregledUtrškaToolStripMenuItem});
            this.pregledFinancijaToolStripMenuItem.Name = "pregledFinancijaToolStripMenuItem";
            this.pregledFinancijaToolStripMenuItem.Size = new System.Drawing.Size(107, 20);
            this.pregledFinancijaToolStripMenuItem.Text = "Pregled financija";
            // 
            // zbrojiUtržakToolStripMenuItem
            // 
            this.zbrojiUtržakToolStripMenuItem.Name = "zbrojiUtržakToolStripMenuItem";
            this.zbrojiUtržakToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.zbrojiUtržakToolStripMenuItem.Text = "Zbroji utržak";
            // 
            // pregledUtrškaToolStripMenuItem
            // 
            this.pregledUtrškaToolStripMenuItem.Name = "pregledUtrškaToolStripMenuItem";
            this.pregledUtrškaToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.pregledUtrškaToolStripMenuItem.Text = "Pregled utrška";
            // 
            // centifikatFiksalizacijeToolStripMenuItem
            // 
            this.centifikatFiksalizacijeToolStripMenuItem.Name = "centifikatFiksalizacijeToolStripMenuItem";
            this.centifikatFiksalizacijeToolStripMenuItem.Size = new System.Drawing.Size(132, 20);
            this.centifikatFiksalizacijeToolStripMenuItem.Text = "Centifikat fiksalizacije";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Showcard Gothic", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(12, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 61);
            this.button1.TabIndex = 4;
            this.button1.Text = "NOVI (F2)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Showcard Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(151, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 61);
            this.button2.TabIndex = 5;
            this.button2.Text = "ZAKLJUCI (F12)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(315, 56);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(219, 32);
            this.comboBox1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(315, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "OPERATER : ";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dateTimePicker1.Location = new System.Drawing.Point(579, 56);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(279, 31);
            this.dateTimePicker1.TabIndex = 8;
            this.dateTimePicker1.CloseUp += new System.EventHandler(this.dateTimePicker1_CloseUp);
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(575, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "DATUM : ";
            // 
            // Maloprodaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1365, 688);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Maloprodaja";
            this.Text = "Maloprodaja";
            this.Load += new System.EventHandler(this.Maloprodaja_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem maloprodajaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noviRacunToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem izbrisiTekućiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pregledFinancijaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zbrojiUtržakToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pregledUtrškaToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem centifikatFiksalizacijeToolStripMenuItem;
    }
}