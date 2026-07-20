---
title: "Estimate the Size of a Database"
description: When you design a database in SQL Server, estimating its size can help you determine the hardware configuration you need for performance and disk space.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 09/22/2025
ms.service: sql
ms.subservice: supportability
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "space allocation [SQL Server], database size"
  - "calculating database size"
  - "increasing database size"
  - "database size [SQL Server], estimating"
  - "predicting database size"
  - "size [SQL Server], databases"
  - "estimating database size"
  - "designing databases [SQL Server], estimating size"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Estimate the size of a database

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

When you design a database, you might have to estimate how large the database will be when filled with data. Estimating the size of the database can help you determine the hardware configuration you'll require to do the following:

- Achieve the performance required by your applications.
- Guarantee the appropriate physical amount of disk space required to store the data and indexes.

Estimating the size of a database can also help you determine whether the database design needs refining. For example, you might determine that the estimated size of the database is too large to implement in your organization and that more normalization is required. Conversely, the estimated size might be smaller than expected. This would allow you to denormalize the database to improve query performance.

To estimate the size of a database, estimate the size of each table individually and then add the values obtained. The size of a table depends on whether the table has indexes and, if they do, what type of indexes.

## In this section

| Article | Description |
| --- | --- |
| [Estimate the size of a table](estimate-the-size-of-a-table.md) | Defines the steps and calculations needed to estimate the amount of space required to store the data in a table and associated indexes. |
| [Estimate the size of a heap](estimate-the-size-of-a-heap.md) | Defines the steps and calculations needed to estimate the amount of space required to store the data in a heap. A heap is a table that doesn't have a clustered index. |
| [Estimate the size of a clustered index](estimate-the-size-of-a-clustered-index.md) | Defines the steps and calculations needed to estimate the amount of space required to store the data in a clustered index. |
| [Estimate the size of a nonclustered index](estimate-the-size-of-a-nonclustered-index.md) | Defines the steps and calculations needed to estimate the amount of space required to store the data in a nonclustered index. |
