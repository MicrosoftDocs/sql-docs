---
title: "MSSQLSERVER_8645"
description: "MSSQLSERVER_8645"
author: MashaMSFT
ms.author: mathoma
ms.reviewer: jopilov
ms.date: 04/01/2025
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "8645 (Database Engine error)"
---
# MSSQLSERVER_8645

[!INCLUDE [SQL Server](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

## Details

| Attribute | Value |
| :--- | :--- |
| Product Name | SQL Server |
| Event ID | 8645 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | MEMTIMEDOUT_ERR |
| Message Text | A timeout occurred while waiting for memory resources to execute the query in resource pool '%ls' (%ld). Rerun the query. |

## Explanation

This error is raised if a SQL Server request has waited for a query execution (QE) memory grant for an extended period of time and memory wasn't available. Query execution memory is primarily used for sort operations, hash operations, bulk copy operations, and index creation and population. A query that performs one of these operations requests a memory grant. If no memory is available, the query is set to wait on a [RESOURCE_SEMAPHORE](../system-dynamic-management-views/sys-dm-exec-query-memory-grants-transact-sql.md) until memory becomes available. 

If the memory isn't available after waiting for a timeout period dynamically determined by the query cost as estimated by the query optimizer, then SQL Server terminates the query with error 8645 `A timeout occurred while waiting for memory resources to execute the query in the resource pool 'default'.` The timeout value varies slightly between versions of SQL Server, but has a max wait timeout value of 24 hours. You can see the timeout value set at the server level by looking at `timeout_sec` in [sys.dm_exec_query_memory_grants](../system-dynamic-management-views/sys-dm-exec-query-memory-grants-transact-sql.md).

For a detailed explanation with troubleshooting steps, review [Slow performance or low memory issues caused by memory grants in SQL Server](/troubleshoot/sql/database-engine/performance/troubleshoot-memory-grant-issues).

## Cause

This error can be seen in relation to memory grants and prolonged waits for memory to become available. Typically when a query completes, it releases the execution memory it uses. Therefore, if you see this error, it means that the timed-out query has waited for other requests to complete beyond the time-out period. There could be just one single request that consumed all the QE memory available or there could be many requests and combined, their memory grants have exhausted the QE memory. If you have such long running requests in your workload, you must take measures to improve their execution duration and decrease the amount of QE memory they use.

## User action

If you aren't using Resource Governor to limit the pool of memory for certain workloads, you can verify the overall server state and workload. If you're using Resource Governor check the resource pool or workload group settings.

The following list summarizes the steps detailed in the aforementioned article. These steps can help in reducing or eliminating QE memory errors:

1. Identify which requests in SQL Server are the large memory grant or QE memory consumers. For more information, see [How to identify waits for query execution memory](/troubleshoot/sql/database-engine/performance/troubleshoot-memory-grant-issues#how-to-identify-waits-for-query-execution-memory).
1. Rewrite queries to minimize or avoid sort and hash operations.
1. Update statistics and keep them updated regularly to ensure SQL Server estimates memory grant correctly.
1. Create appropriate indexes for the query or queries identified. Indexes can reduce the large number of rows processed, thus changing the JOIN algorithms and reducing the size of grants or completely eliminating them.
1. Use the OPTION (min_grant_percent = XX, max_grant_percent = XX) hint in your queries where possible.
1. Use Resource Governor to limit the effect of QE memory usage only to a certain workload.
1. SQL Server 2017 and 2019 use adaptive query processing, allowing the memory grant feedback mechanism to adjust memory grant size dynamically at runtime. This feature may prevent memory grant issues in the first place.
1. Increase SQL Server memory or adjust existing settings.
    1. Check the following SQL Server memory configuration parameters:

       - **max server memory** - increase if needed
       - **min server memory**
       - **min memory per query**

    1. Notice unusual settings. Correct them as necessary. Account for increased memory requirements for [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]. Default and recommended settings are listed in [Server memory configuration](../../database-engine/configure-windows/server-memory-server-configuration-options.md#recommendations) options. 

1. Increase memory at the OS level (physical or virtual RAM).
1. Verify whether other applications or services are consuming memory on this server. Reconfigure less critical applications or services to consume less memory or move them to a separate server. This action can remove external memory pressure.
1. Run the following DBCC commands to free several SQL Server memory caches - a temporary measure.

- DBCC FREESYSTEMCACHE
- DBCC FREESESSIONCACHE
- DBCC FREEPROCCACHE

## Related content

- [sys.dm_exec_query_memory_grants](../system-dynamic-management-views/sys-dm-exec-query-memory-grants-transact-sql.md)
- [Troubleshoot slow performance or low memory issues caused by memory grants in SQL Server](/troubleshoot/sql/database-engine/performance/troubleshoot-memory-grant-issues)
- [Server memory configuration](../../database-engine/configure-windows/server-memory-server-configuration-options.md#recommendations)
