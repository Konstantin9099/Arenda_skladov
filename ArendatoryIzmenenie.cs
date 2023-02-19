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
    public partial class ArendatoryIzmenenie : Form
    {
        public int ID = 0;
        public ArendatoryIzmenenie(int id_user)
        {
            InitializeComponent();
            ID = id_user;
        }

        //Функция, позволяющая отправить команду на сервер БД для оптимизации кода.
        public void Action(string query)
        {
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            try
            {
                conn.Open();
                cmDB.ExecuteReader();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла непредвиденная ошибка!" + Environment.NewLine + ex.Message);
            }
        }

        private void ArendatoryIzmenenie_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //Назад в Аренду.
        private void button2_Click(object sender, EventArgs e)
        {
            Arendatory arend = new Arendatory(ID); // Обращение к форме "Arendatory", на которую будет совершаться переход.
            arend.Owner = this;
            this.Hide();
            arend.Show(); // Запуск окна "Arendatory".
        }
        // Заполняем поля формы данными, полученными с формы Арендаторы.
        private void ArendatoryIzmenenie_Load(object sender, EventArgs e)
        {
            textBox1.Text = DannyeArendatorov.Naimenovanie;
            textBox2.Text = DannyeArendatorov.FIO;
            textBox3.Text = DannyeArendatorov.Adress;
            textBox4.Text = DannyeArendatorov.Telefon;
            textBox5.Text = DannyeArendatorov.Pochta;
        }
        //Изменяем и сохраняем данные арендатора.
        private void button1_Click(object sender, EventArgs e)
        {
            // Проверяем, чтобы были заполнены поля ввода/вывода данных.
            if (textBox1.Text == null || textBox1.Text == "" || textBox2.Text == null || textBox2.Text == "" || textBox3.Text == null || textBox3.Text == "" || textBox4.Text == null || textBox4.Text == "" || textBox5.Text == null || textBox5.Text == "")
            {
                MessageBox.Show("Должны быть заполнены все поля ввода!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult res = MessageBox.Show("Изменить данные арендатора?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(DannyeArendatorov.ID_arend);
                    string query = "UPDATE arendatory SET naimenovanie_ar='" + textBox1.Text + "', adress_ar='" + textBox3.Text + "', rukovoditel_ar='" + textBox2.Text + "', telefon_ar='" + textBox4.Text + "', email_ar='" + textBox5.Text + "' WHERE id_arendator='" + id + "'; ";
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
                    MessageBox.Show("Данные арендатора изменены.", "Операция выполнена успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
