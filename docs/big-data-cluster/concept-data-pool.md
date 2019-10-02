---
title: What are data pools?
titleSuffix: SQL Server big data clusters
description: This article describes the data pool in a SQL Server 2019 big data cluster.
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 08/21/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# What are data pools in a SQL Server big data cluster?

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article describes the role of *SQL Server data pools* in a [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]. The following sections describe the architecture and functionality of a SQL data pool.

## Data pool architecture

A data pool consists of one or more SQL Server data pool instances. SQL data pool instances provide persistent SQL Server storage for the cluster. A data pool is used to ingest data from SQL queries or Spark jobs. To provide better performance across large data sets, data in a data pool is distributed into shards across the member SQL data pool instances.

## Scale-out data marts

Data pools enable the creation of scale-out data marts, where external data from multiple sources is ingested into the data pool. Because data is distributed across data pool instances, parallel queries against the curated data are more efficient.

![Scale-out data mart](media/concept-data-pool/data-virtualization-improvements.png)

## Next steps

To learn more about the [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see the following resources:

- [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md)
- [Workshop: Microsoft [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] Architecture](https://github.com/Microsoft/sqlworkshops/tree/master/sqlserver2019bigdataclusters)
