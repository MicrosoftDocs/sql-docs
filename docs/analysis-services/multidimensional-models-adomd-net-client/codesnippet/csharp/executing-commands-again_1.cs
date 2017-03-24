        void ExecuteXMLAProcessCommand()
        {
            //Open a connection to the local server
            AdomdConnection conn = new AdomdConnection("Data Source=localhost");
            conn.Open();

            //Create a command, and assign it an XMLA command to process the cube.
            AdomdCommand cmd = conn.CreateCommand();
            cmd.CommandText = "<Process xmlns=\"http://schemas.microsoft.com/analysisservices/2003/engine\">\r\n" +
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