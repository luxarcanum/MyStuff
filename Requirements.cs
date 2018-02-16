using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace MyWindowsApp
{
    public class Requirements
    {
        public void Requirement_Run(string startDate)
        {
            string wcDate = startDate;

            try
            {
                // Clear Staff Groups table
                DB_SQL_Connection newConnection = new DB_SQL_Connection();
                newConnection.ExecuteQuery("ZeroStaffNeed");

                Console.WriteLine("Week Commencing Date: " + wcDate);
                DateTime start_Date = DateTime.Parse(wcDate);
                DateTime endDate = start_Date.AddDays(7);

                for (DateTime nDate = start_Date; nDate.Date <= endDate.Date; nDate = nDate.AddDays(1))
                {   // run through each date (option to multi-thread)
                    Console.WriteLine("Current Date: " + nDate.ToString("yyyy/MM/dd"));
                    //double[] timeSlots = { 7, 7.5, 8, 8.5, 9, 9.5, 10, 10.5, 11, 11.5, 12, 12.5, 13, 13.5,
                    //   14, 14.5, 15, 15.5, 16, 16.5, 17, 17.5, 18, 18.5, 19, 19.5, 20, 20.5, 21, 21.5, 22 };
                    double[] timeSlots = { 8, 8.5, 9, 9.5, 10, 10.5, 11, 11.5, 12, 12.5, 13, 13.5,
                        14, 14.5, 15, 15.5, 16, 16.5, 17, 17.5, 18, 18.5, 19, 19.5 };

                    Parallel.ForEach(timeSlots, thisSlot =>
                    //foreach (double thisSlot in timeSlots)
                    {   // run through each slot (option to multi-thread)

                        DB_SQL_Connection connectionObject;
                        //Requirements_Functions reqFuntion;
                        DataSet dataSet0;
                        DataRow dataRow0;
                        int maxRows0;
                        int increment0 = 0;

                        DataSet dataSet1;
                        DataRow dataRow1;
                        int maxRows1;
                        int increment1 = 0;
                        int increment2 = 0;

                        string forcastLine = "ADL";
                        int forcastNeed;
                        string groupName;
                        int minForGroup;
                        int maxForGroup;
                        int allocated;
                        int groupTotal;
                        int initialAllocation = 0;
                        int actualAllocation = 0;
                        int sumLine = 0;
                        int groupSum = 0;
                        int allocate = 0;
                        double nSlot = thisSlot;

                        // add DB Connection Class : Once Only
                        connectionObject = new DB_SQL_Connection();
                        Console.WriteLine("Connection Open ! ");

                        // Open Forecast need table to cycle through
                        Console.WriteLine("Open Forecast Table for " + nDate.ToString("yyyy/MM/dd") + " : " + nSlot);
                        dataSet0 = connectionObject.ReturnQuery("GetForecast", "nSlot", nSlot.ToString());
                        maxRows0 = dataSet0.Tables[0].Rows.Count;
                        // Open Staff Groups as a dataset
                        Console.WriteLine("Open Staff Groups Table for " + nDate.ToString("yyyy/MM/dd") + " : " + nSlot);
                        dataSet1 = connectionObject.ReturnQuery("GetStaffGroups", "nSlot", nSlot.ToString());
                        maxRows1 = dataSet1.Tables[0].Rows.Count;

                        // Fill in Total Need for ratio calcs on each staffGroup
                        increment1 = 0;
                        while (increment1 < maxRows1)
                        {
                            dataRow1 = dataSet1.Tables[0].Rows[increment1];
                            groupName = dataRow1["Group"].ToString().Trim();
                            groupSum = 0;
                            increment0 = 0;
                            while (increment0 < maxRows0)
                            {
                                dataRow0 = dataSet0.Tables[0].Rows[increment0];
                                forcastLine = dataRow0["Line"].ToString().Trim();
                                forcastNeed = int.Parse(dataRow0["Need"].ToString().Trim());
                                if (groupName.Contains(forcastLine))
                                {
                                    groupSum = groupSum + forcastNeed;
                                }
                                increment0++;
                            }
                            dataRow1["Total"] = groupSum;
                            dataRow1["Allocated"] = (int)dataRow1["Max"] * 0.5;
                            connectionObject.UpdateDatabase(dataSet1);
                            increment1++;
                        }
                        //reqFuntion = new Requirements_Functions();
                        //reqFuntion.do_calcs(dataSet0, dataSet1);

                        // Start checking allocation of forecast to staff groups
                        increment0 = 0;
                        while (increment0 < maxRows0)
                        {
                            dataRow0 = dataSet0.Tables[0].Rows[increment0];
                            forcastLine = dataRow0["Line"].ToString().Trim();
                            forcastNeed = int.Parse(dataRow0["Need"].ToString().Trim());

                            increment1 = 0;
                            sumLine = 0;
                            while (increment1 < maxRows1)
                            {
                                dataRow1 = dataSet1.Tables[0].Rows[increment1];
                                groupName = dataRow1["Group"].ToString().Trim();

                                sumLine = sumLine + int.Parse(dataRow1[forcastLine].ToString().Trim());
                                increment1++;
                            }
                            // Divide forecast Need by Count of groups containing Forcast line.
                            // forcast need must deduct proportion already allocated from other staff groups.
                            initialAllocation = (forcastNeed - sumLine);

                            increment1 = 0;
                            while (increment1 < maxRows1)
                            {
                                dataRow1 = dataSet1.Tables[0].Rows[increment1];
                                groupName = dataRow1["Group"].ToString().Trim();
                                minForGroup = int.Parse(dataRow1["Min"].ToString().Trim());
                                maxForGroup = int.Parse(dataRow1["Max"].ToString().Trim());
                                allocated = int.Parse(dataRow1["Allocated"].ToString().Trim());
                                groupTotal = int.Parse(dataRow1["Total"].ToString().Trim());

                                //if (groupName.Contains(forcastLine) && int.Parse(dataRow1[forcastLine].ToString()) == 0)
                                if (groupName.Contains(forcastLine))
                                {
                                    // Cycle through and check if amount is within min/max
                                    // Allocate number
                                    dataRow1 = dataSet1.Tables[0].Rows[increment1];
                                    float ratioMax = maxForGroup * ((float)forcastNeed / (float)groupTotal);
                                    if ((int)ratioMax < initialAllocation + int.Parse(dataRow1[forcastLine].ToString().Trim()))
                                    {
                                        actualAllocation = (int)ratioMax;
                                    }
                                    else
                                    {
                                        actualAllocation = initialAllocation + int.Parse(dataRow1[forcastLine].ToString().Trim());
                                    }

                                    float allocationAmount = actualAllocation / ((float)forcastNeed / (float)groupTotal);
                                    if ((int)allocationAmount > allocated) { allocate = (int)allocationAmount; }
                                    else { allocate = allocated; }
                                    dataRow1["allocated"] = allocate;
                                    initialAllocation = initialAllocation - actualAllocation;
                                }
                                //connectionObject.UpdateDatabase(dataSet1);
                                increment1++;
                            }
                            connectionObject.UpdateDatabase(dataSet1);
                            // Calculate other fields based on Allocated
                            increment2 = 0;
                            while (increment2 < maxRows0)
                            {
                                dataRow0 = dataSet0.Tables[0].Rows[increment2];
                                forcastLine = dataRow0["Line"].ToString().Trim();
                                forcastNeed = int.Parse(dataRow0["Need"].ToString().Trim());

                                int increment3 = 0;
                                while (increment3 < maxRows1)
                                {
                                    dataRow1 = dataSet1.Tables[0].Rows[increment3];
                                    groupName = dataRow1["Group"].ToString().Trim();
                                    allocated = int.Parse(dataRow1["Allocated"].ToString().Trim());
                                    groupTotal = int.Parse(dataRow1["Total"].ToString().Trim());

                                    if (groupName.Contains(forcastLine))
                                    {
                                        // Cycle through and check if amount is within min/max
                                        // Allocate number
                                        if ((float)forcastNeed == 0) { forcastNeed = 1; }
                                        if ((float)groupTotal == 0) { groupTotal = 1; }
                                        dataRow1[forcastLine] = allocated * ((float)forcastNeed / (float)groupTotal);
                                    }
                                    increment3++;
                                }
                                increment2++;
                            }
                            connectionObject.UpdateDatabase(dataSet1);

                            // Loop to next Need Line
                            increment0++;
                        }
                        Console.WriteLine("Finished updates for " + nDate.ToString("yyyy/MM/dd") + " : " + nSlot);
                    });
                    //}
                }

                // Output to Screen
                /*connectionObject.SqlQuery = "Select * from [Staff_Groups]";
                DataSet ds0 = connectionObject.GetConnection;
                int MaxRows0 = ds0.Tables[0].Rows.Count;
                int inc0 = 0;
                Console.WriteLine("Output Table");
                Console.WriteLine("Staff Group".PadRight(22) + "Alloc" + "ADL".PadLeft(7)
                    + "PYE".PadLeft(7) + "SAH".PadLeft(7) + "TCH".PadLeft(7));
                while (inc0 < MaxRows0)
                {
                    DataRow dRow0 = ds0.Tables[0].Rows[inc0];
                    Console.WriteLine(dRow0.ItemArray.GetValue(1).ToString().PadRight(18)
                        + " - " + dRow0.ItemArray.GetValue(4).ToString().PadLeft(4)
                        + " - " + dRow0.ItemArray.GetValue(5).ToString().PadLeft(4)
                        + " - " + dRow0.ItemArray.GetValue(6).ToString().PadLeft(4)
                        + " - " + dRow0.ItemArray.GetValue(7).ToString().PadLeft(4)
                        + " - " + dRow0.ItemArray.GetValue(8).ToString().PadLeft(4)
                        + " - " + dRow0.ItemArray.GetValue(9).ToString().PadLeft(4));
                    inc0++;
                }*/
                // Output to table
                Requirements_Functions outputObject = new Requirements_Functions();
                outputObject.Output_Function(wcDate);
                // Output to json text file

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                String exDetail = String.Format("Exception message: {0}{1}Exception Source: {2}{1}Exception StackTrace: {3}{1}",
                    error.Message, Environment.NewLine, error.Source, error.StackTrace);
                Console.WriteLine(exDetail);
            }
            // Tidy up
            Console.WriteLine("Press Enter to finish.");
            Console.ReadLine();
        }
    }
}
