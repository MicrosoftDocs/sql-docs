using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Microsoft.AnalysisServices;
// Use Adomd.Server
using Microsoft.AnalysisServices.AdomdServer;


namespace MyNS
{
    public class MyClass
    {
        //<snippetGetNodeDescription>
        public string GetNodeDescription(string nodeUniqueName)
        {
            return Context.CurrentMiningModel.GetNodeFromUniqueName(nodeUniqueName).Description;
        }
        //</snippetGetNodeDescription>

        
        //<snippetRandomSample>
        public Set RandomSample(Set set, int returnCount)
        {
            //Return the original set if there are fewer tuples
            //in the set than the number requested.
            if (set.Tuples.Count <= returnCount)
                return set;

            System.Random r = new System.Random();
            SetBuilder returnSet = new SetBuilder();

            //Retrieve random tuples until the return set is filled.
            int i = set.Tuples.Count;
            foreach (Tuple t in set.Tuples)
            {
                if (r.Next(i) < returnCount)
                {
                    returnCount--;
                    returnSet.Add(t);
                }
                i--;
                //Stop the loop if we have enough tuples.
                if (returnCount == 0)
                    break;
            }
            return returnSet.ToSet();
        }
        //</snippetRandomSample>

        // Handling timeouts
        //<snippetLongRunning>
        public string LongRunning()
        {

            try
            {
                //Enter a long running loop
                for (int nIndex = 0; nIndex < 1000; nIndex++)
                {
                    //Exception generated when the query is canceled or Timeout occurs
                    Context.CheckCancelled();
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return "UDF Allowed to Finish";
        }
        //</snippetLongRunning>

        //<snippetGetPreparedTable>
        [SafeToPrepare(true)]
        public System.Data.DataTable GetPreparedTable()
        {
            System.Data.DataTable results = new System.Data.DataTable();
            results.Columns.Add("A", typeof(int));
            results.Columns.Add("B", typeof(string));

            if (Context.ExecuteForPrepare)
            {
                // If preparing, return just the schema with no data
                return results;
            }

            //Otherwise return data
            object[] row = new object[2];
            row[0] = 1;
            row[1] = "A";
            results.Rows.Add(row);

            row[0] = 2;
            row[1] = "B";
            results.Rows.Add(row);

            return results;
        }
        //</snippetGetPreparedTable>

//        Expression e = new Expression(@"Select [Geography].[State-Province].Members on Rows, 
//[Date].[Calendar].[Calendar Year] on Columns
//From [Adventure Works]
//Where [Measures].[Reseller Freight Cost]");
//        TupleBuilder t = new TupleBuilder((Member)Context.CurrentCube.Dimensions[0].Hierarchies[0].CurrentMember);
//        MDXValue m = e.CalculateMdxObject(t.ToTuple());



        public string GetSomething()
        {
            return "Foo";
        }

        public System.Data.DataTable GetTable()
        {
            System.Data.DataTable tbl = new System.Data.DataTable();
            tbl.Columns.Add("A", typeof(int));
            tbl.Columns.Add("B", typeof(string));


            object[] row = new object[2];
            row[0] = 1;
            row[1] = "Foo";
            tbl.Rows.Add(row);

            row[0] = 2;
            row[1] = "Bar";
            tbl.Rows.Add(row);

            return tbl;
        }

        // Returning nested tables
        public System.Data.DataSet GetDataSet()
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            System.Data.DataTable tbl = new System.Data.DataTable();
            tbl.Columns.Add("A", typeof(int));
            tbl.Columns.Add("B", typeof(string));
            tbl.TableName = "SomeTable";


            object[] row = new object[2];
            row[0] = 1;
            row[1] = "Foo";
            tbl.Rows.Add(row);

            row[0] = 2;
            row[1] = "Bar";
            tbl.Rows.Add(row);

            ds.Tables.Add(tbl);
            return ds;
        }


        // Prepared table:
        [SafeToPrepare(true)]
        public System.Data.DataTable GetPreparedTable_Original()
        {
            System.Data.DataTable tbl = new System.Data.DataTable();
            tbl.Columns.Add("A", typeof(int));
            tbl.Columns.Add("B", typeof(string));

            if (Context.ExecuteForPrepare)
            {
                // If preparing, return just the schema with no data
                return tbl;
            }

            object[] row = new object[2];
            row[0] = 1;
            row[1] = "Foo";
            tbl.Rows.Add(row);

            row[0] = 2;
            row[1] = "Bar";
            tbl.Rows.Add(row);

            return tbl;
        }

        // UDF returning a node description
        //public string GetNodeDescription(string nodeUniqueName)
        //{
        //    // UDF, hence there is such thing as <Current Model> (select ... FROM <Current Model> )
        //    return Context.CurrentMiningModel.GetNodeFromUniqueName(nodeUniqueName).Description;
        //}



        //-----------------

        // UDF returning a node description
        public string GetMemberDescription(string dimensionName, string hierarchyName)
        {
            return Context.CurrentCube.Dimensions[dimensionName].Hierarchies[hierarchyName].CurrentMember.Description;
        }

        //public Set RandomSample(Set set, int returnCount)
        //{
        //    //Return the original set if there are fewer tuples
        //    //in the set than the number requested.
        //    if (set.Tuples.Count <= returnCount)
        //        return set;

        //    System.Random r = new System.Random();
        //    SetBuilder returnSet = new SetBuilder();

        //    //Retrieve random tuples until the return set is filled.
        //    int i = set.Tuples.Count;
        //    foreach (Tuple t in set.Tuples)
        //    {
        //        if (r.Next(i) < returnCount)
        //        {
        //            returnCount--;
        //            returnSet.Add(t);
        //        }
        //        i--;
        //        //Stop the loop if we have enough tuples.
        //        if (returnCount == 0)
        //            break;
        //    }
        //    return returnSet.ToSet();
        //}



        /// <summary>
        /// Return the minimum of two numbers
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static float Minimum(float xValue, float yValue)
        {
            if (xValue < yValue)
            {
                return xValue;
            }
            else
            {
                return yValue;
            }
        }

        /// <summary>
        /// Return the maximum of two numbers
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static float Maximum(float xValue, float yValue)
        {
            if (xValue > yValue)
            {
                return xValue;
            }
            else
            {
                return yValue;
            }
        }

        /// <summary>
        /// Filter a set of members by an expression
        /// </summary>
        /// <param name="set"></param>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        //<snippetFilterSet>
        public static Set FilterSet(Set set, String filterExpression)
        {
            Expression expr = new Expression(filterExpression);

            SetBuilder resultSetBuilder = new SetBuilder();

            foreach (Tuple tuple in set)
            {
                if ((bool)(expr.Calculate(tuple)))
                {
                    resultSetBuilder.Add(tuple);
                }
            }

            return resultSetBuilder.ToSet();
        }
        //</snippetFilterSet>

        /// <summary>
        /// Calculate the weighted average of a set
        /// </summary>
        /// <param name="set"></param>
        /// <param name="weightExpression"></param>
        /// <param name="inputExpression"></param>
        /// <returns></returns>
        //<snippetWeightedAverage>
        public static float WeightedAverage(Set set, String weightExpression, String inputExpression)
        {
            float expression;
            float weight;

            Expression weightExpr = new Expression(weightExpression);
            Expression inputExpr = new Expression(inputExpression);

            float total = 0;
            float totalWeight = 0;

            foreach (Tuple tuple in set)
            {
                expression = (float)inputExpr.Calculate(tuple);
                weight = (float)weightExpr.Calculate(tuple);

                total += expression * weight;
                totalWeight += weight;
            }

            if (totalWeight > 0)
            {
                return total / totalWeight;
            }
            else
            {
                return 0;
            }
        }
        //</snippetWeightedAverage>

        /// <summary>
        /// Check the current state of the data warehouse and create partitions if necessary
        /// </summary>
        //<snippetCreateInternetSalesMeasureGroupPartitions>
        public static void CreateInternetSalesMeasureGroupPartitions()
        {
            //Check the current state of the data warehouse and 
            //create partitions with AMO if necessary
            #region Retrieve order date of last sales transaction

            // Open a connection to the data warehouse
            // TODO: Change the first string parameter to reflect the right server\instance.
            SqlConnection conn = new SqlConnection(string.Format("data source={0};initial catalog={1};Integrated Security=SSPI", "server\\instance", Context.CurrentDatabaseName));
            conn.Open();

            // Create a command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            // Get the order date key of the last internet sale
            int lastInternetSaleDateKey = 0;
            cmd.CommandText = "select max(OrderDateKey) from FactInternetSales";
            lastInternetSaleDateKey = (int)cmd.ExecuteScalar();

            // Get the order date key of the last reseller sale
            int lastResellerSaleDateKey = 0;
            cmd.CommandText = "select max(OrderDateKey) from FactResellerSales";
            lastResellerSaleDateKey = (int)cmd.ExecuteScalar();
            #endregion

            #region Create partitions

            // Connect to the calling session
            Server svr = new Server();
            svr.Connect("*");

            // Get the Adventure Works cube
            Database db = svr.Databases.GetByName(Context.CurrentDatabaseName);
            Cube cube = db.Cubes[0];

            MeasureGroup mg;
            int maxOrderDateKey;

            mg = cube.MeasureGroups.GetByName("Internet Sales");
            maxOrderDateKey = 0;
            foreach (Partition part in mg.Partitions)
            {
                maxOrderDateKey = Math.Max(maxOrderDateKey, Convert.ToInt32(
                    part.Annotations.Find("LastOrderDateKey").Value.Value,
                    System.Globalization.CultureInfo.InvariantCulture));
            }

            if (maxOrderDateKey < lastInternetSaleDateKey)
            {
                Partition part = mg.Partitions.Add("Internet_Sales_"
                    + lastInternetSaleDateKey);
                part.StorageMode = StorageMode.Molap;
                part.Source = new QueryBinding(db.DataSources[0].ID,
                    "SELECT * FROM [dbo].[FactInternetSales] WHERE OrderDateKey > '"
                    + maxOrderDateKey + "' and OrderDateKey <= '" + lastInternetSaleDateKey + "'");
                part.Annotations.Add("LastOrderDateKey", Convert.ToString(lastInternetSaleDateKey,
                    System.Globalization.CultureInfo.InvariantCulture));
                part.Update();
                part.Process();
            }

            svr.Disconnect();

            #endregion
        }
        //</snippetCreateInternetSalesMeasureGroupPartitions>
    
    }
}
