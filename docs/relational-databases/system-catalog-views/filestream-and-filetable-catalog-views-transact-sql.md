---
title: "FILESTREAM and FileTable Catalog Views (Transact-SQL)"
description: FILESTREAM and FileTable Catalog Views (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/02/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "FileTables [SQL Server], catalog views"
dev_langs:
  - "TSQL"
---
# FILESTREAM and FileTable catalog views (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This section describes the catalog views related to the FileTable feature.

## FILESTREAM and FileTable catalog views

- [sys.database_filestream_options (Transact-SQL)](sys-database-filestream-options-transact-sql.md)

  Displays information about the level of non-transactional access to FILESTREAM data in FileTables that is enabled. Contains one row for each database in the SQL Server instance.

- [sys.filetable_system_defined_objects (Transact-SQL)](sys-filetable-system-defined-objects-transact-sql.md)

  Displays a list of the system-defined objects that are related to FileTables. Contains one row for each system-defined object.

- [sys.filetables (Transact-SQL)](sys-filetables-transact-sql.md)

  Returns a row for each FileTable. Inherits from `sys.tables`.

## Related content

- [FILESTREAM (SQL Server)](../blob/filestream-sql-server.md)
- [FileTables (SQL Server)](../blob/filetables-sql-server.md)
- [FILESTREAM and FileTable Dynamic Management Views (Transact-SQL)](../system-dynamic-management-views/filestream-and-filetable-dynamic-management-views-transact-sql.md)
- [FILESTREAM and FileTable system stored procedures (Transact-SQL)](../system-stored-procedures/filestream-and-filetable-system-stored-procedures.md)
