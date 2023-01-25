---
description: "MSSQLSERVER_"
title: MSSQLSERVER_4214
ms.custom: ""
ms.date: 08/20/2020
ms.service: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, Masha
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "4214 (Database Engine error)"
ms.assetid: 
author: suresh-kandoth
ms.author: ramakoni
---
# MSSQLSERVER_4214
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|4214|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|DMPX_NODBBACKUP|
|Message Text|BACKUP LOG cannot be performed because there is no current database backup|

## Explanation

The error is raised when you attempt a transaction log backup before doing a full backup of a database. Before you try to back up the transaction log for a database you must perform a full database backup. 
Additionally, you receive the following message in the error log:

> \<Datetime> Backup    Error: 3041, Severity: 16, State: 1.  
\<Datetime>  Backup     BACKUP failed to complete the command BACKUP LOG \<db_name>. Check the backup application log for detailed messages.

## User action

Perform a full database backup before you try to backup the transaction log for a database.

## More information

For more information you can, see: [Back Up and Restore of SQL Server Databases](../backup-restore/back-up-and-restore-of-sql-server-databases.md).
