---
title: View cluster status
titleSuffix: SQL Server big data clusters
description: This article explains how to view the status of a big data cluster using Azure Data Studio, notebooks, and azdata commands.
author: yualan
ms.author: alayu
ms.reviewer: mikeray
ms.date: 08/21/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# How to view the status of a big data cluster

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article describes how to access the service endpoints and view the status of a SQL Server big data cluster (preview). You can use both Azure Data Studio and **azdata**, and this article covers both techniques.

## <a id="datastudio"></a> Use Azure Data Studio

After downloading the latest **insiders build** of [Azure Data Studio](https://aka.ms/getazuredatastudio), you can view service endpoints and the status of a big data cluster with the SQL Server big data cluster dashboard. Note that some of the features below are only first available in the insiders build of Azure Data Studio.

1. First, create a connection to your big data cluster in Azure Data Studio. For more information, see [Connect to a SQL Server big data cluster with Azure Data Studio](connect-to-big-data-cluster.md).

1. Right-click on the big data cluster endpoint, and click **Manage**.

   ![right click manage](media/view-cluster-status/right-click-manage.png)

1. Select the **SQL Server Big Data Cluster** tab to access the big data cluster dashboard.

   ![Big data cluster dashboard](media/view-cluster-status/bdc-dashboard.png)

### Service endpoints

It is important to be able to easily access the various services within a big data cluster. The big data cluster dashboard provides a service endpoints table that allows you to see and copy the service endpoints.

![service endpoints](media/view-cluster-status/service-endpoints.png)

The first several rows expose the following services:

- Application Proxy
- Cluster Management Service
- HDFS and Spark
- Management Proxy

These services list the endpoints that can be copied and pasted when you need the endpoint for connecting to those services. For example, you can click the copy icon to the right of the endpoint and then paste it in a text window requesting that endpoint. The Cluster Management Service endpoint is necessary to run the [cluster status notebook](#notebook).

### Dashboards

The service endpoints table also exposes several dashboards for monitoring:

- Metrics (Grafana)
- Logs (Kibana)
- Spark Job Monitoring
- Spark Resource Management

You can directly click on these links. You are asked twice to provide your user name and password before connecting to the service.

### <a id="notebook"></a> Cluster Status notebook

1. You can also view cluster status of the big data cluster by launching the Cluster Status notebook. To launch the notebook, click the **Cluster Status** task.

    ![launch](media/view-cluster-status/cluster-status-launch.png)

2. Before you begin, you will need the following items:

    - Big data cluster name
    - Controller username
    - Controller password
    - Controller endpoints

    The default big data cluster name is **mssql-cluster** unless you customized it during your deployment. You can find the controller endpoint from the big data cluster dashboard in the Service Endpoints table. The endpoint is listed as **Cluster Management Service**. If you do not know the credentials, ask the admin who deployed your cluster.

3. Click **Run Cells** on the top toolbar.

4. Follow the prompt for your credentials. Press press ENTER after you type each credential for the big data cluster name, controller username, and controller password.

    > [!Note]
    > If you do not have a config file setup with your big data, you will be asked for the controller endpoint. Type or paste it, and then press ENTER to proceed.

5. If you connected successfully, the rest of the notebook will show the output of each component of the big data cluster. When you want to re-run a certain code cell, hover over the code cell and click the **Run** icon.

## Use azdata

You can also use [azdata](deploy-install-azdata.md) commands to view both endpoints and the cluster status.

### Service endpoints

You can obtain the IP addresses of the external endpoints for the big data cluster using the following steps.

1. Find the IP address of the controller endpoint by looking at the EXTERNAL-IP output of the following **kubectl** command:

   ```bash
   kubectl get svc controller-svc-external -n <your-big-data-cluster-name>
   ```

   > [!TIP]
   > If you did not change the default name during deployment, use `-n mssql-cluster` in the previous command. **mssql-cluster** is the default name for the big data cluster.

1. Log in to the big data cluster with [azdata login](reference-azdata.md). Set the **--controller-endpoint** parameter to the external IP address of the controller endpoint.

   ```bash
   azdata login --controller-endpoint https://<ip-address-of-controller-svc-external>:30080 --controller-username <user-name>
   ```

   Specify the username and password that you configured for the controller (AZDATA_USERNAME and AZDATA_PASSWORD) during deployment.

1. Run [azdata bdc endpoint list](reference-azdata-bdc-endpoint.md) to get a list with a description of each endpoint and their corresponding IP address and port values. 

   ```bash
   azdata bdc endpoint list -o table
   ```

   The following list shows sample output from this command:

   ```output
   Description                                             Endpoint                                                   Ip              Name               Port    Protocol
   ------------------------------------------------------  ---------------------------------------------------------  --------------  -----------------  ------  ----------
   Gateway to access HDFS files, Spark                     https://11.111.111.111:30443                               11.111.111.111  gateway            30443   https
   Spark Jobs Management and Monitoring Dashboard          https://11.111.111.111:30443/gateway/default/sparkhistory  11.111.111.111  spark-history      30443   https
   Spark Diagnostics and Monitoring Dashboard              https://11.111.111.111:30443/gateway/default/yarn          11.111.111.111  yarn-ui            30443   https
   Application Proxy                                       https://11.111.111.111:30778                               11.111.111.111  app-proxy          30778   https
   Management Proxy                                        https://11.111.111.111:30777                               11.111.111.111  mgmtproxy          30777   https
   Log Search Dashboard                                    https://11.111.111.111:30777/kibana                        11.111.111.111  logsui             30777   https
   Metrics Dashboard                                       https://11.111.111.111:30777/grafana                       11.111.111.111  metricsui          30777   https
   Cluster Management Service                              https://11.111.111.111:30080                               11.111.111.111  controller         30080   https
   SQL Server Master Instance Front-End                    11.111.111.111,31433                                       11.111.111.111  sql-server-master  31433   tcp
   HDFS File System Proxy                                  https://11.111.111.111:30443/gateway/default/webhdfs/v1    11.111.111.111  webhdfs            30443   https
   Proxy for running Spark statements, jobs, applications  https://11.111.111.111:30443/gateway/default/livy/v1       11.111.111.111  livy               30443   https
   ```

### View cluster status

You can view the status of the cluster with the [azdata bdc status show](reference-azdata-bdc-status.md) command.

```bash
azdata bdc status show -o table
```

> [!TIP]
> To run the status commands, you must first log in with the **azdata login** command, which was shown in the previous endpoints section.

The following shows sample output from this command:

```output
Kind     Name           State
-------  -------------  -------
BDC      mssql-cluster  Ready
Control  default        Ready
Master   default        Ready
Compute  default        Ready
Data     default        Ready
Storage  default        Ready
```

### View pool status

You can view the status of pools within the cluster with the [azdata bdc pool status show](reference-azdata-bdc-pool-status.md) command. To use this command, specify the type of pool with the `--kind` parameter. The pool types are:

- compute
- data
- master
- spark
- storage

For example, the following command displays the pool status of the storage pool:

```bash
azdata bdc pool status show --kind storage
```

You should see text similar to the following output:

```output
[
  {
    "kind": "Pod",
    "logsUrl": "https://11.111.111.111:30080/clusters/mssql-cluster/pods/storage-0-0/logs/ui",
    "name": "storage-0-0",
    "nodeMetricsUrl": "https://11.111.111.111:30080/clusters/mssql-cluster/pods/storage-0-0/nodemetrics/ui",
    "sqlMetricsUrl": "https://11.111.111.111:30080/clusters/mssql-cluster/pods/storage-0-0/sqlmetrics/ui",
    "state": "Running"
  },
  {
    "kind": "Pod",
    "logsUrl": "https://11.111.111.111:30080/clusters/mssql-cluster/pods/storage-0-1/logs/ui",
    "name": "storage-0-1",
    "nodeMetricsUrl": "https://11.111.111.111:30080/clusters/mssql-cluster/pods/storage-0-1/nodemetrics/ui",
    "sqlMetricsUrl": "https://11.111.111.111:30080/clusters/mssql-cluster/pods/storage-0-1/sqlmetrics/ui",
    "state": "Running"
  }
]
```

The `logsUrl` value links to a kibana dashboard with log information:

![Kibana dashboard](./media/view-cluster-status/kibana-dashboard.png)

The `nodeMetricsUrl` and `sqlMetricsUrl` values link to a grafana dashboard for monitoring node health and SQL metrics:

![Grafana dashboard](./media/view-cluster-status/grafana-dashboard.png)

![SQL](./media/view-cluster-status/grafana-sql-status.png)

### View controller status

You can view the controller status with the [azdata bdc control status show](reference-azdata-bdc-control-status.md) command. It provides similar links to the monitoring dashboards related to the controller nodes of the big data cluster.

## Next steps

For more information about big data clusters, see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).
