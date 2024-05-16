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
    public partial class ajouter_produit : Form
    {
        private string selectedFilePath;
        private Produits produitsForm;

        public ajouter_produit(Produits form)
        {
            InitializeComponent();
            produitsForm = form;
            selectedFilePath = null;
        }

        private void btn_ajouter_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedFilePath))
            {
                MessageBox.Show("Veuillez sélectionner un fichier.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_nom.Text))
            {
                MessageBox.Show("Veuillez entrer un nom pour le fichier.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string directory = Path.Combine(appDataPath, "QPDF", "Repertoire");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string extension = Path.GetExtension(selectedFilePath);
            string newFileName = txt_nom.Text + extension;
            string newFilePath = Path.Combine(directory, newFileName);

            try
            {
                File.Copy(selectedFilePath, newFilePath, true);

                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(produitsForm.dgv_produits);
                newRow.Cells[0].Value = newFileName;
                newRow.Cells[1].Value = newFilePath;
                produitsForm.dgv_produits.Rows.Add(newRow);

                MessageBox.Show("Le fichier a été ajouté et renommé avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ajout du fichier : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_parcourir_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            openFileDialog.Title = "Select a File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFilePath = openFileDialog.FileName;
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(selectedFilePath);
                txt_nom.Text = fileNameWithoutExtension;
                label1.Text = selectedFilePath;
            }
        }

        private void ajouter_produit_Load(object sender, EventArgs e)
        {
        }
    }
}