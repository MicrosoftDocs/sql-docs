---
title: "FILESTREAM and FileTable System stored procedures (Transact-SQL)"
description: "FILESTREAM and FileTable System stored procedures (Transact-SQL)"
author: "MashaMSFT"
ms.author: "mathoma"
ms.reviewer: randolphwest
ms.date: 05/24/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "FileTables [SQL Server], catalog views"
dev_langs:
  - "TSQL"
---
# FILESTREAM and FileTable system stored procedures (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This section describes the system stored procedures to the FileTable and FILESTREAM feature.

## FILESTREAM and FileTable system stored procedures

- [sp_filestream_force_garbage_collection (Transact-SQL)](filestream-and-filetable-sp-filestream-force-garbage-collection.md)

  Forces the FILESTREAM garbage collector to run, deleting any unneeded FILESTREAM files.

- [sp_kill_filestream_non_transacted_handles (Transact-SQL)](filestream-and-filetable-sp-kill-filestream-non-transacted-handles.md)

  Closes non-transactional file handles to FileTable data.

## Related content

- [FILESTREAM (SQL Server)](../blob/filestream-sql-server.md)
- [FileTables (SQL Server)](../blob/filetables-sql-server.md)
- [FILESTREAM and FileTable Dynamic Management Views (Transact-SQL)](../system-dynamic-management-views/filestream-and-filetable-dynamic-management-views-transact-sql.md)
- [FILESTREAM and FileTable Catalog Views (Transact-SQL)](../system-catalog-views/filestream-and-filetable-catalog-views-transact-sql.md)
