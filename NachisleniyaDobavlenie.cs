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
    public partial class NachisleniyaDobavlenie : Form
    {
        public int ID = 0;
        public NachisleniyaDobavlenie(int id_user)
        {
            InitializeComponent();
            ID = id_user;
            this.MaximizeBox = false;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        }

        private void NachisleniyaDobavlenie_FormClosed(object sender, FormClosedEventArgs e)
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

        //Назад в Начисления.
        private void button2_Click(object sender, EventArgs e)
        {
            Nachisleniya nach = new Nachisleniya(ID); // Обращение к форме "Nachisleniya", на которую будет совершаться переход.
            nach.Owner = this;
            this.Hide();
            nach.Show(); // Запуск окна "Nachisleniya".
        }

        //Начисление оплаты.
        private void button1_Click(object sender, EventArgs e)
        {
            // Проверяем, чтобы были заполнены все поля.
            if (textBox1.Text == null || textBox1.Text == "" || textBox2.Text == null || textBox2.Text == "")
            {
                MessageBox.Show("Заполните все поля формы.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult res = MessageBox.Show("Начислить оплату?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    try
                    {
                        CultureInfo userCulture = new CultureInfo("tr-TR");
                        string plosh = textBox3.Text;
                        string text3 = double.Parse(plosh, userCulture).ToString();
                        string date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                        string query = "INSERT INTO nachisleniya (dogovor_id, data_nach, period_nach, summa_nach) VALUES ((select id_dogovor from dogovory where nomer_dog='" + textBox2.Text + "'), '" + date + "', '" + textBox1.Text + "', '" + text3 + "'); ";
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
                        MessageBox.Show("Оплата по договору начислена!", "Операция выполнена успешно");
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
