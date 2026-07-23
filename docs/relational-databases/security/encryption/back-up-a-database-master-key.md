---
title: Back up a Database Master Key
description: Learn how to back up a database master key in SQL Server by using Transact-SQL. This essential key encrypts other keys and certificates.
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto, randolphwest
ms.date: 07/22/2026
ms.service: sql
ms.subservice: security
ms.topic: how-to
helpviewer_keywords:
  - "database master key [SQL Server], exporting"
---

# Back up a database master key

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

This article describes how to back up a *database master key* in [!INCLUDE [ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE [tsql](../../../includes/tsql-md.md)]. The database master key encrypts other keys and certificates inside a database. If it's deleted or corrupted, [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] might be unable to decrypt those keys, and the data encrypted with them is effectively lost. For this reason, back up the database master key and store the backup in a secure off-site location.

## Limitations

You must open and decrypt the database master key before backing it up. If the service master key encrypts the database master key, you don't need to explicitly open the database master key. However, if a password is the only encryption method for the database master key, you must explicitly open it.

Back up the database master key as soon as you create it, and store the backup in a secure, off-site location.

## Permissions

Requires `CONTROL` permission on the database.

## Back up the database master key

[!INCLUDE [article-uses-adventureworks](../../../includes/article-uses-adventureworks.md)]

1. Connect to the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] instance containing the database master key you want to back up. [!INCLUDE [connect-instance-client](../../../includes/connect-instance-client.md)]

1. Choose a unique password for encrypting the database master key on the backup medium. [!INCLUDE [password-complexity](../../../linux/includes/password-complexity.md)]

1. Get a removable backup medium for storing a copy of the backed-up key.

1. Identify an NTFS directory where you create the backup of the key. This directory is where you create the file in the next step. Protect the directory with highly restrictive access control lists (ACLs).

1. Review and run the following Transact-SQL script.

   In this example:

   - The service master key doesn't encrypt the database master key, so you must specify a password when opening it.

   - You back up the database master key for [!INCLUDE [sssampledbobject-md](../../../includes/sssampledbobject-md.md)] to `C:\Backups\Keys\AdventureWorks2025_master_key`. Change these settings to match your environment.

   > [!CAUTION]  
   > You need the password to restore a database backup. Make sure you store this password safely and securely.

   ```sql
   USE AdventureWorks2025;
   GO

   OPEN MASTER KEY DECRYPTION BY PASSWORD = '<dmk-password>';

   BACKUP MASTER KEY TO FILE = 'C:\Backups\Keys\AdventureWorks2025_master_key'
       ENCRYPTION BY PASSWORD = '<password>';
   GO
   ```

1. Copy the backed up file to the backup medium and verify the copy.

1. Store the backup in a secure, off-site location.

## Related content

- [Create a database master key](create-a-database-master-key.md)
- [Restore a database master key](restore-a-database-master-key.md)
- [OPEN MASTER KEY (Transact-SQL)](../../../t-sql/statements/open-master-key-transact-sql.md)
- [BACKUP MASTER KEY (Transact-SQL)](../../../t-sql/statements/backup-master-key-transact-sql.md)
