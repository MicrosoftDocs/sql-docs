---
description: "Replication Programming Concepts"
title: "Replication Programming Concepts | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
helpviewer_keywords: 
  - "replication [SQL Server], planning"
  - "programming [SQL Server replication], planning"
  - "programming [SQL Server replication]"
ms.assetid: 2cd846e7-5bf3-4144-8772-703c4f439a2a
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Replication Programming Concepts
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]

  Before developing an application that uses replication functionalities, you should follow the following general planning steps:  
  
1.  Define your replication topology.  
  
2.  Define application functionality.  
  
3.  Plan for security.  
  
4.  Choose a development environment.  
  
5.  Choose the appropriate replication programming interface.  
  
 The rest of this topic describes these steps in more detail. To help illustrate the planning process, an example has been included.  
  
## Defining the Replication Topology  
 The first step in programming replication is to define the replication topology for your application. If you are writing an application that will use an existing replication topology, such as a client application that accesses data at an existing subscriber, you should move onto the next step.  
  
> [!NOTE]  
>  In some cases, deploying the replication topology will be the sole purpose of the application.  
  
 The replication topology that you define depends on many factors, including the following:  
  
-   Whether or not replicated data needs to be updated, and by whom.  
  
-   Your data distribution needs regarding consistency, autonomy, and latency.  
  
-   The replication environment, including business users, technical infrastructure, network and security, and data characteristics.  
  
-   The types of replication and replication options.  
  
-   The replication topologies and how they align with the types of replication.  
  
 If you are new to [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] replication, see [Types of Replication](../../../relational-databases/replication/types-of-replication.md).  
  
## Defining Application Functionality  
 Once the replication topology has been defined, you should decide on the functionalities that your application will offer. These functionalities can range from a script that synchronizes a subscription to an application with a user interface to configure replication. Replication supports the following general programming tasks:  
  
-   Setting up replication.  
  
-   Synchronizing Subscribers.  
  
-   Maintaining a replication topology.  
  
-   Monitoring a replication topology.  
  
-   Troubleshooting replication.  
  
 It is also common extend your application by combining replication functionalities with other functionalities provided by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. The following table highlights some extended functionalities that you might provide in your replication application.  
  
|Functionality|Example|  
|-------------------|-------------|  
|Server administration using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Management Objects (SMO)|An application that enables an administrator to attach and configure a database as a Publisher in a replication topology.|  
|Data access using ADO.NET|An application that enables users to programmatically access and change replicated sales data in a local Subscriber database while offline and then connect and synchronize the pull subscription by clicking a button.|  
  
## Planning for Security  
 Security is important in any application, and planning for security should be completed before writing any code. Application security can be divided into three main parts: securing the database, securing replication, and writing secure code.  
  
 The following topics provide information on security:  
  
-   [View and modify replication security settings](../../../relational-databases/replication/security/view-and-modify-replication-security-settings.md)  
  
-   [Security Center for SQL Server Database Engine and Azure SQL Database](../../../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md)  
  
## Choosing a Development Environment  
 When developing a replication application, there are three basic development environments to consider. Each development environment has access to the same replication functionalities with some exceptions. Replication applications can be developed in each of the following environments.  
  
-   **Managed code**  
  
     Object oriented development environment that leverages the benefits of the [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] programming and the .NET common language runtime (CLR). Managed code is the recommended programming environment for both .NET development and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] applications. Managed replication interfaces enable the programming of replication administration in an object-oriented manner without having to know [!INCLUDE[tsql](../../../includes/tsql-md.md)], and it also provides some callback functionalities when running replication agents that are not available from scripts. Managed code is the best environment for developing reusable components and user interface applications.  
  
-   **Scripting**  
  
     Simple applications that execute a series of commands as either replication system stored procedures in [!INCLUDE[tsql](../../../includes/tsql-md.md)] scripts or commands in batch files. While you can execute scripts in a managed environment using the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in-process managed provider, the same functionality can obtained by using managed replication interfaces, which also provide callback functionalities. Scripting is the best environment for executing tasks that will run only a few times and where callback functionalities are not required, such as installing a replication server.  
  
-   **Native code**  
  
     Object-oriented development environment that utilizes direct access to the system or COM objects such that code is not managed by the CLR. Native code replication interfaces are deprecated or discontinued. For more information, see [Deprecated Features in SQL Server Replication](../../../relational-databases/replication/deprecated-features-in-sql-server-replication.md) or [Replication Backward Compatibility](../../../relational-databases/replication/replication-backward-compatibility.md).  
  
## Choose the Appropriate Replication Programming Interface  
 The final planning step is choosing the appropriate replication programming interface that implements the desired replication functionality for the chosen development environment. The following table shows the replication programming interfaces available.  
  
|Interface|Environment|Uses|  
|---------------|-----------------|----------|  
|[Replication Management Objects Concepts](../../../relational-databases/replication/concepts/replication-management-objects-concepts.md)|Managed code|Administration, monitoring, and synchronization.|  
|<xref:Microsoft.SqlServer.Replication>|Managed code|Synchronization.|  
|<xref:Microsoft.SqlServer.Replication.BusinessLogicSupport>|Managed code|Creation of business logic handlers to integrate custom logic with the merge synchronization process.|  
|[Replication Stored Procedures &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)|Scripting|Administration and monitoring.|  
|[Replication Agent Executables Concepts](../../../relational-databases/replication/concepts/replication-agent-executables-concepts.md)|Scripting|Synchronization.|  
  
## Example  
 At [!INCLUDE[ssSampleDBCoShort](../../../includes/sssampledbcoshort-md.md)], data needs to be published for 200 sales representatives around the world. The sales representatives travel often and will need to use laptop computers or personal digital assistants (PDAs) to change customer data and add new orders. The changes will then need to be synchronized with the Publisher when the sales representative connects the laptop to the network.  
  
 For this application, the planning steps might look like the following:  
  
1.  The replication topology for this application already exists. However, a new pull subscription must be created at the client. The publication should use parameterized filters to replicate a unique set of data to each sales representative.  
  
2.  In addition to the typical data access required for a sales application, this application should enable a salesperson to synchronize the pull subscription on demand by clicking a button. Since a sales representative will install and run the application, it also needs to be able to configure a subscription and apply the initial snapshot at the client. Optionally, the application will use the infrastructure provided by Windows for sensing wireless connectivity to automatically synchronize the subscription when a connection is detected.  
  
3.  Follow all of the security guidelines for replication, including using Windows Authentication and a virtual private network (VPN) when connecting to the publisher. If implementing Web synchronization, use a Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL), connection. For more information, see [Configure Web Synchronization](../../../relational-databases/replication/configure-web-synchronization.md).  
  
4.  In order to take advantage of the features of the [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)], the application is developed using a managed code language.  
  
5.  Based on these requirements, the Replication Management Objects (RMO) managed interface can provide all of the needed replication functionality for this application.  
  
 This example scenario has been implemented in the AdventureWorks sample application that can be downloaded for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
  
