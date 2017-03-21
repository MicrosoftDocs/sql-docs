---
title: "Replication Developer Documentation | Microsoft Docs"
ms.custom: 
  - "rickbyh"
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "developer's guide [SQL Server replication]"
  - "programming [SQL Server replication]"
  - "replication [SQL Server], development"
ms.assetid: 7ee134ae-1cab-4a35-8017-8ac6d8fc64b6
caps.latest.revision: 40
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Replication Developer Documentation
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The ability to programmatically configure, maintain, and monitor a replication topology enables you to both simplify repeated replication tasks and improve the user experience for your replication-based applications. By programming replication, your end-users can be provided with customized replication functionalities without having to be familiar with replication stored procedures and replication agent executables or having to using the replication user interface implemented by [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)].  
  
 The following are scenarios in which your applications might benefit from programmatic access to replication services:  
  
-   Adding replication functionalities to an existing end-user application, such as synchronizing a pull subscription when the user clicks a button.  
  
-   Creating a Web-based user interface for remotely administering replication.  
  
-   Creating a custom user interface that exposes only a subset of administration functionality, can be used to remotely administer multiple replication topologies from a single location, or that combine administration and synchronization functionalities.  
  
-   Improving an existing monitoring tool by adding the ability to monitor the status of a publication, subscription, or at the Distributor.  
  
-   Creating a custom application to administer or synchronize subscriptions to an Oracle publisher.  
  
-   Writing customized business rules that are executed when a merge subscription is synchronized.  
  
-   Generating [!INCLUDE[tsql](../../../includes/tsql-md.md)] scripts that can be run repeated when configuring new Subscribers.  
  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] enables you to programmatically control replication agents and to programmatically administer and monitor a replication topology. To learn more about programming replication, see [Replication Programming Concepts](../../../relational-databases/replication/concepts/replication-programming-concepts.md).  
  
## In This Section  
 [Replication Programming Concepts](../../../relational-databases/replication/concepts/replication-programming-concepts.md)  
 Describes the planning steps to develop an application that uses replication.  
  
 [Replication System Stored Procedures Concepts](../../../relational-databases/replication/concepts/replication-system-stored-procedures-concepts.md)  
 Describes how system stored procedures can be used to proivide programmatic access in a replication topology.  
  
 [Replication Management Objects Concepts](../../../relational-databases/replication/concepts/replication-management-objects-concepts.md)  
 Explains the concepts for using Replication Management Objects (RMO). This is a managed code assembly that encapsulates replication functionalities for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 [Replication Agent Executables Concepts](../../../relational-databases/replication/concepts/replication-agent-executables-concepts.md)  
 Describes the use of Replication Agent Executable files.  
  
 [Developer's Guide: How-to Topics &#40;Replication&#41;](../../../relational-databases/replication/concepts/developer-s-guide-how-to-topics-replication.md)  
 Provides a list of how-to topics that are related to replication.  
  
  