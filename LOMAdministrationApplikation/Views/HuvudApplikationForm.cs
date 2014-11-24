using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LOMAdministrationApplikation.Models;

namespace LOMAdministrationApplikation.Views
{
	public partial class HuvudApplikationForm : Form
	{
		private AdministrationApplikation administrationApplikation = null;
		private ProduktForm produktForm = null;
		private AnvändareForm användareForm = null;

		public HuvudApplikationForm(AdministrationApplikation administrationApplikation)
		{
			this.administrationApplikation = administrationApplikation;
			InitializeComponent();
		}

		private void btnAnvändare_Click(object sender, EventArgs e)
		{
			användareForm = new AnvändareForm(administrationApplikation);
			användareForm.Show();
		}

		private void btnProdukter_Click(object sender, EventArgs e)
		{
			produktForm = new ProduktForm(administrationApplikation);
			produktForm.Show();
		}
	}
}
