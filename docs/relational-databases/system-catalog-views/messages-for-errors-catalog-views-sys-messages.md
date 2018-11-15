---
title: "sys.messages (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "messages_TSQL"
  - "sys.messages_TSQL"
  - "sys.messages"
  - "messages"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "error messages [SQL Server]"
  - "sys.messages catalog view"
  - "error numbers [SQL Server]"
ms.assetid: 8c16ecdf-68f4-4a2a-b594-086e3344e58a
author: stevestein
ms.author: sstein
manager: craigg
---
# Messages (for errors) Catalog Views - sys.messages
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains a row for each **message_id** or **language_id** of the error messages in the system, for both system-defined and user-defined messages. For more information, see [sp_addmessage &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmessage-transact-sql.md).  
   
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**message_id**|**int**|ID of the message. Is unique across server. Message IDs less than 50000 are system messages.|  
|**language_id**|**smallint**|Language ID for which the text in **text** is used, as defined in **syslanguages**. This is unique for a specified **message_id**.|  
|**severity**|**tinyint**|Severity level of the message, between 1 and 25. This is the same for all message languages within a **message_id**.|  
|**is_event_logged**|**bit**|1 = Message is event-logged when an error is raised. This is the same for all message languages within a **message_id**.|  
|**text**|**nvarchar(2048)**|Text of the message used when the corresponding **language_id** is active.|  
  
## Permissions  
 Requires membership in the **public** role. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [THROW &#40;Transact-SQL&#41;](../../t-sql/language-elements/throw-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Messages &#40;for Errors&#41; Catalog Views &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/8ac78c53-7b97-41b3-9cbd-5f97c179f1f2)   
 [Exception Message Box Programming](https://msdn.microsoft.com/library/0b1ba514-6959-4e69-bfd2-3cf3c1ac4b9c)   
 [Error Messages](../../relational-databases/native-client-odbc-error-messages/error-messages.md)   
 [Database Engine Events and Errors](../../relational-databases/errors-events/database-engine-events-and-errors.md)  
  
  
