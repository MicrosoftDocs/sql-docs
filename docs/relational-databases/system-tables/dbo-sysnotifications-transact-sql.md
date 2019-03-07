---
title: "dbo.sysnotifications (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dbo.sysnotifications_TSQL"
  - "sysnotifications"
  - "sysnotifications_TSQL"
  - "dbo.sysnotifications"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysnotifications system table"
ms.assetid: c5150d18-e8b7-48a7-ada7-77c583af6e41
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# dbo.sysnotifications (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains one row for each notification.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**alert_id**|**int**|ID of the alert.|  
|**operator_id**|**int**|Operator ID to whom this notification should be sent.|  
|**notification_method**|**tinyint**|Method of notification:<br /><br /> **1** = E-mail<br /><br /> **2** = Pager<br /><br /> **4** = **netsend**<br /><br /> **7** = All|  
  
  
