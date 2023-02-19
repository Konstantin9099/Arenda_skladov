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
    public partial class Avtorizaciya : Form
    {
        public Avtorizaciya()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        }

        //Вход.
        private void button1_Click(object sender, EventArgs e)
        {
            // Запрос к таблице Delivery.
            string query = "SELECT id_avtorizaciya FROM avtorizaciya WHERE login ='" + textBox1.Text + "' and password = '" + textBox2.Text + "';";
            MySqlConnection conn = DBUtils.GetDBConnection();
            // Объект для выполнения SQL-запроса.
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            try
            {
                // Устанавливаем соединение с БД.
                conn.Open();
                int id_user = 0;
                id_user = Convert.ToInt32(cmDB.ExecuteScalar());
                if (id_user > 0)
                {
                    ArendaSkladov arenda = new ArendaSkladov(id_user); // Обращение к форме "Delivery", на которую будет совершаться переход.
                    arenda.Owner = this;
                    this.Hide();
                    arenda.Show(); // Запуск окна "Delivery".
                    textBox1.Clear(); // Очистка поля - логин.
                    textBox2.Clear(); // Очистка поля - пароль.
                }
                else
                {
                    MessageBox.Show("Возникла ошибка авторизации!");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!" + Environment.NewLine + ex.Message);
            }
        }

        //Выход.
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Переход к форме регистрации.
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registraciya reg = new Registraciya(); // Обращение к форме "Registraciya", на которую будет совершаться переход.
            reg.Owner = this;
            this.Hide();
            reg.Show(); // Запуск окна "Registraciya".
        }

        private void Avtorizaciya_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
