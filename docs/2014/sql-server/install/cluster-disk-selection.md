---
title: "Cluster Disk Selection | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "cluster disk selection"
ms.assetid: 0d6b863d-5972-4a20-9990-64ee8016fea6
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Cluster Disk Selection
  Use the **Cluster Disk Selection** page of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard to select the shared cluster disk resource for your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster. The cluster disk is where the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data will be placed.  
  
 A shared cluster disk is not a requirement for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssDE](../../includes/ssde-md.md)] cluster installations. An SMB file server is a supported storage for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssDE](../../includes/ssde-md.md)] failover cluster installations, and can be specified by using the **Database Engine - Data Directories** page before completing the installation.  
  
> [!WARNING]  
>  If you have selected Analysis Services to be installed, you must specify a shared cluster disk.  
>   
>  If you plan to enable FILESTREAM on this [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster instance, you must specify a shared cluster disk.  
  
## Options  
 **Shared Disks**  
 Select a single disk from the list. The cluster disk is where the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data will be placed.  
  
 Only one disk can be specified. If you select the group containing the cluster quorum resource, a warning will be displayed. We recommend that you do not install to the cluster quorum resource.  
  
 **Available Shared Disks**  
 Displays a list of available disks, whether each is qualified as a shared disk, and a description of each disk resource.  
  
  
