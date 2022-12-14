---
title: "Bulk import large-object data with OPENROWSET bulk rowset provider"
description: The SQL Server OPENROWSET Bulk Rowset Provider enables bulk import as large-object data. Supported types are varbinary(max), varchar(max), and nvarchar(max).
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: data-movement
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "SINGLE_NCLOB option"
  - "bulk rowset providers [SQL Server]"
  - "bulk importing [SQL Server], data formats"
  - "OPENROWSET function, bulk importing large data"
  - "SINGLE_CLOB option"
  - "data formats [SQL Server], large-object data"
  - "large data, bulk imports"
  - "SINGLE_BLOB option"
---
# Bulk import large-object data with OPENROWSET Bulk Rowset Provider (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] OPENROWSET Bulk Rowset Provider enables you to bulk import a data file as large-object data.  
  
 The large-object data types supported by OPENROWSET Bulk Rowset Provider are **varbinary**(max) or **image**, **varchar**(max) or **text**, and **nvarchar**(max) or **ntext**.  
  
> [!NOTE]  
>  The **image**, **text** and **ntext** data types are deprecated.  
  
 The OPENROWSET BULK clause supports three options for importing the contents of a data file as a single-row, single-column rowset. You can specify one of these large-object options instead of using a format file. These options are as follows:  
  
 SINGLE_BLOB  
 Reads the contents of *data_file* as a single-row, returns the contents as a single-column rowset of type **varbinary(max)**.  
  
 SINGLE_CLOB  
 Reads the contents of the specified data file as characters, returns the contents as a single-row, single-column rowset of type **varchar(max)**, using the collation of the current database; such as a text or [!INCLUDE[msCoName](../../includes/msconame-md.md)] Word document.  
  
 SINGLE_NCLOB  
 Reads the contents of the specified data file as Unicode, returns the contents as a single-row, single-column rowset of type **nvarchar(max)**, using the collation of the current database.  
  
## See Also  
 [Import Bulk Data by Using BULK INSERT or OPENROWSET&#40;BULK...&#41; &#40;SQL Server&#41;](../../relational-databases/import-export/import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md)   
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md)   
 [Keep Nulls or Use Default Values During Bulk Import &#40;SQL Server&#41;](../../relational-databases/import-export/keep-nulls-or-use-default-values-during-bulk-import-sql-server.md)   
 [bcp Utility](../../tools/bcp-utility.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/bulk-insert-transact-sql.md)  
  
  
