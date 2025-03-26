using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms3_26
{

    public partial class EditOrderForm : Form
    {
        public Order EditingOrder { get; private set; }
        private BindingSource detailsBindingSource = new BindingSource();

        public EditOrderForm(Order order = null)
        {
            InitializeComponent();
            EditingOrder = order ?? new Order();
            InitializeDataBinding();
            SetupValidation();
        }

        private void InitializeDataBinding()
        {
            // 主订单绑定
            txtOrderId.DataBindings.Add("Text", EditingOrder, "OrderId", true, DataSourceUpdateMode.OnValidation);
            txtCustomer.DataBindings.Add("Text", EditingOrder, "Customer", true, DataSourceUpdateMode.OnValidation);

            // 明细绑定
            detailsBindingSource.DataSource = EditingOrder.Details;
            dgvDetails.DataSource = detailsBindingSource;
        }

        private void SetupValidation()
        {
            txtCustomer.Validating += (s, e) =>
                e.Cancel = string.IsNullOrWhiteSpace(txtCustomer.Text);

            errorProvider.SetIconAlignment(txtCustomer, ErrorIconAlignment.MiddleRight);
        }

        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            var detail = new OrderDetail();
            using var detailForm = new DetailEditForm(detail);
            if (detailForm.ShowDialog() == DialogResult.OK)
            {
                EditingOrder.Details.Add(detail);
                detailsBindingSource.ResetBindings(false);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateChildren() && EditingOrder.Details.Count > 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("请填写完整订单信息");
            }
        }
    }
}
