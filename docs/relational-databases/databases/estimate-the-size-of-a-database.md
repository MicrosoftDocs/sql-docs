---
title: "Estimate the Size of a Database | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "space allocation [SQL Server], database size"
  - "calculating database size"
  - "increasing database size"
  - "database size [SQL Server], estimating"
  - "predicting database size"
  - "size [SQL Server], databases"
  - "estimating database size"
  - "designing databases [SQL Server], estimating size"
ms.assetid: 5b240161-eba4-44b0-946c-61a8fc00fc8c
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Estimate the Size of a Database
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  When you design a database, you may have to estimate how large the database will be when filled with data. Estimating the size of the database can help you determine the hardware configuration you will require to do the following:  
  
-   Achieve the performance required by your applications.  
  
-   Guarantee the appropriate physical amount of disk space required to store the data and indexes.  
  
 Estimating the size of a database can also help you determine whether the database design needs refining. For example, you may determine that the estimated size of the database is too large to implement in your organization and that more normalization is required. Conversely, the estimated size may be smaller than expected. This would allow you to denormalize the database to improve query performance.  
  
 To estimate the size of a database, estimate the size of each table individually and then add the values obtained. The size of a table depends on whether the table has indexes and, if they do, what type of indexes.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Estimate the Size of a Table](../../relational-databases/databases/estimate-the-size-of-a-table.md)|Defines the steps and calculations needed to estimate the amount of space required to store the data in a table and associated indexes.|  
|[Estimate the Size of a Heap](../../relational-databases/databases/estimate-the-size-of-a-heap.md)|Defines the steps and calculations needed to estimate the amount of space required to store the data in a heap. A heap is a table that does not have a clustered index.|  
|[Estimate the Size of a Clustered Index](../../relational-databases/databases/estimate-the-size-of-a-clustered-index.md)|Defines the steps and calculations needed to estimate the amount of space required to store the data in a clustered index.|  
|[Estimate the Size of a Nonclustered Index](../../relational-databases/databases/estimate-the-size-of-a-nonclustered-index.md)|Defines the steps and calculations needed to estimate the amount of space required to store the data in a nonclustered index.|  
  
  
