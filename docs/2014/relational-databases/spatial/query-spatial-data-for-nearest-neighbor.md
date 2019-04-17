---
title: "Query Spatial Data for Nearest Neighbor | Microsoft Docs"
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
ms.assetid: 7af4ad5d-484e-45b4-aa16-83c33b358bb6
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Query Spatial Data for Nearest Neighbor
  A common query used with spatial data is the Nearest Neighbor query. Nearest Neighbor queries are used to find the closest spatial objects to a specific spatial object. For example a store locater for a Web site often must find the closest store locations to a customer location.  
  
 A Nearest Neighbor query can be written in a variety of valid query formats, but for the Nearest Neighbor query to use a spatial index the following syntax must be used.  
  
## Syntax  
  
```vb  
SELECT TOP ( number )  
        [ WITH TIES ]  
        [ * | expression ]   
        [, ...]  
    FROM spatial_table_reference, ...   
        [ WITH   
            (   
                [ INDEX ( index_ref ) ]   
                [ , SPATIAL_WINDOW_MAX_CELLS = <value>]   
                [ ,... ]   
            )   
        ]  
    WHERE   
        column_ref.STDistance ( @spatial_ object )   
            {   
                [ IS NOT NULL ] | [ < const ] | [ > const ]   
                | [ <= const ] | [ >= const ] | [ <> const ] ]   
            }  
            [ AND { other_predicate } ]   
    }  
    ORDER BY column_ref.STDistance ( @spatial_ object ) [ ,...n ]  
[ ; ]  
  
```  
  
## Nearest Neighbor Query and Spatial Indexes  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], `TOP` and `ORDER BY` clauses are used to perform a Nearest Neighbor query on spatial data columns. The `ORDER BY` clause contains a call to the `STDistance()` method for the spatial column data type. The `TOP` clause indicates the number of objects to return for the query.  
  
 The following requirements must be met for a Nearest Neighbor query to use a spatial index:  
  
1.  A spatial index must be present on one of the spatial columns and the `STDistance()` method must use that column in the `WHERE` and `ORDER BY` clauses.  
  
2.  The `TOP` clause cannot contain a `PERCENT` statement.  
  
3.  The `WHERE` clause must contain a `STDistance()` method.  
  
4.  If there are multiple predicates in the `WHERE` clause then the predicate containing `STDistance()` method must be connected by an `AND` conjunction to the other predicates. The `STDistance()` method cannot be in an optional part of the `WHERE` clause.  
  
5.  The first expression in the `ORDER BY` clause must use the `STDistance()` method.  
  
6.  Sort order for the first `STDistance()` expression in the `ORDER BY` clause must be `ASC`.  
  
7.  All the rows for which `STDistance` returns `NULL` must be filtered out.  
  
> [!WARNING]  
>  Methods that take `geography` or `geometry` data types as arguments will return `NULL` if the SRIDs are not the same for the types.  
  
 It is recommended that the new spatial index tessellations be used for indexes used in Nearest Neighbor queries. For more information on spatial index tessellations, see [Spatial Data &#40;SQL Server&#41;](spatial-data-sql-server.md).  
  
## Example  
 The following code example shows a Nearest Neighbor query that can use a spatial index. The example uses the `Person.Address` table in `AdventureWorks2012` database.  
  
```  
USE AdventureWorks2012  
GO  
DECLARE @g geography = 'POINT(-121.626 47.8315)';  
SELECT TOP(7) SpatialLocation.ToString(), City FROM Person.Address  
WHERE SpatialLocation.STDistance(@g) IS NOT NULL  
ORDER BY SpatialLocation.STDistance(@g);  
  
```  
  
 Create a spatial index on the column SpatialLocation to see how a Nearest Neighbor query uses a spatial index. For more information on creating spatial indexes, see [Create, Modify, and Drop Spatial Indexes](create-modify-and-drop-spatial-indexes.md).  
  
## Example  
 The following code example shows a Nearest Neighbor query that cannot use a spatial index.  
  
```  
USE AdventureWorks2012  
GO  
DECLARE @g geography = 'POINT(-121.626 47.8315)';  
SELECT TOP(7) SpatialLocation.ToString(), City FROM Person.Address  
ORDER BY SpatialLocation.STDistance(@g);  
  
```  
  
 The query lacks a `WHERE` clause that uses `STDistance()` in a form specified in the syntax section so the query cannot use a spatial index.  
  
## See Also  
 [Spatial Data &#40;SQL Server&#41;](spatial-data-sql-server.md)  
  
  
