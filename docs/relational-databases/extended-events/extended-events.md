---
title: "XEvents overview - SQL Server, Azure SQL Database, and Azure SQL Managed Instance"
description: The Extended Events architecture lets you collect data necessary to identify and troubleshoot a performance problem in SQL Server, Azure SQL Database, and Azure SQL Managed Instance. It is configurable and scalable.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 04/27/2022
ms.service: sql
ms.subservice: xevents
ms.topic: overview
ms.custom: intro-overview
helpviewer_keywords:
  - "extended events [SQL Server]"
  - "xe"
  - "XEvents"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Extended Events overview
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The Extended Events architecture enables users to collect as much or as little data as is necessary to troubleshoot or identify a performance problem in SQL Server, Azure SQL Database, and Azure SQL Managed Instance. Extended Events is highly configurable, lightweight, and scales very well. For more information, see [Extended Events Architecture](extended-events.md#extended-events-architecture).

Extended Events replace the deprecated [SQL Trace](../../relational-databases/sql-trace/sql-trace.md) and SQL Server Profiler features.

Give XEvents a try: [Quickstart: Extended Events in SQL Server](../../relational-databases/extended-events/quick-start-extended-events-in-sql-server.md).

> [!NOTE]  
> Azure SQL Database supports only database-scoped sessions. Learn how [Code examples can differ for Azure SQL Database and SQL Managed Instance](#code-examples-can-differ-for-azure-sql-database-and-sql-managed-instance) and more about [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr).

## Benefits of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Extended Events  

Extended Events is a lightweight performance monitoring system that uses minimal performance resources. SQL Server Management Studio provides a graphical user interface for Extended Events to create and modify sessions and display and analyze session data. Here you can find out more about those extensions:

- [Manage Event Sessions in the Object Explorer](../../relational-databases/extended-events/manage-event-sessions-in-the-object-explorer.md)
- [Use the SSMS XEvent Profiler](../../relational-databases/extended-events/use-the-ssms-xe-profiler.md) 

## Extended Events concepts  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Extended Events builds on existing concepts, such as an event or an event consumer, uses concepts from Event Tracing for Windows, and introduces new concepts.  
  
 The following table describes the concepts in Extended Events.  
  
|Topic|Description|  
|-----------|-----------------|  
|[SQL Server Extended Events Packages](../../relational-databases/extended-events/sql-server-extended-events-packages.md)|Describes the Extended Events packages that contain objects. These objects are used to obtain and process data when an Extended Events session is running.|  
|[SQL Server Extended Events Targets](/previous-versions/sql/sql-server-2016/bb630339(v=sql.130))|Describes the event consumers that can receive data during an event session.|  
|[SQL Server Extended Events Engine](../../relational-databases/extended-events/sql-server-extended-events-engine.md)|Describes the engine that implements and manages an Extended Events session.|  
|[SQL Server Extended Events Sessions](../../relational-databases/extended-events/sql-server-extended-events-sessions.md)|Describes the Extended Events session.|  
  
## Extended Events architecture  

Extended Events is our name for a general event-handling system for server systems. The Extended Events infrastructure supports the correlation of data from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and under certain conditions, the correlation of data from the operating system and database applications. In the operating system case, Extended Events output must be directed to [Event Tracing for Windows (ETW)](/windows/win32/etw/event-tracing-portal). ETW can correlate the event data with operating system or application event data.

All applications have execution points that are useful both inside and outside an application. Inside the application, asynchronous processing may be enqueued using information that is collected during the initial execution of a task. Outside the application, execution points provide monitoring utilities with information. The information is about the behavioral and performance characteristics of the monitored application.  

 Extended Events supports using event data outside a process. This data is typically used by users either administering or supporting a product by doing performance monitoring or by user developing applications on a product for debugging purposes. Data is consumed or analyzed using tools such as XEvent Profiler and Performance Monitor, T-SQL or Windows command line tools.
    
 Extended Events has the following key design aspects:  
  
-   The Extended Events engine is event agnostic. The engine can bind any event to any target, because the engine is not constrained by event content. For more information about the Extended Events engine, see [SQL Server Extended Events Engine](../../relational-databases/extended-events/sql-server-extended-events-engine.md).  
  
-   Events are separated from event consumers, which are called *targets* in Extended Events. This means that any target can receive any event. In addition, any event that is raised can be automatically consumed by the target, which can log or provide additional event context. For more information, see [SQL Server Extended Events Targets](/previous-versions/sql/sql-server-2016/bb630339(v=sql.130)).  
  
-   Events are distinct from the action to take when an event occurs. Therefore, any action can be associated with any event.  
  
-   Predicates can dynamically filter when event data should be captured. Dynamic filtering adds to the flexibility of the Extended Events infrastructure. For more information, see [SQL Server Extended Events Packages](../../relational-databases/extended-events/sql-server-extended-events-packages.md).  
  
 Extended Events can synchronously generate event data (and asynchronously process that data) which provides a flexible solution for event handling. In addition, Extended Events provides the following features:  
  
-   A unified approach to handling events across the server system, while enabling users to isolate specific events for troubleshooting purposes.  
  
-   Integration with, and support for existing ETW tools.  
  
-   A fully configurable event handling mechanism that is based on [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
-   The ability to dynamically monitor active processes, while having minimal effect on those processes.  
  
-   A default system health session that runs without any noticeable performance effects. The session collects system data that you can use to help troubleshoot performance issues. For more information, see [Use the system_health Session](../../relational-databases/extended-events/use-the-system-health-session.md).  
  
## Extended Events tasks  

Using [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)] to execute [!INCLUDE[tsql](../../includes/tsql-md.md)] Data Definition Language (DDL) statements, consume dynamic management views and functions, or catalog views, you can create simple or complex [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Extended Events troubleshooting solutions for your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] environment.  
  
|Task Description|Article|  
|----------------------|-----------|  
|Use the **Object Explorer** to manage event sessions.|[Manage Event Sessions in the Object Explorer](../../relational-databases/extended-events/manage-event-sessions-in-the-object-explorer.md)|  
|Describes how to create an Extended Events session.|[Create an Extended Events Session](/previous-versions/sql/sql-server-2016/hh213147(v=sql.130))|  
|Describes how to view and refresh target data.| [Advanced Viewing of Target Data from Extended Events in SQL Server](../../relational-databases/extended-events/advanced-viewing-of-target-data-from-extended-events-in-sql-server.md)|  
|Describes how to use Extended Events tools to create and manage your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Extended Events sessions.|[Extended Events Tools](../../relational-databases/extended-events/extended-events-tools.md)|  
|Describes how to alter an Extended Events session.|[Alter an Extended Events Session](../../relational-databases/extended-events/alter-an-extended-events-session.md)|  
|Describes how to get information about the fields associated with the events.|[Get the Fields for All Events](/previous-versions/sql/sql-server-2016/bb677249(v=sql.130))|  
|Describes how to find out what events are available in the registered packages.|[View the Events for Registered Packages](./selects-and-joins-from-system-views-for-extended-events-in-sql-server.md)|  
|Describes how to determine what Extended Events targets are available in the registered packages.|[View the Extended Events Targets for Registered Packages](/previous-versions/sql/sql-server-2016/bb677247(v=sql.130))|  
|Describes how to view the Extended Events events and actions that are equivalent to each SQL Trace event and its associated columns.|[View the Extended Events Equivalents to SQL Trace Event Classes](../../relational-databases/extended-events/view-the-extended-events-equivalents-to-sql-trace-event-classes.md)|  
|Describes how to find the parameters you can set when you use the ADD TARGET argument in CREATE EVENT SESSION or ALTER EVENT SESSION.|[Get the Configurable Parameters for the ADD TARGET Argument](/previous-versions/sql/sql-server-2016/bb677176(v=sql.130))|  
|Describes how to convert an existing SQL Trace script to an Extended Events session.|[Convert an Existing SQL Trace Script to an Extended Events Session](../../relational-databases/extended-events/convert-an-existing-sql-trace-script-to-an-extended-events-session.md)|  
|Describes how to determine which queries are holding the lock, the plan of the query, and the [!INCLUDE[tsql](../../includes/tsql-md.md)] stack at the time the lock was taken.|[Determine Which Queries Are Holding Locks](../../relational-databases/extended-events/determine-which-queries-are-holding-locks.md)|  
|Describes how to identify the source of locks that are hindering database performance.|[Find the Objects That Have the Most Locks Taken on Them](../../relational-databases/extended-events/find-the-objects-that-have-the-most-locks-taken-on-them.md)|  
|Describes how to use Extended Events with Event Tracing for Windows to monitor system activity.|[Monitor System Activity Using Extended Events](../../relational-databases/extended-events/monitor-system-activity-using-extended-events.md)|  
|Using the Catalog views and the Dynamic management views (DMVs) for Extended Events | [SELECTs and JOINs From System Views for Extended Events in SQL Server](../../relational-databases/extended-events/selects-and-joins-from-system-views-for-extended-events-in-sql-server.md) |


Use the following Transact-SQL (T-SQL) query to list out all possible events and their descriptions:

```sql
SELECT
     obj1.name as [XEvent-name],
     col2.name as [XEvent-column],
     obj1.description as [Descr-name],
     col2.description as [Descr-column]
  FROM
               sys.dm_xe_objects        as obj1
      JOIN sys.dm_xe_object_columns as col2 on col2.object_name = obj1.name
  ORDER BY
    obj1.name,
    col2.name
```


## Code examples can differ for Azure SQL Database and SQL Managed Instance

[!INCLUDE[sql-on-premises-vs-azure-similar-sys-views-include.](../../includes/paragraph-content/sql-on-premises-vs-azure-similar-sys-views-include.md)]

## See also

- [Extended Events Dynamic Management Views](../../relational-databases/system-dynamic-management-views/extended-events-dynamic-management-views.md)  
- [Extended Events Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/extended-events-catalog-views-transact-sql.md)  
- [SQL Mysteries: Causality tracking vs Event Sequence for XEvent Sessions](https://techcommunity.microsoft.com/t5/sql-server-blog/sql-mysteries-causality-tracking-vs-event-sequence-for-xevent/ba-p/3198826)
- [Analyze and prevent deadlocks in Azure SQL Database](/azure/azure-sql/database/analyze-prevent-deadlocks)

## Next steps

- [Quickstart: Extended Events in SQL Server](../../relational-databases/extended-events/quick-start-extended-events-in-sql-server.md)
- [Event File target code for Extended Events in Azure SQL Database](/azure/azure-sql/database/xevent-code-event-file)
- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [XELite: Cross-platform library to read XEvents from XEL files or live SQL streams](https://www.nuget.org/packages/Microsoft.SqlServer.XEvent.XELite/), released May 2019.  
- [Read-SQLXEvent PowerShell cmdlet](https://www.powershellgallery.com/packages/SqlServer.XEvent), released June 2019. 
