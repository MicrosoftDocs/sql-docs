---
description: "sysmail_update_account_sp (Transact-SQL)"
title: "sysmail_update_account_sp (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/17/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sysmail_update_account_sp"
  - "sysmail_update_account_sp_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmail_update_account_sp"
ms.assetid: ba2fdccc-5ed4-40ef-a479-79497b4d61aa
author: VanMSFT
ms.author: vanto
---
# sysmail_update_account_sp (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Changes the information in an existing Database Mail account.  
 
 
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sysmail_update_account_sp [ [ @account_id = ] account_id ] [ , ] [ [ @account_name = ] 'account_name' ] ,  
    [ @email_address = ] 'email_address' ,   
    [ @display_name = ] 'display_name' ,   
    [ @replyto_address = ] 'replyto_address' ,  
    [ @description = ] 'description' ,   
    [ @mailserver_name = ] 'server_name' ,   
    [ @mailserver_type = ] 'server_type' ,   
    [ @port = ] port_number ,   
    [ @timeout = ] 'timeout' ,  
    [ @username = ] 'username' ,  
    [ @password = ] 'password' ,  
    [ @use_default_credentials = ] use_default_credentials ,  
    [ @enable_ssl = ] enable_ssl   
```  
  
## Arguments  
`[ @account_id = ] account_id`
 The account ID to update. *account_id* is **int**, with a default of NULL. At least one of *account_id* or *account_name* must be specified. If both are specified, the procedure changes the name of the account.  
  
`[ @account_name = ] 'account_name'`
 The name of the account to update. *account_name* is **sysname**, with a default of NULL. At least one of *account_id* or *account_name* must be specified. If both are specified, the procedure changes the name of the account.  
  
`[ @email_address = ] 'email_address'`
 The new e-mail address to send the message from. This address must be an internet e-mail address. The server name in the address is the server that Database Mail uses to send mail from this account. *email_address* is **nvarchar(128)**, with a default of NULL.  
  
`[ @display_name = ] 'display_name'`
 The new display name to use on e-mail messages from this account. *display_name* is **nvarchar(128)**, with no default.  
  
`[ @replyto_address = ] 'replyto_address'`
 The new address to use in the Reply-To header of e-mail messages from this account. *replyto_address* is **nvarchar(128)**, with no default.  
  
`[ @description = ] 'description'`
 The new description for the account. *description* is **nvarchar(256)**, with a default of NULL.  
  
`[ @mailserver_name = ] 'server_name'`
 The new name of the SMTP mail server to use for this account. The computer that runs [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be able to resolve the *server_name* to an IP address. *server_name* is **sysname**, with no default.  
  
`[ @mailserver_type = ] 'server_type'`
 The new type of the mail server. *server_type* is **sysname**, with no default. Only a value of **'SMTP'** is supported.  
  
`[ @port = ] port_number`
 The new port number of the mail server. *port_number* is **int**, with no default.  
  
`[ @timeout = ] 'timeout'`
 Timeout parameter for SmtpClient.Send of a single email message. *Timeout* is **int** in seconds, with no default.  
  
`[ @username = ] 'username'`
 The new user name to use to log on to the mail server. *User name* is **sysname**, with no default.  
  
`[ @password = ] 'password'`
 The new password to use to log on to the mail server. *password* is **sysname**, with no default.  
  
`[ @use_default_credentials = ] use_default_credentials`
 Specifies whether to send the mail to the SMTP server using the credentials of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] service. **use_default_credentials** is bit, with no default. When this parameter is 1, Database Mail uses the credentials of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. When this parameter is 0, Database Mail uses the **\@username** and **\@password** for authentication on the SMTP server. If **\@username** and **\@password** are NULL then it will use anonymous authentication. Consult with your SMTP administrator before specifying this parameter  
  
`[ @enable_ssl = ] enable_ssl`
 Specifies whether Database Mail encrypts communication using Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL). Use this option if TLS is required on your SMTP server. **enable_ssl** is bit, with no default.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 When both the account name and the account id are specified, the stored procedure changes the account name in addition to updating the information for the account. Changing the account name may be useful to correct errors in the account name.  
  
 The stored procedure **sysmail_update_account_sp** is in the **msdb** database and is owned by the **dbo** schema. The procedure must be executed with a three-part name if the current database is not **msdb**.  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role.  
  
## Examples  
  
### A. Changing the information for an account  
 The following example updates the account `AdventureWorks Administrator` In the **msdb** database. The information for the account is set to the values provided.  
  
```  
EXECUTE msdb.dbo.sysmail_update_account_sp  
     @account_name = 'AdventureWorks Administrator'  
    ,@description = 'Mail account for administrative e-mail.'  
    ,@email_address = 'dba@Adventure-Works.com'  
    ,@display_name = 'AdventureWorks Automated Mailer'  
    ,@replyto_address = NULL  
    ,@mailserver_name = 'smtp.Adventure-Works.com'  
    ,@mailserver_type = 'SMTP'  
    ,@port = 25  
    ,@timeout = 60  
    ,@username = NULL  
    ,@password = NULL  
    ,@use_default_credentials = 0  
    ,@enable_ssl = 0;  
```  
  
### B. Changing the name of an account and the information for an account  
 The following example changes the name and updates the account information for the with account id `125`. The new name of the account is `Backup Mail Server`.  
  
```  
EXECUTE msdb.dbo.sysmail_update_account_sp  
     @account_id = 125  
    ,@account_name = 'Backup Mail Server'  
    ,@description = 'Mail account for administrative e-mail.'  
    ,@email_address = 'dba@Adventure-Works.com'  
    ,@display_name = 'AdventureWorks Automated Mailer'  
    ,@replyto_address = NULL  
    ,@mailserver_name = 'smtp-backup.Adventure-Works.com'  
    ,@mailserver_type = 'SMTP'  
    ,@port = 25  
    ,@timeout = 60  
    ,@username = NULL  
    ,@password = NULL  
    ,@use_default_credentials = 0  
    ,@enable_ssl = 0;  
```  
  
## See Also  
 [Database Mail](../../relational-databases/database-mail/database-mail.md)   
 [Create a Database Mail Account](../../relational-databases/database-mail/create-a-database-mail-account.md)   
 [Database Mail Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-mail-stored-procedures-transact-sql.md)  
  
  
