---
title: "sys.event_notification_event_types (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sys.event_notification_event_types_TSQL"
  - "sys.event_notification_event_types"
  - "event_notification_event_types_TSQL"
  - "event_notification_event_types"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.event_notification_event_types catalog view"
ms.assetid: 73dae456-7044-4b00-b0bd-990ef810b356
caps.latest.revision: 17
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sys.event_notification_event_types (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns a row for each event or event group on which an event notification can fire.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658)), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].|  
  
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
  
  