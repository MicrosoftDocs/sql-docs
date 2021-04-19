---
description: "PolyBase features and limitations"
title: "PolyBase features and limitations"
descriptions: This article summarizes PolyBase features available for SQL Server products and services. It lists T-SQL operators supported for pushdown and known limitations.
ms.date: 04/06/2021
ms.prod: sql
ms.technology: polybase
ms.topic: conceptual
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# PolyBase features and limitations

[!INCLUDE[appliesto-ss2016-asdb-asdw-pdw-md](../../includes/tsql-appliesto-ss2016-all-md.md)]

This article is a summary of PolyBase features available for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] products and services.  
  
## Feature summary for product releases

This table lists the key features for PolyBase and the products in which they're available.  

|**Feature** |**[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]** (Beginning with 2016) |**Azure SQL Database** |**[!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)]** |**Parallel Data Warehouse** |
|---------|---------|---------|---------|---------|
|Query Hadoop data with [!INCLUDE[tsql](../../includes/tsql-md.md)]|Yes|No|No|Yes|
|Import data from Hadoop|Yes|No|No|Yes|
|Export data to Hadoop  |Yes|No|No| Yes|
|Query, import from, export to Azure HDInsight |No|No|No|No
|Push down query computations to Hadoop|Yes|No|No|Yes|  
|Import data from Azure Blob storage|Yes|Yes<sup>*</sup>|Yes|Yes|
|Export data to Azure Blob storage|Yes|No|Yes|Yes|  
|Import data from Azure Data Lake Store|No|No|Yes|No|
|Export data to Azure Data Lake Store|No|No|Yes|No|
|Run PolyBase queries from Microsoft BI tools|Yes|No|Yes|Yes|

<sup>*</sup> Introduced in [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)], see [Examples of bulk access to data in Azure Blob storage](../import-export/examples-of-bulk-access-to-data-in-azure-blob-storage.md).


## Pushdown computation supported by T-SQL operators

In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and APS, not all T-SQL operators can be pushed down to the Hadoop cluster. This table lists all the supported operators and a subset of the unsupported operators.

|**Operator type** |**Pushable to Hadoop** |**Pushable to Blob storage** | 
|---------|---------|---------|
|Column projections|Yes|No|
|Predicates|Yes|No|
|Aggregates|Partial\*|No|
|Joins between external tables|No|No|
|Joins between external tables and local tables|No|No|
|Sorts|No|No|

\* Partial aggregation means that a final aggregation must occur after the data reaches [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. But a portion of the aggregation occurs in Hadoop. This method is common in computing aggregations in massively parallel processing systems.  


#### Hadoop pushdown support

Hadoop providers support the following:

| **Aggregations**                  | **Filters (binary comparison)** | 
|-----------------------------------|---------------------------------| 
| Count_Big                         | NotEqual                        | 
| Sum                               | LessThan                        | 
| Avg                               | LessOrEqual                     | 
| Max                               | GreaterOrEqual                  | 
| Min                               | GreaterThan                     | 
| Approx_Count_Distinct             | Is                              | 
|                                   | IsNot                           | 
|                                   |                                 | 

## Known limitations

PolyBase has the following limitations:

- In order to use PolyBase, you must have sysadmin or CONTROL SERVER level permissions on the database.

- Before [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], the maximum possible row size, which includes the full length of variable length columns, can't exceed 32 KB in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or 1 MB in [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)]. Starting with [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], this limitation is lifted. The limit remains 1 MB for Hadoop data sources, but is limited only by the maximum [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] limit for other data sources.

- When data is exported into an ORC file format from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)], text-heavy columns might be limited. They can be limited to as few as 50 columns because of Java out-of-memory error messages. To work around this issue, export only a subset of the columns.

- PolyBase can't connect to any Hadoop instance if Knox is enabled.

- If you use Hive tables with transactional = true, PolyBase can't access the data in the Hive table's directory.

<!--SQL Server 2016-->
::: moniker range="= sql-server-2016 "

- [PolyBase doesn't install when you add a node to a [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] failover cluster](https://support.microsoft.com/help/3173087/fix-polybase-feature-doesn-t-install-when-you-add-a-node-to-a-sql-server-2016-failover-cluster).

::: moniker-end

## Next steps

For more information about PolyBase, see [What is PolyBase?](polybase-guide.md)
