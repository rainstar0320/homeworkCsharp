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
            // 输入验证
            if (!double.TryParse(textBox1.Text, out double num1))
            {
                MessageBox.Show("第一个数字输入无效！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!double.TryParse(textBox2.Text, out double num2))
            {
                MessageBox.Show("第二个数字输入无效！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("请选择运算符！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 进行计算
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
                    case "×":
                        result = num1 * num2;
                        break;
                    case "÷":
                        if (num2 == 0)
                        {
                            throw new DivideByZeroException();
                        }
                        result = num1 / num2;
                        break;
                }

                textBox3.Text = result.ToString("0.####"); // 格式化显示结果
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("除数不能为零！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Clear();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 初始化运算符下拉列表
            comboBox1.Items.AddRange(new object[] { "+", "-", "×", "÷" });
            comboBox1.SelectedIndex = 0;
        }
    }
}
