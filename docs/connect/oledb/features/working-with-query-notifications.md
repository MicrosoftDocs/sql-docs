---
title: "Working with Query Notifications | Microsoft Docs"
description: "Working with query notifications in OLE DB Driver for SQL Server"
ms.custom: ""
ms.date: "06/12/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
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
  - "OLE DB Driver for SQL Server, query notifications"
  - "consumer notification for rowset changes [OLE DB Driver for SQL Server]"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Working with Query Notifications
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  Query notifications were introduced in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] and OLE DB Driver for SQL Server. Built upon the Service Broker infrastructure introduced in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], query notifications allow applications to be notified when data has changed. This feature is particularly useful for applications that provide a cache of information from a database, such as a Web application, and need to be notified when the source data is changed.  
  
 Query notifications allow you to request notification within a specified time-out period when the underlying data of a query changes. The request for notification specifies the notification options, which include the service name, message text, and time-out value to the server. Notifications are delivered through a Service Broker queue that applications may poll for available notifications.  
  
 The syntax of the query notifications options string is:  
  
 `service=<service-name>[;(local database=<database> | broker instance=<broker instance>)]`  
  
 For example:  
  
 `service=mySSBService;local database=mydb`  
  
 Notification subscriptions outlive the process that initiates them, as an application may create a notification subscription and then terminate. The subscription remains valid, and the notification will occur if the data changes within the time-out period specified when the subscription was created. A notification is identified by the query executed, the notification options, and the message text, and may be cancelled by setting its time-out value to zero.  
  
 Notifications are sent only once. For continuous notification of data change, a new subscription must be created by re-executing the query after each notification is processed.  
  
 OLE DB Driver for SQL Server applications typically receive notifications by using the [!INCLUDE[tsql](../../../includes/tsql-md.md)] [RECEIVE](../../../t-sql/statements/receive-transact-sql.md) command to read notifications from the queue associated with the service specified in the notification options.  
  
> [!NOTE]  
>  Table names must be qualified in queries for which notification is required, for example, `dbo.myTable`. Table names must be qualified with two part names. Subscription is invalid if three- or four-part names are used.  
  
 The notification infrastructure is built on top of a queuing feature introduced in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]. In general, notifications generated at the server are sent through these queues to be processed later.  
  
 To use query notifications a queue and a service must exist on the server. These can be created using [!INCLUDE[tsql](../../../includes/tsql-md.md)] similar to the following:  
  
```  
CREATE QUEUE myQueue  
CREATE SERVICE myService ON QUEUE myQueue   
  
([https://schemas.microsoft.com/SQL/Notifications/PostQueryNotification])  
```  
  
> [!NOTE]  
>  The service must use the predefined contract as shown above.  
  
## OLE DB Driver for SQL Server  
 The OLE DB Driver for SQL Server supports consumer notification on rowset modification. The consumer receives notification at every phase of rowset modification and on any attempted change.  
  
> [!NOTE]  
>  Passing a notifications query to the server with **ICommand::Execute** is the only valid way to subscribe to query notifications with the OLE DB Driver for SQL Server.  
  
### The DBPROPSET_SQLSERVERROWSET Property Set  
 In order to support query notifications through OLE DB, OLE DB Driver for SQL Server adds the following new properties to the DBPROPSET_SQLSERVERROWSET property set.  
  
|Name|Type|Description|  
|----------|----------|-----------------|  
|SSPROP_QP_NOTIFICATION_TIMEOUT|VT_UI4|The number of seconds that the query notification is to remain active.<br /><br /> The default is 432,000 seconds (5 days). The minimum value is 1 second, and the maximum value is 2^31-1 seconds.|  
|SSPROP_QP_NOTIFICATION_MSGTEXT|VT_BSTR|The message text of the notification. This is user-defined, and has no predefined format.<br /><br /> By default, the string is empty. You can specify a message using 1-2000 characters.|  
|SSPROP_QP_NOTIFICATION_OPTIONS|VT_BSTR|The query notification options. These are specified in a string with *name*=*value* syntax. The user is responsible for creating the service and reading notifications off of the queue.<br /><br /> The default is an empty string.|  
  
 The notification subscription is always committed, regardless of whether the statement ran in a user transaction or in auto commit or whether the transaction in which the statement ran committed or rolled back. The server notification fires upon any of the following invalid notification conditions: change of underlying data or schema, or when the timeout period is reached; whichever is first. Notification registrations are deleted as soon as they are fired. Hence upon receiving notifications, the application must subscribe again in case they want to get further updates.  
  
 Another connection or thread can check the destination queue for notifications. For example:  
  
```  
WAITFOR (RECEIVE * FROM MyQueue);   // Where MyQueue is the queue name.   
```  
  
 Note that SELECT * does not delete the entry from the Queue, however RECEIVE \* FROM does. This stalls a server thread if the queue is empty. If there are queue entries at the time of the call, they are returned immediately; otherwise the call waits until a queue entry is made.  
  
```  
RECEIVE * FROM MyQueue  
```  
  
 This statement immediately returns an empty result set if the queue is empty; otherwise it returns all queue notifications.  
  
 If SSPROP_QP_NOTIFICATION_MSGTEXT and SSPROP_QP_NOTIFICATION_OPTIONS are non-NULL and non-empty, the query notifications TDS header containing the three properties defined above are sent to the server with each execution of the command. If either of them is null (or empty), the header is not sent and DB_E_ERRORSOCCURRED is raised, (or DB_S_ERRORSOCCURRED if the properties are both marked as optional), and the status value is set to DBPROPSTATUS_BADVALUE. The validation occurs on Execute/Prepare. Similarly, DB_S_ERRORSOCCURED is raised when the query notification properties are set for connections to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] versions before [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]. The status value in this case is DBPROPSTATUS_NOTSUPPORTED.  
  
 Initiating a subscription does not guarantee that subsequent messages will be successfully delivered. In addition, no check is made as to the validity of the service name specified.  
  
> [!NOTE]  
>  Preparing statements will never cause the subscription to be initiated; only statement execution will achieve this and query notifications are not impacted by the use of OLE DB core services.  
  
 For more information about the DBPROPSET_SQLSERVERROWSET property set, see [Rowset Properties and Behaviors](../../oledb/ole-db-rowsets/rowset-properties-and-behaviors.md).  
  

  
## See Also  
 [OLE DB Driver for SQL Server Features](../../oledb/features/oledb-driver-for-sql-server-features.md)     
  
  
