---
title: "Use contained databases with availability groups"
description: "Learn about the using a contained database with Always On availability groups in SQL Server 2019 (15.x)."
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: how-to
ms.custom: seodec18
helpviewer_keywords:
  - "Availability Groups [SQL Server], interoperability"
  - "contained database, AlwaysOnAvailabilityGroups"
---
# Use contained databases with Always On availability groups 
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

  This topic contains information about the using a contained database with [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)].  
  
##  <a name="Prerequisites"></a> Prerequisites  
  
-   Before adding a contained database to an availability group, ensure that the **contained database authentication** server option is set to **1** on every server instance that hosts an availability replica for the availability group. For more information, see [contained database authentication Server Configuration Option](../../../database-engine/configure-windows/contained-database-authentication-server-configuration-option.md) and [Server Configuration Options &#40;SQL Server&#41;](../../../database-engine/configure-windows/server-configuration-options-sql-server.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Server Configuration Options &#40;SQL Server&#41;](../../../database-engine/configure-windows/server-configuration-options-sql-server.md)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Contained Databases](../../../relational-databases/databases/contained-databases.md)  
  
  
