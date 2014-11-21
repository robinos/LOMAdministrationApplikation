using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOMAdministrationApplikation
{
	public partial class AnvändareForm : Form
	{
		public AnvändareForm()
		{
			InitializeComponent();
		}

		private void AnvändareForm_Load(object sender, EventArgs e)
		{
			// TODO: This line of code loads data into the 'lOM_DBDataSet.Anvandare' table. You can move, or remove it, as needed.
			this.anvandareTableAdapter.Fill(this.lOM_DBDataSet.Anvandare);

		}
	}
}
