---
title: "sys.messages (Transact-SQL)"
description: Messages (for errors) Catalog Views - sys.messages
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "messages_TSQL"
  - "sys.messages_TSQL"
  - "sys.messages"
  - "messages"
helpviewer_keywords:
  - "error messages [SQL Server]"
  - "sys.messages catalog view"
  - "error numbers [SQL Server]"
dev_langs:
  - "TSQL"
ms.assetid: 8c16ecdf-68f4-4a2a-b594-086e3344e58a
---
# Messages (for errors) Catalog Views - sys.messages
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

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
 [Messages &#40;for Errors&#41; Catalog Views &#40;Transact-SQL&#41;]()   
 [Exception Message Box Programming](/previous-versions/sql/sql-server-2016/ms166343(v=sql.130))   
 [Error Messages](../../relational-databases/native-client-odbc-error-messages/error-messages.md)   
 [Database Engine Events and Errors](../../relational-databases/errors-events/database-engine-events-and-errors.md)  
  
