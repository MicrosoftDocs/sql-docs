---
title: "ALTER PARTITION SCHEME (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER PARTITION SCHEME"
  - "ALTER_PARTITION_SCHEME_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ALTER PARTITION SCHEME statement"
  - "partition schemes [SQL Server], modifying"
  - "modifying partition schemes"
  - "adding filegroups"
  - "NEXT USED filegroups"
ms.assetid: f01d6880-9800-4cfb-8d11-d4be21efc8ca
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# ALTER PARTITION SCHEME (Transact-SQL)

[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Adds a filegroup to a partition scheme or alters the designation of the NEXT USED filegroup for the partition scheme. 

>[!NOTE]
>In Azure SQL Database only primary filegroups are supported.  
  
 ![Article link icon](../../database-engine/configure-windows/media/topic-link.gif "Article link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
ALTER PARTITION SCHEME partition_scheme_name   
NEXT USED [ filegroup_name ] [ ; ]  
```  
  
## Arguments  
 *partition_scheme_name*  
 Is the name of the partition scheme to be altered.  
  
 *filegroup_name*  
 Specifies the filegroup to be marked by the partition scheme as NEXT USED. This means the filegroup will accept a new partition that is created by using an [ALTER PARTITION FUNCTION](../../t-sql/statements/alter-partition-function-transact-sql.md) statement.  
  
 In a partition scheme, only one filegroup can be designated NEXT USED. A filegroup that is not empty can be specified. If *filegroup_name* is specified and there currently is no filegroup marked NEXT USED, *filegroup_name* is marked NEXT USED. If *filegroup_name* is specified, and a filegroup with the NEXT USED property already exists, the NEXT USED property transfers from the existing filegroup to *filegroup_name*.  
  
 If *filegroup_name* is not specified and a filegroup with the NEXT USED property already exists, that filegroup loses its NEXT USED state so that there are no NEXT USED filegroups in *partition_scheme_name*.  
  
 If *filegroup_name* is not specified, and there are no filegroups marked NEXT USED, ALTER PARTITION SCHEME returns a warning.  
  
## Remarks  
 Any filegroup affected by ALTER PARTITION SCHEME must be online.  
  
## Permissions  
 The following permissions can be used to execute ALTER PARTITION SCHEME:  
  
-   ALTER ANY DATASPACE permission. This permission defaults to members of the **sysadmin** fixed server role and the **db_owner** and **db_ddladmin** fixed database roles.  
  
-   CONTROL or ALTER permission on the database in which the partition scheme was created.  
  
-   CONTROL SERVER or ALTER ANY DATABASE permission on the server of the database in which the partition scheme was created.  
  
## Examples  
 The following example assumes the partition scheme `MyRangePS1` and the filegroup `test5fg` exist in the current database.  
  
```  
ALTER PARTITION SCHEME MyRangePS1  
NEXT USED test5fg;  
```  
  
 Filegroup `test5fg` will receive any additional partition of a partitioned table or index as a result of an ALTER PARTITION FUNCTION statement.  
  
## See Also  
 [CREATE PARTITION SCHEME &#40;Transact-SQL&#41;](../../t-sql/statements/create-partition-scheme-transact-sql.md)   
 [DROP PARTITION SCHEME &#40;Transact-SQL&#41;](../../t-sql/statements/drop-partition-scheme-transact-sql.md)   
 [CREATE PARTITION FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-partition-function-transact-sql.md)   
 [ALTER PARTITION FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-partition-function-transact-sql.md)   
 [DROP PARTITION FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-partition-function-transact-sql.md)   
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sys.partition_schemes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partition-schemes-transact-sql.md)   
 [sys.data_spaces &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-data-spaces-transact-sql.md)   
 [sys.destination_data_spaces &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-destination-data-spaces-transact-sql.md)   
 [sys.partitions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partitions-transact-sql.md)   
 [sys.tables &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-tables-transact-sql.md)   
 [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)   
 [sys.index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md)  
  
  
