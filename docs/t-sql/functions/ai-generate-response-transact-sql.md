---
title: "AI_GENERATE_RESPONSE (Transact-SQL)"
description: The AI_GENERATE_RESPONSE function generates a response from a prompt and optional context data.
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
  - "ai_generate_response_TSQL"
  - "ai_generate_response"
helpviewer_keywords:
  - "ai_generate_response"
dev_langs:
  - TSQL
monikerRange: "=fabric"
---
# AI_GENERATE_RESPONSE (Transact-SQL)

[!INCLUDE [fabric-se-dw](../../includes/applies-to-version/fabric-se-dw.md)]

`AI_GENERATE_RESPONSE` generates text from a prompt and optional supporting data.

> [!NOTE]
> - `AI_GENERATE_RESPONSE` is in preview.
> - `AI_GENERATE_RESPONSE` is available only in [!INCLUDE [fabric-se-short](../../includes/fabric-se-short.md)] and [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)].

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
AI_GENERATE_RESPONSE ( prompt [ , data ] [ (NULL | ERROR | DEFAULT <value>) ON ERROR ] )
```

## Arguments

#### prompt

An [expression](../language-elements/expressions-transact-sql.md) of a character type that defines the instruction.

#### data

Optional text input used as supporting context for the prompt.

#### ON ERROR

The `ON ERROR` clause controls how an AI function handles processing errors.

- `NULL ON ERROR` returns `NULL` when the function can't process a value. This is the default behavior and doesn't need to be explicitly specified.
- `ERROR ON ERROR` causes the entire query to fail if an error occurs while processing any input value.
- `DEFAULT <value> ON ERROR` returns the specified default value instead of `NULL` when an error occurs.

Errors can be caused by [Responsible AI](https://www.microsoft.com/ai/tools-practices) safety checks, input size limits, transient service issues, or other processing failures.

## Return types

Returns **nvarchar(max)** with generated text.

## Remarks

AI functions return `NULL` if the AI model can't process the text. Common reasons include:

- [Responsible AI](https://www.microsoft.com/ai/tools-practices) rules block inappropriate content in the input text.
- Input text exceeds token limits. The current model supports up to 15 KB of text.

## Examples

### A. Generate a short response

```sql
SELECT ai_generate_response('Reply in 20 words:', 'The room was noisy.') AS response;
```

### B. Generate responses per row

The following query sends every review_text value to external AI service to generate response.

```sql
SELECT review_id,
       ai_generate_response('Draft a professional response in 30 words:', review_text NULL ON ERROR) AS response_text
FROM dbo.hotel_reviews;
```
The optional `NULL ON ERROR` clause instructs AI function to return the `NULL` value for every input value that cannot be processed.

## Related content

- [AI Functions (Preview) for Fabric Data Warehouse and SQL analytics endpoint](/fabric/data-warehouse/ai-functions)
- [AI_ANALYZE_SENTIMENT (Transact-SQL)](ai-analyze-sentiment-transact-sql.md)
- [AI_CLASSIFY (Transact-SQL)](ai-classify-transact-sql.md)
- [AI_EXTRACT (Transact-SQL)](ai-extract-transact-sql.md)
- [AI_SUMMARIZE (Transact-SQL)](ai-summarize-transact-sql.md)
- [AI_FIX_GRAMMAR (Transact-SQL)](ai-fix-grammar-transact-sql.md)
- [AI_TRANSLATE (Transact-SQL)](ai-translate-transact-sql.md)
