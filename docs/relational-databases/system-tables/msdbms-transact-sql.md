---
title: "MSdbms (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSdbms_TSQL"
  - "MSdbms"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSdbms system table"
ms.assetid: 2be631bf-de09-4e7a-9ccb-d6c37b81c237
author: stevestein
ms.author: sstein
manager: craigg
---
# MSdbms (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSdbms** table contains a master list of all versions of the database management systems (DBMS) supported for heterogeneous database replication. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**dbms_id**|**int**|Identifies each unique DBMS and version.|  
|**dbms**|**sysname**|The DBMS name.<br /><br /> MSSQLSERVER<br /><br /> DB2<br /><br /> ORACLE<br /><br /> SYBASE|  
|**version**|**varchar(10)**|The DBMS version.|  
  
## See Also  
 [Heterogeneous Database Replication](../../relational-databases/replication/non-sql/heterogeneous-database-replication.md)   
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
