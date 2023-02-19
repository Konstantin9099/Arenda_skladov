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
    public partial class NachisleniyaIzmenenie : Form
    {
        public int ID = 0;
        public NachisleniyaIzmenenie(int id_user)
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

        private void NachisleniyaIzmenenie_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //Назад в Начисления.
        private void button2_Click(object sender, EventArgs e)
        {
            Nachisleniya nach = new Nachisleniya(ID); // Обращение к форме "Nachisleniya", на которую будет совершаться переход.
            nach.Owner = this;
            this.Hide();
            nach.Show(); // Запуск окна "Nachisleniya".
        }

        //Получаем данные в поля формы из формы Nachisleniya.
        private void NachisleniyaIzmenenie_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Text = Nachislenie.Data_nachis;
            textBox1.Text = Nachislenie.Period_nachis;
            textBox2.Text = Nachislenie.Dog_nachis;
            textBox3.Text = Nachislenie.Summa_nachis;
        }

        //Сохраняем изменения, внесенные в начисление.
        private void button1_Click(object sender, EventArgs e)
        {
            // Проверяем, чтобы были заполнены поля ввода/вывода данных.
            if (textBox1.Text == null || textBox1.Text == "" || textBox2.Text == null || textBox2.Text == "" || textBox3.Text == null || textBox3.Text == "")
            {
                MessageBox.Show("Должны быть заполнены все поля ввода!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult res = MessageBox.Show("Изменить данные начисления оплаты?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    CultureInfo userCulture = new CultureInfo("tr-TR");
                    string summa = textBox3.Text;
                    string text3 = double.Parse(summa, userCulture).ToString();
                    string date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    int id = Convert.ToInt32(Nachislenie.ID_nachis); // id начисления.
                    string query = "UPDATE nachisleniya SET dogovor_id=(select id_dogovor from dogovory where nomer_dog='" + textBox2.Text + "'), data_nach='" + date + "', period_nach='" + textBox1.Text + "', summa_nach='" + text3 + "' where nachislenie_id=" + id + "; ";
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
                    MessageBox.Show("Данные начисления оплаты изменены.", "Операция выполнена успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
