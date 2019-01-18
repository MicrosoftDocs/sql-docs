---
title: "Using WQL with the WMI Provider for Server Events | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
helpviewer_keywords: 
  - "queries [WMI]"
  - "query language [WMI]"
  - "WMI Query Language [WMI]"
  - "WQL [WMI]"
  - "WMI Provider for Server Events, WQL"
ms.assetid: 58b67426-1e66-4445-8e2c-03182e94c4be
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# Using WQL with the WMI Provider for Server Events
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  Management applications access [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] events using the WMI Provider for Server Events by issuing WMI Query Language (WQL) statements. WQL is a simplified subset of structured query language (SQL), with some WMI-specific extensions. In using WQL, an application retrieves an event type against a specific instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a database, or a database object (the only object currently supported is queue). The WMI Provider for Server Events translates the query into an event notification that is created in the target database for database-scoped or object-scoped event notifications, or in the **master** database for server-scoped event notifications.  
  
 For example, consider the following WQL query:  
  
```  
SELECT * FROM DDL_DATABASE_LEVEL_EVENTS WHERE DatabaseName = 'AdventureWorks'  
```  
  
 From this query, the WMI Provider tries to produce the equivalent of this event notification on the target server:  
  
```  
USE AdventureWorks ;  
GO  
  
CREATE EVENT NOTIFICATION SQLWEP_76CF38C1_18BB_42DD_A7DC_C8820155B0E9  
    ON DATABASE  
    WITH FAN_IN  
    FOR DDL_DATABASE_LEVEL_EVENTS  
    TO SERVICE   
        'SQL/Notifications/ProcessWMIEventProviderNotification/v1.0',  
        'A7E5521A-1CA6-4741-865D-826F804E5135';  
GO  
```  
  
 The argument in the `FROM` clause of the WQL query (`DDL_DATABASE_LEVEL_EVENTS`) can be any valid event upon which an event notification can be created. The arguments in the `SELECT` and `WHERE` clauses can specify any event property associated with an event or its parent event. For a list of valid events and event properties, see [Event Notifications (Database Engine)](https://technet.microsoft.com/library/ms182602.aspx).  
  
 The following WQL syntax is supported explicitly by the WMI Provider for Server Events. Additional WQL syntax may be specified, but it is not specific to this provider and is parsed instead by the WMI host service. For more information about the WMI Query Language, see the WQL documentation on the Microsoft Developer Network (MSDN).  
  
## Syntax  
  
```  
  
SELECT { event_property [ ,...n ] | * }  
FROM event_type   
WHERE where_condition   
```  
  
## Arguments  
 *event_property*  
 Is a property of an event. Examples include **PostTime**, **SPID**, and **LoginName**. Look up each event listed in [WMI Provider for Server Events Classes and Properties](../../relational-databases/wmi-provider-server-events/wmi-provider-for-server-events-classes-and-properties.md) to determine which properties it holds. For example, the DDL_DATABASE_LEVEL_EVENTS event holds the **DatabaseName** and **UserName** properties. It also inherits the **SQLInstance**, **LoginName**, **PostTime**, **SPID**, and **ComputerName** properties from its parent events.  
  
 **,** *...n*  
 Indicates that *event_property* can be queried multiple times, separated by commas.  
  
 \*  
 Specifies that all properties associated with an event are queried.  
  
 *event_type*  
 Is any event against which an event notification can be created. For a list of available events, see [WMI Provider for Server Events Classes and Properties](https://technet.microsoft.com/library/ms186449.aspx). Note that *event type* names correspond to the same *event_type* | *event_group* that can be specified when you manually create an event notification by using CREATE EVENT NOTIFICATION. Examples of *event type* include CREATE_TABLE, LOCK_DEADLOCK, DDL_USER_EVENTS, and TRC_DATABASE.  
  
> [!NOTE]  
>  Certain system stored procedures that perform DDL-like operations can also fire event notifications. Test your event notifications to determine their responses to system stored procedures that are run. For example, the CREATE TYPE statement and **sp_addtype** stored procedure will both fire an event notification that is created on a CREATE_TYPE event. However, the **sp_rename** stored procedure does not fire any event notifications. For more information, see[DDL Events](../../relational-databases/triggers/ddl-events.md).  
  
 *where_condition*  
 Is a WHERE clause query predicate made up of *event_property* names and logical and comparison operators. The *where_condition* determines the scope in which the corresponding event notification is registered in the target database. It can also act as a filter to target a particular schema or object from which to query *event_type.* For more information, see the Remarks section later in this topic.  
  
 Only the `=` operand can be used together with **DatabaseName**, **SchemaName**, and **ObjectName**. Other expressions cannot be used with these event properties.  
  
## Remarks  
 The *where_condition* of the WMI Provider for Server Events syntax determines the following:  
  
-   The scope by which the provider tries to retrieve the specified *event_type*: the server level, database level, or object level (the only object currently supported is queue). Ultimately, this scope determines the type of event notification created in the target database. This process called event notification registration.  
  
-   The database, schema, and object, where appropriate, on which to register.  
  
 The WMI Provider for Server Events uses a bottom-up, first-fit algorithm to produce the narrowest possible scope for the underlying EVENT NOTIFICATION. The algorithm tries to minimize internal activity on the server and network traffic between the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the WMI host process. The provider examines the *event_type* specified in the FROM clause and the conditions in the WHERE clause, and tries to register the underlying EVENT NOTIFICATION with the narrowest possible scope. If the provider cannot register at the narrowest scope, it tries to register at successively higher scopes until a registration finally succeeds. If it reaches the highest scope the server-level) and fails, it returns an error to the consumer.  
  
 For example, if DatabaseName=**'**AdventureWorks**'**is specified in the WHERE clause, the provider tries to register an event notification in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. If the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database exists and the calling client has the required permissions to create an event notification in [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)], the registration is successful. Otherwise, an attempt is made to register the event notification at the server level. The registration succeeds if the WMI client has the required permissions. However, under this scenario, events are not returned to the client until the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database has been created.  
  
 The *where_condition* can also act as a filter to additionally limit the query to a specific database, schema, or object. For example, consider the following WQL query:  
  
```  
SELECT * FROM ALTER_TABLE   
WHERE DatabaseName = 'AdventureWorks' AND SchemaName = 'Sales'   
    AND ObjectType='Table' AND ObjectName = 'SalesOrderDetail'  
```  
  
 Depending on the outcome of the registration process, this WQL query may be registered either at the database or server level. However, even if it is registered at the server level, the provider ultimately filters any `ALTER_TABLE` events that do not apply to the `AdventureWorks.Sales.SalesOrderDetail` table. In other words, the provider returns only the properties of the `ALTER_TABLE` events that occur on that specific table.  
  
 If a compound expression such as `DatabaseName='AW1'` OR `DatabaseName='AW2'` is specified, an attempt is made to register a single event notification at the server scope instead of two separate event notifications. The registration succeeds if the calling client has permissions.  
  
 If `SchemaName='X' AND ObjectType='Y' AND ObjectName='Z'` are all specified in the `WHERE` clause, an attempt is made to register the event notification directly on object `Z` in schema `X`. The registration succeeds if the client has permissions. Note that currently, object-level events are supported only on queues, and only for the QUEUE_ACTIVATION *event_type*.  
  
 Note that not all events can be queried at any particular scope. For example, a WQL query on a trace event such as Lock_Deadlock, or a trace event group such as TRC_LOCKS, can only be registered at the server level. Similarly, the CREATE_ENDPOINT event and the DDL_ENDPOINT_EVENTS event group can also be registered only at the server level. For more information about the appropriate scope for registering events, see [Designing Event Notifications](https://technet.microsoft.com/library/ms175854\(v=sql.105\).aspx). An attempt to register a WQL query whose *event_type* can only be registered at the server level is always made at the server level. Registration succeeds if the WMI client has permissions. Otherwise, an error is returned to the client. In some cases, however, you can still use the WHERE clause as a filter for server-level events based on the properties that correspond to the event. For example, many trace events have a **DatabaseName** property that can be used in the WHERE clause as a filter.  
  
 Server-scoped event notifications are created in the **master** database and can be queried for metadata by using the [sys.server_event_notifications](../../relational-databases/system-catalog-views/sys-server-event-notifications-transact-sql.md) catalog view.  
  
 Database-scoped or object-scoped event notifications are created in the specified database and can be queried for metadata by using the [sys.event_notifications](../../relational-databases/system-catalog-views/sys-event-notifications-transact-sql.md) catalog view. (You must prefix the catalog view with the corresponding database name.)  
  
## Examples  
  
### A. Querying for events at the server scope  
 The following WQL query retrieves all event properties for any `SERVER_MEMORY_CHANGE` trace event that occurs on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
```  
SELECT * FROM SERVER_MEMORY_CHANGE  
```  
  
### B. Querying for events at the database scope  
 The following WQL query retrieves specific event properties for any event that occurs in the `AdventureWorks` database and exists under the `DDL_DATABASE_LEVEL_EVENTS` event group.  
  
```  
SELECT SPID, SQLInstance, DatabaseName FROM DDL_DATABASE_LEVEL_EVENTS   
WHERE DatabaseName = 'AdventureWorks'   
```  
  
### C. Querying for events at the database scope, filtering by schema and object  
 The following query retrieves all event properties for any `ALTER_TABLE` event that occurs on table `AdventureWorks.Sales.SalesOrderDetail`.  
  
```  
SELECT * FROM ALTER_TABLE   
WHERE DatabaseName = 'AdventureWorks' AND SchemaName = 'Sales'   
    AND ObjectType='Table' AND ObjectName = 'SalesOrderDetail'  
```  
  
## See Also  
 [WMI Provider for Server Events Concepts](https://technet.microsoft.com/library/ms180560.aspx)   
 [Event Notifications (Database Engine)](https://technet.microsoft.com/library/ms182602.aspx)  
  
  
