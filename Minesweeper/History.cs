using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class History : Form
    {
        List<Log> logs;
        public History(List<Log> logs)
        {
            InitializeComponent();
            this.logs = logs;

            if (logs.Count == 0)
                label1.Text = "No history to show";
            else if (logs.Count == 1)
                label1.Text = "1 game in total";
            else label1.Text = logs.Count + " games in total";

            logs.ForEach(log => lbLogs.Items.Add(log));
        }

        private void History_Load(object sender, EventArgs e)
        {
            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
