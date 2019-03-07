---
title: "Using Collections | Microsoft Docs"
ms.custom: ""
ms.date: "08/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 

ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Management Objects, collections"
  - "SMO [SQL Server], collections"
  - "collections [SMO]"
ms.assetid: 209eb175-2514-4de1-bc32-b2e6a469d945
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Using Collections
[!INCLUDE[appliesto-ss-asdb-asdw-xxx-md](../../../includes/appliesto-ss-asdb-asdw-xxx-md.md)]

  A collection is a list of objects that have been constructed from the same object class and that share the same parent object. The collection object always contains the name of the object type with the Collection suffix. For example, to access the columns in a specified table, use the <xref:Microsoft.SqlServer.Management.Smo.ColumnCollection> object type. It contains all the <xref:Microsoft.SqlServer.Management.Smo.Column> objects that belong to the same <xref:Microsoft.SqlServer.Management.Smo.Table> object.  
  
 The [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[vbprvb](../../../includes/vbprvb-md.md)] **For...Each** statement or the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[csprcs](../../../includes/csprcs-md.md)] **foreach** statement can be used to iterate through each member of the collection.  
  
## Examples  
To use any code example that is provided, you will have to choose the programming environment, the programming template, and the programming language in which to create your application. For more information, see [Create a Visual C&#35; SMO Project in Visual Studio .NET](../../../relational-databases/server-management-objects-smo/how-to-create-a-visual-csharp-smo-project-in-visual-studio-net.md).  
  
## Referencing an Object by Using a Collection in Visual Basic  
 This code example shows how to set a column property by using the <xref:Microsoft.SqlServer.Management.Smo.TableViewTableTypeBase.Columns%2A>, <xref:Microsoft.SqlServer.Management.Smo.Database.Tables%2A>, and <xref:Microsoft.SqlServer.Management.Smo.Server.Databases%2A> properties. These properties represent collections, which can be used to identify a particular object when they are used with a parameter that specifies the name of the object. The name and the schema are required for the <xref:Microsoft.SqlServer.Management.Smo.Database.Tables%2A> collection object property.  
  
```VBNET
'Connect to the local, default instance of SQL Server.
Dim srv As Server
srv = New Server
'Modify a property using the Databases, Tables, and Columns collections to reference a column.
srv.Databases("AdventureWorks2012").Tables("Person", "Person").Columns("ModifiedDate").Nullable = True
'Call the Alter method to make the change on the instance of SQL Server.
srv.Databases("AdventureWorks2012").Tables("Person", "Person").Columns("ModifiedDate").Alter()
```
  
## Referencing an Object by Using a Collection in Visual C#  
 This code example shows how to set a column property by using the <xref:Microsoft.SqlServer.Management.Smo.TableViewTableTypeBase.Columns%2A>, <xref:Microsoft.SqlServer.Management.Smo.Database.Tables%2A>, and <xref:Microsoft.SqlServer.Management.Smo.Server.Databases%2A> properties. These properties represent collections, which can be used to identify a particular object when they are used with a parameter that specifies the name of the object. The name and the schema are required for the <xref:Microsoft.SqlServer.Management.Smo.Database.Tables%2A> collection object property.  
  
```csharp  
{   
//Connect to the local, default instance of SQL Server.   
Server srv;   
srv = new Server();   
//Modify a property using the Databases, Tables, and Columns collections to reference a column.   
srv.Databases("AdventureWorks2012").Tables("Person", "Person").Columns("LastName").Nullable = true;   
//Call the Alter method to make the change on the instance of SQL Server.   
srv.Databases("AdventureWorks2012").Tables("Person", "Person").Columns("LastName").Alter();   
}  
```  
  
## Iterating Through the Members of a Collection in Visual Basic  
 This code example iterates through the <xref:Microsoft.AnalysisServices.Server.Databases%2A> collection property and displays all database connections to the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
```VBNET
'Connect to the local, default instance of SQL Server.
Dim srv As Server
srv = New Server
Dim count As Integer
Dim total As Integer
'Iterate through the databases and call the GetActiveDBConnectionCount method.
Dim db As Database
For Each db In srv.Databases
    count = srv.GetActiveDBConnectionCount(db.Name)
    total = total + count
    'Display the number of connections for each database.
    Console.WriteLine(count & " connections on " & db.Name)
Next
'Display the total number of connections on the instance of SQL Server.
Console.WriteLine("Total connections =" & total)
```
  
## Iterating Through the Members of a Collection in Visual C#  
 This code example iterates through the <xref:Microsoft.AnalysisServices.Server.Databases%2A> collection property and displays all database connections to the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
```csharp  
//Connect to the local, default instance of SQL Server.   
{   
Server srv = default(Server);   
srv = new Server();   
int count = 0;   
int total = 0;   
//Iterate through the databases and call the GetActiveDBConnectionCount method.   
Database db = default(Database);   
foreach ( db in srv.Databases) {   
  count = srv.GetActiveDBConnectionCount(db.Name);   
  total = total + count;   
  //Display the number of connections for each database.   
  Console.WriteLine(count + " connections on " + db.Name);   
}   
//Display the total number of connections on the instance of SQL Server.   
Console.WriteLine("Total connections =" + total);   
}   
```  
  
  
