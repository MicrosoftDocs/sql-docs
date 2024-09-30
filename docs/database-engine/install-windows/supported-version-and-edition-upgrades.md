---
title: "Supported Version and Edition Upgrades (SQL Server 2016)"
description: The supported version and edition upgrades for SQL Server 2016.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: vanto
ms.date: 09/17/2024
ms.service: sql
ms.subservice: install
ms.topic: conceptual
helpviewer_keywords:
  - "components [SQL Server], adding to existing installations"
  - "versions [SQL Server], upgrading"
  - "upgrading SQL Server, upgrades supported"
  - "cross-language support"
monikerRange: ">=sql-server-2016"
---
# Supported version & edition upgrades (SQL Server 2016)

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

You can upgrade from [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)], [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)], [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)], and [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)]. This article lists the supported upgrade paths from these [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] versions, and the supported edition upgrades for [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)].

## Pre upgrade Checklist

- Before upgrading from one edition of [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] to another, verify that the functionality you're currently using is supported in the edition to which you're moving.

- Before upgrading [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], enable Windows Authentication for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent and verify the default configuration that the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account is a member of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] sysadmin group.

- To upgrade to [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)], you must be running a supported operating system. For more information, see [SQL Server 2016 and 2017: Hardware and software requirements](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md).

- Upgrade is blocked if there's a pending restart.

- Upgrade is blocked if the Windows Installer service isn't running.

## Unsupported Scenarios

- Cross-version instances of [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] aren't supported. Version numbers of the [!INCLUDE [ssDE](../../includes/ssde-md.md)], [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)], and [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] components must be the same in an instance of [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)].

- [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] is only available for 64-bit platforms. Cross-platform upgrade isn't supported. You can't upgrade a 32-bit instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to native 64-bit using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup. However, you can back up or detach databases from a 32-bit instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and then restore or attach them to a new instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (64-bit) if the databases aren't published in replication. You must re-create any logins and other user objects in `master`, `msdb`, and `model` system databases.

- You can't add new features during the upgrade of your existing instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. After you upgrade an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)], you can add features by using the [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Setup. For more information, see [Add Features to an Instance of SQL Server (Setup)](add-features-to-an-instance-of-sql-server-setup.md).

- Failover Clusters aren't supported in WOW mode.

- Upgrade from an Evaluation edition of a previous [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] version isn't supported.

- When upgrading from RC1 or previous versions of SQL Server 2016 to RC3 or later versions, PolyBase must be uninstalled before the upgrade and reinstalled after the upgrade.

## Upgrades from Earlier Versions to [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)]

SQL Server 2016 supports upgrade from the following versions of SQL Server:

- [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] SP4 or later
- [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] SP3 or later
- [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP2 or later
- [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] or later

> [!NOTE]  
> To upgrade databases on [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] see [Support for 2005](#SupportFor2005).

The table below lists the supported upgrade scenarios from earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)].

| Upgrade from | Supported upgrade path |
| --- | --- |
| [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] SP4 Enterprise | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br />|
| [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] SP4 Developer | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Developer |
| [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] SP4 Standard | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard |
| [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] SP4 Small Business | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard |
| [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] SP4 Web | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Web |
| [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] SP4 Workgroup | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard |
| [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] SP4 Express | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Web<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Express |
| [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] SP3 Datacenter | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br />|
| [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] SP3 Enterprise | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br />|
| [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] SP3 Developer | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Developer |
| [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] SP3 Small Business | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard |
| [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] SP3 Standard | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard |
| [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] SP3 Web | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Web |
| [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] SP3 Workgroup | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard |
| [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] SP3 Express | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Web<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Express |
| [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP2 Enterprise | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br />|
| [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP2 Developer | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Developer<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Web<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br />|
| [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP2 Standard | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard |
| [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP1 Web | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Web |
| [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP2 Express | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Web<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Express<br />|
| [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP2 Business Intelligence | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br />|
| [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP2 Evaluation | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Evaluation<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Web<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Developer |
| [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] Enterprise | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br />|
| [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] Developer | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Developer<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Web<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br />|
| [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] Standard | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard |
| [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] Web | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Web |
| [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] Express | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Web<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Express<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Developer |
| [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] Business Intelligence | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br />|
| [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] Evaluation | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Evaluation<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Web<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Developer |
| [!INCLUDE [ssSQL15_md](../../includes/sssql16-md.md)] release candidate* | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise |
| [!INCLUDE [ssSQL15_md](../../includes/sssql16-md.md)] Developer | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise |

\* Microsoft support to upgrade from release candidate software is specifically for customers who participated in the Technology Adoption Program (TAP).

### <a name="SupportFor2005"></a> [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Support for [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)]

This section discusses [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] support for [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)]. In [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)], you'll be able to do the following:

- Attach a [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] database (mdf/ldf files) to [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] instance of database engine.

- Restore a [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] database to [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] instance of database engine from a backup.

- Back up a [!INCLUDE [ssASversion2005](../../includes/ssasversion2005-md.md)] cube and restoring on [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)].

> [!NOTE]  
> When a [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] database is upgraded to [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)], the database compatibility level will be changed from 90 to 100.  
> In [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)], valid values for the database compatibility level are 100, 110, 120, and 130. [ALTER DATABASE (Transact-SQL) compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md) discusses how the compatibility level change could affect [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] applications.

Any scenarios not specified in the list above aren't supported, including but not limited to the following:

- Installing [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] and [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] on same computer (side by side).

- Using a [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] instance as a member of the replication topology that involves a [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] instance.

- Configuring database mirroring between [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] and [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] instances.

- Backing up the transaction log with log shipping between [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] and [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] instances.

- Configuring linked servers between [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] and [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] instances.

- Managing a [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] instance from a [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Management Studio.

- Attaching a [!INCLUDE [ssASversion2005](../../includes/ssasversion2005-md.md)] cube in [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Management Studio.

- Connecting to [!INCLUDE [ssISversion2005](../../includes/ssisversion2005-md.md)] from [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Management Studio.

- Managing a [!INCLUDE [ssISversion2005](../../includes/ssisversion2005-md.md)] service from [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Management Studio.

- Support for [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] third party custom Integration Services components, such as execute and upgrade.

## [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Edition Upgrade

The following table lists the supported edition upgrade scenarios in [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)].

For step-by-step instructions on how to perform an edition upgrade, see [Upgrade to a different edition of SQL Server (Setup)](upgrade-to-a-different-edition-of-sql-server-setup.md).

| Upgrade From | Upgrade To |
| --- | --- |
| [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise (Server+CAL and Core)** | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise |
| [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Evaluation Enterprise** | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise (Server+CAL or Core License)<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Developer<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Web<br />Upgrading from Evaluation (a free edition) to any of the paid editions is supported for stand-alone installations, but isn't supported for clustered installations. This limitation doesn't apply to stand-alone instances installed on a Windows Failover Cluster participating in an availability group. |
| [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard** | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise (Server+CAL or Core License) |
| [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Developer** | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise (Server+CAL or Core License)<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Web<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard |
| [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Web | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise (Server+CAL or Core License)<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard |
| [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Express* | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise (Server+CAL or Core License)<br /><br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Developer<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard<br />[!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Web |

Additionally you can also perform an edition upgrade between [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise (Server+CAL license) and [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise (Core License):

| Edition Upgrade From | Edition Upgrade To |
| --- | --- |
| [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise (Server+CAL License)** | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise (Core License) |
| [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise (Core License) | [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise (Server+CAL License) |

\* Also applies to [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Express with Tools and [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Express with Advanced Services.

** Changing the edition of a [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] failover cluster is limited. The following scenarios aren't supported for [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] failover clusters:

- [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Enterprise to [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Developer, Standard, or Evaluation.

- [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Developer to [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard or Evaluation.

- [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard to [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Evaluation.

- [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Evaluation to [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)] Standard.

## Related content

- [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md)
- [SQL Server 2022: Hardware and software requirements](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2022.md)
- [Upgrade SQL Server](upgrade-sql-server.md)
