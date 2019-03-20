---
title: "sys.external_libraries (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date:03/25/201903/19/2019
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "external_libraries"
  - "external_libraries_TSQL"
  - "sys.external_libraries"
  - "sys.external_libraries_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.external_libraries catalog view"
author: dphansen
ms.author: davidph
manager: cgronlun
monikerRange: ">=sql-server-2017||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.external_libraries (Transact-SQL)  
[!INCLUDE[tsql-appliesto-ss2017-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2017-xxxx-xxxx-xxx-md.md)]

Supports the management of package libraries related to external runtimes such as R, Python, and Java.

> [!NOTE]
> In SQL Server 2017, R language and Windows platform are supported. R, Python, and Java on the Windows and Linux platforms are supported in SQL Server 2019 CTP 2.4.

## sys.external_libraries

The catalog view sys.external_libraries lists a row for each external library that has been uploaded into the database.

|Column name |Data type | Description|
|------|------|------|
|external_library_id |int | ID of the external library object. |
|name |sysname |Name of the external library. Is unique within the database per owner.|
|principal_id |int |ID of the principal that owns this external library. |
|language | sysname | Name of the language or runtime that supports the external library. Valid values are 'R', 'Python', and 'Java'. Additional runtimes might be added in future.|
|scope |int |0 for public scope; 1 for private scope |  
|scope_desc |varchar(7) |Indicates whether the package is public or private|

## See also  

+ [sys.external_library_files](sys-external-library-files-transact-sql.md)  
+ [CREATE EXTERNAL LIBRARY](../../t-sql/statements/create-external-library-transact-sql.md)  
+ [Install new R packages on SQL Server](../../advanced-analytics/r/install-additional-r-packages-on-sql-server.md)  
+ [Install new Python packages on SQL Server](../../advanced-analytics/python/install-additional-python-packages-on-sql-server.md)  