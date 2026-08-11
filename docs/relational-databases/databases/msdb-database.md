---
title: The msdb Database
description: The msdb database stores SQL Server Agent jobs, alerts, backup history, and Database Mail settings. Learn its properties, restrictions, and backup best practices.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 08/10/2026
ms.service: sql
ms.topic: concept-article
helpviewer_keywords:
  - "SQL Server Agent, msdb database"
  - "alerts [SQL Server], msdb database"
  - "jobs [SQL Server], msdb database"
  - "msdb database [SQL Server]"
---
# msdb database

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent uses the `msdb` database to schedule alerts and jobs. Other features that use `msdb` include [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE [ssSB](../../includes/sssb-md.md)], and Database Mail.

For example, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] automatically maintains a complete online backup-and-restore history within tables in `msdb`. This information includes the name of the party that performs the backup, the time of the backup, and the devices or files where the backup is stored. [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] uses this information to propose a plan for restoring a database and applying any transaction log backups. The system records backup events for all databases, even if custom applications or third-party tools create them. For example, if you use a [!INCLUDE [c-sharp-md](../../includes/c-sharp-md.md)] application that calls SQL Server Management Objects (SMO) objects to perform backup operations, the event is logged in the `msdb` system tables, the Windows application log, and the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error log. To help protect the information that `msdb` stores, consider placing the `msdb` transaction log on fault-tolerant storage.

By default, `msdb` uses the simple recovery model. If you use the [backup and restore history](../backup-restore/backup-history-and-header-information-sql-server.md) tables, use the full recovery model for `msdb`. For more information, see [Recovery models (SQL Server)](../backup-restore/recovery-models-sql-server.md). When you install or upgrade [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or use Setup.exe to rebuild the system databases, Setup automatically sets the recovery model of `msdb` to simple.

> [!IMPORTANT]  
> After any operation that updates `msdb`, such as backing up or restoring any database, back up `msdb`. For more information, see [Back up and restore: System databases (SQL Server)](../backup-restore/back-up-and-restore-of-system-databases-sql-server.md).  

The `msdb` database provides different options in Azure SQL Managed Instance. For more information, see [backup transparency](/azure/azure-sql/managed-instance/backup-transparency).

## Physical properties of msdb

The following table lists the initial configuration values of the `msdb` data and log files. The sizes of these files might vary slightly for different editions of [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)].

| File | Logical name | Physical name | File growth |
| --- | --- | --- | --- |
| Primary data | `MSDBData` | `MSDBData.mdf` | Autogrow by 10 percent until the disk is full. |
| Log | `MSDBLog` | `MSDBLog.ldf` | Autogrow by 10 percent to a maximum of 2 terabytes. |

To move the `msdb` database or log files, see [Move system databases](move-system-databases.md).

### Database options

The following table lists the default value for each database option in the `msdb` database and whether you can modify the option. To view the current settings for these options, use the [sys.databases](../system-catalog-views/sys-databases-transact-sql.md) catalog view.

| Database option | Default value | Can be modified |
| --- | --- | --- |
| `ALLOW_SNAPSHOT_ISOLATION` | `ON` | No |
| `ANSI_NULL_DEFAULT` | `OFF` | Yes |
| `ANSI_NULLS` | `OFF` | Yes |
| `ANSI_PADDING` | `OFF` | Yes |
| `ANSI_WARNINGS` | `OFF` | Yes |
| `ARITHABORT` | `OFF` | Yes |
| `AUTO_CLOSE` | `OFF` | Yes |
| `AUTO_CREATE_STATISTICS` | `ON` | Yes |
| `AUTO_SHRINK` | `OFF` | Yes |
| `AUTO_UPDATE_STATISTICS` | `ON` | Yes |
| `AUTO_UPDATE_STATISTICS_ASYNC` | `OFF` | Yes |
| `CHANGE_TRACKING` | `OFF` | No |
| `CONCAT_NULL_YIELDS_NULL` | `OFF` | Yes |
| `CURSOR_CLOSE_ON_COMMIT` | `OFF` | Yes |
| `CURSOR_DEFAULT` | `GLOBAL` | Yes |
| Database Availability Options | `ONLINE`<br /><br />`MULTI_USER`<br /><br />`READ_WRITE` | No<br /><br />Yes<br /><br />Yes |
| `DATE_CORRELATION_OPTIMIZATION` | `OFF` | Yes |
| `DB_CHAINING` | `ON` | Yes |
| `ENCRYPTION` | `OFF` | No |
| `MIXED_PAGE_ALLOCATION` | `ON` | No |
| `NUMERIC_ROUNDABORT` | `OFF` | Yes |
| `PAGE_VERIFY` | `CHECKSUM` | Yes |
| `PARAMETERIZATION` | `SIMPLE` | Yes |
| `QUOTED_IDENTIFIER` | `OFF` | Yes |
| `READ_COMMITTED_SNAPSHOT` | `OFF` | No |
| `RECOVERY` | `SIMPLE` | Yes |
| `RECURSIVE_TRIGGERS` | `OFF` | Yes |
| Service Broker Options | `ENABLE_BROKER` | Yes |
| `TRUSTWORTHY` | `ON` | Yes |

For a description of these database options, see [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md).

<a id="restrictions"></a>

## Limitations

You can't perform the following operations on the `msdb` database:

- Changing collation. The default collation is the server collation.

- Dropping the database.
- Dropping the **guest** user from the database.
- Enabling change data capture.
- Participating in database mirroring.
- Removing the primary filegroup, primary data file, or log file.
- Renaming the database or primary filegroup.
- Setting the database to `OFFLINE`.
- Setting the primary filegroup to `READ_ONLY`.

## Recommendations

When you work with the `msdb` database, consider the following recommendations:

- Always have a current backup of the `msdb` database available.
- Back up the `msdb` database as soon as possible after the following operations:

  - Creating, modifying, or deleting any jobs, alerts, proxies, or maintenance plans
  - Adding, changing, or deleting Database Mail profiles
  - Adding, modifying, or deleting Policy-Based Management policies

- Don't create user objects in `msdb`. If you do, back up `msdb` more frequently.
- Treat the `msdb` database as highly sensitive and don't grant access to anyone without a proper need. Members of the **sysadmin** fixed server role often own SQL Server Agent jobs too. Ensure that no one can tamper with the code that runs.
- Audit any changes to objects in `msdb`.

## Related content

- [System Databases](system-databases.md)
- [sys.databases (Transact-SQL)](../system-catalog-views/sys-databases-transact-sql.md)
- [sys.master_files (Transact-SQL)](../system-catalog-views/sys-master-files-transact-sql.md)
- [Move database files](move-database-files.md)
- [Database Mail](../database-mail/database-mail.md)
- [Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md)
