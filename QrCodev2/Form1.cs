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

    public partial class Form1 : Form
    {
        private readonly HttpClient _client;


        public Form1()
        {
            InitializeComponent();
            // Initialisation de _client
            _client = new HttpClient();
            string apiKey = "KEY";
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private async void btn_Generer_Click(object sender, EventArgs e)
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

                        int currentNumero = numeroDepart + i;  // Calcul du numéro pour cette plaque
                        string customUrl = $"https://www.qrcode.mediapush.fr/{currentNumero}";  // Construction de l'URL avec le numéro

                        int linkId = await CreateLinkAsync(currentNumero); // Utilisation du numéro actuel comme paramètre


                        // Use the linkId to create the QR code
                        int qrCodeId = await CreateQrCodeAsync(currentNumero.ToString(), "url", currentNumero);  // Modification pour passer l'URL

                        // Load and draw the QR Code
                        XImage overlayImage = await GetQrCodeImageAsync(qrCodeId);
                        double overlayWidth = 74; // in pixels
                        double overlayHeight = 74; // in pixels
                        double overlayX = x + (imageWidthCm * 28.3465) - overlayWidth - 40; // Adjust right margin
                        double overlayY = y + (imageHeightCm * 28.3465) - overlayHeight - 67; // Adjust bottom margin
                        gfx.DrawImage(overlayImage, overlayX, overlayY, overlayWidth, overlayHeight);

                        // Draw plaque number
                        XSize textSize = gfx.MeasureString(plaqueNumber.ToString(), new XFont("Arial", 10));
                        double plaqueNumberX = x + (imageWidthCm * 28.3465) / 2 - textSize.Width / 2 + 48.5;
                        double plaqueNumberY = overlayY + 90; // Adjust vertical position if necessary
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
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
            saveFileDialog.Title = "Save PDF File";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                document.Save(saveFileDialog.FileName);
            }
        }
        public static void ConvertSvgToPdf(string svgContent, string pdfOutputPath)
        {
            // Créer un objet SVG à partir du contenu SVG
            var svgDocument = SvgDocument.FromSvg<SvgDocument>(svgContent);

            // Dessiner le SVG en bitmap
            using (var bitmap = svgDocument.Draw())
            {
                // Créer un nouveau document PDF
                PdfSharpCore.Pdf.PdfDocument pdf = new PdfSharpCore.Pdf.PdfDocument();
                pdf.Info.Title = "SVG to PDF";
                PdfSharpCore.Pdf.PdfPage page = pdf.AddPage();
                page.Width = bitmap.Width;
                page.Height = bitmap.Height;

                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Convertir le Bitmap en MemoryStream PNG
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    stream.Position = 0; // Remettre le curseur du stream au début pour la lecture

                    // Utilisation d'une fonction lambda pour créer XImage
                    XImage image = XImage.FromStream(() => new MemoryStream(stream.ToArray()));

                    // Dessiner l'image bitmap dans le document PDF
                    gfx.DrawImage(image, 0, 0, bitmap.Width, bitmap.Height);
                }

                // Sauvegarder le PDF
                pdf.Save(pdfOutputPath);
            }
        }
        public async Task<int> CreateQrCodeAsync(string name, string type, int numeroDepart)
        {
            string url = $"https://www.qrcode.mediapush.fr/{numeroDepart}";
            var content = new MultipartFormDataContent
    {
        { new StringContent(name), "name" },
        { new StringContent(type), "type" },
        { new StringContent(url), "url" }
    };

            var apiUrl = "https://www.qrcode.mediapush.fr/api/qr-codes";

            try
            {
                HttpResponseMessage response = await _client.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                dynamic responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);
                return responseObject.data.id;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API error: {ex.Message}");
                throw;
            }
        }

        public async Task<int> CreateLinkAsync(int currentNumero)
        {
            var content = new MultipartFormDataContent
    {
        { new StringContent("https://mediapush.fr"), "location_url" },
        { new StringContent(currentNumero.ToString()), "url" }
    };

            var apiUrl = "https://www.qrcode.mediapush.fr/api/links";

            try
            {
                HttpResponseMessage response = await _client.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                dynamic responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);
                return responseObject.data.id;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API error: {ex.Message}");
                throw;
            }
        }

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

            // Ajouter une colonne d'image pour les miniatures
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.HeaderText = "Prévisualisation";
            imageColumn.Name = "colonneImage";
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            imageColumn.Width = 50; // Largeur de la colonne pour les miniatures
            dgv_emplacement.Columns.Add(imageColumn);

            


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
            dgv_emplacement.Rows.Clear();

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Sélectionnez un dossier contenant les fichiers à ajouter";

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string dossierSelectionne = folderBrowserDialog.SelectedPath;

                lbl_chemin.Text = dossierSelectionne;
                lbl_chemin.Visible = true;

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
                    row.Cells[2].Value = CreateThumbnail(fichier); // Charger la miniature

                    dgv_emplacement.Rows.Add(row);
                }

                dgv_recap.Refresh();
            }
        }
        private Image CreateThumbnail(string imagePath)
        {
            try
            {
                using (Image image = Image.FromFile(imagePath))
                {
                    int thumbnailSize = 50; // Taille de la miniature
                    int width, height;

                    if (image.Width > image.Height)
                    {
                        width = thumbnailSize;
                        height = (int)(image.Height * thumbnailSize / (float)image.Width);
                    }
                    else
                    {
                        height = thumbnailSize;
                        width = (int)(image.Width * thumbnailSize / (float)image.Height);
                    }

                    Bitmap thumbnail = new Bitmap(width, height);
                    using (Graphics g = Graphics.FromImage(thumbnail))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(image, 0, 0, width, height);
                    }

                    return thumbnail;
                }
            }
            catch
            {
                return null;
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
                txt_numDepart.Enabled = false;

                if (dgv_recap.SelectedRows[0].Index == 0)
                {
                    btn_Modifier2.Enabled = true;
                    txt_numDepart.Enabled = true;
                }

            }
            else
            {
                btn_Modifier2.Enabled = true;
                txt_numDepart.Enabled = true;
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

        private void lbl_chemin_Click(object sender, EventArgs e)
        {

        }

        public async Task<XImage> GetQrCodeImageAsync(int qrCodeId)
        {
            string apiUrl = $"https://www.qrcode.mediapush.fr/api/qr-codes/{qrCodeId}";

            try
            {
                HttpResponseMessage response = await _client.GetAsync(apiUrl);
                if (!response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to fetch QR code: {response.StatusCode} - {responseContent}");
                    throw new Exception($"API call failed with status code {response.StatusCode}");
                }

                string responseBody = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(responseBody);

                if (jsonResponse["data"] == null || jsonResponse["data"]["qr_code"] == null)
                {
                    throw new Exception("QR code SVG URL not found in API response.");
                }

                string qrCodeUrl = jsonResponse["data"]["qr_code"].ToString();
                HttpResponseMessage svgResponse = await _client.GetAsync(qrCodeUrl);
                if (!svgResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to download QR code SVG.");
                }

                string svgContent = await svgResponse.Content.ReadAsStringAsync();
                return ConvertSvgToXImage(svgContent);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Network error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error processing QR code SVG: {ex.Message}");
            }
        }
        public static XImage ConvertSvgToXImage(string svgContent)
        {
            var svgDocument = SvgDocument.FromSvg<SvgDocument>(svgContent);
            using (var bitmap = svgDocument.Draw())
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    stream.Position = 0;
                    // Utilisez une fonction lambda pour créer XImage à partir du MemoryStream
                    return XImage.FromStream(() => new MemoryStream(stream.ToArray()));
                }
            }
        }
    }
  
}
