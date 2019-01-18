---
title: "Creating, Altering, and Removing Defaults | Microsoft Docs"
ms.custom: ""
ms.date: "08/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 

ms.topic: "reference"
helpviewer_keywords: 
  - "defaults [SMO]"
ms.assetid: c30ac3b9-8150-4264-ba4c-c549f44261ab
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Creating, Altering, and Removing Defaults
[!INCLUDE[appliesto-ss-asdb-asdw-xxx-md](../../../includes/appliesto-ss-asdb-asdw-xxx-md.md)]

  In [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Management Objects (SMO), the default constraint is represented by the <xref:Microsoft.SqlServer.Management.Smo.Default> object.  
  
 The <xref:Microsoft.SqlServer.Management.Smo.DefaultRuleBase.TextBody%2A> property of the <xref:Microsoft.SqlServer.Management.Smo.Default> object is used to set the value to be inserted. This can be a constant or a [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement that returns a constant value, such as GETDATE(). The <xref:Microsoft.SqlServer.Management.Smo.DefaultRuleBase.TextBody%2A> property cannot be modified by using the <xref:Microsoft.SqlServer.Management.Smo.DefaultRuleBase.Alter%2A> method. Instead, the <xref:Microsoft.SqlServer.Management.Smo.Default> object must be dropped and re-created.  
  
## Example  
 To use any code example that is provided, you will have to choose the programming environment, the programming template, and the programming language in which to create your application. For more information, see [Create a Visual C&#35; SMO Project in Visual Studio .NET](../../../relational-databases/server-management-objects-smo/how-to-create-a-visual-csharp-smo-project-in-visual-studio-net.md).  
  
## Creating, Altering, and Removing a Default in Visual Basic  
 This code example shows how to create one default that is simple text, and another default that is a [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement. The default must be attached to the column by using the <xref:Microsoft.SqlServer.Management.Smo.DefaultRuleBase.BindToColumn%2A> method and detached by using the <xref:Microsoft.SqlServer.Management.Smo.DefaultRuleBase.UnbindFromColumn%2A> method.  
  
```VBNET
'Connect to the local, default instance of SQL Server.
Dim srv As Server
srv = New Server
'Reference the AdventureWorks2012 database.
Dim db As Database
db = srv.Databases("AdventureWorks2012")
'Define a Default object variable by supplying the parent database and the default name 
'in the constructor.
Dim def As [Default]
def = New [Default](db, "Test_Default2")
'Set the TextHeader and TextBody properties that define the default.
def.TextHeader = "CREATE DEFAULT [Test_Default2] AS"
def.TextBody = "GetDate()"
'Create the default on the instance of SQL Server.
def.Create()
'Declare a Column object variable and reference a column in the AdventureWorks2012 database.
Dim col As Column
col = db.Tables("SpecialOffer", "Sales").Columns("StartDate")
'Bind the default to the column.
def.BindToColumn("SpecialOffer", "StartDate", "Sales")
'Unbind the default from the column and remove it from the database.
def.UnbindFromColumn("SpecialOffer", "StartDate", "Sales")
def.Drop()
```
  
## Creating, Altering, and Removing a Default in Visual C#  
 This code example shows how to create one default that is simple text, and another default that is a [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement. The default must be attached to the column by using the <xref:Microsoft.SqlServer.Management.Smo.DefaultRuleBase.BindToColumn%2A> method and detached by using the <xref:Microsoft.SqlServer.Management.Smo.DefaultRuleBase.UnbindFromColumn%2A> method.  
  
```csharp  
{  
  
          Server srv = new Server();  
  
            //Reference the AdventureWorks2012 database.   
            Database  db = srv.Databases["AdventureWorks2012"];  
  
            //Define a Default object variable by supplying the parent database and the default name   
            //in the constructor.   
            Default def = new Default(db, "Test_Default2");  
  
            //Set the TextHeader and TextBody properties that define the default.   
            def.TextHeader = "CREATE DEFAULT [Test_Default2] AS";  
            def.TextBody = "GetDate()";  
  
            //Create the default on the instance of SQL Server.   
            def.Create();  
  
            //Bind the default to a column in a table in AdventureWorks2012  
            def.BindToColumn("SpecialOffer", "StartDate", "Sales");  
  
            //Unbind the default from the column and remove it from the database.   
            def.UnbindFromColumn("SpecialOffer", "StartDate", "Sales");  
            def.Drop();  
        }  
```  
  
## Creating, Altering, and Removing a Default in PowerShell  
 This code example shows how to create one default that is simple text, and another default that is a [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement. The default must be attached to the column by using the <xref:Microsoft.SqlServer.Management.Smo.DefaultRuleBase.BindToColumn%2A> method and detached by using the <xref:Microsoft.SqlServer.Management.Smo.DefaultRuleBase.UnbindFromColumn%2A> method.  
  
```powershell   
# Set the path context to the local, default instance of SQL Server and get a reference to AdventureWorks2012  
CD \sql\localhost\default\databases  
$db = get-item Adventureworks2012  
  
#Define a Default object variable by supplying the parent database and the default name in the constructor.  
$def = New-Object -TypeName Microsoft.SqlServer.Management.SMO.Default `  
-argumentlist $db, "Test_Default2"  
  
#Set the TextHeader and TextBody properties that define the default.   
$def.TextHeader = "CREATE DEFAULT [Test_Default2] AS"  
$def.TextBody = "GetDate()"  
  
#Create the default on the instance of SQL Server.   
$def.Create()  
  
#Bind the default to the column.   
$def.BindToColumn("SpecialOffer", "StartDate", "Sales")  
  
#Unbind the default from the column and remove it from the database.   
$def.UnbindFromColumn("SpecialOffer", "StartDate", "Sales")  
$def.Drop()  
```  
  
## See Also  
 <xref:Microsoft.SqlServer.Management.Smo.Default>  
  
  
