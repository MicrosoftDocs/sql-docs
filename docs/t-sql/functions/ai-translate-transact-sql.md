---
title: AI_TRANSLATE (Transact-SQL)
description: The AI_TRANSLATE function translates input text to a target language.
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
  - "ai_translate_TSQL"
  - "ai_translate"
helpviewer_keywords:
  - "ai_translate"
dev_langs:
  - TSQL
monikerRange: "=fabric"
---
# AI_TRANSLATE (Transact-SQL)

[!INCLUDE [fabricsedw](../../includes/applies-to-version/fabric-se-dw.md)]

`AI_TRANSLATE` translates input text into a target language.

> [!NOTE]
> - `AI_TRANSLATE` is in preview.
> - `AI_TRANSLATE` is available only in [!INCLUDE [fabric-se-short](../../includes/fabric-se-short.md)] and [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)].

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
AI_TRANSLATE ( text, lang_code [ (NULL | ERROR | DEFAULT <value>) ON ERROR ] )
```

## Arguments

#### text

An [expression](../language-elements/expressions-transact-sql.md) of a character type, for example **nvarchar**, **varchar**, **nchar**, or **char**.

#### lang_code

Language code for the translation target.

Supported values: `de`, `en`, `fr`, `it`, `es`, `el`, `pl`, `sv`, `fi`, `cs`.

#### ON ERROR

The `ON ERROR` clause controls how an AI function handles processing errors.

- `NULL ON ERROR` returns `NULL` when the function can't process a value. This is the default behavior and doesn't need to be explicitly specified.
- `ERROR ON ERROR` causes the entire query to fail if an error occurs while processing any input value.
- `DEFAULT <value> ON ERROR` returns the specified default value instead of `NULL` when an error occurs.

Errors can be caused by [Responsible AI](https://www.microsoft.com/ai/tools-practices) safety checks, input size limits, transient service issues, or other processing failures.

## Return types

Returns **nvarchar(max)** with translated text.

## Remarks

AI functions return `NULL` if the AI model can't process the text. Common reasons include:

- [Responsible AI](https://www.microsoft.com/ai/tools-practices) rules block inappropriate content in the input text.
- Input text exceeds token limits. The current model supports up to 15 KB of text.

## Examples

### A. Translate to German

```sql
SELECT ai_translate('The hotel was great', 'de') AS translation_de;
```

Expected result: `Das Hotel war großartig.`

### B. Translate review text into multiple languages

```sql
SELECT review_id,
       ai_translate(review_text, 'de') AS review_de,
       ai_translate(review_text, 'fr' ERROR ON ERROR) AS review_fr,
       ai_translate(review_text, 'es' DEFAULT 'N/A' ON ERROR) AS review_es
FROM dbo.hotel_reviews;
```

If translation to German fails, the first function will return `NULL` instead of error.

The `NULL ON ERROR` clause instructs AI function that translates text to French to fail the query if any input value cannot be translated. The `DEFAULT 'N/A' ON ERROR` clause instructs AI function that translates text to Spanish to return the value `N/A` for every input value that cannot be processed.

## Related content

- [AI Functions (Preview) for Fabric Data Warehouse and SQL analytics endpoint](/fabric/data-warehouse/ai-functions)
- [AI_ANALYZE_SENTIMENT (Transact-SQL)](ai-analyze-sentiment-transact-sql.md)
- [AI_CLASSIFY (Transact-SQL)](ai-classify-transact-sql.md)
- [AI_EXTRACT (Transact-SQL)](ai-extract-transact-sql.md)
- [AI_GENERATE_RESPONSE (Transact-SQL)](ai-generate-response-transact-sql.md)
- [AI_SUMMARIZE (Transact-SQL)](ai-summarize-transact-sql.md)
- [AI_FIX_GRAMMAR (Transact-SQL)](ai-fix-grammar-transact-sql.md)
