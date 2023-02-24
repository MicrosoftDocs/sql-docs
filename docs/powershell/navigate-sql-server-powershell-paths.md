---
title: Navigate SQL Server PowerShell Paths
description: Learn how to use PowerShell cmdlets to navigate the path structures that represent the hierarchy of objects supported by a PowerShell provider. 
ms.service: sql
ms.subservice: sql-server-powershell
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: matteot, drskwier
ms.custom: ""
ms.date: 10/14/2020
---

# Navigate SQL Server PowerShell Paths

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

The [!INCLUDE[ssDE](../includes/ssde-md.md)] PowerShell provider exposes the set of objects in an instance of SQL Server in a structure similar to a file path. You can use Windows PowerShell cmdlets to navigate the provider path, and create custom drives to shorten the path you have to type.  

[!INCLUDE [sql-server-powershell-version](../includes/sql-server-powershell-version.md)]

Windows PowerShell implements cmdlets to navigate the path structure that represent the hierarchy of objects supported by a PowerShell provider. When you have navigated to a node in the path, you can use other cmdlets to perform basic operations on the current object. Because the cmdlets are used frequently, they have short, canonical aliases. There is also one set of aliases that maps the cmdlets to similar command prompt commands, and another set for UNIX shell commands.  

The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provider implements a subset of the provider cmdlets, shown in the following table:  

|cmdlet|Canonical alias|cmd alias|UNIX shell alias|Description|  
|------------|---------------------|---------------|----------------------|-----------------|  
|**Get-Location**|**gl**|**pwd**|**pwd**|Gets the current node.|  
|**Set-Location**|**sl**|**cd, chdir**|**cd, chdir**|Changes the current node.|  
|**Get-ChildItem**|**gci**|**dir**|**ls**|Lists the objects stored at the current node.|  
|**Get-Item**|**gi**|||Returns the properties of the current item.|  
|**Rename-Item**|**rni**|**rn**|**ren**|Renames an object.|  
|**Remove-Item**|**ri**|**del, rd**|**rm, rmdir**|Removes an object.|  

> [!IMPORTANT]
> Some [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] identifiers (object names) contain characters that Windows PowerShell does not support in path names. For more information about how to use names that contain these characters, see [SQL Server Identifiers in PowerShell](sql-server-identifiers-in-powershell.md).  

## SQL Server Information Returned by Get-ChildItem  

The information returned by **Get-ChildItem** (or its **dir** and **ls** aliases) depends on your location in a SQLSERVER: path.  

|Path location|Get-ChildItem results|
|-------------------|----------------------------|
|SQLSERVER:\SQL|Returns the name of the local computer. If you have used SMO or WMI to connect to instances of the [!INCLUDE[ssDE](../includes/ssde-md.md)] on other computers, those computers are also listed.|  
|SQLSERVER:\SQL\\*ComputerName*|The list of instances of the [!INCLUDE[ssDE](../includes/ssde-md.md)] on the computer.|  
|SQLSERVER:\SQL\\*ComputerName*\\*InstanceName*|The list of top-level object types in the instance, such as Endpoints, Certificates, and Databases.|  
|Object class node, such as Databases|The list of objects of that type, such as the list of databases: master, model, AdventureWorks20008R2.|  
|Object name node, such as AdventureWorks2012|The list of object types contained within the object. For example, a database would list object types such as tables and views.|  

By default, **Get-ChildItem** does not list any system objects. Use the *Force* parameter to see system objects, such as the objects in the **sys** schema.  

### Custom Drives

Windows PowerShell lets users define virtual drives, which are referred to as PowerShell drives. These drives map over the starting nodes of a path statement. They are typically used to shorten paths that are typed frequently. SQLSERVER: paths can get long, taking space in the Windows PowerShell window and requiring much typing. If you are going to do a lot of work at a particular path node, you can define a custom Windows PowerShell drive that maps to that node.  
  
## Use PowerShell Cmdlet Aliases

**Use a cmdlet alias**

- Instead of typing a full cmdlet name, type a shorter alias, or one that maps to a familiar commend prompt command.  

### Alias Example (PowerShell)  

For example, you can use one of the following sets of cmdlets or aliases to retrieve a listing of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instances available to you by navigating to the SQLSERVER:\SQL folder and requesting the list of child items for the folder:  
  
```powershell  
## Shows using the full cmdet name.  
Set-Location SQLSERVER:\SQL  
Get-ChildItem  
  
## Shows using canonical aliases.  
sl SQLSERVER:\SQL  
gci  
  
## Shows using command prompt aliases.  
cd SQLSERVER:\SQL  
dir  
  
## Shows using Unix shell aliases.  
cd SQLSERVER:\SQL  
ls  
```
  
## Use Get-ChildItem  
 **Return information by using Get-Childitem**  
  
1.  Navigate to the node for which you want a list of children  
  
2.  Run Get-Childitem to get the list.  
  
### Get-ChildItem Example (PowerShell)  
 These examples illustrate the information returned by Get-Childitem for different nodes in a SQL Server provider path.  
  
```powershell  
## Return the current computer and any computer  
## to which you have made a SQL or WMI connection.  
Set-Location SQLSERVER:\SQL  
Get-ChildItem  
  
## List the instances of the Database Engine on the local computer.  
  
Set-Location SQLSERVER:\SQL\localhost  
Get-ChildItem  
  
## Lists the categories of objects available in the  
## default instance on the local computer.  
Set-Location SQLSERVER:\SQL\localhost\DEFAULT  
Get-ChildItem  
  
## Lists the databases from the local default instance.  
## The force parameter is used to include the system databases.  
Set-Location SQLSERVER:\SQL\localhost\DEFAULT\Databases  
Get-ChildItem -force  
```
  
## Create a Custom Drive  
 **Create and use a custom drive**  
  
1.  Use **New-PSDrive** to define a custom drive. Use the **Root** parameter to specify the path that is represented by the custom drive name.  
  
2.  Reference the custom drive name in path navigation cmdlets such as **Set-Location**.  
  
### Custom Drive Example (PowerShell)  
 This example creates a virtual drive named AWDB that maps to the node for a deployed copy of the AdventureWorks2012 sample database. The virtual drive is then used to navigate to a table in the database.  
  
```powershell  
## Create a new virtual drive.  
New-PSDrive -Name AWDB -Root SQLSERVER:\SQL\localhost\DEFAULT\Databases\AdventureWorks2012  
  
## Use AWDB: to navigate to a specific table.  
Set-Location AWDB:\Tables\Purchasing.Vendor  
```
  
## See Also

- [SQL Server PowerShell Provider](sql-server-powershell-provider.md)
- [Work With SQL Server PowerShell Paths](work-with-sql-server-powershell-paths.md)
- [SQL Server PowerShell](sql-server-powershell.md)
