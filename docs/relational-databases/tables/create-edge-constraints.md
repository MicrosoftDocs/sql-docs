---
title: "Create Edge Constraints | Microsoft Docs"
ms.custom: ""
ms.date: "09/11/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "edge constraints [SQL Server]"
  - "CONNECTION constraints"
  - "edge constraints [Azure SQL Database]"
  - "graph edge constraints"
  - "SQL Graph" 
ms.assetid: 
author: "shkale-msft"
ms.author: shkale
manager: craigg
monikerRange: ">=sql-server-2017||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create Edge Constraints
[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx.md](../../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

   You can define a edge constraint in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[tsql](../../includes/tsql-md.md)]. 
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   An edge constraint can be defined on a graph edge table only. 
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  

### To create an edge constraint on a new edge table
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. The example creates an edge constraint on **bought** edge table.  
  
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

 CREATE TABLE Product 
  (
        ID INTEGER PRIMARY KEY, 
        ProductName VARCHAR(100)
  ) AS NODE
 GO

 CREATE TABLE bought
    (
        PurchaseCount INT,
        CONSTRAINT EC_BOUGHT CONNECTION (Customer TO Product)
    )
 AS EDGE
 GO
 ```  

### To add edge constraint to an existing edge table. 
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. The example uses ALTER TABLE to add an edge constraint to **bought** edge table.
  
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

 CREATE TABLE Product 
  (
        ID INTEGER PRIMARY KEY, 
        ProductName VARCHAR(100)
  ) AS NODE
 GO

 CREATE TABLE bought
    (
        PurchaseCount INT
    )
 AS EDGE
 GO

 ALTER TABLE bought ADD CONSTRAINT EC_BOUGHT1 CONNECTION (Customer TO Product)
 GO
 ```  

### Creating a new edge constraint on existing edge table, with additional edge constraint clauses.
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. The example uses ALTER TABLE to add a new edge constraint with additional edge constraint clauses on the **bought** edge table.
  
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
  ) AS NODE
 GO

 CREATE TABLE bought
    (
        PurchaseCount INT,
        CONSTRAINT EC_BOUGHT CONNECTION (Customer TO Product)
    )
 AS EDGE
 GO

 -- Drop the existing edge constraint first and then create a new one.
 ALTER TABLE bought DROP CONSTRAINT EC_BOUGHT
 GO

 -- User ALTER TABLE to create a new edge constraint.
 ALTER TABLE bought ADD CONSTRAINT EC_BOUGHT1 CONNECTION (Customer TO Product, Supplier TO Product)
 GO
 ```  
 Note that there are two edge constraint clauses in *EC_BOUGHT1* constraint, one that connects **Customer** to **Product** and other connects **Supplier** to **Product**. Both these clauses are applied in disjunction. That is, a given edge has to satisfy either of these two clauses to be allowed in the edge table. 



### Creating a new edge constraint on existing edge table, with new edge constraint clause.
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. The example uses ALTER TABLE to add a new edge constraint with a new edge constraint clause on the **bought** edge table.
  
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
  ) AS NODE
 GO

 CREATE TABLE bought
    (
        PurchaseCount INT,
        CONSTRAINT EC_BOUGHT CONNECTION (Customer TO Product)
    )
 AS EDGE
 GO

 ALTER TABLE bought ADD CONSTRAINT EC_BOUGHT1 CONNECTION (Supplier TO Product)
 GO
 ```  

 In this example, we created two separate edge constraints on the **bought** edge table, *EC_BOUGHT* and *EC_BOUGHT1*. Both these edge constraints have different edge constraint clauses. If an edge table has more than one edge constraint on it, a given edge has to satisfy **ALL** edge constraints to be allowed in the edge table. Since no edge will be able to satisfy both *EC_BOUGHT* and *EC_BOUGHT1* here, the **bought** edge table must remain empty. 

 For inserts to succeed in this edge table, either one of the edge constraint must be dropped, or both of them must be dropped and a new edge constraint should be created which has both edge constraint clauses in it. 

 ```sql
 USE TEMPDB
 GO
 -- Drop the existing edge constraint first and then create a new one.
 ALTER TABLE bought DROP CONSTRAINT EC_BOUGHT
 GO
 
 ALTER TABLE bought DROP CONSTRAINT EC_BOUGHT1
 GO
 
 ALTER TABLE bought ADD CONSTRAINT EC_BOUGHT_NEW CONNECTION (Customer TO Product, Supplier TO Product)
 GO
 ```  

 Now for a given edge to be allowed in the **bought** edge, it has to satisfy either of the edge constraint clauses in *EC_BOUGHT_NEW* constraint. Hence any edge that is trying to connect valid **Customer** to **Product** or **Supplier** to **Product** nodes, will be allowed. 
