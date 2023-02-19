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
    public partial class Nachisleniya : Form
    {
        public int ID = 0;
        public Nachisleniya(int id_user)
        {
            InitializeComponent();
            ID = id_user;
            Get_Info();
            this.MaximizeBox = false;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        }

        private void Nachisleniya_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void Get_Info()
        {
            string query = "SELECT nachislenie_id as 'Код начисления', data_nach as 'Дата начисления', period_nach 'Период оплаты', nomer_dog as 'Номер договора', naimenovanie_ar as 'Наименование арендатора', nomer_sk 'Номер склада', ploshchad_sk as 'Площадь склада', stoimost_ed as 'Стоисость 1 кв.м', stoimost_poln as 'Стоимость аренды за 1 мес.', summa_nach as 'Сумма начисления' FROM dogovory, sklady, arendatory,  nachisleniya where nachisleniya.nachislenie_id=dogovory.id_dogovor and dogovory.id_dogovor=arendatory.id_arendator and dogovory.id_dogovor=sklady.id_sklad; ";
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
                this.dataGridView1.Columns[2].Width = 120;
                this.dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[3].Width = 50;
                this.dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[4].Width = 200;
                this.dataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[5].Width = 50;
                this.dataGridView1.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[6].Width = 70;
                this.dataGridView1.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[7].Width = 70;
                this.dataGridView1.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[8].Width = 70;
                this.dataGridView1.Columns[9].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[9].Width = 90;
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

        //Переход на форму Добавить начисление.
        private void button1_Click(object sender, EventArgs e)
        {
            NachisleniyaDobavlenie nachisleniyaDobavlenie = new NachisleniyaDobavlenie(ID); // Обращение к форме "NachisleniyaDobavlenie", на которую будет совершаться переход.
            nachisleniyaDobavlenie.Owner = this;
            this.Hide();
            nachisleniyaDobavlenie.Show(); // Запуск окна "NachisleniyaDobavlenie".
        }

        //Переход на форму Изменить начисление.
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text == null || textBox2.Text == "" || textBox2.Text == null || textBox3.Text == "" || textBox3.Text == null || textBox9.Text == "" || textBox9.Text == null)
            {
                MessageBox.Show("Для изменения данных выберете строку в таблице начисленных оплат!", "Сообщение");
                return;
            }
            else
            {
                int id_nachis = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()); // Определяем id записи.
                Nachislenie.ID_nachis = id_nachis.ToString();
                Nachislenie.Dog_nachis = textBox3.Text;
                Nachislenie.Data_nachis = textBox1.Text;
                Nachislenie.Period_nachis = textBox2.Text;
                Nachislenie.Summa_nachis = textBox9.Text;
                NachisleniyaIzmenenie nachisleniyaIzmenenie = new NachisleniyaIzmenenie(ID); // Обращение к форме "NachisleniyaIzmenenie", на которую будет совершаться переход.
                nachisleniyaIzmenenie.Owner = this;
                this.Hide();
                nachisleniyaIzmenenie.Show(); // Запуск окна "NachisleniyaIzmenenie".
            }
        }

        //Выод данных в пол формы из строки, выбранной в таблице начисления оплат.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = ((DateTime)dataGridView1.CurrentRow.Cells[1].Value).ToString("yyyy-MM-dd");
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
        }
    }

    static class Nachislenie
    {
        public static string ID_nachis { get; set; }
        public static string Dog_nachis { get; set; }
        public static string Data_nachis { get; set; }
        public static string Period_nachis { get; set; }
        public static string Summa_nachis { get; set; }
    }
}
