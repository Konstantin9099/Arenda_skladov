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
    public partial class Registraciya : Form
    {
        public Registraciya()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
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

        //Запомнить.
        private void button1_Click(object sender, EventArgs e)
        {
            // Проверяем, чтобы были заполнены все поля.
            if (textBox1.Text == null || textBox1.Text == "" || textBox2.Text == null || textBox2.Text == "")
            {
                MessageBox.Show(
                    "Заполните все поля ввода данных.",
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult res = MessageBox.Show("Выполнить регистрацию в системе?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    try
                    {
                        string query = "INSERT INTO avtorizaciya (login, password) VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "'); ";
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
                    }
                    catch (NullReferenceException ex)
                    {
                        MessageBox.Show("Произошла непредвиденная ошибка!" + Environment.NewLine + ex.Message);
                    }
                }
                button1.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                label3.Text = "Вы зарегистрированы!";
            }
        }

        //Назад в Avtorizaciya.
        private void button2_Click(object sender, EventArgs e)
        {
            Avtorizaciya avt = new Avtorizaciya(); // Обращение к форме "Avtorizaciya", на которую будет совершаться переход.
            avt.Owner = this;
            this.Hide();
            avt.Show(); // Запуск окна "Avtorizaciya".
        }

        private void Registraciya_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
