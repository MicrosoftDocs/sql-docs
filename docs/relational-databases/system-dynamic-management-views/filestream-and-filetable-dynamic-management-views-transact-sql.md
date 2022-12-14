---
title: "Filestream and FileTable Dynamic Management Views (Transact-SQL)"
description: Filestream and FileTable Dynamic Management Views (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "FileTables [SQL Server], dynamic management views"
dev_langs:
  - "TSQL"
ms.assetid: e50a135d-6644-42a4-a0df-1c7a2b722051
---
# Filestream and FileTable Dynamic Management Views (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This section describes the dynamic management views related to the FILESTREAM and FileTable features.  
  
## Filestream Dynamic Management Views and Functions  
 [sys.dm_filestream_file_io_handles &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-filestream-file-io-handles-transact-sql.md)  
 Displays the currently open transactional file handles.  
  
 [sys.dm_filestream_file_io_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-filestream-file-io-requests-transact-sql.md)  
 Displays current file input and file output requests.  
  
## FileTable Dynamic Management Views and Functions  
 [sys.dm_filestream_non_transacted_handles &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-filestream-non-transacted-handles-transact-sql.md)  
 Displays the currently open non-transactional file handles to FileTable data.  

## See Also
[Filestream](../../relational-databases/blob/filestream-sql-server.md)
<br>[Filetables](../../relational-databases/blob/filetables-sql-server.md)
<br>[Filestream and FileTable Catalog Views (Transact-SQL)](../system-catalog-views/filestream-and-filetable-catalog-views-transact-sql.md)
<br>[Filestream and FileTable System Stored Procedures (Transact-SQL)](../system-stored-procedures/filestream-and-filetable-system-stored-procedures.md)
  
