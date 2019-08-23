using System;
using Microsoft.AnalysisServices.AdomdClient;

namespace ConsoleADOMDClient
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
    class AdomdExample
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
            AdomdExample ae = new AdomdExample();
            //ae.OutputDataWithXML();
            //ae.ExecuteXMLAProcessCommand();
            //Console.WriteLine(ae.ReturnCommandUsingCellSet());
            //ae.OutputCommandWithDataReader();
            //ae.OutputDimensionSchemaRowset();

            //Console.WriteLine(ae.DemonstrateDisconnectedCellset());
            
            //Connect to the local server
            //AdomdConnection conn = new AdomdConnection("Data Source=localhost");
            //conn.Open();
            //System.Data.DataSet ds = ae.GetActions(conn, "Adventure Works","[Geography].[City]",6 );
            //    foreach (System.Data.DataColumn col in ds.Tables[0].Columns)
            //        System.Diagnostics.Debug.WriteLine(col.ColumnName + ":" + ds.Tables[0].Rows[0][col.Ordinal]);
            //conn.Close();

            Console.WriteLine(ae.RetrieveCubesAndDimensions());

            Console.ReadLine();
		}

        //<snippetRetrieveCubesAndDimensions>
        private string RetrieveCubesAndDimensions()
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();

            //Connect to the local server
            using (AdomdConnection conn = new AdomdConnection("Data Source=localhost;"))
            {
                conn.Open();

                //Loop through every cube
                foreach (CubeDef cube in conn.Cubes)
                {
                    //Skip hidden cubes.
                    if (cube.Name.StartsWith("$"))
                        continue; 

                    //Write the cube name
                    result.AppendLine(cube.Name);

                    //Write out all dimensions, indented by a tab.
                    foreach (Dimension dim in cube.Dimensions)
                    {
                        result.Append("\t");
                        result.AppendLine(dim.Name);
                    }
                }

                //Close the connection
                conn.Close();
            }

            //Return the results
            return result.ToString();
        }
        //</snippetRetrieveCubesAndDimensions>

        //<snippetGetActions>
        //The following function can be called with the following data:
        //ae.GetActions(conn, "Adventure Works","[Geography].[City]",6 );

        //This would return a DataSet containing the actions available for cells
        //in the Adventure Works cube on [Geography].[City].
        private System.Data.DataSet GetActions(AdomdConnection Connection, string Cube, string Coordinate, int CoordinateType)
        {
            //Create a restriction collection to restrict the schema information to be returned.
            AdomdRestrictionCollection restrictions= new AdomdRestrictionCollection();
            restrictions.Add("CUBE_NAME", Cube);
            restrictions.Add("COORDINATE", Coordinate);
            restrictions.Add("COORDINATE_TYPE", CoordinateType); //6 = Cell coordinate

            //Open and return a schema rowset, given the correct restictions
            return Connection.GetSchemaDataSet("MDSCHEMA_ACTIONS", restrictions);
        }
        //</snippetGetActions>

        //<snippetDemonstrateDisconnectedCellset>
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
        //</snippetDemonstrateDisconnectedCellset>

        //<snippetReturnCommandUsingCellSet>
        string ReturnCommandUsingCellSet()
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

                //Execute the query, returning a cellset
                CellSet cs = cmd.ExecuteCellSet();

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
                conn.Close();

                return result.ToString();
            } // using connection
        }
        //</snippetReturnCommandUsingCellSet>

        //<snippetOutputCommandWithDataReader>
        void OutputCommandWithDataReader()
        {
            //Connect to the local server
            AdomdConnection conn = new AdomdConnection("Data Source=localhost");
            conn.Open();

            //Create a command to send to the server.
            AdomdCommand cmd = new AdomdCommand(@"WITH MEMBER [Measures].[FreightCostPerOrder] AS 
[Measures].[Reseller Freight Cost]/[Measures].[Reseller Order Quantity],  
FORMAT_STRING = 'Currency'

SELECT [Geography].[Geography].[Country].&[United States].Children ON ROWS, 
[Date].[Calendar].[Calendar Year] ON COLUMNS
FROM [Adventure Works]
WHERE [Measures].[FreightCostPerOrder]", conn);

            //Execute the command, returning an AdomdDataReader
            AdomdDataReader reader = cmd.ExecuteReader();

            //Retrieve the schema information about the AdomdDataReader
            System.Data.DataTable schema = reader.GetSchemaTable();

            //Cycle through the schema, printing out the 
            //ColumnName information as column headers.
            for (int n = 0; n < schema.Rows.Count; n++)
                Console.Write(schema.Rows[n]["ColumnName"] + "\t");
            Console.WriteLine();

            //Loop through the reader, outputting each cell's value.
            while (reader.Read())
            {
                for (int n = 0; n < reader.FieldCount; n++)
                    Console.Write(reader.GetValue(n) + "\t");
                Console.WriteLine();
            }

            //Close the connection and await user input.
            conn.Close();
            Console.ReadLine();
        }
        //</snippetOutputCommandWithDataReader>

        //<snippetExecuteXMLAProcessCommand>
        void ExecuteXMLAProcessCommand()
        {
            //Open a connection to the local server
            AdomdConnection conn = new AdomdConnection("Data Source=localhost");
            conn.Open();

            //Create a command, and assign it an XMLA command to process the cube.
            AdomdCommand cmd = conn.CreateCommand();
            cmd.CommandText = "<Process xmlns=\"https://schemas.microsoft.com/analysisservices/2003/engine\">\r\n" +
  @"<Object>
    <DatabaseID>Adventure Works DW Standard Edition</DatabaseID>
    <CubeID>Adventure Works DW</CubeID>
  </Object>
  <Type>ProcessFull</Type>
  <WriteBackTableCreation>UseExisting</WriteBackTableCreation>
</Process>";

            //Execute the command
            int result = cmd.ExecuteNonQuery();

            //Close the connection
            conn.Close();
        }
        //</snippetExecuteXMLAProcessCommand>

        //<snippetOutputDimensionSchemaRowset>
        void OutputDimensionSchemaRowset()
        {
            //Open a connection to the local server
            AdomdConnection conn = new AdomdConnection("Data Source=localhost");
            conn.Open();

            //Retrieve the schema rowset that describes the dimensions on the server
            System.Data.DataSet reader = conn.GetSchemaDataSet(AdomdSchemaGuid.Dimensions,null);

            //Retrieve the first table that has been returned
            //Some schema rowsets return more than one table
            System.Data.DataTable data = reader.Tables[0];

            //Loop through the columns and print them out as column headers.
            foreach (System.Data.DataColumn col in data.Columns)
                Console.Write(col.ColumnName + "/t");
            Console.WriteLine();

            //Loop through the cells, printing them out
            foreach (System.Data.DataRow row in data.Rows)
            {
                foreach (System.Data.DataColumn col in data.Columns)
                    Console.Write(row[col.ColumnName].ToString() + "/t");

                Console.WriteLine();
            }

            //Close the connection and await user input.
            conn.Close();
            Console.ReadLine();
        }
        //</snippetOutputDimensionSchemaRowset>

        //<snippetOutputDataWithXML>
        void OutputDataWithXML()
        {
            //Open a connection to the local server.
            AdomdConnection conn = new AdomdConnection("Data Source=localhost");
            conn.Open();

            //Create a command to retrieve the data.
            AdomdCommand cmd = new AdomdCommand(@"WITH MEMBER [Measures].[FreightCostPerOrder] AS 
[Measures].[Reseller Freight Cost]/[Measures].[Reseller Order Quantity],  
FORMAT_STRING = 'Currency'

SELECT [Geography].[Geography].[Country].&[United States].Children ON ROWS, 
[Date].[Calendar].[Calendar Year] ON COLUMNS
FROM [Adventure Works]
WHERE [Measures].[FreightCostPerOrder]", conn);

            //Execute the command, retrieving an XmlReader.
            System.Xml.XmlReader reader = cmd.ExecuteXmlReader();

            //Do something with the reader: Parse data, Parse metadata,
            //                              Save for later loading into CellSet, etc.
            Console.WriteLine(reader.ReadOuterXml());

            //Close the reader, then the connection
            reader.Close();
            conn.Close();

            //Await user input.
            Console.ReadLine();
        }
        //</snippetOutputDataWithXML>
    }
}
