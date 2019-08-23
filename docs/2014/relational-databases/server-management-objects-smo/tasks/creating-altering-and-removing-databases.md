---
title: "Creating, Altering, and Removing Databases | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: "reference"
helpviewer_keywords: 
  - "databases [SMO]"
  - "databases [SMO], creating"
  - "databases [SMO], modifying"
  - "databases [SMO], deleting"
ms.assetid: fcfb3ec2-7556-4f72-971a-501295892cb0
author: stevestein
ms.author: sstein
manager: craigg
---
# Creating, Altering, and Removing Databases
  In SMO, a database is represented by the <xref:Microsoft.SqlServer.Management.Smo.Database> object.  
  
 It is not necessary to create a <xref:Microsoft.SqlServer.Management.Smo.Database> object to modify or remove it. The database can be referenced by using a collection.  
  
## Example  
 To use any code example that is provided, you will have to choose the programming environment, the programming template, and the programming language in which to create your application. For more information, see [Create a Visual Basic SMO Project in Visual Studio .NET](../../../database-engine/dev-guide/create-a-visual-basic-smo-project-in-visual-studio-net.md) or [Create a Visual C&#35; SMO Project in Visual Studio .NET](../how-to-create-a-visual-csharp-smo-project-in-visual-studio-net.md).  
  
## Creating, Altering, and Removing a Database in Visual Basic  
 This code example creates a new database. Files and file groups are automatically created for the database.  
  
<!-- TODO: review snippet reference  [!CODE [SMO How to#SMO_VBDatabase1](SMO How to#SMO_VBDatabase1)]  -->  
  
## Creating, Altering, and Removing a Database in Visual C#  
 This code example creates a new database. Files and file groups are automatically created for the database.  
  
```  
{  
                //Connect to the local, default instance of SQL Server.   
                Server srv;  
                srv = new Server();  
                //Define a Database object variable by supplying the server and the database name arguments in the constructor.   
                Database db;  
                db = new Database(srv, "Test_SMO_Database");  
                //Create the database on the instance of SQL Server.   
                db.Create();  
                //Reference the database and display the date when it was created.   
                db = srv.Databases["Test_SMO_Database"];  
                Console.WriteLine(db.CreateDate);  
                //Remove the database.   
                db.Drop();  
            }  
```  
  
## Creating, Altering, and Removing a Database in PowerShell  
 This code example creates a new database. Files and file groups are automatically created for the database.  
  
```  
#Get a server object which corresponds to the default instance  
cd \sql\localhost\  
$srv = get-item default  
  
#Create a new database  
$db = New-Object -TypeName Microsoft.SqlServer.Management.Smo.Database -argumentlist $srv, "Test_SMO_Database"  
$db.Create()  
  
#Reference the database and display the date when it was created.   
$db = $srv.Databases["Test_SMO_Database"]  
$db.CreateDate  
  
#Drop the database  
$db.Drop()  
```  
  
## See Also  
 <xref:Microsoft.SqlServer.Management.Smo.Database>  
  
  
