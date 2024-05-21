
namespace QrCodev2
{
    partial class Produits
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Produits));
            this.grp_produits = new System.Windows.Forms.GroupBox();
            this.dgv_produits = new System.Windows.Forms.DataGridView();
            this.btn_supprimer = new System.Windows.Forms.Button();
            this.btn_ajouter = new System.Windows.Forms.Button();
            this.btn_modifier = new System.Windows.Forms.Button();
            this.btn_fermer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.grp_produits.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_produits)).BeginInit();
            this.SuspendLayout();
            // 
            // grp_produits
            // 
            this.grp_produits.Controls.Add(this.dgv_produits);
            this.grp_produits.Location = new System.Drawing.Point(12, 12);
            this.grp_produits.Name = "grp_produits";
            this.grp_produits.Size = new System.Drawing.Size(776, 426);
            this.grp_produits.TabIndex = 0;
            this.grp_produits.TabStop = false;
            this.grp_produits.Text = "Les produits";
            // 
            // dgv_produits
            // 
            this.dgv_produits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_produits.Location = new System.Drawing.Point(6, 21);
            this.dgv_produits.Name = "dgv_produits";
            this.dgv_produits.RowHeadersWidth = 51;
            this.dgv_produits.RowTemplate.Height = 24;
            this.dgv_produits.Size = new System.Drawing.Size(764, 399);
            this.dgv_produits.TabIndex = 0;
            // 
            // btn_supprimer
            // 
            this.btn_supprimer.Location = new System.Drawing.Point(470, 445);
            this.btn_supprimer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_supprimer.Name = "btn_supprimer";
            this.btn_supprimer.Size = new System.Drawing.Size(100, 33);
            this.btn_supprimer.TabIndex = 23;
            this.btn_supprimer.Text = "Supprimer";
            this.btn_supprimer.UseVisualStyleBackColor = true;
            this.btn_supprimer.Click += new System.EventHandler(this.btn_supprimer_Click);
            // 
            // btn_ajouter
            // 
            this.btn_ajouter.Location = new System.Drawing.Point(682, 445);
            this.btn_ajouter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_ajouter.Name = "btn_ajouter";
            this.btn_ajouter.Size = new System.Drawing.Size(100, 33);
            this.btn_ajouter.TabIndex = 24;
            this.btn_ajouter.Text = "Ajouter";
            this.btn_ajouter.UseVisualStyleBackColor = true;
            this.btn_ajouter.Click += new System.EventHandler(this.btn_ajouter_Click);
            // 
            // btn_modifier
            // 
            this.btn_modifier.Location = new System.Drawing.Point(576, 445);
            this.btn_modifier.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_modifier.Name = "btn_modifier";
            this.btn_modifier.Size = new System.Drawing.Size(100, 33);
            this.btn_modifier.TabIndex = 25;
            this.btn_modifier.Text = "Modifier";
            this.btn_modifier.UseVisualStyleBackColor = true;
            this.btn_modifier.Click += new System.EventHandler(this.btn_modifier_Click);
            // 
            // btn_fermer
            // 
            this.btn_fermer.Location = new System.Drawing.Point(682, 486);
            this.btn_fermer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_fermer.Name = "btn_fermer";
            this.btn_fermer.Size = new System.Drawing.Size(100, 33);
            this.btn_fermer.TabIndex = 26;
            this.btn_fermer.Text = "Fermer";
            this.btn_fermer.UseVisualStyleBackColor = true;
            this.btn_fermer.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 494);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(348, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Les fichiers doivent être au format PDF et de bonnes dimensions (en cm)";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Produits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 526);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_fermer);
            this.Controls.Add(this.btn_modifier);
            this.Controls.Add(this.btn_ajouter);
            this.Controls.Add(this.btn_supprimer);
            this.Controls.Add(this.grp_produits);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Produits";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Les produits";
            this.Load += new System.EventHandler(this.Produits_Load);
            this.grp_produits.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_produits)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_produits;
        public System.Windows.Forms.DataGridView dgv_produits;
        private System.Windows.Forms.Button btn_supprimer;
        private System.Windows.Forms.Button btn_ajouter;
        private System.Windows.Forms.Button btn_modifier;
        private System.Windows.Forms.Button btn_fermer;
        private System.Windows.Forms.Label label1;
    }
}