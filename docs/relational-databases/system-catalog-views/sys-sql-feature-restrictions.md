---
title: "sys.sql_feature_restrictions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/07/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.sql_sql_feature_restrictions"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sql_feature_restrictions catalog view"
author: vainolo
ms.author: arib
manager: tomerw
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---
# sys.sql_feature_restrictions (Transact-SQL)

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

Returns one row for every restriction in the database.
  
| Column name | Data type | Description |
|-------------|-----------|-------------|
| class       | nvarchar(128) | Class of object to which the restriction applies |
| object      | nvarchar(256) | Name of object to which the restriction applies |
| feature     | nvarchar(128) | Feature that is restricted |
  
## Remarks

Currently the following features can be restricted:

| Feature          | Description |
|------------------|-------------|
| N'ErrorMessages' | When restricted, any user data within the error message will be masked. |
| N'Waitfor'       | When restricted, the command will return immediately without any delay. |
  
## Permissions

The executing principal must have the `CONTROL` permission on the database.
  
## See Also

 [Feature Restrictions](../../relational-databases/security/feature-restrictions.md)
