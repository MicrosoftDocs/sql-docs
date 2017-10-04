---
title: "PolyBase Versioned Feature Summary | Microsoft Docs"
ms.custom: ""
ms.date: "08/29/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 6591994d-6109-4285-9c5b-ecb355f8a111
caps.latest.revision: 10
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# PolyBase Versioned Feature Summary
[!INCLUDE[tsql-appliesto-ss2016-xxxx-asdw-pdw_md](../../includes/tsql-appliesto-ss2016-xxxx-asdw-pdw-md.md)]

Summary of PolyBase features available for SQL Server products and services.  
  
## Feature Summary for Product Releases  
 This table summarizes key features for PolyBase and the products in which they are available.  
  
||||||
|-|-|-|-|-|   
|**Feature**|**SQL Server 2016**|**Azure SQL Database**|**Azure SQL Data Warehouse**|**Parallel Data Warehouse**| 
|Query Hadoop data with [!INCLUDE[tsql](../../includes/tsql-md.md)]|yes|no|no|yes|
|Import data from Hadoop|yes|no|no|yes|
|Export data to Hadoop  |yes|no|no| yes|
|Query, Import from, Export to HDInsights |no|no|no|no
|Push down query computations to Hadoop|yes|no|no|yes|  
|Import data from Azure blob storage|yes|no|yes|yes| 
|Export data to Azure blob storage|yes|no|yes|yes|  
|Import data from Azure Data Lake Store|no|no|yes|no|    
|Export data from Azure Data Lake Store|no|no|yes|no|
|Run PolyBase queries from Microsoft's BI tools|yes|no|yes|yes|   


## Pushdown computation supported T-SQL operators
In SQL Server and APS, not all T-SQL operators can be pushdown to the hadoop cluster. The table below lists the all supported and a subset of the unsupported operators. 

||||
|-|-|-| 
|**Operator type**|**Pushable to Hadoop**|**Pushable to Blob Storage**|
|Column Projections|yes|no|
|Predicates|yes|no|
|Aggregates|partial|no|
|Joins between External Tables|no|no|
|Joins between External Tables and Local tables|no|no|
|Sorts|no|no|

Partial aggregation means that a final aggregation must occur once the data reaches SQL Server, but a portion of the aggregation occurs in Hadoop. This is a common method computing aggregations in Massively Parallel Processing systems.  
## See Also  
 [PolyBase Guide](../../relational-databases/polybase/polybase-guide.md)  
  
  
