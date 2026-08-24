---
title: AI_ANALYZE_SENTIMENT (Transact-SQL)
description: The AI_ANALYZE_SENTIMENT function detects sentiment in input text.
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
  - "ai_analyze_sentiment_TSQL"
  - "ai_analyze_sentiment"
helpviewer_keywords:
  - "ai_analyze_sentiment"
dev_langs:
  - TSQL
monikerRange: "=fabric"
---
# AI_ANALYZE_SENTIMENT (Transact-SQL)

[!INCLUDE [fabricsedw](../../includes/applies-to-version/fabric-se-dw.md)]

The `AI_ANALYZE_SENTIMENT` function analyzes input text and returns one of these sentiment labels: `positive`, `negative`, `mixed`, or `neutral`.

> [!NOTE]
> - `AI_ANALYZE_SENTIMENT` is in preview.
> - `AI_ANALYZE_SENTIMENT` is available only in [!INCLUDE [fabric-se-short](../../includes/fabric-se-short.md)] and [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)].

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
AI_ANALYZE_SENTIMENT ( text )
```

## Arguments

#### text

An [expression](../language-elements/expressions-transact-sql.md) of a character type, for example **nvarchar**, **varchar**, **nchar**, or **char**.

## Return types

Returns `nvarchar` with one of the following values:

- `positive`
- `negative`
- `mixed`
- `neutral`

## Remarks

AI functions return `NULL` if the AI model can't process the text. Common reasons include:
- Responsible AI rules block inappropriate content in the input text.
- Input text exceeds token limits. The current model supports up to 15 KB of text.

## Examples

### A. Analyze sentiment in a string

```sql
SELECT ai_analyze_sentiment('This hotel was great!') AS sentiment;
```

Expected result: `positive`

### B. Analyze sentiment from a table column

```sql
SELECT review_id,
       ai_analyze_sentiment(review_text) AS sentiment
FROM dbo.hotel_reviews;
```

## Related content

- [AI Functions (Preview) for Fabric Data Warehouse and SQL analytics endpoint](/fabric/data-warehouse/ai-functions)
- [AI_CLASSIFY (Transact-SQL)](ai-classify-transact-sql.md)
- [AI_EXTRACT (Transact-SQL)](ai-extract-transact-sql.md)
- [AI_FIX_GRAMMAR (Transact-SQL)](ai-fix-grammar-transact-sql.md)
- [AI_GENERATE_RESPONSE (Transact-SQL)](ai-generate-response-transact-sql.md)
- [AI_SUMMARIZE (Transact-SQL)](ai-summarize-transact-sql.md)
- [AI_TRANSLATE (Transact-SQL)](ai-translate-transact-sql.md)
