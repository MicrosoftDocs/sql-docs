---
title: "Manage Authentication in Database Engine PowerShell | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: scripting
ms.topic: conceptual
ms.assetid: ab9212a6-6628-4f08-a38c-d3156e05ddea
author: stevestein
ms.author: sstein
manager: craigg
---
# Manage Authentication in Database Engine PowerShell
  By default, the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] PowerShell components use Windows Authentication when connecting to an instance of the [!INCLUDE[ssDE](../includes/ssde-md.md)]. You can use SQL Server Authentication by either defining a PowerShell virtual drive, or by specifying the `-Username` and `-Password` parameters for `Invoke-Sqlcmd`.  
  
1.  **Before you begin:**  [Permissions](#Permissions)  
  
2.  **To set authentication, using:**  [A Virtual Drive](#SQLAuthVirtDrv), [Invoke-Sqlcmd](#SQLAuthInvSqlCmd)  
  
##  <a name="Permissions"></a> Permissions  
 All actions you can perform in an instance of the [!INCLUDE[ssDE](../includes/ssde-md.md)] are controlled by the permissions granted to the authentication credentials used to connect to the instance. By default, the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provider and cmdlets use the Windows account under which it is running to make a Windows Authentication connection to the [!INCLUDE[ssDE](../includes/ssde-md.md)].  
  
 To make a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication connection you must supply a SQL Server Authentication login ID and password. When using the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provider, you must associate the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] login credentials with a virtual drive, and then use the change directory command (`cd`) to connect to that drive. In Windows PowerShell, security credentials can only be associated with virtual drives.  
  
##  <a name="SQLAuthVirtDrv"></a> SQL Server Authentication Using a Virtual Drive  
 **To create a virtual drive associated with a SQL Server Authentication login**  
  
1.  Create a function that:  
  
    1.  Has parameters for the name to give the virtual drive, the login ID, and the provider path to associate with the virtual drive.  
  
    2.  Uses `read-host` to prompt the user for the password.  
  
    3.  Uses `new-object` to create a credentials object.  
  
    4.  Uses `new-psdrive` to create a virtual drive with the supplied credentials.  
  
2.  Invoke the function to create a virtual drive with the supplied credentials.  
  
### Example (Virtual Drive)  
 This example creates a function named **sqldrive** that you can use to create a virtual drive that is associated with the specified [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication login and instance.  
  
 The **sqldrive** function prompts you to enter the password for your login, masking the password as you type it in. Then, whenever you use the change directory command (`cd`) to connect to a path by using the virtual drive name, all operations are performed by using the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication login credentials that you supplied when you created the drive.  
  
```  
## Create a function that specifies the login and prompts for the password.  
  
function sqldrive  
{  
    param( [string]$name, [string]$login = "MyLogin", [string]$root = "SQLSERVER:\SQL\MyComputer\MyInstance" )  
    $pwd = read-host -AsSecureString -Prompt "Password"  
    $cred = new-object System.Management.Automation.PSCredential -argumentlist $login,$pwd  
    New-PSDrive $name -PSProvider SqlServer -Root $root -Credential $cred -Scope 1  
}  
  
## Use the sqldrive function to create a SQLAuth virtual drive.  
sqldrive SQLAuth  
  
## CD to the virtual drive, which invokes the supplied authentication credentials.  
cd SQLAuth  
```  
  
##  <a name="SQLAuthInvSqlCmd"></a> SQL Server Authentication Using Invoke-Sqlcmd  
 **To use Invoke-Sqlcmd with SQL Server Authentication**  
  
1.  Use the `-Username` parameter to specify a login ID, and the `-Password` parameter to specify the associated password.  
  
### Example (Invoke-Sqlcmd)  
 This example uses the read-host cmdlet to prompt the user for a password, and then connects using SQL Server Authentication.  
  
```  
## Prompt the user for their password.  
$pwd = read-host -AsSecureString -Prompt "Password"  
  
Invoke-Sqlcmd -Query "SELECT GETDATE() AS TimeOfQuery;" -ServerInstance "MyComputer\MyInstance" -Username "MyLogin" -Password $pwd  
```  
  
## See Also  
 [SQL Server PowerShell](sql-server-powershell.md)   
 [SQL Server PowerShell Provider](sql-server-powershell-provider.md)   
 [Invoke-Sqlcmd cmdlet](../database-engine/invoke-sqlcmd-cmdlet.md)  
  
  
