---
title: Enabling query notifications
description: Learn how to use query notifications, including the requirements for enabling and using them.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-kaywon
ms.date: 04/20/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# Enabling query notifications

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

Applications that consume query notifications have a common set of requirements. Your data source must be correctly configured to support SQL query notifications and the user must have the correct client-side and server-side permissions.

To use query notifications, you must:

- Enable query notifications for your database.
- Ensure that the user ID used to connect to the database has the necessary permissions.
- Use a <xref:Microsoft.Data.SqlClient.SqlCommand> object to execute a valid SELECT statement with an associated notification object—either <xref:Microsoft.Data.SqlClient.SqlDependency> or <xref:Microsoft.Data.Sql.SqlNotificationRequest>.
- Provide code to process the notification if the data being monitored changes.

## Query notifications requirements

Query notifications are supported only for SELECT statements that meet a list of specific requirements. The following table provides links to the Service Broker and Query Notifications documentation in SQL Server Books Online.

### SQL Server documentation

- [Creating a Query for Notification](/previous-versions/sql/sql-server-2008-r2/ms181122(v=sql.105))
- [Security Considerations for Service Broker](/previous-versions/sql/sql-server-2005/ms166059(v=sql.90))
- [Security and Protection (Service Broker)](/previous-versions/sql/sql-server-2008-r2/bb522911(v=sql.105))
- [Security Considerations for Notifications Services](/previous-versions/sql/sql-server-2005/ms172604(v=sql.90))
- [Query Notification Permissions](/previous-versions/sql/sql-server-2008-r2/ms188311(v=sql.105))
- [International Considerations for Service Broker](/previous-versions/sql/sql-server-2005/ms166028(v=sql.90))
- [Solution Design Considerations (Service Broker)](/previous-versions/sql/sql-server-2008-r2/bb522899(v=sql.105))
- [Service Broker Developer InfoCenter](/previous-versions/sql/sql-server-2008-r2/ms166100(v=sql.105))
- [Developer's Guide (Service Broker)](/previous-versions/sql/sql-server-2008-r2/bb522908(v=sql.105))

## Enabling query notifications to run sample code

To enable Service Broker on the **AdventureWorks** database by using SQL Server Management Studio, execute the following Transact-SQL statement:

`ALTER DATABASE AdventureWorks SET ENABLE_BROKER;`

For the query notification samples to run correctly, the following Transact-SQL statements must be executed on the database server.

```sql
CREATE QUEUE ContactChangeMessages;

CREATE SERVICE ContactChangeNotifications
  ON QUEUE ContactChangeMessages
([http://schemas.microsoft.com/SQL/Notifications/PostQueryNotification]);
```

## Query notifications permissions

Users who execute commands requesting notification must have SUBSCRIBE QUERY NOTIFICATIONS database permission on the server.

Client-side code that runs in a partial trust situation requires the <xref:Microsoft.Data.SqlClient.SqlClientPermission>.

The following code creates a <xref:Microsoft.Data.SqlClient.SqlClientPermission> object, setting the <xref:System.Security.Permissions.PermissionState> to <xref:System.Security.Permissions.PermissionState.Unrestricted>. The <xref:System.Security.CodeAccessPermission.Demand%2A> will force a <xref:System.Security.SecurityException> at run time if all callers higher in the call stack haven't been granted the permission.

[!code-csharp[DataWorks SqlClientPermission_Demand#1](~/../sqlclient/doc/samples/SqlClientPermission_Demand.cs#1)]

## Choosing a notification object

The query notifications API provides two objects to process notifications: <xref:Microsoft.Data.SqlClient.SqlDependency> and <xref:Microsoft.Data.Sql.SqlNotificationRequest>.

### Using SqlDependency

To use <xref:Microsoft.Data.SqlClient.SqlDependency>, Service Broker must be enabled for the SQL Server database being used, and users must have permissions to receive notifications. Service Broker objects, such as the notification queue, are predefined.

Also, <xref:Microsoft.Data.SqlClient.SqlDependency> automatically launches a worker thread to process notifications as they're posted to the queue. It also parses the Service Broker message, exposing the information as event argument data. <xref:Microsoft.Data.SqlClient.SqlDependency> must be initialized by calling the `Start` method to establish a dependency to the database. `Start` is a static method that needs to be called only once during application initialization for each database connection required. The `Stop` method should be called at application termination for each dependency connection that was made.

### Using SqlNotificationRequest

In contrast, <xref:Microsoft.Data.Sql.SqlNotificationRequest> requires you to implement the entire listening infrastructure yourself. Also, all the supporting Service Broker objects such as the queue, service, and message types that are supported by the queue must be defined. This manual approach is useful if your application requires special notification messages or notification behaviors, or if your application is part of a larger Service Broker application.

## Next steps

- [Query notifications in SQL Server](query-notifications-sql-server.md)
