using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QrCodev2
{
    public partial class APIKey : Form
    {
        public string ApiKey { get; private set; }
        public APIKey(string currentApiKey)
        {
            InitializeComponent();
            txtApiKey.Text = currentApiKey;
        }

        private void APIKey_Load(object sender, EventArgs e)
        {

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            ApiKey = txtApiKey.Text;
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
