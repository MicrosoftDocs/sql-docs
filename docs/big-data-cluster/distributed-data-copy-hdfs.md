---
title: HDFS distributed data copy
titleSuffix: SQL Server Big Data Clusters
description: Use azdata distcp to perform data movement between SQL Server big data clusters.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 12/01/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# Use azdata distcp to perform data movement between SQL Server big data clusters

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

This article explains how to use **azdata** to perform high performant distributed data copies between [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)].

## <a id="prereqs"></a> Prerequisites

- [Azure Data Studio](deploy-big-data-tools.md)
- [azdata](deploy-big-data-tools.md) version 20.3.8 or superior
- Two SQL Server big data cluster CU13 or superior

## Introduction to distributed data copies on SQL Server Big Data Clusters

Hadoop HDFS DistCP is a command-line tool used to perform distributed parallel copies of files and folders from one HDFS cluster to another. Distributed parallel copying enables fast transfer of Data Lake scale files and folders between two different clusters, enabling migrations, the creation of segmented environments, high-availability, and disaster recovery scenarios.

Hadoop HDFS DistCP uses an internal MapReduce job to expand a list of files and directories into input to multiple map tasks, each of which will copy a partition of the files specified in the source list to the destination. This allows multiple data nodes in the source cluster to send data directly to multiple data nodes in the destination clusters, creating a truly distributed parallel copy scenario.

On [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] CU13 and above, the distributed copy functionality is integrated into the product and is exposed through the [azdata bdc hdfs distcp](../azdata/reference/reference-azdata-bdc-hdfs.md) command. The command is an asynchronous operation, it returns immediately while the copy job executes in the background. Monitor the copy job using either `azdata` or the provided monitoring user interfaces.

Only [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] sources and destinations are supported.

Clusters may be deployed in both Active Directory enabled mode or basic security modes. Copies may be performed between any combination of security modes. For Active Directory enabled clusters, it is required that they are in the same domain.

In this guide we will cover the following data copy scenarios:

- Active Directory enabled cluster to Active Directory enabled cluster
- Basic security cluster to Active Directory enabled cluster
- Basic security cluster to basic security cluster

## Required steps to all scenarios

Certificates are required to create a trusted relationship between source and destination clusters. These steps are required only once per source/destination cluster combination.

> [!IMPORTANT]
> If a SQL Server big data cluster with basic authentication (non-AD) is __directly upgraded to CU13 or above__, the distcp functionality requires activation. (This doesn't affect new CU13+ cluster deployments with basic authentication.)  
>
> To enable the distcp functionality in this scenario execute the following additional steps once:
>
> ```bash
> kubectl -n $CLUSTER_NAME exec -it nmnode-0-0 -- bash
> export HADOOP_USER_NAME=hdfs
> hadoop fs -mkdir -p /tmp/hadoop-yarn/staging/history
> hadoop fs -chown yarn /tmp/hadoop-yarn
> hadoop fs -chown yarn /tmp/hadoop-yarn/staging
> hadoop fs -chown yarn /tmp/hadoop-yarn/staging/history
> hadoop fs -chmod 733 /tmp/hadoop-yarn
> hadoop fs -chmod 733 /tmp/hadoop-yarn/staging
> hadoop fs -chmod 733 /tmp/hadoop-yarn/staging/history
> ```

The required notebooks in the next steps are part of the Operational notebooks for [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)]. For more information how to install and use the notebooks, see [Operational notebooks](cluster-manage-notebooks.md)

### Step 1 - Certificate creation and installation

Connect to your __source cluster__ using Azure Data Studio. This is where the files will be copied from. Perform the following steps:

1. Create new cluster root certificate in source cluster using notebook ``cer001-create-root-ca.ipynb``
1. Download the new cluster root certificate to your machine using ``cer002-download-existing-root-ca.ipynb``
1. Install the new root certificate to the source cluster using ``cer005-install-existing-root-ca.ipynb``. This step will take 10-15 minutes.
1. Generate new knox certificate on the source cluster ``cer021-create-knox-cert.ipynb``
1. Sign with the new knox certificate with the new root certificate using ``cer031-sign-knox-generated-cert.ipynb``
1. Install the new knox certificate on the source cluster using  ``cer041-install-knox-cert.ipynb``

   > [!IMPORTANT]
   > The certificate generation commands will create the root CA file (``cluster-ca-certificate.crt``) and Knox certificate file (``cacert.pem``) in
   > ``"C:\Users\{Username}\AppData\Local\Temp\1\mssql-cluster-root-ca\"`` on Windows and 
   > ``"/tmp/mssql-cluster-root-ca/`` on Unix.

### Step 2 - Certificate installation on destination

Connect to your __destination cluster__ using Azure Data Studio. This is where the files will be copied to.
Perform the following steps:

   > [!WARNING]
   > If you're using Azure Data Studio on different machines to perform this tasks, copy the ``cluster-ca-certificate.crt`` and ``cacert.pem`` files generated on Step 1 to the right locations on the other machine before running the notebook in the next step.

1. Install the new root certificate from the source cluster to the destination cluster using ``cer005-install-existing-root-ca.ipynb``. This step will take 10-15 minutes.

### Step 3 - Keytab file creation

You need to create a keytab file if __any__ of the clusters is Active Directory enabled. The file will perform authentication to enable the copy to take place.

Create the keytab file using `ktutil`. An example follows. Make sure to define or replace the environment variables ``DOMAIN_SERVICE_ACCOUNT_USERNAME``, ``DOMAIN_SERVICE_ACCOUNT_PASSWORD``, and ``REALM`` with the appropriate values.

```bash
ktutil
> add_entry -password -p $DOMAIN_SERVICE_ACCOUNT_USERNAME@$REALM -k 1 -e arcfour-hmac-md5
$DOMAIN_SERVICE_ACCOUNT_PASSWORD
> add_entry -password -p $DOMAIN_SERVICE_ACCOUNT_USERNAME@$REALM -k 1 -e aes256-cts
$DOMAIN_SERVICE_ACCOUNT_PASSWORD
> add_entry -password -p $DOMAIN_SERVICE_ACCOUNT_USERNAME@$REALM -k 1 -e aes256-cts-hmac-sha1-96
$DOMAIN_SERVICE_ACCOUNT_PASSWORD
> wkt /tmp/$DOMAIN_SERVICE_ACCOUNT_USERNAME.keytab
> exit
```

### Step 4 - Copy the keytab to the HDFS location

This step is only required if __any__ of the clusters is Active Directory enabled.

Make sure to define or replace the environment variables ``DOMAIN_SERVICE_ACCOUNT_USERNAME`` with the appropriate value.

Upload the keytab to destination (secure cluster):

```bash
azdata bdc hdfs mkdir -p /user/$DOMAIN_SERVICE_ACCOUNT_USERNAME
azdata bdc hdfs cp -f /tmp/$DOMAIN_SERVICE_ACCOUNT_USERNAME.keytab -t /user/$DOMAIN_SERVICE_ACCOUNT_USERNAME/$DOMAIN_SERVICE_ACCOUNT_USERNAME.keytab
```

## Data copy scenarios

### Scenario 1 - Active Directory enabled cluster to Active Directory enabled cluster

1. Export the required environment variable ``DISTCP_KRB5KEYTAB``:

    ```bash
    export DISTCP_KRB5KEYTAB=/user/$DOMAIN_SERVICE_ACCOUNT_USERNAME/$DOMAIN_SERVICE_ACCOUNT_USERNAME.keytab
    ```

2. Either define or replace ``CLUSTER_CONTROLLER``, ``DESTINATION_CLUSTER_NAMESPACE``, and ``PRINCIPAL`` variables with appropriate vales. Then use `azdata` to authenticate to destination cluster:

    ```bash
    azdata login --auth ad --endpoint $CLUSTER_CONTROLLER --namespace $DESTINATION_CLUSTER_NAMESPACE --principal $PRINCIPAL
    ```

3. Run the distributed data copy:

    ```bash
    azdata bdc hdfs distcp submit -f https://knox.$CLUSTER_NAME.$DOMAIN:30443/file.txt -t  /file.txt
    ```

### Scenario 2 - Basic security cluster to Active Directory enabled cluster

1. Export the required environment variable ``DISTCP_KRB5KEYTAB``, ``DISTCP_AZDATA_USERNAME``, and ``DISTCP_AZDATA_PASSWORD``:

    ```bash
    export DISTCP_KRB5KEYTAB=/user/$DOMAIN_SERVICE_ACCOUNT_USERNAME/$DOMAIN_SERVICE_ACCOUNT_USERNAME.keytab
    export DISTCP_AZDATA_USERNAME=<your basic security bdc username> 
    export DISTCP_AZDATA_PASSWORD=<your basic security bdc password>
    ```

2. Either define or replace ``CLUSTER_CONTROLLER``, ``DESTINATION_CLUSTER_NAMESPACE``, and ``PRINCIPAL`` variables with appropriate vales. Then use ``azdata`` to authenticate to destination cluster:

    ```bash
    azdata login --auth ad --endpoint $CLUSTER_CONTROLLER --namespace $DESTINATION_CLUSTER_NAMESPACE --principal $PRINCIPAL
    ```

3. Run the distributed data copy

    ```bash
    azdata bdc hdfs distcp submit --from-path https://$SOURCE_CLUSTER_IP:30443/file.txt --to-path /file.txt
    ```

### Scenario 3 - Basic security cluster to basic security cluster

1. Export the required environment variable ``DISTCP_AZDATA_USERNAME`` and ``DISTCP_AZDATA_PASSWORD``:

    ```bash
    export DISTCP_AZDATA_USERNAME=<your basic security bdc username> 
    export DISTCP_AZDATA_PASSWORD=<your basic security bdc password>
    ```

2. Use ``azdata`` to authenticate to destination cluster

3. Run the distributed data copy

    ```bash
    azdata bdc hdfs distcp submit --from-path https://$SOURCE_CLUSTER_IP:30443/file.txt --to-path /file.txt
    ```

## Monitoring the distributed copy job

The ``azdata bdc hdfs distcp submit`` command is an asynchronous operation, it returns immediately while the copy job is running on the background. Monitor the copy job using either ``azdata`` or the provided monitoring user interfaces.

### List all distributed copy jobs

```bash
azdata bdc hdfs distcp list
```

Take note of the `job-id` of job you and to track. All other commands depend on it.

### Get the state of a distributed copy job

```bash
azdata bdc hdfs distcp state [--job-id | -i] <JOB_ID>
```

### Get the complete status of a distributed copy job

```bash
azdata bdc hdfs distcp status [--job-id | -i] <JOB_ID>
```

### Get the logs of a distributed copy job

```bash
azdata bdc hdfs distcp log [--job-id | -i] <JOB_ID>
```

## Distributed copy hints

1. In order to copy entire directories and their contents, end the path argument with a directory, without the '/'. The following example copies the entire ``sourceDirectories`` directory to the root HDFS destination:

    ```bash
    azdata bdc hdfs distcp submit --from-path https://$SOURCE_CLUSTER_IP:30443/sourceDirectories --to-path /
    ```

1. The command may contain options, supporting the `--overwrite`, `--preserve`, `--update`, and `--delete` modifiers. The modifier should be placed just after the submit keyword, like below:

    ```bash
    azdata bdc hdfs distcp submit {options} --from-path https://$SOURCE_CLUSTER_IP:30443/sourceDirectories --to-path /
    ```

## Next steps

For more information, see [Introducing [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).

For complete reference of the new command, see [azdata bdc hdfs distcp](../azdata/reference/reference-azdata-bdc-hdfs.md).
