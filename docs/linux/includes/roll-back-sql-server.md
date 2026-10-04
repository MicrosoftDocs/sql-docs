---
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/02/2026
ms.service: sql
ms.subservice: linux
ms.topic: include
ms.custom:
  - linux-related-content
---
To roll back or downgrade [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to a previous release, use the following steps:

1. Find the version number for the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] package you want to downgrade to. For a list of package numbers, see [KB 5122767](https://support.microsoft.com/help/5122767).

1. Downgrade to a previous version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. In the following commands, replace `<version_number>` with the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] version number you found in step 1.

   | Platform | Package update commands |
   | --- | --- |
   | **RHEL** | `sudo yum downgrade mssql-server-<version_number>.x86_64` |
   | **SLES** | `sudo zypper install --oldpackage mssql-server=<version_number>` |
   | **Ubuntu** | `sudo apt-get install mssql-server=<version_number>`<br />`sudo systemctl start mssql-server` |

> [!NOTE]  
> The only supported downgrade is if you downgrade to a release within the same major version, such as [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].
