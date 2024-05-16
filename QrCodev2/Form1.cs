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
        /// <summary>
        /// déclaration des variables
        /// </summary>
        private readonly HttpClient _client;
        private Produits produitsForm;
        /// <summary>
        /// Initialisation de la fenêtre de départ
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            _client = new HttpClient();
            string apiKey = "key";
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            produitsForm = new Produits(this);
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// bouton Générer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                if (dgvRow.Cells["colonneNombre"].Value != null && int.TryParse(dgvRow.Cells["colonneNombre"].Value.ToString(), out int nombreExemplaires) && nombreExemplaires > 0)
                {
                    string imagePath = dgvRow.Cells["colonneChemin"].Value?.ToString();
                    if (dgvRow.Cells["colonneNumDep"].Value != null && int.TryParse(dgvRow.Cells["colonneNumDep"].Value.ToString(), out int numeroDepart))
                    {
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

                            // Ajout du QR code
                            XImage overlayImage = await GetQrCodeImageAsync(qrCodeId);
                            double overlayWidth = 74; // in pixels
                            double overlayHeight = 74; // in pixels
                            double overlayX = x + (imageWidthCm * 28.3465) - overlayWidth - 40; // Adjust right margin
                            double overlayY = y + (imageHeightCm * 28.3465) - overlayHeight - 67; // Adjust bottom margin
                            gfx.DrawImage(overlayImage, overlayX, overlayY, overlayWidth, overlayHeight);

                            // Ajotu du néméro de plaque
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
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
            saveFileDialog.Title = "Save PDF File";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                document.Save(saveFileDialog.FileName);
            }
        }
        /// <summary>
        /// Méthode permettant de convertir le SVG en PDF
        /// </summary>
        /// <param name="svgContent"></param>
        /// <param name="pdfOutputPath"></param>
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
        /// <summary>
        /// Méthode permettant de créer un QR Code en prenant en compte son nom, son type et le numéro
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="numeroDepart"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Load de la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            nup_NbrExemplaire.Enabled = false;
            dgv_recap.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_recap.AllowUserToOrderColumns = false;
            nup_NbrExemplaire.Value = 1;

            dgv_recap.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nom du fond", Name = "colonneFond" });
            dgv_recap.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Chemin du fond", Name = "colonneChemin" });
            dgv_recap.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nombre", Name = "colonneNombre" });
            dgv_recap.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Numéro de départ", Name = "colonneNumDep" });
            dgv_recap.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Numéro de fin", Name = "colonneNumFin" });

            DataGridViewComboBoxColumn colorColumn = new DataGridViewComboBoxColumn();
            colorColumn.HeaderText = "Couleur du texte";
            colorColumn.Name = "colonneCouleur";
            colorColumn.Items.AddRange("blanc", "noir");
            dgv_recap.Columns.Add(colorColumn);

            LoadFilesFromResources(dgv_recap);
            dgv_recap.Columns["colonneChemin"].Visible = false;
        }

    
        /// <summary>
        /// Ajoute au dgv_recap les fichiers venant du dossier Resources
        /// </summary>
        /// <param name="dataGridView"></param>
        public void LoadFilesFromResources(DataGridView dataGridView)
        {
            string resourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Repertoire");

            if (Directory.Exists(resourcesPath))
            {
                string[] extensionsAutorisees = { ".pdf", ".png", ".jpg", ".jpeg", ".svg" };
                string[] fichiersDansDossier = Directory.GetFiles(resourcesPath)
                    .Where(fichier => extensionsAutorisees.Contains(Path.GetExtension(fichier).ToLower()))
                    .ToArray();

                foreach (string fichier in fichiersDansDossier)
                {
                    string nomFichier = Path.GetFileName(fichier);
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridView);
                    row.Cells[0].Value = nomFichier;
                    row.Cells[1].Value = fichier;
                    row.Cells[2].Value = 1; // Par défaut, 1 exemplaire
                    row.Cells[3].Value = ""; // Numéro de départ vide pour le moment
                    row.Cells[4].Value = ""; // Numéro de fin vide pour le moment

                    dataGridView.Rows.Add(row);
                }

                dataGridView.Refresh();
            }
            else
            {
                MessageBox.Show("Le dossier Repertoire n'existe pas.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_nbrExemplaire_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// bouton Ajouter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// bouton Parcourir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Parcourir_Click(object sender, EventArgs e)
        {

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
                    row.Cells[0].Value = nomFichier;
                    row.Cells[1].Value = fichier;
                    
                }

                dgv_recap.Refresh();
            }
        }
        


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// Méthode permettant de désactiver le bouton Modifier quand c'est nécessaire
        /// </summary>
        private void DesactiverModifierBtn()
        {
            bool enableNumericUpDown = false;

            if (dgv_recap.Rows.Count > 0)
            {
                txt_numDepart.Enabled = dgv_recap.SelectedRows.Count > 0 && dgv_recap.SelectedRows[0].Index == 0;

                if (dgv_recap.Rows[0].Cells["colonneNumDep"].Value == null || dgv_recap.Rows[0].Cells["colonneNumDep"].Value.ToString() == "")
                {
                    btn_Modifier.Enabled = false;
                }
                else
                {
                    btn_Modifier.Enabled = true;
                }

                enableNumericUpDown = true;
            }
            else
            {
                txt_numDepart.Enabled = true;
            }

            nup_NbrExemplaire.Enabled = enableNumericUpDown;
        }

        private void dup_NbrExemplaire_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void dup_NbrExemplaire_MouseUp(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// bouton Modifier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Modifier_Click(object sender, EventArgs e)
        {
            if (txt_numDepart != null)
            {
                nup_NbrExemplaire.Enabled = true;
            }
            
            // Vérifier si une ligne est sélectionnée dans dgv_recap
            if (dgv_recap.SelectedRows.Count > 0)
            {
                // Récupérer la ligne sélectionnée
                DataGridViewRow selectedRow = dgv_recap.SelectedRows[0];

                // Récupérer la valeur de nup_NbrExemplaire
                string nouveauNombreExemplaires = nup_NbrExemplaire.Value.ToString();

                // Modifier le contenu de la colonne 3 avec le nouveau nombre d'exemplaires
                selectedRow.Cells["colonneNombre"].Value = nouveauNombreExemplaires;

                MajNumeroFin();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une ligne dans dgv_recap.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dgv_recap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        /// <summary>
        /// méthode sur le clic du Dgv recap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_recap_MouseClick(object sender, MouseEventArgs e)
        {
            DesactiverModifierBtn();
        }
        /// <summary>
        /// Méthode permettant la mise à jour du numéro de fin
        /// </summary>
        private void MajNumeroFin()
        {
            if (dgv_recap.Rows.Count > 0)
            {
                if (!int.TryParse(txt_numDepart.Text, out int numDepart))
                {
                    MessageBox.Show("Le numéro de départ n'est pas valide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                for (int i = 0; i < dgv_recap.Rows.Count; i++)
                {
                    DataGridViewRow row = dgv_recap.Rows[i];
                    if (row.Cells["colonneNombre"].Value != null && int.TryParse(row.Cells["colonneNombre"].Value.ToString(), out int nombreExemplaires))
                    {
                        if (nombreExemplaires > 0)
                        {
                            row.Cells["colonneNumDep"].Value = numDepart;
                            row.Cells["colonneNumFin"].Value = numDepart + nombreExemplaires - 1;
                            numDepart += nombreExemplaires;
                        }
                        else
                        {
                            row.Cells["colonneNumDep"].Value = "";
                            row.Cells["colonneNumFin"].Value = "";
                        }
                    }
                }
            }
        }
       /// <summary>
       /// actions sur le dgv recap quand la valeur d'une cellule change
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void dgv_recap_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.RowIndex == 0)
            {
                object nouvelleValeurObj = dgv_recap.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (nouvelleValeurObj != null && int.TryParse(nouvelleValeurObj.ToString(), out int nouvelleValeur))
                {
                    DataGridViewRow ligneSuivante = dgv_recap.Rows[e.RowIndex + 1];

                    if (ligneSuivante.Index != dgv_recap.Rows.Count - 1)
                    {
                        ligneSuivante.Cells[3].Value = nouvelleValeur + 1;
                    }
                }
            }
        }
        /// <summary>
        /// Actions lorsque la valeur de nup_NbrExemplaire change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nup_NbrExemplaire_ValueChanged(object sender, EventArgs e)
        {
            // Si la première ligne est sélectionnée, mettre à jour le nombre d'exemplaires
            if (dgv_recap.SelectedRows.Count > 0 && dgv_recap.SelectedRows[0].Index == 0)
            {
                DataGridViewRow selectedRow = dgv_recap.SelectedRows[0];
                selectedRow.Cells["colonneNombre"].Value = nup_NbrExemplaire.Value.ToString();
                MajNumeroFin();
            }
        }
        /// <summary>
        /// bouton Supprimer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Méthode permettant de récupérer le QR Code venant de l'API et de le convertir en image
        /// </summary>
        /// <param name="qrCodeId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// méthode permettant la conversion d'un SVP en XImage
        /// </summary>
        /// <param name="svgContent"></param>
        /// <returns></returns>
        public static XImage ConvertSvgToXImage(string svgContent)
        {
            var svgDocument = SvgDocument.FromSvg<SvgDocument>(svgContent);
            using (var bitmap = svgDocument.Draw())
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    stream.Position = 0;
                    // Utilise une fonction lambda pour créer XImage à partir du MemoryStream
                    return XImage.FromStream(() => new MemoryStream(stream.ToArray()));
                }
            }
        }

        /// <summary>
        /// bouton Produits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_produits_Click(object sender, EventArgs e)
        {
            produitsForm.ShowDialog();
        }
    }
  
}
