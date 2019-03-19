---
title: "Glossary of Terms for Oracle Publishing | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Oracle publishing [SQL Server replication], glossary"
ms.assetid: e21dfa4b-6144-4be7-9cbf-ca2709b2bd9f
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Glossary of Terms for Oracle Publishing
  You should be familiar with the following Oracle terms when configuring and administering Oracle publishing. For a complete list of Oracle terms, see the Oracle online documentation.  
  
 Index Organized Tables (IOT)  
 A table whose data is physically sorted on disk in index order; it is similar to a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] table with a clustered index. An IOT is replicated to a Subscriber as a table with a clustered index.  
  
 Instance  
 An Oracle database is associated with an instance. The instance comprises the memory and background processes supporting the database. An Oracle instance always maps to a single database, whereas an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] can contain many databases. There are circumstances in which an Oracle database can have multiple instances.  
  
 Oracle Listener  
 Handles incoming network traffic for an Oracle database instance. When you configure network connectivity to an Oracle database, you specify the protocol through which traffic is sent and the port on which the Listener listens for traffic. The Listener is typically configured to run on the same computer as the Oracle database instance and can be configured to serve one or more instances.  
  
 ROWID  
 A pointer to the location of a specific row in a database. Because retrieving rows using the ROWID is faster than using a table scan or index, replication uses the ROWID temporarily during processing of published table changes.  
  
 Sequence  
 A database object that is used to generate unique numbers. Replication uses sequences to order changes made to published tables.  
  
 SQL\*Plus  
 An application used to access and query Oracle databases. It is similar to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] **sqlcmd**.  
  
 Synonym  
 An alias for an object. The special public synonym **MSSQLSERVERDISTRIBUTOR** is automatically created when an Oracle Publisher is configured. The synonym references the **HREPL_Distributor** table and provides a logical pointer to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor that services the Publisher.  
  
 After an Oracle database has been published, subsequent attempts to configure this Publisher to use a different [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor will fail, because this public synonym identifies the particular Distributor already configured to service the Publisher.  
  
 Tablespace  
 A unit of database storage that is roughly equivalent to a filegroup in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 TNS Service Name  
 TNS (Transparent Network Substrate) is a communication layer used by Oracle databases. The TNS Service Name is the name by which an Oracle database instance is known on a network. You assign a TNS Service Name when you configure connectivity to the Oracle database. Replication uses the TNS Service name to identify the Publisher and to establish connections.  
  
 User schema  
 A user schema can be thought of as a database user who owns a particular set of database objects. The replication administrative user schema owns all objects created by the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] replication process in the Oracle database, with the exception of the **MSSQLSERVERDISTRIBUTOR** public synonym.  
  
## See Also  
 [Configure an Oracle Publisher](configure-an-oracle-publisher.md)   
 [Objects Created on the Oracle Publisher](objects-created-on-the-oracle-publisher.md)   
 [Non-SQL Server Publishers](non-sql-server-publishers.md)   
 [Oracle Publishing Overview](oracle-publishing-overview.md)  
  
  
