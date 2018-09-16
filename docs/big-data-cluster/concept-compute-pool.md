---
title: What is a SQL Big Data Clusters compute pool? | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 09/24/2018
ms.topic: conceptual
ms.prod: sql
---

# What is a SQL Big Data Clusters compute pool?

This article describes the role of *SQL Server compute pools* in a SQL Server 2019 preview Big Data cluster. Compute pools provide scale-out computational resources for a Big Data cluster. The following sections describe the architecture and functionality of a compute pool.

## Compute pool architecture

A compute pool is made of one or more compute pods running in Kubernetes. The automated creation and management of these pods is coordinated by the [SQL Server master instance](concept-master-instance.md). Each pod contains a set of base services and an instance of the SQL Server database engine.

> [!NOTE]
> CTP 2.0 only supports a single compute pool per cluster.

## Scale-out groups

A compute pool can act as a PolyBase scale-out group for distributed queries over different data sources--such as HDFS, Oracle, MongoDB, or Terradata. By using compute pods in Kubernetes, Big Data Clusters can automate creating and configuring compute pods for PolyBase scale-out groups.

## Next steps

To learn more about the SQL Server Big Data Clusters, see the following articles:

- [What is SQL Server 2019 Big Data Clusters?](big-data-cluster-overview.md)
- [Quickstart: Deploy SQL Server Big Data Cluster on Kubernetes](quickstart-big-data-cluster-deploy.md)
