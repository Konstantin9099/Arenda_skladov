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
using System.Drawing.Printing;
using DGVPrinterHelper;

namespace Arenda_skladov
{
    public partial class Dogovory : Form
    {
        public int ID = 0;
        public Dogovory(int id_user)
        {
            InitializeComponent();
            ID = id_user;
            Get_Info();
            this.MaximizeBox = false;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        }

        public void Get_Info()
        {
            string query = "SELECT id_dogovor as 'Код договора', nomer_dog as 'Номер договора', data1_dog as 'Дата начала', data2_dog 'Дата окончания', nomer_sk as 'Номер склада', adress_sk as 'Адрес склада', naznachenie_sk as 'Назначение склада', ploshchad_sk as 'Площадь склада', stoimost_ed as 'Стоисость 1 кв.м', stoimost_poln as 'Стоимость аренды за 1 мес.', naimenovanie_ar as 'Наименование арендатора', rukovoditel_ar as 'ФИО руководителя', adress_ar as 'Юридический адрес', telefon_ar as 'Номер телефона', email_ar as 'Электронная почта' FROM dogovory, sklady, arendatory where dogovory.id_dogovor=arendatory.id_arendator and dogovory.id_dogovor=sklady.id_sklad; ";
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
                this.dataGridView1.Columns[2].Width = 70;
                this.dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[3].Width = 70;
                this.dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[4].Width = 50;
                this.dataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[5].Width = 220;
                this.dataGridView1.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[6].Width = 150;
                this.dataGridView1.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[7].Width = 70;
                this.dataGridView1.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[8].Width = 70;
                this.dataGridView1.Columns[9].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[9].Width = 120;
                this.dataGridView1.Columns[10].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[10].Width = 185;
                this.dataGridView1.Columns[11].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[11].Width = 150;
                this.dataGridView1.Columns[12].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[12].Width = 230;
                this.dataGridView1.Columns[13].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[13].Width = 90;
                this.dataGridView1.Columns[14].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[14].Width = 140;
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

        private void Dogogvory_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //Назад на форму Аренда складов.
        private void button3_Click(object sender, EventArgs e)
        {
            ArendaSkladov arendaSkladov = new ArendaSkladov(ID); // Обращение к форме "ArendaSkladov", на которую будет совершаться переход.
            arendaSkladov.Owner = this;
            this.Hide();
            arendaSkladov.Show(); // Запуск окна "ArendaSkladov".
        }

        //Переход на форму Добавить договор.
        private void button1_Click(object sender, EventArgs e)
        {
            DogovoryDobavlenie dogovoryDobavlenie = new DogovoryDobavlenie(ID); // Обращение к форме "DogovoryDobavlenie", на которую будет совершаться переход.
            dogovoryDobavlenie.Owner = this;
            this.Hide();
            dogovoryDobavlenie.Show(); // Запуск окна "DogovoryDobavlenie".
        }

        //Переход на форму Изменить договор.
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text == null || textBox2.Text == "" || textBox2.Text == null || textBox3.Text == "" || textBox3.Text == null || textBox4.Text == "" || textBox4.Text == null || textBox1.Text == "" || textBox1.Text == null)
            {
                MessageBox.Show("Для изменения данных выберете строку в таблице складов!", "Сообщение");
                return;
            }
            else
            {
                int id_dog = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()); // Определяем id записи.
              //string idString = id_dog.ToString();
                DogovorIzmen.Dog_id = id_dog.ToString();
                DogovorIzmen.Nom_dog = textBox1.Text;
                DogovorIzmen.Nom_sk = textBox4.Text;
                DogovorIzmen.Data_nach = textBox2.Text;
                DogovorIzmen.Data_kon = textBox3.Text;
                DogovorIzmen.Naimen_ar = textBox11.Text;
                DogovoryIzmenenie dogovoryIzmenenie = new DogovoryIzmenenie(ID); // Обращение к форме "DogovoryIzmenenie", на которую будет совершаться переход.
                dogovoryIzmenenie.Owner = this;
                this.Hide();
                dogovoryIzmenenie.Show(); // Запуск окна "DogovoryIzmenenie".
            }
        }

        // Вывод данных в поля формы при выборе строки в таблице договоров.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = ((DateTime)dataGridView1.CurrentRow.Cells[2].Value).ToString("yyyy-MM-dd");
            textBox3.Text = ((DateTime)dataGridView1.CurrentRow.Cells[3].Value).ToString("yyyy-MM-dd");
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            textBox10.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            textBox11.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            textBox12.Text = dataGridView1.CurrentRow.Cells[14].Value.ToString();
            textBox13.Text = dataGridView1.CurrentRow.Cells[13].Value.ToString();
            textBox14.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
            //var dat = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime dt1 = DateTime.Parse(textBox3.Text);
            DateTime dt2 = DateTime.Now;
            if (dt1 > dt2)
            {
                label15.Text = "Действующий";
            }
            else
            {
                label15.Text = "Закончился";
            }
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                DGVPrinter print = new DGVPrinter(); print.Title = "Реестр договоров"; print.SubTitle = "Дата формирования списка: " + DateTime.Now.ToShortDateString(); print.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip; print.PageNumbers = true; print.PageNumberInHeader = false; print.PorportionalColumns = true; print.HeaderCellAlignment = StringAlignment.Near;
                print.Footer = "Аренда складов";
                print.FooterSpacing = 15;
                print.PrintDataGridView(dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Bitmap bmp = new Bitmap(dataGridView1.Size.Width, dataGridView1.Size.Height);
            dataGridView1.DrawToBitmap(bmp, dataGridView1.Bounds);
            e.Graphics.DrawImage(bmp, 0, 0);
        }

        //Печать.
        private void button4_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDialog.Document.Print();
            }
        }

        //Строка поиска.
        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox15.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
            }
        }

        //Сортировка - сначала новые
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
        }
         // Сортировка - сначала старые
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
        }

        //Фильтрация договоров
        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "по ФИО арендаторов")
            {
                string query = "SELECT id_dogovor as 'Код договора', nomer_dog as 'Номер договора', data1_dog as 'Дата начала', data2_dog 'Дата окончания', nomer_sk as 'Номер склада', adress_sk as 'Адрес склада', naznachenie_sk as 'Назначение склада', ploshchad_sk as 'Площадь склада', stoimost_ed as 'Стоисость 1 кв.м', stoimost_poln as 'Стоимость аренды за 1 мес.', naimenovanie_ar as 'Наименование арендатора', rukovoditel_ar as 'ФИО руководителя', adress_ar as 'Юридический адрес', telefon_ar as 'Номер телефона', email_ar as 'Электронная почта' FROM dogovory, sklady, arendatory where dogovory.id_dogovor=arendatory.id_arendator and dogovory.id_dogovor=sklady.id_sklad ORDER BY rukovoditel_ar; ";
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
                    this.dataGridView1.Columns[2].Width = 70;
                    this.dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[3].Width = 70;
                    this.dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[4].Width = 50;
                    this.dataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[5].Width = 220;
                    this.dataGridView1.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[6].Width = 150;
                    this.dataGridView1.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[7].Width = 70;
                    this.dataGridView1.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[8].Width = 70;
                    this.dataGridView1.Columns[9].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[9].Width = 120;
                    this.dataGridView1.Columns[10].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[10].Width = 185;
                    this.dataGridView1.Columns[11].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[11].Width = 150;
                    this.dataGridView1.Columns[12].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[12].Width = 230;
                    this.dataGridView1.Columns[13].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[13].Width = 90;
                    this.dataGridView1.Columns[14].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[14].Width = 140;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла непредвиденая ошибка!" + Environment.NewLine + ex.Message);
                }
            }
            else if (comboBox1.Text == "по площади складов")
            {
                string query = "SELECT id_dogovor as 'Код договора', nomer_dog as 'Номер договора', data1_dog as 'Дата начала', data2_dog 'Дата окончания', nomer_sk as 'Номер склада', adress_sk as 'Адрес склада', naznachenie_sk as 'Назначение склада', ploshchad_sk as 'Площадь склада', stoimost_ed as 'Стоисость 1 кв.м', stoimost_poln as 'Стоимость аренды за 1 мес.', naimenovanie_ar as 'Наименование арендатора', rukovoditel_ar as 'ФИО руководителя', adress_ar as 'Юридический адрес', telefon_ar as 'Номер телефона', email_ar as 'Электронная почта' FROM dogovory, sklady, arendatory where dogovory.id_dogovor=arendatory.id_arendator and dogovory.id_dogovor=sklady.id_sklad ORDER BY ploshchad_sk; ";
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
                    this.dataGridView1.Columns[2].Width = 70;
                    this.dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[3].Width = 70;
                    this.dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[4].Width = 50;
                    this.dataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[5].Width = 220;
                    this.dataGridView1.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[6].Width = 150;
                    this.dataGridView1.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[7].Width = 70;
                    this.dataGridView1.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[8].Width = 70;
                    this.dataGridView1.Columns[9].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[9].Width = 120;
                    this.dataGridView1.Columns[10].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[10].Width = 185;
                    this.dataGridView1.Columns[11].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[11].Width = 150;
                    this.dataGridView1.Columns[12].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[12].Width = 230;
                    this.dataGridView1.Columns[13].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[13].Width = 90;
                    this.dataGridView1.Columns[14].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[14].Width = 140;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла непредвиденая ошибка!" + Environment.NewLine + ex.Message);
                }
            }
        }

        //Сброс фильтрации.
        private void button7_Click(object sender, EventArgs e)
        {
            Get_Info();
        }
    }

    static class DogovorIzmen
    {
        public static string Nom_dog { get; set; }
        public static string Nom_sk { get; set; }
        public static string Data_nach { get; set; }
        public static string Data_kon { get; set; }
        public static string Naimen_ar { get; set; }
        public static string Dog_id { get; set; }
    }
}
