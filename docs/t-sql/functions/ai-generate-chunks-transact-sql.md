---
title: "AI_GENERATE_CHUNKS (Transact-SQL)"
description: The ai_generate_chunks table-valued function creates text chunks.
author: jettermctedder
ms.author: bspendolini
ms.reviewer: randolphwest
ms.date: 06/27/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
ms.custom:
  - sql-ai
  - build-2025
f1_keywords:
  - "ai_generate_chunks_TSQL"
  - "ai_generate_chunks"
helpviewer_keywords:
  - "ai_generate_chunks"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-ver17 || >=sql-server-linux-ver17"
---
# AI_GENERATE_CHUNKS (Transact-SQL) (Preview)

[!INCLUDE [sqlserver2025](../../includes/applies-to-version/sqlserver2025.md)]

> [!NOTE]
> `AI_GENERATE_CHUNKS` in SQL Server 2025 is currently in **preview**.

`AI_GENERATE_CHUNKS` is a table-valued function that creates "chunks", or fragments of text based on a type, size, and source expression.

#### Compatibility level 170

`AI_GENERATE_CHUNKS` requires the compatibility level to be at least 170. When the level is less than 170, the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] is unable to find the `AI_GENERATE_CHUNKS` function.

To change the compatibility level of a database, refer to [View or change the compatibility level of a database](../../relational-databases/databases/view-or-change-the-compatibility-level-of-a-database.md).

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
AI_GENERATE_CHUNKS (source = text_expression
                    , chunk_type = 'FIXED'
                   [ , chunk_size = numeric_expression ]
                   [ , overlap = numeric_expression ]
                   [ , enable_chunk_set_id = numeric_expression ]
)
```

## Arguments

#### *source*

An [expression](../language-elements/expressions-transact-sql.md) of any character type (for example, **nvarchar**, **varchar**, **nchar**, or **char**).

#### *chunk_type*

A string literal naming the type or method to chunk the text/document and can't be `NULL` or a value from a column.

Accepted values for this release:

- `FIXED`

#### *chunk_size*

When `chunk_type` is `FIXED`, this parameter sets the character count size of each chunk specified as a variable, a literal, or a scalar expression of type **tinyint**, **smallint**, **int**, or **bigint**. *chunk_size* can't be `NULL`, negative, or zero (`0`). This parameter is also **required** when using a `chunk_type` of `FIXED`.

#### *overlap*

The *overlap* parameter determines the percentage of the preceding text that should be included in the current chunk. This percentage is applied to the `chunk_size` parameter to calculate the size in characters. The *overlap* value can be specified as a variable, a literal, or a scalar expression of type tinyint, smallint, int, or bigint. It must be a whole number between zero (`0`) and 50, inclusive, and can't be `NULL` or negative. The default value is zero (`0`).

#### *enable_chunk_set_id*

An **int** or **bit** expression that serves as a flag to enable or disable the `chunk_set_id` output column; a column that returns a number to help group returned chunks belonging to the same source. A value of `1` enables the column. If *enable_chunk_set_id* is omitted, `NULL`, or has a value of `0`, the `chunk_set_id` column is disabled and not returned.

## Return types

`AI_GENERATE_CHUNKS` returns a table with the following columns:

| Column name | Data type | Description |
| --- | --- | --- |
| `chunk` | Same as source expression data type | Returned text that was chunked from the source expression. |
| `chunk_order` | **bigint** | A sequence of ordered numbers that relates to the order each chunk was processed starting with `1` and increasing by `1`. |
| `chunk_offset` | **bigint** | Position of the chunk of the source data/document in relation to the start of the chunking process. |
| `chunk_length` | **int** | Character length of the returned text chunk. |
| `chunk_set_id` | **bigint** | An *optional column* that contains an ID that groups all the chunks of a source expression, document, or row. If multiple documents or rows are chunked in a single transaction, they're each given a different `chunk_set_id`. Visibility is controlled by the `enable_chunk_set_id` parameter. |

### Return example

Here's an example of the return results of `AI_GENERATE_CHUNKS` with the following parameters:

- Chunk type of `FIXED`.

- Chunk size of 50 characters.

- The 'chunk_set_id' is enabled.

- Chunk text: `All day long we seemed to dawdle through a country which was full of beauty of every kind. Sometimes we saw little towns or castles on the top of steep hills such as we see in old missals; sometimes we ran by rivers and streams which seemed from the wide stony margin on each side of them to be subject to great floods.`

| chunk | chunk_order | chunk_offset | chunk_length | chunk_set_id |
| --- | --- | --- | --- | --- |
| `All day long we seemed to dawdle through a country` | 1 | 1 | 50 | 1 |
| ` which was full of beauty of every kind. Sometimes` | 2 | 51 | 50 | 1 |
| ` we saw little towns or castles on the top of stee` | 3 | 101 | 50 | 1 |
| `p hills such as we see in old missals; sometimes w` | 4 | 151 | 50 | 1 |
| `e ran by rivers and streams which seemed from the` | 5 | 201 | 50 | 1 |
| ` wide stony margin on each side of them to be subje` | 6 | 251 | 50 | 1 |
| `ct to great floods.` | 7 | 301 | 19 | 1 |

## Remarks

`AI_GENERATE_CHUNKS` can be used on a table with multiple rows. Depending on the chunk size and the amount of text being chunked, the result set indicates when it starts a new column or document with the `chunk_set_id` column. In the following example, the `chunk_set_id` changes when it finishes chunking the text of the first row and moves to the second. The values for `chunk_order` and `chunk_offset` also reset to indicate a new starting point.

```sql
CREATE TABLE textchunk (text_id INT IDENTITY(1,1) PRIMARY KEY, text_to_chunk nvarchar(max));
GO

INSERT INTO textchunk (text_to_chunk)
VALUES
('All day long we seemed to dawdle through a country which was full of beauty of every kind. Sometimes we saw little towns or castles on the top of steep hills such as we see in old missals; sometimes we ran by rivers and streams which seemed from the wide stony margin on each side of them to be subject to great floods.'),
('My Friend, Welcome to the Carpathians. I am anxiously expecting you. Sleep well to-night. At three to-morrow the diligence will start for Bukovina; a place on it is kept for you. At the Borgo Pass my carriage will await you and will bring you to me. I trust that your journey from London has been a happy one, and that you will enjoy your stay in my beautiful land. Your friend, DRACULA')
GO

SELECT c.*
FROM textchunk t
CROSS APPLY
   AI_GENERATE_CHUNKS(source = text_to_chunk, chunk_type = 'FIXED', chunk_size = 50, enable_chunk_set_id = 1) c
```

| chunk | chunk_order | chunk_offset | chunk_length |  chunk_set_id |
| --- | --- | --- | --- | --- |
| `All day long we seemed to dawdle through a country` | **1** | **1** | **50** | **1** | 
| ` which was full of beauty of every kind. Sometimes` | 2 | 51 | 50 | 1 |
| ` we saw little towns or castles on the top of stee` | 3 | 101 | 50 | 1 |
| `p hills such as we see in old missals; sometimes w` | 4 | 151 | 50 | 1 |
| `e ran by rivers and streams which seemed from the` | 5 | 201 | 50 | 1 |
| ` wide stony margin on each side of them to be subje` | 6 | 251 | 50 | 1 |
| `ct to great floods.` | 7 | 301 | 19 | 1 |
| ` My Friend, Welcome to the Carpathians. I am anxi` | **1** | **1** | **50** | **2** |
| `ously expecting you. Sleep well to-night. At three` | 2 | 51 | 50 | 2 |
| ` to-morrow the diligence will start for Bukovina;` | 3 | 101 | 50 | 2 |
| `a place on it is kept for you. At the Borgo Pass m` | 4 | 151 | 50 | 2 |
| `y carriage will await you and will bring you to me` | 5 | 201 | 50 | 2 |
| `. I trust that your journey from London has been a` | 6 | 251 | 50 | 2 |
| ` happy one, and that you will enjoy your stay in m` | 7 | 301 | 50 | 2 |
| `y beautiful land. Your friend, DRACULA` | 8 | 351 | 38 | 2 |

## Examples

### A. Chunk a text column with FIXED type and size of 100 characters

The following example uses `AI_GENERATE_CHUNKS` to chunk a text column. It uses a `chunk_type` of `FIXED` and a `chunk_size` of 100 characters.

```sql
SELECT
    c.chunk
FROM
   docs_table t
CROSS APPLY
   AI_GENERATE_CHUNKS(source = text_column, chunk_type = 'FIXED', chunk_size = 100) c
```

### B. Chunk a text column with overlap

The following example uses `AI_GENERATE_CHUNKS` to chunk a text column using overlap. It uses the chunk_type of FIXED, a chunk_size of 100 characters, and an overlap of 10 percent.

```sql
SELECT
    c.chunk
FROM
   docs_table t
CROSS APPLY
   AI_GENERATE_CHUNKS(source = text_column, chunk_type = 'FIXED', chunk_size = 100, overlap = 10) c
```

### C. Use AI_GENERATE_EMBEDDINGS with AI_GENERATE_CHUNKS

This example uses `AI_GENERATE_EMBEDDINGS` with `AI_GENERATE_CHUNKS` to create embeddings from text chunks and then inserts the returned vector arrays from the AI model inferencing endpoint into a table.

```sql
INSERT INTO
    my_embeddings (chunked_text, vector_embeddings)
SELECT
    c.chunk,
    AI_GENERATE_EMBEDDINGS(c.chunk USE MODEL MyAzureOpenAiModel)
FROM
    table_with_text t
CROSS APPLY
    AI_GENERATE_CHUNKS(source = t.text_to_chunk, chunk_type = 'FIXED', chunk_size = 100) c
```

## Related content

- [AI_GENERATE_EMBEDDINGS (Transact-SQL)](ai-generate-embeddings-transact-sql.md)
- [CREATE EXTERNAL MODEL (Transact-SQL)](../statements/create-external-model-transact-sql.md)
- [ALTER EXTERNAL MODEL (Transact-SQL)](../statements/alter-external-model-transact-sql.md)
- [DROP EXTERNAL MODEL (Transact-SQL)](../statements/drop-external-model-transact-sql.md)
