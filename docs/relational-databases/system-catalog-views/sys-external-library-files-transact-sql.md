---
title: "sys.external_library_files (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/25/2020"
ms.prod: sql
ms.technology: machine-learning
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
author: dphansen
ms.author: davidph
manager: cgronlun
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=azuresqldb-mi-current||=sqlallproducts-allversions"
---
# sys.external_library_files (Transact-SQL)  
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

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
