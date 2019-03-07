---
title: "Contained Databases with Always On Availability Groups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "contained database [SQL Server], AlwaysOn Availability Groups"
  - "Availability Groups [SQL Server], interoperability"
ms.assetid: cacce3ae-e940-4566-8298-6607c4268e74
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Contained Databases with Always On Availability Groups (SQL Server)
  This topic contains information about the using a contained database with [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  
 **In this Topic:**  
  
-   [Prerequisites](#Prerequisites)  
  
-   [Related Tasks](#RelatedTasks)  
  
##  <a name="Prerequisites"></a> Prerequisites  
  
-   Before adding a contained database to an availability group, ensure that the `contained database authentication` server option is set to `1` on every server instance that hosts an availability replica for the availability group. For more information, see [contained database authentication Server Configuration Option](../../configure-windows/contained-database-authentication-server-configuration-option.md) and [Server Configuration Options &#40;SQL Server&#41;](../../configure-windows/server-configuration-options-sql-server.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Server Configuration Options &#40;SQL Server&#41;](../../configure-windows/server-configuration-options-sql-server.md)  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Contained Databases](../../../relational-databases/databases/contained-databases.md)  
  
  
