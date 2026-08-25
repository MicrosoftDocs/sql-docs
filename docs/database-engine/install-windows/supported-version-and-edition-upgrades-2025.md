---
title: "Supported Version and Edition Upgrades (SQL Server 2025)"
description: The supported version and edition upgrades for SQL Server 2025.
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/18/2025
ms.service: sql
ms.subservice: install
ms.topic: upgrade-and-migration-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "components [SQL Server], adding to existing installations"
  - "versions [SQL Server], upgrading"
  - "upgrading SQL Server, upgrades supported"
  - "cross-language support"
monikerRange: ">=sql-server-2017"
---
# Supported version and edition upgrades (SQL Server 2025)

[!INCLUDE [SQL Server - Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

This article lists the supported upgrade paths from the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] versions, and the supported edition upgrades for [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

You can upgrade from:

- [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] SP3 or later
- [!INCLUDE [ssSQL16](../../includes/sssql16-md.md)] SP3 or later
- [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)]
- [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]
- [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]

For older versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], you can also [Migrate to SQL Server 2025](#migrate-to-sql-server-2025).

## Pre-upgrade checklist

- Before you upgrade from one edition of [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] to another, verify that the functionality you're currently using is supported in the edition to which you're moving. [!INCLUDE [editions-latest](../../includes/editions-2025.md)]

- Verify supported hardware and software, including the supported operating system. For more information, see [Hardware and software requirements for SQL Server 2025](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2025.md).

- Before upgrading [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], enable Windows Authentication for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent and verify the default configuration, that the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account is a member of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] sysadmin group.

- Upgrade is blocked if there's a pending restart.

- Upgrade is blocked if the Windows Installer service isn't running.

## Unsupported scenarios

- Cross-version instances of [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] aren't supported. Version numbers of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] components must be the same in an instance of [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

- [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] is only available for 64-bit platforms. Cross-platform upgrade isn't supported. You can't upgrade a 32-bit instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to native 64-bit using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup. However, you can back up or detach databases from a 32-bit instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and then restore or attach them to a new instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (64-bit), if the databases aren't published in replication. You must re-create any logins and other user objects in `master`, `msdb`, and `model` system databases.

- You can't add new features during the upgrade of your existing instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. After you upgrade an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], you can add features by using the [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] Setup. For more information, see [Add Features to an Instance of SQL Server (Setup)](add-features-to-an-instance-of-sql-server-setup.md).

## Upgrades from earlier versions to SQL Server 2025

[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] supports upgrade from the following versions of SQL Server:

- [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] SP3 or later
- [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] SP3 or later
- [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)]
- [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]
- [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]

The following table lists the supported upgrade scenarios from earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

> [!NOTE]  
> Web edition isn't available in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions. Express edition includes all the functionality that was available in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Express edition with Advanced Services.

| Upgrade from | Supported upgrade path |
| --- | --- |
| **[!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] Service Pack 3** | **[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]** |
| Enterprise edition | Enterprise edition |
| Developer edition | Enterprise Developer edition<br />Standard Developer edition<br />Enterprise edition |
| Standard edition | Enterprise edition<br />Standard edition |
| Web edition | Enterprise edition<br />Standard edition |
| Express edition | Enterprise edition<br />Standard edition<br />Express edition |
| Business Intelligence edition | Enterprise edition |
| Evaluation edition | Evaluation edition<br />Enterprise Developer edition<br />Standard Developer edition<br />Enterprise edition<br />Standard edition |
| **[!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] Service Pack 3** | **[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]** |
| Enterprise edition | Enterprise edition |
| Developer edition | Enterprise Developer edition<br />Standard Developer edition<br />Enterprise edition |
| Standard edition | Enterprise edition<br />Standard edition |
| Web edition | Enterprise edition<br />Standard edition |
| Express edition | Enterprise edition<br />Standard edition<br />Express edition |
| Business Intelligence edition | Enterprise edition |
| Evaluation edition | Evaluation edition<br />Enterprise Developer edition<br />Standard Developer edition<br />Enterprise edition<br />Standard edition |
| **[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)]** | **[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]** |
| Enterprise edition | Enterprise edition |
| Developer edition | Enterprise Developer edition<br />Standard Developer edition<br />Enterprise edition |
| Standard edition | Enterprise edition<br />Standard edition |
| Web edition | Enterprise edition<br />Standard edition |
| Express edition | Enterprise edition<br />Standard edition<br />Express edition |
| Business Intelligence edition | Enterprise edition |
| Evaluation edition | Evaluation edition<br />Enterprise Developer edition<br />Standard Developer edition<br />Enterprise edition<br />Standard edition |
| **[!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]** | **[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]** |
| Enterprise edition | Enterprise edition |
| Developer edition | Enterprise Developer edition<br />Standard Developer edition<br />Enterprise edition |
| Standard edition | Enterprise edition<br />Standard edition |
| Web edition | Enterprise edition<br />Standard edition |
| Express edition | Enterprise edition<br />Standard edition<br />Express edition |
| Business Intelligence edition | Enterprise edition |
| Evaluation edition | Evaluation edition<br />Enterprise Developer edition<br />Standard Developer edition<br />Enterprise edition<br />Standard edition |
| **[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]** | **[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]** |
| Enterprise edition | Enterprise edition |
| Developer edition | Enterprise Developer edition<br />Standard Developer edition<br />Enterprise edition |
| Standard edition | Enterprise edition<br />Standard edition |
| Web edition | Enterprise edition<br />Standard edition |
| Express edition | Enterprise edition<br />Standard edition<br />Express edition |
| Business Intelligence edition | Enterprise edition |
| Evaluation edition | Evaluation edition<br />Enterprise Developer edition<br />Standard Developer edition<br />Enterprise edition<br />Standard edition |
| Release candidate <sup>1</sup> | Enterprise edition |
| Developer edition | Enterprise edition |

<sup>1</sup> Microsoft support to upgrade from release candidate software is specifically for customers who participated in the Early Adopter Program.

## Migrate to SQL Server 2025

You can migrate databases from older versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], as long as the source database compatibility level is `90` or higher. Databases with a compatibility level of `90` (for example, on [!INCLUDE [ssversion2005-md](../../includes/ssversion2005-md.md)]), are automatically upgraded to a compatibility level of `100` when migrated to [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]. If the compatibility level on the source database is `100` or higher, it's unchanged on [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

For information, see [Upgrade SQL Server to the latest version](../../sql-server/migrate/guides/sql-server-to-sql-server-upgrade-guide.md).

The following tips and tools can help you plan and implement your migration.

- **Migration tools.** Migration is supported through the [SQL Server migration component in SQL Server Management Studio](/ssms/migrate-sql-server-component).

- **Backup and restore.** A backup taken on [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later, can be restored to [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] without changing its compatibility level, as long as the database backup has a compatibility level of `100` or higher.

  Databases with a compatibility level of `90`, including backups taken on [!INCLUDE [ssversion2005-md](../../includes/ssversion2005-md.md)], are automatically upgraded to a compatibility level of `100` when restored to [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

- **Log shipping.** Log shipping is supported if the primary is running [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] SP3 or later, or [!INCLUDE [sql2008r2-md](../../includes/sql2008r2-md.md)] SP2 or later, and the secondary is running [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

  > [!WARNING]  
  > If an automatic or manual failover happens and the [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] instance becomes primary, [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] or [!INCLUDE [sql2008r2-md](../../includes/sql2008r2-md.md)] instance becomes secondary and can't receive changes from primary.

- Bulk load: Tables can be bulk copied from [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] or [!INCLUDE [sql2008r2-md](../../includes/sql2008r2-md.md)] to [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

## SQL Server 2025 edition upgrade

The following table lists the supported edition upgrade scenarios in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

For step-by-step instructions on how to perform an edition upgrade, see [Upgrade to a different edition of SQL Server (Setup)](upgrade-downgrade-sql-server-edition-setup.md).

| Upgrade from | Upgrade to |
| --- | --- |
| Enterprise (Server+CAL and Core) | Enterprise edition |
| Evaluation Enterprise | Enterprise (Server+CAL or Core License)<br />Standard<br />Developer<br /><br />Upgrading from Evaluation (a free edition) to any of the paid editions is supported for stand-alone installations, but isn't supported for clustered installations. This limitation doesn't apply to stand-alone instances installed on a Windows Server failover cluster participating in an availability group. |
| Standard | Enterprise (Server+CAL or Core License) |
| Developer | Enterprise (Server+CAL or Core License)<br />Standard |
| Express | Enterprise (Server+CAL or Core License)<br />Developer<br />Standard |

Additionally you can also perform an edition upgrade between [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] Enterprise (Server+CAL license) and [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] Enterprise (Core License):

| Edition upgrade from | Edition upgrade to |
| --- | --- |
| Enterprise (Server+CAL License) <sup>1</sup> | Enterprise (Core License) |
| Enterprise (Core License) | Enterprise (Server+CAL License) |

<sup>1</sup> Changing the edition of a clustered instance of [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] is limited. The following scenarios aren't supported for [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] failover clusters:

- Enterprise to Enterprise Developer, Standard Developer, Standard, or Evaluation.
- Enterprise Developer to Standard or Evaluation.
- Standard Developer to Standard or Evaluation.
- Standard to Evaluation.
- Evaluation to Standard.

## Related content

- [Editions and supported features of SQL Server](../../sql-server/editions-and-components-of-sql-server-latest.md)
- [Hardware and software requirements for SQL Server 2025](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2025.md)
- [Upgrade SQL Server](upgrade-sql-server.md)
- [Upgrade the Database Engine](upgrade-database-engine.md)
- [In-place change of a SQL Server edition (Setup)](upgrade-downgrade-sql-server-edition-setup.md)
