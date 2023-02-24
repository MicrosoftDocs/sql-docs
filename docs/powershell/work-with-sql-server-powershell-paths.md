---
title: Work With SQL Server PowerShell Paths
description: Learn how to manipulate and retrieve information using either cmdlets or the methods and properties of the object identified by the provider path.
ms.service: sql
ms.subservice: sql-server-powershell
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: matteot, drskwier
ms.custom: ""
ms.date: 03/14/2017
---

# Work With SQL Server PowerShell Paths

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

After you have navigated to a node in a [!INCLUDE[ssDE](../includes/ssde-md.md)] provider path, you can perform work or retrieve information by using the methods and properties from the
[!INCLUDE[ssDE](../includes/ssde-md.md)] management object associated with the node.  

[!INCLUDE [sql-server-powershell-version](../includes/sql-server-powershell-version.md)]

After you navigate to a node in a [!INCLUDE[ssDE](../includes/ssde-md.md)] provider path, you can perform two types of actions:  

- You can run Windows PowerShell cmdlets that operate on nodes, such as **Rename-Item**.  

- You can call the methods from the associated [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] management object model, such as SMO. For example, if you navigate to the Databases node in a path, you can use the methods and properties of the <xref:Microsoft.SqlServer.Management.Smo.Database> class.  

The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provider is used to manage the objects in an instance of the [!INCLUDE[ssDE](../includes/ssde-md.md)]. It is not used to work with the data in databases. If you have navigated to a table or view, you cannot use the provider to select, insert, update, or delete data. Use the **Invoke-Sqlcmd** cmdlet to query or change data in tables and views from the Windows PowerShell environment. For more information, see [Invoke-Sqlcmd cmdlet](/powershell/module/sqlserver/invoke-sqlcmd).  

##  <a name="ListPropMeth"></a> Listing Methods and Properties  

**Listing Methods and Properties**  

To view the methods and properties available for specific objects or object classes, use the **Get-Member** cmdlet.  

### Examples: Listing Methods and Properties

This example sets a Windows PowerShell variable to the SMO <xref:Microsoft.SqlServer.Management.Smo.Database> class and lists the methods and properties:  

```powershell
$MyDBVar = New-Object Microsoft.SqlServer.Management.SMO.Database  
$MyDBVar | Get-Member -Type Methods  
$MyDBVar | Get-Member -Type Properties  
```  
  
 You can also use **Get-Member** to list the methods and properties that are associated with the end node of a Windows PowerShell path.  
  
 This example navigates to the Databases node in a SQLSERVER: path and lists the collection properties:  
  
```powershell
Set-Location SQLSERVER:\SQL\localhost\DEFAULT\Databases  
Get-Item . | Get-Member -Type Properties  
```  
  
 This example navigates to the AdventureWorks2012 node in a SQLSERVER: path and lists the object properties:  
  
```powershell
Set-Location SQLSERVER:\SQL\localhost\DEFAULT\Databases\AdventureWorks2012  
Get-Item . | Get-Member -Type Properties  
```  

##  <a name="UsePropMeth"></a> Using Methods and Properties  

**Using SMO Methods and Properties**  

To perform work on objects from a [!INCLUDE[ssDE](../includes/ssde-md.md)] provider path, you can use SMO methods and properties.  

### Examples: Using Methods and Properties

This example uses the SMO **Schema** property to get a list of the tables from the Sales schema in AdventureWorks2012:  

```powershell
Set-Location SQLSERVER:\SQL\localhost\DEFAULT\Databases\AdventureWorks2012\Tables  
Get-ChildItem | where {$_.Schema -eq "Sales"}  
```

This example uses the SMO **Script** method to generate a script that contains the **CREATE VIEW** statements you must have to re-create the views in AdventureWorks2012:  

```powershell
Remove-Item C:\PowerShell\CreateViews.sql  
Set-Location SQLSERVER:\SQL\localhost\DEFAULT\Databases\AdventureWorks2012\Views  
foreach ($Item in Get-ChildItem) { $Item.Script() | Out-File -Filepath C:\PowerShell\CreateViews.sql -append }  
```

This example uses the SMO **Create** method to create a database, and then uses the **State** property to show whether the database exists:  

```powershell
Set-Location SQLSERVER:\SQL\localhost\DEFAULT\Databases  
$MyDBVar = New-Object Microsoft.SqlServer.Management.SMO.Database  
$MyDBVar.Parent = (Get-Item ..)  
$MyDBVar.Name = "NewDB"  
$MyDBVar.Create()  
$MyDBVar.State  
```

## See Also

- [SQL Server PowerShell Provider](sql-server-powershell-provider.md)
- [Navigate SQL Server PowerShell Paths](navigate-sql-server-powershell-paths.md)
- [Convert URNs to SQL Server Provider Paths](/powershell/module/sqlserver/Convert-UrnToPath)
- [SQL Server PowerShell](sql-server-powershell.md)