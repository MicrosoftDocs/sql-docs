---
title: "Supported version and edition upgrades (SQL Server 2022)"
description: The supported version and edition upgrades for SQL Server 2022.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/25/2022
ms.service: sql
ms.subservice: install
ms.topic: conceptual
ms.custom: event-tier1-build-2022
helpviewer_keywords:
  - "components [SQL Server], adding to existing installations"
  - "versions [SQL Server], upgrading"
  - "upgrading SQL Server, upgrades supported"
  - "cross-language support"
monikerRange: ">=sql-server-2017"
---
# Supported version and edition upgrades (SQL Server 2022)

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

You can upgrade from [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)], and [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]. This article lists the supported upgrade paths from these [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] versions, and the supported edition upgrades for [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)].

## Pre-upgrade checklist

- Before upgrading from one edition of [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] to another, verify that the functionality you're currently using is supported in the edition to which you're moving.
- Verify supported [hardware and software](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2019.md).
- Before upgrading [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], enable Windows Authentication for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent and verify the default configuration, that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account is a member of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sysadmin group.
- To upgrade to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], you must be running a supported operating system. For more information, see [Hardware and Software Requirements for Installing SQL Server](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2019.md).
- Upgrade will be blocked if there's a pending restart.
- Upgrade will be blocked if the Windows Installer service isn't running.

## Unsupported scenarios

- Cross-version instances of [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] aren't supported. Version numbers of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] components must be the same in an instance of [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)].

- [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] is only available for 64-bit platforms. Cross-platform upgrade isn't supported. You can't upgrade a 32-bit instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to native 64-bit using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup. However, you can back up or detach databases from a 32-bit instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and then restore or attach them to a new instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (64-bit) if the databases aren't published in replication. You must re-create any logins and other user objects in `master`, `msdb`, and `model` system databases.

- You can't add new features during the upgrade of your existing instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. After you upgrade an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], you can add features by using the [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] Setup. For more information, see [Add Features to an Instance of SQL Server &#40;Setup&#41;](./add-features-to-an-instance-of-sql-server-setup.md).

## Upgrades from earlier versions to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]

[!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] supports upgrade from the following versions of SQL Server:

- [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP4 or later
- [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP3 or later
- [!INCLUDE[ssSQL16](../../includes/sssql16-md.md)] SP3 or later
- [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)]
- [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]

Specific version and edition upgrade paths aren't available during community technology preview releases.

## Migrate to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]

You can migrate databases from older versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], as long as the source database compatibility level is `90` or higher. Databases with a compatibility level of `90` (for example, on [!INCLUDE [ssversion2005-md](../../includes/ssversion2005-md.md)]), are automatically upgraded to a compatibility level of `100` when migrated to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]. If the compatibility level on the source database is `100` or higher, it will be unchanged on [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)].

For information, see [Azure Database Migration Guide](https://datamigration.microsoft.com/scenario/sql-to-sqlserver).

The following tips and tools can help you plan and implement your migration.

- **Migration tools.** Migration is supported through [Data Migration Assistant (DMA)](../../dma/dma-overview.md).

- **Backup and restore.** A backup taken on [!INCLUDE [sskatmai-md](../../includes/sskatmai-md.md)] and later, can be restored to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] without changing its compatibility level, as long as the database backup has a compatibility level of `100` or higher.

  Databases with a compatibility level of `90`, including backups taken on [!INCLUDE [ssversion2005-md](../../includes/ssversion2005-md.md)], are automatically upgraded to a compatibility level of `100` when restored to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)].

- **Log shipping.** Log shipping is supported if the primary is running [!INCLUDE [sskatmai-md](../../includes/sskatmai-md.md)] SP3 or later, or [!INCLUDE [sskilimanjaro-md](../../includes/sskilimanjaro-md.md)] SP2 or later, and the secondary is running [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)].

   > [!WARNING]
   > If an automatic or manual failover happens and the [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] instance becomes primary, [!INCLUDE [sskatmai-md](../../includes/sskatmai-md.md)] or [!INCLUDE [sskilimanjaro-md](../../includes/sskilimanjaro-md.md)] instance becomes secondary and cannot receive changes from primary.

- Bulk load: Tables can be bulk copied from [!INCLUDE [sskatmai-md](../../includes/sskatmai-md.md)] or [!INCLUDE [sskilimanjaro-md](../../includes/sskilimanjaro-md.md)] to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)].

## See also

- [Supported version and edition upgrades (SQL Server 2019)](supported-version-and-edition-upgrades-2019.md)
- [Hardware and software requirements for installing SQL Server](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2019.md)

## Next steps

- [Upgrade SQL Server](../../database-engine/install-windows/upgrade-sql-server.md)
- [Upgrade Database Engine](upgrade-database-engine.md)
- [Upgrade to a Different Edition of SQL Server (Setup)](upgrade-to-a-different-edition-of-sql-server-setup.md)
