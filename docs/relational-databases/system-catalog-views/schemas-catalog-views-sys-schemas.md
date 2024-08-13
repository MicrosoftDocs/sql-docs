---
title: "sys.schemas (Transact-SQL)"
description: sys.schemas contains a row for each database schema.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/18/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "schemas_TSQL"
  - "sys.schemas_TSQL"
  - "schemas"
  - "sys.schemas"
helpviewer_keywords:
  - "sys.schemas catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# Schema catalog view - sys.schemas

[!INCLUDE [sql-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Contains a row for each database schema.

> [!NOTE]  
> Database schemas are different from XML schemas, which are used to define the content model of XML documents.

| Column name | Data type | Description |
| --- | --- | --- |
| `name` | **sysname** | Name of the schema. Is unique within the database. |
| `schema_id` | **int** | ID of the schema. Is unique within the database. |
| `principal_id` | **int** | ID of the principal that owns this schema. |

## Remarks

Database schemas act as namespaces or containers for objects, such as tables, views, procedures, and functions, that can be found in the `sys.objects` catalog view.

Each schema has an owner. The owner is a security [principal](../security/authentication-access/principals-database-engine.md).

## Permissions

Requires membership in the **public** role.

## Related content

- [Principals (Database Engine)](../security/authentication-access/principals-database-engine.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [sys.objects (Transact-SQL)](sys-objects-transact-sql.md)
