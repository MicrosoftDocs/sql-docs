---
title: AI_SUMMARIZE (Transact-SQL)
description: The AI_SUMMARIZE function produces a concise summary of input text.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop
ms.date: 06/10/2026
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
AI_SUMMARIZE ( text )
```

## Arguments

#### text

An [expression](../language-elements/expressions-transact-sql.md) of a character type, for example **nvarchar**, **varchar**, **nchar**, or **char**.

## Return types

Returns `nvarchar` containing the generated summary.

## Remarks

AI functions return `NULL` if the AI model can't process the text. Common reasons include:
- Responsible AI rules block inappropriate content in the input text.
- Input text exceeds token limits. The current model supports up to 15 KB of text.

## Examples

### A. Summarize a sentence

```sql
SELECT ai_summarize('The hotel was clean and staff were friendly.') AS summary;
```

Expected result: `Clean hotel, friendly staff.`

### B. Summarize review text in a table

```sql
SELECT review_id,
       ai_summarize(review_text) AS review_summary
FROM dbo.hotel_reviews;
```

## Related content

- [AI Functions (Preview) for Fabric Data Warehouse and SQL analytics endpoint](/fabric/data-warehouse/ai-functions)
- [AI_ANALYZE_SENTIMENT (Transact-SQL)](ai-analyze-sentiment-transact-sql.md)
- [AI_CLASSIFY (Transact-SQL)](ai-classify-transact-sql.md)
- [AI_EXTRACT (Transact-SQL)](ai-extract-transact-sql.md)
- [AI_FIX_GRAMMAR (Transact-SQL)](ai-fix-grammar-transact-sql.md)
- [AI_GENERATE_RESPONSE (Transact-SQL)](ai-generate-response-transact-sql.md)
- [AI_TRANSLATE (Transact-SQL)](ai-translate-transact-sql.md)
