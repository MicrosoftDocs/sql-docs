---
title: "sys.external_language_files (Transact-SQL) - SQL Server"
description: sys.external_language_files (Transact-SQL) - SQL Server
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: rothja
ms.date: 05/22/2019
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "external_languages"
  - "external_languages_TSQL"
  - "sys.external_languages"
  - "sys.external_languages_TSQL"
helpviewer_keywords:
  - "sys.external_languages catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver15"
---

# sys.external_language_files (Transact-SQL)
[!INCLUDE[SQL Server 2019](../../includes/applies-to-version/sqlserver2019.md)]

This catalog view provides a list of the external language extension files in the database. **R** and **Python** are reserved names and no external language can be created with those specific names.

When an external language is created from a file_spec, the extension itself and its properties are listed in this view. This view will contain one entry per language, per OS.

## sys.external_language_files

The catalog view sys.external_language_files lists a row for each external language extension in the database. Parameters

|Column name |Data type | Description|
|------|------|------|
|external_language_id |int | ID of the external language|
|content|varbinary(max) |Content of the external language extension file|
|file_name|sysname|Name of the language extension file|
|platform|tinyint|ID of the host platform on which SQL Server is installed|
|platform_desc |nvarchar(60)|Name of the host platform. Valid values are `WINDOWS`, `LINUX`.|
|parameters|sysname|External language parameters|
|environment_variables|sysname|External language environment variables|

## See also  

+ [sys.external_languages](sys-external-languages-transact-sql.md)  
+ [CREATE EXTERNAL LANGUAGE](../../t-sql/statements/create-external-language-transact-sql.md)  
