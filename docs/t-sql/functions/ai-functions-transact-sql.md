---
title: AI Functions (Transact-SQL)
description: Use AI functions to integrate relational data with AI inference endpoints.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop, bspendolini, randolphwest
ms.date: 06/11/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - sql-ai
  - ignite-2025
helpviewer_keywords:
  - "AI functions"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-ver17 || >=sql-server-linux-ver17 || =fabric || =fabric-sqldb"
---
# AI functions (Transact-SQL)

 [!INCLUDE [sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb.md)]

The functions described in this article provide built-in support for AI and the creation of AI enabled applications.

| Function | Description | Applies to |
| --- | --- | --- |
| [AI_ANALYZE_SENTIMENT](ai-analyze-sentiment-transact-sql.md) | Detect sentiment in input text (`positive`, `negative`, `mixed`, `neutral`) | [!INCLUDE [fabric-se-short](../../includes/fabric-se-short.md)] and [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)] |
| [AI_CLASSIFY](ai-classify-transact-sql.md) | Classify text into one of the provided labels | [!INCLUDE [fabric-se-short](../../includes/fabric-se-short.md)] and [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)] |
| [AI_EXTRACT](ai-extract-transact-sql.md) | Extract named values/entities from text as JSON | [!INCLUDE [fabric-se-short](../../includes/fabric-se-short.md)] and [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)] |
| [AI_FIX_GRAMMAR](ai-fix-grammar-transact-sql.md) | Correct grammar in input text | [!INCLUDE [fabric-se-short](../../includes/fabric-se-short.md)] and [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)] |
| [AI_GENERATE_CHUNKS](ai-generate-chunks-transact-sql.md) | Create chunks of text from text expressions based on size and overlap parameters | [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)] |
| [AI_GENERATE_EMBEDDINGS](ai-generate-embeddings-transact-sql.md) | Create embeddings (vector arrays) from text expressions | [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)] |
| [AI_GENERATE_RESPONSE](ai-generate-response-transact-sql.md) | Generate response text from a prompt and optional context | [!INCLUDE [fabric-se-short](../../includes/fabric-se-short.md)] and [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)] |
| [AI_SUMMARIZE](ai-summarize-transact-sql.md) | Summarize input text | [!INCLUDE [fabric-se-short](../../includes/fabric-se-short.md)] and [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)] |
| [AI_TRANSLATE](ai-translate-transact-sql.md) | Translate input text to a target language | [!INCLUDE [fabric-se-short](../../includes/fabric-se-short.md)] and [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)] |

For more info about the built-in support for AI in various platforms of the [the Microsoft SQL Database Engine](../../database-engine/sql-database-engine.md), review the following articles:

- In [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]:
    - [Vector data type](../data-types/vector-data-type.md)
    - [Vector functions](vector-functions-transact-sql.md)
    - [Intelligent applications and AI](../../sql-server/ai/artificial-intelligence-intelligent-applications.md)
    - [Intelligent applications and AI FAQ](../../sql-server/ai/artificial-intelligence-intelligent-applications-frequently-asked-questions.yml)
- In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]:
    - [Intelligent applications and AI](/azure/azure-sql/database/ai-artificial-intelligence-intelligent-applications)
- In [!INCLUDE [fabric-se-short](../../includes/fabric-se-short.md)] and [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)]:
    - [AI functions in Fabric Data Warehouse and SQL analytical endpoint](/fabric/data-warehouse/ai-functions)

## Related content

- [CREATE EXTERNAL MODEL (Transact-SQL)](../statements/create-external-model-transact-sql.md)
- [ALTER EXTERNAL MODEL (Transact-SQL)](../statements/alter-external-model-transact-sql.md)
- [DROP EXTERNAL MODEL (Transact-SQL)](../statements/drop-external-model-transact-sql.md)
- [sys.sp_invoke_external_rest_endpoint (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql.md)
