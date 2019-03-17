---
title: "Create and Manage Full-Text Indexes | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "full-text indexes [SQL Server], about"
ms.assetid: f8a98486-5438-44a8-b454-9e6ecbc74f83
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Create and Manage Full-Text Indexes
  The information in full-text indexes is used by the Full-Text Engine to compile full-text queries that can quickly search a table for particular words or combinations of words. A full-text index stores information about significant words and their location within one or more columns of a database table. A full-text index is a special type of token-based functional index that is built and maintained by the Full-Text Engine for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The process of building a full-text index differs from building other types of indexes. Instead of constructing a B-tree structure based on a value stored in a particular row, the Full-Text Engine builds an inverted, stacked, compressed index structure based on individual tokens from the text being indexed.  The size of a full-text index is limited only by the available memory resources of the computer on which the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running.  
  
 Beginning in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], the full-text indexes are integrated with the Database Engine, instead of residing in the file system as in previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a new database, the full-text catalog is now a virtual object that does not belong to any filegroup; it is merely a logical concept that refers to a group of the full-text indexes. Note, however, that during upgrade of a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database, any full-text catalog that contains data files, a new filegroup is created; for more information, see [Upgrade Full-Text Search](upgrade-full-text-search.md).  
  
> [!NOTE]  
>  In [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions, the Full-Text Engine resides in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process, rather than in a separate service. Integrating the Full-Text Engine into the Database Engine improves full-text manageability, optimization of mixed query, and overall performance.  
  
 Only one full-text index is allowed per table. For a full-text index to be created on a table, the table must have a single, unique nonnull column. You can build a full-text index on columns of type `char`, `varchar`, `nchar`, `nvarchar`, `text`, `ntext`, `image`, `xml`, `varbinary`, and `varbinary(max)` can be indexed for full-text search. Creating a full-text index on a column whose data type is  `varbinary`, `varbinary(max)`, `image`, or `xml` requires that you specify a type column. A *type column* is a table column in which you store the file extension (.doc, .pdf, .xls, and so forth) of the document in each row.  
  
 The process of creating and maintaining a full-text index is called a *population* (also known as a *crawl*). There are three types of full-text index population: full population, change tracking-based population, and incremental timestamp-based population. For more information, see [Populate Full-Text Indexes](populate-full-text-indexes.md).  
  
##  <a name="tasks"></a> Common Tasks  
 **To create a full-text index**  
  
-   [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-fulltext-index-transact-sql)  
  
 **To alter a full-text index**  
  
-   [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-fulltext-index-transact-sql)  
  
 **To drop a full-text index**  
  
-   [DROP FULLTEXT INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-fulltext-index-transact-sql)  
  
 [In This Topic](#top)  
  
##  <a name="structure"></a> Full-Text Index Structure  
 A good understanding of the structure of a full-text index will help you understand how the Full-Text Engine works. This topic uses the following excerpt of the **Document** table in [!INCLUDE[ssSampleDBCoShort](../../includes/sssampledbcoshort-md.md)] as an example table. This excerpt shows only two columns, the **DocumentID** column and the **Title** column, and three rows from the table.  
  
 For this example, we will assume that a full-text index has been created on the **Title** column.  
  
|DocumentID|Title|  
|----------------|-----------|  
|1|Crank Arm and Tire Maintenance|  
|2|Front Reflector Bracket and Reflector Assembly 3|  
|3|Front Reflector Bracket Installation|  
  
 For example, the following table, which shows Fragment 1, depicts the contents of the full-text index created on the **Title** column of the **Document** table. Full-text indexes contain more information than is presented in this table. The table is a logical representation of a full-text index and is provided for demonstration purposes only. The rows are stored in a compressed format to optimize disk usage.  
  
 Notice that the data has been inverted from the original documents. Inversion occurs because the keywords are mapped to the document IDs. For this reason, a full-text index is often referred to as an inverted index.  
  
 Also notice that the keyword "and" has been removed from the full-text index. This is done because "and" is a stopword, and removing stopwords from a full-text index can lead to substantial savings in disk space thereby improving query performance. For more information about stopwords, see [Configure and Manage Stopwords and Stoplists for Full-Text Search](configure-and-manage-stopwords-and-stoplists-for-full-text-search.md).  
  
 **Fragment 1**  
  
|Keyword|ColId|DocId|Occurrence|  
|-------------|-----------|-----------|----------------|  
|Crank|1|1|1|  
|Arm|1|1|2|  
|Tire|1|1|4|  
|Maintenance|1|1|5|  
|Front|1|2|1|  
|Front|1|3|1|  
|Reflector|1|2|2|  
|Reflector|1|2|5|  
|Reflector|1|3|2|  
|Bracket|1|2|3|  
|Bracket|1|3|3|  
|Assembly|1|2|6|  
|3|1|2|7|  
|Installation|1|3|4|  
  
 The **Keyword** column contains a representation of a single token extracted at indexing time. Word breakers determine what makes up a token.  
  
 The **ColId** column contains a value that corresponds to a particular column that is full-text indexed.  
  
 The `DocId` column contains values for an eight-byte integer that maps to a particular full-text key value in a full-text indexed table. This mapping is necessary when the full-text key is not an integer data type. In such cases, mappings between full-text key values and `DocId` values are maintained in a separate table called the DocId Mapping table. To query for these mappings use the [sp_fulltext_keymappings](/sql/relational-databases/system-stored-procedures/sp-fulltext-keymappings-transact-sql) system stored procedure. To satisfy a search condition, DocId values from the above table need to be joined with the DocId Mapping table to retrieve rows from the base table being queried. If the full-text key value of the base table is an integer type, the value directly serves as the DocId and no mapping is necessary. Therefore, using integer full-text key values can help optimize full-text queries.  
  
 The **Occurrence** column contains an integer value. For each DocId value, there is a list of occurrence values that correspond to the relative word offsets of the particular keyword within that DocId. Occurrence values are useful in determining phrase or proximity matches, for example, phrases have numerically adjacent occurrence values. They are also useful in computing relevance scores; for example, the number of occurrences of a keyword in a DocId may be used in scoring.  
  
 [In This Topic](#top)  
  
##  <a name="fragments"></a> Full-Text Index Fragments  
 The logical full-text index is usually split across multiple internal tables. Each internal table is called a full-text index fragment. Some of these fragments might contain newer data than others. For example, if a user updates the following row whose DocId is 3 and the table is auto change-tracked, a new fragment is created.  
  
|DocumentID|Title|  
|----------------|-----------|  
|3|Rear Reflector|  
  
 In the following example, which shows Fragment 2, the fragment contains newer data about DocId 3 compared to Fragment 1. Therefore, when the user queries for "Rear Reflector" the data from Fragment 2 is used for DocId 3. Each fragment is marked with a creation timestamp that can be queried by using the [sys.fulltext_index_fragments](/sql/relational-databases/system-catalog-views/sys-fulltext-index-fragments-transact-sql) catalog view.  
  
 **Fragment 2**  
  
|Keyword|ColId|DocId|Occ|  
|-------------|-----------|-----------|---------|  
|Rear|1|3|1|  
|Reflector|1|3|2|  
  
 As can be seen from Fragment 2, full-text queries need to query each fragment internally and discard older entries. Therefore, too many full-text index fragments in the full-text index can lead to substantial degradation in query performance. To reduce the number of fragments, reorganize the fulltext catalog by using the REORGANIZE option of the [ALTER FULLTEXT CATALOG](/sql/t-sql/statements/alter-fulltext-catalog-transact-sql)[!INCLUDE[tsql](../../includes/tsql-md.md)] statement. This statement performs a *master merge*, which merges the fragments into a single larger fragment and removes all obsolete entries from the full-text index.  
  
 After being reorganized, the example index would contain the following rows:  
  
|Keyword|ColId|DocId|Occ|  
|-------------|-----------|-----------|---------|  
|Crank|1|1|1|  
|Arm|1|1|2|  
|Tire|1|1|4|  
|Maintenance|1|1|5|  
|Front|1|2|1|  
|Rear|1|3|1|  
|Reflector|1|2|2|  
|Reflector|1|2|5|  
|Reflector|1|3|2|  
|Bracket|1|2|3|  
|Assembly|1|2|6|  
|3|1|2|7|  
  
 [In This Topic](#top)  
  
  
