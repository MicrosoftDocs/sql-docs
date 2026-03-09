---
title: "Replication Developer Documentation"
description: Programming interfaces and concepts for building applications that configure, maintain, and monitor SQL Server replication topologies.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 02/24/2026
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
ms.custom:
  - updatefrequency5
ai-usage: ai-assisted
helpviewer_keywords:
  - "developer's guide [SQL Server replication]"
  - "programming [SQL Server replication]"
  - "replication [SQL Server], development"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Replication developer documentation

[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]

The ability to programmatically configure, maintain, and monitor a replication topology enables you to simplify repeated replication tasks and improve the user experience for your replication-based applications. By programming replication, your end-users can be provided with customized replication functionalities without having to be familiar with replication stored procedures and replication agent executables or having to use the replication user interface implemented by [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)].

The following are scenarios in which your applications might benefit from programmatic access to replication services:

- Adding replication functionalities to an existing end-user application, such as synchronizing a pull subscription when the user selects a button.
- Creating a web-based user interface for remotely administering replication.
- Creating a custom user interface that exposes only a subset of administration functionality, can be used to remotely administer multiple replication topologies from a single location, or that combine administration and synchronization functionalities.
- Improving an existing monitoring tool by adding the ability to monitor the status of a publication, subscription, or at the Distributor.
- Creating a custom application to administer or synchronize subscriptions to an Oracle publisher.
- Writing customized business rules that are executed when a merge subscription is synchronized.
- Generating [!INCLUDE[tsql](../../../includes/tsql-md.md)] scripts that can be run repeated when configuring new Subscribers.

[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] enables you to programmatically control replication agents and to programmatically administer and monitor a replication topology. To learn more about programming replication, see [Replication Programming Concepts](replication-programming-concepts.md).

## Programming interfaces

The following programming interfaces are available for developing replication applications.

| Article | Description |
| --- | --- |
| [Replication programming concepts](replication-programming-concepts.md) | Describes the planning steps to develop an application that uses replication. |
| [Replication system stored procedures concepts](replication-system-stored-procedures-concepts.md) | Describes how system stored procedures can be used to provide programmatic access in a replication topology. |
| [Replication management objects concepts](replication-management-objects-concepts.md) | Explains the concepts for using Replication Management Objects (RMO), a managed code assembly that encapsulates replication functionalities for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. |
| [Replication agent executables concepts](replication-agent-executables-concepts.md) | Describes the use of replication agent executable files for command-line automation. |

## Related content

- [SQL Server replication](../sql-server-replication.md)
- [Replication stored procedures (Transact-SQL)](../../system-stored-procedures/replication-stored-procedures-transact-sql.md)