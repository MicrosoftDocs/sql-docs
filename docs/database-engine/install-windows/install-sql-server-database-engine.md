---
title: "Install SQL Server Database Engine | Microsoft Docs"
ms.custom: ""
ms.date: "09/05/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
helpviewer_keywords: 
  - "Database Engine [SQL Server], installing"
ms.assetid: d0876e7f-aa52-4dd7-bd5c-029e2ffded5f
author: MashaMSFT
ms.author: mathoma
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
manager: craigg
---
# Install SQL Server Database Engine

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

The [!INCLUDE[ssDE](../../includes/ssde-md.md)] component of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is the core service for storing, processing, and securing data. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] provides controlled access and rapid transaction processing to meet the requirements of the most demanding data consuming applications in your enterprise.  
  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports up to 50 instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] on a single computer. To create a typical [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation, see [Install SQL Server from the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md).  
  
>[!IMPORTANT]
>For local installations, you must run Setup as an administrator. If you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share.  
  
## Related features

The following features are installed when you select **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Database Engine** on the Components to Install page of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard:  
  
-   [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
-   Replication - is an optional component  

-   [Machine Learning Services (In-Database) with R and Python](../../advanced-analytics/install/sql-machine-learning-services-windows-install.md) - is an optional component

-   Full-Text Search - is an optional component  
  
-   Data Quality Services - is an optional component  
  
    > [!NOTE]  
    >  In this release, selecting the **Data Quality Services** check box in setup does not install the Data Quality Services (DQS) server. You will have to perform additional steps post installation to install DQS server. For more information, see [Install Data Quality Services](../../data-quality-services/install-windows/install-data-quality-services.md).  
  
 The following additional features are options for many typical user scenarios:  
  
-   Data Quality Client  
  
-   [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]  
  
-   Connectivity components  
  
-   Programming models  
  
-   Management tools  
  
-   [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]  
  
-   Documentation components  
  
> [!NOTE]  
>  By default, sample databases and sample code are not installed as part of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup. To install sample databases and sample code, see [Microsoft SQL Server Samples](../../sample/microsoft-sql-server-samples.md). See older samples on [CodePlex](https://go.microsoft.com/fwlink/?LinkId=87843).  
  
## See also  
 [Editions and supported features of SQL Server 2017](~/sql-server/editions-and-components-of-sql-server-2017.md)   
 [Planning a SQL Server Installation](../../sql-server/install/planning-a-sql-server-installation.md)   
 [High Availability Solutions &#40;SQL Server&#41;](../../sql-server/failover-clusters/high-availability-solutions-sql-server.md)   
 [Upgrade SQL Server Using the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)  
  
  
