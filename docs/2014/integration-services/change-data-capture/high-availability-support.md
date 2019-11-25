---
title: "High Availability Support | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 2e0f6d3f-0536-46d9-8630-835e199515bf
author: janinezhang
ms.author: janinez
manager: craigg
---
# High Availability Support
  The CDC Service for Oracle is designed for high availability. The following features provide part of the high availability support:  
  
-   The CDC Service for Oracle does not use any file resource (local or otherwise). Its entire state is stored in the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. This makes it easy to start the service on a different computer that uses the same [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance if the computer the service runs on fails. To reduce recovery time, long or long-running Oracle transactions are kept in a staging table in the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], preventing the need to re-scan many Oracle transaction logs following a failure (or service restart).  
  
-   The CDC Service for Oracle can use clustered [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances so it can recover after the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance fails over to another cluster node. The Oracle CDC Service Computer Administrator only needs to specify the connection information to the clustered [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance when creating an Oracle CDC Service.  
  
-   The CDC Service for Oracle can use the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]**AlwaysOn** database mirroring feature. This support requires that the MSXDBCDC and all the CDC databases are in the same availability group. It also requires the Oracle CDC Service Computer Administrator to specify the appropriate **AlwaysOn** connection information to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] availability group (for example, the connection properties `Failover_Partner and Network=dbmssocn`). This allows the CDC service to automatically resume processing on a secondary replication of the databases following a failover.  
  
-   The CDC Service for Oracle can be configured as a generic service resource on a Windows failover cluster (along with, or separate from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]), making it simple to fail over and fall back CDC processing with the cluster. To configure the CDC Service for Oracle as a resource in a failover cluster, the system administrator must set the CDC Service for Oracle as a Generic Service Resource on each node on the failover cluster.  
  
-   The CDC Service for Oracle supports Oracle RAC, which allows it to communicate with the Oracle database and process logs even when one of the Oracle RAC nodes is down.  
  
  
