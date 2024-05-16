using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace QrCodev2
{
    public partial class modifier_produit : Form
    {
        private Produits produits;
        private string selectedFilePath;

        public modifier_produit(Produits produitsForm)
        {
            InitializeComponent();
            produits = produitsForm;
            selectedFilePath = null;
        }

        private void btn_modifier_Click(object sender, EventArgs e)
        {
            if (produits.dgv_produits.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = produits.dgv_produits.SelectedRows[0];
                string oldFilePath = selectedRow.Cells["colonneChemin"].Value.ToString();
                string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Repertoire");
                string extension = Path.GetExtension(oldFilePath);
                string newFileName = txt_nom.Text + extension;
                string newFilePath = Path.Combine(directory, newFileName);

                // Demande de confirmation avant de renommer ou remplacer le fichier
                DialogResult result = MessageBox.Show(
                    "Êtes-vous sûr de vouloir remplacer le fichier existant par le nouveau fichier sélectionné ?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (selectedFilePath != null)
                        {
                            // Renommer le fichier parcouru avec le nouveau nom
                            string renamedFilePath = Path.Combine(Path.GetDirectoryName(selectedFilePath), newFileName);

                            if (selectedFilePath != renamedFilePath)
                            {
                                File.Copy(selectedFilePath, renamedFilePath);
                            }
                            else
                            {
                                renamedFilePath = selectedFilePath;
                            }

                            // Déplacer le fichier renommé dans le répertoire Repertoire
                            if (!Directory.Exists(directory))
                            {
                                Directory.CreateDirectory(directory);
                            }
                            File.Copy(renamedFilePath, newFilePath, true);

                            // Supprimer l'ancien fichier dans le répertoire
                            if (File.Exists(oldFilePath))
                            {
                                File.Delete(oldFilePath);
                            }

                            // Mettre à jour le DataGridView avec le nouveau chemin de fichier
                            selectedRow.Cells["colonneChemin"].Value = newFilePath;
                            selectedRow.Cells["colonneFond"].Value = Path.GetFileName(newFilePath);

                            if (selectedFilePath != renamedFilePath)
                            {
                                // Supprimer le fichier temporaire renommé
                                File.Delete(renamedFilePath);
                            }
                        }
                        else
                        {
                            // Si seul le nom du fichier a été changé
                            if (File.Exists(newFilePath))
                            {
                                File.Delete(newFilePath); // Supprime le fichier existant
                            }

                            File.Move(oldFilePath, newFilePath);
                            selectedRow.Cells["colonneChemin"].Value = newFilePath;
                            selectedRow.Cells["colonneFond"].Value = newFileName;
                        }

                        MessageBox.Show("Le fichier a été renommé avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors du renommage du fichier : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une ligne dans le DataGridView.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
        public void SetNomFichier(string nomFichier)
        {
            txt_nom.Text = nomFichier;
        }
        private void modifier_produit_Load(object sender, EventArgs e)
        {
            
        }

        private void btn_parcourir_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files (*.*)|*.*";
            openFileDialog.Title = "Select a File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFilePath = openFileDialog.FileName;
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(selectedFilePath);
                txt_nom.Text = fileNameWithoutExtension; // Met à jour le TextBox avec le nom du fichier sans l'extension
                label1.Text = selectedFilePath; // Met à jour le label avec le chemin du fichier sélectionné
            }

        }
    }
  
}
