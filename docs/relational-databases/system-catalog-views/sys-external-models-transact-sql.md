---
title: "sys.external_models (Transact-SQL)"
description: sys.external_models contains a row for each external model in the current database.
author: dimitri-furman
ms.author: dfurman
ms.reviewer: bspendolini
ms.date: 04/21/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-ver17 || >=sql-server-linux-ver17"
ms.custom:
  - build-2025
---
# sys.external_models (Transact-SQL)

[!INCLUDE [sqlserver2025](../../includes/applies-to-version/sqlserver2025.md)]

Contains a row for each external model in the current database.

| Column name | Data type | Description | Range |
| --- | --- | --- | --- |
| `external_model_id` | **int** | ID of the model, unique within an instance of SQL Server | |
| `name` | **nvarchar** | Name of the model | |
| `principal_id` | **int** | ID of the database principal that owns the external model object | |
| `location` | **nvarchar** | Server name or file path of the model | |
| `api_format` | **nvarchar** | Name of the API format (`Azure OpenAI`, `OpenAI`, etc.) | |
| `model_type_id` | **int** | ID of the model type. (`embeddings`, `chat`, etc.) | |
| `model_type_desc` | **nvarchar** | Name of the model type. (`embeddings`, `chat`, etc.) | |
| `model` | **nvarchar** | Name of the embedding model (`text-ada-small-02`, etc.) | |
| `credential_id` | **int** | ID of the database scoped credential object | |
| `parameters` | **json** | JSON to be appended to the outgoing payload | |
| `create_time` | **datetime2** | Create time of the model | |
| `modify_time` | **datetime2** | Time the model was updated (if updated), and defaults to `create_time` on creation of the model | |

## Permissions

The visibility of the metadata in catalog views is limited to securables that a user either owns, or on which the user is granted some permission. For more information, see [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md).

## Related content

- [AI_GENERATE_EMBEDDINGS (Transact-SQL)](../../t-sql/functions/ai-generate-embeddings-transact-sql.md)
- [AI_GENERATE_CHUNKS (Transact-SQL)](../../t-sql/functions/ai-generate-chunks-transact-sql.md)
- [CREATE EXTERNAL MODEL (Transact-SQL)](../../t-sql/statements/create-external-model-transact-sql.md)
- [ALTER EXTERNAL MODEL (Transact-SQL)](../../t-sql/statements/alter-external-model-transact-sql.md)
- [DROP EXTERNAL MODEL (Transact-SQL)](../../t-sql/statements/drop-external-model-transact-sql.md)
