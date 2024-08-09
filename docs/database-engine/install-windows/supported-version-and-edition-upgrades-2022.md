---
title: "Supported version and edition upgrades (SQL Server 2022)"
description: The supported version and edition upgrades for SQL Server 2022.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/22/2024
ms.service: sql
ms.subservice: install
ms.topic: conceptual
helpviewer_keywords:
  - "components [SQL Server], adding to existing installations"
  - "versions [SQL Server], upgrading"
  - "upgrading SQL Server, upgrades supported"
  - "cross-language support"
monikerRange: ">=sql-server-2017"
---
# Supported version and edition upgrades (SQL Server 2022)

[!INCLUDE [SQL Server - Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

This article lists the supported upgrade paths from the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] versions, and the supported edition upgrades for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

You can upgrade from:

- [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP4 or later
- [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] SP3 or later
- [!INCLUDE [ssSQL16](../../includes/sssql16-md.md)] SP3 or later
- [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)]
- [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]

For older versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], you can also [Migrate to SQL Server 2022](#migrate-to-sql-server-2022).

## Pre-upgrade checklist

- Before you upgrade from one edition of [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] to another, verify that the functionality you're currently using is supported in the edition to which you're moving. For more information, see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).

- Verify supported hardware and software, including the supported operating system. For more information, see [SQL Server 2022: Hardware and software requirements](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2022.md).

- Before upgrading [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], enable Windows Authentication for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent and verify the default configuration, that the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account is a member of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] sysadmin group.

- Upgrade is blocked if there's a pending restart.

- Upgrade is blocked if the Windows Installer service isn't running.

## Unsupported scenarios

- Cross-version instances of [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] aren't supported. Version numbers of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] components must be the same in an instance of [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

- [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] is only available for 64-bit platforms. Cross-platform upgrade isn't supported. You can't upgrade a 32-bit instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to native 64-bit using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup. However, you can back up or detach databases from a 32-bit instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and then restore or attach them to a new instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (64-bit), if the databases aren't published in replication. You must re-create any logins and other user objects in `master`, `msdb`, and `model` system databases.

- You can't add new features during the upgrade of your existing instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. After you upgrade an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], you can add features by using the [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Setup. For more information, see [Add Features to an Instance of SQL Server (Setup)](add-features-to-an-instance-of-sql-server-setup.md).

## Upgrades from earlier versions to SQL Server 2022

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] supports upgrade from the following versions of SQL Server:

- [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP4 or later
- [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] SP3 or later
- [!INCLUDE [ssSQL16](../../includes/sssql16-md.md)] SP3 or later
- [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)]
- [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]

The following table lists the supported upgrade scenarios from earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

| Upgrade from | Supported upgrade path |
| :--- | :--- |
| [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP4 Enterprise | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br />|
| [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP4 Developer | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Developer<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br />|
| [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP4 Standard | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard |
| [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP4 Web | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web |
| [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP4 Express | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Express |
| [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP4 Business Intelligence | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br />|
| [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP4 Evaluation | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Evaluation<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Developer |
| [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] SP3 Enterprise | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br />|
| [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] SP3 Developer | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Developer<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br />|
| [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] SP3 Standard | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard |
| [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] SP3 Web | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web |
| [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] SP3 Express | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Express<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Developer |
| [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] SP3 Business Intelligence | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br />|
| [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] SP3 Evaluation | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Evaluation<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Developer |
| [!INCLUDE [ssSQL16](../../includes/sssql16-md.md)] SP3 Enterprise | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br />|
| [!INCLUDE [ssSQL16](../../includes/sssql16-md.md)] SP3 Developer | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Developer<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br />|
| [!INCLUDE [ssSQL16](../../includes/sssql16-md.md)] SP3 Standard | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard |
| [!INCLUDE [ssSQL16](../../includes/sssql16-md.md)] SP3 Web | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web |
| [!INCLUDE [ssSQL16](../../includes/sssql16-md.md)] SP3 Express | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Express<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Developer |
| [!INCLUDE [ssSQL16](../../includes/sssql16-md.md)] SP3 Business Intelligence | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br />|
| [!INCLUDE [ssSQL16](../../includes/sssql16-md.md)] SP3 Evaluation | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Evaluation<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Developer |
| [!INCLUDE [sssql17](../../includes/sssql17-md.md)] Enterprise | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br />|
| [!INCLUDE [sssql17](../../includes/sssql17-md.md)] Developer | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Developer<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br />|
| [!INCLUDE [sssql17](../../includes/sssql17-md.md)] Standard | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard |
| [!INCLUDE [sssql17](../../includes/sssql17-md.md)] Web | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web |
| [!INCLUDE [sssql17](../../includes/sssql17-md.md)] Express | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Express<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Developer |
| [!INCLUDE [sssql17](../../includes/sssql17-md.md)] Business Intelligence | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br />|
| [!INCLUDE [sssql17](../../includes/sssql17-md.md)] Evaluation | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Evaluation<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Developer |
| [!INCLUDE [sssql19](../../includes/sssql19-md.md)] Enterprise | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br />|
| [!INCLUDE [sssql19](../../includes/sssql19-md.md)] Developer | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Developer<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br />|
| [!INCLUDE [sssql19](../../includes/sssql19-md.md)] Standard | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard |
| [!INCLUDE [sssql19](../../includes/sssql19-md.md)] Web | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web |
| [!INCLUDE [sssql19](../../includes/sssql19-md.md)] Express | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Express<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Developer |
| [!INCLUDE [sssql19](../../includes/sssql19-md.md)] Business Intelligence | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br />|
| [!INCLUDE [sssql19](../../includes/sssql19-md.md)] Evaluation | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Evaluation<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Developer |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] release candidate <sup>1</sup> | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise |
| [!INCLUDE [sssql22_md](../../includes/sssql22-md.md)] Developer | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise |

<sup>1</sup> Microsoft support to upgrade from release candidate software is specifically for customers who participated in the Early Adopter Program.

## Migrate to SQL Server 2022

You can migrate databases from older versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], as long as the source database compatibility level is `90` or higher. Databases with a compatibility level of `90` (for example, on [!INCLUDE [ssversion2005-md](../../includes/ssversion2005-md.md)]), are automatically upgraded to a compatibility level of `100` when migrated to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. If the compatibility level on the source database is `100` or higher, it's unchanged on [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

For information, see [Upgrade SQL Server to the latest version](../../sql-server/migrate/guides/sql-server-to-sql-server-upgrade-guide.md).

The following tips and tools can help you plan and implement your migration.

- **Migration tools.** Migration is supported through [Data Migration Assistant (DMA)](../../dma/dma-overview.md).

- **Backup and restore.** A backup taken on [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later, can be restored to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] without changing its compatibility level, as long as the database backup has a compatibility level of `100` or higher.

  Databases with a compatibility level of `90`, including backups taken on [!INCLUDE [ssversion2005-md](../../includes/ssversion2005-md.md)], are automatically upgraded to a compatibility level of `100` when restored to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

- **Log shipping.** Log shipping is supported if the primary is running [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] SP3 or later, or [!INCLUDE [sql2008r2-md](../../includes/sql2008r2-md.md)] SP2 or later, and the secondary is running [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

   > [!WARNING]  
   > If an automatic or manual failover happens and the [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] instance becomes primary, [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] or [!INCLUDE [sql2008r2-md](../../includes/sql2008r2-md.md)] instance becomes secondary and can't receive changes from primary.

- Bulk load: Tables can be bulk copied from [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] or [!INCLUDE [sql2008r2-md](../../includes/sql2008r2-md.md)] to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

## SQL Server 2022 edition upgrade

The following table lists the supported edition upgrade scenarios in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

For step-by-step instructions on how to perform an edition upgrade, see [Upgrade to a different edition of SQL Server (Setup)](upgrade-to-a-different-edition-of-sql-server-setup.md).

| Upgrade from | Upgrade to |
| --- | --- |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise (Server+CAL and Core) <sup>2</sup> | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Evaluation Enterprise <sup>2</sup> | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise (Server+CAL or Core License)<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Developer<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web<br /><br />Upgrading from Evaluation (a free edition) to any of the paid editions is supported for stand-alone installations, but isn't supported for clustered installations. This limitation doesn't apply to stand-alone instances installed on a Windows Failover Cluster participating in an availability group. |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard <sup>2</sup> | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise (Server+CAL or Core License) |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Developer <sup>2</sup> | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise (Server+CAL or Core License)<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise (Server+CAL or Core License)<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Express <sup>1</sup> | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise (Server+CAL or Core License)<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Developer<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard<br /><br />[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Web |

Additionally you can also perform an edition upgrade between [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise (Server+CAL license) and [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise (Core License):

| Edition upgrade from | Edition upgrade to |
| --- | --- |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise (Server+CAL License) <sup>2</sup> | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise (Core License) |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise (Core License) | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise (Server+CAL License) |

<sup>1</sup> Also applies to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Express with Tools and [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Express with Advanced Services.

<sup>2</sup> Changing the edition of a clustered instance of [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] is limited. The following scenarios aren't supported for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] failover clusters:

- [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Developer, Standard, or Evaluation.
- [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Developer to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard or Evaluation.
- [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Evaluation.
- [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Evaluation to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard.

## Related content

- [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md)
- [SQL Server 2022: Hardware and software requirements](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2022.md)
- [Upgrade SQL Server](upgrade-sql-server.md)
- [Upgrade the Database Engine](upgrade-database-engine.md)
- [Upgrade to a different edition of SQL Server (Setup)](upgrade-to-a-different-edition-of-sql-server-setup.md)
