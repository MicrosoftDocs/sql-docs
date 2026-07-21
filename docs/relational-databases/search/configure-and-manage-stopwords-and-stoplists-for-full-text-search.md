---
title: Configure and Manage Stopwords and Stoplists for Full-Text Search
description: Configure and Manage Stopwords and Stoplists for Full-Text Search
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/20/2026
ms.service: sql
ms.subservice: search
ms.topic: how-to
helpviewer_keywords:
  - "stoplists [full-text search]"
  - "full-text search [SQL Server], stoplists"
  - "full-text search [SQL Server], noise words"
  - "noise words [full-text search]"
  - "full-text search [SQL Server], stopwords"
  - "stopwords [full-text search]"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-current || =azuresqldb-mi-current"
---
# Configure and manage stopwords and stoplists for Full-Text Search

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

To prevent a full-text index from becoming bloated, the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] has a mechanism that discards commonly occurring strings that don't help the search. These discarded strings are called *stopwords*. During index creation, the Full-Text Engine omits stopwords from the full-text index. This means that full-text queries don't search on stopwords.

| Term | Description |
| --- | --- |
| **Stopwords** | A stopword can be a word with meaning in a specific language. For example, in the English language, words such as `a`, `and`, `is`, and `the` are left out of the full-text index since they are known to be useless to a search. A stopword can also be a *token* that doesn't have linguistic meaning. |
| **Stoplists** | Stopwords are managed in databases by using objects called stoplists. A *stoplist* is a list of stopwords that, when associated with a full-text index, is applied to full-text queries on that index. |

## Use an existing stoplist

You can use an existing stoplist in the following ways:

- Use the system-supplied stoplist in the database. The [!INCLUDE [ssde-md](../../includes/ssde-md.md)] ships with a system stoplist that contains the most commonly used stopwords for each supported language, that is for every language associated with given word breakers by default. You can copy the system stoplist and customize your copy by adding and removing stopwords.

  The system stoplist is installed in the [Resource Database](../databases/resource-database.md).

- Use an existing custom stoplist from another database in the current server instance, and then add or drop stopwords as appropriate.

## Create a new stoplist

You can create a stoplist by using [!INCLUDE [tsql-md](../../includes/tsql-md.md)] (T-SQL) or [!INCLUDE [ssmanstudiofull-md](../../includes/ssmanstudiofull-md.md)] (SSMS).

### Create a new stoplist with Transact-SQL

Use [CREATE FULLTEXT STOPLIST](../../t-sql/statements/create-fulltext-stoplist-transact-sql.md).

### Create a new stoplist with SQL Server Management Studio

1. In Object Explorer, expand the server.

1. Expand **Databases**, and then expand the database in which you want to create the full-text stoplist.

1. Expand **Storage**, and then right-click **Full-Text Stoplists**.

1. Select **New Full-Text Stoplist**.

1. Enter your new stoplist's name.

1. Optionally, specify someone else as the stoplist owner.

1. Select one of the following create stoplist options:

   - **Create an empty stoplist**

   - **Create from the system stoplist**

   - **Create from an existing full-text stoplist**

   For more information, see [New Full-Text Stoplist (General Page)](/previous-versions/sql/sql-server-2016/cc280518(v=sql.130)).

1. Select **OK**.

## Use a stoplist in full-text queries

To use a stoplist in queries, you must associate it with a full-text index. You can attach a stoplist to a full-text index when you create the index, or you can alter the index later to add a stoplist.

| Task | Method |
| --- | --- |
| Create a full-text index and associate it with a stoplist | Use [CREATE FULLTEXT INDEX](../../t-sql/statements/create-fulltext-index-transact-sql.md). |
| Associate or disassociate a stoplist with an existing full-text index | Use [ALTER FULLTEXT INDEX](../../t-sql/statements/alter-fulltext-index-transact-sql.md). |

## Change the stopwords in a stoplist

You can modify the stopwords in a stoplist by using [!INCLUDE [tsql-md](../../includes/tsql-md.md)] (T-SQL) or [!INCLUDE [ssmanstudiofull-md](../../includes/ssmanstudiofull-md.md)] (SSMS).

### Add or drop stopwords from a stoplist with Transact-SQL

Use [ALTER FULLTEXT STOPLIST](../../t-sql/statements/alter-fulltext-stoplist-transact-sql.md).

### Add or drop stopwords from a stoplist with SQL Server Management Studio

1. In Object Explorer, expand the server.

1. Expand **Databases**, and then expand the database.

1. Expand **Storage**, and then select **Full Text Stoplists**.

1. Right-click the stoplist whose properties you want to change, and select **Properties**.

1. In the [Full-Text Stoplist Properties](/previous-versions/sql/sql-server-2016/cc280415(v=sql.130)) dialog box:

   1. In the **Action** list box, select one of the following actions: **Add stopword**, **Delete stopword**, **Delete all stopwords**, or **Clear stoplist**.

   1. If the **Stopword** text box is enabled for the selected action, enter a single stopword. This stopword must be unique. It can't already be in this stoplist for the language that you select.

   1. If the **Full-text language** list box is enabled for the selected action, select a language.

1. Select **OK**.

## Manage stoplists and their usage

Use the following methods to view and manage stoplists.

| Task | Method |
| --- | --- |
| View all the stopwords in a stoplist | Use [sys.fulltext_stopwords](../system-catalog-views/sys-fulltext-stopwords-transact-sql.md). |
| Get information about all the stoplists in the current database | Use [sys.fulltext_stoplists](../system-catalog-views/sys-fulltext-stoplists-transact-sql.md) and [sys.fulltext_stopwords](../system-catalog-views/sys-fulltext-stopwords-transact-sql.md). |
| View the tokenization result of a word breaker, thesaurus, and stoplist combination | Use [sys.dm_fts_parser](../system-dynamic-management-objects/sys-dm-fts-parser-transact-sql.md). |
| Suppress an error message if stopwords cause a Boolean operation on a full-text query to fail | Use [Server configuration: transform noise words](../../database-engine/configure-windows/transform-noise-words-server-configuration-option.md). |

## More information about stopword position

Although the full-text index ignores the inclusion of stopwords, it does take into account their position. For example, consider the phrase, `Instructions are applicable to these Adventure Works Cycles models`. The following table depicts the position of the words in the phrase:

| Word | Position |
| --- | --- |
| `Instructions` | 1 |
| `are` | 2 |
| `applicable` | 3 |
| `to` | 4 |
| `these` | 5 |
| `Adventure` | 6 |
| `Works` | 7 |
| `Cycles` | 8 |
| `models` | 9 |

The stopwords `are`, `to`, and `these` that are in positions 2, 4, and 5 are left out of the full-text index. However, their positional information is maintained, thereby leaving the position of the other words in the phrase unaffected.

## Upgrade noise words from SQL Server 2005

[!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] noise words are replaced by stopwords. When a database is upgraded from [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)], the noise-word files are no longer used. However, the noise-word files are stored in the `FTDATA\FTNoiseThesaurusBak` folder, and you can use them later when you update or build the corresponding stoplists. For information about upgrading noise-word files to stoplists, see [Upgrade Full-Text Search](upgrade-full-text-search.md).
