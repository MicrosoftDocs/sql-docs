---
title: "Host a report server database in a SQL Server failover cluster"
description: "Host a report server database in a SQL Server failover cluster"
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Host a report server database in a SQL Server failover cluster

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides failover clustering support so that you can use multiple disks for one or more [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances. Failover clustering is supported only for the report server database; you can't run the Report Server service as part of a failover cluster.  
  
 To host a report server database on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster, the cluster must already be installed and configured. You can then select the failover cluster as the server name when you create the report server database in the Database Setup page of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool.  
  
 Although the Report Server service can't participate in a failover cluster, you can install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] on a computer that has a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster installed. The report server runs independently of the failover cluster. If you install a report server on a computer that is part of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover instance, you aren't required to use the failover cluster for the report server database. You can use a different [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance to host the database.  
  
## Related content

- [Report server database &#40;SSRS native mode&#41;](../../reporting-services/report-server/report-server-database-ssrs-native-mode.md)
- [Create a report server database &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-report-server-create-a-report-server-database.md)
