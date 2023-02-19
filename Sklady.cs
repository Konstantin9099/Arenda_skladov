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
    public partial class Sklady : Form
    {
        public int ID = 0;
        public Sklady(int id_user)
        {
            InitializeComponent();
            ID = id_user;
            Get_Info();
            this.MaximizeBox = false;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        }

        private void Sklady_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void Get_Info()
        {
            string query = "SELECT id_sklad as 'Код склада', nomer_sk as 'Номер склада', naznachenie_sk as 'Назначение склада', adress_sk as 'Адрес', ploshchad_sk as 'Площадь склада', stoimost_ed as 'Стоисость 1 кв.м', stoimost_poln as 'Стоимость аренды за 1 мес.' FROM sklady; ";
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlDataAdapter sda = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                dataGridView1.DataSource = dt;
                dataGridView1.ClearSelection();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.ClearSelection();
                this.dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[0].Width = 70;
                this.dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[1].Width = 70;
                this.dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[2].Width = 240;
                this.dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[3].Width = 200;
                this.dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[4].Width = 80;
                this.dataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[5].Width = 120;
                this.dataGridView1.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[6].Width = 120;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла непредвиденая ошибка!" + Environment.NewLine + ex.Message);
            }
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

        //Назад на форму Аренда складов.
        private void button3_Click(object sender, EventArgs e)
        {
            ArendaSkladov arendaSkladov = new ArendaSkladov(ID); // Обращение к форме "ArendaSkladov", на которую будет совершаться переход.
            arendaSkladov.Owner = this;
            this.Hide();
            arendaSkladov.Show(); // Запуск окна "ArendaSkladov".
        }

        //Переход на форму Добавить склад.
        private void button1_Click(object sender, EventArgs e)
        {
            SkladyDobavlenie skladyDobavlenie = new SkladyDobavlenie(ID); // Обращение к форме "SkladyDobavlenie", на которую будет совершаться переход.
            skladyDobavlenie.Owner = this;
            this.Hide();
            skladyDobavlenie.Show(); // Запуск окна "SkladyDobavlenie".
        }

        //Переход на форму Изменить данные склада.
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text == null || textBox2.Text == "" || textBox2.Text == null || textBox3.Text == "" || textBox3.Text == null || textBox4.Text == "" || textBox4.Text == null || textBox5.Text == "" || textBox5.Text == null || textBox6.Text == "" || textBox6.Text == null)
            {
                MessageBox.Show("Для изменения данных выберете строку в таблице складов!", "Сообщение");
                return;
            }
            else
            {
                int id_sk = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()); // Определяем id записи.
                //string idString = id_sk.ToString();
                DannyeSklasdov.ID_sk = id_sk.ToString();
                DannyeSklasdov.Nomer_sk = textBox1.Text;
                DannyeSklasdov.Adress_sk = textBox2.Text;
                DannyeSklasdov.Naznachenie_sk = textBox3.Text;
                DannyeSklasdov.Ploshchad_sk = textBox4.Text;
                DannyeSklasdov.Cena_sk = textBox5.Text;
                DannyeSklasdov.Stoimost_ar = textBox6.Text;
                SkladyIzmenenie skladyIzmenenie = new SkladyIzmenenie(ID); // Обращение к форме "SkladyIzmenenie", на которую будет совершаться переход.
                skladyIzmenenie.Owner = this;
                this.Hide();
                skladyIzmenenie.Show(); // Запуск окна "SkladyIzmenenie".
            }
        }

        //Вывод данных в поля формы из строки, выбранной в таблице.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }
    }

    static class DannyeSklasdov
    {
        public static string ID_sk { get; set; }
        public static string Nomer_sk { get; set; }
        public static string Adress_sk { get; set; }
        public static string Naznachenie_sk { get; set; }
        public static string Ploshchad_sk { get; set; }
        public static string Cena_sk { get; set; }
        public static string Stoimost_ar { get; set; }
    }
}
