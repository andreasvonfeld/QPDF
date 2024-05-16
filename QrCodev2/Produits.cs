using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.IO;
using PdfSharpCore.Pdf.IO;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Svg;

namespace QrCodev2
{
    public partial class Produits : Form
    {
        private Form1 accueil;
        private modifier_produit FrmModifier;
        private ajouter_produit FrmAjouter;

        public Produits(Form1 mainForm)
        {
            InitializeComponent();
            accueil = mainForm;
            FrmModifier = new modifier_produit(this);
            FrmAjouter = new ajouter_produit(this);
            this.FormClosing += Produits_FormClosing;
        }

        public void Produits_Load(object sender, EventArgs e)
        {
            if (dgv_produits.Columns.Count == 0)
            {
                dgv_produits.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nom du fond", Name = "colonneFond" });
                dgv_produits.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Chemin du fond", Name = "colonneChemin" });
                dgv_produits.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nombre", Name = "colonneNombre" });
                dgv_produits.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Numéro de départ", Name = "colonneNumDep" });
                dgv_produits.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Numéro de fin", Name = "colonneNumFin" });

                DataGridViewComboBoxColumn colorColumn = new DataGridViewComboBoxColumn();
                colorColumn.HeaderText = "Couleur du texte";
                colorColumn.Name = "colonneCouleur";
                colorColumn.Items.AddRange("blanc", "noir");
                dgv_produits.Columns.Add(colorColumn);
            }

            dgv_produits.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_produits.AllowUserToOrderColumns = false;

            CopyDataGridView(accueil.dgv_recap, dgv_produits);

            dgv_produits.Columns["colonneChemin"].Visible = false;
            dgv_produits.Columns["colonneNombre"].Visible = false;
            dgv_produits.Columns["colonneNumDep"].Visible = false;
            dgv_produits.Columns["colonneNumFin"].Visible = false;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            CopyDataGridView(dgv_produits, accueil.dgv_recap);
            this.Hide();
        }

        private void Produits_FormClosing(object sender, FormClosingEventArgs e)
        {
            CopyDataGridView(dgv_produits, accueil.dgv_recap);
        }

        private void btn_modifier_Click(object sender, EventArgs e)
        {
            if (dgv_produits.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgv_produits.SelectedRows[0];
                string nomFichier = selectedRow.Cells["colonneFond"].Value.ToString();

                FrmModifier = new modifier_produit(this);
                FrmModifier.SetNomFichier(nomFichier);
                FrmModifier.ShowDialog();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une ligne dans le DataGridView.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CopyDataGridView(DataGridView source, DataGridView destination)
        {
            destination.Rows.Clear();

            foreach (DataGridViewRow row in source.Rows)
            {
                if (!row.IsNewRow)
                {
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(destination);

                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (i < newRow.Cells.Count)
                        {
                            newRow.Cells[i].Value = row.Cells[i].Value;
                        }
                    }
                    destination.Rows.Add(newRow);
                }
            }

            destination.Refresh();
        }

            private void btn_supprimer_Click(object sender, EventArgs e)
        {
            if (dgv_produits.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show(
                    "Êtes-vous sûr de vouloir supprimer le fichier sélectionné ?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        DataGridViewRow selectedRow = dgv_produits.SelectedRows[0];
                        string filePath = selectedRow.Cells["colonneChemin"].Value.ToString();

                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }

                        dgv_produits.Rows.Remove(selectedRow);

                        MessageBox.Show("Le fichier a été supprimé avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de la suppression du fichier : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une ligne dans le DataGridView.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ajouter_Click(object sender, EventArgs e)
        {
            FrmAjouter.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}