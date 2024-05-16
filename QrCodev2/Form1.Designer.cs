
namespace QrCodev2
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_margePlaque = new System.Windows.Forms.TextBox();
            this.txt_hauteurPlaque = new System.Windows.Forms.TextBox();
            this.txt_largeurPlaque = new System.Windows.Forms.TextBox();
            this.txt_hauteurDocument = new System.Windows.Forms.TextBox();
            this.txt_largeurDocument = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_numDepart = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_Generer = new System.Windows.Forms.Button();
            this.grp_infos = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_Modifier = new System.Windows.Forms.Button();
            this.nup_NbrExemplaire = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.dgv_recap = new System.Windows.Forms.DataGridView();
            this.btn_produits = new System.Windows.Forms.Button();
            this.picture_box = new System.Windows.Forms.PictureBox();
            this.btn_api = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grp_infos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nup_NbrExemplaire)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_recap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_box)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txt_margePlaque);
            this.groupBox1.Controls.Add(this.txt_hauteurPlaque);
            this.groupBox1.Controls.Add(this.txt_largeurPlaque);
            this.groupBox1.Controls.Add(this.txt_hauteurDocument);
            this.groupBox1.Controls.Add(this.txt_largeurDocument);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 71);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(1144, 107);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dimensions des plaques";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(670, 82);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(93, 15);
            this.label10.TabIndex = 19;
            this.label10.Text = "(conseillé : 0,5) ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(335, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 15);
            this.label9.TabIndex = 16;
            this.label9.Text = "(conseillé : 8,4) ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(24, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 15);
            this.label8.TabIndex = 15;
            this.label8.Text = "(conseillé : 8,4) ";
            // 
            // txt_margePlaque
            // 
            this.txt_margePlaque.Location = new System.Drawing.Point(875, 63);
            this.txt_margePlaque.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_margePlaque.Name = "txt_margePlaque";
            this.txt_margePlaque.Size = new System.Drawing.Size(100, 22);
            this.txt_margePlaque.TabIndex = 11;
            this.txt_margePlaque.Text = "0,5";
            // 
            // txt_hauteurPlaque
            // 
            this.txt_hauteurPlaque.Location = new System.Drawing.Point(545, 63);
            this.txt_hauteurPlaque.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_hauteurPlaque.Name = "txt_hauteurPlaque";
            this.txt_hauteurPlaque.Size = new System.Drawing.Size(100, 22);
            this.txt_hauteurPlaque.TabIndex = 10;
            this.txt_hauteurPlaque.Text = "8,4";
            // 
            // txt_largeurPlaque
            // 
            this.txt_largeurPlaque.Location = new System.Drawing.Point(229, 63);
            this.txt_largeurPlaque.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_largeurPlaque.Name = "txt_largeurPlaque";
            this.txt_largeurPlaque.Size = new System.Drawing.Size(100, 22);
            this.txt_largeurPlaque.TabIndex = 9;
            this.txt_largeurPlaque.Text = "8,4";
            // 
            // txt_hauteurDocument
            // 
            this.txt_hauteurDocument.Location = new System.Drawing.Point(545, 28);
            this.txt_hauteurDocument.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_hauteurDocument.Name = "txt_hauteurDocument";
            this.txt_hauteurDocument.Size = new System.Drawing.Size(100, 22);
            this.txt_hauteurDocument.TabIndex = 8;
            this.txt_hauteurDocument.Text = "120";
            // 
            // txt_largeurDocument
            // 
            this.txt_largeurDocument.Location = new System.Drawing.Point(229, 28);
            this.txt_largeurDocument.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_largeurDocument.Name = "txt_largeurDocument";
            this.txt_largeurDocument.Size = new System.Drawing.Size(100, 22);
            this.txt_largeurDocument.TabIndex = 7;
            this.txt_largeurDocument.Text = "120";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(665, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(199, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "Marge entre plaques (en cm) :";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(335, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Hauteur des plaques (en cm) :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Largeur des plaques (en cm) :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(335, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Hauteur du document (en cm) :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Largeur du document (en cm) :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txt_numDepart
            // 
            this.txt_numDepart.Location = new System.Drawing.Point(173, 390);
            this.txt_numDepart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_numDepart.Name = "txt_numDepart";
            this.txt_numDepart.Size = new System.Drawing.Size(56, 22);
            this.txt_numDepart.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 394);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "Numéro de départ :";
            // 
            // btn_Generer
            // 
            this.btn_Generer.Location = new System.Drawing.Point(1003, 337);
            this.btn_Generer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Generer.Name = "btn_Generer";
            this.btn_Generer.Size = new System.Drawing.Size(135, 42);
            this.btn_Generer.TabIndex = 14;
            this.btn_Generer.Text = "Générer la planche sous...";
            this.btn_Generer.UseVisualStyleBackColor = true;
            this.btn_Generer.Click += new System.EventHandler(this.btn_Generer_Click);
            // 
            // grp_infos
            // 
            this.grp_infos.Controls.Add(this.button1);
            this.grp_infos.Controls.Add(this.label7);
            this.grp_infos.Controls.Add(this.btn_Modifier);
            this.grp_infos.Controls.Add(this.nup_NbrExemplaire);
            this.grp_infos.Controls.Add(this.label13);
            this.grp_infos.Controls.Add(this.txt_numDepart);
            this.grp_infos.Controls.Add(this.dgv_recap);
            this.grp_infos.Controls.Add(this.btn_Generer);
            this.grp_infos.Controls.Add(this.label5);
            this.grp_infos.Location = new System.Drawing.Point(12, 186);
            this.grp_infos.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grp_infos.Name = "grp_infos";
            this.grp_infos.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grp_infos.Size = new System.Drawing.Size(1144, 438);
            this.grp_infos.TabIndex = 1;
            this.grp_infos.TabStop = false;
            this.grp_infos.Text = "Récapitulatif";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(862, 337);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 42);
            this.button1.TabIndex = 27;
            this.button1.Text = "Générer les QR codes sous...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn_enregistrerQRcode_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(952, 417);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(186, 17);
            this.label7.TabIndex = 26;
            this.label7.Text = "QPDF v1.1 - mediapush.fr ©";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btn_Modifier
            // 
            this.btn_Modifier.Location = new System.Drawing.Point(248, 385);
            this.btn_Modifier.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Modifier.Name = "btn_Modifier";
            this.btn_Modifier.Size = new System.Drawing.Size(100, 33);
            this.btn_Modifier.TabIndex = 25;
            this.btn_Modifier.Text = "Modifier";
            this.btn_Modifier.UseVisualStyleBackColor = true;
            this.btn_Modifier.Click += new System.EventHandler(this.btn_Modifier_Click);
            // 
            // nup_NbrExemplaire
            // 
            this.nup_NbrExemplaire.Location = new System.Drawing.Point(173, 346);
            this.nup_NbrExemplaire.Margin = new System.Windows.Forms.Padding(4);
            this.nup_NbrExemplaire.Name = "nup_NbrExemplaire";
            this.nup_NbrExemplaire.Size = new System.Drawing.Size(57, 22);
            this.nup_NbrExemplaire.TabIndex = 24;
            this.nup_NbrExemplaire.ValueChanged += new System.EventHandler(this.nup_NbrExemplaire_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(20, 350);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(139, 15);
            this.label13.TabIndex = 21;
            this.label13.Text = "Nombre d\'exemplaires :";
            // 
            // dgv_recap
            // 
            this.dgv_recap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_recap.Location = new System.Drawing.Point(7, 21);
            this.dgv_recap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgv_recap.Name = "dgv_recap";
            this.dgv_recap.RowHeadersWidth = 51;
            this.dgv_recap.RowTemplate.Height = 24;
            this.dgv_recap.Size = new System.Drawing.Size(1131, 311);
            this.dgv_recap.TabIndex = 0;
            this.dgv_recap.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_recap_CellContentClick);
            this.dgv_recap.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_recap_CellValueChanged);
            this.dgv_recap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgv_recap_MouseClick);
            // 
            // btn_produits
            // 
            this.btn_produits.Location = new System.Drawing.Point(1050, 13);
            this.btn_produits.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_produits.Name = "btn_produits";
            this.btn_produits.Size = new System.Drawing.Size(100, 33);
            this.btn_produits.TabIndex = 26;
            this.btn_produits.Text = "Produits";
            this.btn_produits.UseVisualStyleBackColor = true;
            this.btn_produits.Click += new System.EventHandler(this.btn_produits_Click);
            // 
            // picture_box
            // 
            this.picture_box.Image = global::QrCodev2.Properties.Resources.c2644f94cfda9ba79a25b8e737bc5748;
            this.picture_box.Location = new System.Drawing.Point(12, 12);
            this.picture_box.Name = "picture_box";
            this.picture_box.Size = new System.Drawing.Size(294, 52);
            this.picture_box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture_box.TabIndex = 25;
            this.picture_box.TabStop = false;
            // 
            // btn_api
            // 
            this.btn_api.Location = new System.Drawing.Point(944, 13);
            this.btn_api.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_api.Name = "btn_api";
            this.btn_api.Size = new System.Drawing.Size(100, 33);
            this.btn_api.TabIndex = 27;
            this.btn_api.Text = "Clé API";
            this.btn_api.UseVisualStyleBackColor = true;
            this.btn_api.Click += new System.EventHandler(this.btn_api_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1165, 631);
            this.Controls.Add(this.btn_api);
            this.Controls.Add(this.btn_produits);
            this.Controls.Add(this.picture_box);
            this.Controls.Add(this.grp_infos);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Générer le PDF des plaques";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grp_infos.ResumeLayout(false);
            this.grp_infos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nup_NbrExemplaire)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_recap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_box)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_largeurDocument;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_Generer;
        private System.Windows.Forms.TextBox txt_numDepart;
        private System.Windows.Forms.TextBox txt_margePlaque;
        private System.Windows.Forms.TextBox txt_hauteurPlaque;
        private System.Windows.Forms.TextBox txt_largeurPlaque;
        private System.Windows.Forms.TextBox txt_hauteurDocument;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox grp_infos;
        internal System.Windows.Forms.DataGridView dgv_recap;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown nup_NbrExemplaire;
        private System.Windows.Forms.Button btn_Modifier;
        private System.Windows.Forms.PictureBox picture_box;
        private System.Windows.Forms.Button btn_produits;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_api;
        private System.Windows.Forms.Button button1;
    }
}

