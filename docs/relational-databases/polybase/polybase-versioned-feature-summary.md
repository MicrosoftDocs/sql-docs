---
title: "PolyBase features and limitations | Microsoft Docs"
ms.custom: ""
ms.date: 09/24/2018
ms.prod: sql
ms.technology: polybase
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 6591994d-6109-4285-9c5b-ecb355f8a111
author: rothja
ms.author: jroth
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# PolyBase features and limitations

[!INCLUDE[appliesto-ss2016-asdb-asdw-pdw-md](../../includes/tsql-appliesto-ss2016-all-md.md)]

This article is a summary of PolyBase features available for SQL Server products and services.  
  
## Feature summary for product releases

This table lists the key features for PolyBase and the products in which they're available.  
  
||||||
|-|-|-|-|-|   
|**Feature**|**SQL Server 2016**|**Azure SQL Database**|**Azure SQL Data Warehouse**|**Parallel Data Warehouse**| 
|Query Hadoop data with [!INCLUDE[tsql](../../includes/tsql-md.md)]|Yes|No|No|Yes|
|Import data from Hadoop|Yes|No|No|Yes|
|Export data to Hadoop  |Yes|No|No| Yes|
|Query, import from, export to Azure HDInsight |No|No|No|No
|Push down query computations to Hadoop|Yes|No|No|Yes|  
|Import data from Azure Blob storage|Yes|No|Yes|Yes| 
|Export data to Azure Blob storage|Yes|No|Yes|Yes|  
|Import data from Azure Data Lake Store|No|No|Yes|No|    
|Export data from Azure Data Lake Store|No|No|Yes|No|
|Run PolyBase queries from Microsoft BI tools|Yes|No|Yes|Yes|   

## Pushdown computation supported by T-SQL operators

In SQL Server and APS, not all T-SQL operators can be pushed down to the Hadoop cluster. This table lists all the supported operators and a subset of the unsupported operators. 

||||
|-|-|-| 
|**Operator type**|**Pushable to Hadoop**|**Pushable to Blob storage**|
|Column projections|Yes|No|
|Predicates|Yes|No|
|Aggregates|Partial|No|
|Joins between external tables|No|No|
|Joins between external tables and local tables|No|No|
|Sorts|No|No|

Partial aggregation means that a final aggregation must occur after the data reaches SQL Server. But a portion of the aggregation occurs in Hadoop. This method is common in computing aggregations in massively parallel processing systems.  

## Known limitations

PolyBase has the following limitations:

- In order to use PolyBase you must have sysadmin or CONTROL SERVER level permissions on the database.

- The maximum possible row size, which includes the full length of variable length columns, can't exceed 32 KB in SQL Server or 1 MB in Azure SQL Data Warehouse.

- When data is exported into an ORC file format from SQL Server or SQL Data Warehouse, text-heavy columns might be limited. They can be limited to as few as 50 columns because of Java out-of-memory error messages. To work around this issue, export only a subset of the columns.

- PolyBase can't connect to a Hortonworks instance if Knox is enabled.

- If you use Hive tables with transactional = true, PolyBase can't access the data in the Hive table's directory.

<!--SQL Server 2016-->
::: moniker range="= sql-server-2016 || =sqlallproducts-allversions"

- [PolyBase doesn't install when you add a node to a SQL Server 2016 failover cluster](https://support.microsoft.com/help/3173087/fix-polybase-feature-doesn-t-install-when-you-add-a-node-to-a-sql-server-2016-failover-cluster).

::: moniker-end

## Next steps

For more information about PolyBase, see [What is PolyBase?](polybase-guide.md).
