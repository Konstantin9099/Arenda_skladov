using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Arenda_skladov
{
    public partial class Profil : Form
    {
        public int ID = 0;
        public Profil(int id_user)
        {
            InitializeComponent();
            ID = id_user;
            textBox1.Visible = false;
            textBox2.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
        }

        private void Profil_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //назад на форму Аренда складов.
        private void button2_Click(object sender, EventArgs e)
        {
            ArendaSkladov arendaSkladov = new ArendaSkladov(ID); // Обращение к форме "ArendaSkladov", на которую будет совершаться переход.
            arendaSkladov.Owner = this;
            this.Hide();
            arendaSkladov.Show(); // Запуск окна "ArendaSkladov".
        }

        //Изменение логина и пароля.
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Изменить")
            {
                label1.Visible = true;
                label2.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                button1.Text = "Сохранить";
            }
            else if (button1.Text == "Сохранить")
            {
                string query = "update avtorizaciya set login ='" + textBox1.Text + "', password ='" + textBox2.Text + "' where id_avtorizaciya = " + ID.ToString() + ";";
                MySqlConnection conn = DBUtils.GetDBConnection();
                MySqlCommand cmDB = new MySqlCommand(query, conn);
                try
                {
                    conn.Open();
                    cmDB.ExecuteReader();
                    conn.Close();
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    button1.Visible = false;
                    label1.Visible = false;
                    label2.Visible = false;
                    label3.Text = "Данные профиля изменены!";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Возникла непредвиденная ошибка!" + Environment.NewLine + ex.Message);
                }
            }
        }
    }
}
