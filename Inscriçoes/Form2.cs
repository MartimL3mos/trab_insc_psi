using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Inscriçoes
{
    public partial class Form2 : Form
    {
      
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CarregarCursos();
        }

      
        private void CarregarCursos()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT IdCurso, NomeCurso FROM Curso", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboCurso.DataSource = dt;
                    comboCurso.DisplayMember = "NomeCurso";
                    comboCurso.ValueMember = "IdCurso";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar cursos: " + ex.Message);
                }
            }
        }

      
        private void buttonEnviar_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            DateTime dataNascimento = dateNascimento.Value;
            string email = txtEmail.Text;
            string morada = txtMorada.Text;
            int idCurso = Convert.ToInt32(comboCurso.SelectedValue);

            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(morada))
            {
                MessageBox.Show("Por favor, preencha todos os campos obrigatórios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"INSERT INTO Inscricoes (Nome, DataNascimento, Email, Morada, IdCurso)
                                     VALUES (@Nome, @DataNascimento, @Email, @Morada, @IdCurso)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nome", nome);
                        cmd.Parameters.AddWithValue("@DataNascimento", dataNascimento);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Morada", morada);
                        cmd.Parameters.AddWithValue("@IdCurso", idCurso);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Inscrição efetuada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                   
                    txtNome.Clear();
                    txtEmail.Clear();
                    txtMorada.Clear();
                    dateNascimento.Value = DateTime.Now;
                    comboCurso.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao guardar inscrição: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

  
        private void buttonVoltar_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
