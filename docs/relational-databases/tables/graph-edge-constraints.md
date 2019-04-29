---
title: "Graph edge constraints | Microsoft Docs"
ms.custom: ""
ms.date: 04/23/2019
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
ms.author: "shkale"
manager: craigg
monikerRange: ">=sql-server-2017||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Edge constraints
[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx.md](../../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

  Edge constraints can be used to enforce data integrity and  specific semantics on the edge tables in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] graph database. 
  
This article contains the following sections.  
  
[Edge Constraints](../../relational-databases/tables/graph-edge-constraints.md#Connection)  

[Edge Constraints](../../relational-databases/tables/graph-edge-constraints.md#Connection)  
  
[Related Tasks](../../relational-databases/tables/graph-edge-constraints.md#Tasks)  
  
##  <a name="Connection"></a> Edge Constraints
 In the first release of graph features, edge tables did not enforce anything for the endpoints of the edge. That is, an edge in a graph database could connect any node to any other node, regardless of the type. 

 This release introduces edge constraints, which enable users to add constraints to their edge tables, thereby enforcing specific semantics and also maintaining data integrity. When a new edge is added to an edge table with edge constraints, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] enforces that the nodes which the edge is trying to connect, exist in the proper node tables. It is also ensured that a node cannot be dropped, if it is still referenced by an edge. 

 ### Edge Constraint Clauses
 Each edge constraint consists of one or more edge constraint clause(s). An edge constraint clause is the pair of FROM and TO nodes that the given edge could connect. 

 Consider that you have `Product` and `Customer` nodes in your graph and you use `Bought` edge to connect these nodes. The edge constraint clause specifies the FROM and TO node pair and the direction of the edge. In this case the edge constraint clause will be `Customer` TO `Product`. That is,
 inserting a `Bought` that goes from a `Customer` to `Product` will be allowed. Attempts to insert an edge that goes from `Product` to `Customer` fail. 
  
- An edge constraint clause contains a pair of FROM and TO node tables that the edge constraint is enforced on. 
  
- Users may specify multiple edge constraint clauses per edge constraint, which will be applied as a disjunction.

- If multiple edge constraints are created on a single edge table, edges must satisfy ALL constraints to be allowed.
  
### Indexes on Edge Constraints
 Creating an edge constraint does not automatically create a corresponding index on `$from_id` and `$to_id` columns in the edge table. Manually creating an index on a `$from_id`, `$to_id` pair is recommended if you have point lookup queries or OLTP workload. 

##  <a name="Tasks"></a> Related Tasks  
 The following table lists the common tasks associated with edge constraints.  
  
|Task|Article|  
|----------|-----------|  
|Describes how to create edge constraints.|[Create Edge Constraints](../../relational-databases/tables/create-edge-constraints.md)|  
|Describes how to delete an edge constraint.|[Delete Edge Constraint](../../relational-databases/tables/delete-edge-constraint.md)|  
|Describes how to modify an edge constraint.|[Modify Edge Constraint](../../relational-databases/tables/modify-edge-constraint.md)|  
|Describes how to view edge constraint properties.|[View Edge Constraint Properties](../../relational-databases/tables/view-edge-constraint-properties.md)|  
