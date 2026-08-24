---
title: "sys.external_libraries (Transact-SQL)"
description: sys.external_libraries lists a row for each external library that is uploaded into the database.
author: rwestMSFT
ms.author: randolphwest
ms.date: 02/17/2026
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
f1_keywords:
  - "sys.external_libraries_TSQL"
  - "sys.external_libraries"
  - "external_libraries_TSQL"
  - "external_libraries"
helpviewer_keywords:
  - "sys.external_libraries catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-ver15 || =azuresqldb-mi-current"
---
# sys.external_libraries (Transact-SQL)

[!INCLUDE [SQL Server 2017 SQL MI](../../includes/applies-to-version/sqlserver2017-asdbmi.md)]

The `sys.external_libraries` catalog view supports the management of package libraries related to external runtimes such as R, Python, and Java.

`sys.external_libraries` lists a row for each external library that is uploaded into the database.

| Column name | Data type | Description |
| --- | --- | --- |
| `external_library_id` | **int** | ID of the external library object. |
| `name` | **sysname** | Name of the external library. Is unique within the database per owner. |
| `principal_id` | **int** | ID of the principal that owns this external library. |
| `language` | **sysname** | Name of the language or runtime that supports the external library. Valid values are `R`, `Python`, and `Java`. |
| `scope` | **int** | `0` for public scope; `1` for private scope. |
| `scope_desc` | **varchar(7)** | Indicates whether the package is public or private. |

## Remarks

In [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], R language and Windows platform are supported. R, Python, and Java on the Windows and Linux platforms are supported in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later. On Azure SQL Managed Instance, R and Python are supported.

## Related content

- [sys.external_library_files (Transact-SQL)](sys-external-library-files-transact-sql.md)
- [CREATE EXTERNAL LIBRARY (Transact-SQL)](../../t-sql/statements/create-external-library-transact-sql.md)
- [Install R packages with sqlmlutils](../../machine-learning/package-management/install-additional-r-packages-on-sql-server.md)
