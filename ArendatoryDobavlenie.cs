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
    public partial class ArendatoryDobavlenie : Form
    {
        public int ID = 0;
        public ArendatoryDobavlenie(int id_user)
        {
            InitializeComponent();
            ID = id_user;
        }

        private void ArendatoryDobavlenie_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        void Action(string query)
        {
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            try
            {
                conn.Open();
                MySqlDataReader rd = cmDB.ExecuteReader();
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла непредвиденная ошибка!");
            }
        }

        //Назад в Аренду.
        private void button2_Click(object sender, EventArgs e)
        {
            Arendatory arend = new Arendatory(ID); // Обращение к форме "Arendatory", на которую будет совершаться переход.
            arend.Owner = this;
            this.Hide();
            arend.Show(); // Запуск окна "Arendatory".
        }

        //Добавление арендатора.
        private void button1_Click(object sender, EventArgs e)
        {
            // Проверяем, чтобы были заполнены все поля.
            if (textBox1.Text == null || textBox1.Text == "" || textBox2.Text == null || textBox2.Text == "" || textBox3.Text == null || textBox3.Text == "" || textBox4.Text == null || textBox4.Text == "" || textBox5.Text == null || textBox5.Text == "")
            {
                MessageBox.Show( "Заполните все поля формы.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult res = MessageBox.Show("Добавить данные о заказе?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    try
                    {
                        string query = "INSERT INTO arendatory (naimenovanie_ar, adress_ar, rukovoditel_ar, telefon_ar, email_ar) VALUES ('" + textBox1.Text + "', '" + textBox3.Text + "', '" + textBox2.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "'); ";
                        MySqlConnection conn = DBUtils.GetDBConnection();
                        MySqlCommand cmDB = new MySqlCommand(query, conn);
                        try
                        {
                            conn.Open();
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Произошла непредвиденная ошибка!" + Environment.NewLine + ex.Message);
                        }
                        Action(query);
                        MessageBox.Show("Данные арендатора записаны.", "Операция выполнена успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (NullReferenceException ex)
                    {
                        MessageBox.Show("Произошла непредвиденная ошибка!" + Environment.NewLine + ex.Message);
                    }
                }
            }
        }
    }
}
