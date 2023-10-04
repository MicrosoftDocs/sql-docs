---
title: Working with query notifications
description: Learn how query notifications allow applications to be notified when data has changed in the OLE DB Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: 04/20/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "data access [OLE DB Driver for SQL Server], query notifications"
  - "rowsets [SQL Server], notifications"
  - "OLE DB Driver for SQL Server, query notifications"
  - "notifications [OLE DB Driver for SQL Server]"
  - "query notifications [SQL Server], OLE DB Driver for SQL Server"
  - "canceling rowset changes"
  - "IRowsetNotify interface"
  - "MSOLEDBSQL, query notifications"
  - "consumer notification for rowset changes [OLE DB Driver for SQL Server]"
---
# Working with query notifications

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

Query notifications were introduced in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] and OLE DB Driver for SQL Server. Built on the SQL Service Broker infrastructure introduced in SQL Server 2005 (9.x), query notifications allow applications to be notified when data has changed. This feature is useful for applications that provide a cache of information from a database, such as a web application, and need to be notified when the source data is changed.

By using query notifications, you can request notifications within a specified timeout period when the underlying data of a query changes. The request specifies the notification options, which include the service name, message text, and timeout value to the server. Notifications are delivered through a Service Broker queue that applications can poll for available notifications.

The syntax of the query notifications options string is:

`service=<service-name>[;(local database=<database> | broker instance=<broker instance>)]`

 For example:

`service=mySSBService;local database=mydb`

Notification subscriptions outlive the process that starts them. That's because an application can create a notification subscription and then end. The subscription stays valid, and the notification occurs if the data changes within the specified timeout period. A notification is identified by the query executed, the notification options, and the message text. You can cancel it by setting its timeout value to zero.

Notifications are sent only once. To be continually notified of data changes, create a new subscription by re-executing the query after each notification is processed.

OLE DB Driver for SQL Server applications typically receives notifications by using the [!INCLUDE[tsql](../../../includes/tsql-md.md)] [RECEIVE](../../../t-sql/statements/receive-transact-sql.md) command. It uses this command to read notifications from the queue that is associated with the service specified in the notification options.

> [!NOTE]
> Table names must be qualified in queries for which notification is required. For example, `dbo.myTable`. Table names must be qualified with two-part names. Subscription is invalid if three- or four-part names are used.

The notification infrastructure is built on top of a queuing feature introduced in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]. In general, notifications generated at the server are sent through these queues, to be processed later.

To use query notifications, a queue and a service must exist on the server. These items can be created by using the [!INCLUDE[tsql](../../../includes/tsql-md.md)] command, similar to the following ones:

```sql
CREATE QUEUE myQueue
CREATE SERVICE myService ON QUEUE myQueue
([https://schemas.microsoft.com/SQL/Notifications/PostQueryNotification])
```

> [!NOTE]
> The service must use the predefined contract, as shown above.

## OLE DB Driver for SQL Server

The OLE DB Driver for SQL Server supports consumer notifications upon rowset modification. The consumer receives a notification at every phase of rowset modification and on any attempted change.

> [!NOTE]
> Passing a notifications query to the server with **ICommand::Execute** is the only valid way to subscribe to query notifications with the OLE DB Driver for SQL Server.

### DBPROPSET_SQLSERVERROWSET property set

To support query notifications through OLE DB, the OLE DB Driver for SQL Server adds the following new properties to the `DBPROPSET_SQLSERVERROWSET` property set.

| Name | Type | Description |
|--|--|--|
| SSPROP_QP_NOTIFICATION_TIMEOUT | VT_UI4 | The number of seconds that the query notification is to remain active.<br /><br /> The default is 432,000 seconds (5 days). The minimum value is 1 second, and the maximum value is 2^31-1 seconds. |
| SSPROP_QP_NOTIFICATION_MSGTEXT | VT_BSTR | The message text of the notification. This text is user-defined and has no predefined format.<br /><br /> By default, the string is empty. Specify a message by using 1 to 2000 characters. |
| SSPROP_QP_NOTIFICATION_OPTIONS | VT_BSTR | The query notification options. These options are specified in a string with *name*=*value* syntax. The user is responsible for creating the service and reading notifications off of the queue.<br /><br /> The default is an empty string. |

The notification subscription is always committed. It happens regardless of whether the statement ran in a user transaction or in autocommit or whether the transaction in which the statement ran committed or rolled back. The server notification fires upon any of the following invalid notification conditions: change of underlying data or schema, or when the timeout period is reached; whichever is first.

Notification registrations are deleted as soon as they're fired. So, upon receiving notifications, the application must subscribe again if you want to get further updates.

Another connection or thread can check the destination queue for notifications. For example:

```sql
WAITFOR (RECEIVE * FROM MyQueue); -- Where MyQueue is the queue name.
```

> [!NOTE]
> `SELECT *` doesn't delete the entry from the queue. However, `RECEIVE * FROM` does. This stalls a server thread if the queue is empty. If there are queue entries at the time of the call, they're returned immediately. Otherwise, the call waits until a queue entry is made.

```sql
RECEIVE * FROM MyQueue
```

This statement immediately returns an empty result set if the queue is empty. Otherwise, it returns all queue notifications.

If `SSPROP_QP_NOTIFICATION_MSGTEXT` and `SSPROP_QP_NOTIFICATION_OPTIONS` are non-null and non-empty, the query notifications TDS header that contains the three properties defined above are sent to the server. This header is sent with each execution of the command. If either of them is null (or empty), the header isn't sent and `DB_E_ERRORSOCCURRED` is raised (or `DB_S_ERRORSOCCURRED` is raised, if the properties are both marked as optional). The status value is then set to `DBPROPSTATUS_BADVALUE`. The validation occurs upon execute and prepare. Similarly, `DB_S_ERRORSOCCURED` is raised when the query notification properties are set for connections to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] versions before [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]. The status value in this case is `DBPROPSTATUS_NOTSUPPORTED`.

Starting a subscription doesn't guarantee that future messages will be successfully delivered. Also, no check is made as to the validity of the service name specified.

> [!NOTE]
> Preparing statements will never cause the subscription to be initiated. Only statement execution will achieve initiation. Query notifications aren't affect by the use of OLE DB core services.

For more information about the `DBPROPSET_SQLSERVERROWSET` property set, see [Rowset Properties and Behaviors](../ole-db-rowsets/rowset-properties-and-behaviors.md).

## See also

[OLE DB Driver for SQL Server Features](oledb-driver-for-sql-server-features.md)
