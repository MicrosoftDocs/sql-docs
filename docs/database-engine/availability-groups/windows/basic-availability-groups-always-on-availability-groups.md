---
title: "Basic availability groups for a single database"
description: "Describes the differences between a regular and basic Always On availability group, as well as how to configure a basic availability group. "
ms.custom: "seodec18"
ms.date: "02/01/2018"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
ms.assetid: 285adbc7-ac9b-40f6-b4a9-3f1591d3b632
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Basic Always On availability groups for a single database
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  Always On Basic Availability Groups provide a high availability solution for SQL Server 2016 and SQL Server 2017 Standard Edition. A basic availability group supports a failover environment for a single database. It is created and managed much like traditional (advanced) [Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md) with Enterprise Edition. The differences and limitations of basic availability groups are summarized in this document.  
  
## Features  
 Always On Basic Availability Groups replaces the deprecated Database Mirroring feature and provides a similar level of feature support. Basic availability groups enable a primary database to maintain a single replica. This replica can use either synchronous-commit mode or asynchronous-commit mode. For more information about availability modes, see [Availability Modes &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/availability-modes-always-on-availability-groups.md). The secondary replica remains inactive unless there is a need to failover. This failover reverses the primary and secondary role assignments, causing the secondary replica to become the primary active database. For more information on failover, see [Failover and Failover Modes &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md). Basic availability groups can operate in a hybrid environment that spans on-premises and Microsoft Azure.  
  
## Limitations  
 Basic availability groups use a subset of features compared to advanced availability groups on SQL Server 2016 Enterprise Edition. Basic availability groups include the following limitations:  
  
- Limit of two replicas (primary and secondary). Basic availabililty groups for SQL Server 2017 on Linux support an additional configuration only replica.
  
- No read access on secondary replica.  
  
- No backups on secondary replica.  

- No integrity checks on secondary replicas. 

- No support for replicas hosted on servers running a version of SQL Server prior to SQL Server 2016 Community Technology Preview 3 (CTP3).  

- Support for one availability database.  
  
- Basic availability groups cannot be upgraded to advanced availability groups. The group must be dropped and re-added to a group that contains servers running only SQL Server 2016 Enterprise Edition.  
  
- Basic availability groups are only supported for Standard Edition servers. 

- Basic availability groups can not be part of a distributed availability group. 
  
## Configuration  
 An Always On basic availability group can be created on any two SQL Server 2016 Standard Edition servers. When you create a basic availability group, you must specify both replicas during creation.  
  
 To create a basic availability group, use the **CREATE AVAILABILITY GROUP** transact-SQL command and specify the **WITH BASIC** option (the default is **ADVANCED**). You can also create the basic availability group using the UI in SQL Server Management Studio starting with version 17.8. For more information, see [CREATE AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../../t-sql/statements/create-availability-group-transact-sql.md). 
  
> [!NOTE]  
>  The limitations of basic availability groups apply to the **CREATE AVAILABILITY GROUP** command when **WITH BASIC** is specified. For example, you will get an error if you attempt to create a basic availability group that permits read access. Other limitations apply in the same manner. Refer to the Limitations section of this topic for details.  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)  
  
  
