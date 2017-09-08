---
title: "SQL Server Replication | Microsoft Docs"
ms.custom: ""
ms.date: "03/30/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "replication [SQL Server], about"
  - "replication [SQL Server]"
ms.assetid: 3a5f4592-3c61-4b4d-9ceb-39716aeeba41
caps.latest.revision: 58
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# SQL Server Replication
  Replication is a set of technologies for copying and distributing data and database objects from one database to another and then synchronizing between databases to maintain consistency. Use replication to distribute data to different locations and to remote or mobile users over local and wide area networks, dial-up connections, wireless connections, and the Internet.  
  
 Transactional replication is typically used in server-to-server scenarios that require high throughput, including: improving scalability and availability; data warehousing and reporting; integrating data from multiple sites; integrating heterogeneous data; and offloading batch processing. Merge replication is primarily designed for mobile applications or distributed server applications that have possible data conflicts. Common scenarios include: exchanging data with mobile users; consumer point of sale (POS) applications; and integration of data from multiple sites. Snapshot replication is used to provide the initial data set for transactional and merge replication; it can also be used when complete refreshes of data are appropriate. With these three types of replication, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides a powerful and flexible system for synchronizing data across your enterprise. Replication to SQLCE 3.5 and SQLCE 4.0 is supported on both [!INCLUDE[win8srv](../../includes/win8srv-md.md)] and [!INCLUDE[win8](../../includes/win8-md.md)].  
  
 As an alternative to replication, you can synchronize databases by using Microsoft Sync Framework. Sync Framework includes components and an intuitive and flexible API that make it easy to synchronize among SQL Server, SQL Server Express, SQL Server Compact, and SQL Azure databases. Sync Framework also includes classes that can be adapted to synchronize between a SQL Server database and any other database that is compatible with ADO.NET. For detailed documentation of the Sync Framework database synchronization components, see [Synchronizing Databases](http://go.microsoft.com/fwlink/?LinkId=209079). For an overview of Sync Framework, see [Microsoft Sync Framework Developer Center](http://go.microsoft.com/fwlink/?LinkId=209078). For a comparison between Sync Framework and Merge Replication, see [Synchronizing Databases Overview](http://msdn.microsoft.com/library/bb902818\(SQL.110\).aspx)  
  
 **Browse by area**  
 - [What’s New](../../relational-databases/replication/what-s-new-replication.md)  
 - [Backward Compatibility](../../relational-databases/replication/replication-backward-compatibility.md)  
 - [Replication Features and Tasks](../../relational-databases/replication/replication-features-and-tasks.md)  
 - [Technical Reference](../../relational-databases/replication/technical-reference-replication.md)  
  
  
