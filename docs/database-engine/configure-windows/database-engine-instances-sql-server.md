---
title: "Database Engine Instances (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
ms.assetid: af9ae643-9866-4014-b36f-11ab556a773e
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Database Engine Instances (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  An instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is a copy of the **sqlservr.exe** executable that runs as an operating system service. Each instance manages several system databases and one or more user databases. Each computer can run multiple instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Applications connect to the instance in order to perform work in a database managed by the instance.  
  
## Instances  
 An instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] operates as a service that handles all application requests to work with the data in any of the databases managed by that instance. It is the target of the connection requests (logins) from applications. The connection runs through a network connection if the application and instance are on separate computers. If the application and instance are on the same computer, the SQL Server connection can run as either a network connection or an in-memory connection. When a connection has been completed, an application sends [!INCLUDE[tsql](../../includes/tsql-md.md)] statements across the connection to the instance. The instance resolves the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements into operations against the data and objects in the databases, and if the required permissions have been granted to the login credentials, performs the work. Any data retrieved is returned to the application, along with any messages such as errors.  
  
 You can run multiple instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] on a computer. One instance can be the default instance. The default instance has no name. If a connection request specifies only the name of the computer, the connection is made to the default instance. A named instance is one where you specify an instance name when installing the instance. A connection request must specify both the computer name and instance name in order to connect to the instance. There is no requirement to install a default instance; all of the instances running on a computer can be named instances.  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Describes how to configure the properties of an instance. Configure defaults such as file locations and date formats, or how the instance uses operating system resources, such as memory or threads.|[Configure Database Engine Instances &#40;SQL Server&#41;](../../database-engine/configure-windows/configure-database-engine-instances-sql-server.md)|  
|Describes how to manage the collation for an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Collations define the bit patterns used to represent characters, and associated behaviors such as sorting, and case or accent sensitivity in comparison operations.|[Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md)|  
|Describes how to configure linked server definitions, which allow [!INCLUDE[tsql](../../includes/tsql-md.md)] statements run in an instance to work with data stored in separate OLE DB data sources.|[Linked Servers &#40;Database Engine&#41;](../../relational-databases/linked-servers/linked-servers-database-engine.md)|  
|Describes how to create a logon trigger, which specifies actions to be taken after a logon attempt has been validated, but before it starts working with resources in the instance. Logon triggers support actions such as logging connection activity, or restricting logins based on logic in addition to the credential authentication performed by Windows and SQL Server.|[Logon Triggers](../../relational-databases/triggers/logon-triggers.md)|  
|Describes how to manage the service associated with an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. This includes actions such as starting and stopping the service, or configuring startup options.|[Manage the Database Engine Services](../../database-engine/configure-windows/manage-the-database-engine-services.md)|  
|Describes how to perform server network configuration tasks such as enabling protocols, modifying the port or pipe used by a protocol, configuring encryption, configuring the SQL Server Browser service, exposing or hiding the SQL Server Database Engine on the network, and registering the Server Principal Name.|[Server Network Configuration](../../database-engine/configure-windows/server-network-configuration.md)|  
|Describes how to perform client network configuration tasks such as configuring client protocols and creating or deleting a Server Alias.|[Client Network Configuration](../../database-engine/configure-windows/client-network-configuration.md)|  
|Describes the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] editors that can be used to design, debug, and run scripts such as [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts. Also describes how to code Windows PowerShell scripts to work with SQL Server components.|[Database Engine Scripting](../../relational-databases/scripting/database-engine-scripting.md)|  
|Describes how to use maintenance plans to specify a workflow of common administration tasks for an instance. Workflows include tasks such as backing up databases and updating statistics to improve performance.|[Maintenance Plans](../../relational-databases/maintenance-plans/maintenance-plans.md)|  
|Describes how to use the resource governor to manage resource consumption and workloads by specifying limits to the amount of CPU and memory that application requests can use.|[Resource Governor](../../relational-databases/resource-governor/resource-governor.md)|  
|Describes how database applications can use database mail to send e-mail messages from the [!INCLUDE[ssDE](../../includes/ssde-md.md)].|[Database Mail](../../relational-databases/database-mail/database-mail.md)|  
|Describes how to use extended events to capture performance data can be used to build performance baselines or to diagnose performance problems. Extended events are a light-weight, highly scalable system for gathering performance data.|[Extended Events](../../relational-databases/extended-events/extended-events.md)|  
|Describes how to use SQL Trace to build a customized system for capturing and recording events in the [!INCLUDE[ssDE](../../includes/ssde-md.md)].|[SQL Trace](../../relational-databases/sql-trace/sql-trace.md)|  
|Describes how to use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Profiler to capture traces of application requests coming in to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. These traces can later be replayed for activities such as performance testing or problem diagnosis.|[SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md)|  
|Describes the Change Data Capture (CDC) and Change Tracking features and describes how to use these features to track changes to data in a database.|[Track Data Changes &#40;SQL Server&#41;](../../relational-databases/track-changes/track-data-changes-sql-server.md)|  
|Describes how to use the Log File viewer to find and view [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] errors and messages in various logs, such as the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] job history, the SQL Server logs, and Windows event logs.|[Log File Viewer](../../relational-databases/logs/log-file-viewer.md)|  
|Describes how to use the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor to analyze databases and make recommendations for addressing potential performance problems.|[Database Engine Tuning Advisor](../../relational-databases/performance/database-engine-tuning-advisor.md)|  
|Describes how the production database administrators can make a diagnostic connection to instances when standard connections are not being accepted.|[Diagnostic Connection for Database Administrators](../../database-engine/configure-windows/diagnostic-connection-for-database-administrators.md)|  
|Describes how to use the deprecated remote servers feature to enable access from one instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to another. The preferred mechanism for this functionality is a linked server.|[Remote Servers](../../database-engine/configure-windows/remote-servers.md)|  
|Describes the capabilities of Service Broker for messaging and queueing applications and provides pointers to the Service Broker documentation.|[Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md)|  
|Describes how the buffer pool extension can be used to provide seamless integration of nonvolatile random access storage (solid-state drives) to the Database Engine buffer pool to significantly improve I/O throughput.|[Buffer Pool Extension File](../../database-engine/configure-windows/buffer-pool-extension.md)|  
  
## See Also  
 [sqlservr Application](../../tools/sqlservr-application.md)   
 [Database Features](../../relational-databases/database-features.md)   
 [Database Engine Cross-Instance Features](../../relational-databases/database-engine-cross-instance-features.md)  
  
  
