---
title: "systranschemas (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
applies_to: 
  - "SQL Server"
f1_keywords: 
  - "systranschemas"
  - "systranschemas_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "systranschemas system table"
ms.assetid: 864c3966-cb61-4f2b-8939-ccda112de853
caps.latest.revision: 12
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# systranschemas (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **systranschemas** table is used to track schema changes in articles published in transactional and snapshot publications. This table is stored in both publication and subscription databases.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**tabid**|**int**|Identifies the table article on which the schema change occurred.|  
|**startlsn**|**binary**|LSN value at the start of the schema change.|  
|**endlsn**|**binary**|LSN value at the end of the schema change.|  
|**typeid**|**int**|Type of schema change.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)  
  
  