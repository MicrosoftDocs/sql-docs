---
title: "ALTER FULLTEXT INDEX (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER FULLTEXT INDEX"
  - "ALTER_FULLTEXT_INDEX_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "full-text search [SQL Server], search property lists"
  - "modifying full-text indexes"
  - "full-text indexes [SQL Server], modifying"
  - "search property lists [SQL Server], associating with full-text indexes"
  - "ALTER FULLTEXT INDEX statement"
ms.assetid: b6fbe9e6-3033-4d1b-b6bf-1437baeefec3
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# ALTER FULLTEXT INDEX (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Changes the properties of a full-text index in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
ALTER FULLTEXT INDEX ON table_name  
   { ENABLE   
   | DISABLE  
   | SET CHANGE_TRACKING [ = ] { MANUAL | AUTO | OFF }  
   | ADD ( column_name   
           [ TYPE COLUMN type_column_name ]   
           [ LANGUAGE language_term ]  
           [ STATISTICAL_SEMANTICS ]  
           [,...n]   
         )  
     [ WITH NO POPULATION ]  
   | ALTER COLUMN column_name  
     { ADD | DROP } STATISTICAL_SEMANTICS  
     [ WITH NO POPULATION ]  
   | DROP ( column_name [,...n] )  
     [ WITH NO POPULATION ]   
   | START { FULL | INCREMENTAL | UPDATE } POPULATION  
   | {STOP | PAUSE | RESUME } POPULATION   
   | SET STOPLIST [ = ] { OFF| SYSTEM | stoplist_name }  
     [ WITH NO POPULATION ]   
   | SET SEARCH PROPERTY LIST [ = ] { OFF | property_list_name }  
     [ WITH NO POPULATION ]   
   }  
[;]  
```  
  
## Arguments  
 *table_name*  
 Is the name of the table or indexed view that contains the column or columns included in the full-text index. Specifying database and table owner names is optional.  
  
 ENABLE | DISABLE  
 Tells [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] whether to gather full-text index data for *table_name*. ENABLE activates the full-text index; DISABLE turns off the full-text index. The table will not support full-text queries while the index is disabled.  
  
 Disabling a full-text index allows you to turn off change tracking but keep the full-text index, which you can reactivate at any time using ENABLE. When the full-text index is disabled, the full-text index metadata remains in the system tables. If CHANGE_TRACKING is in the enabled state (automatic or manual update) when the full-text index is disabled, the state of the index freezes, any ongoing crawl stops, and new changes to the table data are not tracked or propagated to the index.  
  
 SET CHANGE_TRACKING {MANUAL | AUTO | OFF}  
 Specifies whether changes (updates, deletes, or inserts) made to table columns that are covered by the full-text index will be propagated by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the full-text index. Data changes through WRITETEXT and UPDATETEXT are not reflected in the full-text index, and are not picked up with change tracking.  
  
> [!NOTE]  
>  For information about the interaction of change tracking and WITH NO POPULATION, see "Remarks," later in this topic.  
  
 MANUAL  
 Specifies that the tracked changes will be propagated manually by calling the ALTER FULLTEXT INDEX ... START UPDATE POPULATION [!INCLUDE[tsql](../../includes/tsql-md.md)] statement (*manual population*). You can use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to call this [!INCLUDE[tsql](../../includes/tsql-md.md)] statement periodically.  
  
 AUTO  
 Specifies that the tracked changes will be propagated automatically as data is modified in the base table (*automatic population*). Although changes are propagated automatically, these changes might not be reflected immediately in the full-text index. AUTO is the default.  
  
 OFF  
 Specifies that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will not keep a list of changes to the indexed data.  
  
 ADD | DROP *column_name*  
 Specifies the columns to be added or deleted from a full-text index. The column or columns must be of type **char**, **varchar**, **nchar**, **nvarchar**, **text**, **ntext**, **image**, **xml**, **varbinary**, or **varbinary(max)**.  
  
 Use the DROP clause only on columns that have been enabled previously for full-text indexing.  
  
 Use TYPE COLUMN and LANGUAGE with the ADD clause to set these properties on the *column_name*. When a column is added, the full-text index on the table must be repopulated in order for full-text queries against this column to work.  
  
> [!NOTE]  
>  Whether the full-text index is populated after a column is added or dropped from a full-text index depends on whether change-tracking is enabled and whether WITH NO POPULATION is specified. For more information, see "Remarks," later in this topic.  
  
 TYPE COLUMN *type_column_name*  
 Specifies the name of a table column, *type_column_name*, that is used to hold the document type for a **varbinary**, **varbinary(max)**, or **image** document. This column, known as the type column, contains a user-supplied file extension (.doc, .pdf, .xls, and so forth). The type column must be of type **char**, **nchar**, **varchar**, or **nvarchar**.  
  
 Specify TYPE COLUMN *type_column_name* only if *column_name* specifies a **varbinary**, **varbinary(max)** or **image** column, in which data is stored as binary data; otherwise, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error.  
  
> [!NOTE]  
>  At indexing time, the Full-Text Engine uses the abbreviation in the type column of each table row to identify which full-text search filter to use for the document in *column_name*. The filter loads the document as a binary stream, removes the formatting information, and sends the text from the document to the word-breaker component. For more information, see [Configure and Manage Filters for Search](../../relational-databases/search/configure-and-manage-filters-for-search.md).  
  
 LANGUAGE *language_term*  
 Is the language of the data stored in **column_name**.  
  
 *language_term* is optional and can be specified as a string, integer, or hexadecimal value corresponding to the locale identifier (LCID) of a language. If *language_term* is specified, the language it represents will be applied to all elements of the search condition. If no value is specified, the default full-text language of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance is used.  
  
 Use the **sp_configure** stored procedure to access information about the default full-text language of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
 When specified as a string, *language_term* corresponds to the **alias** column value in the **syslanguages** system table. The string must be enclosed in single quotation marks, as in '*language_term*'. When specified as an integer, *language_term* is the actual LCID that identifies the language. When specified as a hexadecimal value, *language_term* is 0x followed by the hex value of the LCID. The hex value must not exceed eight digits, including leading zeros.  
  
 If the value is in double-byte character set (DBCS) format, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will convert it to Unicode.  
  
 Resources, such as word breakers and stemmers, must be enabled for the language specified as *language_term*. If such resources do not support the specified language, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error.  
  
 For non-BLOB and non-XML columns containing text data in multiple languages, or for cases when the language of the text stored in the column is unknown, use the neutral (0x0) language resource. For documents stored in XML- or BLOB-type columns, the language encoding within the document will be used at indexing time. For example, in XML columns, the xml:lang attribute in XML documents will identify the language. At query time, the value previously specified in *language_term* becomes the default language used for full-text queries unless *language_term* is specified as part of a full-text query.  
  
 STATISTICAL_SEMANTICS  
 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Creates the additional key phrase and document similarity indexes that are part of statistical semantic indexing. For more information, see [Semantic Search &#40;SQL Server&#41;](../../relational-databases/search/semantic-search-sql-server.md).  
  
 [ **,**_...n_]  
 Indicates that multiple columns may be specified for the ADD, ALTER, or DROP clauses. When multiple columns are specified, separate these columns with commas.  
  
 WITH NO POPULATION  
 Specifies that the full-text index will not be populated after an ADD or DROP column operation or a SET STOPLIST operation. The index will only be populated if the user executes a START...POPULATION command.  
  
 When NO POPULATION is specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not populate an index. The index is populated only after the user gives an ALTER FULLTEXT INDEX...START POPULATION command. When NO POPULATION is not specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] populates the index.  
  
 If CHANGE_TRACKING is enabled and WITH NO POPULATION is specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error. If CHANGE_TRACKING is enabled and WITH NO POPULATION is not specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performs a full population on the index.  
  
> [!NOTE]  
>  For more information about the interaction of change tracking and WITH NO POPULATION, see "Remarks," later in this topic.  
  
 {ADD | DROP } STATISTICAL_SEMANTICS  
 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Enables or disables statistical semantic indexing for the specified columns. For more information, see [Semantic Search &#40;SQL Server&#41;](../../relational-databases/search/semantic-search-sql-server.md).  
  
 START {FULL|INCREMENTAL|UPDATE} POPULATION  
 Tells [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to begin population of the full-text index of *table_name*. If a full-text index population is already in progress, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns a warning and does not start a new population.  
  
 FULL  
 Specifies that every row of the table be retrieved for full-text indexing even if the rows have already been indexed.  
  
 INCREMENTAL  
 Specifies that only the modified rows since the last population be retrieved for full-text indexing. INCREMENTAL can be applied only if the table has a column of the type **timestamp**. If a table in the full-text catalog does not contain a column of the type **timestamp**, the table undergoes a FULL population.  
  
 UPDATE  
 Specifies the processing of all insertions, updates, or deletions since the last time the change-tracking index was updated. Change-tracking population must be enabled on a table, but the background update index or the auto change tracking should not be turned on.  
  
 {STOP | PAUSE | RESUME } POPULATION  
 Stops, or pauses any population in progress; or stops or resumes any paused population.  
  
 STOP POPULATION does not stop auto change tracking or background update index. To stop change tracking, use SET CHANGE_TRACKING OFF.  
  
 PAUSE POPULATION and RESUME POPULATION can only be used for full populations. They are not relevant to other population types because the other populations resume crawls from where the crawl stopped.  
  
 SET STOPLIST { OFF| SYSTEM | *stoplist_name* }  
 Changes the full-text stoplist that is associated with the index, if any.  
  
 OFF  
 Specifies that no stoplist be associated with the full-text index.  
  
 SYSTEM  
 Specifies that the default full-text system STOPLIST should be used for this full-text index.  
  
 *stoplist_name*  
 Specifies the name of the stoplist to be associated with the full-text index.  
  
 For more information, see [Configure and Manage Stopwords and Stoplists for Full-Text Search](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md).  
  
 SET SEARCH PROPERTY LIST { OFF | *property_list_name* } [ WITH NO POPULATION ]  
 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Changes the search property list that is associated with the index, if any.  
  
 OFF  
 Specifies that no property list be associated with the full-text index. When you turn off the search property list of a full-text index (ALTER FULLTEXT INDEX ... SET SEARCH PROPERTY LIST OFF), property searching on the base table is no longer possible.  
  
 By default, when you turn off an existing search property list, the full-text index automatically repopulates. If you specify WITH NO POPULATION when you turn off the search property list, automatic repopulation does not occur. However, we recommend that you eventually run a full population on this full-text index at your convenience. Repopulating the full-text index removes the property-specific metadata of each dropped search property, making the full-text index smaller and more efficient.  
  
 *property_list_name*  
 Specifies the name of the search property list to be associated with the full-text index.  
  
 Adding a search property list to a full-text index requires repopulating the index to index the search properties that are registered for the associated search property list. If you specify WITH NO POPULATION when adding the search property list, you will need to run a population on the index, at an appropriate time.  
  
> [!IMPORTANT]  
>  If the full-text index was previously associated with a different search it must be rebuilt property list in order to bring the index into a consistent state. The index is truncated immediately and is empty until the full population runs. For more information about when changing the search property list causes rebuilding, see "Remarks," later in this topic.  
  
> [!NOTE]  
>  You can associate a given search property list with more than one full-text index in the same database.  
  
 **To find the search property lists on the current database**  
  
-   [sys.registered_search_property_lists](../../relational-databases/system-catalog-views/sys-registered-search-property-lists-transact-sql.md)  
  
 For more information about search property lists, see [Search Document Properties with Search Property Lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md).  
  
## Interactions of Change Tracking and NO POPULATION Parameter  
 Whether the full-text index is populated depends on whether change-tracking is enabled and whether WITH NO POPULATION is specified in the ALTER FULLTEXT INDEX statement. The following table summarizes the result of their interaction.  
  
|Change Tracking|WITH NO POPULATION|Result|  
|---------------------|------------------------|------------|  
|Not Enabled|Not specified|A full population is performed on the index.|  
|Not Enabled|Specified|No population of the index occurs until an ALTER FULLTEXT INDEX...START POPULATION statement is issued.|  
|Enabled|Specified|An error is raised, and the index is not altered.|  
|Enabled|Not specified|A full population is performed on the index.|  
  
 For more information about populating full-text indexes, see [Populate Full-Text Indexes](../../relational-databases/search/populate-full-text-indexes.md).  
  
## Changing the Search Property List Causes Rebuilding the Index  
 The first time that a full-text index is associated with a search property list, the index must be repopulated to index property-specific search terms. The existing index data is not truncated.  
  
 However, if you associate the full-text index with a different property list, the index is rebuilt. Rebuilding immediately truncates the full-text index, removing all existing data, and the index must be repopulated. While the population progresses, full-text queries on the base table search only on the table rows that have already been indexed by the population. The repopulated index data will include metadata from the registered properties of the newly added search property list.  
  
 Scenarios that cause rebuilding include:  
  
-   Switching directly to a different search property list (see "Scenario A," later in this section).  
  
-   Turning off the search property list and later associating the index with any search property list (see "Scenario B," later in this section)  
  
> [!NOTE]  
>  For more information about how full-text search works with search property lists, see [Search Document Properties with Search Property Lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md). For information about full populations, see [Populate Full-Text Indexes](../../relational-databases/search/populate-full-text-indexes.md).  
  
### Scenario A: Switching Directly to a Different Search Property List  
  
1.  A full-text index is created on `table_1` with a search property list `spl_1`:  
  
    ```  
    CREATE FULLTEXT INDEX ON table_1 (column_name) KEY INDEX unique_key_index   
       WITH SEARCH PROPERTY LIST=spl_1,   
       CHANGE_TRACKING OFF, NO POPULATION;   
    ```  
  
2.  A full population is run on the full-text index:  
  
    ```  
    ALTER FULLTEXT INDEX ON table_1 START FULL POPULATION;  
    ```  
  
3.  The full-text index is later associated a different search property list, `spl_2`, using the following statement:  
  
    ```  
    ALTER FULLTEXT INDEX ON table_1 SET SEARCH PROPERTY LIST spl_2;  
    ```  
  
     This statement causes a full population, the default behavior.  However, before beginning this population, the Full-Text Engine automatically truncates the index.  
  
### Scenario B: Turning Off the Search Property List and Later Associating the Index with Any Search Property List  
  
1.  A full-text index is created on `table_1` with a search property list `spl_1`, followed by an automatic full population (the default behavior):  
  
    ```  
    CREATE FULLTEXT INDEX ON table_1 (column_name) KEY INDEX unique_key_index   
       WITH SEARCH PROPERTY LIST=spl_1;   
    ```  
  
2.  The search property list is turned off, as follows:  
  
    ```  
    ALTER FULLTEXT INDEX ON table_1   
       SET SEARCH PROPERTY LIST OFF WITH NO POPULATION;   
    ```  
  
3.  The full-text index is once more associated either the same search property list or a different one.  
  
     For example the following statement re-associates the full-text index with the original search property list, `spl_1`:  
  
    ```  
    ALTER FULLTEXT INDEX ON table_1 SET SEARCH PROPERTY LIST spl_1;  
    ```  
  
     This statement starts a full population, the default behavior.  
  
    > [!NOTE]  
    >  The rebuild would also be required for a different search property list, such as `spl_2`.  
  
## Permissions  
 The user must have ALTER permission on the table or indexed view, or be a member of the **sysadmin** fixed server role, or the **db_ddladmin** or **db_owner** fixed database roles.  
  
 If SET STOPLIST is specified, the user must have REFERENCES permission on the stoplist. If SET SEARCH PROPERTY LIST is specified, the user must have REFERENCES permission on the search property list. The owner of the specified stoplist or search property list can grant REFERENCES permission, if the owner has  ALTER FULLTEXT CATALOG permissions.  
  
> [!NOTE]  
>  The public is granted REFERENCES permission to the default stoplist that is shipped with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Examples  
  
### A. Setting manual change tracking  
 The following example sets manual change tracking on the full-text index on the `JobCandidate` table.  
  
```  
USE AdventureWorks2012;  
GO  
ALTER FULLTEXT INDEX ON HumanResources.JobCandidate  
   SET CHANGE_TRACKING MANUAL;  
GO  
```  
  
### B. Associating a property list with a full-text index  
  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 The following example associates the `DocumentPropertyList` property list with the full-text index on the `Production.Document` table. This ALTER FULLTEXT INDEX statement starts a full population, which is the default behavior of the SET SEARCH PROPERTY LIST clause.  
  
> [!NOTE]  
>  For an example that creates the `DocumentPropertyList` property list, see [CREATE SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](../../t-sql/statements/create-search-property-list-transact-sql.md).  
  
```  
USE AdventureWorks2012;  
GO  
ALTER FULLTEXT INDEX ON Production.Document   
   SET SEARCH PROPERTY LIST DocumentPropertyList;   
GO  
```  
  
### C. Removing a search property list  
  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 The following example removes the `DocumentPropertyList` property list from the full-text index on the `Production.Document`. In this example, there is no hurry for removing the properties from the index, so the WITH NO POPULATION option is specified. However, property-level searching is longer allowed against this full-text index.  
  
```  
USE AdventureWorks2012;  
GO  
ALTER FULLTEXT INDEX ON Production.Document   
   SET SEARCH PROPERTY LIST OFF WITH NO POPULATION;   
GO  
```  
  
### D. Starting a full population  
 The following example starts a full population on the full-text index on the `JobCandidate` table.  
  
```  
USE AdventureWorks2012;  
GO  
ALTER FULLTEXT INDEX ON HumanResources.JobCandidate   
   START FULL POPULATION;  
GO  
```  
  
## See Also  
 [sys.fulltext_indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-indexes-transact-sql.md)   
 [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-index-transact-sql.md)   
 [DROP FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/drop-fulltext-index-transact-sql.md)   
 [Full-Text Search](../../relational-databases/search/full-text-search.md)   
 [Populate Full-Text Indexes](../../relational-databases/search/populate-full-text-indexes.md)  
  
  
