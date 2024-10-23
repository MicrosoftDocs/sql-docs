---
title: "MSSQLSERVER_3168"
description: "MSSQLSERVER_3168"
author: MashaMSFT
ms.author: mathoma
ms.date: 02/10/2023
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "3168 (Database Engine error)"
---
# MSSQLSERVER_3168

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|3168|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|LDDB_SYSTEMWRONGVER|  
|Message Text|The backup of the system database on the device %ls cannot be restored because it was created by a different version of the server (%ls) than this server (%ls).|  
  
## Explanation

You can't restore a backup of a system database (**master**, **model**, or **msdb**) on a server build that differs from the build on which the backup was originally created.  
  
> [!NOTE]  
> Installing a servicing update like cumulative update or service pack or a GDR changes the server build number. Server builds are always incremental.

  
### Possible causes

The database schema for system databases might be changed across server builds. To make sure that a schema change doesn't cause inconsistencies, the RESTORE statement compares the server build number of the backup file to the build number of the server on which you are trying to restore the backup. If the builds are different, the statement issues a *"3168"* error message, and the restore operation terminates abnormally.  

  
Some scenarios in which this problem might occur include the following:  
  
- You try to restore a system database on Server A from a backup that's taken on Server B. Servers A and B are on different server builds. For example, Server A might be on the original release version build and Server B might be on a service pack 1 (SP1) build.  
  
- You try to restore a system database from a backup that's taken on the same server. However, the server was running a different build when the backup process ran. That is, the server was upgraded since the backup was created.  
  
## User action

To resolve the issue, follow these steps:

> [!NOTE]
> For the following procedures, Server A is the source SQL Server-based server on which the backup is taken, and Server B is the destination SQL Server-based server that you're trying to restore your backup to:

1. Determine the version of Server B (Version B), by using the following query:

   ```sql
   SELECT @@VERSION;
   ```

1. Run a query that resembles the following to determine the version of SQL Server that was running when the source backup was taken (Version A):

      `RESTORE headeronly FROM disk = 'c:\sqlbackups\masterdb.bak'`

1. Review the values of `SoftwareVersionMajor`, `SoftwareVersionMinor`, and `SoftwareVersionBuild` columns to determine the build of the source server that was used when the backup was taken. For example, assume that the values are as follows:

   - SoftwareVersionMajor: 15
   - SoftwareVersionMinor: 0
   - SoftwareVersionBuild: 4236
In this case, the source SQL Server version when the backup was taken is 15.0.4236.

1. Use either the [SQL Server complete version list tables](/troubleshoot/sql/releases/download-and-install-latest-updates) or the [Excel builds spreadsheet](https://aka.ms/sqlserverbuilds) to determine the version of SQL Server that the build corresponds to. For example, 15.0.4236 maps to SQL Server 2019 CU16+GDR (Version A).

1. Use one of the following options:

    - If Version A is greater than Version B, use the information that's in [Latest updates and version history for SQL Server](/troubleshoot/sql/releases/download-and-install-latest-updates) to upgrade Server B to the same build as Version A.

    - If Version A is lesser than Version B, temporarily remove later updates by using the following steps:

        1. In **Control Panel**, select **Programs > Programs and Features**, and then select **View installed updates**.

        1. Locate the entry that corresponds to each of the later update packages that correspond to Version B.

        1. Press and hold (or right-click) the entry, and then select **Uninstall**.

    - After you verify that Version B is same as Version A, retry the restore operation of master database on Server B.

1. (Recommended) Update Server B to the latest available version, and then take a new backup of your system databases.

## See also

[Limitations on Restoring System Databases &#40;SQL Server&#41;](~/relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md#limitations-on-restoring-system-databases)  
  
[Restore the master database (Transact-SQL)](../backup-restore/restore-the-master-database-transact-sql.md)

[The Easiest Way To Rebuild The master Database](https://techcommunity.microsoft.com/t5/sql-server-blog/the-easiest-way-to-rebuild-the-master-database/ba-p/383742)
