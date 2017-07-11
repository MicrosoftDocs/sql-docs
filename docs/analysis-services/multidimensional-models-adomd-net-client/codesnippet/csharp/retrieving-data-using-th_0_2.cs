        string DemonstrateDisconnectedCellset()
        {
            //Create a new string builder to store the results
            System.Text.StringBuilder result = new System.Text.StringBuilder();

            //Connect to the local server
            using (AdomdConnection conn = new AdomdConnection("Data Source=localhost;"))
            {
                conn.Open();

                //Create a command, using this connection
                AdomdCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"
                              WITH MEMBER [Measures].[FreightCostPerOrder] AS 
                                    [Measures].[Reseller Freight Cost]/[Measures].[Reseller Order Quantity],  
                                    FORMAT_STRING = 'Currency'
                              SELECT 
                                    [Geography].[Geography].[Country].&[United States].Children ON ROWS, 
                                    [Date].[Calendar].[Calendar Year] ON COLUMNS
                              FROM [Adventure Works]
                              WHERE [Measures].[FreightCostPerOrder]";


                //Execute the query, returning an XmlReader
                System.Xml.XmlReader x = cmd.ExecuteXmlReader();

                //At this point, the XmlReader could be stored on disk,
                //transmitted, modified, cached, or otherwise manipulated

                //Load the CellSet with the specified XML
                CellSet cs = CellSet.LoadXml(x);

                //Now that the XmlReader has finished being read
                //we can close it and the connection, while the
                //CellSet can continue being used.
                x.Close();
                conn.Close();

                //Output the column captions from the first axis
                //Note that this procedure assumes a single member exists per column.
                result.Append("\t");
                TupleCollection tuplesOnColumns = cs.Axes[0].Set.Tuples;
                foreach (Tuple column in tuplesOnColumns)
                {
                    result.Append(column.Members[0].Caption + "\t");
                }
                result.AppendLine();

                //Output the row captions from the second axis and cell data
                //Note that this procedure assumes a two-dimensional cellset
                TupleCollection tuplesOnRows = cs.Axes[1].Set.Tuples;
                for (int row = 0; row < tuplesOnRows.Count; row++)
                {
                    result.Append(tuplesOnRows[row].Members[0].Caption + "\t");
                    for (int col = 0; col < tuplesOnColumns.Count; col++)
                    {
                        result.Append(cs.Cells[col, row].FormattedValue + "\t");
                    }
                    result.AppendLine();
                }

                return result.ToString();
            } // using connection
        }