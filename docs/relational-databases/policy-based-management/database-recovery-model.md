---
title: "Database recovery model best practice policy"
description: Learn how to enable a policy to check the backup recovery model for user databases to reduce data loss.
author: dzsquared
ms.author: drskwier
ms.reviewer: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: reference
ms.custom:
  - kr2b-contr-experiment
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---

# Database recovery model best practice policy

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

For SQL Server Enterprise and Standard editions, this rule checks for non-read-only user databases that have recovery set to simple. For production databases, we recommend that you use the full recovery model instead of the simple recovery model. The full recovery model enables point-in-time recovery. This approach helps reduce data loss if there's a disaster recovery.

## Best practices recommendations

Production databases should be in the full recovery model. The transaction log should be backed up frequently to help ensure recoverability with minimum data loss.

## Related content

- [Restore and recovery overview (SQL Server)](../backup-restore/restore-and-recovery-overview-sql-server.md)
- [Complete Database Restores (Full Recovery Model)](../backup-restore/complete-database-restores-full-recovery-model.md)
- [Transaction log backups (SQL Server)](../backup-restore/transaction-log-backups-sql-server.md)
