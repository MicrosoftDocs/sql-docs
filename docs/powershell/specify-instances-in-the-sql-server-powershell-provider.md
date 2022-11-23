---
title: "Specify instances in SQL Server PowerShell Provider"
description: Learn how to specify instances when using the SQL Server PowerShell provider. 
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: sql-server-powershell
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: matteot, drskwier
---

# Specify Instances in the SQL Server PowerShell Provider

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

The paths specified for the SQL Server PowerShell provider must identify the instance of the [!INCLUDE[ssDE](../includes/ssde-md.md)] and the computer it is running on. The syntax for specifying the computer and the instance must comply with both the rules for SQL Server identifiers and Windows PowerShell paths.  

[!INCLUDE [sql-server-powershell-version](../includes/sql-server-powershell-version.md)]

## Before You Begin  

The first node following the SQLSERVER:\SQL in a SQL Server provider path is the name of the computer that is running the instance of the [!INCLUDE[ssDE](../includes/ssde-md.md)]; for example:  
  
```powershell
SQLSERVER:\SQL\MyComputer  
```  
  
 If you are running Windows PowerShell on the same computer as the instance of the [!INCLUDE[ssDE](../includes/ssde-md.md)], you can use either localhost or (local) instead of the name of the computer. Scripts that use localhost or (local) can be run on any computer without having to be changed to reflect the different computer names.  
  
 You can run multiple instances of the [!INCLUDE[ssDE](../includes/ssde-md.md)] executable program on the same computer. The node following the computer name in a SQL Server provider path identifies the instance; for example:  
  
```  
SQLSERVER:\SQL\MyComputer\MyInstance  
```  
  
 Each computer can have one default instance of the [!INCLUDE[ssDE](../includes/ssde-md.md)]. You do not specify a name for the default instance when you install it. If you specify only a computer name in a connection string, you are connected to the default instance on that computer. All other instances on the computer must be named instances. You specify the instance name during setup, and connection strings must specify both the computer name and the instance name.  
  
###  <a name="LimitationsRestrictions"></a> Limitations and Restrictions  
 You cannot use a period (.) to specify the local computer in PowerShell scripts. The period is not supported because the period is interpreted as a command by PowerShell.  
  
 The parenthesis characters in (local) are normally treated as commands by Windows PowerShell. You must either encode them or escape them for use in a path, or enclose the path in double-quotation marks. For more information, see Encode and Decode SQL Server Identifiers.  
  
 The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provider requires that you always specify an instance name. For default instances, you must specify an instance name of DEFAULT.  
  
##  <a name="Examples"></a> Examples; Computer and Instance Names  
 This example uses localhost and DEFAULT to specify the default instance on the local computer:  
  
```  
Set-Location SQLSERVER:\SQL\localhost\DEFAULT   
```  
  
 The parenthesis characters in (local) are normally treated as commands by Windows PowerShell. You must either:  
  
-   Enclose the path string in quotes:  
  
    ```  
    Set-Location "SQLSERVER:\SQL\(local)\DEFAULT"  
    ```  
  
-   Escape the parenthesis using the back tick character (`):  
  
    ```  
    Set-Location SQLSERVER:\SQL\`(local`)\DEFAULT  
    ```  
  
-   Encode the parenthesis using their hexadecimal representation:  
  
    ```  
    Set-Location SQLSERVER:\SQL\%28local%29\DEFAULT  
    ```  
  
## See Also  
 [SQL Server Identifiers in PowerShell](sql-server-identifiers-in-powershell.md)   
 [SQL Server PowerShell Provider](sql-server-powershell-provider.md)   
 [SQL Server PowerShell](sql-server-powershell.md)  
  
  
