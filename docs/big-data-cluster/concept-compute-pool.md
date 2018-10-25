---
title: What is a SQL big data clusters compute pool? | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 10/01/2018
ms.topic: conceptual
ms.prod: sql
---

# What is a SQL big data clusters compute pool?

This article describes the role of *SQL Server compute pools* in a SQL Server 2019 preview big data cluster. Compute pools provide scale-out computational resources for a big data cluster. The following sections describe the architecture and functionality of a compute pool.

## Compute pool architecture

A compute pool is made of one or more compute pods running in Kubernetes. The automated creation and management of these pods is coordinated by the [SQL Server master instance](concept-master-instance.md). Each pod contains a set of base services and an instance of the SQL Server database engine.

> [!NOTE]
> CTP 2.0 only supports a single compute pool per cluster.

## Scale-out groups

A compute pool can act as a PolyBase scale-out group for distributed queries over different data sources--such as HDFS, Oracle, MongoDB, or Terradata. By using compute pods in Kubernetes, big data clusters can automate creating and configuring compute pods for PolyBase scale-out groups.

## Next steps

To learn more about the SQL Server big data clusters, see the following overview:

- [What is SQL Server 2019 big data clusters?](big-data-cluster-overview.md)
