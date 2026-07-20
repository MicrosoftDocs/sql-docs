---
title: "Supported Version and Edition Upgrades (SQL Server 2017)"
description: The supported version and edition upgrades for SQL Server 2017.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/03/2025
ms.service: sql
ms.subservice: install
ms.topic: upgrade-and-migration-article
helpviewer_keywords:
  - "components [SQL Server], adding to existing installations"
  - "versions [SQL Server], upgrading"
  - "upgrading SQL Server, upgrades supported"
  - "cross-language support"
monikerRange: ">=sql-server-2017"
---
# Supported version and edition upgrades (SQL Server 2017)

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

You can upgrade from [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)], [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)], [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)], and [!INCLUDE [sssql15-md](../../includes/sssql16-md.md)]. This article lists the supported upgrade paths from these [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] versions, and the supported edition upgrades for [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)].

## Pre upgrade checklist

Before upgrading from one edition of [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] to another, verify that the functionality you're currently using is supported in the edition to which you're moving.

Before upgrading [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], enable Windows Authentication for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent and verify the default configuration: that the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account is a member of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] **sysadmin** group.

To upgrade to [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], you must be running a supported operating system. For more information, see [Hardware and software requirements for SQL Server 2016 and SQL Server 2017](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md).

Upgrade is blocked if there's a pending restart.

Upgrade is blocked if the Windows Installer service isn't running.

## Unsupported Scenarios

- Cross-version instances of [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] aren't supported. Version numbers of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] components must be the same in an instance of [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)].

- [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] is only available for 64-bit platforms. Cross-platform upgrade isn't supported. You can't upgrade a 32-bit instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to native 64-bit using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup. However, you can back up or detach databases from a 32-bit instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and then restore or attach them to a new instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (64-bit) if the databases aren't published in replication. You must re-create any logins and other user objects in `master`, `msdb`, and `model` system databases.

- You can't add new features during the upgrade of your existing instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. After you upgrade an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], you can add features by using the [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Setup. For more information, see [Add Features to an Instance of SQL Server (Setup)](add-features-to-an-instance-of-sql-server-setup.md).

- Failover Clusters aren't supported in WOW mode.

## Upgrades from Earlier Versions to SQL Server 2017 (14.x)

[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] supports upgrade from the following versions of SQL Server:

- SQL Server 2008 SP4 or later
- SQL Server 2008 R2 SP3 or later
- SQL Server 2012 SP2 or later
- SQL Server 2014 or later
- SQL Server 2016 or later

> [!NOTE]  
> To upgrade databases on [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] see [Support for 2005](#SupportFor2005).

The following table lists the supported upgrade scenarios from earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)].

| Upgrade from | Supported upgrade path |
| :--- | :--- |
| [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] SP4 Enterprise | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise |
| [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] SP4 Developer | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Developer |
| [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] SP4 Standard | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard |
| [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] SP4 Small Business | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard |
| [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] SP4 Web | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Web |
| [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] SP4 Workgroup | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard |
| [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] SP4 Express | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Web<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Express |
| [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] SP3 Datacenter | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise |
| [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] SP3 Enterprise | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise |
| [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] SP3 Developer | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Developer |
| [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] SP3 Small Business | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard |
| [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] SP3 Standard | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard |
| [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] SP3 Web | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Web |
| [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] SP3 Workgroup | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard |
| [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] SP3 Express | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Web<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Express |
| [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP2 Enterprise | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise |
| [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP2 Developer | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Developer<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Web<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise |
| [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP2 Standard | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard |
| [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP1 Web | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Web |
| [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP2 Express | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Web<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Express |
| [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP2 Business Intelligence | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise |
| [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP2 Evaluation | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Evaluation<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Web<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Developer |
| [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] Enterprise | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise |
| [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] Developer | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Developer<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Web<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise |
| [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] Standard | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard |
| [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] Web | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Web |
| [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] Express | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Web<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Express<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Developer |
| [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] Business Intelligence | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise |
| [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] Evaluation | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Evaluation<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Web<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Developer |
| [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] Enterprise | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise |
| [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] Developer | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Developer<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Web<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise |
| [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] Standard | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard |
| [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] Web | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Web |
| [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] Express | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Web<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Express<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Developer |
| [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] Business Intelligence | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise |
| [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] Evaluation | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Evaluation<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Web<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Developer |
| [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] release candidate <sup>1</sup> | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise |
| [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Developer | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise |

<sup>1</sup> Microsoft support to upgrade from release candidate software is specifically for customers who participated in the Technology Adoption Program (TAP).

<a id="SupportFor2005"></a>

### SQL Server 2017 support for SQL Server 2005

This section discusses [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] support for [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)]. In [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], you can:

- Attach a [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] database (mdf/ldf files) to [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] instance of database engine.

- Restore a [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] database to [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] instance of database engine from a backup.

- Back up a [!INCLUDE [ssASversion2005](../../includes/ssasversion2005-md.md)] cube and restore it on [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)].

When a [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] database is upgraded to [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], the database compatibility level is changed from 90 to 100. (In [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], valid values for the database compatibility level are 100, 110, 120, 130, and 140.) [ALTER DATABASE (Transact-SQL) compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md) discusses how the compatibility level change could affect [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] applications.

Any scenarios not specified in the previous list aren't supported, including but not limited to:

- Installing [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] and [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] on same computer (side by side).

- Using a [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] instance as a member of the replication topology that involves a [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] instance.

- Configuring database mirroring between [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] instances.

- Backing up the transaction log with log shipping between [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] instances.

- Configuring linked servers between [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] instances.

- Managing a [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] instance from a [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Management Studio.

- Attaching a [!INCLUDE [ssASversion2005](../../includes/ssasversion2005-md.md)] cube in [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Management Studio.

- Connecting to [!INCLUDE [ssISversion2005](../../includes/ssisversion2005-md.md)] from [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Management Studio.

- Managing a [!INCLUDE [ssISversion2005](../../includes/ssisversion2005-md.md)] service from [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Management Studio.

- Support for [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] third party custom Integration Services components, such as execute and upgrade.

## SQL Server 2017 edition upgrade

The following table lists the supported edition upgrade scenarios in [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)].

For step-by-step instructions on how to perform an edition upgrade, see [Upgrade to a different edition of SQL Server (Setup)](upgrade-downgrade-sql-server-edition-setup.md).

| Upgrade from | Upgrade to |
| --- | --- |
| [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise (Server+CAL and Core) <sup>2</sup> | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise |
| [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Evaluation Enterprise <sup>2</sup> | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise (Server+CAL or Core License)<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Developer<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Web<br /><br />Upgrading from Evaluation (a free edition) to any of the paid editions is supported for stand-alone installations, but isn't supported for clustered installations. This limitation doesn't apply to stand-alone instances installed on a Windows Failover Cluster participating in an availability group. |
| [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard <sup>2</sup> | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise (Server+CAL or Core License) |
| [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Developer <sup>2</sup> | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise (Server+CAL or Core License)<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Web<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard |
| [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Web | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise (Server+CAL or Core License)<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard |
| [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Express <sup>1</sup> | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise (Server+CAL or Core License)<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Developer<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard<br /><br />[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Web |

<sup>1</sup> Also applies to [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Express with Tools and [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Express with Advanced Services.

<sup>2</sup> Changing the edition of a [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] failover cluster is limited. The following scenarios aren't supported for [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] failover clusters:

Additionally you can also perform an edition upgrade between [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise (Server+CAL license) and [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise (Core License):

| Edition upgrade from | Edition upgrade to |
| --- | --- |
| [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise (Server+CAL License) <sup>1</sup> | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise (Core License) |
| [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise (Core License) | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise (Server+CAL License) |

<sup>1</sup> Changing the edition of a [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] failover cluster is limited. The following scenarios aren't supported for [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] failover clusters:

- [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Enterprise to [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Developer, Standard, or Evaluation.
- [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Developer to [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard or Evaluation.
- [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard to [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Evaluation.
- [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Evaluation to [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Standard.

## Related content

- [Editions and supported features of SQL Server 2017](../../sql-server/editions-and-components-of-sql-server-2017.md)
- [Hardware and software requirements for SQL Server 2016 and SQL Server 2017](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)
- [Upgrade SQL Server](upgrade-sql-server.md)
