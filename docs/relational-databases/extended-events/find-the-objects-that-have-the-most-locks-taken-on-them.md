---
title: "Find objects with the most locks using Extended Events"
description: This article shows how to find objects that have the most locks. Database administrators may need to find most locked objects to improve database performance.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "10/18/2019"
ms.service: sql
ms.subservice: xevents
ms.topic: tutorial
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "objects [SQL Server], extended events"
  - "xe"
  - "extended events [SQL Server], locks"
  - "objects [SQL Server], locks"
ms.assetid: fcbadbda-c91c-43f0-a1b5-601e40110e07
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Find the Objects That Have the Most Locks Taken on Them

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]

Database administrators often need to identify the source of locks that are hindering database performance.  
  
For example, you are monitoring your production server for any possible bottlenecks. You suspect that there might be highly contested resources, and would like to know how many locks are taken on those objects. Once the most frequently locked objects are identified, steps can be taken to optimize access to the contended objects.  
  
To do this, use Query Editor in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
### To find the objects that have the most locks  
  
1. In Query Editor, issue the following statements.

    ```sql
    -- Find objects in a particular database that have the most
    -- lock acquired. This sample uses AdventureWorksDW2012.
    -- Create the session and add an event and target.
    
    IF EXISTS(SELECT * FROM sys.server_event_sessions WHERE name='LockCounts')
        DROP EVENT session LockCounts ON SERVER;
    GO
    DECLARE @dbid int;
  
    SELECT @dbid = db_id('AdventureWorksDW2012');
  
    DECLARE @sql nvarchar(1024);
    SET @sql = '
        CREATE event session LockCounts ON SERVER
            ADD EVENT sqlserver.lock_acquired (WHERE database_id ='
                + CAST(@dbid AS nvarchar) +')
            ADD TARGET package0.histogram(
                SET filtering_event_name=''sqlserver.lock_acquired'',
                    source_type=0, source=''resource_0'')';
  
    EXEC (@sql);
    GO
    ALTER EVENT session LockCounts ON SERVER
        STATE=start;
    GO
    -- Create a simple workload that takes locks.
    
    USE AdventureWorksDW2012;
    GO
    SELECT TOP 1 * FROM dbo.vAssocSeqLineItems;
    GO
    -- The histogram target output is available from the
    -- sys.dm_xe_session_targets dynamic management view in
    -- XML format.
    -- The following query joins the bucketizing target output with
    -- sys.objects to obtain the object names.
    
    SELECT name, object_id, lock_count
        FROM
        (
        SELECT objstats.value('.','bigint') AS lobject_id,
            objstats.value('@count', 'bigint') AS lock_count
            FROM (
                SELECT CAST(xest.target_data AS XML)
                    LockData
                FROM     sys.dm_xe_session_targets xest
                    JOIN sys.dm_xe_sessions        xes  ON xes.address = xest.event_session_address
                    JOIN sys.server_event_sessions ses  ON xes.name    = ses.name
                WHERE xest.target_name = 'histogram' AND xes.name = 'LockCounts'
                 ) Locks
            CROSS APPLY LockData.nodes('//HistogramTarget/Slot') AS T(objstats)
        ) LockedObjects
        INNER JOIN sys.objects o  ON LockedObjects.lobject_id = o.object_id
        WHERE o.type != 'S' AND o.type = 'U'
        ORDER BY lock_count desc;
    GO
    
    -- Stop the event session.
    
    ALTER EVENT SESSION LockCounts ON SERVER
        state=stop;
    GO
    ```

> [!NOTE]
> The preceding Transact-SQL code example runs on SQL Server on-premises, but might _not quite run on Azure SQL Database._ The core portions of the example directly involving Events, such as `ADD EVENT sqlserver.lock_acquired` do work on Azure SQL Database too. But preliminary items, such as `sys.server_event_sessions` must be edited to their Azure SQL Database counterparts like `sys.database_event_sessions` for the example to run.
> For more information about these minor differences between SQL Server on-premises versus Azure SQL Database, see the following articles:
> - [Extended events in Azure SQL Database](/azure/sql-database/sql-database-xevent-db-diff-from-svr#transact-sql-differences)
> - [System objects that support Extended Events](xevents-references-system-objects.md)

After the statements in the preceding Transact-SQL script finish, the **Results** tab of Query Editor displays the following columns:
  
- name
- object_id
- lock_count
  
## See Also

[CREATE EVENT SESSION &#40;Transact-SQL&#41;](../../t-sql/statements/create-event-session-transact-sql.md)  
[ALTER EVENT SESSION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-event-session-transact-sql.md)  
[sys.dm_xe_session_targets &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-xe-session-targets-transact-sql.md)  
[sys.dm_xe_sessions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-xe-sessions-transact-sql.md)  
[sys.server_event_sessions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-event-sessions-transact-sql.md)  
