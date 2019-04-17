---
title: "View Edge Constraint Properties | Microsoft Docs"
ms.custom: ""
ms.date: "09/18/2018"
ms.prod: sql
ms.prod_service: "table-view-index, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "edge constraints [SQL Server], attributes"
  - "displaying edge constraint attributes"
  - "viewing edge constraint attributes"
ms.assetid: b0e57cb7-9b26-4b96-b76a-1f59f5f498c5
author: shkale-msft
ms.author: shkale
manager: craigg
monikerRange: ">=sql-server-2017||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# View Edge Constraint Properties
[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx.md](../../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

  You can view the edge constraint properties [!INCLUDE[noversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To view the foreign key attributes of a specific table, using:**  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To view the foreign key attributes of a relationship in a specific table  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. The example returns all edge constraints and their properties for the edge table `bought` in the tempdb database.  
    
    ```sql
    USE TEMPDB
    GO
    -- CREATE node and edge tables
    CREATE TABLE Customer
      (
        ID INTEGER PRIMARY KEY, 
        CustomerName VARCHAR(100)
      )
    AS NODE
    GO

    CREATE TABLE Supplier
      (
        ID INTEGER PRIMARY KEY, 
        SupplierName VARCHAR(100)
      )
    AS NODE
    GO

    CREATE TABLE Product 
      (
        ID INTEGER PRIMARY KEY, 
        ProductName VARCHAR(100)
      ) 
    AS NODE
    GO
    -- CREATE edge table with edge constraints.
    CREATE TABLE bought
      (
        PurchaseCount INT,
        CONSTRAINT EC_BOUGHT CONNECTION (Customer TO Product, Supplier TO Product)
      )
    AS EDGE
    GO
    
    -- Query sys.edge_constraints and sys.edge_constraint_clauses to view 
    -- edge constraint properties. 
    SELECT
	    EC.name AS edge_constraint_name
	    , OBJECT_NAME(EC.parent_object_id) AS edge_table_name
	    , OBJECT_NAME(ECC.from_object_id) AS from_node_table_name
	    , OBJECT_NAME(ECC.to_object_id) AS to_node_table_name
	    , is_disabled
	    , is_not_trusted
    FROM sys.edge_constraints EC
    INNER JOIN sys.edge_constraint_clauses ECC
	    ON EC.object_id = ECC.object_id
    WHERE EC.parent_object_id = object_id('bought')
    ```  
  
 For more information, see [sys.edge_constraints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-edge-constraints-transact-sql.md) and [sys.edge_constraint_clauses &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-edge-constraint-clauses-transact-sql.md).  
  
###  <a name="TsqlExample"></a>  
