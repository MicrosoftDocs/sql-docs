---
title: "DROP EXTERNAL MODEL (Transact-SQL)"
description: DROP EXTERNAL MODEL (Transact-SQL) for dropping an external model object that contains the location, authentication method, and purpose of an AI model inference endpoint.
author: jettermctedder
ms.author: bspendolini
ms.reviewer: randolphwest
ms.date: 10/14/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
ms.custom:
  - sql-ai
  - ignite-2025
f1_keywords:
  - "DROP_EXTERNAL_MODEL"
  - "EXTERNAL MODEL"
  - "ai_generate_embeddings"
helpviewer_keywords:
  - "External"
  - "External model"
  - "ai_generate_embeddings, external model"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-ver17 || >=sql-server-linux-ver17 || =fabric-sqldb"
---

# DROP EXTERNAL MODEL (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-fabricsqldb.md)]

Drops an external model object.

## Syntax

```syntaxsql
DROP EXTERNAL MODEL external_model_object_name
[ ; ]
```

## Arguments

### external_model_object_name

Specifies the user-defined name for the external model. The name must be unique within the database.

## Remarks

Dropping an external model doesn't drop any credentials that the model was using for AI model inference endpoint authentication.

## Examples

### Drop EXTERNAL MODEL

This example drops the model named `dbo.myAImodel`.

```sql
DROP EXTERNAL MODEL myAImodel;
```

## Related content

- [CREATE EXTERNAL MODEL (Transact-SQL)](create-external-model-transact-sql.md)
- [ALTER EXTERNAL MODEL (Transact-SQL)](alter-external-model-transact-sql.md)
- [sys.external_data_sources](../../relational-databases/system-catalog-views/sys-external-models-transact-sql.md)
- [AI_GENERATE_EMBEDDINGS (Transact-SQL)](../functions/ai-generate-embeddings-transact-sql.md)
- [AI_GENERATE_CHUNKS (Transact-SQL)](../functions/ai-generate-chunks-transact-sql.md)
