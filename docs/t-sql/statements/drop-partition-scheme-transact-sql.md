---
title: "DROP PARTITION SCHEME (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP PARTITION SCHEME"
  - "DROP_PARTITION_SCHEME_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DROP PARTITION SCHEME statement"
  - "deleting partition schemes"
  - "dropping partition schemes"
  - "removing partition schemes"
  - "partition schemes [SQL Server], removing"
ms.assetid: 6efbc87c-1c92-4e43-96a7-e0f30f1db185
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# DROP PARTITION SCHEME (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Removes a partition scheme from the current database. Partition schemes are created by using [CREATE PARTITION SCHEME](../../t-sql/statements/create-partition-scheme-transact-sql.md) and modified by using [ALTER PARTITION SCHEME](../../t-sql/statements/alter-partition-scheme-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DROP PARTITION SCHEME partition_scheme_name [ ; ]  
```  
  
## Arguments  
 *partition_scheme_name*  
 Is the name of the partition scheme to be dropped.  
  
## Remarks  
 A partition scheme can be dropped only if there are no tables or indexes currently using the partition scheme. If there are tables or indexes using the partition scheme, DROP PARTITION SCHEME returns an error. DROP PARTITION SCHEME does not remove the filegroups themselves.  
  
## Permissions  
 The following permissions can be used to execute DROP PARTITION SCHEME:  
  
-   ALTER ANY DATASPACE permission. This permission defaults to members of the **sysadmin** fixed server role and the **db_owner** and **db_ddladmin** fixed database roles.  
  
-   CONTROL or ALTER permission on the database in which the partition scheme was created.  
  
-   CONTROL SERVER or ALTER ANY DATABASE permission on the server of the database in which the partition scheme was created.  
  
## Examples  
 The following example drops the partition scheme `myRangePS1` from the current database:  
  
```  
DROP PARTITION SCHEME myRangePS1;  
```  
  
## See Also  
 [CREATE PARTITION SCHEME &#40;Transact-SQL&#41;](../../t-sql/statements/create-partition-scheme-transact-sql.md)   
 [ALTER PARTITION SCHEME &#40;Transact-SQL&#41;](../../t-sql/statements/alter-partition-scheme-transact-sql.md)   
 [sys.partition_schemes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partition-schemes-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sys.data_spaces &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-data-spaces-transact-sql.md)   
 [sys.destination_data_spaces &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-destination-data-spaces-transact-sql.md)   
 [sys.partitions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partitions-transact-sql.md)   
 [sys.tables &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-tables-transact-sql.md)   
 [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)   
 [sys.index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md)  
  
  
