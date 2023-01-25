---
description: "Configure and Manage Stopwords and Stoplists for Full-Text Search"
title: "Configure & manage stopwords & stoplists for Full-Text Search"
ms.date: "02/02/2017"
ms.service: sql
ms.subservice: search
ms.topic: conceptual
helpviewer_keywords: 
  - "stoplists [full-text search]"
  - "full-text search [SQL Server], stoplists"
  - "full-text search [SQL Server], noise words"
  - "noise words [full-text search]"
  - "full-text search [SQL Server], stopwords"
  - "stopwords [full-text search]"
ms.assetid: 43b5ce7b-9f09-4443-8a5b-c3da6eb28bcc
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
ms.custom: "seo-lt-2019"
---
# Configure and Manage Stopwords and Stoplists for Full-Text Search
[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]
  To prevent a full-text index from becoming bloated, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has a mechanism that discards commonly occurring strings that do not help the search. These discarded strings are called *stopwords*. During index creation, the Full-Text Engine omits stopwords from the full-text index. This means that full-text queries will not search on stopwords.  
   
**Stopwords**. A stopword can be a word with meaning in a specific language. For example, in the English language, words such as "a," "and," "is," and "the" are left out of the full-text index since they are known to be useless to a search. A stopword can also be a *token* that does not have linguistic meaning.  

**Stoplists**. Stopwords are managed in databases using objects called stoplists. A *stoplist* is a list of stopwords that, when associated with a full-text index, is applied to full-text queries on that index.
   
## Use an existing stoplist  
 You can use an existsing stoplist in the following ways:  
  
-   Use the system-supplied stoplist in the database. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ships with a system stoplist that contains the most commonly used stopwords for each supported language, that is for every language associated with given word breakers by default. You can copy the system stoplist and customize your copy by adding and removing stopwords.  
  
     The system stoplist is installed in the [Resource](../../relational-databases/databases/resource-database.md) database.  
  
-   Use an existing custom stoplist from another database in the current server instance, then add or drop stopwords as appropriate.  
  
## Create a new stoplist 
### Create a new stoplist with Transact-SQL
Use [CREATE FULLTEXT STOPLIST](../../t-sql/statements/create-fulltext-stoplist-transact-sql.md).

### Create a new stoplist with Management Studio
  
1.  In Object Explorer, expand the server.  
  
2.  Expand **Databases**, and then expand the database in which you want to create the full-text stoplist.  
  
3.  Expand **Storage**, and then right-click **Full-Text Stoplists**.  
  
4.  Select **New Full-Text Stoplist**.  
  
5.  Enter your new stoplist's name.  
  
6.  Optionally, specify someone else as the stoplist owner.  
  
7.  Select one of the following create stoplist options:  
  
    -   **Create an empty stoplist**  
  
    -   **Create from the system stoplist**  
  
    -   **Create from an existing full-text stoplist**  
  
     For more information, see [New Full-Text Stoplist &#40;General Page&#41;](/previous-versions/sql/sql-server-2016/cc280518(v=sql.130)).  
  
8.  Select **OK**.
  
##  Use a stoplist in full-text queries  
 To use a stoplist in queries, you must associate it with a full-text index. You can attach a stoplist to a full-text index when you create the index, or you can alter the index later to add a stoplist.  
  
### Create a full-text index and associate a stoplist with it
Use [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-index-transact-sql.md).
  
### Associate or disassociate a stoplist with an existing full-text index
Use [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-index-transact-sql.md). 
  
## Change the stopwords in a stoplist  
### Add or drop stopwords from a stoplist with Transact-SQL
Use [ALTER FULLTEXT STOPLIST &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-stoplist-transact-sql.md).
  
### Add or drop stopwords from a stoplist with Management Studio  
  
1.  In Object Explorer, expand the server.  
  
2.  Expand **Databases**, and then expand the database.  
  
3.  Expand **Storage**, and then select **Full Text Stoplists**.  
  
4.  Right-click the stoplist whose properties you want to change, and select **Properties**.  
  
5.  In the [Full-Text Stoplist Properties](/previous-versions/sql/sql-server-2016/cc280415(v=sql.130)) dialog box:  
  
    1.  In the **Action** list box, select one of the following actions: **Add stopword**, **Delete stopword**, **Delete all stopwords**, or **Clear stoplist**.  
  
    2.  If the **Stopword** text box is enabled for the selected action, enter a single stopword. This stopword must be unique; that is, not yet in this stoplist for the language that you select.  
  
    3.  If the **Full-text language** list box is enabled for the selected action, select a language.  
  
6.  Select **OK**.

## Manage stoplists and their usage
  
### View all the stopwords in a stoplist
Use [sys.fulltext_stopwords &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-stopwords-transact-sql.md). 
  
### Get info about all the stoplists in the current database
Use [sys.fulltext_stoplists &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-stoplists-transact-sql.md) and  [sys.fulltext_stopwords &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-stopwords-transact-sql.md).
  
### View the tokenization result of a word breaker, thesaurus, and stoplist combination
Use [sys.dm_fts_parser &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-parser-transact-sql.md).

### Suppress an error message if stopwords cause a Boolean operation on a full-text query to fail
Use the [transform noise words Server Configuration Option](../../database-engine/configure-windows/transform-noise-words-server-configuration-option.md). 
   
## More info about stopword position
 Although it ignores the inclusion of stopwords, the full-text index does take into account their position. For example, consider the phrase, "Instructions are applicable to these Adventure Works Cycles models". The following table depicts the position of the words in the phrase:  
  
|Word|Position|  
|----------|--------------|  
|Instructions|1|  
|are|2|  
|applicable|3|  
|to|4|  
|these|5|  
|Adventure|6|  
|Works|7|  
|Cycles|8|  
|models|9|  
  
 The stopwords "are", "to", and "these" that are in positions 2, 4, and 5 are left out of the full-text index. However, their positional information is maintained, thereby leaving the position of the other words in the phrase unaffected.   
  
## Upgrade noise words from SQL Server 2005  
 [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] noise words have been replaced by stopwords. When a database is upgraded from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], the noise-word files are no longer used. However, the noise-word files are stored in the FTDATA\ FTNoiseThesaurusBak folder, and you can use them later when updating or building the corresponding stoplists. For information about upgrading noise-word files to stoplists, see [Upgrade Full-Text Search](../../relational-databases/search/upgrade-full-text-search.md).  
  
  
