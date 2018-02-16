using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace MyWindowsApp
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {

        }

        private void Btn_Run_01_Click(object sender, EventArgs e)
        {
            Scheduling_Run_01 run_Code = new Scheduling_Run_01();
            run_Code.ScheduleRun();
        }

        private void Btn_Requirement_Click(object sender, EventArgs e)
        {
            Requirements run_Code = new Requirements();
            run_Code.Requirement_Run(Start_Date.Value.ToString("yyyy/MM/dd"));
        }

        private void Btn_Test_Click(object sender, EventArgs e)
        {
 
            string start_Date = Start_Date.Value.ToString("yyyy/MM/dd");

            DB_SQL_Connection newConn;
            //Requirements_Functions reqFuntion;
            DataSet ds3;
            DataRow dRow3;
            int MaxRows3;
            int inc3 = 0;

            // Open Staff Groups as a dataset
            newConn = new DB_SQL_Connection();
            newConn.SqlQuery = "SELECT * FROM [Allocation] WHERE [aDate] = '" + start_Date + "'";
            ds3 = newConn.GetConnection;
            MaxRows3 = ds3.Tables[0].Rows.Count;
            inc3 = 0;
            dRow3 = ds3.Tables[0].Rows[inc3];

            //string output = JsonConvert.SerializeObject(ds3);
            Console.WriteLine("Starting");
            JsonSerializer serializer = new JsonSerializer();
            
            serializer.NullValueHandling = NullValueHandling.Ignore;
            
            using(StreamWriter sw = new StreamWriter(@"C:\Users\Andy\json.txt"))
            using(JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, ds3);
            }
            Console.WriteLine("Finished");
            Console.WriteLine("Press Enter when ready to unpack:");
            Console.ReadLine();

            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(File.ReadAllText(@"C:\Users\Andy\json.txt"));

            DataTable dataTable = dataSet.Tables["Table1"];
            //Console.WriteLine(dataTable.Rows.Count);

            int  MaxRows4 = dataSet.Tables[0].Rows.Count;
            int inc4 = 0;
            DataRow dRow4;

            while (inc4 < MaxRows4)
            {
                dRow4 = dataSet.Tables[0].Rows[inc4];
                Console.WriteLine(dRow4["Id"] + " - " + dRow4["Group"]);
                inc4++;
            }


        }
    }
}
