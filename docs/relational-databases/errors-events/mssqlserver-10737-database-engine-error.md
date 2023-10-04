---
title: "MSSQLSERVER_10737"
description: "MSSQLSERVER_10737"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "10737 (Database Engine error)"
---
# MSSQLSERVER_10737
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|MSSQLSERVER|  
|Event ID|10737|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|REBUILD_PARTITION_ALL_NOT_SPECIFIED|  
|Message Text|In an ALTER TABLE REBUILD or ALTER INDEX REBUILD statement, when a partition is specified in a DATA_COMPRESSION clause, PARTITION=ALL must be specified. The PARTITION=ALL clause is used to reinforce that all partitions of the table or index will be rebuilt, even if only a subset is specified in the DATA_COMPRESSION clause.|  
  
## User Action  
Add the PARTITION=ALL clause to the ALTER TABLE or ALTER INDEX statement. Or, to rebuild a specific partition, use REBUILD PARTITION = \<partition-number-expr> WITH (DATA_COMPRESSION={ON | OFF}).  
  
