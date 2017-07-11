---
title: "sys.sysforeignkeys (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sysforeignkeys"
  - "sys.sysforeignkeys"
  - "sys.sysforeignkeys_TSQL"
  - "sysforeignkeys_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysforeignkeys system table"
  - "sys.sysforeignkeys compatibility view"
ms.assetid: 41544236-1c46-4501-be88-18c06963b6e8
caps.latest.revision: 36
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sys.sysforeignkeys (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains information about the FOREIGN KEY constraints that are in the definitions of tables in the database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658)).|  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**constid**|**int**|ID of the FOREIGN KEY constraint.|  
|**fkeyid**|**int**|Object ID of the table with the FOREIGN KEY constraint.|  
|**rkeyid**|**int**|Object ID of the table referenced in the FOREIGN KEY constraint.|  
|**fkey**|**smallint**|ID of the referencing column.|  
|**rkey**|**smallint**|ID of the referenced column.|  
|**keyno**|**smallint**|Position of the column in the reference column list.|  
  
## See Also  
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)   
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)  
  
  
