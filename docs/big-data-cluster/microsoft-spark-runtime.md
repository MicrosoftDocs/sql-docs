---
title: Microsoft Spark Runtime Guide
titleSuffix: SQL Server Big Data Clusters
description: Microsoft Spark Runtime Guide
author: DaniBunny
ms.author: dacoelho
ms.reviewer: wiassaf
ms.date: 09/23/2021
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Microsoft Spark Runtime Guide

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

## Introducing the Microsoft Spark Runtime

The __Microsoft Spark Runtime__ is a standardized distribution specification for Apache Spark that enables seamless interoperability and shared experiences between Spark-based Microsoft data product offerings.

It is a consistent versioned block of programming language distributions, engine optimizations, core libraries and packages.

Every product that uses this Spark runtime specification, will contain the same versions of Apache Spark Core, PySpark, Scala Spark, Spark.R, sparklyr and .NET for Spark.

All the distributed packages and libraries are also the same. One of the primary goals for the specification is to provide a first class experience for Data Engineers and Data Scientists, by providing an constantly curated and updated list of packages and connectors out-of-the-box.

Benefits of the Microsoft Spark Runtime:

1. Spark engine optimizations and features available on all products and services
1. Established release cadence
1. Seamless interoperability between Spark products and services
1. Curated packages for Data Engineers and Data Scientists
1. Consistent package management story

## The runtime and product offerings

__SQL Server Big Data Clusters__ and [Azure Synapse Analytics](https://docs.microsoft.com/azure/synapse-analytics/spark/apache-spark-version-support) implement the Microsoft Spark Runtime specification.

This means that under the same specific runtime release version, for example, you can migrate a PySpark notebook between SQL Server Big Data Cluster and your Azure Synapse workspace Spark Pool with minor modifications. The PySpark version, Spark core, all libraries will be the same. Modifications in the code might be required regarding hard-coded file and folder paths. This applies to languages that are available on both offerings, of course.

## Release cadence and naming standards

The Microsoft Spark Runtime specification defines the following:

* The naming standard is "Microsoft Spark __SPARK_MAJOR_VERSION__ Runtime __CALENDAR_YEAR__.__RELEASE#__".
* There may be many releases per calendar year. __RELEASE#__ is a sequential number (not bound to months or other standard). The expected cadence is at least twice a year.
* Once a runtime releases is created, it is immutable. All components versions are locked in.
* A product, such as SQL Server Big Data Clusters, will pick which runtime release it will ship in an update.
* Each product offering may have a different release cadences. For example Azure Synapse may ship runtime releases more frequently.

## SQL Server Big Data Cluster and its runtime implementation

Regarding how SQL Server Big Data Cluster releases the runtime:

1. SQL Server Big Data Cluster will ship two runtime releases per year within its cumulative updates release cycle. It currently has six cumulative updates per calendar year, for example from CU14 to CU19; only two of those updates will contain a newer runtime releases in order to provide a more stable cadence.
1. Standardizes in at least 6 months between refreshing runtime releases.
1. A SQL Server big data cluster is tied to one runtime distribution at a specific cumulative update. It is not possible to run multiple runtime releases in the same cluster.

## Whats in the current runtime release?

The [SQL Server Big Data Clusters release notes](release-notes-big-data-cluster.md) have the runtime name and complete contents of the release.

## Next steps

For more information about SQL Server big data cluster, see [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).
