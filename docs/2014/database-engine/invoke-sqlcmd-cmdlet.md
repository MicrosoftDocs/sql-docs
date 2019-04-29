---
title: "Invoke-Sqlcmd cmdlet | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "PowerShell [SQL Server], Invoke-Sqlcmd"
  - "Cmdlets [SQL Server], Invoke-Sqlcmd"
  - "Invoke-Sqlcmd cmdlet"
  - "sqlcmd utility, PowerShell"
ms.assetid: 0c74d21b-84a5-4fa4-be51-90f0f7230044
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Invoke-Sqlcmd cmdlet
  **Invoke-Sqlcmd** is a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] cmdlet that runs scripts that contain statements from the languages ([!INCLUDE[tsql](../includes/tsql-md.md)] and XQuery) and commands that are supported by the **sqlcmd** utility.  
  
## Using Invoke-Sqlcmd  
 The **Invoke-Sqlcmd** cmdlet lets you run your **sqlcmd** script files in a Windows PowerShell environment. Much of what you can do with **sqlcmd** can also be done using **Invoke-Sqlcmd**.  
  
 This is an example of calling Invoke-Sqlcmd to execute a simple query, similar to specifying **sqlcmd** with the **-Q** and **-S** options:  
  
```  
Invoke-Sqlcmd -Query "SELECT GETDATE() AS TimeOfQuery;" -ServerInstance "MyComputer\MyInstance"  
```  
  
 This is an example of calling **Invoke-Sqlcmd**, specifying an input file and piping the output to a file This is similar to specifying **sqlcmd** with the **-i** and **-o** options:  
  
```  
Invoke-Sqlcmd -InputFile "C:\MyFolder\TestSQLCmd.sql" | Out-File -filePath "C:\MyFolder\TestSQLCmd.rpt"  
```  
  
 This is an example of using a Windows PowerShell array to pass multiple **sqlcmd** scripting variables to **Invoke-Sqlcmd**. The "$" characters identifying the **sqlcmd** scripting variables in the SELECT statement have been escaped by using the PowerShell back-tick "`" escape character:  
  
```  
$MyArray = "MyVar1 = 'String1'", "MyVar2 = 'String2'"  
Invoke-Sqlcmd -Query "SELECT `$(MyVar1) AS Var1, `$(MyVar2) AS Var2;" -Variable $MyArray  
```  
  
 This is an example of using the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provider for Windows PowerShell to navigate to an instance of the [!INCLUDE[ssDE](../includes/ssde-md.md)], and then using the Windows PowerShell **Get-Item** cmdlet to retrieve the SMO Server object for the instance and passing it to **Invoke-Sqlcmd**:  
  
```  
Set-Location SQLSERVER:\SQL\MyComputer\MyInstance  
Invoke-Sqlcmd -Query "SELECT GETDATE() AS TimeOfQuery;" -ServerInstance (Get-Item .)  
```  
  
 The -Query parameter is positional and does not have to be named. If the first string that is passed to **Invoke-Sqlcmd**: is unnamed, it is treated as the -Query parameter.  
  
```  
Invoke-Sqlcmd "SELECT GETDATE() AS TimeOfQuery;" -ServerInstance "MyComputer\MyInstance"  
```  
  
## Path Context in Invoke-Sqlcmd  
 If you do not use the -Database parameter, the database context for Invoke-Sqlcmd is set by the path that is active when the cmdlet is called.  
  
|Path|Database Context|  
|----------|----------------------|  
|Starts with a drive other than SQLSERVER:|The default database for the login ID in the default instance on the local computer.|  
|SQLSERVER:\SQL|The default database for the login ID in the default instance on the local computer.|  
|SQLSERVER:\SQL\ComputerName|The default database for the login ID in the default instance on the specified computer.|  
|SQLSERVER:\SQL\ComputerName\InstanceName|The default database for the login ID in the specified instance on the specified computer.|  
|SQLSERVER:\SQL\ComputerName\InstanceName\Databases|The default database for the login ID in the specified instance on the specified computer.|  
|SQLSERVER:\SQL\ComputerName\InstanceName\Databases\DatabaseName|The specified database in the specified instance on the specified computer. This also applies to longer paths, such as a path that specifies the Tables and Columns node within a database.|  
  
 For example, assume that the default database for your Windows account in the default instance of the local computer is master. Then, the following commands would return master:  
  
```  
Set-Location SQLSERVER:\SQL  
Invoke-Sqlcmd "SELECT DB_NAME() AS DatabaseName;"  
```  
  
 The following commands would return [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)]:  
  
```  
Set-Location SQLSERVER:\SQL\MyComputer\DEFAULT\Databases\AdventureWorks2012\Tables\Person.Person  
Invoke-Sqlcmd "SELECT DB_NAME() AS DatabaseName;"  
```  
  
 Invoke-Sqlcmd provides a warning when it uses the path database context. You can use the -SuppressProviderContextWarning parameter to turn off the warning message. You can use the -IgnoreProviderContext parameter to tell Invoke-Sqlcmd to always use the default database for the login.  
  
## Comparing Invoke-Sqlcmd and the sqlcmd Utility  
 **Invoke-Sqlcmd** can be used to run many of the scripts that can be run using the **sqlcmd** utility. However, **Invoke-Sqlcmd** runs in a Windows PowerShell environment which is different than the command prompt environment that **sqlcmd** is run in. The behavior of **Invoke-Sqlcmd** has been modified to work in a Windows PowerShell environment.  
  
 Not all of the **sqlcmd** commands are implemented in **Invoke-Sqlcmd**. Commands that are not implemented include the following: **:!!**, **:connect**, **:error**, **:out**, **:ed**, **:list**, **:listvar**, **:reset**, **:perftrace**, and **:serverlist**.  
  
 **Invoke-Sqlcmd** does not initialize the **sqlcmd** environment or scripting variables such as SQLCMDDBNAME or SQLCMDWORKSTATION.  
  
 **Invoke-Sqlcmd** does not display messages, such as the output of PRINT statements, unless you specify the Windows PowerShell **-Verbose** common parameter. For example:  
  
```  
Invoke-Sqlcmd -Query "PRINT N'abc';" -Verbose  
```  
  
 Not all of the **sqlcmd** parameters are needed in a PowerShell environment. For example, Windows PowerShell formats all output from cmdlets, so the **sqlcmd** parameters specifying formatting options are not implemented in **Invoke-Sqlcmd**. The follwoing table shows the relationship between the **Invoke-Sqlcmd** parameters and **sqlcmd** options:  
  
|Description|sqlcmd option|Invoke-Sqlcmd parameter|  
|-----------------|-------------------|------------------------------|  
|Server and instance name.|-S|-ServerInstance|  
|The initial database to use.|-d|-Database|  
|Run the specified query and exit.|-Q|-Query|  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication login ID.|-U|-Username|  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication password.|-P|-Password|  
|Variable definition.|-v|-Variable|  
|Query timeout interval.|-t|-QueryTimeout|  
|Stop running on an error|-b|-AbortOnError|  
|Dedicated Administrator Connection.|-A|-DedicatedAdministratorConnection|  
|Disable interactive commands, startup script, and environment variables.|-X|-DisableCommands|  
|Disable variable substitution.|-x|-DisableVariables|  
|Minimum severity level to report.|-V|-SeverityLevel|  
|Minimum error level to report.|-m|-ErrorLevel|  
|Login timeout interval.|-l|-ConnectionTimeout|  
|Hostname.|-H|-HostName|  
|Change password and exit.|-Z|-NewPassword|  
|Input file containing a query|-i|-InputFile|  
|Maximum length of character output.|-w|-MaxCharLength|  
|Maximum length of binary output.|-w|-MaxBinaryLength|  
|Connect using SSL encryption.|No parameter|-EncryptConnection|  
|Display errors|No parameter|-OutputSqlErrors|  
|Output messages to stderr.|-r|No parameter|  
|Use client's regional settings|-R|No parameter|  
|Run the specified query and remain running.|-q|No parameter|  
|Code page to use for output data.|-f|No parameter|  
|Change a password and remain running|-z|No parameter|  
|Packet size|-a|No parameter|  
|Column separator|-s|No parameter|  
|Control output headers|-h|No parameter|  
|Specify control characters|-k|No parameter|  
|Fixed length display width|-Y|No parameter|  
|Variable length display width|-y|No parameter|  
|Echo input|-e|No parameter|  
|Enable quoted identifiers|-I|No parameter|  
|Remove trailing spaces|-W|No parameter|  
|List instances|-L|No parameter|  
|Format output as Unicode|-u|No parameter|  
|Print statistics|-p|No parameter|  
|Command end|-c|No parameter|  
|Connect using Windows Authentication|-E|No parameter|  
  
## See Also  
 [Use the Database Engine cmdlets](../../2014/database-engine/use-the-database-engine-cmdlets.md)   
 [sqlcmd Utility](../tools/sqlcmd-utility.md)   
 [Use the sqlcmd Utility](../relational-databases/scripting/sqlcmd-use-the-utility.md)  
  
  
