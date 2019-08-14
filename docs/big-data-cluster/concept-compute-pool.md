---
title: What are compute pools?
titleSuffix: SQL Server big data clusters
description: This article describes the compute pool in a [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)].
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab 
ms.date: 02/28/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# What are compute pools in a SQL Server big data cluster?

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article describes the role of *SQL Server compute pools* in a SQL Server 2019 preview big data cluster. Compute pools provide scale-out computational resources for a big data cluster. The following sections describe the architecture and functionality of a compute pool.

## Compute pool architecture

A compute pool is made of one or more compute pods running in Kubernetes. The automated creation and management of these pods is coordinated by the [SQL Server master instance](concept-master-instance.md). Each pod contains a set of base services and an instance of the SQL Server database engine.

## Scale-out groups

A compute pool can act as a PolyBase scale-out group for distributed queries over different data sources--such as HDFS, Oracle, MongoDB, or Terradata. By using compute pods in Kubernetes, big data clusters can automate creating and configuring compute pods for PolyBase scale-out groups.

## Next steps

To learn more about the [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see the following resources:

- [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md)
- [Workshop: Microsoft [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] Architecture](https://github.com/Microsoft/sqlworkshops/tree/master/sqlserver2019bigdataclusters)
