---
title: Bulk Copy Operations in SQL Server
description: Describes bulk copy functionality in the SqlClient .NET Data Provider for SQL Server. Bulk copy is a fast way to load large amounts of data into SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mahyon
ms.date: 08/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
---
# Bulk copy operations in SQL Server

[!INCLUDE [Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

[!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] includes a popular command-line utility named **`bcp`**. You can use **`bcp`** to quickly bulk copy large files into tables or views in SQL Server databases. The <xref:Microsoft.Data.SqlClient.SqlBulkCopy> class lets you write managed code solutions with similar functionality. There are other ways to load data into a table (`INSERT` statements, for example), but <xref:Microsoft.Data.SqlClient.SqlBulkCopy> gives better performance.

With the <xref:Microsoft.Data.SqlClient.SqlBulkCopy> class, you can perform:

- A single bulk copy operation
- Multiple bulk copy operations
- A bulk copy operation within a transaction

## In this section

| Article | Description |
| --- | --- |
| [Bulk copy example setup](bulk-copy-example-setup.md) | Describes the tables used in the bulk copy examples and provides SQL scripts for creating the tables in the AdventureWorks database. |
| [Single bulk copy operations](single-bulk-copy-operations.md) | Describes how to do a single bulk copy of data into a database instance using the <xref:Microsoft.Data.SqlClient.SqlBulkCopy> class. It includes how to do the bulk copy operation using Transact-SQL statements and the <xref:Microsoft.Data.SqlClient.SqlCommand> class. |
| [Multiple bulk copy operations](multiple-bulk-copy-operations.md) | Describes how to do multiple bulk copy operations of data into a database instance using the <xref:Microsoft.Data.SqlClient.SqlBulkCopy> class. |
| [Transaction and bulk copy operations](transaction-bulk-copy-operations.md) | Describes how to do a bulk copy operation within a transaction, including how to commit or roll back the transaction. |
| [Order hints for bulk copy operations](bulk-copy-order-hints.md) | Describes how to use order hints to improve bulk copy performance. |

## Related content

- [SQL Server and ADO.NET](index.md)
