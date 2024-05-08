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

namespace QrCodev2
{
    public partial class Form1 : Form
    {
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
            try
            {
                float imageWidthCm = float.Parse(txt_largeurPlaque.Text); // Largeur de l'image en centimètres
                float imageHeightCm = float.Parse(txt_hauteurPlaque.Text); // Hauteur de l'image en centimètres
                float marginCm = float.Parse(txt_margePlaque.Text); // Marge en centimètres
                int totalWidthCm = int.Parse(txt_largeurDocument.Text); // Largeur totale du document en centimètres
                int totalHeightCm = int.Parse(txt_hauteurDocument.Text); // Hauteur totale du document en centimètres

                // Calcul du nombre de plaques par ligne
                int plaquesPerLine = (int)((totalWidthCm - marginCm) / (imageWidthCm + marginCm));

                // Création du document PDF
                PdfDocument document = new PdfDocument();
                PdfPage page = document.AddPage();

                // Définir la taille de la page
                page.Width = XUnit.FromCentimeter(totalWidthCm);
                page.Height = XUnit.FromCentimeter(totalHeightCm);
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Chargement et dessin de l'image à superposer (QR Code)
                XImage overlayImage = XImage.FromFile(@"C:\Users\andre\OneDrive\Desktop\thumbnail_0346.png");

                // Paramètres pour la superposition
                double overlayWidth = 74; // en pixels
                double overlayHeight = 74; // en pixels

                for (int i = 0; i < int.Parse(txt_nbrExemplaire.Text); i++)
                {
                    int row = i / plaquesPerLine;
                    int col = i % plaquesPerLine;

                    // Positionnement des plaques avec marge
                    double x = col * (imageWidthCm * 28.3465 + marginCm * 28.3465) + marginCm * 28.3465;
                    double y = row * (imageHeightCm * 28.3465 + marginCm * 28.3465) + marginCm * 28.3465;

                    // Chargement et dessin de l'image principale
                    XImage image = XImage.FromFile(imagePath);
                    gfx.DrawImage(image, x, y, imageWidthCm * 28.3465, imageHeightCm * 28.3465);

                    // Dessiner le QR code
                    double overlayX = x + (imageWidthCm * 28.3465) - overlayWidth - 40; // Marge de droite ajustée
                    double overlayY = y + (imageHeightCm * 28.3465) - overlayHeight - 67; // Marge de bas ajustée
                    gfx.DrawImage(overlayImage, overlayX, overlayY, overlayWidth, overlayHeight);

                    // Ajouter le numéro sur chaque plaque
                    string plaqueNumber = (int.Parse(txt_numDepart.Text) + i).ToString();
                    XSize textSize = gfx.MeasureString(plaqueNumber, new XFont("Verdana", 20));
                    double textX = x + (imageWidthCm * 28.3465) - textSize.Width - 50; // Marge de droite pour le texte
                    double textY = y + 190; // Position verticale du texte ajustée
                    gfx.DrawString(plaqueNumber, new XFont("Poppins-SemiBold", 6), XBrushes.Black, new XPoint(textX, textY));
                }

                // Enregistrement du PDF
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Fichiers PDF (*.pdf)|*.pdf|Tous les fichiers (*.*)|*.*";
                saveFileDialog.Title = "Enregistrer le fichier PDF";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    document.Save(saveFileDialog.FileName);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Veuillez remplir correctement les champs");
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            lbl_chemin.Visible = false;

            DataGridViewTextBoxColumn colonne1 = new DataGridViewTextBoxColumn();
            colonne1.HeaderText = "Nom du fond";
            colonne1.Name = "colonneFond";

            dgv_recap.Columns.Add(colonne1);

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
            openFileDialog.Filter = "All Files|*.*"; // Filtre pour tous les types de fichiers

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Récupérer le chemin du fichier sélectionné
                string cheminFichier = openFileDialog.FileName;

                // Création d'une nouvelle ligne
                DataGridViewRow row = new DataGridViewRow();

                // Création de la cellule pour le nom du fichier
                DataGridViewTextBoxCell celluleNomFichier = new DataGridViewTextBoxCell();
                celluleNomFichier.Value = Path.GetFileName(cheminFichier);
                row.Cells.Add(celluleNomFichier);

                // Stocker le chemin du fichier dans la cellule
                row.Tag = cheminFichier;

                // Création de la cellule pour le nombre d'exemplaires
                DataGridViewTextBoxCell celluleNombreExemplaires = new DataGridViewTextBoxCell();
                celluleNombreExemplaires.Value = txt_nbrExemplaire.Text; // Vous pouvez modifier le nombre d'exemplaires selon vos besoins
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
    }
}
