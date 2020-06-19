using System;
using System.Windows.Forms;

namespace TaursAndCows
{
    public partial class Form1 : Form
    {
        private Game _game;
        private int _length;
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar ==  (decimal) Keys.Back);
        }



        DateTime date1 = new DateTime(0, 0);
        private void timer1_Tick(object sender, EventArgs e)
        {
            date1 = date1.AddSeconds(1);
            label1.Text = date1.ToString("mm:ss");
        }
        public void newGameButton_Click(object sender, EventArgs e)
        {
            
            if (timer1.Enabled == true)
                timer1.Enabled = false;
            else
                timer1.Enabled = true;
            if (newGameButton.Text == "Остановить") newGameButton.Text = "Начать";
            else newGameButton.Text = "Остановить";
            if (newGameButton.Text == "Остановить")
            {
                
                _length = (int)numericUpDown1.Value;
                answerTextBox.Text = "";
                textBox1.MaxLength = _length;
                _game = new Game(_length);
                groupBox2.Visible = true;
                panel1.Visible = true;
            }
            else
            {
                label1.Text = "00:00";
                timer1.Stop();
                date1 = new DateTime(0, 0);
                _length = 0;
                groupBox2.Visible = false;
                panel1.Visible = false;
            }
        }

        private void tryButton_Click(object sender, EventArgs e)
        {
            var number = textBox1.Text;
            if (number.Length != _length)
            {
                MessageBox.Show("Длина должна быть равна "+ _length + "!");
                return;
            }
            string result;
            if (_game.Try(number, out result)) 
                GameOver();
            else 
                answerTextBox.Text += string.Format("Ваш ответ: {0} \r\n{1}",number,result);
        }

        private void GameOver()
        {
            groupBox2.Visible = false;
            MessageBox.Show("Вы угадали!" + "Затраченное время: " + label1.Text);
            timer1.Stop();
            groupBox2.Visible = false;
            panel1.Visible = false;
            newGameButton.Text = "Начать";
        }
    }
}
