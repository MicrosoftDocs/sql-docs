---
title: Restore the Service Master Key
description: Learn how to restore the service master key in SQL Server by using Transact-SQL. The service master key is the root of the SQL Server encryption hierarchy.
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto, randolphwest
ms.date: 07/22/2026
ms.service: sql
ms.subservice: security
ms.topic: how-to
helpviewer_keywords:
  - "service master key [SQL Server], importing"
  - "service master key [SQL Server], restoring"
---

# Restore a service master key

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

This article describes how to restore the service master key in [!INCLUDE [ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE [tsql](../../../includes/tsql-md.md)].

> [!WARNING]  
> It's unlikely that you ever need to restore the service master key. If you do, proceed with extreme caution. For more information, see [Back up a service master key](back-up-the-service-master-key.md).

## Remarks

When you restore the service master key, [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] decrypts all the keys and secrets encrypted with the current service master key, and then encrypts them with the service master key loaded from the backup file.

If any one of the decryptions fails, the restore fails. You can use the `FORCE` option to ignore errors, but this option causes the loss of any data that can't be decrypted.

Regenerating the encryption hierarchy is a resource-intensive operation. Schedule this operation during a period of low demand.

> [!CAUTION]  
> The service master key is the root of the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] encryption hierarchy. The service master key directly or indirectly secures all other keys in the tree. If a dependent key can't be decrypted during a forced restore, data secured by that key is lost.

## Permissions

Requires `CONTROL SERVER` permission on the server.

## Restore the service master key

1. Retrieve a copy of the backed-up service master key, either from a physical backup medium or a directory on the local file system.

1. Connect to the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] instance where you want to restore the service master key. [!INCLUDE [connect-instance-client](../../../includes/connect-instance-client.md)]

1. Review and run the following Transact-SQL script.

   In this example, you restore the service master key from `C:\Backups\Keys\service_master_key`. Change these settings to match your environment.

   ```sql
   USE master;
   GO

   RESTORE SERVICE MASTER KEY
       FROM FILE = 'C:\Backups\Keys\service_master_key'
       DECRYPTION BY PASSWORD = '<password>';
   GO
   ```

## Related content

- [Back up a service master key](back-up-the-service-master-key.md)
- [RESTORE SERVICE MASTER KEY (Transact-SQL)](../../../t-sql/statements/restore-service-master-key-transact-sql.md)
