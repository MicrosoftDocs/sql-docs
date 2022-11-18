---
title: "Filestream and FileTable Catalog Views (Transact-SQL)"
description: Filestream and FileTable Catalog Views (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "FileTables [SQL Server], catalog views"
dev_langs:
  - "TSQL"
ms.assetid: 2c83a4a7-720b-4435-a3b5-788c29f56949
---
# Filestream and FileTable Catalog Views (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This section describes the catalog views related to the FileTable feature.  
  
## Filestream and filetable Catalog Views (Transact-SQL)
 [sys.database_filestream_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-filestream-options-transact-sql.md)  
 Displays information about the level of non-transactional access to FILESTREAM data in FileTables that is enabled. Contains one row for each database in the SQL Server instance.  
  
 [sys.filetable_system_defined_objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-filetable-system-defined-objects-transact-sql.md)  
 Displays a list of the system-defined objects that are related to FileTables. Contains one row for each system-defined object.  
  
 [sys.filetables &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-filetables-transact-sql.md)  
 Returns a row for each FileTable. Inherits from **sys.tables**.  

## See Also
[Filestream](../../relational-databases/blob/filestream-sql-server.md)
<br>[Filetables](../../relational-databases/blob/filetables-sql-server.md)
<br>[Filestream and FileTable Dynamic Management Views (Transact-SQL)](../system-dynamic-management-views/filestream-and-filetable-dynamic-management-views-transact-sql.md)
<br>[Filestream and FileTable System Stored Procedures (Transact-SQL)](../system-stored-procedures/filestream-and-filetable-system-stored-procedures.md)
  
  
