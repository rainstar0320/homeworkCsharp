namespace WinForms2_26
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ������֤
            if (!double.TryParse(textBox1.Text, out double num1))
            {
                MessageBox.Show("��һ������������Ч��", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!double.TryParse(textBox2.Text, out double num2))
            {
                MessageBox.Show("�ڶ�������������Ч��", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("��ѡ���������", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ���м���
            string op = comboBox1.SelectedItem.ToString();
            double result = 0;

            try
            {
                switch (op)
                {
                    case "+":
                        result = num1 + num2;
                        break;
                    case "-":
                        result = num1 - num2;
                        break;
                    case "��":
                        result = num1 * num2;
                        break;
                    case "��":
                        if (num2 == 0)
                        {
                            throw new DivideByZeroException();
                        }
                        result = num1 / num2;
                        break;
                }

                textBox3.Text = result.ToString("0.####"); // ��ʽ����ʾ���
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("��������Ϊ�㣡", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Clear();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ��ʼ������������б�
            comboBox1.Items.AddRange(new object[] { "+", "-", "��", "��" });
            comboBox1.SelectedIndex = 0;
        }
    }
}
