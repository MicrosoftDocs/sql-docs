---
title: "CREATE EVENT SESSION (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/10/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CREATE EVENT SESSION"
  - "SESSION"
  - "EVENT SESSION"
  - "SESSION_TSQL"
  - "EVENT_SESSION_TSQL"
  - "CREATE_EVENT_SESSION_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "event sessions [SQL Server]"
  - "CREATE EVENT SESSION statement"
ms.assetid: 67683027-2b0f-47aa-b223-604731af8b4d
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# CREATE EVENT SESSION (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Creates an Extended Events session that identifies the source of the events, the event session targets, and the event session options.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).  
  
## Syntax  
  
```    
CREATE EVENT SESSION event_session_name  
ON SERVER  
{  
    <event_definition> [ ,...n]  
    [ <event_target_definition> [ ,...n] ]  
    [ WITH ( <event_session_options> [ ,...n] ) ]  
}  
;  
  
<event_definition>::=  
{  
    ADD EVENT [event_module_guid].event_package_name.event_name   
         [ ( {   
                 [ SET { event_customizable_attribute = <value> [ ,...n] } ]  
                 [ ACTION ( { [event_module_guid].event_package_name.action_name [ ,...n] } ) ]  
                 [ WHERE <predicate_expression> ]  
        } ) ]  
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
  
<event_target_definition>::=  
{  
    ADD TARGET [event_module_guid].event_package_name.target_name  
        [ ( SET { target_parameter_name = <value> [ ,...n] } ) ]  
}  
  
<event_session_options>::=  
{  
    [    MAX_MEMORY = size [ KB | MB ] ]  
    [ [,] EVENT_RETENTION_MODE = { ALLOW_SINGLE_EVENT_LOSS | ALLOW_MULTIPLE_EVENT_LOSS | NO_EVENT_LOSS } ]  
    [ [,] MAX_DISPATCH_LATENCY = { seconds SECONDS | INFINITE } ]  
    [ [,] MAX_EVENT_SIZE = size [ KB | MB ] ]  
    [ [,] MEMORY_PARTITION_MODE = { NONE | PER_NODE | PER_CPU } ]  
    [ [,] TRACK_CAUSALITY = { ON | OFF } ]  
    [ [,] STARTUP_STATE = { ON | OFF } ]  
}  
```  
  
## Arguments  
 *event_session_name*  
 Is the user-defined name for the event session. *event_session_name* is alphanumeric, can be up to 128 characters, must be unique within an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and must comply with the rules for [Identifiers](../../relational-databases/databases/database-identifiers.md).  
  
 ADD EVENT [ *event_module_guid* ].*event_package_name*.*event_name*  
 Is the event to associate with the event session, where:  
  
-   *event_module_guid* is the GUID for the module that contains the event.  
  
-   *event_package_name* is the package that contains the action object.  
  
-   *event_name* is the event object.  
  
 Events appear in the sys.dm_xe_objects view as object_type 'event'.  
  
 SET { *event_customizable_attribute*= \<value> [ ,...*n*] }  
 Allows customizable attributes for the event to be set. Customizable attributes appear in the sys.dm_xe_object_columns view as column_type 'customizable ' and object_name = *event_name*.  
  
 ACTION ( { [*event_module_guid*].*event_package_name*.*action_name* [ **,**...*n*] })  
 Is the action to associate with the event session, where:  
  
-   *event_module_guid* is the GUID for the module that contains the event.  
  
-   *event_package_name* is the package that contains the action object.  
  
-   *action_name* is the action object.  
  
 Actions appear in the sys.dm_xe_objects view as object_type 'action'.  
  
 WHERE \<predicate_expression> 
 Specifies the predicate expression used to determine if an event should be processed. If \<predicate_expression> is true, the event is processed further by the actions and targets for the session. If \<predicate_expression> is false, the event is dropped by the session before being processed by the actions and targets for the session. Predicate expressions are limited to 3000 characters, which limits string arguments. 
  
 *event_field_name*  
 Is the name of the event field that identifies the predicate source.  
  
 [*event_module_guid*].*event_package_name*.*predicate_source_name*  
 Is the name of the global predicate source where:  
  
-   *event_module_guid* is the GUID for the module that contains the event.  
  
-   *event_package_name* is the package that contains the predicate object.  
  
-   *predicate_source_name* is defined in the sys.dm_xe_objects view as object_type 'pred_source'.  
  
 [*event_module_guid*].*event_package_name*.*predicate_compare_name*  
 Is the name of the predicate object to associate with the event, where:  
  
-   *event_module_guid* is the GUID for the module that contains the event.  
  
-   *event_package_name* is the package that contains the predicate object.  
  
-   *predicate_compare_name* is a global source defined in the sys.dm_xe_objects view as object_type 'pred_compare'.  
  
 *number*  
 Is any numeric type including **decimal**. Limitations are the lack of available physical memory or a number that is too large to be represented as a 64-bit integer.  
  
 '*string*'  
 Either an ANSI or Unicode string as required by the predicate compare. No implicit string type conversion is performed for the predicate compare functions. Passing the wrong type results in an error.  
  
 ADD TARGET [*event_module_guid*].*event_package_name*.*target_name*  
 Is the target to associate with the event session, where:  
  
-   *event_module_guid* is the GUID for the module that contains the event.  
  
-   *event_package_name* is the package that contains the action object.  
  
-   *target_name* is the target. Targets appear in sys.dm_xe_objects view as object_type 'target'.  
  
 SET { *target_parameter_name*= \<value> [, ...*n*] }  
 Sets a target parameter. Target parameters appear in the sys.dm_xe_object_columns view as column_type 'customizable' and object_name = *target_name*.  
  
> [!IMPORTANT]  
>  If you are using the ring buffer target, we recommend that you set the max_memory target parameter to 2048 kilobytes (KB) to help avoid possible data truncation of the XML output. For more information about when to use the different target types, see [SQL Server Extended Events Targets](https://msdn.microsoft.com/library/e281684c-40d1-4cf9-a0d4-7ea1ecffa384).  
  
 WITH ( \<event_session_options> [ ,...*n*] ) 
 Specifies options to use with the event session.  
  
 MAX_MEMORY =*size* [ KB | **MB** ]  
 Specifies the maximum amount of memory to allocate to the session for event buffering. The default is 4 MB. *size* is a whole number and can be a kilobyte (KB) or a megabyte (MB) value.  
  
 EVENT_RETENTION_MODE = { **ALLOW_SINGLE_EVENT_LOSS** | ALLOW_MULTIPLE_EVENT_LOSS | NO_EVENT_LOSS }  
 Specifies the event retention mode to use for handling event loss.  
  
 **ALLOW_SINGLE_EVENT_LOSS**  
 An event can be lost from the session. A single event is only dropped when all the event buffers are full. Losing a single event when event buffers are full allows for acceptable [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performance characteristics, while minimizing the loss of data in the processed event stream.  
  
 ALLOW_MULTIPLE_EVENT_LOSS  
 Full event buffers containing multiple events can be lost from the session. The number of events lost is dependant upon the memory size allocated to the session, the partitioning of the memory, and the size of the events in the buffer. This option minimizes performance impact on the server when event buffers are quickly filled, but large numbers of events can be lost from the session.  
  
 NO_EVENT_LOSS  
 No event loss is allowed. This option ensures that all events raised will be retained. Using this option forces all tasks that fire events to wait until space is available in an event buffer. This may cause detectable performance issues while the event session is active. User connections may stall while waiting for events to be flushed from the buffer.  
  
 MAX_DISPATCH_LATENCY = { *seconds* SECONDS | **INFINITE** }  
 Specifies the amount of time that events will be buffered in memory before being dispatched to event session targets. By default, this value is set to 30 seconds.  
  
 *seconds* SECONDS  
 The time, in seconds, to wait before starting to flush buffers to targets. *seconds* is a whole number. The minimum latency value is 1 second. However, 0 can be used to specify INFINITE latency.  
  
 **INFINITE**  
 Flush buffers to targets only when the buffers are full, or when the event session closes.  
  
> [!NOTE]  
>  MAX_DISPATCH_LATENCY = 0 SECONDS is equivalent to MAX_DISPATCH_LATENCY = INFINITE.  
  
 MAX_EVENT_SIZE =*size* [ KB | **MB** ]  
 Specifies the maximum allowable size for events. MAX_EVENT_SIZE should only be set to allow single events larger than MAX_MEMORY; setting it to less than MAX_MEMORY will raise an error. *size* is a whole number and can be a kilobyte (KB) or a megabyte (MB) value. If *size* is specified in kilobytes, the minimum allowable size is 64 KB. When MAX_EVENT_SIZE is set, two buffers of *size* are created in addition to MAX_MEMORY. This means that the total memory used for event buffering is MAX_MEMORY + 2 * MAX_EVENT_SIZE.  
  
 MEMORY_PARTITION_MODE = { **NONE** | PER_NODE | PER_CPU }  
 Specifies the location where event buffers are created.  
  
 **NONE**  
 A single set of buffers are created within the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
 PER_NODE  
 A set of buffers are created for each NUMA node.  
  
 PER_CPU  
 A set of buffers are created for each CPU.  
  
 TRACK_CAUSALITY = { ON | **OFF** }  
 Specifies whether or not causality is tracked. If enabled, causality allows related events on different server connections to be correlated together.  
  
 STARTUP_STATE = { ON | **OFF** }  
 Specifies whether or not to start this event session automatically when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] starts.  
  
> [!NOTE]  
> If `STARTUP_STATE = ON`, the event session will only start if [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is stopped and then restarted.  
  
 ON  
 The event session is started at startup.  
  
 **OFF**  
 The event session is not started at startup.  
  
## Remarks  
The order of precedence for the logical operators is `NOT` (highest), followed by `AND`, followed by `OR`.  
  
## Permissions  
Requires the `ALTER ANY EVENT SESSION` permission.  
  
## Examples  
 The following example shows how to create an event session named `test_session`. This example adds two events and uses the Event Tracing for Windows target.  
  
```sql  
IF EXISTS(SELECT * FROM sys.server_event_sessions WHERE name='test_session')  
    DROP EVENT session test_session ON SERVER;  
GO  
CREATE EVENT SESSION test_session  
ON SERVER  
    ADD EVENT sqlos.async_io_requested,  
    ADD EVENT sqlserver.lock_acquired  
    ADD TARGET package0.etw_classic_sync_target   
        (SET default_etw_session_logfile_path = N'C:\demo\traces\sqletw.etl' )  
    WITH (MAX_MEMORY=4MB, MAX_EVENT_SIZE=4MB);  
GO  
```  
  
## See Also  
 [ALTER EVENT SESSION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-event-session-transact-sql.md)   
 [DROP EVENT SESSION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-event-session-transact-sql.md)   
 [sys.server_event_sessions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-event-sessions-transact-sql.md)   
 [sys.dm_xe_objects &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-xe-objects-transact-sql.md)   
 [sys.dm_xe_object_columns &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-xe-object-columns-transact-sql.md)  
  
  

