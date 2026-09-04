---
title: Bulk-Load Data into Tables in a Merge Publication
description: Bulk-loading data into merge replication tables skips tracking triggers by default. Discover two reliable ways to generate the metadata your publication needs.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "bulk load [SQL Server replication]"
  - "merge replication bulk loading [SQL Server replication]"
  - "sp_addtabletocontents"
dev_langs:
  - "TSQL"
---
# Bulk-load data into tables in a merge publication
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  When you load data into tables by using the [bcp Utility](../../tools/bcp-utility.md) or the [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) command, merge replication triggers that keep tracking data in the [MSmerge_contents](../../relational-databases/system-tables/msmerge-contents-transact-sql.md) system table don't fire by default. You can either force the merge replication triggers to fire as you load the data or insert the generated replication metadata programmatically after the bulk copy operation by using replication stored procedures.  
  
### To bulk-load data into tables published by merge replication using the bcp utility  
  
1.  At either the Publisher or Subscriber, run the [bcp Utility](../../tools/bcp-utility.md) or [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) to insert data into a table published by merge replication.  
  
1.  Use one of the following methods to ensure that replication metadata is generated for the inserted data:  
  
    -   Run the bulk copy operation by using the `FIRE_TRIGGERS` option.  
  
    -   On the database where you inserted data, run [sp_addtabletocontents (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-addtabletocontents-transact-sql.md). Specify the table name where you inserted the data for `@table_name`.  
  
  
