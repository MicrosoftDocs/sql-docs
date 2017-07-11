---
title: "DROP EXTERNAL FILE FORMAT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 8cf9009b-59f9-4aac-bef1-dcf2cf0708b2
caps.latest.revision: 12
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# DROP EXTERNAL FILE FORMAT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-asdw-pdw_md](../../includes/tsql-appliesto-ss2016-xxxx-asdw-pdw-md.md)]

  Removes a PolyBase external file format.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
-- Drop an external file format  
DROP EXTERNAL FILE FORMAT external_file_format_name  
[;]  
```  
  
## Arguments  
 *external_file_format_name*  
 The name of the external file format to drop.  
  
## Metadata  
 To view a list of external file formats use the [sys.external_file_formats &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-external-file-formats-transact-sql.md) system view.  
  
```  
SELECT * FROM sys.external_file_formats;  
```  
  
## Permissions  
 Requires ALTER ANY EXTERNAL FILE FORMAT.  
  
## General Remarks  
 Dropping an external file format does not remove the external data.  
  
## Locking  
 Takes a shared lock on the external file format object.  
  
## Examples  
  
### A. Using basic syntax  
  
```  
DROP EXTERNAL FILE FORMAT myfileformat;  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### B. Using basic syntax  
  
```  
DROP EXTERNAL FILE FORMAT myfileformat;  
```  
  
## See Also  
 [CREATE EXTERNAL FILE FORMAT &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-file-format-transact-sql.md)  
  
  

