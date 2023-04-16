using Microsoft.VisualBasic.Logging;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace Bilet1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            string mail = textBox3.Text;

            using (ConnectDB db = new ConnectDB())
            { 
                DataTable users = db.ExecuteSql($"select * from [USERS] where login = '{login}'");
                if (users.Rows.Count > 0) 
                {
                    MessageBox.Show("Пользователь с таким логином уже зарегистрирован!");
                }
                else
                {
                    db.ExecuteSqlNonQuery($"INSERT INTO USERS VALUES ('{login}', '{password}', '{mail}')");
                   
                    MailAddress from = new MailAddress("Rytrex@yandex.ru", "Дмитрий");
                    MailAddress to = new MailAddress($"{mail}");
                    MailMessage m = new MailMessage(from, to);

                    m.Subject = "Регистрация";
                    m.Body = $"<h2>Вы успешно зарегистрированы! Ваш логин -  {login} </h2>";
                    m.IsBodyHtml = true;

                    // Поставить галку "С сервера pop.yandex.ru по протоколу POP3" - https://mail.yandex.ru/?uid=460765826#setup/client
                    SmtpClient smtp = new SmtpClient("smtp.yandex.ru");
                    smtp.UseDefaultCredentials = false;
                    // Создать пароль для приложения здесь - https://id.yandex.ru/profile/apppasswords-list
                    smtp.Credentials = new NetworkCredential("Rytrex@yandex.ru", "lggumqihdvqnilhf");
                    smtp.EnableSsl = true;
                    smtp.Send(m);

                    MessageBox.Show($"Вы успешно зарегистрированы! Данные о вашем аккаунте высланы на почту {mail}");
                }    
            }
        }
    }
}