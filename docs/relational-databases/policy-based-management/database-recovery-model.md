---
title: "Database recovery model best practice policy"
description: Learn how to enable a policy to check the backup recovery model for user databases to reduce data loss. 
ms.custom:
- seo-lt-2019
- kr2b-contr-experiment
ms.date: 05/10/2022
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
author: dzsquared
ms.author: drskwier
---

# Database recovery model best practice policy

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

For SQL Server Enterprise and Standard editions, this rule checks for non-read-only user databases that have recovery set to simple. For production databases, we recommend that you use the full recovery model instead of the simple recovery model. Full recovery mode enables point-in-time recovery. This approach helps reduce data loss if there's a disaster recovery.
  
## Best practices recommendations  

Production databases should be in full recovery mode. The transaction log should be backed up frequently to help ensure recoverability with minimum data loss.
  
## See also

- [Restore and Recovery Overview](../backup-restore/restore-and-recovery-overview-sql-server.md)
- [Complete Database Restores (Full Recovery Model)](../backup-restore/complete-database-restores-full-recovery-model.md)  
- [Transaction Log Backups](../backup-restore/transaction-log-backups-sql-server.md)
