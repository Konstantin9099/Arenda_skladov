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
    public partial class OplatyIzmenenie : Form
    {
        public int ID = 0;
        public OplatyIzmenenie(int id_user)
        {
            InitializeComponent();
            ID = id_user;
            Get_Info();
            this.MaximizeBox = false;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        }

        private void OplatyIzmenenie_FormClosed(object sender, FormClosedEventArgs e)
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

        public void Get_Info()
        {
            string query = "SELECT nachislenie_id as 'Код начисления', data_nach as 'Дата начисления', period_nach 'Период оплаты', naimenovanie_ar as 'Наименование арендатора', summa_nach as 'Сумма начисления' FROM dogovory, sklady, arendatory,  nachisleniya where nachisleniya.nachislenie_id=dogovory.id_dogovor and dogovory.id_dogovor=arendatory.id_arendator and dogovory.id_dogovor=sklady.id_sklad; ";
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlDataAdapter sda = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.ClearSelection();
                this.dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[0].Width = 70;
                this.dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[1].Width = 100;
                this.dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[2].Width = 120;
                this.dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[3].Width = 200;
                this.dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[4].Width = 120;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла непредвиденая ошибка!" + Environment.NewLine + ex.Message);
            }
        }

        //Назад в Оплаты.
        private void button2_Click(object sender, EventArgs e)
        {
            Oplaty opl = new Oplaty(ID); // Обращение к форме "Oplaty", на которую будет совершаться переход.
            opl.Owner = this;
            this.Hide();
            opl.Show(); // Запуск окна "Oplaty".
        }

        //Фильтрация начислений по арендатору.
        private void button3_Click(object sender, EventArgs e)
        {
            string query = "SELECT nachislenie_id as 'Код начисления', data_nach as 'Дата начисления', period_nach 'Период оплаты', naimenovanie_ar as 'Наименование арендатора', summa_nach as 'Сумма начисления' FROM dogovory, sklady, arendatory,  nachisleniya where nachisleniya.nachislenie_id=dogovory.id_dogovor and dogovory.id_dogovor=arendatory.id_arendator and dogovory.id_dogovor=sklady.id_sklad and naimenovanie_ar='" + comboBox1.Text + "'; ";
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlDataAdapter sda = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.ClearSelection();
                this.dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[0].Width = 70;
                this.dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[1].Width = 100;
                this.dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[2].Width = 120;
                this.dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[3].Width = 200;
                this.dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[4].Width = 120;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла непредвиденая ошибка!" + Environment.NewLine + ex.Message);
            }
        }

        //Сброс фильтрации.
        private void button4_Click(object sender, EventArgs e)
        {
            Get_Info();
        }

        //Сохранение изменений арендной платы.
        private void button1_Click(object sender, EventArgs e)
        {
            // Проверяем, чтобы были заполнены поля ввода/вывода данных.
            if (textBox1.Text == null || textBox1.Text == "" || textBox2.Text == null || textBox2.Text == "" || comboBox1.Text.Equals(""))
            {
                MessageBox.Show("Должны быть заполнены все поля ввода!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult res = MessageBox.Show("Изменить данные оплаты?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    CultureInfo userCulture = new CultureInfo("tr-TR");
                    string summa = textBox2.Text;
                    string text2 = double.Parse(summa, userCulture).ToString();
                    string date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    int id = Convert.ToInt32(Oplata.ID_opl); // id оплаты.
                    string query = "UPDATE oplaty SET nachislenie_id='" + textBox1.Text + "', arendator_id=(select id_arendator from arendatory where naimenovanie_ar='" + comboBox1.Text + "'), data_oplat='" + date + "', summa_opl='" + text2 + "' where id_oplata=" + id + "; ";
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
                    MessageBox.Show("Данные оплаты изменены.", "Операция выполнена успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //Заполняем поля формы данными, олученными с формы "Оплаты".
        private void OplatyIzmenenie_Load(object sender, EventArgs e)
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
            textBox1.Text = Oplata.Dog_nachis;
            comboBox1.Text = Oplata.Arendator_opl;
            textBox2.Text = Oplata.Summa_opl;
        }
    }
}
