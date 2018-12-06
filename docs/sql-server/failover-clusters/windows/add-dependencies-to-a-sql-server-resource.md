---
title: "Add Dependencies to a SQL Server Resource | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "resource dependencies [SQL Server]"
  - "failover clustering [SQL Server], dependencies"
  - "clusters [SQL Server], dependencies"
  - "dependencies [SQL Server], clustering"
ms.assetid: 25dbb751-139b-4c8e-ac62-3ec23110611f
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Add Dependencies to a SQL Server Resource
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to add dependencies to an Always On Failover Cluster Instance (FCI) resource by using the Failover Cluster Manager snap-in. The Failover Cluster Manager snap-in is the cluster management application for the Windows Server Failover Clustering (WSFC) service.  
  
-   **Before you begin:**  [Limitations and Restrictions](#Restrictions), [Prerequisites](#Prerequisites)  
  
-   **To add a dependency to a SQL Server resource, using:** [Windows Failover Cluster Manager](#WinClusManager)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
 It is important to note that if you add any other resources to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] group, those resources must always have their own unique SQL network name resources and their own SQL IP address resources.  
  
 Do not use the existing SQL network name resources and SQL IP address resources for anything other than [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. If [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resources are shared with other resources, the following problems may occur:  
  
-   Outages that are not expected may occur.  
  
-   Service pack installations may not be successful.  
  
-   The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup program may not be successful. If this problem occurs, you cannot install additional instances of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] or perform routine maintenance.  
  
 Consider these additional issues:  
  
-   FTP with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] replication: For instances of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that use FTP with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] replication, your FTP service must use one of the same physical disks as the installation of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that is set up to use the FTP service.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resource dependencies: If you add a resource to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] group and you have a dependency on the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resource to make sure that [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is available, [!INCLUDE[msCoName](../../../includes/msconame-md.md)] recommends that you add a dependency on the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent resource. Do not add a dependency on the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resource. To make sure that the computer that is running [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] remains highly available, configure the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent resource so that it does not affect the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] group if the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent resource fails.  
  
-   File shares and printer resources: When you install File Share resources or Printer cluster resources, they should not be put on the same physical disk resources as the computer that is running [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. If they are put on the same physical disk resources, you may experience performance degradation and loss of service to the computer that is running [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
-   MS DTC considerations: After you install the operating system and configure your FCI, you must configure [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC) to work in a cluster by using the Failover Cluster Manager snap-in. Failure to cluster MS DTC will not block [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup, but [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] application functionality may be affected if MS DTC is not properly configured.  
  
     If you install MS DTC in your [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] group and you have other resources that are dependent on MS DTC, MS DTC will not be available if this group is offline or during a failover. [!INCLUDE[msCoName](../../../includes/msconame-md.md)] recommends that you put MS DTC in its own group with its own physical disk resource, if it is possible.  
  
###  <a name="Prerequisites"></a> Prerequisites  
 If you install [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] into a WSFC resource group with multiple disk drives and choose to place your data on one of the drives, the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resource will be set to be dependent only on that drive. To put data or logs on another disk, you must first add a dependency to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resource for the additional disk.  
  
##  <a name="WinClusManager"></a> Using the Failover Cluster Manager Snap-in  
 **To add a dependency to a SQL Server resource**  
  
-   Open the Failover Cluster Manager snap-in.  
  
-   Locate the group that contains the applicable [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resource that you would like to make dependent.  
  
-   If the resource for the disk is already in this group, go to step 4. Otherwise, locate the group that contains the disk. If that group and the group that contains [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] are not owned by the same node, move the group containing the resource for the disk to the node that owns the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] group.  
  
-   Select the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resource, open the **Properties** dialog box, and use the **Dependencies** tab to add the disk to the set of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] dependencies.  
  
  
