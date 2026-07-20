---
title: "Estimate the Size of a Table"
description: Use this procedure to estimate the amount of space that is required to store data in a table in SQL Server.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 09/22/2025
ms.service: sql
ms.subservice: supportability
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "pages [SQL Server], space"
  - "space [SQL Server], tables"
  - "row size [SQL Server]"
  - "size [SQL Server], tables"
  - "column size [SQL Server]"
  - "predicting table size [SQL Server]"
  - "table size [SQL Server]"
  - "estimating table size"
  - "clustered indexes, table size"
  - "disk space [SQL Server], tables"
  - "space allocation [SQL Server], table size"
  - "designing databases [SQL Server], estimating size"
  - "reserved free rows per page [SQL Server]"
  - "calculating table size"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Estimate the size of a table

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

You can use the following steps to estimate the amount of space required to store data in a table:

1. Calculate the space required for the heap or clustered index following the instructions in [Estimate the size of a heap](estimate-the-size-of-a-heap.md) or [Estimate the size of a clustered index](estimate-the-size-of-a-clustered-index.md).

1. For each nonclustered index, calculate the space required for it by following the instructions in [Estimate the size of a nonclustered index](estimate-the-size-of-a-nonclustered-index.md).

1. Add the values calculated in steps 1 and 2.

## Related content

- [Estimate the size of a database](estimate-the-size-of-a-database.md)
- [Estimate the size of a heap](estimate-the-size-of-a-heap.md)
- [Estimate the size of a clustered index](estimate-the-size-of-a-clustered-index.md)
- [Estimate the size of a nonclustered index](estimate-the-size-of-a-nonclustered-index.md)
