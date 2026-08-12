---
title: "Install SQL Server Database Engine"
description: Learn about features that can be installed when you select SQL Server Database Engine from Components to Install of the SQL Server Installation Wizard.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/03/2025
ms.service: sql
ms.subservice: install
ms.topic: install-set-up-deploy
ms.custom:
  - intro-installation
helpviewer_keywords:
  - "Database Engine [SQL Server], installing"
monikerRange: ">=sql-server-2017"
---
# Install SQL Server Database Engine

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

## Overview

The [!INCLUDE [ssDE](../../includes/ssde-md.md)] component of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is the core service for storing, processing, and securing data. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] provides controlled access and rapid transaction processing to meet the requirements of the most demanding data consuming applications in your enterprise.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports up to 50 instances of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] on a single computer. To create a typical [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation, see [Install SQL Server from the Installation Wizard (Setup)](install-sql-server-from-the-installation-wizard-setup.md).

> [!IMPORTANT]  
> For local installations, you must run Setup as an administrator. If you install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share.

## Features

The following features are installed when you select **SQL Server Database Engine** on the Components to Install page of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard:

- [!INCLUDE [ssDE](../../includes/ssde-md.md)]

- [SQL Server Replication](../../relational-databases/replication/sql-server-replication.md) - is an optional component

::: moniker range=">=sql-server-ver15"

- [Machine Learning Services](../../machine-learning/install/sql-machine-learning-services-windows-install.md) (R and Python) and [Language Extensions](../../language-extensions/install/windows-java.md) (Java) - is an optional component
::: moniker-end

::: moniker range="=sql-server-2017 || =sql-server-ver15"

- [Machine Learning Services (In-Database)](../../machine-learning/install/sql-machine-learning-services-windows-install.md) (R and Python) - is an optional component
::: moniker-end

- Full-Text Search - is an optional component

- Data Quality Services - is an optional component

    > [!NOTE]  
    > In this release, selecting the **Data Quality Services** check box in setup doesn't install the Data Quality Services (DQS) server. You'll have to perform additional steps post installation to install DQS server. For more information, see [Install Data Quality Services](../../data-quality-services/install-windows/install-data-quality-services.md).

- [Data virtualization with PolyBase in SQL Server](../../relational-databases/polybase/overview.md) is an optional component. In [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)], a Java connector for HDFS data sources is also available.

The following additional features are options for many typical user scenarios:

- Data Quality Client
- [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)]
- Connectivity components
- Programming models
- Management tools
- [!INCLUDE [ssManStudio](../../includes/ssmanstudio-md.md)]
- Documentation components

> [!NOTE]  
> By default, sample databases and sample code aren't installed as part of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup. To install sample databases and sample code, see [SQL samples](../../samples/sql-samples-where-are.md). See older samples on [CodePlex](https://go.microsoft.com/fwlink/?LinkId=87843).

## Related content

- [Editions and supported features of SQL Server](../../sql-server/editions-and-components-of-sql-server-latest.md)
- [Plan a SQL Server installation](../../sql-server/install/planning-a-sql-server-installation.md)
- [Business continuity and database recovery - SQL Server](../sql-server-business-continuity-dr.md)
- [Upgrade SQL Server Using the Installation Wizard (Setup)](upgrade-sql-server-using-the-installation-wizard-setup.md)
