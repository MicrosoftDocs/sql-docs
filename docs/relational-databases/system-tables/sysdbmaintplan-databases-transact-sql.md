---
title: "sysdbmaintplan_databases (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sysdbmaintplan_databases"
  - "sysdbmaintplan_databases_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysdbmaintplan_databases system table"
ms.assetid: f8413a44-8fcc-4899-84f2-b4afe0f8ec08
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sysdbmaintplan_databases (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  This table is included to preserve existing information for instances upgraded from a previous version of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions do not change the contents of this table. This table is stored in the **msdb** database.  
  
 [!INCLUDE[ssNoteDepNextAvoid](../../includes/ssnotedepnextavoid-md.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**plan_id**|**Uniqueidentifier**|Maintenance plan ID.|  
|**database_name**|**sysname**|Name of the database associated with the database maintenance plan.|  
  
  
