---
title: "sys.external_languages (Transact-SQL) - SQL Server"
description: sys.external_languages (Transact-SQL) - SQL Server
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

# sys.external_languages (Transact-SQL)
[!INCLUDE[SQL Server 2019](../../includes/applies-to-version/sqlserver2019.md)]

This catalog view provides a list of the external languages in the database. **R** and **Python** are reserved names and no external language can be created with those specific names.

## sys.external_languages

The catalog view sys.external_languages lists a row for each external language in the database.

|Column name |Data type | Description|
|------|------|------|
|external_language_id |int | ID of the external language|
|language |sysname |Name of the external language. Is unique within the database. R and Python are reserved names per instance|
|create_date |datetime2 |Date and time of creation|
|principal_id |int |ID of the principal that owns this external library|

## See also  

+ [sys.external_language_files](sys-external-language-files-transact-sql.md)  
+ [CREATE EXTERNAL LANGUAGE](../../t-sql/statements/create-external-language-transact-sql.md) 
