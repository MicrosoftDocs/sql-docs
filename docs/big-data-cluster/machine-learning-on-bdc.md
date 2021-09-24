---
title: Machine Learning on SQL Server Big Data Clusters | Microsoft Docs
titleSuffix: SQL Server Big Data Clusters
description: Machine Learning guide for SQL Server big data clusters.
author: DaniBunny
ms.author: dacoelho
ms.reviewer: wiassaf
ms.date: 09/23/2021
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Machine Learning guide for SQL Server big data clusters

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

This article explains how to use [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] for Machine Learning Scenarios.

## Introduction to Machine Learning in SQL Server Big Data Clusters

__SQL Server Big Data Clusters__ enables machine learning scenarios and solutions using different technology stacks: __SQL Server Machine Learning Services__ and __Apache Spark ML__.

It offers Machine Learning capabilities inside the SQL Server engine, using the established SQL Server Machine Learning Services technology stack; enabling high-performant in-database Machine Learning inference and scoring scenarios.

For big data based machine learning scenarios, the usage of HDFS for big data hosting and Apache Spark ML capabilities is a more cost-effective, scalable and powerful approach.

## Machine Learning Scenarios

The machine learning capabilities enable different applications and solutions such as: fraud detection, forecasting, churn, and general classification and regression tasks. Yet, it is important to use the best technology for a scenario.

| |SQL Server Machine Learning Services|Apache Spark ML|
|---------|---------|---------|
|Data placement|Leverages tabular data locality on SQL Server. Premium data tier.|Scalable Big Data data tier using HDFS; either unstructured, semi-structured and structured data. |
|Best for|Low latency inference and scoring scenarios|1. Distributed batch training and scoring machine learning models on top of Big Data<br/>2. ETL sinks and large scale data preparation and featurization for ML|
|Feeds|ML powered BI dashboards, reports and applications. Low latency required|Batch scored data may be promoted to SQL Server to drive ML powered scenarios|
|Latency|Low latency required|Higher latency acceptable|
|Read more|[Run Python and R scripts with Machine Learning Services on SQL Server Big Data Clusters](machine-learning-services.md)|[Introducing Spark Machine Learning on SQL Server big data clusters](spark-machine-learning.md)|

## Next steps

For more information about SQL Server big data cluster, see [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).
