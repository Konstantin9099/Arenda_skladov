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
    public partial class Oplaty : Form
    {
        public int ID = 0;
        public Oplaty(int id_user)
        {
            InitializeComponent();
            ID = id_user;
            Get_Info();
            this.MaximizeBox = false;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        }

        private void Oplaty_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void Get_Info()
        {
            string query = "SELECT id_oplata as 'Код оплаты', data_nach as 'Дата начисления', period_nach as 'Период оплаты', nomer_dog as 'Номер договора', nomer_sk as 'Номер склада', ploshchad_sk as 'Площадь склада', stoimost_ed as 'Цена за 1 м.кв.', naimenovanie_ar as 'Наименование арендатора', stoimost_poln as 'Сумма по договру', summa_nach as 'Сумма начисления', data_oplat 'Дата оплаты', summa_opl as 'Сумма оплаты' FROM dogovory, sklady, arendatory, nachisleniya, oplaty where dogovory.id_dogovor=sklady.id_sklad and dogovory.id_dogovor=arendatory.id_arendator and nachisleniya.nachislenie_id=dogovory.id_dogovor and oplaty.id_oplata=nachisleniya.nachislenie_id and oplaty.id_oplata=arendatory.id_arendator; ";
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
                this.dataGridView1.Columns[1].Width = 70;
                this.dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[2].Width = 120;
                this.dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[3].Width = 70;
                this.dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[4].Width = 70;
                this.dataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[5].Width = 70;
                this.dataGridView1.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[6].Width = 70;
                this.dataGridView1.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[7].Width = 180;
                this.dataGridView1.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[8].Width = 90;
                this.dataGridView1.Columns[9].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[9].Width = 90;
                this.dataGridView1.Columns[10].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[10].Width = 90;
                this.dataGridView1.Columns[11].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[11].Width = 90;
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

        //Переход на форму Добавление оплаты.
        private void button1_Click(object sender, EventArgs e)
        {
            OplatyDobavlenie oplatyDobavlenie = new OplatyDobavlenie(ID); // Обращение к форме "OplatyDobavlenie", на которую будет совершаться переход.
            oplatyDobavlenie.Owner = this;
            this.Hide();
            oplatyDobavlenie.Show(); // Запуск окна "OplatyDobavlenie".
        }

        //Переход на форму Изменение оплаты.
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text == null || textBox2.Text == "" || textBox2.Text == null || textBox3.Text == "" || textBox3.Text == null || textBox9.Text == "" || textBox9.Text == null)
            {
                MessageBox.Show("Для изменения данных выберете строку в таблице оплат!", "Сообщение");
                return;
            }
            else
            {
                int id_nachis = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()); // Определяем id записи.
                Oplata.ID_opl = id_nachis.ToString();
                Oplata.Dog_nachis = textBox3.Text;
                Oplata.Arendator_opl = textBox11.Text;
                Oplata.Nachis_opl = textBox8.Text;
                Oplata.Summa_opl = textBox10.Text;
                OplatyIzmenenie oplatyIzmenenie = new OplatyIzmenenie(ID); // Обращение к форме "OplatyIzmenenie", на которую будет совершаться переход.
                oplatyIzmenenie.Owner = this;
                this.Hide();
                oplatyIzmenenie.Show(); // Запуск окна "OplatyIzmenenie".
            }
        }

        //Заполнение полей формы данными из стрки, выбранной в таблице Оплаты (dataGridView1).
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = ((DateTime)dataGridView1.CurrentRow.Cells[1].Value).ToString("yyyy-MM-dd");
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            textBox9.Text = ((DateTime)dataGridView1.CurrentRow.Cells[10].Value).ToString("yyyy-MM-dd");
            textBox10.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            textBox11.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }

        // Удаление записи о выполненной арендной плате.
        private void button4_Click(object sender, EventArgs e)
        {
            // Проверка, что выбрана строка в таблице арендных платежей.
            if (dataGridView1.SelectedRows.Count < 1)
            {
                MessageBox.Show("Не выбрана строка в таблице арендных платежей!");
                return;
            }
            else
            {
                DialogResult res = MessageBox.Show("Удалить оплату?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    string valueCell = dataGridView1.CurrentCell.Value != null ? dataGridView1.CurrentCell.Value.ToString() : "";
                    string del = "DELETE FROM oplaty WHERE id_oplata = " + valueCell + ";";
                    Action(del);
                    Get_Info();
                }
                else
                {
                    MessageBox.Show("Удаление записи отменено!");
                }
                foreach (TextBox textBox in Controls.OfType<TextBox>())
                    textBox.Text = "";
            }
        }
    }

    static class Oplata
    {
        public static string ID_opl { get; set; }
        public static string Dog_nachis { get; set; }
        public static string Arendator_opl { get; set; }
        public static string Nachis_opl { get; set; }
        public static string Summa_opl { get; set; }
    }
}
