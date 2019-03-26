---
title: "xp_cmdshell (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "xp_cmdshell"
  - "xp_cmdshell_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "xp_cmdshell"
ms.assetid: 18935cf4-b320-4954-b6c1-e007fcefe358
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# xp_cmdshell (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Spawns a Windows command shell and passes in a string for execution. Any output is returned as rows of text.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
xp_cmdshell { 'command_string' } [ , no_output ]  
```  
  
## Arguments  
 **'** *command_string* **'**  
 Is the string that contains a command to be passed to the operating system. *command_string* is **varchar(8000)** or **nvarchar(4000)**, with no default. *command_string* cannot contain more than one set of double quotation marks. A single pair of quotation marks is required if any spaces are present in the file paths or program names referenced in *command_string*. If you have trouble with embedded spaces, consider using FAT 8.3 file names as a workaround.  
  
 **no_output**  
 Is an optional parameter, specifying that no output should be returned to the client.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 Executing the following `xp_cmdshell` statement returns a directory listing of the current directory.  
  
```  
EXEC xp_cmdshell 'dir *.exe';  
GO  
```  
  
 The rows are returned in an **nvarchar(255)** column. If the **no_output** option is used, only the following will be returned:  
  
```  
The command(s) completed successfully.  
```  
  
## Remarks  
 The Windows process spawned by **xp_cmdshell** has the same security rights as the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account.  
  
 **xp_cmdshell** operates synchronously. Control is not returned to the caller until the command-shell command is completed.  
  
 **xp_cmdshell** can be enabled and disabled by using the Policy-Based Management or by executing **sp_configure**. For more information, see [Surface Area Configuration](../../relational-databases/security/surface-area-configuration.md) and [xp_cmdshell Server Configuration Option](../../database-engine/configure-windows/xp-cmdshell-server-configuration-option.md).  
  
> [!IMPORTANT]
>  If **xp_cmdshell** is executed within a batch and returns an error, the batch will fail. This is a change of behavior. In earlier versions of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] the batch would continue to execute.  
  
## xp_cmdshell Proxy Account  
 When it is called by a user that is not a member of the **sysadmin** fixed server role, **xp_cmdshell** connects to Windows by using the account name and password stored in the credential named **##xp_cmdshell_proxy_account##**. If this proxy credential does not exist, **xp_cmdshell** will fail.  
  
 The proxy account credential can be created by executing **sp_xp_cmdshell_proxy_account**. As arguments, this stored procedure takes a Windows user name and password. For example, the following command creates a proxy credential for Windows domain user `SHIPPING\KobeR` that has the Windows password `sdfh%dkc93vcMt0`.  
  
```  
EXEC sp_xp_cmdshell_proxy_account 'SHIPPING\KobeR','sdfh%dkc93vcMt0';  
```  
  
 For more information, see [sp_xp_cmdshell_proxy_account &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-xp-cmdshell-proxy-account-transact-sql.md).  
  
## Permissions  
 Because malicious users sometimes attempt to elevate their privileges by using **xp_cmdshell**, **xp_cmdshell** is disabled by default. Use **sp_configure** or **Policy Based Management** to enable it. For more information, see [xp_cmdshell Server Configuration Option](../../database-engine/configure-windows/xp-cmdshell-server-configuration-option.md).  
  
 When first enabled, **xp_cmdshell** requires CONTROL SERVER permission to execute and the Windows process created by **xp_cmdshell** has the same security context as the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account often has more permissions than are necessary for the work performed by the process created by **xp_cmdshell**. To enhance security, access to **xp_cmdshell** should be restricted to highly privileged users.  
  
 To allow non-administrators to use **xp_cmdshell**, and allow [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to create child processes with the security token of a less-privileged account, follow these steps:  
  
1.  Create and customize a Windows local user account or a domain account with the least privileges that your processes require.  
  
2.  Use the **sp_xp_cmdshell_proxy_account** system procedure to configure **xp_cmdshell** to use that least-privileged account.  
  
    > [!NOTE]  
    >  You can also configure this proxy account using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] by right-clicking **Properties** on your server name in Object Explorer, and looking on the **Security** tab for the **Server proxy account** section.  
  
3.  In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], using the master database, execute the `GRANT exec ON xp_cmdshell TO '<somelogin>'` statement to give specific non-**sysadmin** users the ability to execute **xp_cmdshell**. The specified login must be mapped to a user in the master database.  
  
 Now non-administrators can launch operating system processes with **xp_cmdshell** and those processes run with the permissions of the proxy account that you have configured. Users with CONTROL SERVER permission (members of the **sysadmin** fixed server role) will continue to receive the permissions of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account for child processes that are launched by **xp_cmdshell**.  
  
 To determine the Windows account being used by **xp_cmdshell** when launching operating system processes, execute the following statement:  
  
```  
xp_cmdshell 'whoami.exe'  
  
```  
  
 To determine the security context for another login, execute the following:  
  
```  
EXECUTE AS LOGIN = '<other_login>' ;  
GO  
xp_cmdshell 'whoami.exe' ;  
REVERT ;  
  
```  
  
## Examples  
  
### A. Returning a list of executable files  
 The following example shows the `xp_cmdshell` extended stored procedure executing a directory command.  
  
```  
EXEC master..xp_cmdshell 'dir *.exe''  
```  
  
### B. Returning no output  
 The following example uses `xp_cmdshell` to execute a command string without returning the output to the client.  
  
```  
USE master;  
  
EXEC xp_cmdshell 'copy c:\SQLbcks\AdvWorks.bck  
    \\server2\backups\SQLbcks, NO_OUTPUT';  
GO  
```  
  
### C. Using return status  
 In the following example, the `xp_cmdshell` extended stored procedure also suggests return status. The return code value is stored in the variable `@result`.  
  
```  
DECLARE @result int;  
EXEC @result = xp_cmdshell 'dir *.exe';  
IF (@result = 0)  
   PRINT 'Success'  
ELSE  
   PRINT 'Failure';  
```  
  
### D. Writing variable contents to a file  
 The following example writes the contents of the `@var` variable to a file named `var_out.txt` in the current server directory.  
  
```  
DECLARE @cmd sysname, @var sysname;  
SET @var = 'Hello world';  
SET @cmd = 'echo ' + @var + ' > var_out.txt';  
EXEC master..xp_cmdshell @cmd;  
```  
  
### E. Capturing the result of a command to a file  
 The following example writes the contents of the current directory to a file named `dir_out.txt` in the current server directory.  
  
```  
DECLARE @cmd sysname, @var sysname;  
SET @var = 'dir/p';  
SET @cmd = @var + ' > dir_out.txt';  
EXEC master..xp_cmdshell @cmd;  
```  
  
## See Also  
 [General Extended Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/general-extended-stored-procedures-transact-sql.md)   
 [xp_cmdshell Server Configuration Option](../../database-engine/configure-windows/xp-cmdshell-server-configuration-option.md)   
 [Surface Area Configuration](../../relational-databases/security/surface-area-configuration.md)   
 [sp_xp_cmdshell_proxy_account &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-xp-cmdshell-proxy-account-transact-sql.md)  
  
  
