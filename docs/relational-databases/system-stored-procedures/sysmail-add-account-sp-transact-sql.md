---
title: "sysmail_add_account_sp (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sysmail_add_account_sp"
  - "sysmail_add_account_sp_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmail_add_account_sp"
ms.assetid: 65e15e2e-107c-49c3-b12c-f4edf0eb1617
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sysmail_add_account_sp (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates a new Database Mail account holding information about an SMTP account.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sysmail_add_account_sp  [ @account_name = ] 'account_name',  
    [ @email_address = ] 'email_address' ,  
    [ [ @display_name = ] 'display_name' , ]  
    [ [ @replyto_address = ] 'replyto_address' , ]  
    [ [ @description = ] 'description' , ]  
    [ @mailserver_name = ] 'server_name'   
    [ , [ @mailserver_type = ] 'server_type' ]  
    [ , [ @port = ] port_number ]  
    [ , [ @username = ] 'username' ]  
    [ , [ @password = ] 'password' ]  
    [ , [ @use_default_credentials = ] use_default_credentials ]  
    [ , [ @enable_ssl = ] enable_ssl ]  
    [ , [ @account_id = ] account_id OUTPUT ]  
```  
  
## Arguments  
`[ @account_name = ] 'account_name'`
 The name of the account to add. *account_name* is **sysname**, with no default.  
  
`[ @email_address = ] 'email_address'`
 The e-mail address to send the message from. This address must be an internet e-mail address. *email_address* is **nvarchar(128)**, with no default. For example, an account for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent may send e-mail from the address **SqlAgent@Adventure-Works.com**.  
  
`[ @display_name = ] 'display_name'`
 The display name to use on e-mail messages from this account. *display_name* is **nvarchar(128)**, with a default of NULL. For example, an account for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent may display the name **SQL Server Agent Automated Mailer** on e-mail messages.  
  
`[ @replyto_address = ] 'replyto_address'`
 The address that responses to messages from this account are sent to. *replyto_address* is **nvarchar(128)**, with a default of NULL. For example, replies to an account for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent may go to the database administrator, **danw@Adventure-Works.com**.  
  
`[ @description = ] 'description'`
 Is a description for the account. *description* is **nvarchar(256)**, with a default of NULL.  
  
`[ @mailserver_name = ] 'server_name'`
 The name or IP address of the SMTP mail server to use for this account. The computer that runs [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be able to resolve the *server_name* to an IP address. *server_name* is **sysname**, with no default.  
  
`[ @mailserver_type = ] 'server_type'`
 The type of e-mail server. *server_type* is **sysname**, with a default of **'SMTP'**..  
  
`[ @port = ] port_number`
 The port number for the e-mail server. *port_number* is **int**, with a default of 25.  
  
`[ @username = ] 'username'`
 The user name to use to log on to the e-mail server. *username* is **nvarchar(128)**, with a default of NULL. When this parameter is NULL, Database Mail does not use authentication for this account. If the mail server does not require authentication, use NULL for the username.  
  
`[ @password = ] 'password'`
 The password to use to log on to the e-mail server. *password* is **nvarchar(128)**, with a default of NULL. There is no need to provide a password unless a username is specified.  
  
`[ @use_default_credentials = ] use_default_credentials`
 Specifies whether to send the mail to the SMTP server using the credentials of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. **use_default_credentials** is bit, with a default of 0. When this parameter is 1, Database Mail uses the credentials of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. When this parameter is 0, Database Mail sends the **@username** and **@password** parameters if present, otherwise sends mail without **@username** and **@password** parameters.  
  
`[ @enable_ssl = ] enable_ssl`
 Specifies whether Database Mail encrypts communication using Secure Sockets Layer. **Enable_ssl** is bit, with a default of 0.  
  
`[ @account_id = ] account_id OUTPUT`
 Returns the account id for the new account. *account_id* is **int**, with a default of NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 Database Mail provides separate parameters for **@email_address**, **@display_name**, and **@replyto_address**. The **@email_address** parameter is the address from which the message is sent. The **@display_name** parameter is the name shown in the **From:** field of the e-mail message. The **@replyto_address** parameter is the address where replies to the e-mail message will be sent. For example, an account used for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent may send e-mail messages from an e-mail address that is only used for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. Messages from that address should display a friendly name, so recipients can easily determine that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent sent the message. If a recipient replies to the message, the reply should go to the database administrator rather than the address used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. For this scenario, the account uses **SqlAgent@Adventure-Works.com** as the e-mail address. The display name is set to **SQL Server Agent Automated Mailer**. The account uses **danw@Adventure-Works.com** as the reply to address, so replies to messages sent from this account go to the database administrator rather than the e-mail address for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. By providing independent settings for these three parameters, Database Mail allows you to configure messages to suit your needs.  
  
 The **@mailserver_type** parameter supports the value **'SMTP'**.  
  
 When **@use_default_credentials** is 1 mail is sent to the SMTP server using the credentials of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. When **@use_default_credentials** is 0 and a **@username** and **@password** are specified for an account, the account uses SMTP authentication. The **@username** and **@password** are the credentials the account uses for the SMTP server, not credentials for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or the network that the computer is on.  
  
 The stored procedure **sysmail_add_account_sp** is in the **msdb** database and is owned by the **dbo** schema. The procedure must be executed with a three-part name if the current database is not **msdb**.  
  
## Permissions  
 Execute permissions for this procedure default to members of the **sysadmin** fixed server role.  
  
## Examples  
 The following example creates an account named `AdventureWorks Administrator`. The account uses the e-mail address `dba@Adventure-Works.com` and sends mail to the SMTP mail server `smtp.Adventure-Works.com`. E-mail messages sent from this account show `AdventureWorks Automated Mailer` on the **From:** line of the message. Replies to the messages are directed to `danw@Adventure-Works.com`.  
  
```  
EXECUTE msdb.dbo.sysmail_add_account_sp  
    @account_name = 'AdventureWorks Administrator',  
    @description = 'Mail account for administrative e-mail.',  
    @email_address = 'dba@Adventure-Works.com',  
    @display_name = 'AdventureWorks Automated Mailer',  
    @mailserver_name = 'smtp.Adventure-Works.com' ;  
```  
  
## See Also  
 [Database Mail](../../relational-databases/database-mail/database-mail.md)   
 [Create a Database Mail Account](../../relational-databases/database-mail/create-a-database-mail-account.md)   
 [Database Mail Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-mail-stored-procedures-transact-sql.md)  
  
  
