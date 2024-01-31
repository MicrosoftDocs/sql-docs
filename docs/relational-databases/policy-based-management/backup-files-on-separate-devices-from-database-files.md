---
title: "Backup files must be on separate devices from database files"
description: Learn how to enable a policy to check the backup file location compared to the database file location for Policy-Based Management with SQL Server.
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

# Backup files must be on separate devices from the database files

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rule checks whether database files are on devices separate from the backup files. If database files and backup files are on the same device and the device fails, the database and backups are unavailable. Also, putting the database and backup files on the separate devices optimizes the I/O performance for both the production use of the database and the writing of backups.

## Best practices recommendations

We recommend that a backup disk is a different disk than the database data and log disks. This precaution is necessary to make sure that you can access the backups if the data or log disk fails.

If database files and backup files are on the same device and the device fails, the database and backups are unavailable. Also, putting the database and backup files on the separate devices optimizes the I/O performance for both the production use of the database and the writing of backups.

## Related content

- [Backup Devices (SQL Server)](../backup-restore/backup-devices-sql-server.md)
