---
title: "SQL Server 2019 Release Notes | Microsoft Docs"
ms.date: 08/21/2019
ms.prod: sql
ms.reviewer: ""
ms.technology: release-landing
ms.topic: "article"
ms.assetid: 13942af8-5a40-4cef-80f5-918386767a47
author: MikeRayMSFT
ms.author: mikeray
monikerRange: "= sql-server-ver15 || = sqlallproducts-allversions"
---
# [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] release notes
[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article describes limitations and known issues for the [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)]. For related information, see:

>[What's New in [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)]](../sql-server/what-s-new-in-sql-server-ver15.md)

## [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)]

[!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] is the latest public release of [!INCLUDE[SQL Server 2019](../includes/ssnoversion-md.md)].

Complete details about licensing are in `License Terms` folder on the installation media.

## Documentation

- **Issue and customer impact**: [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] documentation can be filtered by version. Use the control at the top left of each documentation page to filter for your requirements.

## Build number

The latest build number for SQL Server 2019 is `15.0.1900.25`.

## Installation Wizard may wait between EULA pages

- **Issue and customer impact**: During installation with Installation Wizard, the process may wait an excessive amount of time between the end user license agreement (EULA) for R Services and the EULA for Python.

- **Workaround**: Wait for the Installation Wizard to proceed. The time to wait may exceed 30 minutes.

- **Applies to**: [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] RTM

## UTF-8 collations

- **Issue and customer impact**: UTF-8 enabled collations cannot be used with in-memory OLTP.

  > [!Note]
  > There is currently no UI support to choose UTF-8 enabled collations in Azure Data Studio or SQL Server Data Tools (SSDT). The latest [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] (SSMS) version 18 supports choice of UTF-8 enabled collations in the UI.

- **Applies to**: [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] RTM

## Master Data Service notification email contains broken link

- **Issue and customer impact**: The notification email from Master Data Services (MDS) contains a broken link. The link navigates to a page that returns an error like the following message:

   `The view 'Index' or its master was not found or no view engine supports the searched locations.`

- **Workaround**: Open the MDS portal and go to the resource manually.

- **Applies to**: [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] RTM

## See also

- [Hardware and Software Requirements for Installing SQL Server](../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-ver15.md)

[!INCLUDE[get-help-options-msft-only](../includes/paragraph-content/get-help-options.md)]

![MS_Logo_X-Small](../sql-server/media/ms-logo-x-small.png)
