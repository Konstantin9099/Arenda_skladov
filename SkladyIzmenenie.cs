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
using System.Globalization;

namespace Arenda_skladov
{
    public partial class SkladyIzmenenie : Form
    {
        public int ID = 0;
        public SkladyIzmenenie(int id_user)
        {
            InitializeComponent();
            ID = id_user;
            this.MaximizeBox = false;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
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

        private void SkladyIzmenenie_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //Назад в склады.
        private void button2_Click(object sender, EventArgs e)
        {
            Sklady skl = new Sklady(ID); // Обращение к форме "Sklady", на которую будет совершаться переход.
            skl.Owner = this;
            this.Hide();
            skl.Show(); // Запуск окна "Sklady".
        }

        //Сохранение изменений данных склада.
        private void button1_Click(object sender, EventArgs e)
        {
            // Проверяем, чтобы были заполнены поля ввода/вывода данных.
            if (textBox1.Text == null || textBox1.Text == "" || textBox2.Text == null || textBox2.Text == "" || textBox3.Text == null || textBox3.Text == "" || textBox4.Text == null || textBox4.Text == "" || textBox5.Text == null || textBox5.Text == "" || textBox6.Text == null || textBox6.Text == "")
            {
                MessageBox.Show("Должны быть заполнены все поля ввода!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult res = MessageBox.Show("Изменить данные склада?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    CultureInfo userCulture = new CultureInfo("tr-TR");
                    string plosh = textBox4.Text;
                    string text4 = double.Parse(plosh, userCulture).ToString();
                    string stoim = textBox5.Text;
                    string text5 = double.Parse(stoim, userCulture).ToString();
                    string cena = textBox6.Text;
                    string text6 = double.Parse(cena, userCulture).ToString();
                    int id = Convert.ToInt32(DannyeSklasdov.ID_sk); // id склада.
                    string query = "UPDATE sklady SET adress_sk='" + textBox2.Text + "', nomer_sk='" + textBox1.Text + "', naznachenie_sk='" + textBox3.Text + "', ploshchad_sk='" + text4 + "', stoimost_ed='" + text5 + "', stoimost_poln='" + text6 + "' WHERE id_sklad='" + id + "'; ";
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
                    MessageBox.Show("Данные склада изменены.", "Операция выполнена успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //Заполняем поля формы данными из формы Склады.
        private void SkladyIzmenenie_Load(object sender, EventArgs e)
        {
            textBox1.Text = DannyeSklasdov.Nomer_sk;
            textBox2.Text = DannyeSklasdov.Adress_sk;
            textBox3.Text = DannyeSklasdov.Naznachenie_sk;
            textBox4.Text = DannyeSklasdov.Ploshchad_sk;
            textBox5.Text = DannyeSklasdov.Cena_sk;
            textBox6.Text = DannyeSklasdov.Stoimost_ar;
        }
    }
}
