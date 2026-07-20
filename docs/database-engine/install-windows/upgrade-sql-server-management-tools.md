---
title: "Upgrade SQL Server Management Tools"
description: This article describes support for upgrading SQL Server Management Tools and management components, such as SQL Server Agent.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/03/2025
ms.service: sql
ms.subservice: install
ms.topic: upgrade-and-migration-article
helpviewer_keywords:
  - "management tools, upgrading"
monikerRange: ">=sql-server-2017"
---
# Upgrade SQL Server Management Tools

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

[!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] supports upgrade from [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later versions. This article documents support and behavior for upgrading [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Management Tools and management components such as [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent, Database Mail, Maintenance Plans, XPStar, and XPWeb.

For local installations, you must run [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup as an administrator. If you run [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup from a remote share, you must use a domain account that has read and execute permissions on the remote share.

## Known upgrade issues

- Consider the following issues before you upgrade to [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)]:

- All TSX servers should be upgraded before the MSX server is upgraded. For more information about MSX/TSX in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Automated Administration Across an Enterprise](/ssms/agent/automated-administration-across-an-enterprise).

- All components in an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] must be upgraded at the same time. Version numbers of the [!INCLUDE [ssDE](../../includes/ssde-md.md)], [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)], and [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] components must be the same in an instance of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)].

- You can add components to an existing installation of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] at the time that you upgrade to [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)]. For more information, see [Upgrade SQL Server Using the Installation Wizard (Setup)](upgrade-sql-server-using-the-installation-wizard-setup.md).

- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Client Tools, such as [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)], the [!INCLUDE [ssDE](../../includes/ssde-md.md)] Tuning Advisor, sqlcmd, and osql aren't upgraded to [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)]. Instead, Client Tools run side-by-side with tools from previous [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] versions. [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] supports importing settings from earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Client Tools.

- Authentication from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is updated from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication to Windows Authentication during upgrade. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication isn't supported in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)].

- Data for jobs and alerts are preserved during upgrade to [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)].

- If SQLMail is being used in the instance to be upgraded, associated XPs are supported and enabled after the upgrade. Otherwise, they're off.

- Database Mail is upgraded with the [!INCLUDE [ssDE](../../includes/ssde-md.md)] component of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)]. By default, Database Mail is off after upgrade. Any schema updates should be reconciled with an update script after upgrade.

## Related content

- [Supported version and edition upgrades (SQL Server 2022)](supported-version-and-edition-upgrades-2022.md)
- [Backward Compatibility](/previous-versions/sql/sql-server-2016/cc280407(v=sql.130))
- [Upgrade SQL Server Using the Installation Wizard (Setup)](upgrade-sql-server-using-the-installation-wizard-setup.md)
