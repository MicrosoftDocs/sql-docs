---
title: "Planning a SQL Server Installation | Microsoft Docs"
ms.custom: ""
ms.date: "08/23/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: quickstart
helpviewer_keywords: 
  - "installing SQL Server, planning"
ms.assetid: b1d56f2f-603f-48f2-b902-c715f14a6db9
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Planning a SQL Server Installation
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  To install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], follow these steps:  
  
-   Review installation requirements, system configuration checks, and security considerations for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation.  
  
-   Run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup to install or upgrade to a later version. Before upgrading, review [Upgrade SQL Server](../../database-engine/install-windows/upgrade-sql-server.md).  
  
-   Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] utilities to configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 Regardless of the installation method, you are required to confirm acceptance of the software license terms as an individual or on behalf of an entity, unless your use of the software is governed by a separate agreement such as a [!INCLUDE[msCoName](../../includes/msconame-md.md)] volume licensing agreement or a third-party agreement with an ISV or OEM.  
  
 The license terms are displayed for review and acceptance in the Setup user interface. Unattended installations (using the `/Q` or `/QS` parameters) must include the `/IAcceptSQLServerLicenseTerms` parameter. Download and review the license terms separately at [Microsoft SQL Server License Terms and Information](https://www.microsoft.com/Licensing/product-licensing/sql-server.aspx). For volume licensing terms, see [Licensing Termss and Documentation](https://www.microsoftvolumelicensing.com/DocumentSearch.aspx?Mode=3&DocumentTypeId=53). For older versions of SQL Server, see [Microsoft Software License Terms](https://go.microsoft.com/fwlink/?LinkID=148209).  
  
> [!NOTE]  
>  Depending on how you received the software (for example, through [!INCLUDE[msCoName](../../includes/msconame-md.md)] volume licensing), your use of the software may be subject to additional terms and conditions.  
  
## In This Section  
 [What's New in SQL Server Installation](../../sql-server/install/what-s-new-in-sql-server-installation.md)  
 This article describes the details about the new or improved features of installation in this version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 [Hardware and Software Requirements for Installing SQL Server](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)  
 This article lists the minimum hardware and software requirements to install and run an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 [Security Considerations for a SQL Server Installation](../../sql-server/install/security-considerations-for-a-sql-server-installation.md)  
 This article describes some security best practices that you should consider before you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and after you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)  
 This article describes the default configuration of services in this release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and configuration options for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services that you can set during and after [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation.  
  
 [Network Protocols and Network Libraries](../../sql-server/install/network-protocols-and-network-libraries.md)  
 This article describes the default configuration of network protocols in this release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and the configuration options available.  
  
 [Work with Multiple Versions and Instances of SQL Server](../../sql-server/install/work-with-multiple-versions-and-instances-of-sql-server.md)  
 This article describes the considerations for installing multiple versions and instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 [Local Language Versions in SQL Server](../../sql-server/install/local-language-versions-in-sql-server.md)  
 This article describes about the localized versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Related Sections  
 [Install SQL Server](../../database-engine/install-windows/install-sql-server.md)  
 This section provides an overview of different installation options we have for installing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 [Install SQL Server Business Intelligence Features](../../sql-server/install/install-sql-server-business-intelligence-features.md)  
 This section of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup documentation explains how to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features that are part of the Microsoft BI platform.  
  
 [Upgrade SQL Server](../../database-engine/install-windows/upgrade-sql-server.md)  
 The section provides an overview of upgrading instances of previous [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] versions to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 [Uninstall SQL Server](../../sql-server/install/uninstall-sql-server.md)  
 Refer this section to uninstall an existing instance of [!INCLUDE[ssNoversion](../../includes/ssnoversion-md.md)] completely, and prepare the system so that you can reinstall [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 [SQL Server Failover Cluster Installation](../../sql-server/failover-clusters/install/sql-server-failover-cluster-installation.md)  
 This section of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup documentation explains how to install, and configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster.  
  
## See Also  
 [Install SQL Server from the Command Prompt](../../database-engine/install-windows/install-sql-server-2016-from-the-command-prompt.md)   
 [High Availability Solutions &#40;SQL Server&#41;](../../database-engine/sql-server-business-continuity-dr.md)   
 [Before Installing Failover Clustering](../../sql-server/failover-clusters/install/before-installing-failover-clustering.md)   
 [Upgrade SQL Server Using the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)  
