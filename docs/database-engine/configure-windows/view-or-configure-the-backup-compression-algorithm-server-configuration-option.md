---
title: "Server Configuration: Backup Compression Algorithm"
description: Find out about the backup compression algorithm option. See how it determines the algorithm to use for backup compression, and learn how to set it.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest, wiassaf, dinethi
ms.date: 11/18/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "backup compression algorithm [SQL Server], backup compression algorithm option"
---

# Server configuration: backup compression algorithm

[!INCLUDE [SQL Server 2022](../../includes/applies-to-version/sqlserver2022-and-later.md)]

This article describes how to view or configure the `backup compression algorithm` server configuration option in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] with [!INCLUDE [tsql](../../includes/tsql-md.md)].

The `backup compression algorithm` option determines which compression algorithm is used by default for backups that use compression. The `backup compression algorithm` configuration option is required for you to implement [integrated acceleration and offloading solutions](../../relational-databases/integrated-acceleration/use-integrated-acceleration-and-offloading.md).

## Prerequisites

- Windows operating system
- [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions

## Permissions

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default. To execute `sp_configure` with both parameters to change a configuration option or to run the `RECONFIGURE` statement, a user must be granted the `ALTER SETTINGS` server-level permission. The `ALTER SETTINGS` permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.

## Backup compression algorithms

You can use the `backup compression algorithm` option to specify the algorithm used for backup compression. The following algorithms are available:

- **MS_XPRESS**: The default backup compression algorithm in all editions of SQL Server.
- **Intel QAT**: The [Intel QuickAssist Technology (QAT)](../../relational-databases/integrated-acceleration/use-integrated-acceleration-and-offloading.md) backup compression algorithm. This algorithm is available in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions.
- **ZSTD**: The backup compression algorithm that uses the faster and more effective Zstandard (ZSTD) compression algorithm. This algorithm is available in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions.

## View the backup compression algorithm option

> [!NOTE]  
> There's currently a [known issue](../../sql-server/sql-server-2025-known-issues.md#issue-when-setting-the-backup-compression-algorithm-to-zstd) with setting the `backup compression algorithm` to ZSTD.

1. In [!INCLUDE [ssmanstudiofull-md](../../includes/ssmanstudiofull-md.md)], connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example queries the [sys.configurations](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md) catalog view to determine the value for `backup compression algorithm`:
   - `0` = Backup compression is off, specified by the [backup compression default](view-or-configure-the-backup-compression-default-server-configuration-option.md) option.
   - `1` = SQL Server uses the MS_XPRESS backup compression algorithm (default).
   - `2` = SQL Server uses the Intel&reg; QAT backup compression algorithm.
   - `3` = SQL Server uses the ZSTD backup compression algorithm.

   ```sql
   SELECT value
   FROM sys.configurations
   WHERE name = 'backup compression algorithm';
   GO
   ```

## Configure the backup compression algorithm option

1. In [!INCLUDE [ssmanstudiofull-md](../../includes/ssmanstudiofull-md.md)], connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to configure the server instance to use Intel&reg; QAT as the default compression algorithm:

   ```sql
   EXECUTE sp_configure 'backup compression algorithm', 2;

   RECONFIGURE;
   ```

   To change the compression algorithm back to the ZSTD algorithm (new in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]), use the following script:

   ```sql
   EXECUTE sp_configure 'backup compression algorithm', 3;

   RECONFIGURE;
   ```

   To change the default compression algorithm back to the default, use the following script:

   ```sql
   EXECUTE sp_configure 'backup compression algorithm', 1;

   RECONFIGURE;
   ```

For more information, see [Server configuration options](server-configuration-options-sql-server.md).

## Related content

- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [sys.sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [Backup overview (SQL Server)](../../relational-databases/backup-restore/backup-overview-sql-server.md)
