---
description: "Graph edge constraints"
title: "Graph edge constraints | Microsoft Docs"
ms.custom: ""
ms.date: 09/09/2019
ms.service: sql
ms.reviewer: ""
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "edge constraints [SQL Server]"
  - "CONNECTION constraints"
  - "edge constraints [Azure SQL Database]"
  - "graph edge constraints"
  - "SQL Graph"
author: "MikeRayMSFT"
ms.author: "mikeray"
monikerRange: ">=sql-server-2017||>=sql-server-linux-2017||=azuresqldb-mi-current||=azuresqldb-current"
---
# Edge constraints

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Edge constraints can be used to enforce data integrity and specific semantics on the edge tables in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] graph database.

## <a name="Connection"></a> Edge Constraints

By default, edge tables do not enforce anything for the endpoints of the edge. That is, an edge in a graph database could connect any node to any other node, regardless of the type.

SQL Graph supports edge constraints, which enable users to add constraints to their edge tables, thereby enforcing specific semantics and also maintaining data integrity. When a new edge is added to an edge table with edge constraints, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] enforces that the nodes which the edge is trying to connect, exist in the proper node tables. It is also ensured that a node cannot be dropped, if it is still referenced by an edge.

### Edge Constraint Clauses

A single edge constraint consists of one or more edge constraint clause(s). 
````
CONSTRAINT constraint_name CONNECTION (cause1[, clause2...])
````

- An edge constraint clause is a pair of node table names, separated by the `TO` keyword.
- The first table name in the edge constraint clause is the name of the FROM node table for the edge relationship.
- The second table name in the edge constraint clause is the name of the TO node table for the edge relationship.
- The pair of table names therefore indicates the _direction_ of the edge relationship.
- As stated previously, an edge constraint can contain one or more edge constraint clauses.  

#### Multiple constraints and clauses
- Multiple edge constraints, defined for the same edge table, are enforced with an `AND` operator.
- Multiple edge constraint _clauses_, defined within the same edge constraint, are enforced with an `OR` operator.

Consider the `Supplier` and `Customer` nodes in your graph. Each can be related to the `Product` node by a single, shared edge table: `bought`. The `bought` edge table supports `Customer-(bought)->Product` and `Supplier-(bought)->Product` relationship types. This can be accomplished using a single edge constraint with multiple edge constraint clauses.

##### Examples

```sql
CONSTRAINT EC_BOUGHT CONNECTION (Customer TO Product)
```

The above example shows one edge constraint, with one edge constraint clause. This constraint supports `Customer-(bought)->Product`. That is, inserting a `bought` edge relationship going from a `Customer` to `Product` would be allowed. Inserting any other combination of nodes, like `Supplier-(bought)->Product`, even though it may describe a valid relationship in the real world, would fail.

```sql
CONSTRAINT EC_BOUGHT CONNECTION (Supplier TO Product, Customer TO Product)
```

The above example defines one edge constraint with two edge constraint clauses. These constraint clauses allow the `bought` edge to contain either `Supplier-(bought)->Product` or `Customer-(bought)->Product` relationships. Inserting any other types of edge relationships into the `bought` table would fail.

```sql
CONSTRAINT EC_BOUGHT1 CONNECTION (Supplier TO Product)
CONSTRAINT EC_BOUGHT2 CONNECTION (Customer TO Product)
```

The above example shows two constraints on the same edge table, with each edge constraint specifying one constraint clause. In this situation, SQL would only allow inserts that satisfy BOTH edge constraint clauses simultaneously. This is, of course, impossible. There is no node pair that can satisfy both edge constraint clauses. This edge constraint combination makes the edge table unusable.

For a detailed explanation of where multiple edge constraints can be used in a real-life scenario, please see the example "Creating a new edge constraint on existing edge table, with new edge constraint clause" later in this page.

### Indexes on edge constraints

Creating an edge constraint does not automatically create a corresponding index on `$from_id` and `$to_id` columns in the edge table. Manually creating an index on a `$from_id`, `$to_id` pair is recommended if you have point lookup queries or OLTP workload.

### ON DELETE referential actions on edge constraints
Cascading actions on an edge constraint let users define the actions that the database engine takes when a user deletes the node(s), which the given edge connects. The following referential actions can be defined:
*NO ACTION*
The database engine raises an error when you try to delete a node that has connecting edge(s).

*CASCADE*
When a node is deleted from the database, connecting edge(s) are deleted.

## Working with edge constraints

You can define an edge constraint in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[tsql](../../includes/tsql-md.md)]. An edge constraint can be defined on a graph edge table only. To create, delete, or modify an edge constraint, you must have **ALTER** permission on the table.

### Create edge constraints

The following examples show you how to create an edge constraint on new or existing tables.

#### To create an edge constraint on a new edge table

The following example creates an edge constraint on the **bought** edge table.

```sql
-- CREATE node and edge tables
CREATE TABLE Customer
   (
      ID INTEGER PRIMARY KEY
      ,CustomerName VARCHAR(100)
   )
AS NODE;
GO
CREATE TABLE Product
   (
      ID INTEGER PRIMARY KEY
      ,ProductName VARCHAR(100)
   )
AS NODE;
GO
CREATE TABLE bought
   (
      PurchaseCount INT
         ,CONSTRAINT EC_BOUGHT CONNECTION (Customer TO Product) ON DELETE NO ACTION
   )
   AS EDGE;
   ```

#### Defining referential actions on a new edge table

The following example creates an edge constraint on the **bought** edge table and defines ON DELETE CASCADE referential action.

```sql
-- CREATE node and edge tables
CREATE TABLE Customer
   (
      ID INTEGER PRIMARY KEY
      ,CustomerName VARCHAR(100)
   )
AS NODE;
GO
CREATE TABLE Product
   (
      ID INTEGER PRIMARY KEY
      ,ProductName VARCHAR(100)
   )
AS NODE;
GO
CREATE TABLE bought
   (
      PurchaseCount INT
         ,CONSTRAINT EC_BOUGHT CONNECTION (Customer TO Product) ON DELETE CASCADE
   )
   AS EDGE;
   ```

#### To add edge constraint to an existing edge table

The following example uses ALTER TABLE to add an edge constraint to the **bought** edge table.

```sql
-- CREATE node and edge tables
CREATE TABLE Customer
   (
      ID INTEGER PRIMARY KEY
      , CustomerName VARCHAR(100)
   )
   AS NODE;
CREATE TABLE Product
   (
      ID INTEGER PRIMARY KEY
      , ProductName VARCHAR(100)
   )
   AS NODE;
GO
CREATE TABLE bought
   (
      PurchaseCount INT
   )
   AS EDGE;
GO
ALTER TABLE bought ADD CONSTRAINT EC_BOUGHT1 CONNECTION (Customer TO Product);
```

#### Create a new edge constraint on existing edge table, with additional edge constraint clauses

The following example uses the `ALTER TABLE` command to add a new edge constraint with additional edge constraint clauses on the **bought** edge table.

```sql
-- CREATE node and edge tables
CREATE TABLE Customer
   (
      ID INTEGER PRIMARY KEY
      , CustomerName VARCHAR(100)
   )
   AS NODE;
GO
CREATE TABLE Supplier
   (
      ID INTEGER PRIMARY KEY
      , SupplierName VARCHAR(100)
   )
   AS NODE;
GO
CREATE TABLE Product
   (
      ID INTEGER PRIMARY KEY
      , ProductName VARCHAR(100)
   )
   AS NODE;
GO
CREATE TABLE bought
   (
      PurchaseCount INT
      , CONSTRAINT EC_BOUGHT CONNECTION (Customer TO Product)
   )
   AS EDGE;
-- Drop the existing edge constraint first and then create a new one.
ALTER TABLE bought DROP CONSTRAINT EC_BOUGHT;
GO
-- User ALTER TABLE to create a new edge constraint.
ALTER TABLE bought ADD CONSTRAINT EC_BOUGHT1 CONNECTION (Customer TO Product, Supplier TO Product);
```

In the preceding example, there are two edge constraint clauses in *EC_BOUGHT1* constraint, one that connects **Customer** to **Product** and other connects **Supplier** to **Product**. Both these clauses are applied in disjunction. That is, a given edge has to satisfy either of these two clauses to be allowed in the edge table.

#### Creating a new edge constraint on existing edge table, with new edge constraint clause

The following example uses the `ALTER TABLE` command to add a new edge constraint with a new edge constraint clause on the **bought** edge table.

 ```sql
-- CREATE node and edge tables
CREATE TABLE Customer
   (
      ID INTEGER PRIMARY KEY
      , CustomerName VARCHAR(100)
   )
   AS NODE;
GO
CREATE TABLE Supplier
   (
      ID INTEGER PRIMARY KEY
      , SupplierName VARCHAR(100)
   )
   AS NODE;
GO
CREATE TABLE Product
   (
      ID INTEGER PRIMARY KEY
      , ProductName VARCHAR(100)
   )
   AS NODE;
GO
CREATE TABLE bought
   (
      PurchaseCount INT,
         CONSTRAINT EC_BOUGHT CONNECTION (Customer TO Product)
   )
   AS EDGE;
GO
 ```

In the preceding example, imagine that we now need to also include the `Supplier` to `Product` relationship, through the `bought` edge table. You may first think of adding a new edge constraint, as given below:
``` sql
ALTER TABLE bought ADD CONSTRAINT EC_BOUGHT1 CONNECTION (Supplier TO Product);
```

However, this is not the correct solution. We created two separate edge constraints on the **bought** edge table, *EC_BOUGHT* and *EC_BOUGHT1*. Both these edge constraints have different edge constraint clauses. If an edge table has more than one edge constraint on it, a given edge has to satisfy **ALL** edge constraints to be allowed in the edge table. Since no edge will be able to satisfy both *EC_BOUGHT* and *EC_BOUGHT1* here, the above `ALTER TABLE` statement will fail if there are any rows at all in the **bought** edge table.

For this edge constraint to be created successfully, the prescribed way is to follow a sequence like the below:

```sql
-- First, add the desired ("super-set") constraint:
ALTER TABLE bought ADD CONSTRAINT EC_BOUGHT_NEW CONNECTION (Customer TO Product, Supplier TO Product);
GO

-- Then, drop the older edge constraint:
ALTER TABLE bought DROP CONSTRAINT EC_BOUGHT;
GO

-- If needed, you can remame the new edge constraint to match the original name:
EXECUTE sp_rename '[dbo].[EC_BOUGHT_NEW]', '[dbo].[EC_BOUGHT]';
```

The fact that we added the new "super-set" constraint first without dropping the earlier one, allows the operation to be a metadata-only operation - it does not need to check all the existing data in the **bought** table, as it encompasses the existing constraint.

With this, for a given edge to be allowed in the **bought** edge, it has to satisfy either of the edge constraint clauses in *EC_BOUGHT_NEW* constraint. Hence any edge that is trying to connect valid **Customer** to **Product** or **Supplier** to **Product** nodes, will be allowed.

### Delete edge constraints

The following example first identifies the name of the edge constraint and then deletes the constraint.

```sql
-- CREATE node and edge tables
CREATE TABLE Customer
   (
      ID INTEGER PRIMARY KEY
      , CustomerName VARCHAR(100)
   )
   AS NODE;
GO
CREATE TABLE Product
   (
      ID INTEGER PRIMARY KEY
      , ProductName VARCHAR(100)
   ) AS NODE;
GO
CREATE TABLE bought
   (
      PurchaseCount INT
      , CONSTRAINT EC_BOUGHT CONNECTION (Customer TO Product)
    )
    AS EDGE;
GO
-- Return the name of edge constraint.
SELECT name
   FROM sys.edge_constraints
   WHERE type = 'EC' AND parent_object_id = OBJECT_ID('bought');
GO
-- Delete the primary key constraint.
ALTER TABLE bought
DROP CONSTRAINT EC_BOUGHT;
```

### Modify edge constraints

To modify an edge constraint using Transact-SQL, you must first delete the existing edge constraint and then re-create it with the new definition.


### View edge constraints

[!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).

The example returns all edge constraints and their properties for the edge table `bought` in the tempdb database.

```sql
-- CREATE node and edge tables
CREATE TABLE Customer
   (
      ID INTEGER PRIMARY KEY
      , CustomerName VARCHAR(100)
   )
   AS NODE;
GO
CREATE TABLE Supplier
   (
      ID INTEGER PRIMARY KEY
      , SupplierName VARCHAR(100)
   )
   AS NODE;
   GO
CREATE TABLE Product
   (
      ID INTEGER PRIMARY KEY
      , ProductName VARCHAR(100)
   )
   AS NODE;

-- CREATE edge table with edge constraints.
CREATE TABLE bought
   (
      PurchaseCount INT
      , CONSTRAINT EC_BOUGHT CONNECTION (Customer TO Product, Supplier TO Product)
   )
   AS EDGE;

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
WHERE EC.parent_object_id = object_id('bought');
```

## Related tasks

[CREATE TABLE (SQL Graph)](../../t-sql/statements/create-table-sql-graph.md)
[ALTER TABLE table_constraint](../../t-sql/statements/alter-table-table-constraint-transact-sql.md)

For information about graph technology in SQL Server, see [Graph processing with SQL Server and Azure SQL Database](../graphs/sql-graph-overview.md).
