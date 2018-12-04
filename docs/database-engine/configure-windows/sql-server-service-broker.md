---
title: "SQL Server Service Broker | Microsoft Docs"
ms.custom: ""
ms.date: "09/07/2018"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
f1_keywords: 
  - SQL13.SWB.SSBMSGTYPEPROPERTIES.GENERAL.F1
  - SQL13.SWB.SSBCONTRACTPROPERTIES.GENERAL.F1
  - SQL13.SWB.SSBQUEUEPROPERTIES.GENERAL.F1
  - SQL13.SWB.SSBREMSVCBINDPROPERTIES.GENERAL.F1
  - SQL13.SWB.SSBROUTEPROPERTIES.GENERAL.F1
  - SQL13.SWB.SSBPRIORITYPROPERTIES.GENERAL.F1
  - SQL13.SWB.SSBSERVICEPROPERTIES.GENERAL.F1
helpviewer_keywords: 
  - "Broker See Service Broker"
  - "SQL Server Service Broker"
  - "Service Broker"
ms.assetid: 8b8b3b57-fd46-44de-9a4e-e3a8e3999c1e
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017"
---
# SQL Server Service Broker
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssSB](../../includes/sssb-md.md)] provides native support for messaging and queuing applications in the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. This makes it easier for developers to create sophisticated applications that use the [!INCLUDE[ssDE](../../includes/ssde-md.md)] components to communicate between disparate databases. Developers can use [!INCLUDE[ssSB](../../includes/sssb-md.md)] to easily build distributed and reliable applications.  
  
 Application developers who use [!INCLUDE[ssSB](../../includes/sssb-md.md)] can distribute data workloads across several databases without programming complex communication and messaging internals. This reduces development and test work because [!INCLUDE[ssSB](../../includes/sssb-md.md)] handles the communication paths in the context of a conversation. It also improves performance. For example, front-end databases supporting Web sites can record information and send process intensive tasks to queue in back-end databases. [!INCLUDE[ssSB](../../includes/sssb-md.md)] ensures that all tasks are managed in the context of transactions to assure reliability and technical consistency.  
  
## Where is the documentation for Service Broker?  
 The reference documentation for [!INCLUDE[ssSB](../../includes/sssb-md.md)] is included in the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] documentation. This reference documentation includes the following sections:  
  
-   [Data Definition Language &#40;DDL&#41; Statements &#40;Transact-SQL&#41;](~/mdx/mdx-data-definition-statements-mdx.md) for CREATE, ALTER, and DROP statements  
  
-   [Service Broker Statements](../../t-sql/statements/service-broker-statements.md)  
  
-   [Service Broker Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/service-broker-catalog-views-transact-sql.md)  
  
-   [Service Broker Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/service-broker-related-dynamic-management-views-transact-sql.md)  
  
-   [ssbdiagnose Utility &#40;Service Broker&#41;](../../tools/ssbdiagnose/ssbdiagnose-utility-service-broker.md)  
  
 See the [previously published documentation](https://go.microsoft.com/fwlink/?LinkId=231312) for [!INCLUDE[ssSB](../../includes/sssb-md.md)] concepts and for development and management tasks. This documentation is not reproduced in the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] documentation due to the small number of changes in [!INCLUDE[ssSB](../../includes/sssb-md.md)] in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
## What's new in Service Broker  
 No significant changes are introduced in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  The following changes were introduced in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].  

### Service broker and Azure SQL Database Managed Instance

- Cross-instance service broker is not supported 
 - `sys.routes` - Prerequisite: select address from sys.routes. Address must be LOCAL on every route. See [sys.routes](../../relational-databases/system-catalog-views/sys-routes-transact-sql.md).
 - `CREATE ROUTE` - you cannot use `CREATE ROUTE` with `ADDRESS` other than `LOCAL`. See [CREATE ROUTE](https://docs.microsoft.com/sql/t-sql/statements/create-route-transact-sql).
 - `ALTER ROUTE` cannot use `ALTER ROUTE` with `ADDRESS` other than `LOCAL`. See [ALTER ROUTE](../../t-sql/statements/alter-route-transact-sql.md).  
  
### Messages can be sent to multiple target services (multicast)  
 The syntax of the [SEND &#40;Transact-SQL&#41;](../../t-sql/statements/send-transact-sql.md) statement has been extended to enable multicast by supporting multiple conversation handles.  
  
### Queues expose the message enqueued time  
 Queues have a new column, **message_enqueue_time**, that shows how long a message has been in the queue.  
  
### Poison message handling can be disabled  
 The [CREATE QUEUE &#40;Transact-SQL&#41;](../../t-sql/statements/create-queue-transact-sql.md) and [ALTER QUEUE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-queue-transact-sql.md) statements now have the ability to enable or disable poison message handling by adding the clause, `POISON_MESSAGE_HANDLING (STATUS = ON | OFF)`. The catalog view **sys.service_queues** now has the column **is_poison_message_handling_enabled** to indicate whether poison message is enabled or disabled.  
  
### Always On support in Service Broker  
 For more information, see [Service Broker with Always On Availability Groups (SQL Server)](../../database-engine/availability-groups/windows/service-broker-with-always-on-availability-groups-sql-server.md).  
  
  

