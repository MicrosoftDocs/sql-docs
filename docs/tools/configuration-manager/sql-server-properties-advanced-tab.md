---
title: "SQL Server Properties (Advanced Tab)"
description: Learn about the options on the Advanced tab in the SQL Server Properties dialog box, such as the data path, the instance ID, and custom properties.
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
# SQL Server Properties (Advanced tab)

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

The following properties appear on the **Advanced** tab by default. If custom properties are defined, they also appear on this tab, with their values.

## Options

#### Clustered

Indicates if this service is installed as a resource of a clustered server.

#### Customer Feedback Reporting

Indicates whether Service Quality Monitoring is enabled on this service. For more information, see [Configure usage and diagnostic data collection for SQL Server (CEIP)](../../sql-server/usage-and-diagnostic-data-configuration-for-sql-server.md).

#### Data Path

Displays the path to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] binaries for this installation of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

#### Dump Directory

Displays the location where memory dumps are placed in case of an error.

#### Error Reporting

When set to **Yes**, the Dr. Watson program sends information to either [!INCLUDE [msCoName](../../includes/msconame-md.md)] or your error server if a serious failure occurs. For more information on Error Reporting, see [Configure usage and diagnostic data collection for SQL Server (CEIP)](../../sql-server/usage-and-diagnostic-data-configuration-for-sql-server.md).

To change this value, in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Object Explorer, right-click your server, select **Properties**, and then select the **Misc. Server Settings** page. The options are presented in the **Information Reporting** area.

#### File Version

Displays the version of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] executable.

#### Install Path

Displays the path to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] binaries for this installation of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

#### Instance ID

Indicates the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance that used this service.

#### Language

Displays the default language for server messages.

#### Lock Pages In Memory

**Applies to**: [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions.

Grants the [Lock pages in memory](../../database-engine/configure-windows/enable-the-lock-pages-in-memory-option-windows.md) privilege to the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] service account.

#### Registry Root

Displays the location of the registry keys used by this application.

#### Service Pack Level

Displays the service pack level of this instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

#### SKU Name

Displays the product stock keeping unit (SKU), sometimes called the product edition.

#### Startup Parameters

Lists any startup parameters that are used by this instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Parameters are separated by semi-colons. The default parameters include the paths to the data file for the `master` database (`master.mdf`), the log file for the `master` database (`mastlog.ldf`), and the error log file.

For the syntax of startup parameters, see [Database Engine Service startup options](../../database-engine/configure-windows/database-engine-service-startup-options.md).

#### Stock Keeping Unit

Displays the product stock keeping unit (SKU) number.

#### Version

Displays the version number of this instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

#### Virtual Server Name

**Virtual Server Name** when [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is installed on a clustered server.
