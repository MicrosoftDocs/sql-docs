---
title: Workload management in Analytics Platform System | Microsoft Docs
description: Workload management in Analytics Platform System.
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
---

# Workload management in Analytics Platform System

SQL Server PDW's workload management capabilities allow users and administrators to assign requests to pre-set configurations of memory, and concurrency. Use workload management to improve performance of your workload, either consistent or mixed, by allowing requests to have the appropriate resources without starving any requests forever.  
  
For example, with the workload management techniques in SQL Server PDW, you could:  
  
-   Allocate a large number of resources to a load job.  
  
-   Specify more resources for building a columnstore index.  
  
-   Troubleshoot a slow-performing hash join to see if it needs more memory, and then give it more memory.  
  
## <a name="Basics"></a>Workload Management Basics  
  
### Key Terms  
Workload Management  
*Workload Management* is the ability to understand and adjust system resource utilization in order to achieve the best performance for concurrent requests.  
  
Resource Class  
In SQL Server PDW, a *resource class* is a built-in server role that has pre-assigned limits for memory and concurrency. SQL Server PDW allocates resources to requests according to the resource class server role membership of the login that submits the requests.  
  
On the Compute nodes, the implementation of resource classes uses the Resource Governor feature in SQL Server. For more information about Resource Governor, see [Resource Governor](../relational-databases/resource-governor/resource-governor.md) on MSDN.  
  
### Understand Current Resource Utilization  
To understand system resource utilization for the currently running requests, use the SQL Server PDW dynamic management views. For example, you can use DMVs to understand if a slow-running large hash join could benefit by having more memory.  
  
<!-- MISSING LINKS
For examples, see [Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md).  
-->
  
### Adjust Resource Allocations  
To adjust resource utilization, change the resource class membership of the login that is submitting the request. The resource class server roles are named **mediumrc**, **largerc**, and **xlargerc**. They represent medium, large, and extra large resource allocations respectively.  
  
For example, to allocate a large amount of system resources to a request, add the login that is submitting the request to the **largerc** server role. The following ALTER SERVER ROLE statement adds the login Anna to the largerc server role.  
  
```sql  
ALTER SERVER ROLE largerc ADD MEMBER Anna;  
```  
  
## <a name="RC"></a>Resource Class Descriptions  
The following table describes the resource classes and their system resource allocations.  
  
|Resource Class|Request Importance|Maximum Memory Usage*|Concurrency Slots (Maximum = 32)|Description|  
|------------------|----------------------|--------------------------|---------------------------------------|---------------|  
|default|Medium|400 MB|1|By default, each login is allowed a small amount of memory, and concurrency resources for its requests.<br /><br />When a login is added to a resource class, the new class takes precedence. When a login is dropped from all resource classes, the login reverts back to the default resource allocation.|  
|MediumRC|Medium|1200 MB|3|Examples of requests that might need the medium resource class:<br /><br />CTAS operations that have large hash joins.<br /><br />SELECT operations that need more memory to avoid caching to disk.<br /><br />Loading data into clustered columnstore indexes.<br /><br />Building, rebuilding, and reorganizing clustered columnstore indexes for smaller tables that have 10-15 columns.|  
|Largerc|High|2.8 GB|7|Examples of requests that might need the large resource class:<br /><br />Very large CTAS operations that have huge hash joins, or contain large aggregations, such as large ORDER BY or GROUP BY clauses.<br /><br />SELECT operations that require very large amounts of memory for operations such as hash joins, or aggregations such as ORDER BY or GROUP BY clauses<br /><br />Loading data into clustered columnstore indexes.<br /><br />Building, rebuilding, and reorganizing clustered columnstore indexes for smaller tables that have 10-15 columns.|  
|xlargerc|High|8.4 GB|22|The extra large resource class is for requests that could require extra large resource consumption at run time.|  
  
<sup>*</sup>Maximum memory usage is an approximation.  
  
### Request Importance  
The request importance maps to the amount of CPU time that SQL Server, running on the Compute nodes, will give to the requests.  Requests with higher priority receive more CPU time.  
  
### Maximum Memory Usage  
Maximum memory usage is the maximum amount of available memory a request can use within each processing space. For example a mediumrc request can use up to 1200 MB for processing within each  distribution. It is still important to ensure data is not skewed in order to avoid having a few distributions performing most of the work.  
  
### Concurrency Slots  
The goal of allocating 1, 3, 7, and 22 concurrency slots is to allow both large and small processes to run at the same time, without blocking small process when a large process is running.  For example, SQL Server PDW can allocate maximum of 32 concurrency slots to run 1 extra large request (22 slots), 1 large request (7 slots), and 1 medium request  (3 slots) at the same time.  
  
Examples of allocating up to 32 concurrency slots to concurrent requests:  
  
-   28 slots = 4 large  
  
-   30 slots = 10 medium  
  
-   32 slots = 32 default  
  
-   32 slots = 1 extra large + 1 large + 1 medium  
  
-   32 slots = 2 large + 4 medium + 6 default  
  
Suppose 6 large requests are submitted to SQL Server PDW, and then 10 default requests are submitted. SQL Server PDW will process the requests in priority order as follows:  
  
-   Allocate 28 concurrency slots to start processing 4 large requests as memory becomes available, and keep 2 large requests in the queue.  
  
-   Allocate 4 concurrency slots to start processing 4 default requests and keep 6 default requests in the wait queue.  
  
As requests finish and concurrency slots become available, SQL Server PDW will allocate the remaining requests according to available resources and priority. For example, when there are 7 concurrency slots open, waiting large requests will have higher priority for the 7 slots than waiting medium requests.  If 6 slots open, then SQL Server PDW will allocate 6 more default-sized requests. However, memory and concurrency slots must all be available before SQL Server PDW allows a request to run.  
  
Within each resource class, the requests run in first in first out (FIFO) order.  
  
## <a name="GeneralRemarks"></a>General Remarks  
If a login is a member of more than one resource class, the class with the most resources takes precedence.  
  
When a login is added to or dropped from a resource class, the change takes effect immediately for all future requests; current requests that are running or waiting are not affected. The login does not need to disconnect and reconnect in order for the change to occur.  
  
For each login, the resource class settings are applied to individual statements and operations, and not to the session.  
  
Before SQL Server PDW runs a statement, it tries to acquire the concurrency slots needed for the request. If it cannot acquire enough concurrency slots, SQL Server PDW moves the request into a waiting-to-be-executed state. All resources system that were already allocated to the request are returned back to the system.  
  
Most of the SQL statements always need the default resource allocations, and therefore are not controlled by resource classes. For example, CREATE LOGIN only needs a small amount of resources, and is allocated the default resources even if the login calling CREATE LOGIN is a member of a resource class.  For example, if Anna is a member of the largerc resource class and she submits a CREATE LOGIN statement, the CREATE LOGIN statement will run with the default number of resources.  
  
SQL statements and operations governed by resource classes:  
  
-   ALTER INDEX REBUILD  
  
-   ALTER INDEX REORGANIZE  
  
-   ALTER TABLE REBUILD  
  
-   CREATE CLUSTERED INDEX  
  
-   CREATE CLUSTERED COLUMNSTORE INDEX  
  
-   CREATE TABLE AS SELECT  
  
-   CREATE REMOTE TABLE AS SELECT  
  
-   Loading data with **dwloader**.  
  
-   INSERT-SELECT  
  
-   UPDATE  
  
-   DELETE  
  
-   RESTORE DATABASE when restoring into an appliance with more Compute nodes.  
  
-   SELECT, excluding DMV-only queries  
  
## <a name="Limits"></a>Limitations and Restrictions  
The resource classes govern memory and concurrency allocations.  They do not govern input/output operations.  
  
## <a name="Metadata"></a>Metadata  
DMVs that contain information about resource classes and resource class members.  
  
-   [sys.server_role_members](../relational-databases/system-catalog-views/sys-server-role-members-transact-sql.md)  
  
-   [sys.server_principals](../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md)  
  
DMVs that contain information about the state of requests and the resources they require:  
  
-   [sys.dm_pdw_lock_waits](../relational-databases/system-dynamic-management-views/sys-dm-pdw-lock-waits-transact-sql.md)  
  
-   [sys.dm_pdw_resource_waits](../relational-databases/system-dynamic-management-views/sys-dm-pdw-resource-waits-transact-sql.md)  
  
Related system views exposed from the SQL Server DMVs on the Compute nodes. See [SQL Server Dynamic Management Views](../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md) for links to these DMVs on MSDN.  
  
-   sys.dm_pdw_nodes_resource_governor_resource_pools  
  
-   sys.dm_pdw_nodes_resource_governor_workload_groups  
  
-   sys.dm_pdw_nodes_resource_governor_resource_pools  
  
-   sys.dm_pdw_nodws_resource_governor_workload_groups  
  
-   sys.dm_pdw_nodes_exec_sessions  
  
-   sys.dm_pdw_nodes_exec_requests  
  
-   sys.dm_pdw_nodes_exec_query_memory_grants  
  
-   sys.dm_pdw_nodes_exec_query_resource_semaphores  
  
-   sys.dm_pdw_nodes_os_memory_brokers  
  
-   sys.dm_pdw_nodes_os_memory_cache_entries  
  
-   sys.dm_pdw_nodes_exec_cached_plans  
  
## <a name="RelatedTasks"></a>Related Tasks  
[Workload Management Tasks](workload-management-tasks.md)  
  
<!-- MISSING LINKS
See the Workload Management section of [Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md) for the following tasks:  
  
1.  Find the number of active requests for each resource group.  
  
2.  Determine if my requests need more memory  
  
3.  Determine if there are a high number of query optimizations or suboptimal plan generations.  
  
4.  Determine Average CPU time per request in each resource pool to date  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
-->
  
