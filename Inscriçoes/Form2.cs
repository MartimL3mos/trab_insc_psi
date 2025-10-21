using System;
using System.Windows.Forms;

namespace Inscriçoes
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();       // Mostra o Form1
            this.Hide();        // Esconde o Form2 (ou use this.Close() para fechar completamente)
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();  // Cria uma instância do Form2
            form1.Show();               // Mostra o Form2
            this.Hide();                // Esconde o Form1 (opcional)
        }
    }
}

