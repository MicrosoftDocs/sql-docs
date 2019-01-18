---
title: "Bulk Import Large-Object Data by using the OPENROWSET Bulk Rowset Provider (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "SINGLE_NCLOB option"
  - "bulk rowset providers [SQL Server]"
  - "bulk importing [SQL Server], data formats"
  - "OPENROWSET function, bulk importing large data"
  - "SINGLE_CLOB option"
  - "data formats [SQL Server], large-object data"
  - "large data, bulk imports"
  - "SINGLE_BLOB option"
ms.assetid: 171cdd5c-1e47-4bd7-b99a-4f0fd4e10526
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Bulk Import Large-Object Data by using the OPENROWSET Bulk Rowset Provider (SQL Server)
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
 [Import Bulk Data by Using BULK INSERT or OPENROWSET&#40;BULK...&#41; &#40;SQL Server&#41;](import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md)   
 [BACKUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-transact-sql)   
 [OPENROWSET &#40;Transact-SQL&#41;](/sql/t-sql/functions/openrowset-transact-sql)   
 [Keep Nulls or Use Default Values During Bulk Import &#40;SQL Server&#41;](keep-nulls-or-use-default-values-during-bulk-import-sql-server.md)   
 [bcp Utility](../../tools/bcp-utility.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/bulk-insert-transact-sql)  
  
  
