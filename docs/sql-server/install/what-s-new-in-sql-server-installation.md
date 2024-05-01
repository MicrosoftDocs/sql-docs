---
title: "What's new in SQL Server installation"
description: This article summarizes some changes to the SQL Server installation process, including SysPrep support and upgrading from SQL Server 2005.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/07/2023
ms.service: sql
ms.subservice: install
ms.topic: conceptual
ms.custom:
  - intro-whats-new
---
# What's new in SQL Server installation

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

Installation is supported on x64 processors only. For more information, see [SQL Server 2022: Hardware and software requirements](hardware-and-software-requirements-for-installing-sql-server-2022.md).

Installation of [!INCLUDE [ssExpress](../../includes/ssexpress-md.md)] prompts you to specify the directory to save the extracted package. If no location is entered, the server defaults to the computer's system drive (usually `C:\`). The extracted files will remain after [!INCLUDE [ssExpress](../../includes/ssexpress-md.md)] installation is complete.

**SysPrep** is supported for all installations of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. **SysPrep** now supports failover cluster installations. For more information, see [Considerations for installing SQL Server using SysPrep](../../database-engine/install-windows/considerations-for-installing-sql-server-using-sysprep.md) and [Install SQL Server with SysPrep](../../database-engine/install-windows/install-sql-server-using-sysprep.md).

You can upgrade from [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)], and [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]. For more information, see [Supported version and edition upgrades (SQL Server 2022)](../../database-engine/install-windows/supported-version-and-edition-upgrades-2022.md).

## Related content

- [What's new in SQL Server 2022](../what-s-new-in-sql-server-2022.md)
- [Maximum capacity specifications for SQL Server](../maximum-capacity-specifications-for-sql-server.md)
- [Planning a SQL Server Installation](planning-a-sql-server-installation.md)
- [SQL Server 2022: Hardware and software requirements](hardware-and-software-requirements-for-installing-sql-server-2022.md)
- [Installation guidance for SQL Server on Linux](../../linux/sql-server-linux-setup.md)
