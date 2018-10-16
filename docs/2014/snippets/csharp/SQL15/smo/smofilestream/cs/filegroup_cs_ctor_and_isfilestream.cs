//<snippetFileGroup_CS_ctor_and_IsFileStream>
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Management.Smo;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Connect to the local, default instance of SQL Server.
            Server svr = new Server();

            //Define a Database object instance by supplying the server
            //and the database name arguments in the constructor.
            Database db = new Database(svr, "MyPhotos");

            //Create the database on the instance of SQL Server.
            db.Create();

            //Define a FileGroup object instance for creation of FILESTREAM
            //groups by supplying the database, group name, and supplying
            //true for the isFileStream parameters.

            FileGroup myFileGroup = new FileGroup(db, "MyPhotoGroup", true);

            if (myFileGroup.IsFileStream == true)
                Console.WriteLine("MyPhotoGroup is enabled for the creation of a FILESTREAM file group.");

            //Remove the database to clean up.
            db.Drop();
        }
    }
}
//</snippetFileGroup_CS_ctor_and_IsFileStream>
