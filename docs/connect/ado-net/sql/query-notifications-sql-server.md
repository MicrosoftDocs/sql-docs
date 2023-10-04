---
title: "Query notifications in SQL Server"
description: "Describes how .NET applications can request notification from SQL Server when data has changed."
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-kaywon
ms.date: "08/15/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Query notifications in SQL Server

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

Built upon the Service Broker infrastructure, query notifications allow applications to be notified when data has changed. This feature is particularly useful for applications that provide a cache of information from a database, such as a Web application, and need to be notified when the source data is changed.  
  
There are three ways you can implement query notifications using ADO.NET:  
  
- The low-level implementation is provided by the `SqlNotificationRequest` class that exposes server-side functionality, enabling you to execute a command with a notification request.  
  
- The high-level implementation is provided by the `SqlDependency` class, which is a class that provides a high-level abstraction of notification functionality between the source application and SQL Server, enabling you to use a dependency to detect changes in the server. In most cases, this is the simplest and most effective way to leverage SQL Server notifications capability by managed client applications using the Microsoft SqlClient Data Provider for SQL Server.  
  
- In addition, Web applications built using ASP.NET 2.0 or later can use the `SqlCacheDependency` helper classes.  
  
Query notifications are used for applications that need to refresh displays or caches in response to changes in underlying data. Microsoft SQL Server allows .NET applications to send a command to SQL Server and request notification if executing the same command would produce result sets different from those initially retrieved. Notifications generated at the server are sent through queues to be processed later.  
  
You can set up notifications for SELECT and EXECUTE statements. When using an EXECUTE statement, SQL Server registers a notification for the command executed rather than the EXECUTE statement itself. The command must meet the requirements and limitations for a SELECT statement. When a command that registers a notification contains more than one statement, the Database Engine creates a notification for each statement in the batch.  
  
If you are developing an application where you need reliable sub-second notifications when data changes, review the sections **Planning an Efficient Query Notifications Strategy** and **Alternatives to Query Notifications** in the [Planning for Notifications](/previous-versions/sql/sql-server-2008-r2/ms187528(v=sql.105)) topic in SQL Server Books Online. For more information about Query Notifications and SQL Server Service Broker, see the following links to topics in SQL Server Books Online.  
  
**SQL Server documentation**  
  
- [Using Query Notifications](/previous-versions/sql/sql-server-2008-r2/ms175110(v=sql.105))  
  
- [Creating a Query for Notification](/previous-versions/sql/sql-server-2008-r2/ms181122(v=sql.105))  
  
- [Development (Service Broker)](/previous-versions/sql/sql-server-2008-r2/bb522889(v=sql.105))  
  
- [Service Broker Developer InfoCenter](/previous-versions/sql/sql-server-2008-r2/ms166100(v=sql.105))  
  
- [Developer's Guide (Service Broker)](/previous-versions/sql/sql-server-2008-r2/bb522908(v=sql.105))  
  
## In this section  
[Enabling query notifications](enable-query-notifications.md)  
Discusses how to use query notifications, including the requirements for enabling and using them.  
  
[SqlDependency in an ASP.NET application](sqldependency-aspnet-app.md)  
Demonstrates how to use query notifications from an ASP.NET application.  
  
[Detecting changes with SqlDependency](detect-changes-sqldependency.md)  
Demonstrates how to detect when query results will be different from those originally received.  
  
[SqlCommand execution with a SqlNotificationRequest](sqlcommand-execution-sqlnotificationrequest.md)  
Demonstrates configuring a <xref:Microsoft.Data.SqlClient.SqlCommand> object to work with a query notification.  
  
## Reference  
<xref:Microsoft.Data.Sql.SqlNotificationRequest>  
Describes the <xref:Microsoft.Data.Sql.SqlNotificationRequest> class and all of its members.  
  
<xref:Microsoft.Data.SqlClient.SqlDependency>  
Describes the <xref:Microsoft.Data.SqlClient.SqlDependency> class and all of its members.  
  
<xref:System.Web.Caching.SqlCacheDependency>  
Describes the <xref:System.Web.Caching.SqlCacheDependency> class and all of its members.  
  
## Next steps
- [SQL Server and ADO.NET](index.md)