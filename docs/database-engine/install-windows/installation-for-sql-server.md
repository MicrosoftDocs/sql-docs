---
title: "SQL Server Installation | Microsoft Docs"
ms.custom: ""
ms.date: "09/06/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
f1_keywords: 
  - "sql13.portal.Installation.f1"
helpviewer_keywords: 
  - "installing SQL Server, initial installation"
  - "installation [SQL Server]"
  - "initial installation [SQL Server]"
ms.assetid: edd75f68-dc62-4479-a596-57ce8ad632e5
author: MashaMSFT
ms.author: mathoma
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
manager: craigg
---
# SQL Server Installation

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard provides a single feature tree to install all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components:  
  
-   [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
-   [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]  
-   [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]  
-   [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]  
-   [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)]  
-   [!INCLUDE[ssDQSnoversion](../../includes/ssdqsnoversion-md.md)]  
-   Connectivity components  
  
Starting with [!INCLUDE[ss2016](../../includes/sssql15-md.md)], SQL Server Management Tools is no longer installed from  the main feature tree; for details see [Download SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md)  
  
You can install each component individually or select a combination of the components listed above. To make the best choice among the editions and components available in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see the features supported by your version of SQL Server:

- [Editions and supported features of [!INCLUDE[ss2017](../../includes/sssqlv14-md.md)]](~/sql-server/editions-and-components-of-sql-server-2017.md).  
- [Editions and supported features of [!INCLUDE[ss2016](../../includes/sssql15-md.md)]](~/sql-server/editions-and-components-of-sql-server-2016.md).  
- [Features supported by the editions of [!INCLUDE[ss2014](../../includes/sssql14-md.md)]](https://technet.microsoft.com/library/cc645993(v=sql.120).aspx)
  
## In This Section  
Regardless of whether you use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard or the command prompt to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Setup involves the following steps:  
  
[Planning a SQL Server Installation](../../sql-server/install/planning-a-sql-server-installation.md)  
Describes how to prepare your computer for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   Hardware and software requirements.  
-   System Configuration Checker requirements and blocking issues.  
-   Security considerations.  
  
[Install SQL Server](../../database-engine/install-windows/install-sql-server.md)  
 Describes installation options for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
[SQL Server Setup User Interface Reference](https://msdn.microsoft.com/library/183b5cdd-962e-41ca-8064-ea44f622c77d)  
 Describes the installation options presented by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard.  
  
[Upgrade SQL Server](../../database-engine/install-windows/upgrade-sql-server.md)  
 Describes options for upgrading to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
[Uninstall SQL Server](../../sql-server/install/uninstall-sql-server.md)  
 Describes procedures to uninstall [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)].  
  
[SQL Server Failover Cluster Installation](../../sql-server/failover-clusters/install/sql-server-failover-cluster-installation.md)  
 This section of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup documentation explains how to install, and configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster.  
  
[Install SQL Server Business Intelligence Features](../../sql-server/install/install-sql-server-business-intelligence-features.md)  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features that are part of the Microsoft BI platform include [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], and several client applications used for creating or working with analytical data. This section of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup documentation explains how to install [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
## More Information
[Install SQL Server BI Features with SharePoint &#40;Power Pivot and Reporting Services&#41;](https://msdn.microsoft.com/library/3166107c-30c2-468e-bb1b-bb42b79b37c3)  
 This section explains how to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features in a SharePoint environment. It identifies which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features are available given a specific version and edition of SharePoint. It also includes installation procedures for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint and Reporting Services in SharePoint mode.  
  
![ssrs_fyi_note](../../analysis-services/instances/install-windows/media/ssrs-fyi-note.png) Install the new sample database, [Wide World Importers](../../sample/world-wide-importers/wide-world-importers-documentation.md). 
  
[Additional SQL Server Samples and Sample Databases](https://sqlserversamples.codeplex.com/)  
 Describes how to install and configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] samples and sample databases.  
  
See the [SQL Server Update Center](https://msdn.microsoft.com/library/ff803383.aspx) for links and information for all supported versions of [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)].  
  
## See Also  
[What's New in SQL Server Installation](../../sql-server/install/what-s-new-in-sql-server-installation.md)   
[Hardware and Software Requirements for Installing SQL Server](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)  
