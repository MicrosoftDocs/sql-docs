---
title: Restore a Database Master Key
description: Learn how to restore a database master key in SQL Server by using Transact-SQL. This essential key encrypts other keys and certificates.
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto, randolphwest
ms.date: 07/22/2026
ms.service: sql
ms.subservice: security
ms.topic: how-to
helpviewer_keywords:
  - "database master key [SQL Server], importing"
---

# Restore a database master key

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

This article describes how to restore a *database master key* in [!INCLUDE [ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE [tsql](../../../includes/tsql-md.md)]. The database master key encrypts other keys and certificates inside a database. If it's deleted or corrupted, [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] might be unable to decrypt those keys, and the data encrypted with them is effectively lost. Restore the database master key from a backup to recover access to those keys.

## Limitations

When you restore the database master key, [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] decrypts all the keys encrypted with the currently active database master key, and then encrypts them with the restored database master key. Schedule this resource-intensive operation during a period of low demand. If the current database master key isn't open or can't be opened, or if any of the keys encrypted by it can't be decrypted, the restore operation fails.

If any one of the decryptions fails, the restore fails. You can use the `FORCE` option to ignore errors, but this option causes the loss of any data that can't be decrypted.

If the service master key encrypts the database master key, it also encrypts the restored database master key.

If there's no database master key in the current database, `RESTORE MASTER KEY` creates a database master key. The service master key doesn't automatically encrypt the new database master key.

## Permissions

Requires `CONTROL` permission on the database.

## Restore the database master key

[!INCLUDE [article-uses-adventureworks](../../../includes/article-uses-adventureworks.md)]

1. Retrieve a copy of the backed-up database master key, either from a physical backup medium or a directory on the local file system.

1. Connect to the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] instance containing the database master key you want to restore. [!INCLUDE [connect-instance-client](../../../includes/connect-instance-client.md)]

1. Review and run the following Transact-SQL script.

   In this example:

   - You restore the database master key for [!INCLUDE [sssampledbobject-md](../../../includes/sssampledbobject-md.md)] from `C:\Temp\AdventureWorks2025_master_key`.

   - You set a new encryption password `<new-password>`. Change these settings to match your environment.

   > [!CAUTION]  
   > You need the new password to open the database master key in the future. Make sure you store this password safely and securely.

   ```sql
   USE AdventureWorks2025;
   GO

   RESTORE MASTER KEY
       FROM FILE = 'C:\Backups\Keys\AdventureWorks2025_master_key'
       DECRYPTION BY PASSWORD = '<password>'
       ENCRYPTION BY PASSWORD = '<new-password>';
   GO
   ```

## Related content

- [Create a database master key](create-a-database-master-key.md)
- [Back up a database master key](back-up-a-database-master-key.md)
- [RESTORE MASTER KEY (Transact-SQL)](../../../t-sql/statements/restore-master-key-transact-sql.md)
