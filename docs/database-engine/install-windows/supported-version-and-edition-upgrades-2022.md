---
title: "Supported version and edition upgrades (SQL Server 2022 Preview)"
description: The supported version and edition upgrades for SQL Server 2022 Preview. 
ms.custom: ""
ms.date: 05/24/2022
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
helpviewer_keywords: 
  - "components [SQL Server], adding to existing installations"
  - "versions [SQL Server], upgrading"
  - "upgrading SQL Server, upgrades supported"
  - "cross-language support"
author: rwestMSFT
ms.author: randolphwest
monikerRange: ">=sql-server-2017"
---
# Supported version & edition upgrades (SQL Server 2022 Preview)

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]
  
  You can upgrade from [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)], and [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]. This article lists the supported upgrade paths from these [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] versions, and the supported edition upgrades for [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)].  
  
## Pre-upgrade checklist  

- Before upgrading from one edition of [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] to another, verify that the functionality you are currently using is supported in the edition to which you are moving.  
- Verify supported [hardware and software](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2019.md).
- Before upgrading [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], enable Windows Authentication for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent and verify the default configuration: that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account is a member of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sysadmin group.
- To upgrade to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], you must be running a supported operating system. For more information, see [Hardware and Software Requirements for Installing SQL Server](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2019.md).  
- Upgrade will be blocked if there is a pending restart.  
- Upgrade will be blocked if the Windows Installer service is not running.

## Unsupported scenarios

- Cross-version instances of [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] are not supported. Version numbers of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] components must be the same in an instance of [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)].  
  
- [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] is only available for 64-bit platforms. Cross-platform upgrade is not supported. You cannot upgrade a 32-bit instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to native 64-bit using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup. However, you can back up or detach databases from a 32-bit instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and then restore or attach them to a new instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (64-bit) if the databases are not published in replication. You must re-create any logins and other user objects in `master`, `msdb`, and `model` system databases.  
  
- You cannot add new features during the upgrade of your existing instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. After you upgrade an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], you can add features by using the [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] Setup. For more information, see [Add Features to an Instance of SQL Server &#40;Setup&#41;](./add-features-to-an-instance-of-sql-server-setup.md).  
 
## Upgrades from earlier versions to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]  
 
[!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] supports upgrade from the following versions of SQL Server:

- [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP4 or later
- [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP2 or later
- [!INCLUDE[ssSQL16](../../includes/sssql16-md.md)] SP2 or later
- [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)]
- [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]

Specific version and edition upgrade paths are not available during community technology preview releases.

## Migrate to SQL Server 2022 Preview

You can migrate databases from older versions. For example, you can migrate databases from [!INCLUDE [sskilimanjaro-md](../../includes/sskilimanjaro-md.md)] to SQL Server 2022 Preview.

For information, see [Azure Database Migration Guide](https://datamigration.microsoft.com/scenario/sql-to-sqlserver).

The following tips and tools can help you plan and implement your migration.

- Migration tools: Migration is supported through [Data Migration Assistant (DMA)](../../dma/dma-overview.md).
- Backup and restore: A backup taken on SQL Server 2008 or SQL Server 2008 R2 can be restored to SQL Server 2022 Preview.
- Log shipping: Log shipping is supported if primary is running SQL Server 2008 SP3 or later, or SQL Server 2008 R2 SP2 or later, and secondary is running SQL Server 2022 Preview. 

   > [!WARNING]
   > If an automatic or manual failover happens and the SQL Server 2022 Preview instance becomes primary, SQL Server 2008 or SQL Server 2008 R2 instance becomes secondary and cannot receive changes from primary.

- Bulk load: Tables can be bulk copied from SQL Server 2008 or SQL Server 2008 R2 to SQL Server 2022 Preview.
  
## See also  

- [Hardware and software requirements for installing SQL Server](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2019.md)

## Next steps

- [Upgrade SQL Server](../../database-engine/install-windows/upgrade-sql-server.md)
- [Upgrade Database Engine](upgrade-database-engine.md)
- [Upgrade to a Different Edition of SQL Server (Setup)](upgrade-to-a-different-edition-of-sql-server-setup.md)