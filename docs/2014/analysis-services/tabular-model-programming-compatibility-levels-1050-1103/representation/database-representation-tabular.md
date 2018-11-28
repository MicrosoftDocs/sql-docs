---
title: "Database Representation(Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
ms.assetid: 16a233fb-f83b-4ca1-acb5-6186eca0a62c
author: minewiskan
ms.author: owend
manager: craigg
---
# Database Representation(Tabular)
  In Tabular Mode, the database is the container for all objects in the tabular model.  
  
## Database Representation  
 The database is the place where all objects that form a tabular model reside. Contained by the database, the developer finds objects like connections, tables, roles and many more.  
  
### Database in AMO  
 When using AMO to manage a tabular model database, the <xref:Microsoft.AnalysisServices.Database> object in AMO matches one-to-one the database logical object in a tabular model.  
  
> [!NOTE]  
>  In order to gain access to a database object, in AMO, the user needs to have access to a server object and connect to it.  
  
### Database in ADOMD.Net  
 When using ADOMD to consult and query a tabular model database, connection to a specific database is obtained through the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object.  
  
 You can connect directly to a certain database using the following code snippet:  
  
```csharp  
using ADOMD = Microsoft.AnalysisServices.AdomdClient;  
...  
   ADOMD.AdomdConnection currrentCnx = new ADOMD.AdomdConnection("Data Source=<<server\instance>>;Catalog=<<database>>");  
   currrentCnx.Open();  
...  
  
```  
  
 Also, over an existing connection object (that hasn't been closed), you can change the current database to another as shown in the following code snippet:  
  
```csharp  
currentCnx.ChangeDatabase("myOtherDatabase");  
  
```  
  
## Database in AMO  
 When using AMO to manage a database object, start with a <xref:Microsoft.AnalysisServices.Server> object. Then search for your database in the databases collection or create a new database by adding one to the collection.  
  
 The following code snippet shows the steps to connect to a server and create an empty database, after checking the database doesn't exist:  
  
```  
  
AMO.Server CurrentServer = new AMO.Server();  
try  
{  
    CurrentServer.Connect(currentServerName);  
}  
catch (Exception cnxException)  
{  
    MessageBox.Show(string.Format("Error while trying to connect to server: [{0}]\nError message: {1}", currentServerName, cnxException.Message), "AMO to Tabular message", MessageBoxButtons.OK, MessageBoxIcon.Error);  
    return;  
}  
newDatabaseName = DatabaseName.Text;  
if (CurrentServer.Databases.Contains(newDatabaseName))  
{  
    return;  
}  
try  
{  
    AMO.Database newDatabase = CurrentServer.Databases.Add(newDatabaseName);  
  
    CurrentServer.Update();  
}  
catch (Exception createDBxc)  
{  
    MessageBox.Show(String.Format("Database [{0}] couldn't be created.\n{1}", newDatabaseName, createDBxc.Message), "AMO to Tabular message", MessageBoxButtons.OK, MessageBoxIcon.Error);  
    newDatabaseAvailable = false;  
}  
  
```  
  
 For a practical understanding on how to use AMO to create and manipulate database representations, see source code in the Tabular AMO 2012 sample; specifically check in the following source file: Database.cs. Sample code is provided only as a support to the logical concepts explained here, and should not be used in a production environment.  
  
  
