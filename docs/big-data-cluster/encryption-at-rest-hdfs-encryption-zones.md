---
title: SQL Server Big Data Clusters HDFS encryption zones usage guide
titleSuffix: SQL Server big data clusters
description: This article show how to use SQL Server HDFS encryption zones feature of BDC
author: DaniBunny
ms.author: dacoelho
ms.reviewer: mihaelab
ms.date: 10/19/2020
ms.topic: tutorial
ms.prod: sql
ms.technology: big-data-cluster
---

# SQL Server Big Data Clusters HDFS Encryption Zones usage guide

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

This guide demonstrates how to use Encryption at Rest capabilities of SQL Server Big Data Clusters to encrypt HDFS folders using Encryption Zones.

Note that there is already a default encryption zone mounted at __```/securelake```__ ready to be used. It was created with a system generated 256-bit key named __securelakekey__. This key can be used to create additional encryption zones.

## <a id="prereqs"></a> Prerequisites

- [SQL Server Big Data Cluster CU8+](release-notes-big-data-cluster.md) with Active Directory Integration.
- User with administrative privileges.

## Login into the name node

Use [Active Directory connection instructions](active-directory-connect.md) to perform cluster login. Log into the namenode (nmnode-0-0) to issue key and encryption zones commands.

   ```console
   kubectl exec -it -c hadoop -n <cluster_namespace> nmnode-0-0 -- /bin/bash
   ```

## Create an encryption zone using the provided system managed key

1. Create a HDFS folder

   ```console
   hdfs dfs -mkdir -p /user/zone/folder
   ```

1. Issue the encryption zone create command to encrypt the folder using the __securelakekey__ key.

   ```console
   hdfs crypto -createZone -keyName securelakekey -path /user/zone/folder
   ```

## Create a custom new key and encryption zone

1. Use the following pattern to create a 256-bit key.

   ```console
   kinit hdfs
   hadoop key create mydatalakekey -size 256
   ```

1. Create and encrypt a new HDFS path using the user key.

   ```console
   hdfs dfs -mkdir -p /user/mydatalake
   hdfs crypto -createZone -keyName mydatalakekey -path /user/mydatalake
   ```
