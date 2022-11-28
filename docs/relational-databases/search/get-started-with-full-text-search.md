---
description: "Get Started with Full-Text Search"
title: "Get Started with Full-Text Search | Microsoft Docs"
ms.date: "03/31/2020"
ms.service: sql
ms.subservice: search
ms.topic: conceptual
helpviewer_keywords:
  - "full-text catalogs [SQL Server], creating"
  - "full-text indexes [SQL Server], creating"
  - "full-text search [SQL Server], about"
  - "full-text search [SQL Server], setting up"
ms.assetid: 1fa628ba-0ee4-4d8f-b086-c4e52962ca4a
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
ms.custom:
  - intro-get-started
---
# Get Started with Full-Text Search
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
SQL Server databases are full-text enabled by default. Before you can run full-text queries, however, you must create a full text catalog and create a full-text index on the tables or indexed views you want to search.

## Set up full-text search in two steps
There are two basic steps to set up full-text search:  
1.  Create a full-text catalog.  
2.  Create a full-text index on tables or indexed view you want to search. 

Each full-text index must belong to a full-text catalog. You can create a separate text catalog for each full-text index, or you can associate multiple full-text indexes with a given catalog. A full-text catalog is a virtual object and does not belong to any filegroup. The catalog is a logical concept that refers to a group of full-text indexes.

> [!NOTE]
> These steps assume that you installed the optional Full-Text Search components when you installed SQL Server. If not, you have to run SQL Server Setup again to add them.  

## Set up full-text search with a wizard 
 
To set up full-text search by using a wizard, see [Use the Full-Text Indexing Wizard](../../relational-databases/search/use-the-full-text-indexing-wizard.md).

## Set up full-text search with Transact-SQL 
 The following two-part example creates a full-text catalog named `AdvWksDocFTCat` on the AdventureWorks sample database and then creates a full-text index on the `Document` table in the sample database. This statement creates the full-text catalog in the default directory specified during SQL Server setup. The folder named `AdvWksDocFTCat` is in the default directory.  
  
1.  To create a full-text catalog named `AdvWksDocFTCat`, the example uses a [CREATE FULLTEXT CATALOG](../../t-sql/statements/create-fulltext-catalog-transact-sql.md) statement:  
  
    ```sql
    USE AdventureWorks;  
    GO  
    CREATE FULLTEXT CATALOG AdvWksDocFTCat;  
    ```  
    For more info, see [Create and Manage Full-Text Catalogs](../../relational-databases/search/create-and-manage-full-text-catalogs.md).
 
2.  Before you can create a full-text index on the Document table, ensure that the table has a unique, single-column, non-nullable index. The following [CREATE INDEX](../../t-sql/statements/create-index-transact-sql.md) statement creates a unique index, `ui_ukDoc`, on the DocumentID column of the Document table:  
  
    ```sql 
    CREATE UNIQUE INDEX ui_ukDoc ON Production.Document(DocumentNode);  
    ```  

3.  Drop the existing full-text index on the `Document` table by using the following [DROP FULLTEXT INDEX](../../t-sql/statements/drop-fulltext-index-transact-sql.md) statement. 

    ```sql
    DROP FULLTEXT INDEX ON Production.Document
    GO
    ```

4.  After you have a unique key, you can create a full-text index on the `Document` table by using the following [CREATE FULLTEXT INDEX](../../t-sql/statements/create-fulltext-index-transact-sql.md) statement.  
  
    ```sql  
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
    
  
  
     The TYPE COLUMN defined in this example specifies the type column in the table that contains the type of the document in each row of the column 'Document' (which is of binary type). The type column stores the user-supplied file extension - ".doc", ".xls", and so forth - of the document in a given row. The Full-Text Engine uses the file extension in a given row to invoke the correct filter to use for parsing the data in that row. After the filter has parsed the binary data of the row, the specified word breaker parses the content. (In this example, the word breaker for British English is used.) For more information, see [Configure and Manage Filters for Search](../../relational-databases/search/configure-and-manage-filters-for-search.md).  

    For more info, see [Create and Manage Full-Text Indexes](../../relational-databases/search/create-and-manage-full-text-indexes.md).

##  <a name="options"></a> Choose options for a full-text index 
  
### Choose a language  
 For information about choosing the column language, see [Choose a Language When Creating a Full-Text Index](../../relational-databases/search/choose-a-language-when-creating-a-full-text-index.md).  
  
### Choose a filegroup  
 The process of building a full-text index is fairly I/O intensive. In summary, it consists of reading data from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and then propagating the filtered data to the full-text index. As a best practice, locate a full-text index in the database filegroup that is best for maximizing I/O performance or locate the full-text indexes in a different filegroup on another volume.
  
### Choose a full-text catalog   
 
 We recommend associating tables with the same update characteristics (such as small number of changes versus large number of changes, or tables that change frequently during a particular time of day) together under the same full-text catalog. By setting up full-text catalog population schedules, full-text indexes stay synchronous with the tables without adversely affecting the resource usage of the database server during periods of high database activity.  
  
 Consider the following guidelines:  
  
-   If you are indexing a table with millions of rows, assign the table to its own full-text catalog.  
  
-   Consider the amount of change occurring in the tables being full-text indexed, as well as the total number of rows. If the total number of rows being changed, together with the number of rows in the table present during the last full-text population, represents millions of rows, assign the table to its own full-text catalog.  

### Associate a unique index
Always select the smallest unique index available for your full-text unique key. (A 4-byte, integer-based index is optimal.) This significantly reduces the resources required by [!INCLUDE[msCoName](../../includes/msconame-md.md)] Search service in the file system. If the primary key is large (over 100 bytes), consider choosing another unique index in the table (or creating another unique index) as the full-text unique key. Otherwise, if the full-text unique key size exceeds the maximum size allowed (900 bytes), full-text population will not be able to proceed.  
 
### Associate a stoplist   
  A *stoplist* is a list of stopwords, also known as noise words. A stoplist is associated with each full-text index, and the words in that stoplist are applied to full-text queries on that index. By default, the system stoplist is associated with a new full-text index. You can create and use your own stoplist too.   
  
 For example, the following [CREATE FULLTEXT STOPLIST](../../t-sql/statements/create-fulltext-stoplist-transact-sql.md) [!INCLUDE[tsql](../../includes/tsql-md.md)] statement creates a new full-text stoplist named myStoplist by copying from the system stoplist:  
  
```sql  
CREATE FULLTEXT STOPLIST myStoplist FROM SYSTEM STOPLIST;  
GO  
```  
  
 The following [ALTER FULLTEXT STOPLIST](../../t-sql/statements/alter-fulltext-stoplist-transact-sql.md) [!INCLUDE[tsql](../../includes/tsql-md.md)] statement alters a stoplist named myStoplist, adding the word 'en', first for Spanish and then for French:  
  
```sql  
ALTER FULLTEXT STOPLIST myStoplist ADD 'en' LANGUAGE 'Spanish';  
ALTER FULLTEXT STOPLIST myStoplist ADD 'en' LANGUAGE 'French';  
GO  
```  
For more information, see [Configure and Manage Stopwords and Stoplists for Full-Text Search](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md).

## Update a full-text index  
 Like regular [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] indexes, full-text indexes can be automatically updated as data is modified in the associated tables. This is the default behavior. Alternatively, you can keep your full-text indexes up-to-date manually, or at specified scheduled intervals. Populating a full-text index can be time-consuming and resource-intensive. Therefore, index updating is usually performed as an asynchronous process that runs in the background and keeps the full-text index up to date after modifications in the base table. 
 
Updating a full-text index immediately after each change in the base table is also resource-intensive. Therefore, if you have a high update/insert/delete rate, you may experience some degradation in query performance. If this occurs, consider scheduling manual change tracking updates to keep up with the numerous changes from time to time, rather than competing with queries for resources.  
  
For more info, see [Populate Full-Text Indexes](../../relational-databases/search/populate-full-text-indexes.md). 

## Next steps
After you set up SQL Server Full-Text Search, you're ready to run full-text queries. For more info, see [Query with Full-Text Search](../../relational-databases/search/query-with-full-text-search.md).
