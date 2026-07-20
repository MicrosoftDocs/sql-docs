---
title: sys.fulltext_document_types (Transact-SQL)
description: sys.fulltext_document_types returns a row for each document type that is available for full-text indexing operations.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/09/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.fulltext_document_types_TSQL"
  - "sys.fulltext_document_types"
  - "fulltext_document_types_TSQL"
  - "fulltext_document_types"
helpviewer_keywords:
  - "sys.fulltext_document_types catalog view"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.fulltext_document_types (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Returns a row for each document type that is available for full-text indexing operations. Each row represents the IFilter interface that is registered in the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

| Column name | Data type | Description |
| --- | --- | --- |
| `document_type` | **sysname** | The file extension of the supported document type.<br /><br />This value can be used to identify the filter that will be used during full-text indexing of columns of type **varbinary(max)** or **image**. |
| `class_id` | **uniqueidentifier** | GUID of the IFilter class that supports file extension. |
| `path` | **nvarchar(260)** | The path to the IFilter DLL. The path is only visible to members of the **serveradmin** fixed server role. |
| `version` | **sysname** | Version of the IFilter DLL. |
| `manufacturer` <sup>1</sup> | **sysname** | Name of the IFilter manufacturer. |

<sup>1</sup> Only documents with [!INCLUDE [msCoName](../../includes/msconame-md.md)] as the manufacturer are supported on [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)]

## Related content

- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
