
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
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_nbrExemplaire = new System.Windows.Forms.TextBox();
            this.txt_numDepart = new System.Windows.Forms.TextBox();
            this.txt_margePlaque = new System.Windows.Forms.TextBox();
            this.txt_hauteurPlaque = new System.Windows.Forms.TextBox();
            this.txt_largeurPlaque = new System.Windows.Forms.TextBox();
            this.txt_hauteurDocument = new System.Windows.Forms.TextBox();
            this.txt_largeurDocument = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_chemin = new System.Windows.Forms.Label();
            this.btn_Ajouter = new System.Windows.Forms.Button();
            this.btn_Generer = new System.Windows.Forms.Button();
            this.grp_infos = new System.Windows.Forms.GroupBox();
            this.dgv_recap = new System.Windows.Forms.DataGridView();
            this.btn_modifier = new System.Windows.Forms.Button();
            this.btn_supprimer = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grp_infos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_recap)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txt_nbrExemplaire);
            this.groupBox1.Controls.Add(this.txt_numDepart);
            this.groupBox1.Controls.Add(this.txt_margePlaque);
            this.groupBox1.Controls.Add(this.txt_hauteurPlaque);
            this.groupBox1.Controls.Add(this.txt_largeurPlaque);
            this.groupBox1.Controls.Add(this.txt_hauteurDocument);
            this.groupBox1.Controls.Add(this.txt_largeurDocument);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 49);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Size = new System.Drawing.Size(499, 205);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Générer le PDF des plaques";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 151);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Fond de la plaque :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(18, 101);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "(conseillé : 0,5) ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(251, 62);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "(conseillé : 8,4) ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(18, 65);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "(conseillé : 8,4) ";
            // 
            // txt_nbrExemplaire
            // 
            this.txt_nbrExemplaire.Location = new System.Drawing.Point(172, 120);
            this.txt_nbrExemplaire.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_nbrExemplaire.Name = "txt_nbrExemplaire";
            this.txt_nbrExemplaire.Size = new System.Drawing.Size(76, 20);
            this.txt_nbrExemplaire.TabIndex = 13;
            this.txt_nbrExemplaire.TextChanged += new System.EventHandler(this.txt_nbrExemplaire_TextChanged);
            // 
            // txt_numDepart
            // 
            this.txt_numDepart.Location = new System.Drawing.Point(409, 85);
            this.txt_numDepart.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_numDepart.Name = "txt_numDepart";
            this.txt_numDepart.Size = new System.Drawing.Size(76, 20);
            this.txt_numDepart.TabIndex = 12;
            // 
            // txt_margePlaque
            // 
            this.txt_margePlaque.Location = new System.Drawing.Point(172, 85);
            this.txt_margePlaque.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_margePlaque.Name = "txt_margePlaque";
            this.txt_margePlaque.Size = new System.Drawing.Size(76, 20);
            this.txt_margePlaque.TabIndex = 11;
            // 
            // txt_hauteurPlaque
            // 
            this.txt_hauteurPlaque.Location = new System.Drawing.Point(409, 46);
            this.txt_hauteurPlaque.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_hauteurPlaque.Name = "txt_hauteurPlaque";
            this.txt_hauteurPlaque.Size = new System.Drawing.Size(76, 20);
            this.txt_hauteurPlaque.TabIndex = 10;
            // 
            // txt_largeurPlaque
            // 
            this.txt_largeurPlaque.Location = new System.Drawing.Point(172, 46);
            this.txt_largeurPlaque.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_largeurPlaque.Name = "txt_largeurPlaque";
            this.txt_largeurPlaque.Size = new System.Drawing.Size(76, 20);
            this.txt_largeurPlaque.TabIndex = 9;
            // 
            // txt_hauteurDocument
            // 
            this.txt_hauteurDocument.Location = new System.Drawing.Point(409, 23);
            this.txt_hauteurDocument.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_hauteurDocument.Name = "txt_hauteurDocument";
            this.txt_hauteurDocument.Size = new System.Drawing.Size(76, 20);
            this.txt_hauteurDocument.TabIndex = 8;
            // 
            // txt_largeurDocument
            // 
            this.txt_largeurDocument.Location = new System.Drawing.Point(172, 23);
            this.txt_largeurDocument.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_largeurDocument.Name = "txt_largeurDocument";
            this.txt_largeurDocument.Size = new System.Drawing.Size(76, 20);
            this.txt_largeurDocument.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 123);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Nombre d\'exemplaires :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(251, 88);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Numéro de départ :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 88);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Marge entre plaques (en cm) :";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Hauteur des plaques (en cm) :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 49);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Largeur des plaques (en cm) :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Hauteur du document (en cm) :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Largeur du document (en cm) :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lbl_chemin
            // 
            this.lbl_chemin.AutoSize = true;
            this.lbl_chemin.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_chemin.Location = new System.Drawing.Point(11, 582);
            this.lbl_chemin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_chemin.Name = "lbl_chemin";
            this.lbl_chemin.Size = new System.Drawing.Size(56, 13);
            this.lbl_chemin.TabIndex = 20;
            this.lbl_chemin.Text = "lbl_chemin";
            // 
            // btn_Ajouter
            // 
            this.btn_Ajouter.Location = new System.Drawing.Point(11, 552);
            this.btn_Ajouter.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Ajouter.Name = "btn_Ajouter";
            this.btn_Ajouter.Size = new System.Drawing.Size(75, 27);
            this.btn_Ajouter.TabIndex = 17;
            this.btn_Ajouter.Text = "Ajouter";
            this.btn_Ajouter.UseVisualStyleBackColor = true;
            this.btn_Ajouter.Click += new System.EventHandler(this.btn_ajouter);
            // 
            // btn_Generer
            // 
            this.btn_Generer.Location = new System.Drawing.Point(400, 551);
            this.btn_Generer.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Generer.Name = "btn_Generer";
            this.btn_Generer.Size = new System.Drawing.Size(101, 34);
            this.btn_Generer.TabIndex = 14;
            this.btn_Generer.Text = "Générer et enregistrer sous...";
            this.btn_Generer.UseVisualStyleBackColor = true;
            this.btn_Generer.Click += new System.EventHandler(this.btn_Generer_Click);
            // 
            // grp_infos
            // 
            this.grp_infos.Controls.Add(this.dgv_recap);
            this.grp_infos.Location = new System.Drawing.Point(6, 270);
            this.grp_infos.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.grp_infos.Name = "grp_infos";
            this.grp_infos.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.grp_infos.Size = new System.Drawing.Size(499, 276);
            this.grp_infos.TabIndex = 1;
            this.grp_infos.TabStop = false;
            this.grp_infos.Text = "Récapitulatif";
            // 
            // dgv_recap
            // 
            this.dgv_recap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_recap.Location = new System.Drawing.Point(5, 17);
            this.dgv_recap.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dgv_recap.Name = "dgv_recap";
            this.dgv_recap.RowHeadersWidth = 51;
            this.dgv_recap.RowTemplate.Height = 24;
            this.dgv_recap.Size = new System.Drawing.Size(490, 253);
            this.dgv_recap.TabIndex = 0;
            // 
            // btn_modifier
            // 
            this.btn_modifier.Location = new System.Drawing.Point(90, 552);
            this.btn_modifier.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_modifier.Name = "btn_modifier";
            this.btn_modifier.Size = new System.Drawing.Size(75, 27);
            this.btn_modifier.TabIndex = 21;
            this.btn_modifier.Text = "Modifier";
            this.btn_modifier.UseVisualStyleBackColor = true;
            // 
            // btn_supprimer
            // 
            this.btn_supprimer.Location = new System.Drawing.Point(169, 552);
            this.btn_supprimer.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_supprimer.Name = "btn_supprimer";
            this.btn_supprimer.Size = new System.Drawing.Size(75, 27);
            this.btn_supprimer.TabIndex = 22;
            this.btn_supprimer.Text = "Supprimer";
            this.btn_supprimer.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 600);
            this.Controls.Add(this.btn_supprimer);
            this.Controls.Add(this.btn_modifier);
            this.Controls.Add(this.grp_infos);
            this.Controls.Add(this.lbl_chemin);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Generer);
            this.Controls.Add(this.btn_Ajouter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.Text = "Générer le PDF des plaques";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grp_infos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_recap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_largeurDocument;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_Generer;
        private System.Windows.Forms.TextBox txt_nbrExemplaire;
        private System.Windows.Forms.TextBox txt_numDepart;
        private System.Windows.Forms.TextBox txt_margePlaque;
        private System.Windows.Forms.TextBox txt_hauteurPlaque;
        private System.Windows.Forms.TextBox txt_largeurPlaque;
        private System.Windows.Forms.TextBox txt_hauteurDocument;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn_Ajouter;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl_chemin;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox grp_infos;
        private System.Windows.Forms.DataGridView dgv_recap;
        private System.Windows.Forms.Button btn_modifier;
        private System.Windows.Forms.Button btn_supprimer;
    }
}

