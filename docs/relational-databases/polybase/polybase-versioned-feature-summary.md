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

## Known Limitations

PolyBase has the following limitations:

- The maximum possible row size, including the full length of variable length columns, can not exceed 32 KB in SQL Server or 1 MB in Azure SQL Data Warehouse.

- PolyBase doesn’t support the Hive 0.12+ data types (i.e. Char(), VarChar())

- When exporting data into an ORC File Format from SQL Server or Azure SQL Data Warehouse text heavy columns can be limited to as few as 50 columns due to java out of memory errors. To work around this, export only a subset of the columns.

- Cannot Read or Write data encrypted at rest in Hadoop. This includes HDFS Encrypted Zones or Transparent Encryption.

- PolyBase cannot connect to a Hortonworks instance if KNOX is enabled.

- If you are using Hive tables with transactional = true, PolyBase cannot access the data in the Hive table's directory.

<!--SQL Server 2016-->
::: moniker range="= sql-server-2016 || =sqlallproducts-allversions"

- [PolyBase doesn't install when you add a node to a SQL Server 2016 Failover Cluster](https://support.microsoft.com/en-us/help/3173087/fix-polybase-feature-doesn-t-install-when-you-add-a-node-to-a-sql-server-2016-failover-cluster)

::: moniker-end
- Integration authentication is not support. Only user name and password are supported for now.  
- We enable encryption by default. In order to disable encrption you must... (talk to thanh)
- [Type mapping limitations](polybase-type-mapping.md)


## Security and Authentication 

## See Also  

[PolyBase Guide](../../relational-databases/polybase/polybase-guide.md)  
