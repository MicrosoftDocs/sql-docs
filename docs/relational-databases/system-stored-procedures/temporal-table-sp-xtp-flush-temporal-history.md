---
description: "sp_xtp_flush_temporal_history (Transact-SQL)"
title: "sp_xtp_flush_temporal_history | Microsoft Docs"
ms.custom: ""
ms.date: "02/21/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: conceptual
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_xtp_flush_temporal_history"
  - "sp_xtp_flush_temporal_history_TSQL"
  - "sys.sp_xtp_flush_temporal_history"
  - "sys.sp_xtp_flush_temporal_history_TSQL"
helpviewer_keywords: 
  - "sp_xtp_flush_temporal_history"
ms.assetid: 322e3170-93f8-468a-a123-104ce7bd7fad
author: markingmyname
ms.author: maghan
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_xtp_flush_temporal_history (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

  Invokes the data flush task to move all committed rows from in-memory staging table to the disk-based history table.  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.sp_xtp_flush_temporal_history @schema_name, @object_name  
  
```  
  
## Arguments  
 *\@schema_name*  
 The schema name for the current or temporal table  
  
 *\@object_name*  
 The name of the current or temporal table  
  
## Return Code Values  
 0 (success) or >0 (failure)  
  
## Permissions  
 Requires db_owner permissions.  
  
## See Also  
 [Performance Considerations with Memory-Optimized System-Versioned Temporal Tables](../../relational-databases/tables/memory-optimized-system-versioned-temporal-tables-performance.md)  
  
  
