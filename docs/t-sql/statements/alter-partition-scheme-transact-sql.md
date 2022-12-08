---
title: "ALTER PARTITION SCHEME (Transact-SQL)"
description: ALTER PARTITION SCHEME (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "4/5/2022"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER PARTITION SCHEME"
  - "ALTER_PARTITION_SCHEME_TSQL"
helpviewer_keywords:
  - "ALTER PARTITION SCHEME statement"
  - "partition schemes [SQL Server], modifying"
  - "modifying partition schemes"
  - "adding filegroups"
  - "NEXT USED filegroups"
dev_langs:
  - "TSQL"
---
# ALTER PARTITION SCHEME (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Adds a filegroup to a partition scheme or alters the designation of the NEXT USED filegroup for the partition scheme.

Learn more about filegroups and partitioning strategies in [Filegroups](../../relational-databases/partitions/partitioned-tables-and-indexes.md#filegroups).

>[!NOTE]
>In Azure SQL Database only primary filegroups are supported.  
  
![Article link icon](../../database-engine/configure-windows/media/topic-link.gif "Article link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
ALTER PARTITION SCHEME partition_scheme_name   
NEXT USED [ filegroup_name ] [ ; ]  
```  
  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

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
  
```sql  
ALTER PARTITION SCHEME MyRangePS1  
NEXT USED test5fg;  
```  
  
Filegroup `test5fg` will receive any additional partition of a partitioned table or index as a result of an ALTER PARTITION FUNCTION statement.  
  
## Next steps

Learn more about table partitioning and related concepts in the following articles:

- [CREATE PARTITION SCHEME &#40;Transact-SQL&#41;](../../t-sql/statements/create-partition-scheme-transact-sql.md)
- [Modify a Partition Function](../../relational-databases/partitions/modify-a-partition-function.md)
- [Partitioned tables and indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md)
- [Create partitioned tables and indexes](../../relational-databases/partitions/create-partitioned-tables-and-indexes.md)
- [Modify a Partition Scheme](../../relational-databases/partitions/modify-a-partition-scheme.md)
