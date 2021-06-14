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

This guide demonstrates how to use Encryption at Rest capabilities of SQL Server Big Data Clusters to encrypt HDFS folders using Encryption Zones. It also covers HDFS key management tasks.

Note that there is a default encryption zone mounted at __```/securelake```__ ready to be used. It was created with a system generated 256-bit key named __securelakekey__. This key can be used to create additional encryption zones.

## <a id="prereqs"></a> Prerequisites

- [SQL Server Big Data Cluster CU8+](release-notes-big-data-cluster.md) with [Active Directory](active-directory-prerequisites.md) Integration.
- User with cluster administrative privileges. For more information, see [Manage big data cluster access in Active Directory mode](manage-user-access.md).
- [!INCLUDE[azdata](../includes/azure-data-cli-azdata.md)] configured and logged into the cluster in AD mode.

## Create an encryption zone using the provided system managed key

1. Create a HDFS folder

   ```console
   azdata bdc hdfs mkdir -p /user/zone/folder
   ```

1. Issue the encryption zone create command to encrypt the folder using the __securelakekey__ key.

   ```console
   azdata bdc hdfs encryption-zone create --path /user/zone/folder --keyname securelakekey
   ```

## Manage encryption zones when using external providers

For more information on the way key versions are used on SQL Server Big Data Clusters encryption at rest, see [Key Versions in [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-key-versions.md). The section "Main key rotation for HDFS" contains an end-to-end example on how to manage encryption zones when using external key providers.

## Create a custom new key and encryption zone

1. Use the following pattern to create a 256-bit key.

   ```console
   azdata bdc hdfs key create -n mydatalakekey
   ```

1. Create and encrypt a new HDFS path using the user key.

   ```console
   azdata bdc hdfs encryption-zone create --path /user/mydatalake --keyname mydatalakekey
   ```

## HDFS Key rotation and encryption zone re-encryption

1. This creates a new version of the __securelakekey__ with new key material.

   ```console
   azdata hdfs bdc key roll -n securelakekey
   ```

1. Re-encrypt the encryption zone associated with the key above

   ```console
   azdata bdc hdfs encryption-zone reencrypt --path /securelake --action start
   ```

## HDFS Key and encryption zone monitoring

1. To monitor the status of a encryption zone re-encryption 

   ```console
   azdata bdc hdfs encryption-zone status
   ```

1. To get the encryption information about a file in an encryption zone

   ```console
   azdata bdc hdfs encryption-zone get-file-encryption-info --path /securelake/data.csv
   ```

1. Listing all encryption zones

   ```console
   azdata bdc hdfs encryption-zone list
   ```

1. To list all the available keys for HDFS

   ```console
   azdata bdc hdfs key list
   ```

1. In order to create a custom key for HDFS encryption. Sizes possible are 128, 192 256. Default is 256

   ```console
   azdata hdfs key create --name key1 --size 256
   ```

## Next steps

Use azdata with Big Data Clusters, see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).

Use azdata with [Azure Arc enabled data services](/azure/azure-arc/data/)
