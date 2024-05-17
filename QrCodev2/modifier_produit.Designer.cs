
namespace QrCodev2
{
    partial class modifier_produit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(modifier_produit));
            this.grp_modifier = new System.Windows.Forms.GroupBox();
            this.btn_parcourir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_fichier = new System.Windows.Forms.Label();
            this.btn_modifier = new System.Windows.Forms.Button();
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
            this.grp_modifier.Controls.Add(this.btn_modifier);
            this.grp_modifier.Controls.Add(this.txt_nom);
            this.grp_modifier.Controls.Add(this.lbl_nom);
            this.grp_modifier.Location = new System.Drawing.Point(9, 10);
            this.grp_modifier.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grp_modifier.Name = "grp_modifier";
            this.grp_modifier.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grp_modifier.Size = new System.Drawing.Size(297, 130);
            this.grp_modifier.TabIndex = 0;
            this.grp_modifier.TabStop = false;
            this.grp_modifier.Text = "Modifier le produit";
            // 
            // btn_parcourir
            // 
            this.btn_parcourir.Location = new System.Drawing.Point(69, 49);
            this.btn_parcourir.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_parcourir.Name = "btn_parcourir";
            this.btn_parcourir.Size = new System.Drawing.Size(75, 27);
            this.btn_parcourir.TabIndex = 29;
            this.btn_parcourir.Text = "Parcourir";
            this.btn_parcourir.UseVisualStyleBackColor = true;
            this.btn_parcourir.Click += new System.EventHandler(this.btn_parcourir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 79);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 28;
            // 
            // lbl_fichier
            // 
            this.lbl_fichier.AutoSize = true;
            this.lbl_fichier.Location = new System.Drawing.Point(14, 58);
            this.lbl_fichier.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_fichier.Name = "lbl_fichier";
            this.lbl_fichier.Size = new System.Drawing.Size(44, 13);
            this.lbl_fichier.TabIndex = 27;
            this.lbl_fichier.Text = "Fichier :";
            // 
            // btn_modifier
            // 
            this.btn_modifier.Location = new System.Drawing.Point(211, 94);
            this.btn_modifier.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_modifier.Name = "btn_modifier";
            this.btn_modifier.Size = new System.Drawing.Size(75, 27);
            this.btn_modifier.TabIndex = 26;
            this.btn_modifier.Text = "Modifier";
            this.btn_modifier.UseVisualStyleBackColor = true;
            this.btn_modifier.Click += new System.EventHandler(this.btn_modifier_Click);
            // 
            // txt_nom
            // 
            this.txt_nom.Location = new System.Drawing.Point(69, 26);
            this.txt_nom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_nom.Name = "txt_nom";
            this.txt_nom.Size = new System.Drawing.Size(76, 20);
            this.txt_nom.TabIndex = 1;
            // 
            // lbl_nom
            // 
            this.lbl_nom.AutoSize = true;
            this.lbl_nom.Location = new System.Drawing.Point(14, 28);
            this.lbl_nom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_nom.Name = "lbl_nom";
            this.lbl_nom.Size = new System.Drawing.Size(35, 13);
            this.lbl_nom.TabIndex = 0;
            this.lbl_nom.Text = "Nom :";
            // 
            // modifier_produit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 145);
            this.Controls.Add(this.grp_modifier);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "modifier_produit";
            this.Text = "QPDF - Modifier le produit";
            this.Load += new System.EventHandler(this.modifier_produit_Load);
            this.grp_modifier.ResumeLayout(false);
            this.grp_modifier.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_modifier;
        private System.Windows.Forms.TextBox txt_nom;
        private System.Windows.Forms.Label lbl_nom;
        private System.Windows.Forms.Button btn_parcourir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_fichier;
        private System.Windows.Forms.Button btn_modifier;
    }
}