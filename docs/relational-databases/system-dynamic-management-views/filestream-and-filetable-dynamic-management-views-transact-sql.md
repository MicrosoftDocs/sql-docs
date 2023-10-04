---
title: "FILESTREAM and FileTable Dynamic Management Views (Transact-SQL)"
description: FILESTREAM and FileTable Dynamic Management Views (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/02/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "FileTables [SQL Server], dynamic management views"
dev_langs:
  - "TSQL"
---
# FILESTREAM and FileTable dynamic management views (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This section describes the dynamic management views related to the FILESTREAM and FileTable features.

## FILESTREAM dynamic management views and functions

- [sys.dm_filestream_file_io_handles (Transact-SQL)](sys-dm-filestream-file-io-handles-transact-sql.md)

  Displays the currently open transactional file handles.

- [sys.dm_filestream_file_io_requests (Transact-SQL)](sys-dm-filestream-file-io-requests-transact-sql.md)

  Displays current file input and file output requests.

## FileTable dynamic management views and functions

- [sys.dm_filestream_non_transacted_handles (Transact-SQL)](sys-dm-filestream-non-transacted-handles-transact-sql.md)

  Displays the currently open non-transactional file handles to FileTable data.

## Related content

- [FILESTREAM (SQL Server)](../blob/filestream-sql-server.md)
- [FileTables (SQL Server)](../blob/filetables-sql-server.md)
- [FILESTREAM and FileTable Catalog Views (Transact-SQL)](../system-catalog-views/filestream-and-filetable-catalog-views-transact-sql.md)
- [FILESTREAM and FileTable system stored procedures (Transact-SQL)](../system-stored-procedures/filestream-and-filetable-system-stored-procedures.md)
