---
title: "Contained Databases with Always On Availability Groups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "05/17/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-high-availability"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], interoperability"
  - "contained database, AlwaysOnAvailabilityGroups"
ms.assetid: cacce3ae-e940-4566-8298-6607c4268e74
caps.latest.revision: 9
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "jhubbard"
---
# Contained Databases with Always On Availability Groups (SQL Server)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  This topic contains information about the using a contained database with [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  
 **In this Topic:**  
  
-   [Prerequisites](#Prerequisites)  
  
-   [Related Tasks](#RelatedTasks)  
  
##  <a name="Prerequisites"></a> Prerequisites  
  
-   Before adding a contained database to an availability group, ensure that the **contained database authentication** server option is set to **1** on every server instance that hosts an availability replica for the availability group. For more information, see [contained database authentication Server Configuration Option](../../../database-engine/configure-windows/contained-database-authentication-server-configuration-option.md) and [Server Configuration Options &#40;SQL Server&#41;](../../../database-engine/configure-windows/server-configuration-options-sql-server.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Server Configuration Options &#40;SQL Server&#41;](../../../database-engine/configure-windows/server-configuration-options-sql-server.md)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Contained Databases](../../../relational-databases/databases/contained-databases.md)  
  
  
