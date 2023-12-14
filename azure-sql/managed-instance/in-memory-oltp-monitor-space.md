---
title: Monitor XTP In-memory storage
description: Estimate and monitor XTP in-memory storage use, capacity; resolve capacity error 41823.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma
ms.date: 12/12/2023
ms.service: sql-managed-instance
ms.subservice: performance
ms.topic: how-to
ms.custom:
  - sqldbrb=2
monikerRange: "=azuresql-mi"
---
# Monitor in-memory OLTP storage in Azure SQL Database and Azure SQL Managed Instance
[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

With [in-memory OLTP](in-memory-oltp-overview.md), data in memory-optimized tables and table variables resides in in-memory OLTP storage.

## Determine whether data fits within the in-memory OLTP storage cap

Each supported pricing tier includes a certain amount of **Max In-Memory OLTP memory**, a [limit determined by the number of vCores](resource-limits.md?view=azuresql-mi&preserve-view=true).

Estimating memory requirements for a memory-optimized table works the same way for SQL Server as it does in Azure SQL Database and Azure SQL Managed Instance. Take a few minutes to review [Estimate memory requirements](/sql/relational-databases/in-memory-oltp/estimate-memory-requirements-for-memory-optimized-tables?view=azuresqlmi-current&preserve-view=true).

Table and table variable rows, as well as indexes, count toward the max user data size. In addition, `ALTER TABLE` needs enough room to create a new version of the entire table and its indexes.

Once this limit is exceeded, insert and update operations might start failing. At that point you need to either delete data to reclaim memory, or upgrade the service tier or compute size of your database. For more information, see [Correct out-of-In-memory OLTP storage situations - Errors 41823 and 41840](#correct-out-of-in-memory-oltp-storage-situations---errors-41823-and-41840).

## Monitoring and alerting

You can monitor in-memory storage use as a percentage of the storage cap for your compute size in the [Azure portal](https://portal.azure.com/):

1. On the **Database** page, locate the **Resource utilization** box and select **Edit**.
1. Select the metric `In-memory OLTP Storage percentage`.
1. To add an alert, select on **Resource Utilization** box to open the **Metric** page, then select **Add alert**.

Or, use the following query to show the in-memory storage utilization:

    ```sql
    SELECT xtp_storage_percent FROM sys.dm_db_resource_stats;
    ```

## Correct out-of-In-memory OLTP storage situations - Errors 41823 and 41840

Meeting the in-memory OLTP storage cap in your database results in INSERT, UPDATE, ALTER and CREATE operations failing with error 41823 (for single databases) or error 41840 (for elastic pools). Both errors cause the active transaction to abort. 

Errors 41823 and 41840 indicate that the memory-optimized tables and table variables in the database or pool reached the maximum in-memory OLTP storage size.

To resolve this error, either:

- Delete data from the memory-optimized tables, potentially offloading the data to traditional, disk-based tables; or,
- Upgrade the service tier to one with enough in-memory storage for the data you need to keep in memory-optimized tables.

> [!NOTE]
> In rare cases, errors 41823 and 41840 can be transient, meaning there is enough available in-memory OLTP storage, and retrying the operation succeeds. We therefore recommend to both monitor the overall available in-memory OLTP storage and to retry when first encountering error 41823 or 41840. For more information about retry logic, see [Conflict Detection and Retry Logic with in-memory OLTP](/sql/relational-databases/In-memory-oltp/transactions-with-memory-optimized-tables#conflict-detection-and-retry-logic).

## Related content

- [Monitor Microsoft Azure SQL Database performance using dynamic management views](monitoring-with-dmvs.md?view=azuresql-mi&preserve-view=true)
