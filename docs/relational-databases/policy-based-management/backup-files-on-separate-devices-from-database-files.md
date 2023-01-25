---
title: "Backup files must be on separate devices from database files"
description: Learn how to enable a policy to check the backup file location compared to the database file location for Policy-Based Management with SQL Server. 
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

# Backup files must be on separate devices from the database files

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rule checks whether database files are on devices separate from the backup files. If database files and backup files are on the same device and the device fails, the database and backups are unavailable. Also, putting the database and backup files on the separate devices optimizes the I/O performance for both the production use of the database and the writing of backups.  
  
## Best practices recommendations

We recommend that a backup disk is a different disk than the database data and log disks. This precaution is necessary to make sure that you can access the backups if the data or log disk fails.

If database files and backup files are on the same device and the device fails, the database and backups are unavailable. Also, putting the database and backup files on the separate devices optimizes the I/O performance for both the production use of the database and the writing of backups.
  
## See also

[Backup Devices](../backup-restore/backup-devices-sql-server.md)  
