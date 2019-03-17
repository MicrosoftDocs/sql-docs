---
title: "SQL Server Identifiers in PowerShell | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: scripting
ms.topic: conceptual
helpviewer_keywords: 
  - "Cmdlets [SQL Server], Encode-Sqlname"
  - "PowerShell [SQL Server], identifiers"
  - "Encode-Sqlname cmdlet"
  - "PowerShell [SQL Server], Encode-Sqlname"
  - "Decode-Sqlname cmdlet"
  - "PowerShell [SQL Server], Decode-Sqlname"
  - "identifiers [SQL Server], PowerShell"
  - "Cmdlets [SQL Server], Decode-Sqlname"
ms.assetid: 651099b0-33b4-453a-a864-b067f21eb8b9
author: stevestein
ms.author: sstein
manager: craigg
---
# SQL Server Identifiers in PowerShell
  The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provider for Windows PowerShell uses [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] identifiers in Windows PowerShell paths. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] identifiers can contain characters that Windows PowerShell does not support in paths. You must escape these characters or use special encoding for them when using the identifiers in Windows PowerShell paths.  
  
## SQL Server Identifiers in Windows PowerShell Paths  
 Windows PowerShell providers expose data hierarchies using a path structure similar to that used for the Windows file system. The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provider implements paths to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] objects. For the [!INCLUDE[ssDE](../includes/ssde-md.md)], the drive is set to SQLSERVER:, the first folder is set to \SQL, and the database objects are referenced as containers and items. This is the path to the Vendor table in the Purchasing schema of the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database in a default instance of the [!INCLUDE[ssDE](../includes/ssde-md.md)]:  
  
```  
SQLSERVER:\SQL\MyComputer\DEFAULT\Databases\AdventureWorks2012\Tables\Purchasing.Vendor  
```  
  
 [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] identifiers are the names of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] objects, such as table or column names. There are two types of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] identifiers:  
  
-   Regular identifiers are limited to a set of characters that are also supported in Windows PowerShell paths. These names can be used in Windows PowerShell paths without being changed.  
  
-   Delimited identifiers can use characters not supported in Windows PowerShell path names. Delimited identifiers are called bracketed identifiers if they are enclosed in brackets ([IdentifierName]) and quoted identifiers if they are enclosed in double quotes ("IdentifierName"). If a delimited identifier uses characters not supported in Windows PowerShell paths, the characters must either be encoded or escaped before using the identifier as a container or item name. Encoding works for all characters. Some characters, such as the colon character (:), cannot be escaped.  
  
## SQL Server Identifiers in cmdlets  
 Some [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] cmdlets have a parameter that takes an identifier as input. The parameter values are typically supplied as quoted string constants or in string variables. When identifiers are supplied as string constants or in variables, there is no conflict with the set of characters that are supported by Windows PowerShell.  
  
## SQL Server Identifier Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Describes how to specify an instance name, including the name of the computer the instance is running on.|[Specify Instances in the SQL Server PowerShell Provider](sql-server-powershell-provider.md)|  
|Describes how to specify the hexadecimal encoding for characters in delimited identifiers that are not supported in Windows PowerShell paths. Also describes how to decode the hexadecimal characters.|[Encode and Decode SQL Server Identifiers](encode-and-decode-sql-server-identifiers.md)|  
|Describes how to use the Windows PowerShell escape character for characters not supported in PowerShell paths.|[Escape SQL Server Identifiers](escape-sql-server-identifiers.md)|  
  
## See Also  
 [SQL Server PowerShell Provider](sql-server-powershell-provider.md)   
 [SQL Server PowerShell](sql-server-powershell.md)   
 [Database Identifiers](../relational-databases/databases/database-identifiers.md)  
  
  
