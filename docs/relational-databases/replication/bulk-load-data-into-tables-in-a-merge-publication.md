---
title: "Bulk-Load Data into Tables in a Merge Publication | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "bulk load [SQL Server replication]"
  - "merge replication bulk loading [SQL Server replication]"
  - "sp_addtabletocontents"
ms.assetid: 16e6498a-b449-4051-aec4-ea814a2ad993
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Bulk-Load Data into Tables in a Merge Publication
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  When data is loaded into tables using the [bcp Utility](../../tools/bcp-utility.md) or the [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) command, by default, the merge replication triggers that maintain tracking data in the [MSmerge_contents](../../relational-databases/system-tables/msmerge-contents-transact-sql.md) system table are not fired. You can either force the merge replication triggers to fire as the data is loaded, or you can insert the generated replication metadata programmatically after the bulk copy operation using replication stored procedures.  
  
### To bulk-load data into tables published by merge replication using the bcp utility  
  
1.  At either the Publisher or Subscriber, execute the [bcp Utility](../../tools/bcp-utility.md) or [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) to insert data into a table published using merge replication.  
  
2.  Use one of the following methods to ensure that replication metadata is generated for the inserted data.  
  
    -   Execute the bulk copy using the FIRE_TRIGGERS option.  
  
    -   On the database into which data was inserted, execute [sp_addtabletocontents &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addtabletocontents-transact-sql.md). Specify the table name into which the data was inserted for **@table_name**.  
  
  
