---
description: "sp_altermessage (Transact-SQL)"
title: "sp_altermessage (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_altermessage_TSQL"
  - "sp_altermessage"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_altermessage"
ms.assetid: 1b28f280-8ef9-48e9-bd99-ec14d79abaca
author: markingmyname
ms.author: maghan
---
# sp_altermessage (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Alters the state of user-defined or system messages in an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. User-defined messages can be viewed using the **sys.messages** catalog view.  

  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_altermessage [ @message_id = ] message_number   ,[ @parameter = ]'write_to_log'  
   ,[ @parameter_value = ]'value'   
```  
  
## Arguments  
 [**@message_id =** ] *message_number*  
 Is the error number of the message to alter from **sys.messages**. *message_number* is **int** with no default value.  
  
`[ @parameter = ] 'write\_to\_log_'`
 Is used with **\@parameter_value** to indicate that the message is to be written to the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows application log. *write_to_log* is **sysname** with no default value. *write_to_log* must be set to WITH_LOG or NULL. If *write_to_log* is set to WITH_LOG or NULL, and the value for **\@parameter_value** is **true**, the message is written to the Windows application log. If *write_to_log* is set to WITH_LOG or NULL and the value for **\@parameter_value** is **false**, the message is not always written to the Windows application log, but may be written depending upon how the error was raised. If *write_to_log* is specified, the value for **\@parameter_value** must also be specified.  
  
> [!NOTE]  
>  If a message is written to the Windows application log, it is also written to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] error log file.  
  
`[ @parameter_value = ]'value_'`
 Is used with **\@parameter** to indicate that the error is to be written to the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows application log. *value* is **varchar(5)**, with no default value. If **true**, the error is always written to the Windows application log. If **false**, the error is not always written to the Windows application log, but may be written depending upon how the error was raised. If *value* is specified, *write_to_log* for **\@parameter** must also be specified.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 The effect of **sp_altermessage** with the WITH_LOG option is similar to that of the RAISERROR WITH LOG parameter, except that **sp_altermessage** changes the logging behavior of an existing message. If a message has been altered to be WITH_LOG, it is always written to the Windows application log, regardless of how a user invokes the error. Even if RAISERROR is executed without the WITH_LOG option, the error is written to the Windows application log.  
  
 System messages can be modified by using **sp_altermessage**.  
  
## Permissions  
 Requires membership in the **serveradmin** fixed server role.  
  
## Examples  
 The following example causes existing message `55001` to be logged to the Windows application log.  
  
```  
EXECUTE sp_altermessage 55001, 'WITH_LOG', 'true';  
GO  
```  
  
## See Also  
 [RAISERROR &#40;Transact-SQL&#41;](../../t-sql/language-elements/raiserror-transact-sql.md)   
 [sp_addmessage &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmessage-transact-sql.md)   
 [sp_dropmessage &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropmessage-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
