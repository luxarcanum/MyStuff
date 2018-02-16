using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MyWindowsApp
{
    class Requirements_Functions
    {
        public void Output_Function(string wcDate)
        {
            DateTime start_Date = DateTime.Parse(wcDate);
            DateTime endDate = start_Date.AddDays(7);

            for (DateTime nDate = start_Date; nDate.Date <= endDate.Date; nDate = nDate.AddDays(1))
            {   // run through each date (option to multi-thread)
                // Add output to Allocation Table
                DB_SQL_Connection newConn = new DB_SQL_Connection();
                //DB_SQL_Connection connectionObject;
                DataSet ds3;
                DataRow dRow3;
                int MaxRows3;
                int inc3 = 0;

                DataSet ds4;
                DataRow dRow4;
                int MaxRows4;
                int inc4 = 0;

                string timeslot4 = "";
                string allocated4 = "";
                string insertString = "";
                string currentGroup = "";
                string cDate = "";
                string xDate = "";

                DB_SQL_Connection newConnection = new DB_SQL_Connection();
                newConnection.ExecuteQuery("DELETE FROM [Allocation]");


                newConn.SqlQuery = "Select [Group] from [Groups]";
                ds3 = newConn.GetConnection;
                MaxRows3 = ds3.Tables[0].Rows.Count;
                inc3 = 0;

                newConn.SqlQuery = "Select [Group], [rDate], [Allocated], [TimeSlot] from [Staff_Groups]";
                ds4 = newConn.GetConnection;
                MaxRows4 = ds4.Tables[0].Rows.Count;
                inc4 = 0;

                while (inc3 < MaxRows3)
                {
                    dRow3 = ds3.Tables[0].Rows[inc3];
                    currentGroup = dRow3["Group"].ToString().Trim();
                    timeslot4 = "[Group], [aDate]";
                    allocated4 = "'" + currentGroup + "', '" + nDate.ToString("yyyy/MM/dd") + "'";

                    inc4 = 0;
                    while (inc4 < MaxRows4)
                    {
                        dRow4 = ds4.Tables[0].Rows[inc4];
                        cDate = dRow4["rDate"].ToString().Trim();
                        xDate = nDate.ToString();

                        Console.WriteLine("nDate: " + nDate);
                        Console.WriteLine("cDate: " + cDate);
                        Console.WriteLine("xDate: " + xDate);
                        Console.WriteLine("cGroup: " + currentGroup);
                        Console.WriteLine("gGroup: " + dRow4["Group"].ToString().Trim());

                        if (currentGroup == dRow4["Group"].ToString().Trim() && cDate == xDate)
                        {
                            timeslot4 = timeslot4 + ", [" + double.Parse(dRow4["TimeSlot"].ToString().Trim()) + "]";
                            allocated4 = allocated4 + ", '" + dRow4["Allocated"].ToString().Trim() + "'";
                        }
                        inc4++;
                    }
                    insertString = "INSERT INTO [Allocation] (" + timeslot4 + ")" +
                        " VALUES (" + allocated4 + ")";
                    //Console.WriteLine(insertString);
                    newConn.ExecuteQuery(insertString);
                    inc3++;
                }


            }
        }
    }
}
