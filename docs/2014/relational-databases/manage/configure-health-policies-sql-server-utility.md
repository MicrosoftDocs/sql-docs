---
title: "Configure Health Policies (SQL Server Utility) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
ms.assetid: 030aac3b-8901-4c41-91ed-aba96420276c
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Configure Health Policies (SQL Server Utility)
  Use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility dashboard in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to view [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility resource parameters for managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and data-tier applications. For more information, see [SQL Server Utility Features and Tasks](sql-server-utility-features-and-tasks.md).  
  
 To view [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility health policy results, connect to a utility control point from [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. For more information, see [Use Utility Explorer to Manage the SQL Server Utility](use-utility-explorer-to-manage-the-sql-server-utility.md).  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility health policies can be configured for data-tier applications and managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Health policies can be defined globally for all data-tier applications and managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, or they can be defined individually for each data-tier application and for each managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility.  
  
## Monitoring Policies for Data-tier Applications  
 Overutilization and underutilization policies for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data-tier applications are as follows:  
  
-   Data-tier application processor utilization.  
  
-   Data-tier application file space for database files.  
  
-   Data-tier application file space for storage volumes.  
  
-   Computer processor utilization.  
  
> [!NOTE]  
>  Storage volume and processor utilization are read-only policies for data-tier applications.  
  
 For more information about viewing or changing global monitoring policies for data-tier applications, see [Utility Administration &#40;SQL Server Utility&#41;](../../database-engine/utility-administration-sql-server-utility.md).  
  
 For more information about viewing or changing monitoring policies for individual data-tier applications, see [Deployed Data-tier Application Details &#40;SQL Server Utility&#41;](../../database-engine/deployed-data-tier-application-details-sql-server-utility.md).  
  
## Monitoring Policies for Managed Instances of SQL Server  
 Overutilization and underutilization policies for managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are as follows:  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance processor utilization.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance file space for database files.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance file space for storage volumes.  
  
-   Computer processor utilization.  
  
 For more information about viewing or changing global monitoring policies for managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Utility Administration &#40;SQL Server Utility&#41;](../../database-engine/utility-administration-sql-server-utility.md).  
  
 For more information about viewing or changing monitoring policies for individual managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Managed Instance Details &#40;SQL Server Utility&#41;](../../database-engine/managed-instance-details-sql-server-utility.md).  
  
## See Also  
 [SQL Server Utility Features and Tasks](sql-server-utility-features-and-tasks.md)   
 [Reduce Noise in CPU Utilization Policies &#40;SQL Server Utility&#41;](reduce-noise-in-cpu-utilization-policies-sql-server-utility.md)  
  
  
