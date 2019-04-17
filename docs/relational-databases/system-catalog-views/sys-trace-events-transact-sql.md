---
title: "sys.trace_events (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "trace_events_TSQL"
  - "trace_events"
  - "sys.trace_events"
  - "sys.trace_events_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.trace_events catalog view"
ms.assetid: e7d2c5df-0e17-4e94-9d41-d36c7ee60662
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# sys.trace_events (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **sys.trace_events** catalog view contains a list of all SQL trace events. These trace events do not change for a given version of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
> **IMPORTANT!** [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use Extended Event catalog views instead.  
  
 For more information about these trace events, see [SQL Server Event Class Reference](../../relational-databases/event-classes/sql-server-event-class-reference.md).  
  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**trace_event_id**|**smallint**|Unique ID of the event. This column is also in the **sys.trace_event_bindings** and **sys.trace_subclass_values** catalog views.|  
|**category_id**|**smallint**|Category ID of the event. This column is also in the **sys.trace_categories** catalog view.|  
|**name**|**nvarchar(128)**|Unique name of this event. This parameter is not localized.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [sys.traces &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-traces-transact-sql.md)   
 [sys.trace_categories &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-trace-categories-transact-sql.md)   
 [sys.trace_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-trace-columns-transact-sql.md)   
 [sys.trace_event_bindings &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-trace-event-bindings-transact-sql.md)   
 [sys.trace_subclass_values &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-trace-subclass-values-transact-sql.md)  
  
  
