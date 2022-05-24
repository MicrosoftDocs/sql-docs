---
title: "SQL Server 2022 Release Notes | Microsoft Docs"
description: Find information about SQL Server 2019 (16.x) limitations, known issues, help resources, and other release notes.
ms.date: 05/24/2022
ms.prod: sql
ms.reviewer: ""
ms.technology: release-landing
ms.topic: "article"
ms.custom:
- event-tier1-build-2022
author: MikeRayMSFT
ms.author: mikeray
monikerRange: "= sql-server-ver16"
---
# [!INCLUDE[SQL Server 2022](../includes/sssql22-md.md)] release notes
[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

This article describes requirements, limitations and known issues for the [!INCLUDE[SQL Server 2022](../includes/sssql22-md.md)] community technology preview (CTP) 2.0.

Complete details about licensing are in `License Terms` folder on the installation media.

## Hardware and software requirements

This release has the same hardware and software requirements as [SQL Server 2019](install/hardware-and-software-requirements-for-installing-sql-server-2019.md), except as noted below:

- .NET Framework: 4.7.2. [Download](https://dotnet.microsoft.com/download/dotnet-framework/net472).

## Linux

SQL Server 2022 Preview on Linux images are coming soon.

## Azure Virtual Machine

SQL Server 2022 Preview Azure VM images are coming soon.

## Feature notes

This section identifies known issues you may experience with this product:

### Query Store

- **Issue and customer impact**: Query Store is not enabled by default.
- **Workaround**: To experience intelligent query processing features, enable Query Store.

### Memory grant feedback

- **Issue and customer impact**: : Immediately after you enable Query Store, SQL Server may return an error (access violation) if the Query Store is still starting up and you try to use memory grant feedback.
- **Workaround**: After you enable Query Store, wait for a period of time before you execute queries that are leveraging memory grant feedback. This happens most often with substantial concurrent workload.

## Build number

The CTP 2.0 build number for SQL Server 2022 Preview is `16.0.600.3`.

## Next steps

For related information about new features and capabilities in this version of SQL Server, see:

[What's new in [!INCLUDE[sql-server-2022](../includes/sssql22-md.md)]](what-s-new-in-sql-server-2022.md).
