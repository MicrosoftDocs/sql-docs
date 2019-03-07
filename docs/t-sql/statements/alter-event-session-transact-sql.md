---
title: "ALTER EVENT SESSION (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/07/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER EVENT SESSION"
  - "ALTER_EVENT_SESSION_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "event sessions [SQL Server]"
  - "extended events [SQL Server], Transact-SQL"
  - "ALTER EVENT SESSION statement"
ms.assetid: da006ac9-f914-4995-a2fb-25b5d971cd90
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# ALTER EVENT SESSION (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Starts or stops an event session or changes an event session configuration.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
ALTER EVENT SESSION event_session_name  
ON SERVER  
{  
    [ [ {  <add_drop_event> [ ,...n] }     
       | { <add_drop_event_target> [ ,...n ] } ]   
    [ WITH ( <event_session_options> [ ,...n ] ) ]  
    ]  
    | [ STATE = { START | STOP } ]  
}  
  
<add_drop_event>::=  
{  
    [ ADD EVENT <event_specifier>   
         [ ( {   
                 [ SET { event_customizable_attribute = <value> [ ,...n ] } ]  
                 [ ACTION ( { [event_module_guid].event_package_name.action_name [ ,...n ] } ) ]  
                 [ WHERE <predicate_expression> ]  
        } ) ]  
   ]   
   | DROP EVENT <event_specifier> }  
  
<event_specifier> ::=  
{  
[event_module_guid].event_package_name.event_name  
}  
  
<predicate_expression> ::=   
{  
    [ NOT ] <predicate_factor> | {( <predicate_expression> ) }   
    [ { AND | OR } [ NOT ] { <predicate_factor> | ( <predicate_expression> ) } ]   
    [ ,...n ]  
}  
  
<predicate_factor>::=   
{  
    <predicate_leaf> | ( <predicate_expression> )  
}  
  
<predicate_leaf>::=  
{  
      <predicate_source_declaration> { = | < > | ! = | > | > = | < | < = } <value>   
    | [event_module_guid].event_package_name.predicate_compare_name ( <predicate_source_declaration>, <value> )   
}  
  
<predicate_source_declaration>::=   
{  
    event_field_name | ( [event_module_guid].event_package_name.predicate_source_name )  
}  
  
<value>::=   
{  
    number | 'string'  
}  
  
<add_drop_event_target>::=  
{  
    ADD TARGET <event_target_specifier>  
        [ ( SET { target_parameter_name = <value> [ ,...n] } ) ]  
    | DROP TARGET <event_target_specifier>  
}  
  
<event_target_specifier>::=  
{  
    [event_module_guid].event_package_name.target_name  
}  
  
<event_session_options>::=  
{  
    [    MAX_MEMORY = size [ KB | MB] ]  
    [ [,] EVENT_RETENTION_MODE = { ALLOW_SINGLE_EVENT_LOSS | ALLOW_MULTIPLE_EVENT_LOSS | NO_EVENT_LOSS } ]  
    [ [,] MAX_DISPATCH_LATENCY = { seconds SECONDS | INFINITE } ]  
    [ [,] MAX_EVENT_SIZE = size [ KB | MB ] ]  
    [ [,] MEMORY_PARTITION_MODE = { NONE | PER_NODE | PER_CPU } ]  
    [ [,] TRACK_CAUSALITY = { ON | OFF } ]  
    [ [,] STARTUP_STATE = { ON | OFF } ]  
}  
```  
  
## Arguments  
  
|||  
|-|-|  
|Term|Definition|  
|*event_session_name*|Is the name of an existing event session.|  
|STATE = START &#124; STOP|Starts or stops the event session. This argument is only valid when ALTER EVENT SESSION is applied to an event session object.|  
|ADD EVENT \<event_specifier>|Associates the event identified by \<event_specifier>with the event session.|
|[*event_module_guid*]*.event_package_name.event_name*|Is the name of an event in an event package, where:<br /><br /> -   *event_module_guid* is the GUID for the module that contains the event.<br />-   *event_package_name* is the package that contains the action object.<br />-   *event_name* is the event object.<br /><br /> Events appear in the sys.dm_xe_objects view as object_type 'event'.|  
|SET { *event_customizable_attribute*= \<value> [ ,...*n*] }|Specifies customizable attributes for the event. Customizable attributes appear in the sys.dm_xe_object_columns view as column_type 'customizable ' and object_name = *event_name*.|  
|ACTION ( { [*event_module_guid*]*.event_package_name.action_name* [ **,**...*n*] } )|Is the action to associate with the event session, where:<br /><br /> -   *event_module_guid* is the GUID for the module that contains the event.<br />-   *event_package_name* is the package that contains the action object.<br />-   *action_name* is the action object.<br /><br /> Actions appear in the sys.dm_xe_objects view as object_type 'action'.|  
|WHERE \<predicate_expression>|Specifies the predicate expression used to determine if an event should be processed. If \<predicate_expression> is true, the event is processed further by the actions and targets for the session. If \<predicate_expression> is false, the event is dropped by the session before being processed by the actions and targets for the session. Predicate expressions are limited to 3000 characters, which limits string arguments.|
|*event_field_name*|Is the name of the event field that identifies the predicate source.|  
|[event_module_guid].event_package_name.predicate_source_name|Is the name of the global predicate source where:<br /><br /> -   *event_module_guid* is the GUID for the module that contains the event.<br />-   *event_package_name* is the package that contains the predicate object.<br />-   *predicate_source_name* is defined in the sys.dm_xe_objects view as object_type 'pred_source'.|  
|[*event_module_guid*].*event_package_name*.*predicate_compare_name*|Is the name of the predicate object to associate with the event, where:<br /><br /> -   *event_module_guid* is the GUID for the module that contains the event.<br />-   *event_package_name* is the package that contains the predicate object.<br />-   *predicate_compare_name* is a global source defined in the sys.dm_xe_objects view as object_type 'pred_compare'.|  
|DROP EVENT \<event_specifier>|Drops the event identified by *\<event_specifier>*. \<event_specifier> must be valid in the event session.|  
|ADD TARGET \<event_target_specifier>|Associates the target identified by \<event_target_specifier>with the event session.|
|[*event_module_guid*].*event_package_name*.*target_name*|Is the name of a target in the event session, where:<br /><br /> -   *event_module_guid* is the GUID for the module that contains the event.<br />-   *event_package_name* is the package that contains the action object.<br />-   *target_name* is the action. Actions appear in sys.dm_xe_objects view as object_type 'target'.|  
|SET { *target_parameter_name*= \<value> [, ...*n*] }|Sets a target parameter. Target parameters appear in the sys.dm_xe_object_columns view as column_type 'customizable' and object_name = *target_name*.<br /><br /> **NOTE!!** If you are using the ring buffer target, we recommend that you set the max_memory target parameter to 2048 kilobytes (KB) to help avoid possible data truncation of the XML output. For more information about when to use the different target types, see [SQL Server Extended Events Targets](https://msdn.microsoft.com/library/e281684c-40d1-4cf9-a0d4-7ea1ecffa384).|  
|DROP TARGET \<event_target_specifier>|Drops the target identified by \<event_target_specifier>. \<event_target_specifier> must be valid in the event session.|  
|EVENT_RETENTION_MODE = { **ALLOW_SINGLE_EVENT_LOSS** &#124; ALLOW_MULTIPLE_EVENT_LOSS &#124; NO_EVENT_LOSS }|Specifies the event retention mode to use for handling event loss.<br /><br /> **ALLOW_SINGLE_EVENT_LOSS**<br /> An event can be lost from the session. A single event is only dropped when all the event buffers are full. Losing a single event when event buffers are full allows for acceptable [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performance characteristics, while minimizing the loss of data in the processed event stream.<br /><br /> ALLOW_MULTIPLE_EVENT_LOSS<br /> Full event buffers containing multiple events can be lost from the session. The number of events lost is dependent upon the memory size allocated to the session, the partitioning of the memory, and the size of the events in the buffer. This option minimizes performance impact on the server when event buffers are quickly filled, but large numbers of events can be lost from the session.<br /><br /> NO_EVENT_LOSS<br /> No event loss is allowed. This option ensures that all events raised will be retained. Using this option forces all tasks that fire events to wait until space is available in an event buffer. This may cause detectable performance issues while the event session is active. User connections may stall while waiting for events to be flushed from the buffer.|  
|MAX_DISPATCH_LATENCY = { *seconds* SECONDS &#124; **INFINITE** }|Specifies the amount of time that events are buffered in memory before being dispatched to event session targets. The minimum latency value is 1 second. However, 0 can be used to specify INFINITE latency. By default, this value is set to 30 seconds.<br /><br /> *seconds* SECONDS<br /> The time, in seconds, to wait before starting to flush buffers to targets. *seconds* is a whole number.<br /><br /> **INFINITE**<br /> Flush buffers to targets only when the buffers are full, or when the event session closes.<br /><br /> **NOTE!!** MAX_DISPATCH_LATENCY = 0 SECONDS is equivalent to MAX_DISPATCH_LATENCY = INFINITE.|  
|MAX_EVENT_SIZE =*size* [ KB &#124; **MB** ]|Specifies the maximum allowable size for events. MAX_EVENT_SIZE should only be set to allow single events larger than MAX_MEMORY; setting it to less than MAX_MEMORY will raise an error. *size* is a whole number and can be a kilobyte (KB) or a megabyte (MB) value. If *size* is specified in kilobytes, the minimum allowable size is 64 KB. When MAX_EVENT_SIZE is set, two buffers of *size* are created in addition to MAX_MEMORY. This means that the total memory used for event buffering is MAX_MEMORY + 2 * MAX_EVENT_SIZE.|  
|MEMORY_PARTITION_MODE = { **NONE** &#124; PER_NODE &#124; PER_CPU }|Specifies the location where event buffers are created.<br /><br /> **NONE**<br /> A single set of buffers is created within the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.<br /><br /> PER NODE - A set of buffers is created for each NUMA node.<br /><br /> PER CPU - A set of buffers is created for each CPU.|  
|TRACK_CAUSALITY = { ON &#124; **OFF** }|Specifies whether or not causality is tracked. If enabled, causality allows related events on different server connections to be correlated together.|  
|STARTUP_STATE = { ON &#124; **OFF** }|Specifies whether or not to start this event session automatically when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] starts.<br /><br /> If STARTUP_STATE=ON the event session will only start if  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is stopped and then restarted.<br /><br /> ON= Event session is started at startup.<br /><br /> **OFF** = Event session is NOT started at startup.|  
  
## Remarks  
 The `ADD` and `DROP` arguments cannot be used in the same statement.  
  
## Permissions  
 Requires the `ALTER ANY EVENT SESSION` permission.  
  
## Examples  
 The following example starts an event session, obtains some live session statistics, and then adds two events to the existing session.  
  
```sql  
-- Start the event session  
ALTER EVENT SESSION test_session ON SERVER  
STATE = start;  
GO  

-- Obtain live session statistics   
SELECT * FROM sys.dm_xe_sessions;  
SELECT * FROM sys.dm_xe_session_events;  
GO  
  
-- Add new events to the session  
ALTER EVENT SESSION test_session ON SERVER  
ADD EVENT sqlserver.database_transaction_begin,  
ADD EVENT sqlserver.database_transaction_end;  
GO  
```  
  
## See Also  
 [CREATE EVENT SESSION &#40;Transact-SQL&#41;](../../t-sql/statements/create-event-session-transact-sql.md)   
 [DROP EVENT SESSION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-event-session-transact-sql.md)   
 [SQL Server Extended Events Targets](https://msdn.microsoft.com/library/e281684c-40d1-4cf9-a0d4-7ea1ecffa384)   
 [sys.server_event_sessions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-event-sessions-transact-sql.md)   
 [sys.dm_xe_objects &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-xe-objects-transact-sql.md)   
 [sys.dm_xe_object_columns &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-xe-object-columns-transact-sql.md)  
  
  
