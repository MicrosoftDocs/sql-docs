---
title: "Get Help SQL Server PowerShell | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Help [PowerShell]"
  - "Help [SQL Server], PowerShell"
  - "PowerShell [SQL Server], help"
ms.assetid: 968c316d-db83-4c24-8ea6-9f18736842f7
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Get Help SQL Server PowerShell
  There are several sources of information about using the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provider for Windows PowerShell and cmdlets. This includes the help that is available in the Windows PowerShell environment.  
  
## Before You Begin  
 To learn about Windows PowerShell, see [Windows PowerShell Getting Started Guide](https://technet.microsoft.com/library/hh857337.aspx).  
  
 For an overview of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] cmdlets and provider, see [SQL Server PowerShell](../powershell/sql-server-powershell.md).  
  
### Help in the Windows PowerShell Environment  
 Use the **Get-Help** cmdlet to get help in the Windows PowerShell environment. **Get-Help** provides basic help for the Windows PowerShell language and the various cmdlets and providers available in Windows PowerShell.  
  
 For more information on the ways you can use **Get-Help**, see [Get-Help: Getting Help](https://go.microsoft.com/fwlink/?LinkId=102136).  
  
### SQL Server PowerShell Provider Help  
 The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] PowerShell provider implements several folders on a SQLSERVER virtual drive, such as the SQLSERVER:\SQL and SQLSERVER:\DAC folders. Each folder is associated with one of the SQL Server manageability object models. While you can list the methods and properties associated with each node in a SQL Server path, you cannot get help for them in the PowerShell environment. For a table of the folders with links to the associated programming reference, see [SQL Server PowerShell Provider](../powershell/sql-server-powershell-provider.md).  
  
### Invoke-Sqlcmd Help  
 The **Invoke-Sqlcmd** cmdlet takes as input any query or script file that can be run by the **sqlcmd** utility. You can use **Get-Help** to get information about **Invoke-Sqlcmd** and its parameters, but there is no **Get-Help** coverage for the **sqlcmd** queries.  
  
 The *-Query* or *-QueryFromFile* input can contain:  
  
-   **sqlcmd** variables and commands. For information about these variables and commands, see the Remarks section of [sqlcmd Utility](../tools/sqlcmd-utility.md).  
  
-   [!INCLUDE[tsql](../includes/tsql-md.md)] statements. For more information about the [!INCLUDE[tsql](../includes/tsql-md.md)] language, see [Transact-SQL Reference &#40;Database Engine&#41;](/sql/t-sql/language-reference).  
  
-   XQuery statements. For more information about the XQuery language supported by [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], see [XQuery Language Reference &#40;SQL Server&#41;](/sql/xquery/xquery-language-reference-sql-server).  
  
## Get Help for a SQL Server cmdlet  
 **To get help for a cmdlet**  
  
-   Run Get-Help specifying the name of the cmdlet and the level of help to be returned.  
  
### Example: cmdlet Get-Help  
 The following examples return various levels of help for **Invoke-Sqlcmd**:  
  
```  
## Get the basic help.  
Get-Help Invoke-Sqlcmd  
  
## Get the full help.  
Get-Help Invoke-Sqlcmd -Full  
  
## Get the parameter descriptions.  
Get-Help Invoke-Sqlcmd -Parameter *  
  
## Get the code examples.  
Get-Help Invoke-Sqlcmd -Examples  
  
## Get the syntax diagram.  
Get-Help Invoke-Sqlcmd -Syntax  
```  
  
## Get a List of Providers  
 **To get a list of active providers**  
  
1.  Run Get-Help specifying the provider category.  
  
 For more information about getting provider help in Windows PowerShell, see [Drives and Providers](https://go.microsoft.com/fwlink/?LinkId=102137).  
  
### Example: Get a List of Providers  
 This code returns a list of the providers currently enabled in your Windows PowerShell session:  
  
```  
Get-Help -Category provider  
```  
  
## Get Help About the SQL Server Provider  
 **To get help about the provider**  
  
1.  Run Get-Help specifying the name SQLServer  
  
### Example: Get SQL Server Provider Help  
 This example returns basic information about the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provider:  
  
```  
Get-Help SQLServer  
```  
  
## List Methods and Properties  
 **To list the methods and properties for a node in a SQL Server provider path**  
  
1.  Either CD to a node in the SQL Server path, or create a variable set to that location.  
  
2.  Run the **Get-Member** cmdlet with the -Type parameter set to either Methods or Properties  
  
### Examples: Listing Methods and Properties  
 This example lists the methods supported for the Databases node:  
  
```  
Set-Location SQL:\MyComputer\DEFAULT\Databases  
Get-Item . | Get-Member -Type Methods  
```  
  
 This example lists the properties for a variable that has been set to an SMO Table object:  
  
```  
$MyVar = New-Object Microsoft.SqlServer.Management.SMO.Table  
$MyVar | Get-Member -Type Properties  
```  
  
## See Also  
 [SQL Server PowerShell Provider](../powershell/sql-server-powershell-provider.md)   
 [Use the Database Engine cmdlets](../../2014/database-engine/use-the-database-engine-cmdlets.md)  
  
  
