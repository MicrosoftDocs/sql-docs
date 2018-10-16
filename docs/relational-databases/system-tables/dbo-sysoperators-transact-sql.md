---
title: "dbo.sysoperators (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sysoperators"
  - "dbo.sysoperators"
  - "dbo.sysoperators_TSQL"
  - "sysoperators_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysoperators system table"
ms.assetid: c2afa20c-b15f-46ca-ae74-2eb65909409e
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# dbo.sysoperators (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains one row for each [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent operator.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|ID of the operator.|  
|**name**|**sysname**|Name of the operator.|  
|**enabled**|**tinyint**|Status of alert notifications (Boolean). If **1**, this operator can receive notifications when an alert occurs.|  
|**email_address**|**nvarchar(100)**|E-mail address for this operator.|  
|**last_email_date**|**int**|Date this operator last received an e-mail alert notification.|  
|**last_email_time**|**int**|Time of day this operator last received an e-mail alert notification.|  
|**pager_address**|**nvarchar(100)**|Pager address for this operator.|  
|**last_pager_date**|**int**|Date this operator last received a pager alert notification.|  
|**last_pager_time**|**int**|Time of day this operator last received a pager alert notification.|  
|**weekday_pager_start_time**|**int**|Time of day on a weekday (Monday through Friday) after which this operator is available to receive a pager alert notification.|  
|**weekday_pager_end_time**|**int**|Time of day on a weekday (Monday through Friday) after which this operator is not available to receive a pager alert notification.|  
|**saturday_pager_start_time**|**int**|Time of day on Saturday after which this operator is available to receive a pager alert notification.|  
|**saturday_pager_end_time**|**int**|Time of day on Saturday after which this operator is not available to receive a pager alert notification.|  
|**sunday_pager_start_time**|**int**|Time of day on Sunday after which this operator is available to receive a pager alert notification.|  
|**sunday_pager_end_time**|**int**|Time of day on Sunday after which this operator is not available to receive a pager alert notification.|  
|**pager_days**|**tinyint**|Bitmask representing the days of the week during which this operator is available to receive a pager alert notification.|  
|**netsend_address**|**nvarchar(100)**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**last_netsend_date**|**int**|Date that the most recent network message was last sent to the specified operator ID.|  
|**last_netsend_time**|**int**|Time that the most recent network message was last sent to the specified operator ID.|  
|**category_id**|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
  
## See Also  
 [SQL Server Agent Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/sql-server-agent-tables-transact-sql.md)  
  
  
