---
title: Azure Synapse Pathway behind the scenes.
description: Technical deep dive into how Azure Synapse Pathway translates your code. 
author: WilliamDAssafMSFT 
ms.author: wiassaf 
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual 
ms.date: 02/11/2022
monikerRange: "=azure-sqldw-latest"
ms.custom: template-concept 
ms.reviewer: wiassaf
---

# Azure Synapse Pathway behind the scenes
[!INCLUDE [Azure Synapse Analytics](../../includes/applies-to-version/asa.md)]

Azure Synapse Pathway's goal is to preserve the functional intent of the original code while optimizing for Synapse SQL. Synapse Pathway uses a three-stage process for translating SQL code from a source system to Azure Synapse SQL.

Each of the stages preserves and augments the knowledge of the source including source-specific metadata to ensure the highest quality in translation.

:::image type="content" source="./media/synapse-pathway-behind-the-scenes/azure-synapse-pathway-behind-the-scenes.svg" alt-text="Diagram explaining the Azure Synapse Pathway source, translation, and output ":::

## Stage 1 – Lexing and parsing

SQL Language parsing is a problem that has been solved many times over. There are many commercial and open-source parsers that help with the underlying process of taking a source statement, breaking it down into logical tokens and then executing against a set or parser rules to ensure language consistency. 

Synapse Pathway defines source grammars that allow the tool to identify and process the SQL input into an augmented Abstract Syntax Tree (AST) that is used in further processing. 

## Stage 2 - Augmented abstract syntax tree (AST)

Synapse Pathway defines a common representation of all objects in an augmented Abstract Syntax Tree (AST). The Pathway AST includes metadata from other statements or fragments to assist in the proper conversion of a statement.

By not just tracking that a token is a function but rather the source system type requirement, the script generation components can make smarter decisions about translating to Synapse SQL.

For example, the source function for the absolute function is defined as:

```sql  
ABS( float_expression ) 
```

Azure Synapse SQL defines the absolute function as:

```sql  
ABS ( numeric_expression )  
```

In this simple case, Synapse Pathway understands that the conversion in Synapse SQL from float to numeric is an implicit [conversion](../../t-sql/functions/cast-and-convert-transact-sql.md?view=azure-sqldw-latest&preserve-view=true#implicit-conversions) and requires no further type casting. Simple, clean, and effective code translation.

Keeping this meta-information about the source statements and fragments helps the structural differences between platforms – conversions in opt-out logic for search condition predicates in a WHERE clause for example.

## Stage 3 - Syntax generation

The last stage of the process is to generate syntax for Synapse SQL. Using the AST structure generated from the source files, Synapse Pathway writes each DDL object to an individual file. The syntax generators use in-depth knowledge of the target platform to optimize statements.

For example, a common pattern that is seen in data loading scenarios is to first delete all of the contents in a staging table and then load the data from another staging table in an INSERT/SELECT fashion.

```sql  
DELETE staging.table1 ALL;
INSERT INTO staging.table1…
FROM staging.table2;
```

Synapse SQL has an optimized path for this scenario – a [CREATE TABLE AS SELECT](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-develop-ctas). The CTAS statement is a batch-based operation and minimally logged driving high performance by using all the compute infrastructure available. Without this insight about Synapse SQL, tools often produce a truncate and INSERT/SELECT statement.

```sql  
TRUNCATE TABLE staging.table1;
INSERT INTO staging.table1…
FROM staging.table2;
```

While not bad, this code can be optimized to a DROP TABLE and CTAS to have higher performance.

```sql  
DROP TABLE staging.table1;
CREATE TABLE staging.table1
WITH
(
    -- Derived from the original table definition 
    DISTRIBUTION = HASH(column1),
    -- Derived from the original table definition
    CLUSTERED COLUMNSTORE INDEX
)
AS SELECT  * FROM staging.table2;
```

## Next steps

- [Download Azure Synapse Pathway](synapse-pathway-download.md)
- [FAQ](pathway-faq.yml)
