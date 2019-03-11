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
# Frequently asked questions

## PolyBase VS. linked servers
The following table highlights the differences between PolyBase and linked server features:

|PolyBase | Linked Servers|
|--------------------------|--------------------------|  
|Database scoped object|Instance scoped object|
|Uses ODBC drivers|Uses OLEDB providers|
|Supports read-only operations for all data sources and insert operation for HADOOP & data pool data source only|Supports both read and write operations|
|Queries to remote data source from a single connection can be scaled-out |Queries to remote data source from a single connection cannot be scaled-out|
|Predicates push-down is supported|Predicates push-down is supported|
|No separate configuration needed for availability group|Separate configuration needed for each instance in availability group|
|Basic authentication only|Basic & integrated authentication|
|Suitable for analytic queries processing large number of rows|Suitable for OLTP queries returning single or few rows|
|Queries using external table cannot participate in distributed transaction|Distributed queries can participate in distributed transaction|

## What's new in PolyBase 2019? 

PolyBase in [!INCLUDE[sssqlv15](../../includes/sssqlv15-md.md)] can now read data from a larger variety of data sources. The data from theses external data sources can be store as external tables on your SQL Server. PolyBase also supports push-down computation to these external data sources, excluding ODBC generic types.

### Compatible Data Sources

- SQL Server
- Oracle
- Teradata
- MongoDB
- Compatible ODBC generic types
  > **Note**
  > PolyBase can allow connection to external data sources using third party ODBC drivers. These drivers are not provided along with PolyBase and may not work as intended. For more information, visit our [guide](../../relational-databases/polybase/polybase-configure-odbc-generic.md) for PolyBase ODBC generic configuration.  

## PolyBase in big data clusters vs. PolyBase in stand-alone instances

The following table highlights the PolyBase features available in [!INCLUDE[sssqlv15](../../includes/sssqlv15-md.md)] stand-alone install and [!INCLUDE[sssqlv15](../../includes/sssqlv15-md.md)] big data cluster:

|Feature |Big data cluster|Stand alone instance|
|--------------------------|--------------------------|---------|   
|Create external data source for SQL Server, Oracle, Teradata, and Mongo DB |X|X |
|Create external data source using a compatible third-party ODBC Driver | | X|
|Create external data source for HADOOP data source | X| X|
|Create external data source for Azure Blob Storage | X| X|
|Create external table on a SQL Server data pool | X| |
|Create external table on a SQL Server storage pool | X| |
|Scale-out query execution | X| X|

> **Note**
>
>The table does not describe the functionality available in the latest [!INCLUDE[sssqlv15](../../includes/sssqlv15-md.md)] CTP. For the available features, please reference the release notes. For more information on connections using the ODBC generic connector visit our [How to guide for configuring ODBC generic types](polybase-configure-odbc-generic.md).