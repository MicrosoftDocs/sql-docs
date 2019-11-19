---
title: Use curl to load data into HDFS | Microsoft Docs
titleSuffix: SQL Server big data clusters
description: Use curl to load data into HDFS on [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)].
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mihaelab 
ms.date: 08/21/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Use curl to load data into HDFS on [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article explains how to use **curl** to load data into HDFS on [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)].

## <a id="prereqs"></a> Prerequisites

- [Load sample data into your big data cluster](tutorial-load-sample-data.md)

## Obtain the service external IP

WebHDFS is started when deployment is completed, and its access goes through Knox. The Knox endpoint is exposed through a Kubernetes service called **gateway-svc-external**.  To create the necessary WebHDFS URL to upload/download files, you need the **gateway-svc-external** service external IP address and the name of your big data cluster. You can get the **gateway-svc-external** service external IP address by running the following command:

```bash
kubectl get service gateway-svc-external -n <big data cluster name> -o json | jq -r .status.loadBalancer.ingress[0].ip
```

> [!NOTE]
> The `<big data cluster name>` here is the name of the cluster that you specified in the deployment configuration file. The default name is `mssql-cluster`.

## Construct the URL to access WebHDFS

Now, you can construct the URL to access the WebHDFS as follows:

`https://<gateway-svc-external service external IP address>:30443/gateway/default/webhdfs/v1/`

For example:

`https://13.66.190.205:30443/gateway/default/webhdfs/v1/`

## List a file

To list file under **hdfs:///product_review_data**, use the following curl command:

```bash
curl -i -k -u root:root-password -X GET 'https://<gateway-svc-external IP external address>:30443/gateway/default/webhdfs/v1/product_review_data/?op=liststatus'
```

## Put a local file into HDFS

To put a new file **test.csv** from local directory to product_review_data directory, use the following curl command (the **Content-Type** parameter is required):

```bash
curl -i -L -k -u root:root-password -X PUT 'https://<gateway-svc-external IP external address>:30443/gateway/default/webhdfs/v1/product_review_data/test.csv?op=create' -H 'Content-Type: application/octet-stream' -T 'test.csv'
```

## Create a directory

To create a directory **test** under `hdfs:///`, use the following command:

```bash
curl -i -L -k -u root:root-password -X PUT 'https://<gateway-svc-external IP external address>:30443/gateway/default/webhdfs/v1/test?op=MKDIRS'
```

## Next steps

For more information about SQL Server big data cluster, see [What is SQL Server big data cluster?](big-data-cluster-overview.md).
