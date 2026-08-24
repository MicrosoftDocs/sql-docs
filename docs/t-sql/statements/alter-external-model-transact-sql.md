---
title: "ALTER EXTERNAL MODEL (Transact-SQL)"
description: ALTER EXTERNAL MODEL (Transact-SQL) for altering an external model object that contains the location, authentication method, and purpose of an AI model inference endpoint.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: bspendolini
ms.date: 10/14/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
ms.custom:
  - sql-ai
  - ignite-2025
f1_keywords:
  - "ALTER_EXTERNAL_MODEL"
  - "ALTER EXTERNAL MODEL"
  - "ai_generate_embeddings"
helpviewer_keywords:
  - "External"
  - "External model"
  - "ai_generate_embeddings, external model"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-ver17 || >=sql-server-linux-ver17 || =fabric-sqldb"
---

# ALTER EXTERNAL MODEL (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-fabricsqldb.md)]

Alters an external model object.

## Syntax

```syntaxsql
ALTER EXTERNAL MODEL external_model_object_name
SET
  (   LOCATION = '<prefix>://<path> [ :<port> ] '
    , API_FORMAT = '<OpenAI , Azure OpenAI , etc>'
    , MODEL_TYPE = EMBEDDINGS
    , MODEL = 'text-embedding-ada-002'
    [ , CREDENTIAL = <credential_name> ]
    [ , PARAMETERS = ' { "valid":"JSON" } ' ]
  );
```

## Arguments

### external_model_object_name

Specifies the user-defined name for the external model. The name must be unique within the database.

### LOCATION

Provides the connectivity protocol and path to the AI model inference endpoint.

### API_FORMAT

The API message format for the AI model inference endpoint provider. Accepted values are `Azure OpenAI`, `OpenAI`, and `Ollama`.

### MODEL_TYPE

The type of model being accessed from the AI model inference endpoint location. Accepted value is `EMBEDDINGS`.

### MODEL

The specific model hosted by the AI provider. For example, `text-embedding-ada-002`, `text-embedding-3-large`, or `o3-mini`.

### CREDENTIAL

Indicate which `DATABASE SCOPED CREDENTIAL` object is used with the AI model inference endpoint.

### PARAMETERS

A valid JSON string that contains parameters to be appended to the AI model inference endpoint request message. For example:

```text
'{ "dimensions": 1536 }'
```

## Remarks

Only single external model object can be modified at a time. Concurrent requests to modify the same external model object causes one statement to wait. However, different external model objects can be modified at the same time. This statement can run concurrently with other statements.

## Examples

### Alter EXTERNAL MODEL and change the MODEL parameter

This example alters the `EXTERNAL MODEL` named `myAImodel`, and changes the `MODEL` parameter.

```sql
-- Alter an external model
ALTER EXTERNAL MODEL myAImodel
SET
(
  MODEL = 'text-embedding-3-large'
);
```

## Related content

- [CREATE EXTERNAL MODEL (Transact-SQL)](create-external-model-transact-sql.md)
- [DROP EXTERNAL MODEL (Transact-SQL)](drop-external-model-transact-sql.md)
- [sys.external_models (Transact-SQL)](../../relational-databases/system-catalog-views/sys-external-models-transact-sql.md)
- [AI_GENERATE_EMBEDDINGS (Transact-SQL)](../functions/ai-generate-embeddings-transact-sql.md)
- [AI_GENERATE_CHUNKS (Transact-SQL)](../functions/ai-generate-chunks-transact-sql.md)
