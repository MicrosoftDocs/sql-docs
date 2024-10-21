---
title: "Server configuration: backup compression algorithm"
description: "Find out about the backup compression algorithm option. See how it determines the algorithm to use for backup compression, and learn how to set it."
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 10/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "backup compression algorithm [SQL Server], backup compression algorithm Option"
---

# Server configuration: backup compression algorithm

[!INCLUDE [SQL Server 2022](../../includes/applies-to-version/sqlserver2022.md)]

This article describes how to view or configure the `backup compression algorithm` server configuration option in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] with [!INCLUDE [tsql](../../includes/tsql-md.md)]. The `backup compression algorithm` option determines which algorithm to use to set the backup.

The `backup compression algorithm` configuration option is required for you to implement [integrated acceleration and offloading solutions](../../relational-databases/integrated-acceleration/use-integrated-acceleration-and-offloading.md).

## Prerequisites

- Windows operating system
- [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]

## Permissions

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default. To execute `sp_configure` with both parameters to change a configuration option or to run the `RECONFIGURE` statement, a user must be granted the `ALTER SETTINGS` server-level permission. The `ALTER SETTINGS` permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.

## View the backup compression algorithm option

1. In [!INCLUDE [ssmanstudiofull-md](../../includes/ssmanstudiofull-md.md)], connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example queries the [sys.configurations](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md) catalog view to determine the value for `backup compression algorithm`. A value of `0` means that backup compression is off, and a value of `1` means that SQL Server uses the default backup compression algorithm (MS_XPRESS). A value of `2` means that SQL Server uses the Intel&reg QAT backup compression algorithm.

   ```sql
   SELECT value
   FROM sys.configurations
   WHERE name = 'backup compression algorithm';
   GO
   ```

## Configure the backup compression default option

1. In [!INCLUDE [ssmanstudiofull-md](../../includes/ssmanstudiofull-md.md)], connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to configure the server instance to use Intel&reg; QAT as the default compression algorithm:

   ```sql
   EXECUTE sp_configure 'backup compression algorithm', 2;

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
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [Backup overview (SQL Server)](../../relational-databases/backup-restore/backup-overview-sql-server.md)
