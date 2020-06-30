---
title: What are compute pools?
titleSuffix: SQL Server big data clusters
description: This article describes the compute pool in a SQL Server 2019 big data cluster.
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab 
ms.date: 11/04/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# What are compute pools in a SQL Server big data cluster?

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

This article describes the role of *SQL Server compute pools* in a SQL Server big data cluster. Compute pools provide scale-out computational resources for a big data cluster. The following sections describe the architecture and functionality of a compute pool.

You can also watch this 5-minute video for an introduction into compute pools:

> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Overview-Big-Data-Cluster-Compute-Pool/player?WT.mc_id=dataexposed-c9-niner]


## Compute pool architecture

A compute pool is made of one or more compute pods running in Kubernetes. The automated creation and management of these pods is coordinated by the [SQL Server master instance](concept-master-instance.md). Each pod contains a set of base services and an instance of the SQL Server database engine.

## Scale-out groups

A compute pool can act as a PolyBase scale-out group for distributed queries over different data sources--such as HDFS, Oracle, MongoDB, or Teradata. By using compute pods in Kubernetes, big data clusters can automate creating and configuring compute pods for PolyBase scale-out groups.

## Next steps

To learn more about the [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see the following resources:

- [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md)
- [Workshop: Microsoft [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] Architecture](https://github.com/Microsoft/sqlworkshops/tree/master/sqlserver2019bigdataclusters)
