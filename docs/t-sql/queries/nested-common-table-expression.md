---
title: "Nested Common Table Expression (CTE) in Fabric data warehousing"
description: "Nested CTE in Fabric Data Warehousing. Common Table Expressions (CTEs) can simplify complex queries by deconstructing ordinarily complex queries into reusable blocks."
author: XiaoyuMSFT
ms.author: xiaoyul
ms.reviewer: wiassaf, randolphwest
ms.date: 10/06/2024
ms.service: sql
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: "=fabric"
---
# Nested Common Table Expression (CTE) in Fabric data warehousing (Transact-SQL)

[!INCLUDE [fabric-se-and-dw](../../includes/applies-to-version/fabric-se-and-dw.md)]

Common Table Expressions (CTEs) can simplify complex queries by deconstructing ordinarily complex queries into reusable blocks.

There are four types of CTE, including *standard*, *sequential*, *recursive*, and *nested* CTE.

- A standard CTE doesn't reference or define another CTE in its definition.
- A nested CTE's definition includes defining another CTE.
- A sequential CTE's definition can reference an existing CTE but can't define another CTE.
- A recursive CTE references itself in its definition.

Fabric Warehouse and SQL analytics endpoint both support *standard*, *sequential*, and *nested* CTEs (Preview). While standard and sequential CTEs are generally available in Microsoft Fabric, nested CTEs are currently a preview feature.

For more information about common table expressions, see [WITH common_table_expression (Transact-SQL)](with-common-table-expression-transact-sql.md?view=fabric&preserve-view=true).

> [!NOTE]
> During preview, creation of nested CTE is supported by SQL Server Management Studio (SSMS) only. Intellisense in SSMS doesn't recognize nested CTE syntax but this doesn't block creating nested CTE. For best experience, limit nesting levels to 64.

## Syntax

```syntaxsql
WITH <NESTED_CTE_NAME_LEVEL1> [ (column_name , ...) ] AS
    (WITH <NESTED_CTE_NAME_LEVEL2> [ (column_name , ...) ] AS
        (
            ...
                WITH <NESTED_CTE_NAME_LEVELn-1> [ ( column_name , ...) ] AS
                (
                    WITH <NESTED_CTE_NAME_LEVELn> [ ( column_name , ...) ] AS
                    (
                        Standard_CTE_query_definition
                    )
                    <SELECT statement> -- Data source must include NESTED_CTE_NAME_LEVELn
                )
                <SELECT statement> -- Data source must include NESTED_CTE_NAME_LEVELn-1
            ...
        )
    <SELECT statement> -- Data source must include NESTED_CTE_NAME_LEVEL2
    )
```

## Guidelines for creating and using a nested CTE

In addition to guidelines for creating and using standard CTEs, here are extra guidelines for nested CTEs:

- A nested CTE can only be used in a SELECT statement. It can't be used in UPDATE, INSERT, or DELETE statements.
- No UPDATE, INSERT, or DELETE statements are allowed in the definition of a nested CTE.
- CTE names at the same nesting level can't be duplicated.
- A nested CTE is only visible to the nested CTE or sequential CTEs that are at its immediate higher level.
- Cross-database queries are allowed in a nested CTE definition.
- Query hints (i.e. OPTION clause) are not allowed in a nested CTE's definition.
- Nested CTE can't be used in CREATE VIEW.
- AS OF is not supported in the definition of a nested CTE.
- Nested CTEs are supported in a CTE subquery definition, but not in a general subquery.

## Examples

### Differences between standard, sequential, and nested CTEs

```sql
-- Standard CTE
;WITH Standard_CTE AS (
    SELECT * FROM T1
)
SELECT * FROM Standard_CTE;

-- Sequential CTE
;WITH CTE1 AS (
    SELECT * FROM T1
),
CTE2 AS (SELECT * FROM CTE1),
CTE3 AS (SELECT * FROM CTE2)
SELECT * FROM CTE3

-- Nested CTE
;WITH OUTER_CTE AS (
    WITH INNER_CTE AS (
        SELECT * FROM T1
    )
    SELECT * FROM INNER_CTE
)
SELECT * FROM OUTER_CTE;
```

## Name scope of CTE is restricted to its scope

CTE names can be reused at different nesting levels. CTE names at the same nesting level can't be duplicated. In this example, the name `cte1` is used in both outer and inner scope.

```sql
;WITH
    cte1 AS (
        WITH
            inner_cte1_1 AS (
                SELECT * FROM NestedCTE_t1 WHERE c1 = 1
            ),
            inner_cte1_2 AS (
                SELECT * FROM inner_cte1_1 WHERE c2 = 1
            )
        SELECT * FROM inner_cte1_2
    ),
    cte2 AS (
        WITH
            cte1 AS (
                SELECT * FROM NestedCTE_t1 WHERE c3 = 1
            ),
            inner_cte2_2 AS (
                SELECT * FROM cte1 WHERE c4 = 1
            )
        SELECT * FROM inner_cte2_2
    )
```

## Complex nested CTE with union, union all, intersect, and except

```sql
CREATE TABLE NestedCTE_t1 (
    c1 INT,
    c2 INT,
    c3 INT
);
GO

INSERT INTO NestedCTE_t1
VALUES (1, 1, 1);

INSERT INTO NestedCTE_t1
VALUES (2, 2, 2);

INSERT INTO NestedCTE_t1
VALUES (3, 3, 3);
GO

WITH
    outermost_cte_1 AS (
        WITH
            outer_cte_1 AS (
                WITH
                    inner_cte1_1 AS (
                        SELECT * FROM NestedCTE_t1 WHERE c1 = 1
                    ),
                    inner_cte1_2 AS (
                        SELECT * FROM inner_cte1_1
                        UNION SELECT * FROM inner_cte1_1
                    )
                SELECT * FROM inner_cte1_1
                UNION ALL SELECT * FROM inner_cte1_2
            ),
            outer_cte_2 AS (
                WITH
                    inner_cte2_1 AS (
                        SELECT * FROM NestedCTE_t1 WHERE c2 = 1
                        EXCEPT SELECT * FROM outer_cte_1
                    ),
                    inner_cte2_2 AS (
                        SELECT * FROM NestedCTE_t1 WHERE c3 = 1
                        UNION SELECT * FROM inner_cte2_1
                    )
                SELECT * FROM inner_cte2_1
                UNION ALL SELECT * FROM outer_cte_1
            )
        SELECT * FROM outer_cte_1
        INTERSECT SELECT * FROM outer_cte_2
    ),
    outermost_cte_2 AS (
        SELECT * FROM outermost_cte_1
        UNION SELECT * FROM outermost_cte_1
    )
SELECT * FROM outermost_cte_1
UNION ALL SELECT * FROM outermost_cte_2;
```

## Nested CTE is supported in CTE subquery definition but not in general subquery

This query fails with the following error: `Msg 156, Level 15, State 1, Line 3. Incorrect syntax near the keyword 'WITH'.`

```sql
SELECT * FROM
(
    WITH
        inner_cte1_1 AS (SELECT * FROM NestedCTE_t1 WHERE c1 = 1),
        inner_cte1_2 AS  (SELECT * FROM inner_cte1_1)
    SELECT * FROM inner_cte1_2
) AS subq1;
```

## References to a CTE can't exceed its scope

This query fails with the following error: `Msg 208, Level 16, State 1, Line 1. Invalid object name 'inner_cte1_1'.`

```sql
;WITH
    outer_cte_1 AS (
        WITH
            inner_cte1_1 AS (
                SELECT * FROM NestedCTE_t1 WHERE c1 = 1
            ),
            inner_cte1_2 AS (
                SELECT * FROM inner_cte1_1 WHERE c2 = 1
            )
        SELECT * FROM inner_cte1_2
    ),
    outer_cte_2 AS (
        WITH inner_cte2_1 AS (
            SELECT * FROM NestedCTE_t1 WHERE c3 = 1
        )
        SELECT
            tmp2.*
        FROM
            inner_cte1_1 AS tmp1,
            inner_cte2_1 AS tmp2
        WHERE
            tmp1.c4 = tmp2.c4
    )
SELECT * FROM outer_cte_2;
```

## Related content

- [WITH common_table_expression (Transact-SQL)](with-common-table-expression-transact-sql.md?view=fabric&preserve-view=true)
- [SELECT (Transact-SQL)](select-transact-sql.md?view=fabric&preserve-view=true)
- [T-SQL surface area in Microsoft Fabric](/fabric/data-warehouse/tsql-surface-area)
