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
    public partial class SkladyDobavlenie : Form
    {
        public int ID = 0;
        public SkladyDobavlenie(int id_user)
        {
            InitializeComponent();
            ID = id_user;
            this.MaximizeBox = false;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        }

        private void SkladyDobavlenie_FormClosed(object sender, FormClosedEventArgs e)
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

        //Назад в склады.
        private void button2_Click(object sender, EventArgs e)
        {
            Sklady skl = new Sklady(ID); // Обращение к форме "Sklady", на которую будет совершаться переход.
            skl.Owner = this;
            this.Hide();
            skl.Show(); // Запуск окна "Sklady".
        }

        //Добавление склада.
        private void button1_Click(object sender, EventArgs e)
        {
            // Проверяем, чтобы были заполнены все поля.
            if (textBox1.Text == null || textBox1.Text == "" || textBox2.Text == null || textBox2.Text == "" || textBox3.Text == null || textBox3.Text == "" || textBox4.Text == null || textBox4.Text == "" || textBox5.Text == null || textBox5.Text == "" || textBox6.Text == null || textBox6.Text == "")
            {
                MessageBox.Show("Заполните все поля формы.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult res = MessageBox.Show("Добавить новый склад?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    try
                    {
                        CultureInfo userCulture = new CultureInfo("tr-TR");
                        string plosh = textBox4.Text;
                        string text4 = double.Parse(plosh, userCulture).ToString();
                        string stoim = textBox5.Text;
                        string text5 = double.Parse(stoim, userCulture).ToString();
                        string cena = textBox6.Text;
                        string text6 = double.Parse(cena, userCulture).ToString();
                        string query = "INSERT INTO sklady (adress_sk, nomer_sk, naznachenie_sk, ploshchad_sk, stoimost_ed, stoimost_poln) VALUES ('" + textBox2.Text + "', '" + textBox1.Text + "', '" + textBox3.Text + "', '" + text4 + "', '" + text5 + "', '" + text6 + "'); ";
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
                        MessageBox.Show("Склад добавлен!", "Операция выполнена успешно");
                    }
                    catch (NullReferenceException ex)
                    {
                        MessageBox.Show("Произошла ошибка!" + Environment.NewLine + ex.Message);
                    }
                }
            }
        }
    }
}
