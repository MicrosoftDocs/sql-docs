---
description: "sys.sysoledbusers (Transact-SQL)"
title: "sys.sysoledbusers (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sys.sysoledbusers"
  - "sys.sysoledbusers_TSQL"
  - "sysoledbusers"
  - "sysoledbusers_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysoledbusers system table"
  - "sys.sysoledbusers compatibility view"
ms.assetid: fe924c17-9cad-4b2b-8124-1e0fd82931e3
author: rwestMSFT
ms.author: randolphwest
---
# sys.sysoledbusers (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

    
> [!IMPORTANT]  
>  This [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] system table is included in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as a view for backward compatibility only. We recommend that you use [catalog views](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md) instead.  
  
 Contains one row for each user and password mapping for the specified linked server. **sysoledbusers** is stored in the **master** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**rmtsrvid**|**smallint**|Security identification number (SID) of the server.|  
|**rmtloginame**|**nvarchar(**128**)**|Name of the remote login that **loginsid** maps to for linked **rmtservid**.|  
|**rmtpassword**|**nvarchar(**128**)**|Returns NULL.|  
|**loginsid**|**varbinary(**85**)**|SID of the local login to be mapped.|  
|**status**|**smallint**|If 1, the mapping should use the credentials of the user.|  
|**changedate**|**datetime**|Date the mapping information was last changed.|  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)  
  
  
