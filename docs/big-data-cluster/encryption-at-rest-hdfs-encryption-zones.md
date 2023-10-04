---
title: SQL Server Big Data Clusters HDFS encryption zones usage
titleSuffix: SQL Server Big Data Clusters
description: Learn how to use encryption at rest capabilities with the SQL Server HDFS encryption zones feature of Big Data Clusters and about key management tasks.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 06/14/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: tutorial
ms.custom: kr2b-contr-experiment
---

# HDFS encryption zones usage guide in [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

This article shows how to use the encryption at rest capabilities of SQL Server Big Data Clusters to encrypt HDFS folders using Encryption Zones. It also describes HDFS key management tasks.

A default encryption zone, at */securelake*, is ready to be used. It was created with a system generated 256-bit key named `securelakekey`. This key can be used to create other encryption zones.

## <a id="prereqs"></a> Prerequisites

- [SQL Server Big Data Cluster CU8+](release-notes-big-data-cluster.md) with [Active Directory](active-directory-prerequisites.md) Integration.
- SQL Server Big Data Clusters user with Kubernetes administrative privileges, a member of the clusterAdmins role. For more information, see [Manage big data cluster access in Active Directory mode](manage-user-access.md).
- [!INCLUDE[azdata](../includes/azure-data-cli-azdata.md)] configured and logged into the cluster in AD mode.

## Create an encryption zone using the provided system managed key

1. Create your HDFS folder by using this [azdata](../azdata/reference/reference-azdata.md) command:

   ```console
   azdata bdc hdfs mkdir --path /user/zone/folder
   ```

1. Issue the encryption zone create command to encrypt the folder using the `securelakekey` key.

   ```console
   azdata bdc hdfs encryption-zone create --path /user/zone/folder --keyname securelakekey
   ```

## Manage encryption zones when using external providers

For more information on the way key versions are used on SQL Server Big Data Clusters encryption at rest, see [Main key rotation for HDFS](big-data-cluster-key-versions.md#main-key-rotation-for-hdfs) for an end-to-end example of how to manage encryption zones when using external key providers.

## Create a custom new key and encryption zone

1. Use the following pattern to create a 256-bit key.

   ```console
   azdata bdc hdfs key create --name mydatalakekey
   ```

1. Create and encrypt a new HDFS path using the user key.

   ```console
   azdata bdc hdfs encryption-zone create --path /user/mydatalake --keyname mydatalakekey
   ```

## HDFS Key rotation and encryption zone re-encryption

1. This approach creates a new version of the `securelakekey` with new key material.

   ```console
   azdata hdfs bdc key roll --name securelakekey
   ```

1. Re-encrypt the encryption zone associated with the key above.

   ```console
   azdata bdc hdfs encryption-zone reencrypt --path /securelake --action start
   ```

## HDFS Key and encryption zone monitoring

- To monitor the status of an encryption zone re-encryption, use this command:

  ```console
  azdata bdc hdfs encryption-zone status
  ```

- To get the encryption information about a file in an encryption zone, use this command:

  ```console
  azdata bdc hdfs encryption-zone get-file-encryption-info --path /securelake/data.csv
  ```

- To list all encryption zones, use this command:

  ```console
  azdata bdc hdfs encryption-zone list
  ```

- To list all the available keys for HDFS, use this command:

  ```console
  azdata bdc hdfs key list
  ```

- To create a custom key for HDFS encryption, use this command:

  ```console
  azdata hdfs key create --name key1 --size 256
  ```

  Possible sizes are 128, 192 256. The default is 256.

## Next steps

Use `azdata` with Big Data Clusters, see [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-overview.md).

To use an external key provider for encryption at rest, see [External Key Providers in [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](encryption-at-rest-external-provider.md).

- [SQL Server Big Data Clusters Transparent Data Encryption (TDE) at rest usage guide](encryption-at-rest-sql-server-tde.md)
- [Key Versions in [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-key-versions.md)
