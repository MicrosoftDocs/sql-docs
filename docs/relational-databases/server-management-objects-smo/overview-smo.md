---
description: "Overview (SMO)"
title: "Overview (SMO) | Microsoft Docs"
ms.custom: ""
ms.date: "08/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 

ms.topic: "reference"
ms.assetid: e988f9e8-6801-41d1-8069-726f487244d5
author: "markingmyname"
ms.author: "maghan"
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Overview (SMO)
[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Objects (SMO) are objects designed for programmatic management of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can use SMO to build customized [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] management applications. Although [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] is a powerful and extensive application for managing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], there might be times when you would be better served by an SMO application.  
  
 For example, the user applications that control the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] management tasks might have to be simplified to meet the needs of new users and to reduce training costs. You might have to create customized [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases, or create an application for creating and monitoring the efficiency of indexes. An SMO application might also be used to include third-party hardware or software seamlessly into the database management application.  
  
 Because SMO is compatible with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions, you can easily manage a multi-version environment.  
  
 Features in SMO include the following:  
  
-   Cached object model and optimized object instance creation. Objects are loaded only when referenced. Object properties are only partially loaded when the object is created. The remaining objects and properties are loaded when they are referenced directly.  
  
-   Batched execution of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. Statements are batched to improve network performance.  
  
-   Capture [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. Allows any operation to be captured into a script. [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] uses this capability to script an operation instead of executing it immediately.  
  
-   Management of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services with the WMI Provider. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services can be started, stopped, and paused programmatically.  
  
-   Advanced Scripting. [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts can be generated to re-create [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects that describe relationships to other objects on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Use of Unique Resource Names (URNs). A URN allows you to create instances of and reference SMO objects.  
  
 SMO also represents as new objects or properties many features and components that were introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. These new features and components include the following:  
  
-   Table and index partitioning for storage of data on a partition scheme. For more information, see [Partitioned Tables and Indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md).  
  
-   HTTP endpoints for managing SOAP requests. For more information, see [Implementing Endpoints](../../relational-databases/server-management-objects-smo/tasks/implementing-endpoints.md).  
  
-   Snapshot isolation and row level versioning for increased concurrency. For more information, see [Working with Snapshot Isolation](../../relational-databases/native-client/features/working-with-snapshot-isolation.md).  
  
-   XML Schema collection, XML indexes, and XML datatype provide validation and storage of XML data. For more information, see [XML Schema Collections &#40;SQL Server&#41;](../../relational-databases/xml/xml-schema-collections-sql-server.md) and [Using XML Schemas](../../relational-databases/server-management-objects-smo/tasks/using-xml-schemas.md).  
  
-   Snapshot databases for creating read-only copies of databases.  
  
-   [!INCLUDE[ssSB](../../includes/sssb-md.md)] support for message-based communication. For more information, see [SQL Server Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md).  
  
-   Synonym support for multiple names of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database objects. For more information, see [Synonyms &#40;Database Engine&#41;](../../relational-databases/synonyms/synonyms-database-engine.md).  
  
-   The management of Database Mail that lets you create e-mail servers, e-mail profiles, and e-mail accounts in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Database Mail](../../relational-databases/database-mail/database-mail.md).  
  
-   Registered Servers support for registering connection information. For more information, see [Register Servers](../../ssms/register-servers/register-servers.md).  
  
-   Trace and replay of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] events. For more information, see [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md), [SQL Trace](../../relational-databases/sql-trace/sql-trace.md), [SQL Server Distributed Replay](../../tools/distributed-replay/sql-server-distributed-replay.md), and [Extended Events](../../relational-databases/extended-events/extended-events.md).  
  
-   Support for certificates and keys for security control. For more information, see [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md).  
  
-   DDL triggers for adding functionality when DDL events occur. For more information, see [DDL Triggers](../../relational-databases/triggers/ddl-triggers.md).  
  
 The SMO namespace is <xref:Microsoft.SqlServer.Management.Smo>. SMO is implemented as a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] assembly. This means that the common language runtime from the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] version 2.0 must be installed before using the SMO objects. The SMO assemblies are installed by default into the Global Assembly Cache (GAC) with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SDK option. The assemblies are located in C:\Program Files\Microsoft SQL Server\130\SDK\Assemblies\. For more information, see the [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)][!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] documentation.  
  
## SMO Classes  
 SMO classes include two categories: instance classes and utility classes.  
  
 **Instance Classes**  
  
 The instance classes represent [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects such as servers, databases, tables, triggers, and stored procedures. The <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class is used to establish a connection to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and control the capture mode of commands sent to it.  
  
 The SMO instance objects form a hierarchy that represents the hierarchy of a database server. At the top are the instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], under which are the databases, and following on with tables, columns, triggers, and so on. If it is logical that there is a one parent to many children relationship, such as a table having one or more columns, then the child is represented by a collection of objects. Otherwise the child is represented by one object.  
  
 **Utility Classes**  
  
 Utilities classes are a group of objects that have been created explicitly to perform specific tasks. They have been divided into different object hierarchies based on function:  
  
-   Transfer class. This is used to transfer schema and data to another database.  
  
-   Backup and Restore classes. These are used to back up and restore databases.  
  
-   Scripter Class. This is used to create script files for the regeneration of objects and their dependencies.  
  
## SMO Features  
 **Optimized Performance**  
  
 The SMO architecture is efficient in terms of memory because objects are only partially instantiated at first, and minimal property information is requested from the server. Full instantiation of objects is delayed until the object is explicitly referenced. An object is fully instantiated when a property is requested that is not in the set of properties that are first retrieved, or when a method is called that requires such a property. The transition between partially instantiated and fully instantiated objects is transparent to the user. Additionally, some properties that use lots of memory are never retrieved, unless the property explicitly referenced. An example of this is the <xref:Microsoft.SqlServer.Management.Smo.Database.Size%2A> property of the <xref:Microsoft.SqlServer.Management.Smo.Database> object property. However, partial instantiation does require more network round trips and might not be the best performing option for your application.  
  
 You can control instantiation to suit the system environment. Relying on delayed instantiation minimizes the amount of memory required by the application, although it might trigger many server requests when properties are referenced.  
  
 Instance classes, objects that represent real database objects, can exist in three levels of instantiation. These are minimal-instantiated (only the minimal required properties are read in one block), partially instantiated (all the properties that use a relatively large amount of memory are read in one block), and fully instantiated. Uninstantiated and fully instantiated are the traditional states of instantiation. The partially instantiated state increases efficiency because a partially instantiated object does not contain values for the full set of object properties. Partial instantiation is the default state for an object that is not directly referenced. When one of these properties is referenced, a fault is generated that prompts a full instantiation of the object.  
  
 **Capture Execution**  
  
 Direct execution is the usual method of execution. Statements are sent to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directly as they are incurred. Capture execution is the alternative to this.  
  
 Capture execution lets you capture [!INCLUDE[tsql](../../includes/tsql-md.md)] batches that would typically be executed. This lets the SMO programmer defer the script, store it for later execution, or provide a preview for the end-user. For example, a **create database**, a **create table**, and a **create index** statement can be sent in one batch and then run as three sequential steps. This functionality is controlled by the user by using the <xref:Microsoft.SqlServer.Management.Smo.Server.%23ctor%2A> object.  
  
 **WMI Provider**  
  
 The WMI Provider objects are wrapped by SMO. This provides the SMO programmer with a simple object model that is similar to SMO classes closely, without the requirement to understand the programming model that is represented by the namespace and the details of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] WMI Provider. The WMI Provider lets you configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services, aliases, and client and server network libraries.  
  
 **Scripting**  
  
 In SMO, scripting has been enhanced and moved into the **Scripter** class. The **Scripter** class can discover dependencies, understand the relationships between objects, and enables manipulation of the dependency-hierarchy. The main scripting object is the **Scripter** object. There are also several supporting objects that handle the dependencies and respond to progress or error events.  
  
 The **Scripter** object supports the following advanced scripting options:  
  
-   Simple 1-phase scripting (creates the script in one step)  
  
-   Advanced 3-phase scripting (creates the script in three steps; dependency discovery, list generation, script generation)  
  
-   Two-way dependency discovery (allows for discovery of dependencies, or dependents)  
  
-   Response to progress events  
  
-   Response to error events  
  
 **Unique Resource Names**  
  
 A key concept in using the SMO object library is the Unique Resource Name (URN). The URN uses a syntax similar to XPath. The XPath is a hierarchy path used to specify an object in which each level has qualifiers and functions. In SMO the URN has two elements, the path and attribute naming that has limited functionality. The path is used to specify the location of the object whereas the attribute naming allows for a degree of filtering.  
  
 An example of an URN for a database is  
  
```  
/Server/Database[@Name='Adventureworks2012']  
```  
  
 The URN of an object can be retrieved by referencing its URN property. The Scripter object also uses URNs as parameters that pass object references to the method of the **Scripter** object. Additionally, an URN can be specified for the **GetSmoObject** method of the **Server** object. This is used to create an instance of the SMO object.  
  
## SQL Server Features Represented in SMO  
 **Table and Index Partitioning**  
  
 Index Table Partitioning lets you manage the spread of data in tables and indexes across file groups. This new feature is represented by SMO objects.  
  
 **EndPoints**  
  
 SOAP and database mirroring requests are handled by endpoints using the <xref:Microsoft.SqlServer.Management.Smo.Endpoint> object.  
  
 **Snapshot Isolation/Row Level Versioning**  
  
 Snapshot Isolation (row level versioning) is represented by new <xref:Microsoft.SqlServer.Management.Smo.Database> object properties.  
  
 **XML Schema Namespace, XML Indexes and XML datatype**  
  
 XML Schema Namespaces are represented in SMO by a collection of objects. XML indexes are represented in SMO by an **Index** object property.  
  
 **Full-Text Search Enhancements**  
  
 New objects are provided in SMO that represent the enhancements to full text search.  
  
 **Page Verify**  
  
 The <xref:Microsoft.SqlServer.Management.Smo.DatabaseOptions.PageVerify%2A> object represents database page verify options.  
  
 **Snapshot Databases**  
  
 A snapshot database is a read-only copy of a specified database as a specific point in time. A snapshot database can be specified by using the <xref:Microsoft.SqlServer.Management.Smo.Database.IsDatabaseSnapshot%2A> property of the <xref:Microsoft.SqlServer.Management.Smo.Database> object.  
  
 **Service Broker**  
  
 [!INCLUDE[ssSB](../../includes/sssb-md.md)] and its functionality is represented by a group of objects  
  
 **Index Enhancements**  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] index enhancements are represented by new properties in the <xref:Microsoft.SqlServer.Management.Smo.Index> object.  
  
## See Also  
 [Replication Management Objects Concepts](../../relational-databases/replication/concepts/replication-management-objects-concepts.md)  
  
