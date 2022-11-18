---
title: Manage Tab Completion (SQL Server PowerShell)
description: Learn how to control Windows PowerShell tab completion by making proper use of three variables in the SQL Server PowerShell modules.
ms.service: sql
ms.subservice: sql-server-powershell
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: matteot, drskwier
ms.custom: ""
ms.date: 10/14/2020
---

# Manage Tab Completion with SQL Server PowerShell

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] PowerShell snap-ins introduce three variables (**$SqlServerMaximumTabCompletion**, **$SqlServerMaximumChildItems**, and **$SqlServerIncludeSystemObjects**) to control Windows PowerShell tab completion. Tab completion reduces the amount of typing you must do by returning tables of items whose names start with the string you are typing.  

[!INCLUDE [sql-server-powershell-version](../includes/sql-server-powershell-version.md)]

With Windows PowerShell tab-completion, when you have typed part of a path or cmdlet name, you can hit the Tab key to get a list of the items whose names match what you have already typed. You can then select the item you want from the list without having to type the rest of the name.  

If you are working in a database that has many objects, the tab-completion lists can become large. Some [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] object types, such as views, also have large numbers of system objects.  

The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] snap-ins introduces three system variables that you can use to control the amount of information presented by tab-completion and **Get-ChildItem**.

## $SqlServerMaximumTabCompletion =** *n*

Specifies the maximum number of objects to include in a tab-completion list. If you select Tab at a path node having more than *n* objects, the tab-completion list is truncated at *n*. *n* is an integer. 0 is the default setting, and means there is no limit to the number of objects listed.  

## $SqlServerMaximumChildItems =** *n*

Specifies the maximum number of objects displayed by **Get-ChildItem**. If **Get-ChildItem** is run at a path node having more than *n* objects, the list is truncated at *n*. *n* is an integer. 0 is the default setting, and means there is no limit to the number of objects listed.  

## $SqlServerIncludeSystemObjects =** { **$True** | **$False** }

If **$True**, system objects are displayed by tab-completion and **Get-ChildItem**. If **$False**, no system objects are displayed. The default setting is **$False**.  

## Set the SQL Server Tab Completion Variables

For any of the variables you want to change from the default value, set the variable to the new value.  

### Example (PowerShell)

The following example sets all three variables and lists their settings:  

```powershell
$SqlServerMaximumTabCompletion = 20  
$SqlServerMaximumChildItems = 10  
$SqlServerIncludeSystemObjects = $False  
dir variable:sqlserver*  
```

## See Also

- [Install SQL Server PowerShell](download-sql-server-ps-module.md)
- [SQL Server PowerShell](sql-server-powershell.md)