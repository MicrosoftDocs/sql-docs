---
title: "Configure the max worker threads Server Configuration Option"
description: Find out how to use the max worker threads option to configure the number of worker threads that are available to SQL Server to process certain requests.
author: rwestMSFT
ms.author: randolphwest
ms.date: "04/14/2020"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
ms.custom: contperf-fy20q4
helpviewer_keywords:
  - "worker threads [SQL Server]"
  - "max worker threads option"
---
# Configure the max worker threads Server Configuration Option
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This topic describes how to configure the **max worker threads** server configuration option in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. The **max worker threads** option configures the number of worker threads that are available [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-wide to process query requests, login, logout, and similar application requests.


[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the native thread services of the operating systems to ensure the following conditions:

- One or more threads simultaneously support each network that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports.

- One thread handles database checkpoints.

- A pool of threads handles all users.

The default value for **max worker threads** is 0. This enables [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to automatically configure the number of worker threads at startup. The default setting is best for most systems. However, depending on your system configuration, setting **max worker threads** to a specific value sometimes improves performance.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The actual number of query requests can exceed the value set in **max worker threads** in which case  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] pools the worker threads so that the next available worker thread can handle the request. A worker thread is assigned only to active requests and is released once the request is serviced. This happens even if the user session/connection on which the request was made remains open. 

-   The **max worker threads** server configuration option does not limit all threads that may be spawned inside the engine. System threads required for tasks such as LazyWriter, Checkpoint, Log Writer, Service Broker, Lock Manager, or others are spawned outside this limit. Availability Groups use some of the worker threads from within the **max worker thread limit** but also use system threads (see [Thread Usage by Availability Groups](../availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md#ThreadUsage) ) If the number of threads configured is being exceeded, the following query will provide information about the system tasks that have spawned the additional threads.  
  
 ```sql  
 SELECT  s.session_id, r.command, r.status,  
    r.wait_type, r.scheduler_id, w.worker_address,  
    w.is_preemptive, w.state, t.task_state,  
    t.session_id, t.exec_context_id, t.request_id  
 FROM sys.dm_exec_sessions AS s  
 INNER JOIN sys.dm_exec_requests AS r  
    ON s.session_id = r.session_id  
 INNER JOIN sys.dm_os_tasks AS t  
    ON r.task_address = t.task_address  
 INNER JOIN sys.dm_os_workers AS w  
    ON t.worker_address = w.worker_address  
 WHERE s.is_user_process = 0;  
 ```  
  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   This option is an advanced option and should be changed only by an experienced database administrator or certified [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] professional. If you suspect that there is a performance problem, it is probably not the availability of worker threads. The cause is more likely related to activies that occupy the worker threads and do not release them. Examples include long-running queries or bottlenecks on the system (I/O, blocking, latch waits, network waits) that cause long-waiting queries. It is best to find the root cause of a performance issue before you change the max worker threads setting. For more information on assessing performance, see [Monitor and tune for performance](../../relational-databases/performance/monitor-and-tune-for-performance.md).
  
-   Thread pooling helps optimize performance when a large number of clients connect to the server. Usually, a separate operating system thread is created for each query request. However, with hundreds of connections to the server, using one thread per query request can consume large amounts of system resources. The **max worker threads** option enables [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to create a pool of worker threads to service a larger number of query requests, which improves performance.  
  
-   The following table shows the automatically configured number of max worker threads (when value is set to 0) based on various combinations of CPUs, computer architecture, and versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], using the formula: ***Default Max Workers* + ((*logical CPUs* - 4) * *Workers per CPU*)**.  
  
    |Number of logical CPUs|32-bit computer (up to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)])|64-bit computer (up to [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] SP1)|64-bit computer (starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] SP2 and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)])|   
    |------------|------------|------------|------------|  
    |\<= 4|256|512|512|   
    |8|288|576|576|   
    |16|352|704|704|   
    |32|480|960|960|   
    |64|736|1472|1472|   
    |128|1248|2496|4480|   
    |256|2272|4544|8576|   
    
    Up to [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] SP1, the *Workers per CPU* only depend on the architecture (32-bit or 64-bit):
    
    |Number of logical CPUs|32-bit computer <sup>Note 1</sup>|64-bit computer|   
    |------------|------------|------------|   
    |\<= 4|256|512|   
    |\> 4|256 + ((logical CPU's - 4) * 8)|512 <sup>Note 2</sup> + ((logical CPU's - 4) * 16)|   
    
    Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] SP2 and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)], the *Workers per CPU* depend on the architecture and number of processors (between 4 and 64, or greater than 64):
    
    |Number of logical CPUs|32-bit computer <sup>Note 1</sup>|64-bit computer|   
    |------------|------------|------------|   
    |\<= 4|256|512|   
    |\> 4 and \<= 64|256 + ((logical CPU's - 4) * 8)|512 <sup>Note 2</sup> + ((logical CPU's - 4) * 16)|   
    |\> 64|256 + ((logical CPU's - 4) * 32)|512 <sup>Note 2</sup> + ((logical CPU's - 4) * 32)|   
  
    <sup>Note 1</sup> Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can no longer be installed on a 32-bit operating system. 32-bit computer values are listed for the assistance of customers running [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and earlier. We recommend 1,024 as the maximum number of worker threads for an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is running on a 32-bit computer.
    
    <sup>Note 2</sup> Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)], the *Default Max Workers* value is divided by 2 for machines with less than 2GB of memory.
  
    > [!TIP]  
    > For recommendations on using more than 64 CPUs, refer to [Best Practices for Running SQL Server on Computers That Have More Than 64 CPUs](../../relational-databases/thread-and-task-architecture-guide.md#best-practices-for-running-sql-server-on-computers-that-have-more-than-64-cpus).  
  
-   When all worker threads are active with long running queries, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might appear unresponsive until a worker thread completes and becomes available. Although this is not a defect, it can sometimes be undesirable. If a process appears to be unresponsive and no new queries can be processed, then connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using the dedicated administrator connection (DAC), and kill the process. To prevent this, increase the number of max worker threads.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Execute permissions on **sp_configure** with no parameters or with only the first parameter are granted to all users by default. To execute **sp_configure** with both parameters to change a configuration option or to run the `RECONFIGURE` statement, a user must be granted the `ALTER SETTINGS` server-level permission. The `ALTER SETTINGS` permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.  
  
##  <a name="SSMSProcedure"></a> Using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]  
  
#### To configure the max worker threads option  
  
1.  In Object Explorer, right-click a server and select **Properties**.  
  
2.  Click the **Processors** node.  
  
3.  In the **Max worker threads** box, type or select a value from 128 through 65,535.  
  
> [!TIP]
> Use the **max worker threads** option to configure the number of worker threads available to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] processes. The default setting for **max worker threads** is best for most systems. 
> However, depending on your system configuration, setting **max worker threads** to a smaller value sometimes improves performance.
> See [Recommendations](#Recommendations) in this page for more information.
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To configure the max worker threads option  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to configure the `max worker threads` option to `900`.  
  
```sql  
USE AdventureWorks2012 ;  
GO  
EXEC sp_configure 'show advanced options', 1;  
GO  
RECONFIGURE ;  
GO  
EXEC sp_configure 'max worker threads', 900 ;  
GO  
RECONFIGURE;  
GO  
```  
  
##  <a name="FollowUp"></a> Follow Up: After you configure the max worker threads option  
 The change will take effect immediately after executing [RECONFIGURE](../../t-sql/language-elements/reconfigure-transact-sql.md), without requiring the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to restart.  
  
## See Also  
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [Diagnostic Connection for Database Administrators](../../database-engine/configure-windows/diagnostic-connection-for-database-administrators.md)
