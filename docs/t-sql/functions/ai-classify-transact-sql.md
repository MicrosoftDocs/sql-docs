---
title: AI_CLASSIFY (Transact-SQL)
description: The AI_CLASSIFY function classifies input text into one of the provided labels.
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
  - "ai_classify_TSQL"
  - "ai_classify"
helpviewer_keywords:
  - "ai_classify"
dev_langs:
  - TSQL
monikerRange: "=fabric"
---
# AI_CLASSIFY (Transact-SQL)

[!INCLUDE [fabricsedw](../../includes/applies-to-version/fabric-se-dw.md)]

The `AI_CLASSIFY` function classifies input text into one of the labels you provide.

> [!NOTE]
> - `AI_CLASSIFY` is in preview.
> - `AI_CLASSIFY` is available only in [!INCLUDE [fabric-se-short](../../includes/fabric-se-short.md)] and [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)].

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
AI_CLASSIFY ( text, class1, class2 [ , ...n ] [ (NULL | ERROR | DEFAULT <value>) ON ERROR ] )
```

## Arguments

#### text

An [expression](../language-elements/expressions-transact-sql.md) of a character type, for example **nvarchar**, **varchar**, **nchar**, or **char**.

#### class1, class2, ...n

One or more candidate class labels, provided as string literals or string expressions.

#### ON ERROR

The `ON ERROR` clause controls how an AI function handles processing errors.

- `NULL ON ERROR` returns `NULL` when the function can't process a value. This is the default behavior and doesn't need to be explicitly specified.
- `ERROR ON ERROR` causes the entire query to fail if an error occurs while processing any input value.
- `DEFAULT <value> ON ERROR` returns the specified default value instead of `NULL` when an error occurs.

Errors can be caused by [Responsible AI](https://www.microsoft.com/ai/tools-practices) safety checks, input size limits, transient service issues, or other processing failures.

## Return types

Returns `nvarchar` containing the selected class label.

## Remarks

AI functions return `NULL` if the AI model can't process the text. Common reasons include:
- [Responsible AI](https://www.microsoft.com/ai/tools-practices) rules block inappropriate content in the input text.
- Input text exceeds token limits. The current model supports up to 15 KB of text.

## Examples

### A. Classify text with custom labels

```sql
SELECT ai_classify('Room was dirty', 'service', 'dirt', 'food') AS classification;
```

Expected result: `dirt`

### B. Classify rows in a table

The following query sends every `review_text` value to external AI service to classify it.

```sql
SELECT review_id,
       ai_classify(review_text, 'service', 'dirt', 'food', 'other' DEFAULT 'unknown' ON ERROR) AS category
FROM dbo.hotel_reviews;
```

The optional `DEFAULT 'unknown' ON ERROR` clause instructs AI function to return the value 'unknown' for every input value that cannot be processed.

## Related content

- [AI Functions (Preview) for Fabric Data Warehouse and SQL analytics endpoint](/fabric/data-warehouse/ai-functions)
- [AI_ANALYZE_SENTIMENT (Transact-SQL)](ai-analyze-sentiment-transact-sql.md)
- [AI_EXTRACT (Transact-SQL)](ai-extract-transact-sql.md)
- [AI_FIX_GRAMMAR (Transact-SQL)](ai-fix-grammar-transact-sql.md)
- [AI_GENERATE_RESPONSE (Transact-SQL)](ai-generate-response-transact-sql.md)
- [AI_SUMMARIZE (Transact-SQL)](ai-summarize-transact-sql.md)
- [AI_TRANSLATE (Transact-SQL)](ai-translate-transact-sql.md)
