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
    public partial class Arendatory : Form
    {
        public int ID = 0;
        public Arendatory(int id_user)
        {
            InitializeComponent();
            ID = id_user;
            Get_Info();
            this.MaximizeBox = false;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        }

        private void Arendatory_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void Get_Info()
        {
            string query = "SELECT id_arendator as 'Код арендатора', naimenovanie_ar 'Наименование арендатора', adress_ar 'Адрес', rukovoditel_ar 'ФИО руководителя', telefon_ar 'Номер телефона', email_ar 'Адрес почты' FROM arendatory; ";
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
                this.dataGridView1.Columns[1].Width = 180;
                this.dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[2].Width = 240;
                this.dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[3].Width = 160;
                this.dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[4].Width = 80;
                this.dataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[5].Width = 120;
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


        //Переход на форму Добавления арендатора.
        private void button1_Click(object sender, EventArgs e)
        {
            ArendatoryDobavlenie arendatoryDobavlenie = new ArendatoryDobavlenie(ID); // Обращение к форме "ArendatoryDobavlenie", на которую будет совершаться переход.
            arendatoryDobavlenie.Owner = this;
            this.Hide();
            arendatoryDobavlenie.Show(); // Запуск окна "ArendatoryDobavlenie".
        }

        //Переход на форму изменения данных арендатора.
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text == null || textBox2.Text == "" || textBox2.Text == null)
            {
                MessageBox.Show("Для изменения данных выберете строку в таблице арендаторов!", "Сообщение");
                return;
            }
            else
            {
                int id_arend = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()); // Определяем id записи.
                string idString = id_arend.ToString();
                DannyeArendatorov.ID_arend = idString;
                DannyeArendatorov.Naimenovanie = textBox1.Text;
                DannyeArendatorov.Adress = textBox3.Text;
                DannyeArendatorov.FIO = textBox2.Text;
                DannyeArendatorov.Telefon = textBox4.Text;
                DannyeArendatorov.Pochta = textBox5.Text;
                ArendatoryIzmenenie arendatoryIzmenenie = new ArendatoryIzmenenie(ID); // Обращение к форме "ArendatoryIzmenenie", на которую будет совершаться переход.
                arendatoryIzmenenie.Owner = this;
                this.Hide();
                arendatoryIzmenenie.Show(); // Запуск окна "ArendatoryIzmenenie".
            }
        }

        //Строка поиска.
        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox6.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
            }
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                DGVPrinter print = new DGVPrinter(); print.Title = "Реестр арендаторов"; print.SubTitle = "Дата формирования списка: " + DateTime.Now.ToShortDateString(); print.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip; print.PageNumbers = true; print.PageNumberInHeader = false; print.PorportionalColumns = true; print.HeaderCellAlignment = StringAlignment.Near;
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

        //Назад на форму Аренда складов
        private void button5_Click(object sender, EventArgs e)
        {
            ArendaSkladov arendaSkladov = new ArendaSkladov(ID); // Обращение к форме "ArendaSkladov", на которую будет совершаться переход.
            arendaSkladov.Owner = this;
            this.Hide();
            arendaSkladov.Show(); // Запуск окна "ArendaSkladov".
        }

        //Вывод данных в поля формы из строки, выбранной в таблице.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }
    }
    static class DannyeArendatorov
    {
        public static string ID_arend { get; set; }
        public static string Naimenovanie { get; set; }
        public static string Adress { get; set; }
        public static string FIO { get; set; }
        public static string Telefon { get; set; }
        public static string Pochta { get; set; }
    }
}
