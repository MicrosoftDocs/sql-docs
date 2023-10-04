---
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/20/2022
ms.service: sql
ms.subservice: linux
ms.topic: include
ms.custom:
  - linux-related-content
---
To roll back or downgrade [!INCLUDE[ssnoversion-md](../../includes/ssnoversion-md.md)] to a previous release, use the following steps:

1. Identify the version number for the [!INCLUDE[ssnoversion-md](../../includes/ssnoversion-md.md)] package you want to downgrade to. For a list of package numbers, see the release notes:

   - [Release notes for SQL Server 2022 on Linux](../sql-server-linux-release-notes-2022.md)
   - [Release notes for SQL Server 2019 on Linux](../sql-server-linux-release-notes-2019.md)
   - [Release notes for SQL Server 2017 on Linux](../sql-server-linux-release-notes-2017.md)

1. Downgrade to a previous version of [!INCLUDE[ssnoversion-md](../../includes/ssnoversion-md.md)]. In the following commands, replace `<version_number>` with the [!INCLUDE[ssnoversion-md](../../includes/ssnoversion-md.md)] version number you identified in step one.

   | Platform | Package update command(s) |
   |-----|-----|
   | RHEL | `sudo yum downgrade mssql-server-<version_number>.x86_64` |
   | SLES | `sudo zypper install --oldpackage mssql-server=<version_number>` |
   | Ubuntu | `sudo apt-get install mssql-server=<version_number>`<br/>`sudo systemctl start mssql-server` |

> [!NOTE]  
> It is only supported to downgrade to a release within the same major version, such as [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)].
