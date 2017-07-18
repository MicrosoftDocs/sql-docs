---
title: "sys.external_libraries (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/17/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
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
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# sys.external_libraries (Transact-SQL)  
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]  


Supports the management of package libraries related to external runtimes such as R or Python.

## sys.external_libraries

The catalog view sys.external_libraries lists a row for each external library that has been uploaded into the database.

|Column name |Data type | Description|
|------|------|------|
|external_library_id |int | ID of the external library object. |
|name |sysname |Name of the external library. Is unique within the database per owner.|
|principal_id |int |ID of the principal that owns this external library. |
|language | sysname | Name of the language or runtime that supports the external library. Valid values are 'R'. Additional runtimes might be added in future.|
|scope |int |TBD |  
|scope_desc |varchar(7) |TBD |


## See also  
sys.external_library_files  
[Package management for SQL Server R Services](../advanced-analytics/r/installing-and-managing-r-packages.md)  
