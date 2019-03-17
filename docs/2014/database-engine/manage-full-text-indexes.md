---
title: "Manage Full-Text Indexes | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
ms.assetid: 28ff17dc-172b-4ac4-853f-990b5dc02fd1
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Manage Full-Text Indexes
     
##  <a name="view"></a> Viewing and Changing the Properties of a Full-Text Index  
  
#### To view or change the properties of a full-text index in Management Studio  
  
1.  In Object Explorer, expand the server.  
  
2.  Expand **Databases**, and then expand the database that contains the full-text index.  
  
3.  Expand **Tables**.  
  
4.  Right-click the table on which the full-text index is defined, select **Full-Text index**, and on the **Full-Text index** context menu, click **Properties**. This opens the **Full-text index Properties** dialog box.  
  
5.  In the **Select a page** pane, you can select any of the following pages:  
  
    |Page|Description|  
    |----------|-----------------|  
    |**General**|Displays basic properties of the full-text index. These include several modifiable properties and a number of unchangeable properties such as database name, table name, and the name of full-text key column. The modifiable properties are:<br /><br /> **Full-Text Index Stoplist**<br /><br /> **Full-Text Indexing Enabled**<br /><br /> **Change Tracking**<br /><br /> **Search Property List**<br /><br /> <br /><br /> For more information, see [Full-Text Index Properties &#40;General Page&#41;](full-text-index-properties-general-page.md).|  
    |**Columns**|Displays the table columns that are available for full-text indexing. The selected column or columns are full-text indexed. You can select as many of the available columns as you want to include in the full-text index. For more information, see [Full-Text Index Properties &#40;Columns Page&#41;](../../2014/database-engine/full-text-index-properties-columns-page.md).|  
    |**Schedules**|Use this page to create or manage schedules for a SQL Server Agent job that starts an incremental table population for the full-text index populations. For more information, see [Populate Full-Text Indexes](../relational-databases/indexes/indexes.md).<br /><br /> <strong>\*\* Important \*\*</strong> After you exit the **Full-Text Index Properties** dialog box, any newly created schedule is associated with a SQL Server Agent job (Start Incremental Table Population on *database_name*.*table_name*).|  
  
6.  [!INCLUDE[clickOK](../includes/clickok-md.md)] to save any changes and exit the **Full-text index Properties** dialog box.  
  
##  <a name="props"></a> Viewing the Properties of Indexed Tables and Columns  
 Several [!INCLUDE[tsql](../includes/tsql-md.md)] functions such as OBJECTPROPERTYEX can be used to obtain the value of various full-text indexing properties. This information is useful for administering and troubleshooting full-text search.  
  
 The following table lists the full-text properties related to indexed tables and columns and their related [!INCLUDE[tsql](../includes/tsql-md.md)] functions.  
  
|Property|Description|Function|  
|--------------|-----------------|--------------|  
|`FullTextTypeColumn`|TYPE COLUMN in the table that holds the document type information of the column.|[COLUMNPROPERTY](/sql/t-sql/functions/columnproperty-transact-sql)|  
|`IsFulltextIndexed`|Whether a column has been enabled for full-text indexing.|COLUMNPROPERTY|  
|`IsFulltextKey`|Whether the index is the full-text key for a table.|[INDEXPROPERTY](/sql/t-sql/functions/indexproperty-transact-sql)|  
|**TableFulltextBackgroundUpdateIndexOn**|Whether a table has full-text background update indexing.|[OBJECTPROPERTYEX](/sql/t-sql/functions/objectproperty-transact-sql)|  
|`TableFulltextCatalogId`|Full-text catalog ID in which the full-text index data for the table resides.|OBJECTPROPERTYEX|  
|`TableFulltextChangeTrackingOn`|Whether a table has full-text change-tracking enabled.|OBJECTPROPERTYEX|  
|`TableFulltextDocsProcessed`|Number of rows processed since the start of full-text indexing.|OBJECTPROPERTYEX|  
|**TableFulltextFailCount**|Number of rows Full-Text Search did not index.|OBJECTPROPERTYEX|  
|**TableFulltextItemCount**|Number of rows that were successfully full-text indexed.|OBJECTPROPERTYEX|  
|`TableFulltextKeyColumn`|The column ID of the full-text unique key column.|OBJECTPROPERTYEX|  
|`TableFullTextMergeStatus`|Whether a table that has a full-text index is currently in merging.|OBJECTPROPERTYEX|  
|**TableFulltextPendingChanges**|Number of pending change tracking entries to process.|OBJECTPROPERTYEX|  
|`TableFulltextPopulateStatus`|Population status of a full-text table.|OBJECTPROPERTYEX|  
|`TableHasActiveFulltextIndex`|Whether a table has an active full-text index.|OBJECTPROPERTYEX|  
  
##  <a name="key"></a> Getting Information about the Full-Text Key Column  
 Typically, the result of CONTAINSTABLE or FREETEXTTABLE rowset-valued functions need to be joined with the base table. In such cases, you need to know the unique key column name. You can inquire whether a given unique index is used as the full-text key, and you can obtain the identifier of the full-text key column.  
  
#### To inquire whether a given unique index is used as the full-text key column  
  
1.  Use a [SELECT](/sql/t-sql/queries/select-transact-sql) statement to call the [INDEXPROPERTY](/sql/t-sql/functions/indexproperty-transact-sql) function. In the function call use the OBJECT_ID function to convert the name of the table (*table_name*) into the table ID, specify the name of a unique index for the table, and specify the `IsFulltextKey` index property, as follows:  
  
    ```  
    SELECT INDEXPROPERTY( OBJECT_ID('table_name'), 'index_name',  'IsFulltextKey' );  
    ```  
  
     This statement returns 1 if the index is used to enforce uniqueness of the full-text key column and 0 if it is not.  
  
 **Example**  
  
 The following example inquires whether the `PK_Document_DocumentID` index is used to enforce the uniqueness of the full-text key column, as follows:  
  
```  
USE AdventureWorks  
GO  
SELECT INDEXPROPERTY ( OBJECT_ID('Production.Document'), 'PK_Document_DocumentID',  'IsFulltextKey' )  
```  
  
 This example returns 1 if the `PK_Document_DocumentID` index is used to enforce uniqueness of the full-text key column. Otherwise, it returns 0 or NULL. NULL implies you are using an invalid index name, the index name does not correspond to the table, the table does not exist, or so forth.  
  
#### To find the identifier of the full-text key column  
  
1.  Each full-text enabled table has a column that is used to enforce unique rows for the table (the *unique**key column*). The `TableFulltextKeyColumn` property, obtained from the OBJECTPROPERTYEX function, contains the column ID of the unique key column.  
  
     To obtain this identifier, you can use a SELECT statement to call the OBJECTPROPERTYEX function. Use the OBJECT_ID function to convert the name of the table (*table_name*) into the table ID and specify the `TableFulltextKeyColumn` property, as follows:  
  
    ```  
    SELECT OBJECTPROPERTYEX(OBJECT_ID( 'table_name'), 'TableFulltextKeyColumn' ) AS 'Column Identifier';  
    ```  
  
 **Examples**  
  
 The following example returns the identifier of the full-text key column or NULL. NULL implies that you are using an invalid index name, the index name does not correspond to the table, the table does not exist, or so forth.  
  
```  
USE AdventureWorks;  
GO  
SELECT OBJECTPROPERTYEX(OBJECT_ID('Production.Document'), 'TableFulltextKeyColumn');  
GO  
```  
  
 The following example shows how to use the identifier of the unique key column to obtain the name of the column.  
  
```  
USE AdventureWorks;  
GO  
DECLARE @key_column sysname  
SET @key_column = Col_Name(Object_Id('Production.Document'),  
ObjectProperty(Object_id('Production.Document'),  
'TableFulltextKeyColumn')   
)  
SELECT @key_column AS 'Unique Key Column';  
GO  
```  
  
 This example returns a result set column named `Unique Key Column`, which displays a single row containing the name of the unique key column of the Document table, DocumentID. Note that if this query contained an invalid index name, the index name did not correspond to the table, the table did not exist, and so forth, it would return NULL.  
  
##  <a name="disable"></a> Disabling or Re-enabling a Table for Full-Text Indexing  
 In [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], all user-created databases are full-text enabled by default. Additionally, an individual table is automatically enabled for full-text indexing as soon as a full-text index is created on it and a column is added to the index. A table is automatically disabled for full-text indexing when the last column is dropped from its full-text index.  
  
 On a table that has a full-text index, you can manually disable or re-enable a table for full-text indexing using [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
#### To enable a table for full-text indexing  
  
1.  Expand the server group, expand **Databases**, and expand the database that contains the table you want to enable for full-text indexing.  
  
2.  Expand **Tables**, and right-click the table that you want to disable or re-enable for full-text indexing.  
  
3.  Select **Full-Text index**, and then click **Disable Full-Text index** or **Enable Full-Text index**.  
  
##  <a name="remove"></a> Removing a Full-Text Index from a Table  
  
#### To remove a full-text index from a table  
  
1.  In Object Explorer, right-click the table that has the full-text index that you want to delete.  
  
2.  Select **Delete Full-Text index**.  
  
3.  When prompted, click **OK** to confirm that you want to delete the full-text index.  
  
  
