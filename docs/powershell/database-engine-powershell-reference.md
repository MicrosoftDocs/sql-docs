---
title: "Database Engine PowerShell Reference | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 3c379a43-c497-47dd-8e7d-2b015c068bb7
caps.latest.revision: 8
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Database Engine PowerShell Reference
  [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] includes a set of Windows PowerShell cmdlets for performing common actions in the [!INCLUDE[ssDE](../includes/ssde-md.md)]. In addition, Query Expressions and Uniform Resource Names (URN) can be converted to SQL Server PowerShell paths, or used to specify one or more objects in the [!INCLUDE[ssDE](../includes/ssde-md.md)].  
  
## Database Engine Cmdlets  
 [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] includes relatively few cmdlets for the [!INCLUDE[ssDE](../includes/ssde-md.md)]. Most PowerShell scripts working with the [!INCLUDE[ssDE](../includes/ssde-md.md)] use the SQL Server PowerShell provider and the manageability object models. For more information, see [SQL Server PowerShell Provider](../relational-databases/scripting/sql-server-powershell-provider.md).  
  
 To learn how to get help about the SQL Server cmdlets in a Windows PowerShell environment, see [Get Help SQL Server PowerShell](../relational-databases/scripting/get-help-sql-server-powershell.md).  
  
### In This Section  
 This section contains information about these cmdlets.  
  
|Description|Cmdlet|  
|-----------------|------------|  
|Runs Transact-SQL and XQuery scripts, such as scripts that can be run using the **sqlcmd** utility.|[Invoke-Sqlcmd cmdlet](../powershell/invoke-sqlcmd-cmdlet.md)|  
|Evaluates whether a Database Engine object complies with a Policy-based Management policy.|[Invoke-PolicyEvaluation cmdlet](../powershell/invoke-policyevaluation-cmdlet.md)|  
  
### Information About Other Cmdlets  
 The **Encode-Sqlname** and **Decode-Sqlname** cmdlets help you specify SQL Server identifiers that contain characters not supported in PowerShell paths. For more information, see [SQL Server Identifiers in PowerShell](../relational-databases/scripting/sql-server-identifiers-in-powershell.md).  
  
 Use the **Convert-UrnToPath** cmdlet to convert a Unique Resource Name for a [!INCLUDE[ssDE](../includes/ssde-md.md)] object to a path for the SQL Server PowerShell provider. For more information, see [Convert URNs to SQL Server Provider Paths](../relational-databases/scripting/convert-urns-to-sql-server-provider-paths.md).  
  
## Query Expressions and Unique Resource Names  
 Query expressions are strings that use syntax similar to XPath to specify a set of criteria that enumerate one or more objects in an object model hierarchy. A Unique Resource Name (URN) is a specific type of query expression string that uniquely identifies a single object. For more information, see [Query Expressions and Uniform Resource Names](../powershell/query-expressions-and-uniform-resource-names.md).  
  
## See Also  
 [SQL Server PowerShell](../relational-databases/scripting/sql-server-powershell.md)  
  
  