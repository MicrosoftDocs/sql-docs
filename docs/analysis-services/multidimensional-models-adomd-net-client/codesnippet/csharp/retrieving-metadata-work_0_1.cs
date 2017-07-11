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