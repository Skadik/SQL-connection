using MySql.Data.MySqlClient;
using SQL.Repository;
using SQL.Service.Data_Base;
using System.Windows.Forms;

namespace SQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ProductRepository pr = new ProductRepository();

            //create
            //pr.create("light","sonne",1111);

            //delete
            pr.delete(11);



            //MessageBox.Show(pr.getRangeID());
        }
    }
}
