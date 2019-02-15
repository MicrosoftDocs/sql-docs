---
title: "Calling Methods | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: "reference"
helpviewer_keywords: 
  - "methods [SMO]"
  - "calling methods"
  - "SQL Server Management Objects, method calling"
  - "SMO [SQL Server], method calling"
ms.assetid: c88d5c5f-9ff0-4f84-b2b6-24c6b90fa15e
author: stevestein
ms.author: sstein
manager: craigg
---
# Calling Methods
  Methods perform specific tasks related to the object, such as issuing a `Checkpoint` on a database or requesting an enumerated list of logons for the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 Methods perform an operation on an object. Methods can take parameters and often have a return value. The return value can be a simple data type, a complex object, or a structure that contains many members.  
  
 Use exception handling to detect whether the method has been successful. For more information, see [Handling SMO Exceptions](handling-smo-exceptions.md).  
  
## Examples  
 [!INCLUDE[ssChooseProgEnv](../../../includes/sschooseprogenv-md.md)]  
  
## Using a Simple SMO Method in Visual Basic  
 In this example, the <xref:Microsoft.SqlServer.Management.Smo.Database.Create%2A> method takes no parameters and has no return value.  
  
<!-- TODO: review snippet reference  [!CODE [SMO How to#SMO_VBMethods1](SMO How to#SMO_VBMethods1)]  -->  
  
## Using a Simple SMO Method in Visual C#  
 In this example, the <xref:Microsoft.SqlServer.Management.Smo.Database.Create%2A> method takes no parameters and has no return value.  
  
```  
{   
//Connect to the local, default instance of SQL Server.   
Server srv;   
srv = new Server();   
//Define a Database object variable by supplying the parent server and the database name arguments in the constructor.   
Database db;   
db = new Database(srv, "Test_SMO_Database");   
//Call the Create method to create the database on the instance of SQL Server.   
db.Create();   
```  
  
 }  
  
## Using an SMO Method with a Parameter in Visual Basic  
 The <xref:Microsoft.SqlServer.Management.Smo.Table> object has a method called <xref:Microsoft.SqlServer.Management.Smo.Table.RebuildIndexes%2A>. This method requires a numeric parameter that specifies the `FillFactor`.  
  
```  
Dim srv As Server  
srv = New Server  
Dim tb As Table  
tb = srv.Databases("AdventureWorks2012").Tables("Employee", "HumanResources")  
tb.RebuildIndexes(70)  
```  
  
## Using an SMO Method with a Parameter in Visual C#  
 The <xref:Microsoft.SqlServer.Management.Smo.Table> object has a method called <xref:Microsoft.SqlServer.Management.Smo.Table.RebuildIndexes%2A>. This method requires a numeric parameter that specifies the `FillFactor`.  
  
```  
{   
Server srv = default(Server);   
srv = new Server();   
Table tb = default(Table);   
tb = srv.Databases("AdventureWorks2012").Tables("Employee", "HumanResources");   
tb.RebuildIndexes(70);   
}   
```  
  
## Using an Enumeration Method that Returns a DataTable Object in Visual Basic  
 This section describes how to call an enumeration method and how to handle the data in the returned <xref:System.Data.DataTable> object.  
  
 The <xref:Microsoft.SqlServer.Management.Smo.Server.EnumCollations%2A> method returns a <xref:System.Data.DataTable> object, which requires further navigation to access all available collation information about the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
```  
'Connect to the local, default instance of SQL Server.  
Dim srv As Server  
srv = New Server  
'Call the EnumCollations method and return collation information to DataTable variable.  
Dim d As DataTable  
'Select the returned data into an array of DataRow.  
d = srv.EnumCollations  
'Iterate through the rows and display collation details for the instance of SQL Server.  
Dim r As DataRow  
Dim c As DataColumn  
For Each r In d.Rows  
    Console.WriteLine("==")  
    For Each c In r.Table.Columns  
        Console.WriteLine(c.ColumnName + " = " + r(c).ToString)  
    Next  
Next  
```  
  
## Using an Enumeration Method that Returns a DataTable Object in Visual C#  
 This section describes how to call an enumeration method and how to handle the data in the returned <xref:System.Data.DataTable> object.  
  
 The <xref:Microsoft.SqlServer.Management.Smo.Server.EnumCollations%2A> method returns a system <xref:System.Data.DataTable> object. The <xref:System.Data.DataTable> object requires further navigation to access all available collation information about the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
```  
//Connect to the local, default instance of SQL Server.   
{   
Server srv = default(Server);   
srv = new Server();   
//Call the EnumCollations method and return collation information to DataTable variable.   
DataTable d = default(DataTable);   
//Select the returned data into an array of DataRow.   
d = srv.EnumCollations;   
//Iterate through the rows and display collation details for the instance of SQL Server.   
DataRow r = default(DataRow);   
DataColumn c = default(DataColumn);   
foreach ( r in d.Rows) {   
  Console.WriteLine("=========");   
  foreach ( c in r.Table.Columns) {   
    Console.WriteLine(c.ColumnName + " = " + r(c).ToString);   
  }   
}   
}   
```  
  
## Constructing an Object in Visual Basic  
 The constructor of any object can be called by using the `New` operator. The <xref:Microsoft.SqlServer.Management.Smo.Database> object constructor is overloaded and the version of the <xref:Microsoft.SqlServer.Management.Smo.Database> object constructor that is used in the sample takes two parameters: the parent <xref:Microsoft.SqlServer.Management.Smo.Server> object to which the database belongs, and a string that represents the name of the new database.  
  
<!-- TODO: review snippet reference  [!CODE [SMO How to#SMO_VBMethods4](SMO How to#SMO_VBMethods4)]  -->  
  
## Constructing an Object in Visual C#  
 The constructor of any object can be called by using the `New` operator. The <xref:Microsoft.SqlServer.Management.Smo.Database> object constructor is overloaded and the version of the <xref:Microsoft.SqlServer.Management.Smo.Database> object constructor that is used in the sample takes two parameters: the parent <xref:Microsoft.SqlServer.Management.Smo.Server> object to which the database belongs, and a string that represents the name of the new database.  
  
```  
{   
Server srv;   
srv = new Server();   
Table tb;   
tb = srv.Databases("AdventureWorks2012").Tables("Employee", "HumanResources");   
tb.RebuildIndexes(70);   
//Connect to the local, default instance of SQL Server.   
Server srv;   
srv = new Server();   
//Declare and define a Database object by supplying the parent server and the database name arguments in the constructor.   
Database d;   
d = new Database(srv, "Test_SMO_Database");   
//Create the database on the instance of SQL Server.   
d.Create();   
Console.WriteLine(d.Name);   
}  
```  
  
## Copying an SMO Object in Visual Basic  
 This code example uses the <xref:Microsoft.SqlServer.Management.Common.ServerConnection.Copy%2A> method to create a copy of the <xref:Microsoft.SqlServer.Management.Smo.Server> object. The <xref:Microsoft.SqlServer.Management.Smo.Server> object represents a connection to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
<!-- TODO: review snippet reference  [!CODE [SMO How to#SMO_VCMethods6](SMO How to#SMO_VCMethods6)]  -->  
  
## Copying an SMO Object in Visual C#  
 This code example uses the <xref:Microsoft.SqlServer.Management.Common.ServerConnection.Copy%2A> method to create a copy of the <xref:Microsoft.SqlServer.Management.Smo.Server> object. The <xref:Microsoft.SqlServer.Management.Smo.Server> object represents a connection to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
```  
{   
//Connect to the local, default instance of SQL Server.   
Server srv1;   
srv1 = new Server();   
//Modify the default database and the timeout period for the connection.   
srv1.ConnectionContext.DatabaseName = "AdventureWorks2012";   
srv1.ConnectionContext.ConnectTimeout = 30;   
//Make a second connection using a copy of the ConnectionContext property and verify settings.   
Server srv2;   
srv2 = new Server(srv1.ConnectionContext.Copy);   
Console.WriteLine(srv2.ConnectionContext.ConnectTimeout.ToString);   
}  
```  
  
## Monitoring Server Processes in Visual Basic  
 You can obtain the current status type information about the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] through enumeration methods. The code example uses the <xref:Microsoft.SqlServer.Management.Smo.Server.EnumProcesses%2A> method to discover information about the current processes. It also demonstrates how to work with the columns and rows in the returned <xref:System.Data.DataTable> object.  
  
<!-- TODO: review snippet reference  [!CODE [SMO How to#SMO_VBMethods5](SMO How to#SMO_VBMethods5)]  -->  
  
## Monitoring Server Processes in Visual C#  
 You can obtain the current status type information about the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] through enumeration methods. The code example uses the <xref:Microsoft.SqlServer.Management.Smo.Server.EnumProcesses%2A> method to discover information about the current processes. It also demonstrates how to work with the columns and rows in the returned <xref:System.Data.DataTable> object.  
  
```  
//Connect to the local, default instance of SQL Server.   
{   
Server srv = default(Server);   
srv = new Server();   
//Call the EnumCollations method and return collation information to DataTable variable.   
DataTable d = default(DataTable);   
//Select the returned data into an array of DataRow.   
d = srv.EnumProcesses;   
//Iterate through the rows and display collation details for the instance of SQL Server.   
DataRow r = default(DataRow);   
DataColumn c = default(DataColumn);   
foreach ( r in d.Rows) {   
  Console.WriteLine("=====");   
  foreach ( c in r.Table.Columns) {   
    Console.WriteLine(c.ColumnName + " = " + r(c).ToString);   
  }   
}   
}   
```  
  
## See Also  
 <xref:Microsoft.SqlServer.Management.Smo.Server>   
 <xref:Microsoft.SqlServer.Management.Common.ServerConnection>  
  
  
