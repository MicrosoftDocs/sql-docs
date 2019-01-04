---
title: "Creating, Altering, and Removing Views | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: "reference"
helpviewer_keywords: 
  - "views [SMO]"
ms.assetid: 7d445c0e-77ef-4734-993b-e022de31df23
author: stevestein
ms.author: sstein
manager: craigg
---
# Creating, Altering, and Removing Views
  In [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Management Objects (SMO), [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] views are represented by the <xref:Microsoft.SqlServer.Management.Smo.View> object.  
  
 The <xref:Microsoft.SqlServer.Management.Smo.View.TextBody%2A> property of the <xref:Microsoft.SqlServer.Management.Smo.View> object defines the view. It is the equivalent of the [!INCLUDE[tsql](../../../includes/tsql-md.md)] SELECT statement for creating a view.  
  
## Example  
 To use any code example that is provided, you will have to choose the programming environment, the programming template, and the programming language in which to create your application. For more information, see [Create a Visual Basic SMO Project in Visual Studio .NET](../../../database-engine/dev-guide/create-a-visual-basic-smo-project-in-visual-studio-net.md) or [Create a Visual C&#35; SMO Project in Visual Studio .NET](../how-to-create-a-visual-csharp-smo-project-in-visual-studio-net.md).  
  
## Creating, Altering, and Removing a View in Visual Basic  
 This code sample shows how to create a view of two tables by using an inner join. The view is created by using text mode, so the <xref:Microsoft.SqlServer.Management.Smo.View.TextHeader%2A> property must be set.  
  
<!-- TODO: review snippet reference  [!CODE [SMO How to#SMO_VBViews1](SMO How to#SMO_VBViews1)]  -->  
  
## Creating, Altering, and Removing a View in Visual C#  
 This code sample shows how to create a view of two tables by using an inner join. The view is created by using text mode, so the <xref:Microsoft.SqlServer.Management.Smo.View.TextHeader%2A> property must be set.  
  
```  
{  
        //Connect to the local, default instance of SQL Server.   
        Server srv;   
        srv = new Server();   
        //Reference the AdventureWorks2012 database.   
        Database db;   
        db = srv.Databases["AdventureWorks2012"];   
        //Define a View object variable by supplying the parent database, view name and schema in the constructor.   
        View myview;   
        myview = new View(db, "Test_View", "Sales");   
        //Set the TextHeader and TextBody property to define the view.   
        myview.TextHeader = "CREATE VIEW [Sales].[Test_View] AS";   
        myview.TextBody = "SELECT h.SalesOrderID, d.OrderQty FROM Sales.SalesOrderHeader AS h INNER JOIN Sales.SalesOrderDetail AS d ON h.SalesOrderID = d.SalesOrderID";   
        //Create the view on the instance of SQL Server.   
        myview.Create();   
        //Remove the view.   
        myview.Drop();   
        }  
```  
  
## Creating, Altering, and Removing a View in PowerShell  
 This code sample shows how to create a view of two tables by using an inner join. The view is created by using text mode, so the <xref:Microsoft.SqlServer.Management.Smo.View.TextHeader%2A> property must be set.  
  
```  
# Set the path context to the local, default instance of SQL Server and get a reference to AdventureWorks2012  
CD \sql\localhost\default\databases  
$db = get-item Adventureworks2012  
  
# Define a View object variable by supplying the parent database, view name and schema in the constructor.   
$myview  = New-Object -TypeName Microsoft.SqlServer.Management.SMO.View `  
-argumentlist $db, "Test_View", "Sales"  
  
# Set the TextHeader and TextBody property to define the view.   
$myview.TextHeader = "CREATE VIEW [Sales].[Test_View] AS"  
$myview.TextBody ="SELECT h.SalesOrderID, d.OrderQty FROM Sales.SalesOrderHeader AS h INNER JOIN Sales.SalesOrderDetail AS d ON h.SalesOrderID = d.SalesOrderID"  
  
# Create the view on the instance of SQL Server.   
$myview.Create()  
  
# Remove the view.   
$myview.Drop();  
```  
  
## See Also  
 <xref:Microsoft.SqlServer.Management.Smo.View>  
  
  
