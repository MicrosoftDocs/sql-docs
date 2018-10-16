---
title: "Histogram Target | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "bucketing target [SQL Server extended events]"
  - "event bucketing target"
  - "targets [SQL Server extended events], bucketing"
ms.assetid: 2ea39141-7eb0-4c74-abf8-114c2c106a19
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Histogram Target
  The histogram target groups occurrences of a specific event type based on event data. The groupings of events are counted based on a specified event column or action. You can use the histogram target to troubleshoot performance issues. By identifying which events are occurring most frequently, you can find "hotspots" that indicate a potential cause of a performance problem.  
  
 The following table describes the options that can be used to configure the histogram target.  
  
|Option|Allowed values|Description|  
|------------|--------------------|-----------------|  
|slots|Any integer value. This value is optional.|A user-specified value indicating the maximum number of groupings to retain. When this value is reached, new events that do not belong to the existing groups are ignored.<br /><br /> Note that to improve performance, the slot number is rounded up to the next power of 2.|  
|filtering_event_name|Any event present in the Extended Events session. This value is optional.|A user-specified value that is used to identify a class of events. Only instances of the specified event are bucketed. All other events are ignored.<br /><br /> If you specify this value, you must use the format: *package_name*.*event_name*, for example `'sqlserver.checkpoint_end'`. You can identify the package name by using the following query:<br /><br /> SELECT p.name, se.event_name<br />FROM sys.dm_xe_session_events se<br />JOIN sys.dm_xe_packages p<br />ON se_event_package_guid = p.guid<br />ORDER BY p.name, se.event_name<br /><br /> <br /><br /> If you do not specify the filtering_event_name value, source_type must be set to 1 (the default).|  
|source_type|The type of object that the bucket is based on. This value is optional and if not specified has a default value of 1.|Can have one of the following values:<br /><br /> 0 for an event<br /><br /> 1 for an action|  
|source|Event column or action name.|The event column or action name that is used as the data source.<br /><br /> When you specify an event column for source, you must specify a column from the event that is used for the filtering_event_name value. You can identify the potential columns by using the following query:<br /><br /> SELECT name FROM sys.dm_xe_object_columns<br />WHERE object_name = '\<eventname>'<br />AND column_type != 'readonly'<br /><br /> When you specify an event column for source, you do not have to include the package name in the source value.<br /><br /> When you specify an action name for source, you must use one of the actions that is configured for collection in the event session for which this target is being used. To find potential values for the action name, you can query the action_name column of the sys.dm_xe_sesssion_event_actions view.<br /><br /> If you are using an action name as the data source, you must specify the source value by using the format: *package_name*.*action_name*.|  
  
 The following example illustrates at a high level how the histogram target collects data. In this example, you want to use the histogram target to count how many waits of each wait type occurred. To do this, you would specify the following options when you define the histogram target:  
  
-   filtering_event_name = 'wait_info'  
  
-   source = 'wait_type'  
  
-   source_type = 0 (because wait_type is an event column)  
  
 In the example scenario, the following data is recorded for the wait_type source.  
  
|Filtering event name|Source column value|  
|--------------------------|-------------------------|  
|wait_info|file_io|  
|wait_info|file_io|  
|wait_info|network|  
|wait_info|network|  
|wait_info|sleep|  
  
 The wait type values would be categorized into three slots, with the following values and slot counts:  
  
|Value|Slot count|  
|-----------|----------------|  
|file_io|2|  
|network|2|  
|sleep|1|  
  
 The histogram target only retains event data for the specified source. In some cases the event data may be too large to retain completely, in which case the data is truncated. When event data is truncated, the number of bytes is recorded and displayed as XML output.  
  
## Adding the Target to a Session  
 To add the histogram target to an Extended Events session, you must include either of the following statements when you create or alter an event session, depending on the desired target type:  
  
```  
ADD TARGET package0.histogram  
```  
  
 You can use the SET statement to set the various options. The following example shows the addition of the histogram target, where data for the sqlserver.checkpoint_end event is collected.  
  
```  
ADD TARGET package0.histogram  
(SET slots = 32, filtering_event_name = 'sqlserver.checkpoint_end', source_type = 0, source = 'database_id')  
```  
  
 For more information, see [Find the Objects That Have the Most Locks Taken on Them](../relational-databases/extended-events/find-the-objects-that-have-the-most-locks-taken-on-them.md), and [Monitor System Activity Using Extended Events](../relational-databases/extended-events/monitor-system-activity-using-extended-events.md).  
  
## Reviewing the Target Output  
 The histogram target serializes data to a calling program or procedure in XML format. The target output does not conform to any schema.  
  
 To review the output from the histogram target, you can use the following query, replacing *session_name* with the name of the event session.  
  
```  
SELECT name, target_name, CAST(xet.target_data AS xml)  
FROM sys.dm_xe_session_targets AS xet  
JOIN sys.dm_xe_sessions AS xe  
   ON (xe.address = xet.event_session_address)  
WHERE xe.name = 'session_name'  
```  
  
 The following example illustrates histogram target output format.  
  
```  
<Slots truncated = "0" buckets=[count]>  
    <Slot count=[count] trunc=[truncated bytes]>  
        <value>  
        </value>  
    </Slot>  
</Slots>  
```  
  
## See Also  
 [SQL Server Extended Events Targets](../../2014/database-engine/sql-server-extended-events-targets.md)   
 [sys.dm_xe_session_targets &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-session-targets-transact-sql)   
 [CREATE EVENT SESSION &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-event-session-transact-sql)   
 [ALTER EVENT SESSION &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-event-session-transact-sql)  
  
  
