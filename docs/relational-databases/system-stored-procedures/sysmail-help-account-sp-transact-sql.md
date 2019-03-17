---
title: "sysmail_help_account_sp (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sysmail_help_account_sp_TSQL"
  - "sysmail_help_account_sp"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmail_help_account_sp"
ms.assetid: 87c7c39c-8e05-4e68-9272-45f908809c3b
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sysmail_help_account_sp (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Lists information (except passwords) about Database Mail accounts.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sysmail_help_account_sp [ [ @account_id = ] account_id | [ @account_name = ] 'account_name' ]  
```  
  
## Arguments  
 [ **@account_id** = ] *account_id*  
 The account ID of the account to list information for. *account_id* is **int**, with a default of NULL.  
  
 [ **@account_name** = ] **'***account_name***'**  
 The name of the account to list information for. *account_name* is **sysname**, with a default of NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 Returns a result set containing the columns listed below.  
  
||||  
|-|-|-|  
|Column name|Data type|Description|  
|**account_id**|**int**|The ID of the account.|  
|**name**|**sysname**|The name of the account.|  
|**description**|**nvarchar(256)**|The description for the account.|  
|**email_address**|**nvarchar(128)**|The e-mail address to send messages from.|  
|**display_name**|**nvarchar(128)**|The display name for the account.|  
|**replyto_address**|**nvarchar(128)**|The address where replies to messages from this account are sent.|  
|**servertype**|**sysname**|The type of e-mail server for the account.|  
|**servername**|**sysname**|The name of the e-mail server for the account.|  
|**port**|**int**|The port number of the e-mail server uses.|  
|**username**|**nvarchar(128)**|The user name to use to sign in to the e-mail server, if the e-mail server uses authentication. When **username** is NULL, Database Mail does not use authentication for this account.|  
|**use_default_credentials**|**bit**|Specifies whether to send the mail to the SMTP server using the credentials of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. **use_default_credentials** is bit, with no default. When this parameter is 1, Database Mail uses the credentials of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] service. When this parameter is 0, Database Mail uses the **@username** and **@password** for authentication on the SMTP server. If **@username** and **@password** are NULL, then Database Mail uses anonymous authentication. Consult you SMTP administrator before specifying this parameter.|  
|**enable_ssl**|**bit**|Specifies whether Database Mail encrypts communication using Secure Sockets Layer (SSL). Use this option if SSL is required on your SMTP server. **enable_ssl** is bit, with no default. 1 indicates Database Mail encrypts communication using SSL. 0 indicates Database Mail sends the mail without SSL encryption.|  
  
## Remarks  
 When no *account_id* or *account_name* is provided, **sysmail_help_account** lists information on all Database Mail accounts in the Microsoft SQL Server instance.  
  
 The stored procedure **sysmail_help_account_sp** is in the **msdb** database and is owned by the **dbo** schema. The procedure must be executed with a three-part name if the current database is not **msdb**.  
  
## Permissions  
 Execute permissions for this procedure default to members of the **sysadmin** fixed server role.  
  
## Examples  
 **A. Listing the information for all accounts**  
  
 The following example shows listing the account information for all accounts in the instance.  
  
```  
EXECUTE msdb.dbo.sysmail_help_account_sp ;  
```  
  
 Here is a sample result set, edited for line length:  
  
```  
account_id  name                         description                             email_address             display_name                     replyto_address servertype servername                port        username use_default_credentials enable_ssl  
----------- ---------------------------- --------------------------------------- ------------------------- -------------------------------- --------------- ---------- ------------------------- ----------- -------- ----------------------- ----------  
148         AdventureWorks Administrator Mail account for administrative e-mail. dba@Adventure-Works.com   AdventureWorks Automated Mailer  NULL            SMTP       smtp.Adventure-Works.com  25          NULL 0                          0        
149         Audit Account                Account for audit e-mail.               audit@Adventure-Works.com Automated Mailer (Audit)         NULL            SMTP       smtp.Adventure-Works.com  25          NULL 0                          0        
```  
  
 **B. Listing the information for a specific account**  
  
 The following example shows listing the account information for the account named `AdventureWorks Administrator`.  
  
```  
EXECUTE msdb.dbo.sysmail_help_account_sp  
    @account_name = 'AdventureWorks Administrator' ;  
```  
  
 Here is a sample result set, edited for line length:  
  
```  
account_id  name                         description                             email_address             display_name                     replyto_address servertype servername                port        username use_default_credentials enable_ssl  
----------- ---------------------------- ------------------------------------------------------ ------------------------- ---------------- ---------- ------------------------- ----------- -------- ----------------------- ----------  
148         AdventureWorks Administrator Mail account for administrative e-mail. dba@Adventure-Works.com   AdventureWorks Automated Mailer  NULL            SMTP       smtp.Adventure-Works.com  25          NULL     0                       0       
```  
  
## See Also  
 [Database Mail](../../relational-databases/database-mail/database-mail.md)   
 [Create a Database Mail Account](../../relational-databases/database-mail/create-a-database-mail-account.md)   
 [Database Mail Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-mail-stored-procedures-transact-sql.md)  
  
  
