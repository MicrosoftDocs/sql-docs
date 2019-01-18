---
title: "sysmail_configure_sp (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sysmail_configure_sp_TSQL"
  - "sysmail_configure_sp"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmail_configure_sp"
ms.assetid: 73b33c56-2bff-446a-b495-ae198ad74db1
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sysmail_configure_sp (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes configuration settings for Database Mail. The configuration settings specified with **sysmail_configure_sp** apply to the entire [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sysmail_configure_sp [ [ @parameter_name = ] 'parameter_name' ]  
    [ , [ @parameter_value = ] 'parameter_value' ]  
    [ , [ @description = ] 'description' ]  
```  
  
## Arguments  
 [**@parameter_name** = ] **'**_parameter_name_**'**  
 The name of the parameter to change.  
  
 [**@parameter_value** = ] **'**_parameter_value_**'**  
 The new value of the parameter.  
  
 [**@description** = ] **'**_description_**'**  
 A description of the parameter.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 Database Mail uses the following parameters:  
  
||||  
|-|-|-|  
|Parameter name|Description|Default Value|  
|*AccountRetryAttempts*|The number of times that the external mail process attempts to send the e-mail message using each account in the specified profile.|**1**|  
|*AccountRetryDelay*|The amount of time, in seconds, for the external mail process to wait between attempts to send a message.|**5000**|  
|*DatabaseMailExeMinimumLifeTime*|The minimum amount of time, in seconds, that the external mail process remains active. When Database Mail is sending many messages, increase this value to keep Database Mail active and avoid the overhead of frequent starts and stops.|**600**|  
|*DefaultAttachmentEncoding*|The default encoding for e-mail attachments.|MIME|  
|*MaxFileSize*|The maximum size of an attachment, in bytes.|**1000000**|  
|*ProhibitedExtensions*|A comma-separated list of extensions which cannot be sent as an attachment to an e-mail message.|**exe,dll,vbs,js**|  
|*LoggingLevel*|Specify which messages are recorded in the Database Mail log. One of the following numeric values:<br /><br /> 1 - This is normal mode. Logs only errors.<br /><br /> 2 - This is extended mode. Logs errors, warnings, and informational messages.<br /><br /> 3 - This is verbose mode. Logs errors, warnings, informational messages, success messages, and additional internal messages. Use this mode for troubleshooting.|**2**|  
  
 The stored procedure **sysmail_configure_sp** is in the **msdb** database and is owned by the **dbo** schema. The procedure must be executed with a three-part name if the current database is not **msdb**.  
  
## Permissions  
 Execute permissions for this procedure default to members of the **sysadmin** fixed server role.  
  
## Examples  
 **A. Setting Database Mail to retry each account 10 times**  
  
 The following example shows setting Database Mail to retry each account ten times before considering the account to be unreachable.  
  
```  
EXECUTE msdb.dbo.sysmail_configure_sp  
    'AccountRetryAttempts', '10' ;  
```  
  
 **B. Setting the maximum attachment size to two megabytes**  
  
 The following example shows setting the maximum attachment size to 2 megabytes.  
  
```  
EXECUTE msdb.dbo.sysmail_configure_sp  
    'MaxFileSize', '2097152' ;  
```  
  
## See Also  
 [Database Mail](../../relational-databases/database-mail/database-mail.md)   
 [sysmail_help_configure_sp &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sysmail-help-configure-sp-transact-sql.md)   
 [Database Mail Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-mail-stored-procedures-transact-sql.md)  
  
  
