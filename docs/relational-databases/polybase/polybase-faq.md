---
title: "Frequently Asked Questions in PolyBase | Microsoft Docs"
ms.custom: ""
ms.date: 01/07/2019
ms.prod: sql
ms.reviewer: ""
ms.technology: polybase
ms.topic: conceptual
author: Abiola
ms.author: aboke
manager: 
---
# Frequently Asked Questions

## PolyBase in Big Data Clusters VS. PolyBase in Stand-Alone Instances

|Feature |Big Data Cluster| Stand-alone Instance|
|--------------------------|--------------------------|---------|   
|Create external tables| X| X|
|Create external data source from SQL Server, Oracle, Teradata, and Mongo DB |X|X |
|Create external data source using a compatible third party ODBC Driver | | X|
|PolyBase scale out groups | X | |
|data pool Instances | X| |
|storage pool instances| X| |

> **Note**
>
>For more information on connections using the ODBC generic connector visit our [How to guide for configuring ODBC generic types](polybase-configure-odbc-generic.md)

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
> PolyBase can allow connection to external data sources using third party ODBC drivers. These drivers are not provided along with PolyBase and may work as intended. for more information visit our [guide](../../polybase-configure-odbc-generic.md) for PolyBase ODBC generic configuration.  



## PolyBase VS. Linked Servers

|PolyBase | Linked Servers|
|--------------------------|--------------------------|  
|Database scoped object|Instance scoped object| 
|Uses ODBC drivers|Uses OLEDB providers| 
| Supports read-only operations only. Will be expanded in future| Supports read-only operations only. Will be expanded in future| 
|Queries can be scaled-out & push-down supported|Queries are single-threaded & push-down supportedt|
|No separate configuration needed for Always On Availability Group|Separate configuration needed for each instance in Always On Availability Group| 
|Basic authentication only. Improvements in SQL Server 2019|Basic & Integrated authentication| 

