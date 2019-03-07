---
title: "Create an Extended Events Session Using Query Editor | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "create extended events session"
  - "extended events [SQL Server], create session"
ms.assetid: cba0e02b-b201-4863-bf1b-9164e68e5fa8
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Create an Extended Events Session Using Query Editor
  You can create an Extended Events session by using the Query Editor, or you can create a session in Object Explorer. In Object Explorer, Extended Events provides two user interfaces you can use to create, modify, and view event session data - a wizard that guides you through the event session creation process, and a New Session UI that provides more advanced configuration options. You can create Extended Events sessions to diagnose SQL Server tracing, which enables you to resolve issues such as the following:  
  
-   Find your most expensive queries  
  
-   Find root causes of latch contention  
  
-   Find a query that is blocking other queries  
  
-   Troubleshoot excessive CPU usage caused by query recompilation  
  
-   Troubleshoot deadlocks  
  
 For information about how to create an Extended Events session using the New Session Wizard, see [Create an Extended Events Session Using the Wizard &#40;Object Explorer&#41;](../ssms/object/object-explorer.md). For information about how to create an Extended Events session using the New Session UI, see [Create an Extended Events Session Using the New Session Dialog](../../2014/database-engine/create-an-extended-events-session-using-the-new-session-dialog.md).  
  
##  <a name="BeforeYouBegin"></a> Permissions  
 To create an Extended Events session, you must have the ALTER ANY EVENT SESSION permission.  
  
## Creating an Extended Events session using Query Editor  
  
#### To create an Extended Events session  
  
1.  The following procedure shows how to create an Extended Events session by using Query Editor in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
     Determine which events that you want to use in the session. To see all the events that are available, together with the keyword and channel, use the following query:  
  
    > [!NOTE]  
    >  For information about keywords and channels, see [SQL Server Extended Events Packages](../relational-databases/extended-events/sql-server-extended-events-packages.md).  
  
    ```  
    SELECT p.name, c.event, k.keyword, c.channel, c.description FROM  
       (  
       SELECT event_package = o.package_guid, o.description,   
       event=c.object_name, channel = v.map_value  
       FROM sys.dm_xe_objects o  
       LEFT JOIN sys.dm_xe_object_columns c ON o.name = c.object_name  
       INNER JOIN sys.dm_xe_map_values v ON c.type_name = v.name   
       AND c.column_value = cast(v.map_key AS nvarchar)  
       WHERE object_type = 'event' AND (c.name = 'CHANNEL' or c.name IS NULL)  
       ) c LEFT JOIN   
       (  
       SELECT event_package = c.object_package_guid, event = c.object_name,   
       keyword = v.map_value  
       FROM sys.dm_xe_object_columns c INNER JOIN sys.dm_xe_map_values v   
       ON c.type_name = v.name AND c.column_value = v.map_key   
       AND c.type_package_guid = v.object_package_guid  
       INNER JOIN sys.dm_xe_objects o ON o.name = c.object_name   
       AND o.package_guid = c.object_package_guid  
       WHERE object_type = 'event' AND c.name = 'KEYWORD'   
       ) k  
       ON  
       k.event_package = c.event_package AND (k.event=c.event or k.event IS NULL)  
       INNER JOIN sys.dm_xe_packages p ON p.guid = c.event_package  
    ORDER BY keyword desc, channel, event  
    ```  
  
2.  In a new query window, add the following statements to create an event session, replacing *session_name* with the session name that you want to use:  
  
    > [!IMPORTANT]  
    >  Steps 2 through 6 of this procedure describe each section of the event session definition. You would add all the statements to a single query window before executing. For a full example, see the Example section of this topic.  
  
    ```  
    CREATE EVENT SESSION session_name   
    ON SERVER  
    ```  
  
3.  Add the events that you want to monitor, in the format *package_name*.*event_name*. For each event, add a line similar to the following:  
  
    ```  
    ADD EVENT package_name.event_name  
    ```  
  
     For example:  
  
    ```  
    ADD EVENT sqlserver.file_read_completed,  
    ADD EVENT sqlserver.file_write_completed  
    ```  
  
4.  (Optional) After you add an event, you can add actions to take. You can also add predicates. Predicates are used to establish criteria for when the event information should be consumed by the target. Actions are added by using an ACTION clause, and predicates are added by using a WHERE clause. For example, to add an action and predicate where the [!INCLUDE[tsql](../includes/tsql-md.md)] text is captured for the sqlserver.file_read_completed event, where the file ID equals 1, you would include the following statement:  
  
    ```  
    ADD EVENT sqlserver.file_read_completed  
       (ACTION (sqlserver.sql_text)  
       WHERE file_id = 1),  
    ```  
  
    -   To view which actions are available, use the following query:  
  
        ```  
        SELECT p.name AS 'package_name', xo.name AS 'action_name', xo.description, xo.object_type  
        FROM sys.dm_xe_objects AS xo  
        JOIN sys.dm_xe_packages AS p  
           ON xo.package_guid = p.guid  
        WHERE xo.object_type = 'action'  
        AND (xo.capabilities & 1 = 0   
        OR xo.capabilities IS NULL)  
        ORDER BY p.name, xo.name  
        ```  
  
    -   To view which predicates are available for an event, use the following query, replacing *event_name* with the name of the event for which you want to add a predicate:  
  
        ```  
        SELECT *  
        FROM sys.dm_xe_object_columns  
        WHERE object_name = 'event_name'  
        AND column_type = 'data'  
        ```  
  
         For example:  
  
        ```  
        SELECT *   
        FROM sys.dm_xe_object_columns   
        WHERE object_name = 'file_read_completed'  
        AND column_type = 'data'  
        ```  
  
         Be aware that you can also add global predicate sources. A global predicate source can be used in any predicate expression. To view which global predicate sources are available, use the following query:  
  
        ```  
        SELECT p.name AS package_name, xo.name AS predicate_name  
           , xo.description, xo.object_type  
        FROM sys.dm_xe_objects AS xo  
        JOIN sys.dm_xe_packages AS p  
           ON xo.package_guid = p.guid  
        WHERE xo.object_type = 'pred_source'  
        ORDER BY p.name, xo.name  
        ```  
  
         For example, you could use the following predicate expression to specify that data should only be collected for an event the first five times that an event occurs.  
  
        ```  
        WHERE package0.counter <= 5  
        ```  
  
5.  Add the desired target, where the event data will be processed and consumed. Use the following format:  
  
    ```  
    ADD TARGET package_name.target_name  
    ```  
  
     The following example adds the asynchronous file target:  
  
    ```  
    ADD TARGET package0.asynchronous_file_target  
       (SET filename = 'c:\temp\xelog.xel', metadatafile = 'c:\temp\xelog.xem')  
    ```  
  
     To view the list of available targets, use the following query:  
  
    ```  
    SELECT p.name AS 'package_name', xo.name AS 'target_name'  
       , xo.description, xo.object_type   
    FROM sys.dm_xe_objects AS xo  
    JOIN sys.dm_xe_packages AS p  
       ON xo.package_guid = p.guid  
    WHERE xo.object_type = 'target'  
    AND (xo.capabilities & 1 = 0  
    OR xo.capabilities IS NULL)  
    ORDER BY p.name, xo.name  
    ```  
  
    > [!NOTE]  
    >  For information about the different target types, see [SQL Server Extended Events Targets](../../2014/database-engine/sql-server-extended-events-targets.md).  
  
6.  Review and add any additional configuration options. For example, you can configure options such as the event retention mode, how long events are buffered in memory, or whether the event session should start automatically when [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] starts. The options are described in the topic [ALTER EVENT SESSION &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-event-session-transact-sql). Be aware that default values are assigned if these options are not specified.  
  
7.  Start the session.  
  
    > [!NOTE]  
    >  For more information about how to view the session results, see the corresponding topic for the target type that you used in the [SQL Server Extended Events Targets](../../2014/database-engine/sql-server-extended-events-targets.md) node of Books Online.  
  
 The following example creates an Extended Events session named IOActivity that captures the following information:  
  
-   Event data for completed file reads, including the associated [!INCLUDE[tsql](../includes/tsql-md.md)] text for file reads where the file ID is equal to 1.  
  
-   Event data for completed file writes.  
  
-   Event data for when data is written from the log cache to the physical log file.  
  
 The session sends the output to a file target.  
  
```  
CREATE EVENT SESSION IOActivity  
ON SERVER  
  
ADD EVENT sqlserver.file_read_completed  
   (  
   ACTION (sqlserver.sql_text)  
   WHERE file_id = 1),  
ADD EVENT sqlserver.file_write_completed,  
ADD EVENT sqlserver.databases_log_flush  
  
ADD TARGET package0.asynchronous_file_target   
   (SET filename = 'c:\temp\xelog.xel', metadatafile = 'c:\temp\xelog.xem')  
```  
  
## See Also  
 [CREATE EVENT SESSION &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-event-session-transact-sql)   
 [SQL Server Extended Events Targets](../../2014/database-engine/sql-server-extended-events-targets.md)   
 [SQL Server Extended Events Packages](../relational-databases/extended-events/sql-server-extended-events-packages.md)  
  
  
