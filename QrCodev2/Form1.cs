using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using Svg;
using ZXing;
using ZXing.Common;

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

        private void label1_Click(object sender, EventArgs e) { }

        private void groupBox1_Enter(object sender, EventArgs e) { }

        /// <summary>
        /// bouton Générer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btn_Generer_Click(object sender, EventArgs e)
        {
            try
            {
                float imageWidthCm = float.Parse(txt_largeurPlaque.Text);
                float imageHeightCm = float.Parse(txt_hauteurPlaque.Text);
                float marginCm = float.Parse(txt_margePlaque.Text);
                int totalWidthCm = int.Parse(txt_largeurDocument.Text);
                int totalHeightCm = int.Parse(txt_hauteurDocument.Text);

                // Dimensions de référence
                float referenceWidthCm = 8.4f;
                float referenceHeightCm = 8.4f;

                // Ratios de redimensionnement
                float widthRatio = imageWidthCm / referenceWidthCm;
                float heightRatio = imageHeightCm / referenceHeightCm;

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
                            string color = dgvRow.Cells["colonneCouleur"].Value?.ToString() ?? "noir";
                            string style = dgvRow.Cells["colonneStyle"].Value?.ToString() ?? "square";
                            XBrush brush = color == "blanc" ? XBrushes.White : XBrushes.Black;

                            for (int i = 0; i < nombreExemplaires; i++)
                            {
                                double x = currentCol * (imageWidthCm * 28.3465 + marginCm * 28.3465) + marginCm * 28.3465;
                                double y = currentRow * (imageHeightCm * 28.3465 + marginCm * 28.3465) + marginCm * 28.3465;

                                XImage image = XImage.FromFile(imagePath);
                                gfx.DrawImage(image, x, y, imageWidthCm * 28.3465, imageHeightCm * 28.3465);

                                int currentNumero = numeroDepart + i;
                                string customUrl = $"https://www.qrcode.mediapush.fr/{currentNumero}";

                                int linkId = await CreateLinkAsync(currentNumero);
                                int qrCodeId = await CreateQrCodeAsync(currentNumero.ToString(), "url", currentNumero, "#000000", style);

                                // Ajout du QR code avec redimensionnement
                                XImage overlayImage = await GetQrCodeImageAsync(qrCodeId);
                                double overlayWidth = 84 * widthRatio; // largeur redimensionnée
                                double overlayHeight = 84 * heightRatio; // hauteur redimensionnée
                                double overlayX = x + (imageWidthCm * 28.3465) - overlayWidth - (34.5 * widthRatio); // position X ajustée
                                double overlayY = y + (imageHeightCm * 28.3465) - overlayHeight - (62.5 * heightRatio); // position Y ajustée
                                gfx.DrawImage(overlayImage, overlayX, overlayY, overlayWidth, overlayHeight);

                                // Ajout du numéro de plaque avec redimensionnement
                                XFont font = new XFont("Poppins SemiBold", 4 * heightRatio); // taille de police redimensionnée
                                XSize textSize = gfx.MeasureString(plaqueNumber.ToString(), font);
                                double plaqueNumberX = x + (imageWidthCm * 28.3465) / 1.47 - textSize.Width / 2; // position X ajustée
                                double plaqueNumberY = overlayY + overlayHeight + (9 * heightRatio); // position Y ajustée
                                gfx.DrawString(plaqueNumber.ToString(), font, brush, plaqueNumberX, plaqueNumberY);

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
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



            /// <summary>
            /// Méthode permettant de convertir le SVG en PDF
            /// </summary>
            /// <param name="svgContent"></param>
            /// <param name="pdfOutputPath"></param>
            public static void ConvertSvgToPdf(string svgContent, string pdfOutputPath)
        {
            var svgDocument = SvgDocument.FromSvg<SvgDocument>(svgContent);

            using (var bitmap = svgDocument.Draw())
            {
                PdfSharpCore.Pdf.PdfDocument pdf = new PdfSharpCore.Pdf.PdfDocument();
                pdf.Info.Title = "SVG to PDF";
                PdfSharpCore.Pdf.PdfPage page = pdf.AddPage();
                page.Width = bitmap.Width;
                page.Height = bitmap.Height;

                XGraphics gfx = XGraphics.FromPdfPage(page);

                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    stream.Position = 0;

                    XImage image = XImage.FromStream(() => new MemoryStream(stream.ToArray()));
                    gfx.DrawImage(image, 0, 0, bitmap.Width, bitmap.Height);
                }

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
        public async Task<int> CreateQrCodeAsync(string name, string type, int numeroDepart, string foregroundColor = "#000000", string style = "square")
        {
            string url = $"https://www.qrcode.mediapush.fr/{numeroDepart}";
            var content = new MultipartFormDataContent {
        { new StringContent(name), "name" },
        { new StringContent(type), "type" },
        { new StringContent(url), "url" },
        { new StringContent(foregroundColor), "foreground_color" },
        { new StringContent(style), "style" }
    };

            var apiUrl = "https://www.qrcode.mediapush.fr/api/qr-codes";

            try
            {
                Console.WriteLine($"Sending color to API: {foregroundColor}, style: {style}");
                HttpResponseMessage response = await _client.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Response: {responseBody}");
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
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Erreur : Merci de vérifier votre clé d'API ou qu'un QR code ne possède pas le même numéro");
                }
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

            DataGridViewComboBoxColumn styleColumn = new DataGridViewComboBoxColumn();
            styleColumn.HeaderText = "Style QR Code";
            styleColumn.Name = "colonneStyle";
            styleColumn.Items.AddRange("round", "square");
            dgv_recap.Columns.Add(styleColumn);

            LoadFilesFromResources(dgv_recap);
            dgv_recap.Columns["colonneChemin"].Visible = false;

            string apiKey = GetCurrentApiKey();
            lbl_api.Visible = string.IsNullOrEmpty(apiKey) || apiKey == "key";
        }

        /// <summary>
        /// Ajoute au dgv_recap les fichiers venant du dossier Resources
        /// </summary>
        /// <param name="dataGridView"></param>
        public void LoadFilesFromResources(DataGridView dataGridView)
        {
            // Utilisation du dossier AppData pour éviter les problèmes de permissions
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string resourcesPath = Path.Combine(appDataPath, "QPDF", "Repertoire");

            if (!Directory.Exists(resourcesPath))
            {
                Directory.CreateDirectory(resourcesPath);
            }

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
                row.Cells[2].Value = 1;
                row.Cells[3].Value = "";
                row.Cells[4].Value = "";

                dataGridView.Rows.Add(row);
            }

            dataGridView.Refresh();
        }

        private void txt_nbrExemplaire_TextChanged(object sender, EventArgs e) { }

        private void label6_Click(object sender, EventArgs e) { }

        /// <summary>
        /// bouton Ajouter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ajouter(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "PDF Files|*.pdf";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string cheminFichier = openFileDialog.FileName;
                string nomFichier = Path.GetFileName(cheminFichier);

                DataGridViewRow row = new DataGridViewRow();

                DataGridViewTextBoxCell celluleNomFichier = new DataGridViewTextBoxCell();
                celluleNomFichier.Value = nomFichier;
                row.Cells.Add(celluleNomFichier);

                DataGridViewTextBoxCell celluleCheminFichier = new DataGridViewTextBoxCell();
                celluleCheminFichier.Value = cheminFichier;
                row.Cells.Add(celluleCheminFichier);

                DataGridViewTextBoxCell celluleNombreExemplaires = new DataGridViewTextBoxCell();
                celluleNombreExemplaires.Value = nup_NbrExemplaire.Text;
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) { }

        /// <summary>
        /// bouton Parcourir
        /// </summary>
        /// <param sender="object"></param>
        /// <param e="EventArgs"></param>
        private void btn_Parcourir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Sélectionnez un dossier contenant les fichiers à ajouter";

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string dossierSelectionne = folderBrowserDialog.SelectedPath;

                string[] extensionsAutorisees = new string[] { ".pdf", ".png", ".jpg", ".jpeg", ".svg" };
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

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

        private void dup_NbrExemplaire_SelectedItemChanged(object sender, EventArgs e) { }

        private void dup_NbrExemplaire_MouseUp(object sender, MouseEventArgs e) { }

        /// <summary>
        /// bouton Modifier
        /// </summary>
        /// <param sender="object"></param>
        /// <param e="EventArgs"></param>
        private void btn_Modifier_Click(object sender, EventArgs e)
        {
            if (txt_numDepart != null)
            {
                nup_NbrExemplaire.Enabled = true;
            }

            if (dgv_recap.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgv_recap.SelectedRows[0];
                string nouveauNombreExemplaires = nup_NbrExemplaire.Value.ToString();
                selectedRow.Cells["colonneNombre"].Value = nouveauNombreExemplaires;
                MajNumeroFin();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une ligne dans dgv_recap.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_recap_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

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
        /// <param sender="object"></param>
        /// <param e="DataGridViewCellEventArgs"></param>
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
        /// <param sender="object"></param>
        /// <param e="EventArgs"></param>
        private void nup_NbrExemplaire_ValueChanged(object sender, EventArgs e)
        {
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
        /// <param sender="object"></param>
        /// <param e="EventArgs"></param>
        private void btn_supprimer_Click(object sender, EventArgs e)
        {
            if (dgv_recap.SelectedRows.Count > 0)
            {
                int NombreLignes = dgv_recap.SelectedRows.Count;

                for (int k = NombreLignes - 1; k >= 0; k--)
                {
                    DataGridViewRow selectedRow = dgv_recap.SelectedRows[k];
                    dgv_recap.Rows.Remove(selectedRow);
                }
            }
        }

        private void lbl_chemin_Click(object sender, EventArgs e) { }

        /// <summary>
        /// Méthode permettant de récupérer le QR Code venant de l'API et de le convertir en image
        /// </summary>
        /// <param qrCodeId="int"></param>
        /// <returns Task="XImage"></returns>
        public async Task<XImage> GetQrCodeImageAsync(int qrCodeId)
        {
            string apiUrl = $"https://www.qrcode.mediapush.fr/api/qr-codes/{qrCodeId}";

            try
            {
                HttpResponseMessage response = await _client.GetAsync(apiUrl);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Erreur : Merci de vérifier votre clé d'API ou qu'un QR code ne possède pas le même numéro");
                }
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
                if (svgResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Erreur : Merci de vérifier votre clé d'API ou qu'un QR code ne possède pas le même numéro");
                }
                if (!svgResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to download QR code SVG.");
                }

                string svgContent = await svgResponse.Content.ReadAsStringAsync();
                return await ConvertSvgToXImageAsync(svgContent);
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
        /// méthode permettant la conversion d'un SVG en XImage
        /// </summary>
        /// <param svgContent="string"></param>
        /// <returns XImage="Task<XImage>"></returns>
        public static async Task<XImage> ConvertSvgToXImageAsync(string svgContent)
        {
            var svgDocument = SvgDocument.FromSvg<SvgDocument>(svgContent);
            using (var bitmap = svgDocument.Draw())
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    stream.Position = 0;
                    return XImage.FromStream(() => new MemoryStream(stream.ToArray()));
                }
            }
        }

        /// <summary>
        /// bouton Produits
        /// </summary>
        /// <param sender="object"></param>
        /// <param e="EventArgs"></param>
        private void btn_produits_Click(object sender, EventArgs e)
        {
            produitsForm.ShowDialog();
        }

        private void btn_api_Click(object sender, EventArgs e)
        {
            using (APIKey apiKeyForm = new APIKey(GetCurrentApiKey()))
            {
                if (apiKeyForm.ShowDialog() == DialogResult.OK)
                {
                    string newApiKey = apiKeyForm.ApiKey;
                    UpdateApiKey(newApiKey);

                    // Check API key and set lbl_api visibility
                    lbl_api.Visible = string.IsNullOrEmpty(newApiKey) || newApiKey == "key";
                }
            }
        }

        private string GetCurrentApiKey()
        {
            // Obtenez la clé API actuelle, cette méthode devrait être implémentée pour récupérer la clé actuelle si nécessaire
            return _client.DefaultRequestHeaders.Authorization?.Parameter;
        }

        private void UpdateApiKey(string newApiKey)
        {
            // Met à jour l'API Key dans le HttpClient
            _client.DefaultRequestHeaders.Remove("Authorization");
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {newApiKey}");

            // Vous pouvez également enregistrer la nouvelle clé API dans un fichier de configuration ou autre stockage persistant si nécessaire
        }

        private async void btn_enregistrerQRcode_Click(object sender, EventArgs e)
        {
            // Open a dialog to select a folder
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select a folder to save the QR code SVG files";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFolder = folderBrowserDialog.SelectedPath;

                    foreach (DataGridViewRow dgvRow in dgv_recap.Rows)
                    {
                        if (dgvRow.Cells["colonneNombre"].Value != null && int.TryParse(dgvRow.Cells["colonneNombre"].Value.ToString(), out int nombreExemplaires) && nombreExemplaires > 0)
                        {
                            if (dgvRow.Cells["colonneNumDep"].Value != null && int.TryParse(dgvRow.Cells["colonneNumDep"].Value.ToString(), out int numeroDepart))
                            {
                                string style = dgvRow.Cells["colonneStyle"].Value?.ToString() ?? "square";

                                for (int i = 0; i < nombreExemplaires; i++)
                                {
                                    int currentNumero = numeroDepart + i;

                                    // Create the link and the QR code
                                    int linkId = await CreateLinkAsync(currentNumero);
                                    int qrCodeId = await CreateQrCodeAsync(currentNumero.ToString(), "url", currentNumero, "#000000", style);

                                    // Generate the SVG file for the QR code
                                    string filePath = Path.Combine(selectedFolder, $"{currentNumero}.svg");
                                    await SaveQrCodeToSvgAsync(qrCodeId, filePath);
                                }
                            }
                        }
                    }

                    MessageBox.Show("The QR code SVG files have been successfully saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public async Task SaveQrCodeToSvgAsync(int qrCodeId, string filePath)
        {
            string apiUrl = $"https://www.qrcode.mediapush.fr/api/qr-codes/{qrCodeId}";

            try
            {
                HttpResponseMessage response = await _client.GetAsync(apiUrl);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Erreur : Merci de vérifier votre clé d'API ou qu'un QR code ne possède pas le même numéro");
                }
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API call failed with status code {response.StatusCode}");
                }

                string responseBody = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(responseBody);

                if (jsonResponse["data"] == null || jsonResponse["data"]["qr_code"] == null)
                {
                    throw new Exception("QR code SVG URL not found in API response.");
                }

                string qrCodeSvgUrl = jsonResponse["data"]["qr_code"].ToString();
                string svgContent = await _client.GetStringAsync(qrCodeSvgUrl);

                await Task.Run(() => File.WriteAllText(filePath, svgContent));
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

        private async void btn_PNG_Click(object sender, EventArgs e)
        {
            // Open a dialog to select a color
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Color selectedColor = colorDialog.Color; // Foreground color

                // Open a dialog to select a folder
                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                {
                    folderBrowserDialog.Description = "Select a folder to save the QR code PNG files";

                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        string selectedFolder = folderBrowserDialog.SelectedPath;

                        foreach (DataGridViewRow dgvRow in dgv_recap.Rows)
                        {
                            if (dgvRow.Cells["colonneNombre"].Value != null && int.TryParse(dgvRow.Cells["colonneNombre"].Value.ToString(), out int nombreExemplaires) && nombreExemplaires > 0)
                            {
                                if (dgvRow.Cells["colonneNumDep"].Value != null && int.TryParse(dgvRow.Cells["colonneNumDep"].Value.ToString(), out int numeroDepart))
                                {
                                    string style = dgvRow.Cells["colonneStyle"].Value?.ToString() ?? "square";

                                    for (int i = 0; i < nombreExemplaires; i++)
                                    {
                                        int currentNumero = numeroDepart + i;
                                        string qrCodeText = $"https://www.qrcode.mediapush.fr/{currentNumero}";

                                        // Generate the QR code in PNG with the selected color and style
                                        Bitmap qrCodeBitmap = await GenerateQrCodeAsync(qrCodeText, selectedColor, Color.White, style);
                                        string filePath = Path.Combine(selectedFolder, $"{currentNumero}.png");
                                        qrCodeBitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                                    }
                                }
                            }
                        }

                        MessageBox.Show("The QR code PNG files have been successfully saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        public async Task SaveQrCodeToPngAsync(int qrCodeId, string filePath)
        {
            string apiUrl = $"https://www.qrcode.mediapush.fr/api/qr-codes/{qrCodeId}";

            try
            {
                HttpResponseMessage response = await _client.GetAsync(apiUrl);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Erreur : Merci de vérifier votre clé d'API ou qu'un QR code ne possède pas le même numéro");
                }
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API call failed with status code {response.StatusCode}");
                }

                string responseBody = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(responseBody);

                if (jsonResponse["data"] == null || jsonResponse["data"]["qr_code"] == null)
                {
                    throw new Exception("QR code SVG URL not found in API response.");
                }

                string qrCodeSvgUrl = jsonResponse["data"]["qr_code"].ToString();
                string svgContent = await _client.GetStringAsync(qrCodeSvgUrl);
                Console.WriteLine($"SVG Content: {svgContent}");

                var svgDocument = SvgDocument.FromSvg<SvgDocument>(svgContent);
                using (var bitmap = svgDocument.Draw())
                {
                    bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                }
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

        public async Task<Bitmap> GenerateQrCodeAsync(string text, Color foregroundColor, Color backgroundColor, string style)
        {
            int qrCodeId = await CreateQrCodeAsync(text, "url", text.GetHashCode(), ColorTranslator.ToHtml(foregroundColor), style);
            string svgContent = await GetQrCodeSvgContentAsync(qrCodeId);

            // Update the SVG content to reflect the foreground color
            svgContent = svgContent.Replace("#000000", ColorTranslator.ToHtml(foregroundColor)); // Assuming default color is black

            var svgDocument = SvgDocument.FromSvg<SvgDocument>(svgContent);
            using (var bitmap = svgDocument.Draw())
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    return new Bitmap(stream);
                }
            }
        }
        public async Task<string> GetQrCodeSvgContentAsync(int qrCodeId)
        {
            string apiUrl = $"https://www.qrcode.mediapush.fr/api/qr-codes/{qrCodeId}";

            try
            {
                HttpResponseMessage response = await _client.GetAsync(apiUrl);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Error: Please check your API key or if a QR code has the same number");
                }
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API call failed with status code {response.StatusCode}");
                }

                string responseBody = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(responseBody);

                if (jsonResponse["data"] == null || jsonResponse["data"]["qr_code"] == null)
                {
                    throw new Exception("QR code SVG URL not found in API response.");
                }

                string qrCodeSvgUrl = jsonResponse["data"]["qr_code"].ToString();
                return await _client.GetStringAsync(qrCodeSvgUrl);
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

    }



}
