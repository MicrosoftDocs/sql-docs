---
title: SQL Server Big Data Clusters runtime for Apache Spark Guide
titleSuffix: SQL Server Big Data Clusters
description: SQL Server Big Data Clusters runtime for Apache Spark Guide
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 12/14/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# SQL Server Big Data Clusters runtime for Apache Spark Guide

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

## Introducing the SQL Server Big Data Clusters runtime for Apache Spark

The __SQL Server Big Data Clusters runtime for Apache Spark__ is a standardized specification for Apache Spark that enables seamless interoperability between distributions. This Spark runtime is a consistent, versioned block of programming language distributions, engine optimizations, core libraries, and packages.

Every product that uses this runtime specification, will contain the same versions of Apache Spark Core, PySpark, Scala Spark, Spark.R, sparklyr, and .NET for Spark.

All the distributed packages and libraries are also the same. One of the primary goals for the specification is to provide a first-class experience for Data Engineers and Data Scientists by providing a constantly curated and updated list of packages and connectors, out-of-the-box.

Benefits of the SQL Server Big Data Clusters runtime for Apache Spark:

1. Spark engine optimizations and features available on all products and services
1. Established release cadence
1. Seamless interoperability between Spark products and services
1. Curated packages for Data Engineers and Data Scientists
1. Consistent package management story

## Release cadence and naming standards

The SQL Server Big Data Clusters runtime for Apache Spark specification defines the following:

The runtime naming standard is as follows: 

"__PRODUCT_NAME__.__SPARK_MAJOR_VERSION__.__CALENDAR_YEAR__.__RELEASE#__"

Example is "BDC.3.2021.1".

__RELEASE#__ is a sequential semantic number. It is not bound to months or any other standard. Once a runtime release is created, it is immutable. Each release of SQL Server Big Data Clusters ships with one version of the runtime.

## What's in the current runtime release?

The [SQL Server Big Data Clusters platform release notes](release-notes-big-data-cluster.md) have the runtime name and complete contents of the release.

## Next steps

For more information, see [Introducing [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).
