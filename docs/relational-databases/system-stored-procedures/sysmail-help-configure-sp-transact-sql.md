---
title: "sysmail_help_configure_sp (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sysmail_help_configure_sp"
  - "sysmail_help_configure_sp_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmail_help_configure_sp"
ms.assetid: e598d4c8-3041-4965-b046-dce3a8e3d3e0
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sysmail_help_configure_sp (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Displays configuration settings for Database Mail.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sysmail_help_configure_sp  [ [ @parameter_name = ] 'parameter_name' ]  
```  
  
## Arguments  
 [**@parameter_name** = ] **'***parameter_name***'**  
 The name of the configuration setting to retrieve. When specified, the value of the configuration setting is returned in the **@parameter_value** OUTPUT parameter. When no **@parameter_name** is specified, this stored procedure returns a result set containing all of the Database Mail configuration settings in the instance.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 When no **@parameter_name** is specified, returns a result set with the following columns.  
  
||||  
|-|-|-|  
|Column name|Data type|Description|  
|**paramname**|**nvarchar(256)**|The name of the configuration parameter.|  
|**paramvalue**|**nvarchar(256)**|The value of the configuration parameter.|  
|**description**|**nvarchar(256)**|A description of the configuration parameter.|  
  
## Remarks  
 The stored procedure **sysmail_help_configure_sp** lists the current Database Mail configuration settings for the instance.  
  
 When a **@parameter_name** is specified, but no output parameter is provided for **@parameter_value**, this stored procedure produces no output.  
  
 The stored procedure **sysmail_help_configure_sp** is in the **msdb** database and is owned by the **dbo** schema. The procedure must be invoked with a three-part name if the current database is not **msdb**.  
  
## Permissions  
 Execute permissions for this procedure default to members of the **sysadmin** fixed server role.  
  
## Examples  
 The following example shows listing the Database Mail configuration settings for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
```  
EXECUTE msdb.dbo.sysmail_help_configure_sp ;  
```  
  
 Here is a sample result set, edited for line length:  
  
```  
paramname                       paramvalue      description  
------------------------------- --------------- -----------------------------------------------------------------------------  
AccountRetryAttempts            1               Number of retry attempts for a mail server  
AccountRetryDelay               5000            Delay between each retry attempt to mail server  
DatabaseMailExeMinimumLifeTime  600             Minimum process lifetime in seconds  
DefaultAttachmentEncoding       MIME            Default attachment encoding  
LoggingLevel                    2               Database Mail logging level: normal - 1, extended - 2 (default), verbose - 3  
MaxFileSize                     1000000         Default maximum file size  
ProhibitedExtensions            exe,dll,vbs,js  Extensions not allowed in outgoing mails  
  
```  
  
## See Also  
 [Database Mail](../../relational-databases/database-mail/database-mail.md)   
 [Database Mail Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-mail-stored-procedures-transact-sql.md)  
  
  
