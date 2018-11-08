---
title: "Disable Indexes and Constraints | Microsoft Docs"
ms.custom: ""
ms.date: "02/17/2017"
ms.prod: sql
ms.prod_service: "table-view-index, sql-database"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.disableindexes.f1"
helpviewer_keywords: 
  - "disabled indexes [SQL Server], index operations"
  - "nonclustered indexes [SQL Server], disabling"
  - "disabled indexes [SQL Server], guidelines"
  - "clustered indexes, disabling"
  - "constraints [SQL Server], disabling"
  - "disabled indexes [SQL Server], viewing"
  - "FOREIGN KEY constraints, disabling"
  - "statistical information [SQL Server], indexes"
  - "index disabling [SQL Server]"
  - "indexed views [SQL Server], disabled indexes"
ms.assetid: 2198f1af-fa44-47e9-92df-f4fde322ba18
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Disable Indexes and Constraints
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

  This topic describes how to disable an index or constraints in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. Disabling an index prevents user access to the index, and for clustered indexes to the underlying table data. The index definition remains in metadata, and index statistics are kept on nonclustered indexes. Disabling a nonclustered or clustered index on a view physically deletes the index data. Disabling a clustered index on a table prevents access to the data; the data still remains in the table, but is unavailable for data manipulation language (DML) operations until the index is dropped or rebuilt.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To disable an index, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The index is not maintained while it is disabled.  
  
-   The query optimizer does not consider the disabled index when creating query execution plans. Also, queries that reference the disabled index with a table hint fail.  
  
-   You cannot create an index that uses the same name as an existing disabled index.  
  
-   A disabled index can be dropped.  
  
-   When disabling a unique index, the PRIMARY KEY or UNIQUE constraint and all FOREIGN KEY constraints that reference the indexed columns from other tables are also disabled. When disabling a clustered index, all incoming and outgoing FOREIGN KEY constraints on the underlying table are also disabled. The constraint names are listed in a warning message when the index is disabled. After rebuilding the index, all constraints must be manually enabled by using the ALTER TABLE CHECK CONSTRAINT statement.  
  
-   Nonclustered indexes are automatically disabled when the associated clustered index is disabled. They cannot be enabled until either the clustered index on the table or view is enabled or the clustered index on the table is dropped. Nonclustered indexes must be explicitly enabled, unless the clustered index was enabled by using the ALTER INDEX ALL REBUILD statement.  
  
-   The ALTER INDEX ALL REBUILD statement rebuilds and enables all disabled indexes on the table, except for disabled indexes on views. Indexes on views must be enabled in a separate ALTER INDEX ALL REBUILD statement.  
  
-   Disabling a clustered index on a table also disables all clustered and nonclustered indexes on views that reference that table. These indexes must be rebuilt just as those on the referenced table.  
  
-   The data rows of the disabled clustered index cannot be accessed except to drop or rebuild the clustered index.  
  
-   You can rebuild a disabled nonclustered index online when the table does not have a disabled clustered index. However, you must always rebuild a disabled clustered index offline if you use either the ALTER INDEX REBUILD or CREATE INDEX WITH DROP_EXISTING statement. For more information about online index operations, see [Perform Index Operations Online](../../relational-databases/indexes/perform-index-operations-online.md).  
  
-   The CREATE STATISTICS statement cannot be successfully executed on a table that has a disabled clustered index.  
  
-   The AUTO_CREATE_STATISTICS database option creates new statistics on a column when the index is disabled and the following conditions exist:  
  
    -   AUTO_CREATE_STATISTICS is set to ON  
  
    -   There are no existing statistics for the column.  
  
    -   Statistics are required during query optimization.  
  
-   If a clustered index is disabled, [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) cannot return information about the underlying table; instead, the statement reports that the clustered index is disabled. [DBCC INDEXDEFRAG](../../t-sql/database-console-commands/dbcc-indexdefrag-transact-sql.md) cannot be used to defragment a disabled index; the statement fails with an error message. You can use [DBCC DBREINDEX](../../t-sql/database-console-commands/dbcc-dbreindex-transact-sql.md) to rebuild a disabled index.  
  
-   Creating a new clustered index enables previously disabled nonclustered indexes. For more information, see [Enable Indexes and Constraints](../../relational-databases/indexes/enable-indexes-and-constraints.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 To execute ALTER INDEX, at a minimum, ALTER permission on the table or view is required.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To disable an index  
  
1.  In Object Explorer, click the plus sign to expand the database that contains the table on which you want to disable an index.  
  
2.  Click the plus sign to expand the **Tables** folder.  
  
3.  Click the plus sign to expand the table on which you want to disable an index.  
  
4.  Click the plus sign to expand the **Indexes** folder.  
  
5.  Right-click the index you want to disable and select **Disable**.  
  
6.  In the **Disable Indexes** dialog box, verify that the correct index is in the **Indexes to disable** grid and click **OK**.  
  
#### To disable all indexes on a table  
  
1.  In Object Explorer, click the plus sign to expand the database that contains the table on which you want to disable the indexes.  
  
2.  Click the plus sign to expand the **Tables** folder.  
  
3.  Click the plus sign to expand the table on which you want to disable the indexes.  
  
4.  Right-click the **Indexes** folder and select **Disable All**.  
  
5.  In the **Disable Indexes** dialog box, verify that the correct indexes are in the **Indexes to disable** grid and click **OK**. To remove an index from the **Indexes to disable** grid, select the index and then press the Delete key.  
  
 The following information is available in the **Disable Indexes** dialog box:  
  
 **Index Name**  
 Displays the name of the index. During execution, this column also displays an icon representing the status.  
  
 **Table Name**  
 Displays the name of the table or view that the index was created on.  
  
 **Index Type**  
 Displays the type of the index: **Clustered**, **Nonclustered**, **Spatial**, or **XML**.  
  
 **Status**  
 Displays the status of the disable operation. Possible values after execution are:  
  
-   Blank  
  
     Prior to execution **Status** is blank.  
  
-   **In progress**  
  
     Disabling of the indexes has been started but is not complete.  
  
-   **Success**  
  
     The disable operation completed successfully.  
  
-   **Error**  
  
     An error was encountered during the index disable operation, and the operation did not complete successfully.  
  
-   **Stopped**  
  
     The disable of the index was not completed successfully because the user stopped the operation.  
  
 **Message**  
 Provides the text of error messages during the disable operation. During execution, errors appear as hyperlinks. The text of the hyperlinks describes the body of the error. The **Message** column is rarely wide enough to read the full message text. There are two ways to get the full text:  
  
-   Move the mouse pointer over the message cell to display a ToolTip with the error text.  
  
-   Click the hyperlink to display a dialog box displaying the full error.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To disable an index  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    -- disables the IX_Employee_OrganizationLevel_OrganizationNode index  
    -- on the HumanResources.Employee table  
    ALTER INDEX IX_Employee_OrganizationLevel_OrganizationNode ON HumanResources.Employee  
    DISABLE;  
    ```  
  
#### To disable all indexes on a table  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    -- Disables all indexes on the HumanResources.Employee table.  
    ALTER INDEX ALL ON HumanResources.Employee  
    DISABLE;  
    ```  
  
 For more information, see [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md).  
  
  
