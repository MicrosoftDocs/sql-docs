---
title: AI_SUMMARIZE (Transact-SQL)
description: The AI_SUMMARIZE function produces a concise summary of input text.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop
ms.date: 08/17/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - sql-ai
f1_keywords:
  - "ai_summarize_TSQL"
  - "ai_summarize"
helpviewer_keywords:
  - "ai_summarize"
dev_langs:
  - TSQL
monikerRange: "=fabric"
---
# AI_SUMMARIZE (Transact-SQL)

[!INCLUDE [fabricdw](../../includes/applies-to-version/fabric-se-dw.md)]

`AI_SUMMARIZE` creates a concise summary of the input text.

> [!NOTE]
> - `AI_SUMMARIZE` is in preview.
> - `AI_SUMMARIZE` is available only in [!INCLUDE [fabric-se-short](../../includes/fabric-se-short.md)] and [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)].

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
AI_SUMMARIZE ( text [ (NULL | ERROR | DEFAULT <value>) ON ERROR ] )
```

## Arguments

#### text

An [expression](../language-elements/expressions-transact-sql.md) of a character type, for example **nvarchar**, **varchar**, **nchar**, or **char**.

#### ON ERROR

The `ON ERROR` clause controls how an AI function handles processing errors.

- `NULL ON ERROR` returns `NULL` when the function can't process a value. This is the default behavior and doesn't need to be explicitly specified.
- `ERROR ON ERROR` causes the entire query to fail if an error occurs while processing any input value.
- `DEFAULT <value> ON ERROR` returns the specified default value instead of `NULL` when an error occurs.

Errors can be caused by [Responsible AI](https://www.microsoft.com/ai/tools-practices) safety checks, input size limits, transient service issues, or other processing failures.

## Return types

Returns **nvarchar** containing the generated summary.

## Remarks

AI functions return `NULL` if the AI model can't process the text. Common reasons include:

- [Responsible AI](https://www.microsoft.com/ai/tools-practices) rules block inappropriate content in the input text.
- Input text exceeds token limits. The current model supports up to 15 KB of text.

## Examples

### A. Summarize a sentence

```sql
SELECT ai_summarize('The hotel was clean and staff were friendly.') AS summary;
```

Expected result: `Clean hotel, friendly staff.`

### B. Summarize review text in a table

The following query sends every review_text value to external AI service to summarize it.

```sql
SELECT review_id,
       ai_summarize(review_text DEFAULT 'N/A' ON ERROR) AS review_summary
FROM dbo.hotel_reviews;
```

The optional `DEFAULT 'N/A' ON ERROR` clause instructs AI function to return the value 'N/A' for every input value that cannot be processed.

## Related content

- [AI Functions (Preview) for Fabric Data Warehouse and SQL analytics endpoint](/fabric/data-warehouse/ai-functions)
- [AI_ANALYZE_SENTIMENT (Transact-SQL)](ai-analyze-sentiment-transact-sql.md)
- [AI_CLASSIFY (Transact-SQL)](ai-classify-transact-sql.md)
- [AI_EXTRACT (Transact-SQL)](ai-extract-transact-sql.md)
- [AI_FIX_GRAMMAR (Transact-SQL)](ai-fix-grammar-transact-sql.md)
- [AI_GENERATE_RESPONSE (Transact-SQL)](ai-generate-response-transact-sql.md)
- [AI_TRANSLATE (Transact-SQL)](ai-translate-transact-sql.md)
