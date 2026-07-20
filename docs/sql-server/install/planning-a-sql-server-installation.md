---
title: Plan a SQL Server Installation
description: This article helps you plan to install SQL Server. It includes links to resources needed for SQL Server installation.
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/18/2025
ms.service: sql
ms.subservice: install
ms.topic: quickstart
ms.custom:
  - intro-quickstart
  - ignite-2025
helpviewer_keywords:
  - "installing SQL Server, planning"
---
# Plan a SQL Server installation

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

To install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], follow these steps:

- Review installation requirements, system configuration checks, and security considerations for a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation.

- Run [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup to install or upgrade to a later version. Before upgrading, review [Upgrade SQL Server](../../database-engine/install-windows/upgrade-sql-server.md).

- Use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] utilities to configure [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

Regardless of the installation method, you must confirm acceptance of the software license terms as an individual, or on behalf of an entity, unless your use of the software is governed by a separate agreement such as a [!INCLUDE [msCoName](../../includes/msconame-md.md)] volume licensing agreement or a third-party agreement with an independent software vendor (ISV) or original equipment manufacturer (OEM).

The license terms are displayed for review and acceptance in the Setup user interface. Unattended installations (using the `/Q` or `/QS` parameters) must include the `/IAcceptSQLServerLicenseTerms` parameter. Download and review the license terms separately at [Microsoft SQL Server License Terms and Information](https://www.microsoft.com/licensing/product-licensing/sql-server). For volume licensing terms, see [Licensing Terms and Documentation](https://www.microsoft.com/licensing/docs?redirected=true&Mode=3&DocumentTypeId=53). For older versions of SQL Server, see [Microsoft Software License Terms](https://www.microsoft.com/useterms).

> [!NOTE]  
> Depending on how you received the software (for example, through [!INCLUDE [msCoName](../../includes/msconame-md.md)] volume licensing), your use of the software might be subject to additional terms and conditions.

## Considerations

[!INCLUDE [upgrade-warning](../../database-engine/install-windows/includes/upgrade-warning.md)]

| Area of consideration | Description |
| --- | --- |
| [What's new in SQL Server installation](what-s-new-in-sql-server-installation.md) | This article describes the details about the new or improved features of installation in this version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| Hardware and software requirements to run an instance of SQL Server | The following articles list the minimum hardware and software requirements to install and run an instance of SQL Server:<br /><br />- [SQL Server 2017](hardware-and-software-requirements-for-installing-sql-server-2017.md)<br />- [SQL Server 2019](hardware-and-software-requirements-for-installing-sql-server-2019.md)<br />- [SQL Server 2022](hardware-and-software-requirements-for-installing-sql-server-2022.md)<br />- [SQL Server 2025](hardware-and-software-requirements-for-installing-sql-server-2025.md) |
| [Security considerations for a SQL Server installation](security-considerations-for-a-sql-server-installation.md) | This article describes some security best practices that you should consider both before and after you install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md) | This article describes the default configuration of services in this release of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and configuration options for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services that you can set during and after [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation. |
| [Network protocols and network libraries](network-protocols-and-network-libraries.md) | This article describes the default configuration of network protocols in this release of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and the configuration options available. |
| [Work with multiple versions and instances of SQL Server](work-with-multiple-versions-and-instances-of-sql-server.md) | This article describes the considerations for installing multiple versions and instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| [Local language versions in SQL Server](local-language-versions-in-sql-server.md) | This article describes about the localized versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |

## Related tasks

| Article | Description |
| --- | --- |
| [SQL Server installation guide](../../database-engine/install-windows/install-sql-server.md) | This section provides an overview of different installation options we have for installing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| [Install SQL Server Business Intelligence Features](install-sql-server-business-intelligence-features.md) | This section of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup documentation explains how to install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] features that are part of the Microsoft BI platform. |
| [Upgrade SQL Server](../../database-engine/install-windows/upgrade-sql-server.md) | The section provides an overview of upgrading from a previous version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| [Uninstall SQL Server](uninstall-sql-server.md) | Refer this section to uninstall an existing instance of [!INCLUDE [ssNoversion](../../includes/ssnoversion-md.md)] completely, and prepare the system so that you can reinstall [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| [Install a SQL Server failover cluster](../failover-clusters/install/sql-server-failover-cluster-installation.md) | This section of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup documentation explains how to install, and configure [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster. |

## Related content

- [Install, configure, or uninstall SQL Server on Windows from the command prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md)
- [Business continuity and database recovery - SQL Server](../../database-engine/sql-server-business-continuity-dr.md)
- [Before Installing Failover Clustering](../failover-clusters/install/before-installing-failover-clustering.md)
- [Upgrade SQL Server Using the Installation Wizard (Setup)](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)
- [How to license SQL Server](https://www.microsoft.com/sql-server/sql-server-2022-pricing)
- [SQL Server Licensing Resources and Documents](https://www.microsoft.com/licensing/docs/view/SQL-Server)
