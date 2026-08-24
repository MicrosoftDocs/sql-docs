---
title: Fuzzy String Match
description: Look up data with fuzzy or approximate values.
author: markingmyname
ms.author: maghan
ms.reviewer: abhtiwar, randolphwest, wiassaf
ms.date: 07/13/2026
ms.service: sql
ms.topic: overview
ms.custom:
  - ignite-2025
monikerRange: "=azuresqldb-current || =fabric-sqldb"
---

# What is fuzzy string matching?

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

Use fuzzy, or approximate, string matching to check if two strings are similar, and calculate the difference between two strings. Use this capability to identify strings that might be different because of character corruption. Corruption includes spelling errors, transposed characters, missing characters, or abbreviations. Fuzzy string matching uses algorithms to detect similar sounding strings.

> [!NOTE]  
> Fuzzy string matching is currently in preview for [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and requires enabling the [preview feature database scoped configuration](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#preview-features).
>
> Fuzzy string matching is available in Azure SQL Managed Instance with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy).

## Fuzzy functions

| Function | Description |
| --- | --- |
| [EDIT_DISTANCE](../../t-sql/functions/edit-distance-transact-sql.md) | Calculates the number of insertions, deletions, substitutions, and transpositions needed to transform one string to another. |
| [EDIT_DISTANCE_SIMILARITY](../../t-sql/functions/edit-distance-similarity-transact-sql.md) | Calculates a similarity value ranging from 0 (indicating no match) to 100 (indicating full match). |
| [JARO_WINKLER_DISTANCE](../../t-sql/functions/jaro-winkler-distance-transact-sql.md) | Calculates the edit distance between two strings giving preference to strings that match from the beginning for a set prefix length. |
| [JARO_WINKLER_SIMILARITY](../../t-sql/functions/jaro-winkler-similarity-transact-sql.md) | Calculates a similarity value ranging from 0 (indicating no match) to 100 (indicating full match). |

> [!NOTE]  
> The following features currently are supported only on Azure SQL Database, SQL database in Microsoft Fabric, and Azure SQL Managed Instance configured with the *Always-up-to-date* update policy:
>
>  - Fuzzy string matching functions support **Windows collations** and **binary collations (`BIN` and `BIN2`)**. Comparison behavior, including case sensitivity, accent sensitivity, and linguistic rules, is determined by the collation of the input expressions.
>
>     - Non-binary `SQL_*` collations aren't supported. If you're using a non-binary `SQL_*` collation, consider using a corresponding Windows collation instead.
>
>     - If changing the database or column collation isn't feasible, you can apply a supported Windows collation to the arguments passed to a fuzzy string matching function by using the `COLLATE` clause. This approach allows the function to evaluate input values using a supported collation without requiring broader collation changes to the database.

## Examples

The following examples demonstrate the fuzzy string matching functions.

### Example table

Before you can run example queries, create and populate an example table.

To create and populate the example table, connect to a non-production user database, and run the following script:

```sql
-- Step 1: Create the table
CREATE TABLE WordPairs
(
    WordID INT IDENTITY (1, 1) PRIMARY KEY, -- Auto-incrementing ID
    WordUK NVARCHAR (50), -- UK English word
    WordUS NVARCHAR (50)  -- US English word
);

-- Step 2: Insert the data
INSERT INTO WordPairs (WordUK, WordUS)
VALUES ('Colour', 'Color'),
       ('Flavour', 'Flavor'),
       ('Centre', 'Center'),
       ('Theatre', 'Theater'),
       ('Organise', 'Organize'),
       ('Analyse', 'Analyze'),
       ('Catalogue', 'Catalog'),
       ('Programme', 'Program'),
       ('Metre', 'Meter'),
       ('Honour', 'Honor'),
       ('Neighbour', 'Neighbor'),
       ('Travelling', 'Traveling'),
       ('Grey', 'Gray'),
       ('Defence', 'Defense'),
       ('Practise', 'Practice'), -- Verb form in UK
       ('Practice', 'Practice'), -- Noun form in both
       ('Aluminium', 'Aluminum'),
       ('Cheque', 'Check'); -- Bank cheque vs. check
```

### Example *EDIT_DISTANCE*

```sql
SELECT WordUK,
       WordUS,
       EDIT_DISTANCE(WordUK, WordUS) AS Distance
FROM WordPairs
WHERE EDIT_DISTANCE(WordUK, WordUS) <= 2
ORDER BY Distance ASC;
```

Returns:

```output
WordUK                         WordUS                         Distance
------------------------------ ------------------------------ -----------
Practice                       Practice                       0
Aluminium                      Aluminum                       1
Honour                         Honor                          1
Neighbour                      Neighbor                       1
Travelling                     Traveling                      1
Grey                           Gray                           1
Defence                        Defense                        1
Practise                       Practice                       1
Colour                         Color                          1
Flavour                        Flavor                         1
Organise                       Organize                       1
Analyse                        Analyze                        1
Catalogue                      Catalog                        2
Programme                      Program                        2
Metre                          Meter                          2
Centre                         Center                         2
Theatre                        Theater                        2
```

### Example *EDIT_DISTANCE_SIMILARITY*

```sql
SELECT WordUK,
       WordUS,
       EDIT_DISTANCE_SIMILARITY(WordUK, WordUS) AS Similarity
FROM WordPairs
WHERE EDIT_DISTANCE_SIMILARITY(WordUK, WordUS) >= 75
ORDER BY Similarity DESC;
```

Returns:

```output
WordUK                         WordUS                         Similarity
------------------------------ ------------------------------ -----------
Practice                       Practice                       100
Travelling                     Traveling                      90
Aluminium                      Aluminum                       89
Neighbour                      Neighbor                       89
Organise                       Organize                       88
Practise                       Practice                       88
Defence                        Defense                        86
Analyse                        Analyze                        86
Flavour                        Flavor                         86
Colour                         Color                          83
Honour                         Honor                          83
Catalogue                      Catalog                        78
Programme                      Program                        78
Grey                           Gray                           75
```

### Example *JARO_WINKLER_DISTANCE*

```sql
SELECT WordUK,
       WordUS,
       JARO_WINKLER_DISTANCE(WordUK, WordUS) AS Distance
FROM WordPairs
WHERE JARO_WINKLER_DISTANCE(WordUK, WordUS) <= .05
ORDER BY Distance ASC;
```

Returns:

```output
WordUK                         WordUS                         Distance
------------------------------ ------------------------------ -----------
Practice                       Practice                       0
Travelling                     Traveling                      0.02
Neighbour                      Neighbor                       0.0222222222222223
Aluminium                      Aluminum                       0.0222222222222223
Theatre                        Theater                        0.0285714285714286
Flavour                        Flavor                         0.0285714285714286
Centre                         Center                         0.0333333333333333
Colour                         Color                          0.0333333333333333
Honour                         Honor                          0.0333333333333333
Catalogue                      Catalog                        0.0444444444444444
Programme                      Program                        0.0444444444444444
Metre                          Meter                          0.0466666666666667
```

### Example *JARO_WINKLER_SIMILARITY*

```sql
SELECT WordUK,
       WordUS,
       JARO_WINKLER_SIMILARITY(WordUK, WordUS) AS Similarity
FROM WordPairs
WHERE JARO_WINKLER_SIMILARITY(WordUK, WordUS) > 90
ORDER BY Similarity DESC;
```

Returns:

```output
WordUK                         WordUS                         Similarity
------------------------------ ------------------------------ -----------
Practice                       Practice                       100
Aluminium                      Aluminum                       98
Neighbour                      Neighbor                       98
Travelling                     Traveling                      98
Colour                         Color                          97
Flavour                        Flavor                         97
Centre                         Center                         97
Theatre                        Theater                        97
Honour                         Honor                          97
Catalogue                      Catalog                        96
Programme                      Program                        96
Metre                          Meter                          95
Organise                       Organize                       95
Practise                       Practice                       95
Analyse                        Analyze                        94
Defence                        Defense                        94
```

### Example query with all functions

The following query demonstrates all of the regular expression functions currently available.

```sql
SELECT T.source_string,
       T.target_string,
       EDIT_DISTANCE(T.source_string, T.target_string) AS ED_Distance,
       JARO_WINKLER_DISTANCE(T.source_string, T.target_string) AS JW_Distance,
       EDIT_DISTANCE_SIMILARITY(T.source_string, T.target_string) AS ED_Similarity,
       JARO_WINKLER_SIMILARITY(T.source_string, T.target_string) AS JW_Similarity
FROM (VALUES ('Black', 'Red'),
             ('Colour', 'Yellow'),
             ('Colour', 'Color'),
             ('Microsoft', 'Msft'),
             ('Regex', 'Regex')
     ) AS T(source_string, target_string);
```

Returns:

```output
source_string  target_string  ED_Distance    JW_Distance           ED_Similarity  JW_Similarity
-------------- -------------- -------------- --------------------- -------------- --------------
Black          Red            5              1                     0              0
Colour         Yellow         5              0.444444444444445     17             55
Colour         Color          1              0.0333333333333333    83             96
Microsoft      Msft           5              0.491666666666667     44             50
Regex          Regex          0              0                     100            100
```

## Clean up

After you're done using the example data, delete the example table:

```sql
IF OBJECT_ID('dbo.WordPairs', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.WordPairs;
END
```

## Related content

- [EDIT_DISTANCE (Transact-SQL) preview](../../t-sql/functions/edit-distance-transact-sql.md)
- [EDIT_DISTANCE_SIMILARITY (Transact-SQL) preview](../../t-sql/functions/edit-distance-similarity-transact-sql.md)
- [JARO_WINKLER_DISTANCE (Transact-SQL) preview](../../t-sql/functions/jaro-winkler-distance-transact-sql.md)
- [JARO_WINKLER_SIMILARITY (Transact-SQL) preview](../../t-sql/functions/jaro-winkler-similarity-transact-sql.md)
