---
title: "sys.events (Transact-SQL)"
description: sys.events (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.events_TSQL"
  - "sys.events"
  - "events_TSQL"
  - "events"
helpviewer_keywords:
  - "sys.events catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.events (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  Contains a row for each event for which a trigger or event notification fires. These events represent the event types that are specified when the trigger or event notification is created by using [CREATE TRIGGER](../../t-sql/statements/create-trigger-transact-sql.md) or [CREATE EVENT NOTIFICATION](../../t-sql/statements/create-event-notification-transact-sql.md).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|ID of the trigger or event notification. This value, together with **type**, uniquely identifies the row.|  
|**type**|**int**|Event that causes the trigger to fire.|  
|**type_desc**|**nvarchar(60)**|Description of the event that causes the trigger to fire.|  
|**is_trigger_event**|**bit**|1 = Trigger event.<br /><br /> 0 = Notification event.|  
|**event_group_type**|**int**|Event group on which the trigger or event notification is created, or null if not created on an event group.|  
|**event_group_type_desc**|**nvarchar(60)**|Description of the event group on which the trigger or event notification is created, or null if not created on an event group.|  
  
## Related content

- [Object catalog views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
