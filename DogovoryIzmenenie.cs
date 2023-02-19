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
    public partial class DogovoryIzmenenie : Form
    {
        public int ID = 0;
        public DogovoryIzmenenie(int id_user)
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

        private void DogovoryIzmenenie_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //Назад в Договоры.
        private void button2_Click(object sender, EventArgs e)
        {
            Dogovory dog = new Dogovory(ID); // Обращение к форме "Dogovory", на которую будет совершаться переход.
            dog.Owner = this;
            this.Hide();
            dog.Show(); // Запуск окна "Dogovory".
        }

        //Передаем в поля формы данные, полученные с формы Договоры.
        private void DogovoryIzmenenie_Load(object sender, EventArgs e)
        {
            textBox1.Text = DogovorIzmen.Nom_dog;
            textBox4.Text = DogovorIzmen.Nom_sk;
            dateTimePicker1.Text = DogovorIzmen.Data_nach;
            dateTimePicker2.Text = DogovorIzmen.Data_kon;
            comboBox1.Text = DogovorIzmen.Naimen_ar;
        }

        //Сохранение изменений.
        private void button1_Click(object sender, EventArgs e)
        {
            // Проверяем, чтобы были заполнены поля ввода/вывода данных.
            if (textBox1.Text == null || textBox1.Text == "" || textBox4.Text == null || textBox4.Text == "" || comboBox1.Text.Equals(""))
            {
                MessageBox.Show("Должны быть заполнены все поля ввода!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult res = MessageBox.Show("Изменить данные склада?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    string date1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    string date2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                    int id = Convert.ToInt32(DogovorIzmen.Dog_id); // id склада.
                    string query = "UPDATE dogovory SET nomer_dog='" + textBox1.Text + "', data1_dog='" + date1 + "', data2_dog='" + date2 + "', sklad_id=(select id_sklad from sklady where nomer_sk='" + textBox4.Text + "'), arendstor_id=(select id_arendator from arendatory where naimenovanie_ar='" + comboBox1.Text + "') where id_dogovor=" + id + "; ";
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
    }
}
