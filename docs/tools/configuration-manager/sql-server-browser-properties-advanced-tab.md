---
title: "SQL Server Browser Properties (Advanced Tab)"
description: Learn about the options on the Advanced tab in the SQL Server Browser Properties dialog box, such as the dump directory and the instance ID.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/15/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: ui-reference
ms.collection:
  - data-tools
monikerRange: ">=sql-server-2017"
---
# SQL Server Browser Properties (Advanced tab)

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser program runs as a service on the server. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser listens for incoming requests for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] resources and provides information about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances installed on the computer.

## Options

#### Clustered

Indicates if this service is installed as a resource of a clustered server.

#### Customer Feedback Reporting

Indicates whether Service Quality Monitoring is enabled on this service. For more information on Customer Feedback Reporting, see [Configure usage and diagnostic data collection for SQL Server (CEIP)](../../sql-server/usage-and-diagnostic-data-configuration-for-sql-server.md).

#### Dump Directory

The location where memory dumps are placed in case of an error.

#### Error Reporting

When set to **Yes**, the Dr. Watson program sends information to either [!INCLUDE [msCoName](../../includes/msconame-md.md)] or your error server if a serious failure occurs. For more information on Error Reporting, see [Configure usage and diagnostic data collection for SQL Server (CEIP)](../../sql-server/usage-and-diagnostic-data-configuration-for-sql-server.md).

#### Instance ID

Indicates the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance that used this [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent instance. The default instance is `MSSQL10_50.MSSQLSERVER`.

## Related content

- [SQL Server Browser service (Database Engine and SSAS)](../../database-engine/configure-windows/sql-server-browser-service-database-engine-and-ssas.md)
