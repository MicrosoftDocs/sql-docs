---
title: "Frequently Asked Questions in PolyBase | Microsoft Docs"
ms.custom: ""
ms.date: 03/08/2019
ms.prod: sql
ms.reviewer: ""
ms.technology: polybase
ms.topic: conceptual
author: Abiola
ms.author: aboke
manager: 
---
# Frequently Asked Questions

## PolyBase VS. Linked Servers
The following table highlights the differences between PolyBase and Linked Server feature:

|PolyBase | Linked Servers|
|--------------------------|--------------------------|  
|Database scoped object|Instance scoped object| 
|Uses ODBC drivers|Uses OLEDB providers| 
| Supports read-only operations for all data sources and insert operation for HADOOP & Data pool data source only| Supports both read and write operations| 
|Queries to remote data source from a single connection can be scaled-out |Queries to remote data source from a single connection cannot be scaled-out|  where 
|Predicates push-down is supported|Predicates push-down is supported|
|No separate configuration needed for Always On Availability Group|Separate configuration needed for each instance in Always On Availability Group| 
|Basic authentication only|Basic & Integrated authentication| 
|Suitable for analytic queries processing large number of rows|Suitable for OLTP queries returning single or few rows|
|Queries using external table cannot participate in distributed transaction|Distributed queries can participate in distributed transaction|

## What is New in PolyBase 2019? 

PolyBase in SQL Server 2019 can now read data from a larger variety of data sources. The data from theses external data sources can be store as external tables on your SQL Server. PolyBase also supports Push-down computation to these external data sources, excluding ODBC generic types. 

#### Compatible Data Sources
- SQL Server
- Oracle
- Teradata
- MongoDB
- **Compatible** ODBC generic types *

> **Note**
>
> PolyBase can allow connection to external data sources using third party ODBC drivers. These drivers are not provided along with PolyBase and may work as intended. For more information visit our [guide](../../polybase-configure-odbc-generic.md) for PolyBase ODBC generic configuration.  

## PolyBase in Big Data Clusters vs. PolyBase in Stand-Alone Instances
The following table highlights the PolyBase features available in SQL Server 2019 stand-alone install and SQL Server 2019 big data cluster:

|Feature |Big Data Cluster| Stand Alone Instance|
|--------------------------|--------------------------|---------|   
|Create external data source for SQL Server, Oracle, Teradata, and Mongo DB |X|X |
|Create external data source using a compatible third-party ODBC Driver | | X|
|Create external data source for HADOOP data source | X| X|
|Create external data source for Azure Blob Storage | X| X|
|Create external table on a SQL Data Pool | X| |
|Create external table on a SQL Storage Pool | X| |
|Scale-out query execution | X| X|

> **Note**
>
>The table does not describe the functionality available in the latest SQL Server 2019 CTP. For the available features, please reference the release notes. For more information on connections using the ODBC generic connector visit our [How to guide for configuring ODBC generic types](polybase-configure-odbc-generic.md).