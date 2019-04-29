---
title: "Use the Database Engine cmdlets | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Cmdlets [SQL Server], Encode-Sqlname"
  - "Encode-Sqlname cmdlet"
  - "PowerShell [SQL Server], Encode-Sqlname"
  - "Convert-UrnToPath cmdlet"
  - "PowerShell [SQL Server], cmdlets"
  - "Cmdlets [SQL Server]"
  - "PowerShell [SQL Server], Convert-UrnToPath"
  - "Cmdlets [SQL Server], Convert-UrnToPath"
  - "Decode-Sqlname cmdlet"
  - "PowerShell [SQL Server], Decode-Sqlname"
  - "Cmdlets [SQL Server], Decode-Sqlname"
ms.assetid: 720aa982-09ae-41a3-b603-a91004cfbe3e
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Use the Database Engine cmdlets
  Windows PowerShell cmdlets are single-function commands that typically have a verb-noun naming convention, such as **Get-Help** or **Set-MachineName**. The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provider for Windows PowerShell supplies cmdlets specific to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
## Database Engine cmdlets  
 [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] implements a small number of cmdlets for the [!INCLUDE[ssDE](../includes/ssde-md.md)]. These cmdlets are primarily used to run existing Transact-SQL scripts from new PowerShell scripts, evaluate policy-based management policies, and aid in specifying SQL Server identifiers in SQL Server Provider paths.  
  
 Most Windows PowerShell scripts work with the [!INCLUDE[ssDE](../includes/ssde-md.md)] by using the SQL Server PowerShell provider and the SQL Server manageability object models. For more information, see [SQL Server PowerShell](../powershell/sql-server-powershell.md).  
  
### Get Cmdlet Help  
 In the Windows PowerShell environment, the **Get-Help** cmdlet provides help information for each cmdlet. **Get-Help** returns information such as the syntax, parameter definitions, input and output types, and a description of the action performed by the cmdlet. For more information, see [Get Help SQL Server PowerShell](../../2014/database-engine/get-help-sql-server-powershell.md).  
  
### Partial Parameter Names  
 You do not have to specify the entire name of a cmdlet parameter. You only have to specify enough of the name to uniquely separate it from the other parameters that are supported by the cmdlet. For example, these examples show three ways of specifying the **Invoke-Sqlcmd -QueryTimeout** parameter:  
  
```  
Invoke-Sqlcmd -Query "SELECT @@VERSION;" -QueryTimeout 3  
Invoke-Sqlcmd -Query "SELECT @@VERSION;" -QueryTime 3  
Invoke-Sqlcmd -Query "SELECT @@VERSION;" -QueryT 3  
```  
  
## Database Engine cmdlet Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Describes using **Invoke-Sqlcmd** to run **sqlcmd** scripts or commands that contain [!INCLUDE[tsql](../includes/tsql-md.md)] or XQuery statements. It can accept the **sqlcmd** input as either a character string input parameter, or as the name of a script file to open.|[Invoke-Sqlcmd cmdlet](../../2014/database-engine/invoke-sqlcmd-cmdlet.md)|  
|Describes using **Invoke-PolicyEvaluation** to report whether a target set of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] objects comply with the conditions that are defined in policy-based management policies. Optionally, the cmdlet can be used to reconfigure any settable options in the target objects that do not comply with the policy conditions.|[Invoke-PolicyEvaluation cmdlet](../../2014/database-engine/invoke-policyevaluation-cmdlet.md)|  
|Describes using `Encode-Sqlname` and `Decode-Sqlname` to handle SQL Server identifiers that contain characters not supported in Windows PowerShell paths.|[Encode and Decode SQL Server Identifiers](../powershell/encode-and-decode-sql-server-identifiers.md)|  
|Describes using `Convert-UrnToPath` to convert a SQL Server Manageability Object Uniform Resource Name (URN) to the equivalent SQL Server Provider path.|[Convert URNs to SQL Server Provider Paths](../../2014/database-engine/convert-urns-to-sql-server-provider-paths.md)|  
  
## See Also  
 [SQL Server PowerShell Provider](../powershell/sql-server-powershell-provider.md)   
 [SQL Server PowerShell](../powershell/sql-server-powershell.md)   
 [Overview of PowerShell Cmdlets for AlwaysOn Availability Groups &#40;SQL Server&#41;](availability-groups/windows/overview-of-powershell-cmdlets-for-always-on-availability-groups-sql-server.md)  
  
  
