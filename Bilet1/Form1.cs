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
                    MessageBox.Show("������������ � ����� ������� ��� ���������������!");
                }
                else
                {
                    db.ExecuteSqlNonQuery($"INSERT INTO USERS VALUES ('{login}', '{password}', '{mail}')");
                   
                    MailAddress from = new MailAddress("Rytrex@yandex.ru", "�������");
                    MailAddress to = new MailAddress($"{mail}");
                    MailMessage m = new MailMessage(from, to);

                    m.Subject = "�����������";
                    m.Body = $"<h2>�� ������� ����������������! ��� ����� -  {login} </h2>";
                    m.IsBodyHtml = true;

                    // ��������� ����� "� ������� pop.yandex.ru �� ��������� POP3" - https://mail.yandex.ru/?uid=460765826#setup/client
                    SmtpClient smtp = new SmtpClient("smtp.yandex.ru");
                    smtp.UseDefaultCredentials = false;
                    // ������� ������ ��� ���������� ����� - https://id.yandex.ru/profile/apppasswords-list
                    smtp.Credentials = new NetworkCredential("Rytrex@yandex.ru", "lggumqihdvqnilhf");
                    smtp.EnableSsl = true;
                    smtp.Send(m);

                    MessageBox.Show($"�� ������� ����������������! ������ � ����� �������� ������� �� ����� {mail}");
                }    
            }
        }
    }
}