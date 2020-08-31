---
title: "Backup Files Must Be on Separate Devices from the Database Files"
description: Learn how to enable a policy to check the backup file location compared to the database file location for Policy-Based Management with SQL Server. 
ms.custom: seo-lt-2019
ms.date: "08/31/2020"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
author: dzsquared
ms.author: drskwier
---
# Backup Files Must Be on Separate Devices from the Database Files
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This rule checks whether database files are on devices separate from the backup files. If database files and backup files are on the same device and the device fails, the database and backups will be unavailable. Also, putting the database and backup files on the separate devices optimizes the I/O performance for both the production use of the database and the writing of backups.  
  
## Best Practices Recommendations  
 We recommend that a backup disk be a different disk than the database data and log disks. This is necessary to make sure that you can access the backups if the data or log disk fails.

If database files and backup files are on the same device and the device fails, the database and backups will be unavailable. Also, putting the database and backup files on the separate devices optimizes the I/O performance for both the production use of the database and the writing of backups.
  
## For More Information  
   
  
 [Backup Devices](../backup-restore/backup-devices-sql-server.md)  
  
