---
title: "Import the SQLPS Module | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: a972c56e-b2af-4fe6-abbd-817406e2c93a
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Import the SQLPS Module
  The recommended way to manage [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] from PowerShell is to import the `sqlps` module into a Windows PowerShell 2.0 environment. The module loads and registers the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] snap-ins and manageability assemblies.  
  
1.  **Before You Begin:**  [Security](#Security)  
  
2.  **To load the module:**  [Load the sqlps Module](#LoadSqlps)  
  
## Before You Begin  
 After importing the `sqlps` module into Windows PowerShell, you can then:  
  
-   Interactively run Windows PowerShell commands.  
  
-   Run Windows PowerShell script files.  
  
-   Run [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] cmdlets.  
  
-   Use the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provider paths to navigate through the hierarchy of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] objects.  
  
-   Use the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] manageability object models (such as Microsoft.SqlServer.Management.Smo) to manage [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] objects.  
  
> [!NOTE]  
>  The verbs used in the names of two SQL Server cmdlets (`Encode-Sqlname` and `Decode-Sqlname`) do not match the approved verbs for Windows PowerShell 2.0. This has no effect on their operation, but Windows PowerShell raises a warning when the `sqlps` module is imported to a session.  
  
###  <a name="Security"></a> Security  
 By default, Windows PowerShell runs with the scripting execution policy set to **Restricted**, which prevents running any Windows PowerShell scripts. To load the `sqlps` module, you can use the `Set-ExecutionPolicy` cmdlet to enable running signed scripts, or any scripts. Only run scripts from trusted sources, and secure all input and output files using the appropriate NTFS permissions. For more information about enabling Windows PowerShell scripts, see [Running Windows PowerShell Scripts](https://docs.microsoft.com/powershell/scripting/setup/starting-windows-powershell?view=powershell-6#how-to-enable-windows-powershell-ise-on-earlier-releases-of-windows).  
  
##  <a name="LoadSqlps"></a> Load the sqlps Module  
 **To load the sqlps module in Windows PowerShell**  
  
1.  Use the `Set-ExecutionPolicy` cmdlet to set the appropriate script execution policy.  
  
2.  Use the `Import-Module` cmdlet to import the sqlps module. Specify the `DisableNameChecking` parameter if you want to suppress the warning about `Encode-Sqlname` and `Decode-Sqlname`.  
  
### Example (PowerShell)  
 This example loads the `sqlps` module with name checking turned off.  
  
```  
## Import the SQL Server Module.  
  
Import-Module "sqlps" -DisableNameChecking  
  
```  
  

  
## See Also  
 [SQL Server PowerShell](../powershell/sql-server-powershell.md)   
 [SQL Server PowerShell Provider](../powershell/sql-server-powershell-provider.md)   
 [Use the Database Engine cmdlets](../../2014/database-engine/use-the-database-engine-cmdlets.md)  
  
  
