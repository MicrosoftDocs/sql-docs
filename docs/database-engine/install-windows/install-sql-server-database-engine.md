---
title: "Install SQL Server Database Engine"
description: Learn about features that can be installed when you select SQL Server Database Engine from Components to Install of the SQL Server Installation Wizard.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/26/2019
ms.service: sql
ms.subservice: install
ms.topic: conceptual
ms.custom: intro-installation
helpviewer_keywords:
  - "Database Engine [SQL Server], installing"
monikerRange: ">=sql-server-2016"
---
# Install SQL Server Database Engine

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

## Overview
The [!INCLUDE[ssDE](../../includes/ssde-md.md)] component of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is the core service for storing, processing, and securing data. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] provides controlled access and rapid transaction processing to meet the requirements of the most demanding data consuming applications in your enterprise.  
  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports up to 50 instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] on a single computer. To create a typical [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation, see [Install SQL Server from the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md).  
  
>[!IMPORTANT]
>For local installations, you must run Setup as an administrator. If you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share.  

## Features
The following features are installed when you select **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Database Engine** on the Components to Install page of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard:  
  
-   [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
-   [SQL Server Replication](../../relational-databases/replication/sql-server-replication.md) - is an optional component  

::: moniker range=">=sql-server-ver15"
-   [Machine Learning Services](../../machine-learning/install/sql-machine-learning-services-windows-install.md) (R and Python) and [Language Extensions](../..//language-extensions/install/windows-java.md) (Java) - is an optional component
::: moniker-end

::: moniker range=">=sql-server-2017 <=sql-server-2017"
-   [Machine Learning Services (In-Database)](../../machine-learning/install/sql-machine-learning-services-windows-install.md) (R and Python) - is an optional component
::: moniker-end

::: moniker range=">=sql-server-2016 <=sql-server-2016"
-   [R Services (In-Database)](../../machine-learning/install/sql-r-services-windows-install.md) - is an optional component
::: moniker-end

-   Full-Text Search - is an optional component  
  
-   Data Quality Services - is an optional component  
  
    > [!NOTE]  
    >  In this release, selecting the **Data Quality Services** check box in setup does not install the Data Quality Services (DQS) server. You will have to perform additional steps post installation to install DQS server. For more information, see [Install Data Quality Services](../../data-quality-services/install-windows/install-data-quality-services.md).  
    
- [PolyBase Query Service for External Data](../../relational-databases/polybase/polybase-guide.md) - is an optional component. Starting with SQL Server 2019, Java connector for HDFS data sources is also available.

  
 The following additional features are options for many typical user scenarios:  
  
-   Data Quality Client
-   [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]
-   Connectivity components
-   Programming models
-   Management tools
-   [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]
-   Documentation components  
  

> [!NOTE]  
>  By default, sample databases and sample code are not installed as part of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup. To install sample databases and sample code, see [Microsoft SQL Server Samples](../../samples/sql-samples-where-are.md). See older samples on [CodePlex](https://go.microsoft.com/fwlink/?LinkId=87843).  

  
## See also  
 [Editions and supported features of SQL Server 2017](~/sql-server/editions-and-components-of-sql-server-2017.md)   
 [Planning a SQL Server Installation](../../sql-server/install/planning-a-sql-server-installation.md)   
 [High Availability Solutions &#40;SQL Server&#41;](../sql-server-business-continuity-dr.md)   
 [Upgrade SQL Server Using the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)  
  
