---
title: Machine Learning on SQL Server Big Data Clusters
titleSuffix: SQL Server Big Data Clusters
description: Machine Learning guide for SQL Server Big Data Clusters.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 10/05/2021
ms.service: sql
ms.subservice: machine-learning-bdc
ms.topic: conceptual
---

# Machine Learning guide for SQL Server Big Data Clusters

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

This article explains how to use [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] for Machine Learning Scenarios.

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

## Introduction to Machine Learning in SQL Server Big Data Clusters

[!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] enables machine learning scenarios and solutions using different technology stacks: __SQL Server Machine Learning Services__ and __Apache Spark ML__.

[!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] offer Machine Learning capabilities inside the SQL Server engine, using the established SQL Server Machine Learning Services technology stack; enabling a high-performance, in-database Machine Learning inference and scoring scenarios.

For big data-based machine learning scenarios, the usage of HDFS for big data hosting and Apache Spark ML capabilities is more cost-effective, scalable, and powerful.

## Machine Learning Scenarios

The machine learning capabilities enable different applications and solutions such as: fraud detection, forecasting, churn, and general classification and regression tasks. Yet, it is important to use the best technology for a scenario.

|Aspect |SQL Server Machine Learning Services|Apache Spark ML|
|---------|---------|---------|
|Data placement|Leverages tabular data locality on SQL Server. Premium data tier.|Scalable Big Data data tier using HDFS; either unstructured, semi-structured, and structured data. |
|Best for|Low latency inference and scoring scenarios|1. Distributed batch training and scoring machine learning models on top of Big Data<br/>2. ETL sinks and large-scale data preparation and featurization for ML|
|Feeds|ML powered BI dashboards, reports, and applications. Low latency required|Batch scored data may be promoted to SQL Server to drive ML powered scenarios|
|Latency|Low latency required|Higher latency acceptable|
|Read more|[Run Python and R scripts with Machine Learning Services on SQL Server Big Data Clusters](machine-learning-services.md)|[Introducing Spark Machine Learning on SQL Server Big Data Clusters](spark-machine-learning.md)|

## Next steps

For more information, see [Introducing [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).
