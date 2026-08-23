---
title: AI_EXTRACT (Transact-SQL)
description: The AI_EXTRACT function extracts named values from text as JSON.
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
  - "ai_extract_TSQL"
  - "ai_extract"
helpviewer_keywords:
  - "ai_extract"
dev_langs:
  - TSQL
monikerRange: "=fabric"
---
# AI_EXTRACT (Transact-SQL)

[!INCLUDE [fabricsedw](../../includes/applies-to-version/fabric-se-dw.md)]

The `AI_EXTRACT` function extracts values from input text using the classes you provide, and returns the result as a JSON object.

> [!NOTE]
> - `AI_EXTRACT` is in preview.
> - `AI_EXTRACT` is available only in [!INCLUDE [fabric-se-short](../../includes/fabric-se-short.md)] and [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)].

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
AI_EXTRACT ( text, class1, class2 [ , ...n ] )
```

## Arguments

#### text

An [expression](../language-elements/expressions-transact-sql.md) of a character type, for example **nvarchar**, **varchar**, **nchar**, or **char**.

#### class1, class2, ...n

One or more property names to extract from the text.

## Return types

Returns `nvarchar(max)` containing JSON text.

## Remarks

AI functions return `NULL` if the AI model can't process the text. Common reasons include:
- Responsible AI rules block inappropriate content in the input text.
- Input text exceeds token limits. The current model supports up to 15 KB of text.

## Examples

### A. Extract properties from a sentence

```sql
SELECT ai_extract('Check-in was late and room dirty', 'sentiment', 'problem') AS extraction;
```

Expected result: `{"sentiment":"Negative","problem":"Dirty room"}`

### B. Parse extracted JSON into columns

```sql
SELECT sentiment, time_reported, problem
FROM dbo.hotel_reviews
CROSS APPLY OPENJSON(
    ai_extract(review_text, 'sentiment', 'time_reported', 'problem')
) WITH (
    sentiment VARCHAR(1000),
    time_reported VARCHAR(100),
    problem VARCHAR(1000)
);
```

## Related content

- [AI Functions (Preview) for Fabric Data Warehouse and SQL analytics endpoint](/fabric/data-warehouse/ai-functions)
- [AI_ANALYZE_SENTIMENT (Transact-SQL)](ai-analyze-sentiment-transact-sql.md)
- [AI_CLASSIFY (Transact-SQL)](ai-classify-transact-sql.md)
- [AI_FIX_GRAMMAR (Transact-SQL)](ai-fix-grammar-transact-sql.md)
- [AI_GENERATE_RESPONSE (Transact-SQL)](ai-generate-response-transact-sql.md)
- [AI_SUMMARIZE (Transact-SQL)](ai-summarize-transact-sql.md)
- [AI_TRANSLATE (Transact-SQL)](ai-translate-transact-sql.md)
- [OPENJSON (Transact-SQL)](openjson-transact-sql.md)
