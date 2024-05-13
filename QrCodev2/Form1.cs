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
using System.Threading.Tasks;

namespace QrCodev2
{
    public partial class Form1 : Form
    {
        private int ancienneValeurNumeroFinLigne1 = 0;
        string imagePath;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn_Generer_Click(object sender, EventArgs e)
        {
            float imageWidthCm = float.Parse(txt_largeurPlaque.Text);
            float imageHeightCm = float.Parse(txt_hauteurPlaque.Text);
            float marginCm = float.Parse(txt_margePlaque.Text);
            int totalWidthCm = int.Parse(txt_largeurDocument.Text);
            int totalHeightCm = int.Parse(txt_hauteurDocument.Text);

            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            page.Width = XUnit.FromCentimeter(totalWidthCm);
            page.Height = XUnit.FromCentimeter(totalHeightCm);
            XGraphics gfx = XGraphics.FromPdfPage(page);

            int plaquesPerLine = (int)((totalWidthCm - marginCm) / (imageWidthCm + marginCm));

            int currentRow = 0;
            int currentCol = 0;
            int plaqueNumber = int.Parse(txt_numDepart.Text);

            foreach (DataGridViewRow dgvRow in dgv_recap.Rows)
            {
                string imagePath = dgvRow.Cells["colonneChemin"].Value?.ToString();

                if (!string.IsNullOrEmpty(imagePath))
                {
                    int nombreExemplaires = Convert.ToInt32(dgvRow.Cells["colonneNombre"].Value);
                    int numeroDepart = Convert.ToInt32(dgvRow.Cells["colonneNumDep"].Value);

                    for (int i = 0; i < nombreExemplaires; i++)
                    {
                        double x = currentCol * (imageWidthCm * 28.3465 + marginCm * 28.3465) + marginCm * 28.3465;
                        double y = currentRow * (imageHeightCm * 28.3465 + marginCm * 28.3465) + marginCm * 28.3465;

                        XImage image = XImage.FromFile(imagePath);
                        gfx.DrawImage(image, x, y, imageWidthCm * 28.3465, imageHeightCm * 28.3465);


                        // Chargement et dessin du QR Code à superposer
                        XImage overlayImage = XImage.FromFile(@"C:\Users\andre\OneDrive\Desktop\thumbnail_0346.png");

                        // Paramètres pour la superposition
                        double overlayWidth = 74; // en pixels
                        double overlayHeight = 74; // en pixels

                        // Dessiner le QR code
                        double overlayX = x + (imageWidthCm * 28.3465) - overlayWidth - 40; // Marge de droite ajustée
                        double overlayY = y + (imageHeightCm * 28.3465) - overlayHeight - 67; // Marge de bas ajustée
                        gfx.DrawImage(overlayImage, overlayX, overlayY, overlayWidth, overlayHeight);

                        // Récupérer la taille du texte
                        XSize textSize = gfx.MeasureString(plaqueNumber.ToString(), new XFont("Arial", 10));

                        // Calculer la position x pour centrer le texte horizontalement
                        double plaqueNumberX = x + (imageWidthCm * 28.3465) / 2 - textSize.Width / 2 + 48.5;

                        // Calculer la position y pour positionner le texte juste au-dessus du QR code
                        double plaqueNumberY = overlayY + 90; // ajuster la position verticalement si nécessaire

                        // Dessiner le numéro de plaque
                        gfx.DrawString(plaqueNumber.ToString(), new XFont("Poppins - SemiBold", 6), XBrushes.Black, plaqueNumberX, plaqueNumberY);

                        plaqueNumber++;

                        currentCol++;

                        if (currentCol >= plaquesPerLine)
                        {
                            currentCol = 0;
                            currentRow++;
                        }
                    }
                }
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Fichiers PDF (*.pdf)|*.pdf|Tous les fichiers (*.*)|*.*";
            saveFileDialog.Title = "Enregistrer le fichier PDF";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                document.Save(saveFileDialog.FileName);
            }
        }




        private void GeneratePDF(string imagePath, int nombreExemplaires, int numeroDepart) { }


        private void Form1_Load(object sender, EventArgs e)
        {
            lbl_chemin.Visible = false;

            dgv_emplacement.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_recap.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgv_recap.AllowUserToOrderColumns = false;
            nup_NbrExemplaire.Enabled = false;

            nup_NbrExemplaire.Value = 1;



            //colonnes du dgv recap

            DataGridViewTextBoxColumn colonne0 = new DataGridViewTextBoxColumn();
            colonne0.HeaderText = "Nom du fond";
            colonne0.Name = "colonneFond";

            dgv_recap.Columns.Add(colonne0);

            DataGridViewTextBoxColumn colonne1 = new DataGridViewTextBoxColumn();
            colonne1.HeaderText = "Chemin du fond";
            colonne1.Name = "colonneChemin";

            dgv_recap.Columns.Add(colonne1);

            colonne1.Visible = true;

            DataGridViewTextBoxColumn colonne2 = new DataGridViewTextBoxColumn();
            colonne2.HeaderText = "Nombre";
            colonne2.Name = "colonneNombre";

            dgv_recap.Columns.Add(colonne2);

            DataGridViewTextBoxColumn colonne3 = new DataGridViewTextBoxColumn();
            colonne3.HeaderText = "Numéro de départ";
            colonne3.Name = "colonneNumDep";

            dgv_recap.Columns.Add(colonne3);

            DataGridViewTextBoxColumn colonne4 = new DataGridViewTextBoxColumn();
            colonne4.HeaderText = "Numéro de fin";
            colonne4.Name = "colonneNumFin";

            dgv_recap.Columns.Add(colonne4);

            // colonnes du dgv emplacement

            DataGridViewTextBoxColumn colonne00 = new DataGridViewTextBoxColumn();
            colonne00.HeaderText = "Nom du fond";
            colonne00.Name = "colonneFond";

            dgv_emplacement.Columns.Add(colonne00);

            DataGridViewTextBoxColumn colonne01 = new DataGridViewTextBoxColumn();
            colonne01.HeaderText = "Chemin du fichier";
            colonne01.Name = "colonneChemin";

            dgv_emplacement.Columns.Add(colonne01);


        }

        private void txt_nbrExemplaire_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btn_ajouter(object sender, EventArgs e)
        {
            // Assurez-vous d'avoir ajouté la colonne du nom du fichier et du nombre d'exemplaires précédemment
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false; // Assurez-vous que l'utilisateur ne peut sélectionner qu'un seul fichier
            openFileDialog.Filter = "PDF Files|*.pdf"; // Filtre pour les fichiers PDF uniquement

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Récupérer le chemin du fichier sélectionné
                string cheminFichier = openFileDialog.FileName;

                // Récupérer le nom du fichier à partir du chemin complet
                string nomFichier = Path.GetFileName(cheminFichier);

                // Création d'une nouvelle ligne
                DataGridViewRow row = new DataGridViewRow();

                // Création de la cellule pour le nom du fichier
                DataGridViewTextBoxCell celluleNomFichier = new DataGridViewTextBoxCell();
                celluleNomFichier.Value = nomFichier;
                row.Cells.Add(celluleNomFichier);

                // Création de la cellule pour le chemin complet du fichier
                DataGridViewTextBoxCell celluleCheminFichier = new DataGridViewTextBoxCell();
                celluleCheminFichier.Value = cheminFichier;
                row.Cells.Add(celluleCheminFichier);

                // Création de la cellule pour le nombre d'exemplaires
                DataGridViewTextBoxCell celluleNombreExemplaires = new DataGridViewTextBoxCell();
                celluleNombreExemplaires.Value = nup_NbrExemplaire.Text; // Vous pouvez modifier le nombre d'exemplaires selon vos besoins
                row.Cells.Add(celluleNombreExemplaires);

                if (row.Cells.Count > 0)
                {
                    dgv_recap.Rows.Add(row);
                }
                else
                {
                    MessageBox.Show("La ligne ne contient pas de cellules.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_Parcourir_Click(object sender, EventArgs e)
        {
            // Effacer le contenu actuel de dgv_emplacement
            dgv_emplacement.Rows.Clear();

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Sélectionnez un dossier contenant les fichiers à ajouter";

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string dossierSelectionne = folderBrowserDialog.SelectedPath;

                // Définir les extensions de fichiers autorisées
                string[] extensionsAutorisees = new string[] { ".pdf", ".png", ".jpg", ".jpeg", ".svg" };

                // Récupérer les fichiers dans le dossier sélectionné avec les extensions autorisées
                string[] fichiersDansDossier = Directory.GetFiles(dossierSelectionne)
                    .Where(fichier => extensionsAutorisees.Contains(Path.GetExtension(fichier).ToLower()))
                    .ToArray();

                foreach (string fichier in fichiersDansDossier)
                {
                    string nomFichier = Path.GetFileName(fichier);

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dgv_emplacement);
                    row.Cells[0].Value = nomFichier;
                    row.Cells[1].Value = fichier;

                    dgv_emplacement.Rows.Add(row);
                }

                dgv_recap.Refresh();
            }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_Deplacer_Click(object sender, EventArgs e)
        {
            DesactiverModifierBtn();

            if (dgv_recap.Rows.Count == 1)
            {
                // Récupérer la valeur de la colonne "Numéro de départ" de la première ligne
                string numeroDepart = dgv_recap.Rows[0].Cells[3].Value?.ToString();

                // Vérifier si la valeur est vide
                if (string.IsNullOrEmpty(numeroDepart))
                {
                    // Désactiver le bouton Modifier
                    btn_Modifier.Enabled = false;
                }
                else
                {
                    // Activer le bouton Modifier
                    btn_Modifier.Enabled = true;
                }
            }

            if (dgv_emplacement.SelectedRows.Count > 0)
            {
                // Récupérer la ligne sélectionnée
                DataGridViewRow selectedRow = dgv_emplacement.SelectedRows[0];

                // Récupérer les valeurs des cellules de la ligne sélectionnée
                string nomFichier = selectedRow.Cells[0].Value.ToString();
                string cheminFichier = selectedRow.Cells[1].Value.ToString();
                string nbrExemplaire = nup_NbrExemplaire.Value.ToString();

                // Créer une nouvelle ligne dans dgv_recap avec les mêmes valeurs
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(dgv_recap);
                newRow.Cells[0].Value = nomFichier;
                newRow.Cells[1].Value = cheminFichier;
                newRow.Cells[2].Value = nbrExemplaire;

                if (dgv_recap.Rows.Count > 1)
                {
                    // Récupérer l'index de la dernière ligne
                    int derniereLigne = dgv_recap.Rows.Count - 2;

                    if (dgv_recap.Rows[derniereLigne].Cells[4].Value != null)
                    {
                        // Récupérer la valeur de la colonne 3 de la dernière ligne
                        int numeroFin = int.Parse(dgv_recap.Rows[derniereLigne].Cells[4].Value.ToString());

                        // Mettre à jour la valeur de la colonne 2 de la nouvelle ligne avec la valeur de la colonne 3 de la dernière ligne
                        newRow.Cells[3].Value = numeroFin + 1;
                    }
                    else { MessageBox.Show("La colonne 'Numéro de départ' doit être renseignée"); }

                }

                // Ajouter la nouvelle ligne à dgv_recap
                dgv_recap.Rows.Add(newRow);

                
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une ligne dans dgv_emplacement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void DesactiverModifierBtn()
        {
            if (dgv_recap.Rows.Count > 1)
            {
                btn_Modifier2.Enabled = false;
                if (dgv_recap.SelectedRows[0].Index == 0)
                {
                    btn_Modifier2.Enabled = true;
                }

            }
            else
            {
                btn_Modifier2.Enabled = true;
            }
        }

        private void dup_NbrExemplaire_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void dup_NbrExemplaire_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void btn_Modifier_Click(object sender, EventArgs e)
        {
            // Vérifier si une ligne est sélectionnée dans dgv_recap
            if (dgv_recap.SelectedRows.Count > 0)
            {
                // Récupérer la ligne sélectionnée
                DataGridViewRow selectedRow = dgv_recap.SelectedRows[0];

                // Récupérer la valeur de dup_NbrExemplaire
                string nouveauNombreExemplaires = nup_NbrExemplaire.Value.ToString();

                // Modifier le contenu de la colonne 3 avec le nouveau nombre d'exemplaires
                selectedRow.Cells[2].Value = nouveauNombreExemplaires;


            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une ligne dans dgv_recap.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            MajNumeroFin();
        }

        private void btn_Modifier2_Click(object sender, EventArgs e)
        {
            MajNumeroFin();

            if (dgv_recap.Rows.Count > 0)
            {
                // Récupérer la valeur de la colonne "Numéro de départ" de la première ligne
                string numeroDepart = dgv_recap.Rows[0].Cells[3].Value?.ToString();

                // Vérifier si la valeur est vide
                if (string.IsNullOrEmpty(numeroDepart))
                {
                    // Désactiver le bouton Modifier
                    btn_Modifier.Enabled = false;
                    nup_NbrExemplaire.Enabled = false;
                }
                else
                {
                    // Activer le bouton Modifier
                    btn_Modifier.Enabled = true;
                    nup_NbrExemplaire.Enabled = true;
                }
            }
        }

        private void dgv_recap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_recap_MouseClick(object sender, MouseEventArgs e)
        {
            DesactiverModifierBtn();
        }

        private void MajNumeroFin()
        {
            // Vérifier si une ligne est sélectionnée dans dgv_recap
            if (dgv_recap.SelectedRows.Count > 0)
            {
                // Récupérer la ligne sélectionnée
                DataGridViewRow selectedRow = dgv_recap.SelectedRows[0];

                if (dgv_recap.SelectedRows[0].Index == 0)
                {
                    // Récupérer la valeur de txt_numero
                    string numDepart = txt_numDepart.Text;

                    // Modifier le contenu de la colonne 3 avec le nouveau numéro de départ
                    selectedRow.Cells[3].Value = numDepart;

                    // Calculer et modifier le contenu de la colonne 4 avec l'addition du numéro de départ et du nombre d'exemplaires
                    int nombreExemplaires = Convert.ToInt32(selectedRow.Cells[2].Value);
                    int numeroDepart = Convert.ToInt32(numDepart);
                    int total = numeroDepart + nombreExemplaires - 1;
                    selectedRow.Cells[4].Value = total.ToString();



                }
                else
                {
                    // Récupérer la valeur de txt_numero
                    string numDepart = selectedRow.Cells[3].Value.ToString();

                    // Modifier le contenu de la colonne 3 avec le nouveau numéro de départ
                    selectedRow.Cells[3].Value = numDepart;

                    // Calculer et modifier le contenu de la colonne 4 avec l'addition du numéro de départ et du nombre d'exemplaires
                    int nombreExemplaires = Convert.ToInt32(selectedRow.Cells[2].Value);
                    int numeroDepart = Convert.ToInt32(numDepart);
                    int total = numeroDepart + nombreExemplaires - 1;
                    selectedRow.Cells[4].Value = total.ToString();
                }

            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une ligne dans dgv_recap.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void dgv_recap_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Vérifier si la cellule modifiée est dans la colonne "Numéro de fin" et si la ligne modifiée est la première ligne
            if (e.ColumnIndex == 4 && e.RowIndex == 0)
            {
                // Récupérer la nouvelle valeur de la cellule "Numéro de fin"
                object nouvelleValeurObj = dgv_recap.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (nouvelleValeurObj != null)
                {
                    int nouvelleValeur;
                    // Vérifier si la nouvelle valeur peut être convertie en entier
                    if (int.TryParse(nouvelleValeurObj.ToString(), out nouvelleValeur))
                    {
                        // Récupérer la ligne suivante
                        DataGridViewRow ligneSuivante = dgv_recap.Rows[e.RowIndex + 1];

                        // Vérifier si la ligne suivante n'est pas la dernière ligne (la ligne vide)
                        if (ligneSuivante.Index != dgv_recap.Rows.Count - 1)
                        {
                            // Mettre à jour la valeur de la cellule "Numéro de départ" de la ligne suivante
                            ligneSuivante.Cells[3].Value = nouvelleValeur + 1;
                        }
                    }
                }
            }
        }
            private void nup_NbrExemplaire_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btn_supprimer_Click(object sender, EventArgs e)
        {
            // Vérifier si une ligne est sélectionnée dans dgv_recap
            if (dgv_recap.SelectedRows.Count > 0)
            {
                int NombreLignes = dgv_recap.SelectedRows.Count;

                // Utiliser une boucle for descendante pour éviter les problèmes d'index lors de la suppression
                for (int k = NombreLignes - 1; k >= 0; k--)
                {
                    // Récupérer la ligne sélectionnée
                    DataGridViewRow selectedRow = dgv_recap.SelectedRows[k];

                    // Supprimer la ligne du DataGridView
                    dgv_recap.Rows.Remove(selectedRow);
                }
            }
        }
    }
}
