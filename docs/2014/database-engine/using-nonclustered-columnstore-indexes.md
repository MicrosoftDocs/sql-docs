---
title: "Using Nonclustered Columnstore Indexes | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
ms.assetid: 4c341fb8-7cb1-4cab-921b-e80b751d6c19
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Using Nonclustered Columnstore Indexes
  Describes key tasks for using a nonclustered columnstore index on a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] table.  
  
 For an overview of columnstore indexes, see [Columnstore Indexes Described](../relational-databases/indexes/columnstore-indexes-described.md).  
  
 For information about clustered columnstore indexes, see [Using Clustered Columnstore Indexes](../relational-databases/indexes/indexes.md).  
  
## Contents  
  
-   [Create a Nonclustered Columnstore Index](../../2014/database-engine/using-nonclustered-columnstore-indexes.md#load)  
  
-   [Change the Data in a Nonclustered Columnstore Index](../../2014/database-engine/using-nonclustered-columnstore-indexes.md#change)  
  
##  <a name="load"></a> Create a Nonclustered Columnstore Index  
 To load data into a nonclustered columnstore index, first load data into a traditional rowstore table stored as a heap or clustered index, and then use [CREATE COLUMNSTORE INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-columnstore-index-transact-sql) to create a columnstore index.  
  
 ![Loading data into a columnstore index](../../2014/database-engine/media/sql-server-pdw-columnstore-loadprocess-nonclustered.gif "Loading data into a columnstore index")  
  
##  <a name="change"></a> Change the Data in a Nonclustered Columnstore Index  
 Once you create a nonclustered columnstore index on a table, you cannot directly modify the data in that table. A query with INSERT, UPDATE, DELETE, or MERGE will fail and return an error message. To add or modify the data in the table, you can do one of the following:  
  
-   Disable the columnstore index. You can then update the data in the table. If you disable the columnstore index, you can rebuild the columnstore index when you finish updating the data. For example:  
  
    ```  
    ALTER INDEX mycolumnstoreindex ON mytable DISABLE;  
    -- update mytable --  
    ALTER INDEX mycolumnstoreindex on mytable REBUILD  
    ```  
  
-   Drop the columnstore index, update the table, and then re-create the columnstore index with CREATE COLUMNSTORE INDEX. For example:  
  
    ```  
    DROP INDEX mycolumnstoreindex ON mytable  
    -- update mytable --  
    CREATE NONCLUSTERED COLUMNSTORE INDEX mycolumnstoreindex ON mytable;  
  
    ```  
  
-   Load data into a staging table that does not have a columnstore index. Build a columnstore index on the staging table. Switch the staging table into an empty partition of the main table.  
  
-   Switch a partition from the table with the columnstore index into an empty staging table. If there is a columnstore index on the staging table, disable the columnstore index. Perform any updates. Build (or rebuild) the columnstore index. Switch the staging table back into the (now empty) partition of the main table.  
  

  
  
