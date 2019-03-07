---
title: "Convert URNs to SQL Server Provider Paths | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: c9b1b8f1-b117-4e87-9704-2170f62c5c8b
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Convert URNs to SQL Server Provider Paths
  The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Management Object model (SMO) builds Uniform Resource Names (URN) for its objects. Each URN uniquely identifies a SMO object, and can be converted to a SQL Server PowerShell provider path by using the `Convert-UrnToPath` cmdlet.  
  
## Converting URNs to Paths  
 Each URN has the same information as a path to the object, but in a different form. For example, this is the path to a table:  
  
 SQLSERVER:\SQL\MyComputer\DEFAULT\Databases\AdventureWorks2012\Tables\Person.Address  
  
 And this is the URN to the same object:  
  
 Server[@Name='MyComputer']\Database[@Name='AdventureWorks2012']\Table[@Name='Address' and @Schema='Person']  
  
 If you have created a SMO object in a PowerShell script, you can reference the `Urn` property to get the URN for the object, and then use the `Convert-UrnToPath` cmdlet to convert the SMO URN string to a Windows PowerShell path. You can then use the provider to navigate to different locations on the path.  
  
 If node names contain extended characters that are not supported in Windows PowerShell path names, `Convert-UrnToPath` encodes them in their hexadecimal representation. For example "My:Table" is returned as "My%3ATable".  
  
 For examples of using the cmdlet, in Windows PowerShell, run:  
  
```  
Get-Help Convert-UrnToPath -Examples  
```  
  
## See Also  
 [Query Expressions and Uniform Resource Names](../powershell/query-expressions-and-uniform-resource-names.md)   
 [SQL Server PowerShell Provider](../powershell/sql-server-powershell-provider.md)   
 [SQL Server PowerShell](../powershell/sql-server-powershell.md)  
  
  
