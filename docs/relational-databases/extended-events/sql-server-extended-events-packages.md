---
title: "SQL Server Extended Events Packages | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xevents
ms.topic: conceptual
helpviewer_keywords: 
  - "extended events [SQL Server], packages"
  - "xe"
ms.assetid: 6bcb04fc-ca04-48f4-b96a-20b604973447
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQL Server Extended Events Packages
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

  A package is a container for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Extended Events objects. There are three kinds of Extended Events packages, which include the following:  
  
-   package0 - Extended Events system objects. This is the default package.  
  
-   sqlserver - [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] related objects.  
  
-   sqlos - [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Operating System (SQLOS) related objects.  
  
> [!NOTE]  
>  The SecAudit package is used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Audit. None of the objects in the package are available through the Extended Events data definition language (DDL).  
  
 Packages are identified by a name, a GUID, and the binary module that contains the package. For more information, see [sys.dm_xe_packages &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-xe-packages-transact-sql.md).  
  
 A package can contain any or all of the following objects, which are discussed in greater detail later in this topic:  
  
-   Events  
  
-   Targets  
  
-   Actions  
  
-   Types  
  
-   Predicates  
  
-   Maps  
  
 Objects from different packages can be mixed in an event session. For more information, see [SQL Server Extended Events Sessions](../../relational-databases/extended-events/sql-server-extended-events-sessions.md).  
  
## Package Contents  
 The following illustration shows the objects that can exist in packages, which are contained in a module. A module can be an executable or a dynamic link library.  
  
 ![The relationship of a module, packages, and object](../../relational-databases/extended-events/media/xepackagesobjects.gif "The relationship of a module, packages, and object")  
  
### Events  
 Events are monitoring points of interest in the execution path of a program, such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. An event firing carries with it the fact that the point of interest was reached, and state information from the time the event was fired.  
  
 Events can be used solely for tracing purposes or for triggering actions. These actions can either be synchronous or asynchronous.  
  
> [!NOTE]  
>  An event does not have any knowledge of the actions that may be triggered in response to the event firing.  
  
 A set of events in a package cannot change after the package is registered with Extended Events.  
  
 All events have a versioned schema which defines their contents. This schema is composed of event columns with well defined types. An event of a specific type must always provide its data in exactly the same order that is specified in the schema. However, an event target does not have to consume all the data that is provided.  
  
#### Event Categorization  
 Extended Events uses an event categorization model similar to Event Tracing for Windows (ETW). Two event properties are used for categorization, channel and keyword. Using these properties supports the integration of Extended Events with ETW and its tools.  
  
 **Channel**  
  
 A channel identifies the audience for an event. These channels are described in the following table.  
  
|Term|Definition|  
|----------|----------------|  
|Admin|Admin events are primarily targeted to the end users, administrators, and support. The events that are found in the admin channels indicate a problem with a well-defined solution that an administrator can act on. An example of an admin event is when an application fails to connect to a printer. These events are either well-documented or have a message associated with them that tells the reader what to do to rectify the problem.|  
|Operational|Operational events are used for analyzing and diagnosing a problem or occurrence. They can be used to trigger tools or tasks based on the problem or occurrence. An example of an operational event is when a printer is added or removed from a system.|  
|Analytic|Analytic events are published in high volume. They describe program operation and are typically used in performance investigations.|  
|Debug|Debug events are used solely by developers to diagnose a problem for debugging.<br /><br /> Events in the Debug channel return internal implementation-specific state data. The schemas and data that the events return may change or become invalid in future versions of SQL Server. Therefore, events in the Debug channel may change or be removed in future versions of SQL Server without notice.|  
  
 **Keyword**  
  
 A keyword is application specific and enables a finer-grained grouping of related events, which makes it easier for you to specify and retrieve an event that you want to use in a session. You can use the following query to obtain keyword information.  
  
```  
select map_value Keyword from sys.dm_xe_map_values  
where name = 'keyword_map'  
```  
  
> [!NOTE]  
>  Keywords map closely to the current grouping of SQL Trace events.  
  
### Targets  
 Targets are event consumers. Targets process events, either synchronously on the thread that fires the event or asynchronously on a system provided thread. Extended Events provides several targets that you can use as appropriate for directing event output. For more information, see [SQL Server Extended Events Targets](https://msdn.microsoft.com/library/e281684c-40d1-4cf9-a0d4-7ea1ecffa384).  
  
### Actions  
 An action is a programmatic response or series of responses to an event. Actions are bound to an event, and each event may have a unique set of actions.  
  
> [!NOTE]  
>  Actions that are intended for a specific set of events cannot bind to unknown events.  
  
 An action bound to an event is invoked synchronously on the thread that fired the event. There are many types of actions and they have a wide range of capabilities. Actions can:  
  
-   Capture a stack dump and inspect data.  
  
-   Store state information in a local context using variable storage.  
  
-   Aggregate event data.  
  
-   Append data to event data.  
  
 Some typical and well known examples of actions are:  
  
-   Stack dumper  
  
-   Execution plan detection ([!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] only)  
  
-   [!INCLUDE[tsql](../../includes/tsql-md.md)] stack collection ([!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] only)  
  
-   Run time statistics calculation  
  
-   Gather user input on exception  
  
### Predicates  
 Predicates are a set of logical rules that are used to evaluate events when they are processed. This enables the Extended Events user to selectively capture event data based on specific criteria.  
  
 Predicates can store data in a local context that can be used for creating predicates that return true once every *n* minutes or every *n* times that an event fires. This local context storage can also be used to dynamically update the predicate, thereby suppressing future event firing if the events contain similar data.  
  
 Predicates have the ability to retrieve context information, such as the thread ID, as well as event specific data. Predicates are evaluated as full Boolean expressions, and support short circuiting at the first point where the entire expression is found to be false.  
  
> [!NOTE]  
>  Predicates with side effects may not be evaluated if an earlier predicate check fails.  
  
### Types  
 Because data is a collection of bytes strung together, the length and characteristics of the byte collection are required in order to interpret the data. This information is encapsulated in the Type object. The following types are provided for package objects:  
  
-   event  
  
-   action  
  
-   target  
  
-   pred_source  
  
-   pred_compare  
  
-   type  
  
 For more information, see [sys.dm_xe_objects &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-xe-objects-transact-sql.md).  
  
### Maps  
 A map table maps an internal value to a string, which enables a user to know what the value represents. Instead of only being able to obtain a numeric value, a user can get a meaningful description of the internal value. The following query shows how to obtain map values.  
  
```  
select map_key, map_value from sys.dm_xe_map_values  
where name = 'lock_mode'  
```  
  
 The preceding query produces the following output.  
  
 `map_key     map_value`  
  
 `---------------------`  
  
 `0           NL`  
  
 `1           SCH_S`  
  
 `2           SCH_M`  
  
 `3           S`  
  
 `4           U`  
  
 `5           X`  
  
 `6           IS`  
  
 `7           IU`  
  
 `8           IX`  
  
 `9           SIU`  
  
 `10          SIX`  
  
 `11          UIX`  
  
 `12          BU`  
  
 `13          RS_S`  
  
 `14          RS_U`  
  
 `15          RI_NL`  
  
 `16          RI_S`  
  
 `17          RI_U`  
  
 `18          RI_X`  
  
 `19          RX_S`  
  
 `20          RX_U`  
  
 `21          RX_X`  
  
 `21          RX_X`  
  
 Using this table as an example, assume that you have a column named mode, and its value is 5. The table indicates that 5 maps to X, which means the lock type is Exclusive.  
  
## See Also  
 [SQL Server Extended Events Sessions](../../relational-databases/extended-events/sql-server-extended-events-sessions.md)   
 [SQL Server Extended Events Engine](../../relational-databases/extended-events/sql-server-extended-events-engine.md)   
 [SQL Server Extended Events Targets](https://msdn.microsoft.com/library/e281684c-40d1-4cf9-a0d4-7ea1ecffa384)  
  
  
