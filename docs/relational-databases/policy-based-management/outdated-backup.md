---
title: "Outdated Backup"
description: "Outdated Backup"
author: VanMSFT
ms.author: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Outdated backup

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rule checks that a database has recent backups. Scheduling regular backups is important for protecting your databases against data loss from many different failures. The appropriate frequency for backing up data depends on the recovery model of the database, on business requirements about potential data loss, and on how frequently the database is updated. In a frequently updated database, the work-loss exposure increases fairly quickly between backups.

## Best practices recommendations

We recommend that you perform backups frequently enough to protect databases against data loss.

The simple recovery model and full recovery model both require data backups. For either recovery model, you can supplement your full backups with differential backups to efficiently reduce the risk of data loss.

For a database that uses the full recovery model, we recommend that you take frequent log backups. For a production database that contains important data, log backups would typically be taken every one to fifteen minutes.

> [!NOTE]  
> The recommended method for scheduling backups is a database maintenance plan.

## For more information

[Back up and restore: System databases (SQL Server)](../backup-restore/back-up-and-restore-of-system-databases-sql-server.md)

[Recovery Models (SQL Server)](../backup-restore/recovery-models-sql-server.md)

[Create a Differential Database Backup (SQL Server)](../backup-restore/create-a-differential-database-backup-sql-server.md)

[Create a Full Database Backup](../backup-restore/create-a-full-database-backup-sql-server.md)

[Maintenance plans](../maintenance-plans/maintenance-plans.md)

[Transaction log backups (SQL Server)](../backup-restore/transaction-log-backups-sql-server.md)

## Related content

- [Monitor and Enforce Best Practices by Using Policy-Based Management](monitor-and-enforce-best-practices-by-using-policy-based-management.md)
