---
title: What is the SQL big data clusters master instance? | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 10/01/2018
ms.topic: conceptual
ms.prod: sql
---

# What is the SQL big data clusters master instance?

This article describes the role of the *SQL Server master instance* in a SQL Server 2019 preview Big Data cluster. The master instance is a SQL Server instance running in the SQL Server big data clusters [control plane](big-data-cluster-overview.md#controlplane).

The SQL Server master instance provides the following functionality:

## Connectivity

The SQL Server master instance provides an externally accessible TDS endpoint for the cluster.

## Scale-out query management

The SQL Server master instance contains the scale-out query engine that is used to distribute queries across SQL Server instances on nodes in the [compute pool](concept-compute-pool.md). The scale-out query engine also provides access through Transact-SQL to all Hive tables in the cluster without any additional configuration.

## Metadata and user databases

In addition to the standard SQL Server system databases, the SQL master instance also contains the following:

- A metadata database that holds HDFS-table metadata
- A data plane shard map
- Details of external tables that provide access to the cluster data plane.
- External sources using PolyBase.

You can also choose to add your own high-value databases to the SQL master instance.

## Machine Language services on the master instance of SQL Server big data cluster

SQL Server Machine Learning Services is an add-on feature to the database engine, used for executing R and Python code in SQL Server. This feature is based on the SQL Server extensibility framework, which isolates external processes from core engine processes, but fully integrates with the relational data as stored procedures, as T-SQL script containing R or Python statements, or as R or Python code containing T-SQL.

As part of SQL Server big data cluster, this feature will be available from the Master Instance by default. This means that once external script execution is enabled on the Master Instance, it is going to be possible to execute R and Python scripts using sp_execute_external_script.

### Advantages of Machine Learning Services in big data cluster

SQL Server 2019 makes it easy for big data sets to be joined to the dimensional and fact data (high value data) typically stored in the enterprise database. The value of the big data greatly increases when it is not just in the hands of parts of an organization, but is also included in reports, dashboards, and applications. At the same time, the data scientists can continue to use the Spark/HDFS ecosystem tools and have easy, real time access to the high-value data in SQL Server.
With SQL Server 2019 big data clusters, existing customers can do more with their enterprise data lakes. SQL developers and analysts can:

* Build applications consuming enterprise data lakes.
* Reason over all data with Transact-SQL queries.
* Use the existing ecosystem of SQL Server tools and applications to access and analyze enterprise data.
* Reduce the need for data movement through data virtualization and data marts in HDFS.
* Big data engineers and data scientists can:
* Continue to use Spark for big data scenarios.
* Build intelligent enterprise applications using:
* Spark to train models over data lakes.
* Operationalize models in production databases for best performance.
* Stream data directly into Enterprise data marts for real-time analytics.
* Explore data visually using interactive analysis and BI tools.

## Next steps

To learn more about the SQL Server big data clusters, see the following overview:

- [What is SQL Server 2019 big data clusters?](big-data-cluster-overview.md)
