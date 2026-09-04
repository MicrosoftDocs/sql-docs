---
title: Replication Programming Concepts
description: Replication programming in SQL Server requires careful planning. Discover how to define application functionality, plan security, and pick a replication API.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "replication [SQL Server], planning"
  - "programming [SQL Server replication], planning"
  - "programming [SQL Server replication]"
monikerRange: "=azuresqldb-mi-current || >=sql-server-2017"
---
# Replication programming concepts
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]

  Before developing an application that uses replication functionalities, follow these general planning steps:  
  
1.  Define your replication topology.  
  
2.  Define application functionality.  
  
3.  Plan for security.  
  
4.  Choose a development environment.  
  
5.  Choose the appropriate replication programming interface.  
  
 The rest of this article describes these steps in more detail. To help illustrate the planning process, an example is included.  
  
## Defining the replication topology  
 The first step in programming replication is to define the replication topology for your application. If you're writing an application that uses an existing replication topology, such as a client application that accesses data at an existing subscriber, move on to the next step.  
  
> [!NOTE]  
>  In some cases, deploying the replication topology is the sole purpose of the application.  
  
 The replication topology that you define depends on many factors, including the following:  
  
-   Whether replicated data needs to be updated, and by whom.  
  
-   Your data distribution needs regarding consistency, autonomy, and latency.  
  
-   The replication environment, including business users, technical infrastructure, network and security, and data characteristics.  
  
-   The types of replication and replication options.  
  
-   The replication topologies and how they align with the types of replication.  
  
 If you're new to [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] replication, see [Types of Replication](../../../relational-databases/replication/types-of-replication.md).  
  
## Defining application functionality  
 After you define the replication topology, decide on the functionalities that your application offers. These functionalities can range from a script that synchronizes a subscription to an application with a user interface to configure replication. Replication supports the following general programming tasks:  
  
-   Setting up replication.  
  
-   Synchronizing Subscribers.  
  
-   Maintaining a replication topology.  
  
-   Monitoring a replication topology.  
  
-   Troubleshooting replication.  
  
 It's also common to extend your application by combining replication functionalities with other functionalities provided by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. The following table highlights some extended functionalities that you might provide in your replication application.  
  
|Functionality|Example|  
|-------------------|-------------|  
|Server administration using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Management Objects (SMO)|An application that enables an administrator to attach and configure a database as a Publisher in a replication topology.|  
|Data access using ADO.NET|An application that enables users to programmatically access and change replicated sales data in a local Subscriber database while offline and then connect and synchronize the pull subscription by selecting a button.|  
  
## Planning for security  
 Security is important in any application, and you should plan for security before writing any code. Application security can be divided into three main parts: securing the database, securing replication, and writing secure code.  
  
 The following articles provide information on security:  
  
-   [View and modify replication security settings](../../../relational-databases/replication/security/view-and-modify-replication-security-settings.md)  
  
-   [Security Center for SQL Server Database Engine and Azure SQL Database](../../../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md)  
  
## Choosing a development environment  
 When you develop a replication application, consider these three basic development environments. Each development environment has access to the same replication functionalities with some exceptions. You can develop replication applications in each of the following environments.  
  
-   **Managed code**  
  
     Object-oriented development environment that leverages the benefits of the [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] programming and the .NET common language runtime (CLR). Managed code is the recommended programming environment for both .NET development and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] applications. Managed replication interfaces enable you to program replication administration in an object-oriented manner without having to know [!INCLUDE[tsql](../../../includes/tsql-md.md)]. It also provides some callback functionalities when running replication agents that aren't available from scripts. Managed code is the best environment for developing reusable components and user interface applications.  
  
-   **Scripting**  
  
     Simple applications that execute a series of commands as either replication system stored procedures in [!INCLUDE[tsql](../../../includes/tsql-md.md)] scripts or commands in batch files. While you can execute scripts in a managed environment by using the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in-process managed provider, you can get the same functionality by using managed replication interfaces, which also provide callback functionalities. Scripting is the best environment for executing tasks that run only a few times and where callback functionalities aren't required, such as installing a replication server.  
  
-   **Native code**  
  
     Object-oriented development environment that utilizes direct access to the system or COM objects such that code isn't managed by the CLR. Native code replication interfaces are deprecated or discontinued. For more information, see [Deprecated Features in SQL Server Replication](../../../relational-databases/replication/deprecated-features-in-sql-server-replication.md) or [Replication Backward Compatibility](../../../relational-databases/replication/replication-backward-compatibility.md).  
  
## Choose the appropriate replication programming interface  
 The final planning step is choosing the appropriate replication programming interface that implements the desired replication functionality for the chosen development environment. The following table shows the available replication programming interfaces.  
  
|Interface|Environment|Uses|  
|---------------|-----------------|----------|  
|[Replication Management Objects Concepts](../../../relational-databases/replication/concepts/replication-management-objects-concepts.md)|Managed code|Administration, monitoring, and synchronization.|  
|<xref:Microsoft.SqlServer.Replication>|Managed code|Synchronization.|  
|<xref:Microsoft.SqlServer.Replication.BusinessLogicSupport>|Managed code|Creation of business logic handlers to integrate custom logic with the merge synchronization process.|  
|[Replication Stored Procedures &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)|Scripting|Administration and monitoring.|  
|[Replication Agent Executables Concepts](../../../relational-databases/replication/concepts/replication-agent-executables-concepts.md)|Scripting|Synchronization.|  
  
## Example  
 At [!INCLUDE[ssSampleDBCoShort](../../../includes/sssampledbcoshort-md.md)], data needs to be published for 200 sales representatives around the world. The sales representatives travel often and need to use laptop computers or personal digital assistants (PDAs) to change customer data and add new orders. The sales representatives need to synchronize the changes with the Publisher when they connect the laptop to the network.  
  
 For this application, the planning steps might look like the following:  
  
1.  The replication topology for this application already exists. However, you must create a new pull subscription at the client. Use parameterized filters in the publication to replicate a unique set of data to each sales representative.  
  
1.  In addition to the typical data access required for a sales application, this application should enable a salesperson to synchronize the pull subscription on demand by clicking a button. Since a sales representative installs and runs the application, it also needs to be able to configure a subscription and apply the initial snapshot at the client. Optionally, the application uses the infrastructure provided by Windows for sensing wireless connectivity to automatically synchronize the subscription when a connection is detected.  
  
1.  Follow all of the security guidelines for replication, including using Windows Authentication and a virtual private network (VPN) when connecting to the Publisher. If you implement Web synchronization, use a Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL), connection. For more information, see [Configure Web Synchronization](../../../relational-databases/replication/configure-web-synchronization.md).  
  
1.  To take advantage of the features of the [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)], develop the application by using a managed code language.  
  
5.  Based on these requirements, the Replication Management Objects (RMO) managed interface can provide all of the needed replication functionality for this application.  
  
 You can download the AdventureWorks sample application that implements this example scenario from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
  
