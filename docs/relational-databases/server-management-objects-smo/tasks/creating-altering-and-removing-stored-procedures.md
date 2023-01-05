---
description: "Creating, Altering, and Removing Stored Procedures"
title: "Creating, Altering, and Removing Stored Procedures"
ms.custom: seo-dt-2019
ms.date: "08/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 

ms.topic: "reference"
helpviewer_keywords: 
  - "stored procedures [SMO]"
ms.assetid: 2a072f9c-8f11-4364-ab71-3990735a8d66
author: "markingmyname"
ms.author: "maghan"
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Creating, Altering, and Removing Stored Procedures
[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

  In [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Management Objects (SMO), stored procedures are represented by the <xref:Microsoft.SqlServer.Management.Smo.StoredProcedure> object.  
  
 Creating a <xref:Microsoft.SqlServer.Management.Smo.StoredProcedure> object in SMO requires setting the <xref:Microsoft.SqlServer.Management.Smo.StoredProcedure.TextBody%2A> property to the [!INCLUDE[tsql](../../../includes/tsql-md.md)] script that defines the stored procedure. Parameters require the \@ prefix and must be created individually by using <xref:Microsoft.SqlServer.Management.Smo.StoredProcedureParameter> objects and adding to the <xref:Microsoft.SqlServer.Management.Smo.StoredProcedureParameter> collection of the <xref:Microsoft.SqlServer.Management.Smo.StoredProcedure> object.  
  
## Example  
 To use any code example that is provided, you will have to choose the programming environment, the programming template, and the programming language in which to create your application. For more information, see [Create a Visual C&#35; SMO Project in Visual Studio .NET](../../../relational-databases/server-management-objects-smo/how-to-create-a-visual-csharp-smo-project-in-visual-studio-net.md).  
  
## Creating, Altering, and Removing a Stored Procedure in Visual Basic  
 This code example shows how to create a stored procedure for the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal-md.md)] database. The example returns the last name of an employee when it is given the employee ID number. The stored procedure requires one input parameter to specify the employee ID number and one output parameter to return the last name of the employee.  
  
```VBNET
'Connect to the local, default instance of SQL Server.
Dim srv As Server
srv = New Server
'Reference the AdventureWorks2012 2008R2 database.
Dim db As Database
db = srv.Databases("AdventureWorks2012")
'Define a StoredProcedure object variable by supplying the parent database and name arguments in the constructor.
Dim sp As StoredProcedure
sp = New StoredProcedure(db, "GetLastNameByEmployeeID")
'Set the TextMode property to false and then set the other object properties.
sp.TextMode = False
sp.AnsiNullsStatus = False
sp.QuotedIdentifierStatus = False
'Add two parameters.
Dim param As StoredProcedureParameter
param = New StoredProcedureParameter(sp, "@empval", DataType.Int)
sp.Parameters.Add(param)
Dim param2 As StoredProcedureParameter
param2 = New StoredProcedureParameter(sp, "@retval", DataType.NVarChar(50))
param2.IsOutputParameter = True
sp.Parameters.Add(param2)
'Set the TextBody property to define the stored procedure.
Dim stmt As String
stmt = " SELECT @retval = (SELECT LastName FROM Person.Person AS p JOIN HumanResources.Employee AS e ON p.BusinessEntityID = e.BusinessEntityID AND e.BusinessEntityID = @empval )"
sp.TextBody = stmt
'Create the stored procedure on the instance of SQL Server.
sp.Create()
'Modify a property and run the Alter method to make the change on the instance of SQL Server.   
sp.QuotedIdentifierStatus = True
sp.Alter()
'Remove the stored procedure.
sp.Drop()
``` 
  
## Creating, Altering, and Removing a Stored Procedure in Visual C#  
 This code example shows how to create a stored procedure for the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal-md.md)] database. The example returns the last name of an employee when it is given the employee ID number (`BusinessEntityID`). The stored procedure requires one input parameter to specify the employee ID number and one output parameter to return the last name of the employee.  
  
```csharp  
{  
            //Connect to the local, default instance of SQL Server.   
            Server srv;  
            srv = new Server();  
            //Reference the AdventureWorks2012 database.   
            Database db;  
            db = srv.Databases["AdventureWorks2012"];  
            //Define a StoredProcedure object variable by supplying the parent database and name arguments in the constructor.   
            StoredProcedure sp;  
            sp = new StoredProcedure(db, "GetLastNameByBusinessEntityID");  
            //Set the TextMode property to false and then set the other object properties.   
            sp.TextMode = false;  
            sp.AnsiNullsStatus = false;  
            sp.QuotedIdentifierStatus = false;  
            //Add two parameters.   
            StoredProcedureParameter param;  
            param = new StoredProcedureParameter(sp, "@empval", DataType.Int);  
            sp.Parameters.Add(param);  
            StoredProcedureParameter param2;  
            param2 = new StoredProcedureParameter(sp, "@retval", DataType.NVarChar(50));  
            param2.IsOutputParameter = true;  
            sp.Parameters.Add(param2);  
            //Set the TextBody property to define the stored procedure.   
            string stmt;  
            stmt = " SELECT @retval = (SELECT LastName FROM Person.Person,HumanResources.Employee WHERE Person.Person.BusinessEntityID = HumanResources.Employee.BusinessentityID AND HumanResources.Employee.BusinessEntityID = @empval )";  
            sp.TextBody = stmt;  
            //Create the stored procedure on the instance of SQL Server.   
            sp.Create();  
            //Modify a property and run the Alter method to make the change on the instance of SQL Server.   
            sp.QuotedIdentifierStatus = true;  
            sp.Alter();  
            //Remove the stored procedure.   
            sp.Drop();  
        }  
```  
  
## Creating, Altering, and Removing a Stored Procedure in PowerShell  
 This code example shows how to create a stored procedure for the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal-md.md)] database. The example returns the last name of an employee when it is given the employee ID number (`BusinessEntityID`). The stored procedure requires one input parameter to specify the employee ID number and one output parameter to return the last name of the employee.  
  
```powershell  
# Set the path context to the local, default instance of SQL Server and get a reference to AdventureWorks2012  
CD \sql\localhost\default\databases  
$db = get-item Adventureworks2012  
  
# Define a StoredProcedure object variable by supplying the parent database and name arguments in the constructor.   
$sp  = New-Object -TypeName Microsoft.SqlServer.Management.SMO.StoredProcedure `  
-argumentlist $db, "GetLastNameByBusinessEntityID"  
  
#Set the TextMode property to false and then set the other object properties.   
$sp.TextMode = $false  
$sp.AnsiNullsStatus = $false  
$sp.QuotedIdentifierStatus = $false  
  
# Add two parameters  
$type = [Microsoft.SqlServer.Management.SMO.Datatype]::Int  
$param  = New-Object -TypeName Microsoft.SqlServer.Management.SMO.StoredProcedureParameter `  
-argumentlist $sp,"@empval",$type  
$sp.Parameters.Add($param)  
  
$type = [Microsoft.SqlServer.Management.SMO.DataType]::NVarChar(50)  
$param2  = New-Object -TypeName Microsoft.SqlServer.Management.SMO.StoredProcedureParameter `  
-argumentlist $sp,"@retval",$type  
$param2.IsOutputParameter = $true  
$sp.Parameters.Add($param2)  
  
#Set the TextBody property to define the stored procedure.   
$sp.TextBody =  " SELECT @retval = (SELECT LastName FROM Person.Person,HumanResources.Employee WHERE Person.Person.BusinessEntityID = HumanResources.Employee.BusinessentityID AND HumanResources.Employee.BusinessEntityID = @empval )"  
  
# Create the stored procedure on the instance of SQL Server.   
$sp.Create()  
  
# Modify a property and run the Alter method to make the change on the instance of SQL Server.   
$sp.QuotedIdentifierStatus = $true  
$sp.Alter()  
  
#Remove the stored procedure.   
$sp.Drop()  
```  
  
## See Also  
 <xref:Microsoft.SqlServer.Management.Smo.StoredProcedure>  
  
  
