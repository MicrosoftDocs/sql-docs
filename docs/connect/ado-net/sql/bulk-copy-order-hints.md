---
title: Order hints for bulk copy operations
description: Describes how to use order hints in bulk copy operations.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-davidengel
ms.date: 06/01/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# Order hints for bulk copy operations

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

Bulk copy operations offer significant performance advantages over other methods for loading data into a SQL Server table. Performance can be further enhanced by using order hints. Specifying order hints for your bulk copy operations can lower the insertion time of sorted data into tables with clustered indexes.

By default, the bulk insert operation assumes the incoming data is unordered. SQL Server forces an intermediate sort of this data before bulk loading it. If you know your incoming data is already sorted, you can use order hints to tell the bulk copy operation about the sort order of any destination columns that are part of a clustered index.
  
## Adding order hints to a bulk copy operation

The following example bulk copies data from a source table in the **AdventureWorks** sample database to a destination table in the same database.
A SqlBulkCopyColumnOrderHint object is created to define the sort order for the **ProductNumber** column in the destination table. The order hint is then added to the SqlBulkCopy instance, which will append the appropriate order hint argument to the resulting `INSERT BULK` query.

[!code-csharp [SqlBulkCopy.ColumnOrderHint#1](~/../sqlclient/doc/samples/SqlBulkCopy_ColumnOrderHint.cs#1)]

## Next steps

- [Bulk copy operations in SQL Server](bulk-copy-operations-sql-server.md)
