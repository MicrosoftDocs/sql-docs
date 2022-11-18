---
description: "sp_addmessage (Transact-SQL)"
title: "sp_addmessage (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_addmessage"
  - "sp_addmessage_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_addmessage"
ms.assetid: 54746d30-f944-40e5-a707-f2d9be0fb9eb
author: markingmyname
ms.author: maghan
---
# sp_addmessage (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Stores a new user-defined error message in an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. Messages stored by using **sp_addmessage** can be viewed by using the **sys.messages** catalog view.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addmessage [ @msgnum= ] msg_id , [ @severity= ] severity , [ @msgtext= ] 'msg'   
     [ , [ @lang= ] 'language' ]   
     [ , [ @with_log= ] { 'TRUE' | 'FALSE' } ]   
     [ , [ @replace= ] 'replace' ]   
```  
  
## Arguments  
`[ @msgnum = ] msg_id`
 Is the ID of the message. *msg_id* is **int** with a default of NULL. *msg_id* for user-defined error messages can be an integer between 50,001 and 2,147,483,647. The combination of *msg_id* and *language* must be unique; an error is returned if the ID already exists for the specified language.  
  
`[ @severity = ]severity`
 Is the severity level of the error. *severity* is **smallint** with a default of NULL. Valid levels are from 1 through 25. For more information about severities, see [Database Engine Error Severities](../../relational-databases/errors-events/database-engine-error-severities.md).  
  
`[ @msgtext = ] 'msg'`
 Is the text of the error message. *msg* is **nvarchar(255)** with a default of NULL.  
  
`[ @lang = ] 'language'`
 Is the language for this message. *language* is **sysname** with a default of NULL. Because multiple languages can be installed on the same server, *language* specifies the language in which each message is written. When *language* is omitted, the language is the default language for the session.  
  
`[ @with_log = ] { 'TRUE' | 'FALSE' }`
 Is whether the message is to be written to the Windows application log when it occurs. **\@with_log** is **varchar(5)** with a default of FALSE. If TRUE, the error is always written to the Windows application log. If FALSE, the error is not always written to the Windows application log but can be written, depending on how the error was raised. Only members of the **sysadmin** server role can use this option.  
  
> [!NOTE]  
>  If a message is written to the Windows application log, it is also written to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] error log file.  
  
`[ @replace = ] 'replace'`
 If specified as the string *replace*, an existing error message is overwritten with new message text and severity level. *replace* is **varchar(7)** with a default of NULL. This option must be specified if *msg_id* already exists. If you replace a U.S. English message, the severity level is replaced for all messages in all other languages that have the same *msg_id*.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 For non-English versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the U.S. English version of a message must already exist before the message can be added using another language. The severity of the two versions of the message must match.  
  
 When localizing messages that contain parameters, use parameter numbers that correspond to the parameters in the original message. Insert an exclamation point (!) after each parameter number.  
  
|Original message|Localized message|  
|----------------------|-----------------------|  
|'Original message param 1: %s,<br /><br /> param 2: %d'|'Localized message param 1: %1!,<br /><br /> param 2: %2!'|  
  
 Because of language syntax differences, the parameter numbers in the localized message may not occur in the same sequence as in the original message.  
  
## Permissions  
Requires membership in the **sysadmin** or **serveradmin** fixed server roles.  
  
## Examples  
  
### A. Defining a custom message  
 The following example adds a custom message to **sys.messages**.  
  
```  
USE master;  
GO  
EXEC sp_addmessage 50001, 16,   
   N'Percentage expects a value between 20 and 100.   
   Please reexecute with a more appropriate value.';  
GO  
```  
  
### B. Adding a message in two languages  
 The following example first adds a message in U.S. English and then adds the same message in French`.`  
  
```  
USE master;  
GO  
EXEC sp_addmessage @msgnum = 60000, @severity = 16,   
   @msgtext = N'The item named %s already exists in %s.',   
   @lang = 'us_english';  
  
EXEC sp_addmessage @msgnum = 60000, @severity = 16,   
   @msgtext = N'L''élément nommé %1! existe déjà dans %2!',   
   @lang = 'French';  
GO  
```  
  
### C. Changing the order of parameters  
 The following example first adds a message in U.S. English, and then adds a localized message in which the parameter order is changed.  
  
```  
USE master;  
GO  
  
EXEC sp_addmessage   
    @msgnum = 60000,   
    @severity = 16,  
    @msgtext =   
        N'This is a test message with one numeric  
        parameter (%d), one string parameter (%s),   
        and another string parameter (%s).',  
    @lang = 'us_english';  
  
EXEC sp_addmessage   
    @msgnum = 60000,   
    @severity = 16,  
    @msgtext =   
        -- In the localized version of the message,  
        -- the parameter order has changed. The   
        -- string parameters are first and second  
        -- place in the message, and the numeric   
        -- parameter is third place.  
        N'Dies ist eine Testmeldung mit einem   
        Zeichenfolgenparameter (%3!),  
        einem weiteren Zeichenfolgenparameter (%2!),   
        und einem numerischen Parameter (%1!).',  
    @lang = 'German';  
GO    
  
-- Changing the session language to use the U.S. English  
-- version of the error message.  
SET LANGUAGE us_english;  
GO  
  
RAISERROR(60000,1,1,15,'param1','param2') -- error, severity, state,  
GO                                       -- parameters.  
  
-- Changing the session language to use the German  
-- version of the error message.  
SET LANGUAGE German;  
GO  
  
RAISERROR(60000,1,1,15,'param1','param2'); -- error, severity, state,   
GO                                       -- parameters.  
```  
  
## See Also  
 [RAISERROR &#40;Transact-SQL&#41;](../../t-sql/language-elements/raiserror-transact-sql.md)   
 [sp_altermessage &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-altermessage-transact-sql.md)   
 [sp_dropmessage &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropmessage-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
