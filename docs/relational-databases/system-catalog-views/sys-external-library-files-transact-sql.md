---
title: "sys.external_library_files (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "external_library_files"
  - "external_library_files_TSQL"
  - "sys.external_library_files"
  - "sys.external_library_files_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.external_library_files catalog view"
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# sys.external_library_files (Transact-SQL)  
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]  

Lists a row for each file that makes up an external library.

|Column name |Data type |Description|
|------|------|-----|
|external_library_id | int |ID of the external library object. |
|content |varbinary(max) |Content of the external library file artifact. |
|platform |tinyint |ID of the host platform on which SQL Server is installed. |
|platform_desc | nvarchar(60) |Name of the host platform. Valid values are 'WINDOWS', 'LINUX'. |

### See also  
[sys.external_libraries](sys-external-libraries-transact-sql.md)  
[CREATE EXTERNAL LIBRARY](../../t-sql/statements/create-external-library-transact-sql.md)
[Package management for SQL Server R Services](../../advanced-analytics/r/installing-and-managing-r-packages.md)  
