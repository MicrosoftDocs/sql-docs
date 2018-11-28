---
title: "Get Started with Full-Text Search | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "full-text catalogs [SQL Server], creating"
  - "full-text indexes [SQL Server], creating"
  - "full-text search [SQL Server], about"
  - "full-text search [SQL Server], setting up"
ms.assetid: 1fa628ba-0ee4-4d8f-b086-c4e52962ca4a
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Get Started with Full-Text Search
  Databases in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are full-text enabled by default. However, to use a full-text index on a table, you must set up full-text indexing capability on the columns of the tables that you want to access using the Full-Text Engine.  
  
##  <a name="configure"></a> Configuring a Database for Full-Text Search  
 For any scenario, a database administrator performs the following basic steps to configure table columns in a database for full-text search:  
  
1.  Create a full-text catalog.  
  
2.  On each table that you want to search, create a full-text index by:  
  
    1.  Identify each text columns that you want to include in the full-text index.  
  
    2.  If a given column contains documents stored as binary data (`varbinary(max)`, or `image` data), you must specify a table column (the *type column*) that identifies the type of each document in the column being indexed.  
  
    3.  Specify the language that you want full-text search to use on the documents in the column.  
  
    4.  Choose the change-tracking mechanism that you want to use on the full-text index to track changes in the base table and its columns.  
  
 Full-text search supports multiple languages through the use of the following *linguistic components*: word breakers and stemmers, stoplists that contain stopwords (also known as noise words), and thesaurus files. Thesaurus files and, in some cases, stoplists require configuration by a database administrator. A given thesaurus file supports all full-text indexes that use the corresponding language, and a given stoplist can be associated with as many full-text indexes as you want.  
  
##  <a name="setup"></a> Setting Up a Full-Text Catalog and Index  
 This involves the following basic steps:  
  
1.  Create a full-text catalog to store full-text indexes.  
  
     Each full-text index must belong to a full-text catalog. You can create a separate text catalog for each full-text index, or you can associate multiple full-text indexes with a given catalog. A full-text catalog is a virtual object and does not belong to any filegroup. The catalog is a logical concept that refers to a group of full-text indexes.  
  
2.  Create a full-text index on the table or indexed view.  
  
     A full-text index is a special type of token-based functional index that is built and maintained by the Full-Text Engine. To create full-text search on a table or view, it must have a unique, single-column, non-nullable index. The Full-Text Engine requires this unique index to map each row in the table to a unique, compressible key. A full-text index can include `char`, `varchar`, `nchar`, `nvarchar`, `text`, `ntext`, `image`, `xml`, `varbinary`, and `varbinary(max)` columns. For more information, see [Create and Manage Full-Text Indexes](create-and-manage-full-text-indexes.md).  
  
 Before learning about creating full-text indexes, it is important to consider how they differ from regular [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] indexes. The following table lists the differences.  
  
|Full-text indexes|Regular SQL Server indexes|  
|------------------------|--------------------------------|  
|Only one full-text index allowed per table.|Several regular indexes allowed per table.|  
|The addition of data to full-text indexes, called a *population*, can be requested through either a schedule or a specific request, or can occur automatically with the addition of new data.|Updated automatically when the data upon which they are based is inserted, updated, or deleted.|  
|Grouped within the same database into one or more full-text catalogs.|Not grouped.|  
  
  
##  <a name="options"></a> Choosing Options for a Full-Text Index  
 This section covers the following:  
  
-   Choosing the column language  
  
-   Choosing a filegroup for a full-text index  
  
-   Assigning the full-text index to a full-text catalog  
  
-   Associating a stoplist with the full-text index  
  
-   Updating a full-text index  
  
### Choosing the Column Language  
 For information about things to consider when you are choosing the column language, see [Choose a Language When Creating a Full-Text Index](choose-a-language-when-creating-a-full-text-index.md).  
  
### Choosing a Filegroup for a Full-Text Index  
 The process of building a full-text index is fairly I/O intensive (on a high level, it consists of reading data from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and then propagating the filtered data to the full-text index). As a best practice, locate a full-text index in the database filegroup that is best for maximizing I/O performance or locate the full-text indexes in a different filegroup on another volume.  
  
 When ease of management is important to you, we recommend that you store table data and any affiliated full-text catalogs in the same filegroup. Sometimes, for performance reasons, you might want to have the table data and the full-text index in different filegroups that are stored on different volumes to maximize I/O parallelism.  
  
  
### Assigning the Full-Text Index to a Full-Text Catalog  
 It is important to plan the placement of full-text indexes for tables in full-text catalogs.  
  
 We recommend associating tables with the same update characteristics (such as small number of changes versus large number of changes, or tables that change frequently during a particular time of day) together under the same full-text catalog. By setting up full-text catalog population schedules, full-text indexes stay synchronous with the tables without adversely affecting the resource usage of the database server during periods of high database activity.  
  
 When you assign a table to a full-text catalog, consider the following guidelines:  
  
-   Always select the smallest unique index available for your full-text unique key. (A 4-byte, integer-based index is optimal.) This reduces the resources required by [!INCLUDE[msCoName](../../includes/msconame-md.md)] Search service in the file system significantly. If the primary key is large (over 100 bytes), consider choosing another unique index in the table (or creating another unique index) as the full-text unique key. Otherwise, if the full-text unique key size exceeds the maximum size allowed (900 bytes), full-text population will not be able to proceed.  
  
-   If you are indexing a table that has millions of rows, assign the table to its own full-text catalog.  
  
-   Consider the amount of changes occurring in the tables being full-text indexed, as well as the total number of rows. If the total number of rows being changed, together with the numbers of rows in the table present during the last full-text population, represents millions of rows, assign the table to its own full-text catalog.  
  
  
### Associating a Stoplist with the Full-Text Index  
 [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] introduces stoplists. A *stoplist* is a list of stopwords, also known as noise words. A stoplist is associated with each full-text index, and the words in that stoplist are applied to full-text queries on that index. By default, the system stoplist is associated with a new full-text index. However, you can create and use your own stoplist instead. For more information, see [Configure and Manage Stopwords and Stoplists for Full-Text Search](configure-and-manage-stopwords-and-stoplists-for-full-text-search.md).  
  
 For example, the following [CREATE FULLTEXT STOPLIST](/sql/t-sql/statements/create-fulltext-stoplist-transact-sql)[!INCLUDE[tsql](../../../includes/tsql-md.md)] statement creates a new full-text stoplist named myStoplist3 by copying from the system stoplist:  
  
```  
CREATE FULLTEXT STOPLIST myStoplist FROM SYSTEM STOPLIST;  
GO  
```  
  
 The following [ALTER FULLTEXT STOPLIST](/sql/t-sql/statements/alter-fulltext-stoplist-transact-sql)[!INCLUDE[tsql](../../../includes/tsql-md.md)] statement alters a stoplist named myStoplist, adding the word 'en', first for Spanish and then for French:  
  
```  
ALTER FULLTEXT STOPLIST MyStoplist ADD 'en' LANGUAGE 'Spanish';  
ALTER FULLTEXT STOPLIST MyStoplist ADD 'en' LANGUAGE 'French';  
GO  
```  
  
  
### Updating a Full-Text Index  
 Like regular [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] indexes, full-text indexes can be automatically updated as data is modified in the associated tables. This is the default behavior. Alternatively, you can keep your full-text indexes up-to-date manually or at specified scheduled intervals. Populating a full-text index can be time-consuming and resource-intensive, therefore, index updating is usually performed as an asynchronous process that runs in the background and keeps the full-text index up to date after modifications in the base table. Updating a full-text index immediately after each change in the base table can be resource-intensive. Therefore, if you have a very high update/insert/delete rate, you might experience some degradation in query performance. If this occurs, consider scheduling manual change tracking updates to keep up with the numerous changes from time to time, rather than competing with queries for resources.  
  
 To monitor the population status, use either the FULLTEXTCATALOGPROPERTY or OBJECTPROPERTYEX functions. To get the catalog population status, run the following statement:  
  
```  
SELECT FULLTEXTCATALOGPROPERTY('AdvWksDocFTCat', 'Populatestatus');  
```  
  
 Typically, if a full population is in progress, the result returned is 1.  
  
  
##  <a name="example"></a> Example: Setting Up Full-Text Search  
 The following two-part example creates a full-text catalog named `AdvWksDocFTCat` on the AdventureWorks database and then creates a full-text index on the `Document` table in [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)]. This statement creates the full-text catalog in the default directory specified during setup. The folder named `AdvWksDocFTCat` is in the default directory.  
  
1.  To create a full-text catalog named `AdvWksDocFTCat`, the example uses a [CREATE FULLTEXT CATALOG](/sql/t-sql/statements/create-fulltext-catalog-transact-sql) statement:  
  
    ```  
    USE AdventureWorks;  
    GO  
    CREATE FULLTEXT CATALOG AdvWksDocFTCat;  
    ```  
  
2.  Before you can create a full-text index on the Document table, ensure that the table has a unique, single-column, non-nullable index. The following [CREATE INDEX](/sql/t-sql/statements/create-index-transact-sql) statement creates a unique index, `ui_ukDoc`, on the DocumentID column of the Document table:  
  
    ```  
    CREATE UNIQUE INDEX ui_ukDoc ON Production.Document(DocumentID);  
    ```  
  
3.  After you have a unique key, you can create a full-text index on the `Document` table by using the following [CREATE FULLTEXT INDEX](/sql/t-sql/statements/create-fulltext-index-transact-sql) statement.  
  
    ```  
    CREATE FULLTEXT INDEX ON Production.Document  
    (  
        Document                         --Full-text index column name   
            TYPE COLUMN FileExtension    --Name of column that contains file type information  
            Language 2057                 --2057 is the LCID for British English  
    )  
    KEY INDEX ui_ukDoc ON AdvWksDocFTCat --Unique index  
    WITH CHANGE_TRACKING AUTO            --Population type;  
    GO  
  
    ```  
  
     The TYPE COLUMN defined in this example specifies the type column in the table that contains the type of the document in each row of the column 'Document' (which is of binary type). The type column stores the user-supplied file extension-".doc", ".xls", and so on-of the document in a given row. The Full-Text Engine uses the file extension in a given row to invoke the correct filter to use for parsing the data in that row. After the filter has parsed the binary data of the row, the specified word breaker will parse the content (in this example, the word breaker for British English is used). Note that the filtering process happens only at indexing time or if a user inserts or updates a column in the base table while automatic change tracking is enabled for the full-text index. For more information, see [Configure and Manage Filters for Search](configure-and-manage-filters-for-search.md).  
  
  
##  <a name="tasks"></a> Common Tasks  
  
### To Create a Full-Text Catalog  
  
-   [CREATE FULLTEXT CATALOG &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-fulltext-catalog-transact-sql)  
  
-   [Create and Manage Full-Text Catalogs](create-and-manage-full-text-catalogs.md)  
  
### To View the Indexes of a Table (or View)  
  
-   [sys.indexes &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-indexes-transact-sql)  
  
### To Create a Unique Index  
  
-   [CREATE INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-index-transact-sql)  
  
-   [Open Table Designer &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/visual-database-tools.md)  
  
### To Create a Full-Text Index  
  
-   [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-fulltext-index-transact-sql)  
  
-   [Create and Manage Full-Text Indexes](create-and-manage-full-text-indexes.md)  
  
### To View Information about a Full-Text Index  
  
|Catalog or Dynamic Management View|Description|  
|----------------------------------------|-----------------|  
|[sys.fulltext_index_catalog_usages &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-index-catalog-usages-transact-sql)|Returns a row for each full-text catalog to full-text index reference.|  
|[sys.fulltext_index_columns &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-index-columns-transact-sql)|Contains a row for each column that is part of a full-text index.|  
|[sys.fulltext_index_fragments &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-index-fragments-transact-sql)|A fulltext index uses internal tables called full-text index fragments to store the inverted index data. This view can be used to query the metadata about these fragments. This view contains a row for each full-text index fragment in every table that contains a full-text index.|  
|[sys.fulltext_indexes &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-indexes-transact-sql)|Contains a row per full-text index of a tabular object.|  
|[sys.dm_fts_index_keywords &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-transact-sql)|Returns information about the content of a full-text index for the specified table.|  
|[sys.dm_fts_index_keywords_by_document &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-by-document-transact-sql)|Returns information about the document-level content of a full-text index for the specified table. A given keyword can appear in several documents.|  
|[sys.dm_fts_index_population &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-index-population-transact-sql)|Returns information about the full-text index populations currently in progress.|  
  
  
## See Also  
 [CREATE FULLTEXT CATALOG &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-fulltext-catalog-transact-sql)   
 [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-fulltext-index-transact-sql)   
 [CREATE FULLTEXT STOPLIST &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-fulltext-stoplist-transact-sql)   
 [CREATE TABLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-table-transact-sql)   
 [Populate Full-Text Indexes](populate-full-text-indexes.md)   
 [FULLTEXTCATALOGPROPERTY &#40;Transact-SQL&#41;](/sql/t-sql/functions/fulltextcatalogproperty-transact-sql)   
 [OBJECTPROPERTYEX &#40;Transact-SQL&#41;](/sql/t-sql/functions/objectproperty-transact-sql)  
  
  
