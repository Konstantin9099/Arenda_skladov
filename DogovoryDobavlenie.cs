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
    public partial class DogovoryDobavlenie : Form
    {
        public int ID = 0;
        public DogovoryDobavlenie(int id_user)
        {
            InitializeComponent();
            ID = id_user;
            this.MaximizeBox = false;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        }

        private void DogovoryDobavlenie_FormClosed(object sender, FormClosedEventArgs e)
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

        //Назад в Договоры.
        private void button2_Click(object sender, EventArgs e)
        {
            Dogovory dog = new Dogovory(ID); // Обращение к форме "Dogovory", на которую будет совершаться переход.
            dog.Owner = this;
            this.Hide();
            dog.Show(); // Запуск окна "Dogovory".
        }

        // Добавление договора.
        private void button1_Click(object sender, EventArgs e)
        {
            // Проверяем, чтобы были заполнены все поля.
            if (textBox1.Text == null || textBox1.Text == "" || textBox2.Text == null || textBox2.Text == "" || comboBox1.Text.Equals(""))
            {
                MessageBox.Show("Заполните все поля формы.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult res = MessageBox.Show("Создать договор?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    try
                    {
                        string date1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                        string date2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                        string query = "INSERT INTO dogovory (nomer_dog, data1_dog, data2_dog, sklad_id, arendstor_id) VALUES ('" + textBox1.Text + "', '" + date1 + "', '" + date2 + "', (select id_sklad from sklady where nomer_sk='" + textBox2.Text + "'), (select id_arendator from arendatory where naimenovanie_ar='" + comboBox1.Text + "')); ";
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
                        MessageBox.Show("Договор создан!", "Операция выполнена успешно");
                    }
                    catch (NullReferenceException ex)
                    {
                        MessageBox.Show("Произошла ошибка!" + Environment.NewLine + ex.Message);
                    }
                }
            }
        }

        //Выпадающий список арендаторов (наименования организаций).
        private void DogovoryDobavlenie_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT naimenovanie_ar FROM arendatory ORDER BY naimenovanie_ar;";
                MySqlConnection conn = DBUtils.GetDBConnection();
                conn.Open();
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["naimenovanie_ar"].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
