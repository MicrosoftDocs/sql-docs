---
title: "sys.event_notification_event_types (Transact-SQL)"
description: sys.event_notification_event_types (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.event_notification_event_types_TSQL"
  - "sys.event_notification_event_types"
  - "event_notification_event_types_TSQL"
  - "event_notification_event_types"
helpviewer_keywords:
  - "sys.event_notification_event_types catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 73dae456-7044-4b00-b0bd-990ef810b356
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.event_notification_event_types (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns a row for each event or event group on which an event notification can fire.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**type**|**int**|Type of event or event group that causes an event notification to fire.|  
|**type_name**|**nvarchar(128)**|Name of an event or event group. This can be specified in the FOR clause of a [CREATE EVENT NOTIFICATION](../../t-sql/statements/create-event-notification-transact-sql.md) statement.|  
|**parent_type**|**int**|Type of event group that is the parent of the event or event group.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
