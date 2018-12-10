---
title: Connect to master and HDFS
titleSuffix: SQL Server 2019 big data clusters
description: Learn how to connect to the SQL Server master instance and the HDFS/Spark Gateway for a SQL Server 2019 big data cluster (preview).
author: rothja
ms.author: jroth
manager: craigg
ms.date: 12/10/2018
ms.topic: conceptual
ms.prod: sql
---

# Connect to a SQL Server big data cluster with Azure Data Studio

This article describes how to connect to a SQL Server 2019 big data cluster (preview) from Azure Data Studio.

## Prerequisites

- A deployed [SQL Server 2019 big data cluster](deployment-guidance.md).
- [SQL Server 2019 big data tools](deploy-big-data-tools.md).

## Connect to the cluster

When you connect to a big data cluster, you have the option to connect to the SQL Server [master instance](concept-master-instance.md) or to the HDFS/Spark gateway. The following sections show how to connect to each.

## <a id="master"></a> Master instance

1. From the command-line, find the IP of your master instance with the following command:

   **AKS deployments:**

   ```
   kubectl get svc service-master-pool-lb -n <your-cluster-name>
   ```

   **Non-AKS deployments**:

   ```
   kubectl get svc service-master-pool-nodeport -n <your-cluster-name>
   ```

1. In Azure Data Studio, press **F1** > **New Connection**.

1. In **Connection type**, select **Microsoft SQL Server**.

1. Type the IP address of the SQL Server master instance in **Server name** (for example: **\<IP Address\>,31433**).

1. Enter a SQL login **User name** and **Password**.

1. Change the **Database name** to the **high_value_data** database.

   ![Connect to the master instance](./media/connect-to-big-data-cluster/connect-to-cluster.png)

1. Press **Connect**, and the **Server Dashboard** should appear.

## <a id="hdfs"></a> HDFS/Spark gateway

1. From the command-line, find the IP address of your HDFS/Spark gateway with one of the following commands.
   
   **AKS deployments:**

   ```
   kubectl get svc service-security-lb -n <your-cluster-name>
   ```

   **Non-AKS deployments**:

   ```
   kubectl get svc service-security-nodeport -n <your-cluster-name>
   ```
 
1. In Azure Data Studio, press **F1** > **New Connection**.

1. In **Connection type**, select **SQL Server big data cluster**.

1. Type the IP address of the big data cluster in **Server name** (do not specify a port).

1. Enter `root` for the **User** and specify the **Password** to your big data cluster.

   ![Connect to HDFS/Spark gateway](./media/connect-to-big-data-cluster/connect-to-cluster-hdfs-spark.png)

1. Press **Connect**, and the **Server Dashboard** should appear.

## Next steps