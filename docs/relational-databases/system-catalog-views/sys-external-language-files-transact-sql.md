---
title: "sys.external_language_files (Transact-SQL) - SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: 05/22/2019
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "external_languages"
  - "external_languages_TSQL"
  - "sys.external_languages"
  - "sys.external_languages_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.external_languages catalog view"
author: nelgson
ms.author: negust
ms.reviewer: dphansen
manager: cgronlun
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---

# sys.external_language_files (Transact-SQL)
[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This catalog view provides a list of the external language extension files in the database. **R** and **Python** are reserved names and no external language can be created with those specific names.

When an external language is created from a file_spec, the extension itself and its properties are listed in this view. This view will contain one entry per language, per OS.

## sys.external_languages

The catalog view sys.external_language_files lists a row for each external language extension in the database. Parameters

|Column name |Data type | Description|
|------|------|------|
|external_language_id |int | ID of the external language|
|content|varbinary(max) |Content of the external language extension file|
|file_name|nvarchar(266)|Name of the language extension file|
|platform|tinyint|ID of the host platform on which SQL Server is installed|
|platform_desc |nvarchar(60)|Name of the host platform. Valid values are 'WINDOWS', 'LINUX'.|
|parameters|nvarchar(4000)|External language prameters|
|environment_variables |nvarchar(4000)|External language environment variables|

## See also  

+ [sys.external_languages](sys-external-languages-transact-sql.md)  
+ [CREATE EXTERNAL LANGUAGE](../../t-sql/statements/create-external-language-transact-sql.md)  
