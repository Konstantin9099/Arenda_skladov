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
    public partial class ArendaSkladov : Form
    {
        public int ID = 0;
        public ArendaSkladov(int id_user)
        {
            InitializeComponent();
            ID = id_user;
            this.MaximizeBox = false;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        }

        private void ArendaSkladov_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //Склады.
        private void button1_Click(object sender, EventArgs e)
        {
            Sklady skl = new Sklady(ID); // Обращение к форме "Sklady", на которую будет совершаться переход.
            skl.Owner = this;
            this.Hide();
            skl.Show(); // Запуск окна "Sklady".
        }

        //Арендаторы.
        private void button2_Click(object sender, EventArgs e)
        {
            Arendatory arend = new Arendatory(ID); // Обращение к форме "Arendatory", на которую будет совершаться переход.
            arend.Owner = this;
            this.Hide();
            arend.Show(); // Запуск окна "Arendatory".
        }

        //Договоры.
        private void button3_Click(object sender, EventArgs e)
        {
            Dogovory dog = new Dogovory(ID); // Обращение к форме "Dogogvory", на которую будет совершаться переход.
            dog.Owner = this;
            this.Hide();
            dog.Show(); // Запуск окна "Dogogvory".
        }

        //Начисления.
        private void button4_Click(object sender, EventArgs e)
        {
            Nachisleniya nach = new Nachisleniya(ID); // Обращение к форме "Nachisleniya", на которую будет совершаться переход.
            nach.Owner = this;
            this.Hide();
            nach.Show(); // Запуск окна "Nachisleniya".
        }

        //Оплаты.
        private void button5_Click(object sender, EventArgs e)
        {
            Oplaty opl = new Oplaty(ID); // Обращение к форме "Oplaty", на которую будет совершаться переход.
            opl.Owner = this;
            this.Hide();
            opl.Show(); // Запуск окна "Oplaty".
        }

        //Профиль.
        private void button6_Click(object sender, EventArgs e)
        {
            Profil prof = new Profil(ID); // Обращение к форме "Profil", на которую будет совершаться переход.
            prof.Owner = this;
            this.Hide();
            prof.Show(); // Запуск окна "Profil".
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
