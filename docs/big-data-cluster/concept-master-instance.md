---
title: What is the master instance?
titleSuffix: SQL Server big data clusters
description: This article describes the SQL Server master instance in a SQL Server 2019 big data cluster.
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 08/21/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# What is the master instance in a SQL Server big data cluster?

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

This article describes the role of the *SQL Server master instance* in a big data cluster for SQL Server 2019. The master instance is a SQL Server instance running in a big data cluster to manage connectivity, scale-out queries, metadata and user databases, and machine learning services.

The SQL Server master instance provides the following functionality:

## Connectivity

The SQL Server master instance provides an externally accessible TDS endpoint for the cluster. You can connect applications or SQL Server tools like Azure Data Studio or SQL Server Management Studio to this endpoint just like you would any other SQL Server instance.

## Scale-out query management

The SQL Server master instance contains the scale-out query engine that is used to distribute queries across SQL Server instances on nodes in the [compute pool](concept-compute-pool.md). The scale-out query engine also provides access through Transact-SQL to all Hive tables in the cluster without any additional configuration.

## Metadata and user databases

In addition to the standard SQL Server system databases, the SQL master instance also contains the following:

- A metadata database that holds HDFS-table metadata
- A data plane shard map
- Details of external tables that provide access to the cluster data plane.
- PolyBase external data sources and external tables defined in user databases.

You can also choose to add your own user databases to the SQL Server master instance.

## Machine learning services

SQL Server machine learning services is an add-on feature to the database engine, used for executing Java, R and Python code in SQL Server. This feature is based on the SQL Server extensibility framework, which isolates external processes from core engine processes, but fully integrates with the relational data as stored procedures, as T-SQL script containing R or Python statements, or as Java, R or Python code containing T-SQL.

As part of a SQL Server big data cluster, machine learning services will be available on the SQL Server master instance by default. This means that once external script execution is enabled on the SQL Server master instance, it is going to be possible to execute Java, R and Python scripts using sp_execute_external_script.

### Advantages of machine learning services in a big data cluster

SQL Server 2019 makes it easy for big data to be joined to the dimensional data typically stored in the enterprise database. The value of the big data greatly increases when it is not just in the hands of parts of an organization, but is also included in reports, dashboards, and applications. At the same time, data scientists can continue to use the Spark/HDFS ecosystem tools and have easy, real time access to the data in the SQL Server master instance and in external data sources accessible _through_ the SQL Server master instance.

With [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)], you can do more with your enterprise data lakes. SQL Server developers and analysts can:

* Build applications consuming data from enterprise data lakes.
* Reason over all data with Transact-SQL queries.
* Use the existing ecosystem of SQL Server tools and applications to access and analyze enterprise data.
* Reduce the need for data movement through data virtualization and data marts.
* Continue to use Spark for big data scenarios.
* Build intelligent enterprise applications using Spark or SQL Server to train models over data lakes.
* Operationalize models in production databases for best performance.
* Stream data directly into enterprise data marts for real-time analytics.
* Explore data visually using interactive analysis and BI tools.

## Next steps

To learn more about the [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see the following resources:

- [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md)
- [Workshop: Microsoft [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] Architecture](https://github.com/Microsoft/sqlworkshops/tree/master/sqlserver2019bigdataclusters)
