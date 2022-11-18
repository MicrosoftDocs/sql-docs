---
title: Use curl to load data into HDFS
titleSuffix: SQL Server Big Data Clusters
description: Use curl to load data into HDFS on SQL Server 2019 big data cluster.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: wiassaf
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# Use curl to load data into HDFS on [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

This article explains how to use **curl** to load data into HDFS on [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)].

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

## <a id="prereqs"></a> Prerequisites

- [Load sample data into your big data cluster](tutorial-load-sample-data.md)

## Obtain the service external IP

WebHDFS is started when deployment is completed, and its access goes through Knox. The Knox endpoint is exposed through a Kubernetes service called **gateway-svc-external**.  To create the necessary WebHDFS URL to upload/download files, you need the **gateway-svc-external** service external IP address and the name of your big data cluster. You can get the **gateway-svc-external** service external IP address by running the following command:

```terminal
kubectl get service gateway-svc-external -n <big data cluster name> -o json | jq -r .status.loadBalancer.ingress[0].ip
```

> [!NOTE]
> The `<big data cluster name>` here is the name of the cluster that you specified in the deployment configuration file. The default name is `mssql-cluster`.

## Construct the URL to access WebHDFS

Now, you can construct the URL to access the WebHDFS as follows:

`https://<gateway-svc-external service external IP address>:30443/gateway/default/webhdfs/v1/`

For example:

`https://13.66.190.205:30443/gateway/default/webhdfs/v1/`

## Authentication with Active Directory

For deployments with Active Directory, use the authentication parameter with `curl` with Negotiate authentication. 

To use `curl` with Active Directory authentication, run this command:

```
kinit <username>
```

The command generates a Kerberos token for `curl` to use. The commands demonstrated in the next sections specify the `--anyauth` parameter for `curl`. For URLs that require Negotiate authentication, `curl` automatically detects and uses the generated Kerberos token instead of username and password to authenticate to the URLs.

## List a file

To list file under **hdfs:///product_review_data**, use the following curl command:

```terminal
curl -i -k --anyauth -u root:<AZDATA_PASSWORD> -X GET 'https://<gateway-svc-external IP external address>:30443/gateway/default/webhdfs/v1/product_review_data/?op=liststatus'
```

[!INCLUDE [big-data-cluster-root-user](../includes/big-data-cluster-root-user.md)]

For endpoints that do not use root, use the following curl command:

```terminal
curl -i -k --anyauth -u <AZDATA_USERNAME>:<AZDATA_PASSWORD> -X GET 'https://<gateway-svc-external IP external address>:30443/gateway/default/webhdfs/v1/product_review_data/?op=liststatus'
```

## Put a local file into HDFS

To put a new file **test.csv** from local directory to product_review_data directory, use the following curl command (the **Content-Type** parameter is required):

```terminal
curl -i -L -k --anyauth -u root:<AZDATA_PASSWORD> -X PUT 'https://<gateway-svc-external IP external address>:30443/gateway/default/webhdfs/v1/product_review_data/test.csv?op=create' -H 'Content-Type: application/octet-stream' -T 'test.csv'
```

[!INCLUDE [big-data-cluster-root-user](../includes/big-data-cluster-root-user.md)]

For endpoints that do not use root, use the following curl command:

```terminal
curl -i -L -k --anyauth -u <AZDATA_USERNAME>:<AZDATA_PASSWORD> -X PUT 'https://<gateway-svc-external IP external address>:30443/gateway/default/webhdfs/v1/product_review_data/test.csv?op=create' -H 'Content-Type: application/octet-stream' -T 'test.csv'
```

## Create a directory

To create a directory **test** under `hdfs:///`, use the following command:

```terminal
curl -i -L -k --anyauth -u root:<AZDATA_PASSWORD> -X PUT 'https://<gateway-svc-external IP external address>:30443/gateway/default/webhdfs/v1/test?op=MKDIRS'
```

[!INCLUDE [big-data-cluster-root-user](../includes/big-data-cluster-root-user.md)]
For endpoints that do not use root, use the following curl command:

```terminal
curl -i -L -k --anyauth -u <AZDATA_USERNAME>:<AZDATA_PASSWORD> -X PUT 'https://<gateway-svc-external IP external address>:30443/gateway/default/webhdfs/v1/test?op=MKDIRS'
```

## Next steps

For more information, see [Introducing [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).
