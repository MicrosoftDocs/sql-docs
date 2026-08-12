---
title: "Configure Parallel Index Operations"
description: Learn about the max degree of parallelism and learn how to modify this setting in SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 09/22/2025
ms.service: sql
ms.subservice: table-view-index
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "index parallel operations [SQL Server]"
  - "processors [SQL Server], parallel index operations"
  - "max degree of parallelism option"
  - "MAXDOP index option, parallel index operations"
  - "parallel index operations [SQL Server]"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Configure parallel index operations

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

This article defines max degree of parallelism and explains how to modify this setting in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

On multiprocessor systems that are running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Enterprise or higher, index statements might use multiple processors (CPUs) to perform the scan, sort, and index operations associated with the index statement just like other queries do. The number of CPUs used to run a single index statement is determined by the [max degree of parallelism](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md) server configuration option, the current workload, and the index statistics.

The max degree of parallelism option determines the maximum number of processors to use in parallel plan execution. If the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] detects that the system is busy, the degree of parallelism of the index operation is automatically reduced before statement execution starts. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] can also reduce the degree of parallelism if the leading key column of a non-partitioned index has a limited number of distinct values or the frequency of each distinct value varies significantly. For more information, see [Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md#parallel-query-processing).

> [!NOTE]  
> Parallel index operations aren't available in every [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] edition. [!INCLUDE [editions-latest](../../includes/editions-latest.md)]

## Limitations

- The number of processors that are used by the query optimizer typically provides optimal performance. However, operations such as creating, rebuilding, or dropping very large indexes are resource intensive and can cause insufficient resources for other applications and database operations for the duration of the index operation.

  When this problem occurs, you can manually configure the maximum number of processors that are used to run the index statement by limiting the number of processors to use for the index operation.

- The `MAXDOP` index option overrides the max degree of parallelism configuration option only for the query specifying this option. The following table lists the valid integer values that can be specified with the max degree of parallelism configuration option and the `MAXDOP` index option.

  | Value | Description |
  | --- | --- |
  | 0 | Specifies that the server determines the number of CPUs that are used, depending on the current system workload. This is the default value and recommended setting. |
  | 1 | Suppresses parallel plan generation. The operation is executed serially. |
  | 2-64 | Limits the number of processors to the specified value. Fewer processors might be used depending on the current workload. If a value larger than the number of available CPUs is specified, the actual number of available CPUs is used. |

- Parallel index execution and the `MAXDOP` index option apply to the following [!INCLUDE [tsql](../../includes/tsql-md.md)] statements:

  - [CREATE INDEX](../../t-sql/statements/create-index-transact-sql.md)
  - [ALTER INDEX (...) REBUILD](../../t-sql/statements/alter-index-transact-sql.md)
  - [DROP INDEX](../../t-sql/statements/drop-index-transact-sql.md) (This applies to clustered indexes only.)
  - [ALTER TABLE ADD (index) CONSTRAINT](../../t-sql/statements/alter-table-table-constraint-transact-sql.md)
  - [ALTER TABLE DROP (clustered index) CONSTRAINT](../../t-sql/statements/alter-table-table-constraint-transact-sql.md)

- The `MAXDOP` index option can't be specified in the `ALTER INDEX (...) REORGANIZE` statement.

- Memory requirements for partitioned index operations that require sorting can be greater if the Query Optimizer applies degrees of parallelism to the build operation. The higher the degrees of parallelism, the greater the memory requirement is. For more information, see [Partitioned tables and indexes](../partitions/partitioned-tables-and-indexes.md).

## Permissions

Requires `ALTER` permission on the table or view.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

#### Set max degree of parallelism on an index

1. In Object Explorer, select the plus sign to expand the database that contains the table on which you want to set max degree of parallelism for an index.

1. Expand the **Tables** folder.
1. Select the plus sign to expand the table on which you want to set max degree of parallelism for an index.
1. Expand the **Indexes** folder.
1. Right-click the index for which you want to set the max degree of parallelism and select **Properties**.
1. Under **Select a page**, select **Options**.
1. Select **Maximum degree of parallelism**, and then enter some value between 1 and 64.
1. Select **OK**.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

### Set max degree of parallelism on an existing index

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This code alters the `IX_ProductVendor_VendorID` index on the `Purchasing.ProductVendor` table so that, if the server has eight or more processors, the Database Engine limits the execution of the index operation to eight or fewer processors.

   ```sql
   USE AdventureWorks2022;
   GO

   ALTER INDEX IX_ProductVendor_VendorID
       ON Purchasing.ProductVendor REBUILD WITH(MAXDOP = 8);
   GO
   ```

For more information, see [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md).

### Specify max degree of parallelism when creating a new index

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

   ```sql
   USE AdventureWorks2022;
   GO

   CREATE INDEX IX_ProductVendor_NewVendorID
       ON Purchasing.ProductVendor(BusinessEntityID) WITH (MAXDOP = 8);
   GO
   ```

## Related content

- [Query Processing Architecture Guide](../query-processing-architecture-guide.md#parallel-query-processing)
- [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md)
- [ALTER INDEX (Transact-SQL)](../../t-sql/statements/alter-index-transact-sql.md)
- [DROP INDEX (Transact-SQL)](../../t-sql/statements/drop-index-transact-sql.md)
- [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md)
- [ALTER TABLE table_constraint (Transact-SQL)](../../t-sql/statements/alter-table-table-constraint-transact-sql.md)
- [ALTER TABLE index_option (Transact-SQL)](../../t-sql/statements/alter-table-index-option-transact-sql.md)
