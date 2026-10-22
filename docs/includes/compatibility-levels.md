---
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 09/15/2025
ms.service: sql
ms.topic: include
---
| Product | Database Engine version | Default compatibility level designation | Supported compatibility level values |
| --- | --- | --- | --- |
| [!INCLUDE [ssazure-sqldb](ssazure-sqldb.md)] | 17 | 170 | 170, 160, 150, 140, 130, 120, 110, 100 |
| [!INCLUDE [ssazuremi-md](ssazuremi-md.md)] <br /> ([**Always-up-to-date** update policy](/azure/azure-sql/managed-instance/update-policy#always-up-to-date-update-policy)) | 17 | 170 | 170, 160, 150, 140, 130, 120, 110, 100 |
| [!INCLUDE [ssazuremi-md](ssazuremi-md.md)] <br /> ([**SQL Server 2025** update policy](/azure/azure-sql/managed-instance/update-policy#sql-server-2025-update-policy)) | 17 | 170 | 170, 160, 150, 140, 130, 120, 110, 100 |
| [!INCLUDE [ssazuremi-md](ssazuremi-md.md)] <br /> ([**SQL Server 2022** update policy](/azure/azure-sql/managed-instance/update-policy#sql-server-2022-update-policy)) | 16 | 150 | 160, 150, 140, 130, 120, 110, 100 |
| [!INCLUDE [sssql25-md](sssql25-md.md)] | 17 | 170 | 170, 160, 150, 140, 130, 120, 110, 100 |
| [!INCLUDE [sssql22-md](sssql22-md.md)] | 16 | 160 | 160, 150, 140, 130, 120, 110, 100 |
| [!INCLUDE [sssql19-md](sssql19-md.md)] | 15 | 150 | 150, 140, 130, 120, 110, 100 |
| [!INCLUDE [sssql17-md](sssql17-md.md)] | 14 | 140 | 140, 130, 120, 110, 100 |
| [!INCLUDE [sssql16-md](sssql16-md.md)] | 13 | 130 | 130, 120, 110, 100 |
| [!INCLUDE [sssql14-md](sssql14-md.md)] | 12 | 120 | 120, 110, 100 |
| [!INCLUDE [sssql11-md](sssql11-md.md)] | 11 | 110 | 110, 100, 90 |
| [!INCLUDE [sql2008r2-md](sql2008r2-md.md)] | 10.5 | 100 | 100, 90, 80 |
| [!INCLUDE [sql2008-md](sql2008-md.md)] | 10 | 100 | 100, 90, 80 |
| [!INCLUDE [ssVersion2005](ssversion2005-md.md)] | 9 | 90 | 90, 80 |
| [!INCLUDE [ssVersion2000](ssversion2000-md.md)] | 8 | 80 | 80 |

> [!IMPORTANT]  
> The database engine version numbers for [!INCLUDE [ssnoversion-md](ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](ssazure-sqldb.md)] aren't comparable with each other, and rather are internal build numbers for these separate products. The database engine for [!INCLUDE [ssazure-sqldb](ssazure-sqldb.md)] is based on the same code base as the [!INCLUDE [ssdenoversion-md](ssdenoversion-md.md)]. Most importantly, the database engine in [!INCLUDE [ssazure-sqldb](ssazure-sqldb.md)] always has the newest SQL database engine bits. Version 12 of [!INCLUDE [ssazure-sqldb](ssazure-sqldb.md)] is newer than version 15 of [!INCLUDE [ssnoversion-md](ssnoversion-md.md)].
