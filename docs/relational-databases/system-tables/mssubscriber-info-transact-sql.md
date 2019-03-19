---
title: "MSsubscriber_info (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSsubscriber_info_TSQL"
  - "MSsubscriber_info"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSsubscriber_info system table"
ms.assetid: 5ca22f41-6020-4f72-8110-e69baf3447cb
author: stevestein
ms.author: sstein
manager: craigg
---
# MSsubscriber_info (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSsubscriber_info** table contains one row for each Publisher/Subscriber pair that is being pushed subscriptions from the local Distributor. This table is stored in the distribution database.  
  
 **Note** This system table has been deprecated and is being maintained to support previous versions of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Definition  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publisher**|**sysname**|The name of the Publisher.|  
|**subscriber**|**sysname**|The name of the Subscriber.|  
|**type**|**tinyint**|The subscriber type:<br /><br /> **0** = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscriber.<br /><br /> **1** = ODBC data source.|  
|**login**|**sysname**|The login for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. Stored in encrypted format if Subscriber is added with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication mode.|  
|**password**|**nvarchar(524)**|The password for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. Stored in encrypted format if Subscriber is added with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication mode.|  
|**description**|**nvarchar(255)**|The description of the Subscriber.|  
|**security_mode**|**int**|The implemented security mode:<br /><br /> **0** = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.<br /><br /> **1** = [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Authentication.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
