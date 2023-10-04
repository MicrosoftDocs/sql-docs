---
title: "sys.external_libraries (Transact-SQL)"
description: sys.external_libraries (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/25/2020
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
f1_keywords:
  - "external_libraries"
  - "external_libraries_TSQL"
  - "sys.external_libraries"
  - "sys.external_libraries_TSQL"
helpviewer_keywords:
  - "sys.external_libraries catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# sys.external_libraries (Transact-SQL)  
[!INCLUDE [SQL Server 2017 SQL MI](../../includes/applies-to-version/sqlserver2017-asdbmi.md)]

Supports the management of package libraries related to external runtimes such as R, Python, and Java.

> [!NOTE]
> In SQL Server 2017, R language and Windows platform are supported. R, Python, and Java on the Windows and Linux platforms are supported in SQL Server 2019 and later. On Azure SQL Managed Instance, R and Python are supported.

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
+ [Install new R packages](../../machine-learning/package-management/install-additional-r-packages-on-sql-server.md)  
+ [Install new Python packages](../../machine-learning/package-management/install-additional-python-packages-on-sql-server.md)  
