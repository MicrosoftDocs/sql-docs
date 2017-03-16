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