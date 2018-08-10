//<snippetColumn_CS_IsFileStream.CS>
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Management.Smo;


namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //Connect to the local, default instance of SQL Server.
            Server svr = new Server();

            //Define a Database object instance by supplying the server
            //and the database name arguments in the constructor.
            Database myDB = svr.Databases["MyPhotoDatabase"];

            //Connect to the MyPhotos table.
            Table tbl = myDB.Tables["MyPhotos"];

            Console.WriteLine("{0,-8} {1,-12}", "Column", "IsFileStream");

            foreach (Column c in tbl.Columns)
                Console.WriteLine("{0,-8} {1,-12}", c.Name, c.IsFileStream);
        }
    }
}
//</snippetColumn_CS_IsFileStream.CS>
