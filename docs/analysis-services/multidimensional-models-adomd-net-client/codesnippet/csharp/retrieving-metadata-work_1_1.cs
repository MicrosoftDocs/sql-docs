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