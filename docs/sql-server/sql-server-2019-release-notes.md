---
title: "SQL Server 2019 Release Notes | Microsoft Docs"
description: Find information about SQL Server 2019 (15.x) limitations, known issues, help resources, and other release notes.
ms.date: 11/04/2019
ms.service: sql
ms.reviewer: ""
ms.subservice: release-landing
ms.topic: "article"
ms.assetid: 13942af8-5a40-4cef-80f5-918386767a47
author: MikeRayMSFT
ms.author: mikeray
monikerRange: "= sql-server-ver15"
---
# [!INCLUDE[SQL Server 2019](../includes/sssql19-md.md)] release notes
[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

This article describes limitations and known issues for the [!INCLUDE[SQL Server 2019](../includes/sssql19-md.md)]. For related information, see:

> [What's New in [!INCLUDE[SQL Server 2019](../includes/sssql19-md.md)]](../sql-server/what-s-new-in-sql-server-ver15.md)

## [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)]

[!INCLUDE[SQL Server 2019](../includes/sssql19-md.md)] is the latest public release of [!INCLUDE[SQL Server 2019](../includes/ssnoversion-md.md)].

Complete details about licensing are in `License Terms` folder on the installation media.

## Documentation

- **Issue and customer impact**: [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] documentation can be filtered by version. Use the control at the top left of each documentation page to filter for your requirements.

## Build number

The RTM build number for SQL Server 2019 is `15.0.2000.5`.

[!INCLUDE [sql-server-servicing-updates-version-15](../includes/sql-server-servicing-updates-version-15.md)]

## SQL Server installation may fail if SSMS 18.x is installed

- **Issue and customer impact**: [!INCLUDE[SQL Server 2019](../includes/sssql19-md.md)] installation fails when the following installations happen in this order:
  1. SQL Server Management Studio (SSMS) version 18.0, 18.1, 18.2, or 18.3 is installed on the server.
  1. [!INCLUDE[SQL Server 2019](../includes/sssql19-md.md)] installation is attempted from removable media. For example, the installation media is a DVD.

- **Workaround**:
  1. Uninstall any version of SSMS older than SSMS 18.3.1.
  1. Install a newer version of SSMS (18.3.1 or later). For the latest version, see [Download SSMS](../ssms/download-sql-server-management-studio-ssms.md).
  1. Install [!INCLUDE[SQL Server 2019](../includes/sssql19-md.md)] normally.

- **Applies to**: [!INCLUDE[SQL Server 2019](../includes/sssql19-md.md)]

## UTF-8 collations

- **Issue and customer impact**: UTF-8 enabled collations cannot be used with the following features:
  - [In-Memory OLTP tables](../relational-databases/in-memory-oltp/introduction-to-memory-optimized-tables.md)
  - [Always Encrypted with Secure Enclaves](../relational-databases/security/encryption/always-encrypted-enclaves.md) (When not using Secure Enclaves, [Always Encrypted](../relational-databases/security/encryption/always-encrypted-database-engine.md) can use UTF-8)

  > [!WARNING]
  > Creating a [bacpac](../relational-databases/data-tier-applications/data-tier-applications.md#bacpac) of a database containing table columns defined as [CHAR or VARCHAR](../t-sql/data-types/char-and-varchar-transact-sql.md) that use more than 4000 bytes will fail.
  
  > [!NOTE]
  > There is currently no UI support to choose UTF-8 enabled collations in Azure Data Studio or SQL Server Data Tools (SSDT). The latest [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] (SSMS) version 18 supports choice of UTF-8 enabled collations in the UI.

- **Applies to**: [!INCLUDE[SQL Server 2019](../includes/sssql19-md.md)] RTM

## Master Data Service notification email contains broken link

- **Issue and customer impact**: The notification email from Master Data Services (MDS) contains a broken link. The link navigates to a page that returns an error like the following message:

   `The view 'Index' or its master was not found or no view engine supports the searched locations.`

- **Workaround**: Open the MDS portal and go to the resource manually.

- **Applies to**: [!INCLUDE[SQL Server 2019](../includes/sssql19-md.md)] RTM

## See also

- [Hardware and Software Requirements for Installing SQL Server](./install/hardware-and-software-requirements-for-installing-sql-server-2019.md)

## Machine Learning Services

For issues in SQL Server Machine Learning Services, see [Known issues in SQL Server Machine Learning Services](../machine-learning/troubleshooting/known-issues-for-sql-server-machine-learning-services.md).

[!INCLUDE[get-help-options-msft-only](../includes/paragraph-content/get-help-options.md)]