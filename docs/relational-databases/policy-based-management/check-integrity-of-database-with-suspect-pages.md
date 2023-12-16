---
title: "Check integrity of database with suspect pages"
description: "Check integrity of database with suspect pages."
author: VanMSFT
ms.author: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Check integrity of database with suspect pages

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rule checks for user databases that have the database status set to suspect. When the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] reads a database page that contains an 824 error, the page is considered suspect, its page ID is recorded in the suspect_pages table in `msdb`, and the database that contains the page is set to suspect.

Error 824 indicates that a logical consistency error was detected during a read operation. This error frequently indicates data corruption caused by a faulty I/O subsystem component. This is a severe error condition that threatens database integrity and must be corrected immediately.

## Best practices recommendations

- Review the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error log for the details of the 824 error for this database.

- Complete a full database consistency check ([DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md)).

- Implement the user actions that are defined in [MSSQLSERVER_824](/previous-versions/sql/sql-server-2016/aa337274(v=sql.130)).

## For more information

[Manage the suspect_pages Table (SQL Server).](../backup-restore/manage-the-suspect-pages-table-sql-server.md)
