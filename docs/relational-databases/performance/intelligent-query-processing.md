---
title: "Intelligent query processing in Microsoft SQL databases | Microsoft Docs"
description: "Intelligent query processing features to improve query performance in SQL Server and Azure SQL Database."
ms.custom: ""
ms.date: "05/22/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
ms.tgt_pltfrm: ""
ms.topic: conceptual
helpviewer_keywords: 
ms.assetid: 
author: "joesackmsft"
ms.author: "josack"
manager: craigg
monikerRange: "= azuresqldb-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Intelligent query processing in SQL databases
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-xx-asdb-xxxx-xxx-md.md)]

The **Intelligent query processing** feature family contains features with broad impact that improve performance of existing workloads with minimal implementation effort.   This includes improvements of pre-existing constructs and also the introduction of adaptive methods and capabilities.  

## Adaptive query processing
Within the intelligent query processing feature family is the adaptive query processing feature family introduced in SQL Server 2017 and Azure SQL Database which added overall new query processing capabilities that adapt optimization strategies to your application workload’s runtime conditions:
- **Batch mode adaptive joins**. This feature allows your plan to dynamically switch to a better join strategy during execution using a single cached plan.
- **Batch mode memory grant feedback**. This feature recalculates the actual memory required for a query and then updates the grant value for the cached plan, reducing excessive memory grants that impact concurrency and fixing underestimated memory grants that cause expensive spills to disk.
- **Interleaved execution for multi-statement table valued functions (MSTVFs)**. With interleaved execution, we use the actual row counts from the function to make better-informed downstream query plan decisions. 

For more information about adaptive query processing, refer to the [Adaptive query processing in SQL databases](../../relational-databases/performance/adaptive-query-processing.md).

## See also
[Performance Center for SQL Server Database Engine and Azure SQL Database](../../relational-databases/performance/performance-center-for-sql-server-database-engine-and-azure-sql-database.md)     
[Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md)    
[Showplan Logical and Physical Operators Reference](../../relational-databases/showplan-logical-and-physical-operators-reference.md)    
[Joins](../../relational-databases/performance/joins.md)    
[Demonstrating Adaptive Query Processing](https://github.com/joesackmsft/Conferences/blob/master/Data_AMP_Detroit_2017/Demos/AQP_Demo_ReadMe.md)        
