---
title: "Server Configuration: transform noise words"
description: "Learn about the transform noise words option. See how it can be useful in some SQL Server full-text queries that include noise words (stopwords)."
author: adbadram
ms.author: adbadram
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "full-text queries [SQL Server], performance"
  - "transform noise words option"
  - "noise words [full-text search]"
  - "full-text search [SQL Server], stopwords"
  - "stopwords [full-text search]"
---
# Server configuration: transform noise words

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use the `transform noise words` server configuration option to suppress an error message if noise words (also known as [stopwords](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md)) cause a Boolean operation on a full-text query to return zero rows. This option is useful for full-text queries that use the CONTAINS predicate in which Boolean operations or NEAR operations include noise words. The possible values are described in the following table.

| Value | Description |
| --- | --- |
| 0 (default) | Noise words (or stopwords) aren't transformed. When a full-text query contains noise words, the query returns zero rows, and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] raises a warning.<br /><br />**Note:** The warning is a run-time warning. Therefore, if the full-text clause in the query isn't executed, the warning isn't raised. For a local query, only one warning is raised, even when there are multiple full-text query clauses. For a remote query, the linked server might not relay the error; therefore, the warning might not be raised. |
| 1 | Noise words (or stopwords) are transformed. They're ignored, and the rest of the query is evaluated.<br /><br />If noise words are specified in a proximity term, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] removes them. For example, the noise word `is` is removed from `CONTAINS(<column_name>, 'NEAR (hello,is,goodbye)')`, transforming the search query into `CONTAINS(<column_name>, 'NEAR(hello,goodbye)')`. **Note:** `CONTAINS(<column_name>, 'NEAR(hello,is)')` would be transformed into simply `CONTAINS(<column_name>, hello)` because there's only one valid search term. |

## Effects of the transform noise words setting

This section illustrates the behavior of queries containing a noise word, `the`, under the alternate settings of `transform noise words`. The sample full-text query strings are assumed to run against a table row containing the following data: `[1, "The black cat"]`.

> [!NOTE]  
> All such scenarios can generate a noise word warning.

- With transform noise words set to 0:

  | Query string | Result |
  | --- | --- |
  | `cat` AND `the` | No results (The behavior is the same for `the` AND `cat`.) |
  | `cat` NEAR `the` | No results (The behavior is the same for `the` NEAR `cat`.) |
  | `the` AND NOT `black` | No results |
  | `black` AND NOT `the` | No results |

- With transform noise words set to 1:

  | Query string | Result |
  | --- | --- |
  | `cat` AND `the` | Hit for row with ID 1 |
  | `cat` NEAR `the` | Hit for row with ID 1 |
  | `the` AND NOT `black` | No results |
  | `black` AND NOT `the` | Hit for row with ID 1 |

## Examples

The following example sets `transform noise words` to `1`.

```sql
EXECUTE sp_configure 'show advanced options', 1;
RECONFIGURE;
GO

EXECUTE sp_configure 'transform noise words', 1;
RECONFIGURE;
GO
```

## Related content

- [Server configuration options](server-configuration-options-sql-server.md)
- [CONTAINS (Transact-SQL)](../../t-sql/queries/contains-transact-sql.md)
