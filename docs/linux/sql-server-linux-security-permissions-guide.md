---
title: Security and Permissions Guide for SQL Server on Linux
description: Learn about the required service accounts, and file system permissions for SQL Server on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/14/2025
ms.service: sql
ms.subservice: linux
ms.topic: concept-article
ms.custom:
  - linux-related-content
ai-usage: ai-assisted
---
# SQL Server on Linux - Security and permissions guide

This article describes the required service accounts, and file system permissions for [!INCLUDE [sqlonlinux-md](../includes/sqlonlinux-md.md)]. For more information about SQL Server on Windows permissions, see [Configure Windows service accounts and permissions](../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).

## Built‑in Windows principals

Even though [!INCLUDE [sqlonlinux-md](../includes/sqlonlinux-md.md)] runs under the `mssql` operating system account, the following Windows principals exist at the SQL Server layer for compatibility. Don't remove or deny them unless you fully understand the risks.

| Principal | Default SQL&nbsp;Server role | Details |
| --- | --- | --- |
| `BUILTIN\Administrators` | **sysadmin** | Maps to the root‑level administrators of the host. Certain system objects run in the context of this account. |
| `NT AUTHORITY\SYSTEM` | **public** | Service identifier (SID) reserved for the Windows `SYSTEM` account. Still created so that cross‑platform scripts that expect it succeed. |
| `NT AUTHORITY\NETWORK SERVICE` | **sysadmin** (no fixed role) | Historically the default startup account for several SQL Server services on Windows. Present only for backward compatibility. Not used by the [!INCLUDE [sqlonlinux-md](../includes/sqlonlinux-md.md)] [!INCLUDE [ssde-md](../includes/ssde-md.md)] itself. |

## File and directory ownership

All files under the folder `/var/opt/mssql` must be owned by the `mssql` user, and `mssql` group (`mssql:mssql`), with read and write access for both. If you change the default umask (`0022`), or use alternative mount points, you must reapply these permissions manually.

The default permissions for the `mssql` folder are as follows:

```output
drwxr-xr-x  3 root  root  4096 May 14 17:17 ./
drwxr-xr-x 13 root  root  4096 Jan  7  2025 ../
drwxrwx---  7 mssql mssql 4096 May 14 17:17 mssql/
```

The default permissions for the contents of the `mssql` folder are as follows:

```output
drwxrwx--- 7 mssql mssql 4096 May 14 17:17 ./
drwxr-xr-x 3 root  root  4096 May 14 17:17 ../
drwxr-xr-x 5 mssql mssql 4096 May 14 17:17 .system/
drwxr-xr-x 2 mssql mssql 4096 May 14 17:17 data/
drwxr-xr-x 3 mssql mssql 4096 Sep 16 22:57 log/
-rw-r--r-- 1 root  root    85 May 14 17:17 mssql.conf
drwxrwxr-x 2 mssql mssql 4096 May 14 17:17 secrets/
drwxr-xr-x 2 mssql mssql 4096 May 14 17:17 security/
```

For more information on how to change user data location, log file location, or system database and log locations, see [Configure SQL Server on Linux with the mssql-conf tool](sql-server-linux-configure-mssql-conf.md).

### Typical SQL Server directories

| Purpose | Default path | Details |
| --- | --- | --- |
| System and user databases | `/var/opt/mssql/data` | Includes `master`, `model`, `tempdb`, and any new databases unless **mssql-conf** redirects them. For more information, see [Change the default location for system databases](sql-server-linux-configure-mssql-conf.md#masterdatabasedir). |
| Transaction logs (if separated) | `/var/opt/mssql/data`, or the path set via `mssql-conf set filelocation.defaultlogdir`. | Keep the same ownership if you move transaction logs. For more information, see [Change the default data or log directory location](sql-server-linux-configure-mssql-conf.md#change-the-default-data-or-log-directory-location). |
| Backups | `/var/opt/mssql/data` | Create and set with `chown` before first backup, or when mapping a volume or directory. For more information, see [Change the default backup directory location](sql-server-linux-configure-mssql-conf.md#change-the-default-backup-directory-location). |
| Error logs, and Extended Event (XE) logs | `/var/opt/mssql/log` | Also hosts the default system‑health XE session. For more information, see [Change the default error log file directory location](sql-server-linux-configure-mssql-conf.md#change-the-default-error-log-file-directory-location). |
| Memory dumps | `/var/opt/mssql/log` | Used for core dumps and `DBCC CHECK*` dumps. For more information, see [Change the default dump directory location](sql-server-linux-configure-mssql-conf.md#change-the-default-dump-directory-location). |
| Security secrets | `/var/opt/mssql/secrets` | Stores TLS certificates, column master keys, etc. |

### Minimum SQL Server roles for each agent

| Agent | Runs as (Linux) | Connects to | Required database roles/rights |
| --- | --- | --- | --- |
| Snapshot Agent | `mssql` (via SQL Agent job) | Distributor | **db_owner** in distribution database; read/write on snapshot folder |
| Log Reader Agent | `mssql` | Publisher & Distributor | **db_owner** in publication database and distribution. Might need **sysadmin** when using initialize from backup |
| Distribution Agent (push) | `mssql` | Distributor to Subscriber | **db_owner** in distribution; **db_owner** in subscription database. Read snapshot folder. PAL member. |
| Distribution Agent (pull) | `mssql` (on Subscriber) | Subscriber to Distributor<br />Distributor to Subscriber | Same as Distribution Agent (push), but snapshot share permissions apply on Subscriber host |
| Merge Agent | `mssql` | Publisher, Distributor, Subscriber | **db_owner** in distribution. PAL member. Read snapshot folder. Read/write in publication & subscription databases. |
| Queue Reader Agent | `mssql` | Distributor | **db_owner** in distribution. Connects to Publisher with **db_owner** in publication database. |

## Best practices

The `mssql` user and group used by SQL Server is a non-login account by default, and should be kept that way. For security purposes, use Windows authentication for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux where possible. For more information on how to configure Windows authentication for SQL Server on Linux, see [Tutorial: Use adutil to configure Active Directory authentication with SQL Server on Linux](sql-server-linux-ad-auth-adutil-tutorial.md).

Restrict file permissions further (using the command `chmod 700`) whenever the directory doesn't need group access.

When binding host directories or NFS shares into containers or virtual machines, create them first, them, and map UID `10001` (default for `mssql`).

Avoid granting **sysadmin** to application logins. Use granular roles instead, and `EXECUTE AS` wrappers. Disable or rename the `sa` account once you have another **sysadmin** account created. For more information, see [Disable the SA account as a best practice](sql-server-linux-security-overview.md#disable-the-sa-account-as-a-best-practice).

## Related content

- [Configure SQL Server on Linux with the mssql-conf tool](sql-server-linux-configure-mssql-conf.md)
- [SQL Server on Linux frequently asked questions (FAQ)](sql-server-linux-faq.yml)
- [Replication Agent Security Model](../relational-databases/replication/security/replication-agent-security-model.md)
- [Secure the Publisher](../relational-databases/replication/security/secure-the-publisher.md)
