using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace MyWindowsApp
{
    public class Scheduling_Run_01
    {
        public void ScheduleRun()
        {
            // Required once for connection
            DB_SQL_Connection objConnect;
            // Required per Recordset queries
            DataSet ds0;
            DataRow dRow0;
            int MaxRows0;
            int inc0 = 0;

            DataSet ds1;
            DataRow dRow1;
            int MaxRows1;
            int inc1 = 0;

            DataSet ds2;
            DataRow dRow2;
            int MaxRows2;
            int inc2 = 0;

            // Misc Variables 
            string priority_LOB = null;
            string core_LOB = null;

            try
            {
                Console.WriteLine("Connection Open ! ");
                // Once Only
                objConnect = new DB_SQL_Connection();
                
                // Start of Code Run
                ds0 = objConnect.ReturnQuery("GetTest");
                MaxRows0 = ds0.Tables[0].Rows.Count;

                Console.WriteLine("Input Table");
                while (inc0 < MaxRows0)
                {
                    dRow1 = ds0.Tables[0].Rows[inc0];
                    Console.WriteLine(dRow1.ItemArray.GetValue(1).ToString()
                        + " - " + dRow1.ItemArray.GetValue(2).ToString()
                        + " - " + dRow1.ItemArray.GetValue(3).ToString()
                        + " - " + dRow1.ItemArray.GetValue(4).ToString()
                        + " - " + dRow1.ItemArray.GetValue(5).ToString()
                        + " - " + dRow1.ItemArray.GetValue(6).ToString());
                    inc0 ++;
                }

                Console.WriteLine("Open Priority Table");
                inc1 = 0;
                ds1 = objConnect.ReturnQuery("GetLOBPriority");
                MaxRows1 = ds1.Tables[0].Rows.Count;
                while (inc1 < MaxRows1)
                {
                    dRow1 = ds1.Tables[0].Rows[inc1];
                    priority_LOB = dRow1.ItemArray.GetValue(2).ToString().Trim();

                    // Open next table
                    inc2 = 0;
                    ds2 = objConnect.ReturnQuery("GetGroups");
                    MaxRows2 = ds2.Tables[0].Rows.Count;
                    while (inc2 < MaxRows2)
                    {
                        Console.WriteLine("For " + priority_LOB + " cycle through " + inc2 + " of " + MaxRows2);
                        dRow2 = ds2.Tables[0].Rows[inc2];
                        core_LOB = dRow2.ItemArray.GetValue(4).ToString().Trim();
                        if (core_LOB == priority_LOB)
                        {
                            // How to Update a Row
                            DataRow row = ds2.Tables[0].Rows[inc2];
                            row["Sim_LOB"] = priority_LOB;
                        }
                        inc2 ++;
                    }
                    // Update changes in Dataset to Database
                    objConnect.UpdateDatabase(ds2);
                    // move to next LOB
                    inc1 ++;
                }
                //run execute only query
                DB_SQL_Connection newConn = new DB_SQL_Connection();
                newConn.ExecuteQuery("UpdateSimToConfirm");

                Console.WriteLine("Output");
                ds0 = objConnect.ReturnQuery("GetTest");
                MaxRows0 = ds0.Tables[0].Rows.Count;
                inc0 = 0;
                while (inc0 < MaxRows0)
                {
                    dRow0 = ds0.Tables[0].Rows[inc0];
                    Console.WriteLine(dRow0.ItemArray.GetValue(1).ToString()
                        + " - " + dRow0.ItemArray.GetValue(2).ToString()
                        + " - " + dRow0.ItemArray.GetValue(3).ToString()
                        + " - " + dRow0.ItemArray.GetValue(4).ToString()
                        + " - " + dRow0.ItemArray.GetValue(5).ToString()
                        + " - " + dRow0.ItemArray.GetValue(6).ToString());
                    inc0 ++;
                }
                // End of Code Run
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            Console.WriteLine("Press Enter to finish.");
            Console.ReadLine();
        }
    }
}