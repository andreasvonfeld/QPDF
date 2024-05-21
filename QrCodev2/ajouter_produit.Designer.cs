
namespace QrCodev2
{
    partial class ajouter_produit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ajouter_produit));
            this.grp_modifier = new System.Windows.Forms.GroupBox();
            this.btn_parcourir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_fichier = new System.Windows.Forms.Label();
            this.btn_ajouter = new System.Windows.Forms.Button();
            this.txt_nom = new System.Windows.Forms.TextBox();
            this.lbl_nom = new System.Windows.Forms.Label();
            this.grp_modifier.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_modifier
            // 
            this.grp_modifier.Controls.Add(this.btn_parcourir);
            this.grp_modifier.Controls.Add(this.label1);
            this.grp_modifier.Controls.Add(this.lbl_fichier);
            this.grp_modifier.Controls.Add(this.btn_ajouter);
            this.grp_modifier.Controls.Add(this.txt_nom);
            this.grp_modifier.Controls.Add(this.lbl_nom);
            this.grp_modifier.Location = new System.Drawing.Point(12, 12);
            this.grp_modifier.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grp_modifier.Name = "grp_modifier";
            this.grp_modifier.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grp_modifier.Size = new System.Drawing.Size(396, 160);
            this.grp_modifier.TabIndex = 1;
            this.grp_modifier.TabStop = false;
            this.grp_modifier.Text = "Ajouter le produit";
            // 
            // btn_parcourir
            // 
            this.btn_parcourir.Location = new System.Drawing.Point(92, 60);
            this.btn_parcourir.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_parcourir.Name = "btn_parcourir";
            this.btn_parcourir.Size = new System.Drawing.Size(100, 33);
            this.btn_parcourir.TabIndex = 29;
            this.btn_parcourir.Text = "Parcourir";
            this.btn_parcourir.UseVisualStyleBackColor = true;
            this.btn_parcourir.Click += new System.EventHandler(this.btn_parcourir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 28;
            // 
            // lbl_fichier
            // 
            this.lbl_fichier.AutoSize = true;
            this.lbl_fichier.Location = new System.Drawing.Point(19, 71);
            this.lbl_fichier.Name = "lbl_fichier";
            this.lbl_fichier.Size = new System.Drawing.Size(58, 17);
            this.lbl_fichier.TabIndex = 27;
            this.lbl_fichier.Text = "Fichier :";
            // 
            // btn_ajouter
            // 
            this.btn_ajouter.Location = new System.Drawing.Point(281, 116);
            this.btn_ajouter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_ajouter.Name = "btn_ajouter";
            this.btn_ajouter.Size = new System.Drawing.Size(100, 33);
            this.btn_ajouter.TabIndex = 26;
            this.btn_ajouter.Text = "Ajouter";
            this.btn_ajouter.UseVisualStyleBackColor = true;
            this.btn_ajouter.Click += new System.EventHandler(this.btn_ajouter_Click);
            // 
            // txt_nom
            // 
            this.txt_nom.Location = new System.Drawing.Point(92, 32);
            this.txt_nom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_nom.Name = "txt_nom";
            this.txt_nom.Size = new System.Drawing.Size(100, 22);
            this.txt_nom.TabIndex = 1;
            // 
            // lbl_nom
            // 
            this.lbl_nom.AutoSize = true;
            this.lbl_nom.Location = new System.Drawing.Point(19, 34);
            this.lbl_nom.Name = "lbl_nom";
            this.lbl_nom.Size = new System.Drawing.Size(45, 17);
            this.lbl_nom.TabIndex = 0;
            this.lbl_nom.Text = "Nom :";
            // 
            // ajouter_produit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 178);
            this.Controls.Add(this.grp_modifier);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "ajouter_produit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "QPDF - Ajouter un produit";
            this.Load += new System.EventHandler(this.ajouter_produit_Load);
            this.grp_modifier.ResumeLayout(false);
            this.grp_modifier.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_modifier;
        private System.Windows.Forms.Button btn_parcourir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_fichier;
        private System.Windows.Forms.Button btn_ajouter;
        private System.Windows.Forms.TextBox txt_nom;
        private System.Windows.Forms.Label lbl_nom;
    }
}