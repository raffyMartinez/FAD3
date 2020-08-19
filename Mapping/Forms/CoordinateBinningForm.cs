using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FAD3.Mapping.Forms
{
    public partial class CoordinateBinningForm : Form
    {
        public CoordinateBinningForm()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            global.LoadFormSettings(this);
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            global.SaveFormSettings(this);
        }
    }
}
