using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms3_26
{

    public partial class MainForm : Form
    {
        private readonly OrderService orderService = new OrderService();
        private BindingSource ordersBindingSource = new BindingSource();
        private BindingSource detailsBindingSource = new BindingSource();

        public MainForm()
        {
            InitializeComponent();
            InitializeDataBinding();
            LoadData();
        }

        private void InitializeDataBinding()
        {
            // 主订单绑定
            ordersBindingSource.DataSource = typeof(List<Order>);
            dgvOrders.DataSource = ordersBindingSource;
            dgvOrders.AutoGenerateColumns = false;

            // 配置订单列
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "OrderId",
                HeaderText = "订单号"
            });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Customer",
                HeaderText = "客户"
            });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "OrderDate",
                HeaderText = "日期",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" }
            });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalAmount",
                HeaderText = "总金额",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            // 明细绑定
            detailsBindingSource.DataMember = "Details";
            detailsBindingSource.DataSource = ordersBindingSource;
            dgvDetails.DataSource = detailsBindingSource;
            dgvDetails.AutoGenerateColumns = false;

            // 配置明细列
            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductName",
                HeaderText = "商品名称"
            });
            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Price",
                HeaderText = "单价",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "数量"
            });
        }

        private void LoadData()
        {
            ordersBindingSource.DataSource = orderService.QueryOrders();
            detailsBindingSource.ResetBindings(false);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ordersBindingSource.DataSource = orderService.QueryOrders(txtKeyword.Text);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using var editForm = new EditOrderForm();
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                orderService.AddOrder(editForm.EditingOrder);
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (ordersBindingSource.Current is Order selectedOrder)
            {
                using var editForm = new EditOrderForm(selectedOrder.Clone());
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    orderService.UpdateOrder(editForm.EditingOrder);
                    LoadData();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ordersBindingSource.Current is Order selectedOrder)
            {
                if (MessageBox.Show("确认删除该订单？", "删除确认",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    orderService.RemoveOrder(selectedOrder.OrderId);
                    LoadData();
                }
            }
        }
    }
}
