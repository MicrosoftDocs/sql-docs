---
title: How to deploy
titleSuffix: SQL Server 2019 big data clusters
description: Learn how to deploy SQL Server 2019 big data clusters (preview) on Kubernetes.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 02/28/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
ms.custom: seodec18
---

# How to deploy SQL Server big data clusters on Kubernetes

SQL Server big data cluster can be deployed as docker containers on a Kubernetes cluster. This is an overview of the setup and configuration steps:

- Set up Kubernetes cluster on a single VM, cluster of VMs, or in Azure Kubernetes Service (AKS).
- Install the cluster configuration tool **mssqlctl** on your client machine.
- Deploy SQL Server big data cluster in a Kubernetes cluster.

[!INCLUDE [Limited public preview note](../includes/big-data-cluster-preview-note.md)]

## <a id="prereqs"></a> Kubernetes cluster prerequisites

SQL Server big data clusters require a minimum Kubernetes version of at least v1.10 for both server and client (kubectl).

> [!NOTE]
> Note that the client and server Kubernetes versions should be +1 or -1 minor version. For more information, see [Kubernetes supported releases and component skew](https://github.com/kubernetes/community/blob/master/contributors/design-proposals/release/versioning.md#supported-releases-and-component-skew).

## <a id="kubernetes"></a> Kubernetes cluster setup

If you already have a Kubernetes cluster that meets above prerequisites, then you can skip directly to the [deployment step](#deploy). This section assumes a basic understanding of Kubernetes concepts.  For detailed information on Kubernetes, see the [Kubernetes documentation](https://kubernetes.io/docs/home).

You can choose to deploy Kubernetes in any of three ways:

| Deploy Kubernetes on: | Description | Link |
|---|---|---|
| **Minikube** | A single-node Kubernetes cluster in a VM. | [Instructions](deploy-on-minikube.md) |
| **Azure Kubernetes Services (AKS)** | A managed Kubernetes container service in Azure. | [Instructions](deploy-on-aks.md) |
| **Multiple machines** | A Kubernetes cluster deployed on physical or virtual machines using **kubeadm** | [Instructions](deploy-with-kubeadm.md) |
  
> [!TIP]
> For a sample python script that deploys both AKS and SQL Server big data cluster, see [Deploy a SQL Server big data cluster on Azure Kubernetes Service (AKS)](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster/deployment/aks).

## Deploy SQL Server 2019 big data tools

Before deploying SQL Server 2019 big data cluster, first [install the big data tools](deploy-big-data-tools.md):
- **mssqlctl**
- **kubectl**
- **Azure Data Studio**
- **SQL Server 2019 extension**

## <a id="deploy"></a> Deploy SQL Server big data cluster

After you have configured your Kubernetes cluster, you can proceed with the deployment for SQL Server big data cluster. 

> [!NOTE]
> If you are upgrading from a previous release, please see the [Upgrade section of this article](#upgrade).

To deploy a big data cluster in Azure with all default configurations for a dev/test environment, follow the instructions in this article:

[Quickstart: Deploy SQL Server big data cluster on Kubernetes](quickstart-big-data-cluster-deploy.md)

If you want to customize your deployment of big data cluster according to your workload needs, follow the instructions in the remainder of this article.

## Verify kubernetes configuration

Run the **kubectl** command to view the cluster configuration. Ensure that kubectl is pointed to the correct cluster context.

```bash
kubectl config view
```

## <a id="env"></a> Define environment variables

The cluster configuration can be customized using a set of environment variables that are passed to the `mssqlctl create cluster` command. Most of the environment variables are optional with default values as per below. Note that there are environment variables like credentials that require user input.

| Environment variable | Required | Default value | Description |
|---|---|---|---|
| **ACCEPT_EULA** | Yes | N/A | Accept the SQL Server license agreement (for example, 'Yes').  |
| **CLUSTER_NAME** | Yes | N/A | The name of the Kubernetes namespace to deploy SQLServer big data cluster into. |
| **CLUSTER_PLATFORM** | Yes | N/A | The platform the Kubernetes cluster is deployed. Can be `aks`, `minikube`, `kubernetes`|
| **CLUSTER_COMPUTE_POOL_REPLICAS** | No | 1 | The number of compute pool replicas to build out. In CTP 2.3 only valued allowed is 1. |
| **CLUSTER_DATA_POOL_REPLICAS** | No | 2 | The number of data pool replicas to build out. |
| **CLUSTER_STORAGE_POOL_REPLICAS** | No | 2 | The number of storage pool replicas to build out. |
| **DOCKER_REGISTRY** | Yes | TBD | The private registry where the images used to deploy the cluster are stored. |
| **DOCKER_REPOSITORY** | Yes | TBD | The private repository within the above registry where images are stored.  It is required for the duration of the gated public preview. |
| **DOCKER_USERNAME** | Yes | N/A | The username to access the container images in case they are stored in a private repository. It is required for the duration of the gated public preview. |
| **DOCKER_PASSWORD** | Yes | N/A | The password to access the above private repository. It is required for the duration of the gated public preview.|
| **DOCKER_IMAGE_TAG** | No | latest | The label used to tag the images. |
| **DOCKER_IMAGE_POLICY** | No | Always | Always force a pull of the images.  |
| **DOCKER_PRIVATE_REGISTRY** | Yes | N/A | For the timeframe of the gated public preview, you must set this value to "1". |
| **CONTROLLER_USERNAME** | Yes | N/A | The username for the cluster administrator. |
| **CONTROLLER_PASSWORD** | Yes | N/A | The password for the cluster administrator. |
| **KNOX_PASSWORD** | Yes | N/A | The password for Knox user. |
| **MSSQL_SA_PASSWORD** | Yes | N/A | The password of SA user for SQL master instance. |
| **USE_PERSISTENT_VOLUME** | No | true | `true` to use Kubernetes Persistent Volume Claims for pod storage.  `false` to use ephemeral host storage for pod storage. See the [data persistence](concept-data-persistence.md) article for more details. If you deploy SQL Server big data cluster on minikube and USE_PERSISTENT_VOLUME=true, you must set the value for `STORAGE_CLASS_NAME=standard`. |
| **STORAGE_CLASS_NAME** | No | default | If `USE_PERSISTENT_VOLUME` is `true` this indicates the name of the Kubernetes Storage Class to use. See the [data persistence](concept-data-persistence.md) article for more details. If you deploy SQL Server big data cluster on minikube, the default storage class name is different and you must override it by setting `STORAGE_CLASS_NAME=standard`. |
| **CONTROLLER_PORT** | No | 30080 | The TCP/IP port that the controller service listens on the public network. |
| **MASTER_SQL_PORT** | No | 31433 | The TCP/IP port that the master SQL instance listens on the public network. |
| **KNOX_PORT** | No | 30443 | The TCP/IP port that Apache Knox listens on the public network. |
| **PROXY_PORT** | No | 30777 | The TCP/IP port that proxy service listens on the public network. This is the port used for computing the portal URL. |
| **GRAFANA_PORT** | No | 30888 | The TCP/IP port that the Grafana monitoring application listens on the public network. |
| **KIBANA_PORT** | No | 30999 | The TCP/IP port that the Kibana log search application listens on the public network. |


> [!IMPORTANT]
>1. For the duration of the limited private preview, credentials for the private Docker registry will be provided to you upon triaging your [EAP registration](https://aka.ms/eapsignup).
>1. For an on-premises cluster built with **kubeadm**, the value for environment variable `CLUSTER_PLATFORM` is `kubernetes`. Also, when `USE_PERSISTENT_VOLUME=true`, you must pre-provision a Kubernetes storage class and pass it through using the `STORAGE_CLASS_NAME`.
>1. Make sure you wrap the passwords in double quotes if it contains any special characters. You can set the MSSQL_SA_PASSWORD to whatever you like, but make sure they are sufficiently complex and don't use the `!`, `&` or `'` characters. Note that double quotes delimiters work only in bash commands.
>1. The name of your cluster must be only lower case alpha-numeric characters, no spaces. All Kubernetes artifacts (containers, pods, statefull sets, services) for the cluster will be created in a namespace with same name as the cluster name specified.
>1. The **SA** account is a system administrator on the SQL Server Master instance that gets created during setup. After creating your SQL Server container, the MSSQL_SA_PASSWORD environment variable you specified is discoverable by running echo $MSSQL_SA_PASSWORD in the container. For security purposes, change your SA password as per best practices documented [here](https://docs.microsoft.com/sql/linux/quickstart-install-connect-docker?view=sql-server-2017#change-the-sa-password).

Setting the environment variables required for deploying a big data cluster differs depending on whether you are using Windows or Linux client.  Choose the steps below depending on which operating system you are using.

Initialize the following environment variables, they are required for deploying the cluster:

### Windows

Using a CMD window (not PowerShell), configure the following environment variables. Do not use quotes around the values.

```cmd
SET ACCEPT_EULA=yes
SET CLUSTER_PLATFORM=<minikube or aks or kubernetes>

SET CONTROLLER_USERNAME=<controller_admin_name - can be anything>
SET CONTROLLER_PASSWORD=<controller_admin_password - can be anything, password complexity compliant>
SET KNOX_PASSWORD=<knox_password - can be anything, password complexity compliant>
SET MSSQL_SA_PASSWORD=<sa_password_of_master_sql_instance, password complexity compliant>

SET DOCKER_REGISTRY=private-repo.microsoft.com
SET DOCKER_REPOSITORY=mssql-private-preview
SET DOCKER_USERNAME=<your username, credentials provided by Microsoft>
SET DOCKER_PASSWORD=<your password, credentials provided by Microsoft>
SET DOCKER_PRIVATE_REGISTRY="1"
```

### Linux

Initialize the following environment variables. In bash, you can use quotes around each value.

```bash
export ACCEPT_EULA=yes
export CLUSTER_PLATFORM=<minikube or aks or kubernetes>

export CONTROLLER_USERNAME="<controller_admin_name - can be anything>"
export CONTROLLER_PASSWORD="<controller_admin_password - can be anything, password complexity compliant>"
export KNOX_PASSWORD="<knox_password - can be anything, password complexity compliant>"
export MSSQL_SA_PASSWORD="<sa_password_of_master_sql_instance, password complexity compliant>"

export DOCKER_REGISTRY="private-repo.microsoft.com"
export DOCKER_REPOSITORY="mssql-private-preview"
export DOCKER_USERNAME="<your username, credentials provided by Microsoft>"
export DOCKER_PASSWORD="<your password, credentials provided by Microsoft>"
export DOCKER_PRIVATE_REGISTRY="1"
```

### Minikube settings

If you are deploying on minikube and `USE_PERSISTENT_VOLUME=true` (default), you must also override the default value for `STORAGE_CLASS_NAME` environment variable.

Use the following command on Windows for minikube deployments:

```cmd
SET STORAGE_CLASS_NAME=standard
```

Use the following command on Linux for minikube deployments:

```bash
export STORAGE_CLASS_NAME=standard
```

Alternatively, you can suppress using persistent volumes on minikube by setting `USE_PERSISTENT_VOLUME=false`.

### Kubadm settings

If you are deploying with kubeadm on your own physical or virtual machines, you must pre-provision a Kubernetes storage class and pass it through using the `STORAGE_CLASS_NAME`. Alternatively, you can suppress using persistent volumes by setting `USE_PERSISTENT_VOLUME=false`. For more information about persistent storage, see [Data persistence with SQL Server big data cluster on Kubernetes](concept-data-persistence.md).

## Deploy SQL Server big data cluster

The create cluster API is used to initialize the Kubernetes namespace and deploy all the application pods into the namespace. To deploy SQL Server big data cluster on your Kubernetes cluster, run the following command:

```bash
mssqlctl cluster create --name <your-cluster-name>
```

During cluster bootstrap, the client command window will output the deployment status. During the deployment process, you should see a series of messages where it is waiting for the controller pod:

```output
2018-11-15 15:42:02.0209 UTC | INFO | Waiting for controller pod to be up...
```

After 10 to 20 minutes, you should be notified that the controller pod is running:

```output
2018-11-15 15:50:50.0300 UTC | INFO | Controller pod is running.
2018-11-15 15:50:50.0585 UTC | INFO | Controller Endpoint: https://111.111.111.111:30080
```

> [!IMPORTANT]
> The entire deployment can take a long time due to the time required to download the container images for the components of the big data cluster. However, it should not take several hours. If you are experiencing problems with your deployment, see the [troubleshooting](#troubleshoot) section of this article to learn how to monitor and inspect the deployment.

When the deployment finishes, the output notifies you of success:

```output
2018-11-15 16:10:25.0583 UTC | INFO | Cluster state: Ready
2018-11-15 16:10:25.0583 UTC | INFO | Cluster deployed successfully.
```

## <a id="masterip"></a> Get big data cluster endpoints

After the deployment script has completed successfully, you can obtain the IP address of the SQL Server master instance using the steps outlined below. You will use this IP address and port number 31433 to connect to the SQL Server master instance (for example: **\<ip-address-of-endpoint-master-pool\>,31433**). Similarly, you can connect to the SQL Server big data cluster (HDFS/Spark Gateway) IP associated with the **endpoint-security** service.

The following kubectl commands retrieve common endpoints for the big data cluster:

```bash
kubectl get svc endpoint-master-pool -n <your-cluster-name>
kubectl get svc endpoint-security -n <your-cluster-name>
kubectl get svc endpoint-service-proxy -n <your-cluster-name>
```

Look for the **External-IP** value that is assigned to each service.

All cluster endpoints are also outlined in the **Service Endpoints** tab in the Cluster Administration Portal. You can access the portal using the external IP address and port number for the `endpoint-service-proxy` (for example: **https://\<ip-address-of-endpoint-service-proxy\>:30777/portal**). Credentials for accessing the admin portal are the values of `CONTROLLER_USERNAME` and `CONTROLLER_PASSWORD` environment variables provided above. You can also use the Cluster Administration Portal to monitor the deployment.

For more information on how to connect, see [Connect to a SQL Server big data cluster with Azure Data Studio](connect-to-big-data-cluster.md).

### Minikube

If you are using Minikube, you need to run the following command to get the IP address you need to connect to. In addition to the IP, specify the port for the endpoint you need to connect to. To get all the service endpoints for 

```bash
minikube ip
```

Irrespective of the platform you are running your Kubernetes cluster on, to get all the service endpoints deployed for the cluster, run following command:
```bash
kubectl get svc -n <your-cluster-name>
```

## <a id="upgrade"></a> Upgrade to a new release

Currently, the only way to upgrade a big data cluster to a new release is to manually remove and recreate the cluster. Each release has a unique version of **mssqlctl** that is not compatible with the previous version. Also, if an older cluster had to download an image on a new node, the latest image might not be compatible with the older images on the cluster. To upgrade to the latest release, use the following steps:

1. Before deleting the old cluster, back up the data on the SQL Server master instance and on HDFS. For the SQL Server master instance, you can use [SQL Server backup and restore](data-ingestion-restore-database.md). For HDFS, you [can copy the data out with **curl**](data-ingestion-curl.md).

1. Delete the old cluster with the `mssqlctl delete cluster` command.

   ```bash
    mssqlctl cluster delete --name <old-cluster-name>
   ```

   > [!Important]
   > Use the version of **mssqlctl** that matches your cluster. Do not delete an older cluster with the newer version of **mssqlctl**.

1. Uninstall any old versions of **mssqlctl**.

   ```bash
   pip3 uninstall mssqlctl
   ```

   > [!IMPORTANT]
   > You should not install the new version of **mssqlctl** without uninstalling any older versions first.

1. Install the latest version of **mssqlctl**. 
   
   **Windows:**

   ```powershell
   pip3 install -r  https://private-repo.microsoft.com/python/ctp-2.3/mssqlctl/requirements.txt --trusted-host https://private-repo.microsoft.com
   ```

   **Linux:**
   
   ```bash
   pip3 install -r  https://private-repo.microsoft.com/python/ctp-2.3/mssqlctl/requirements.txt --trusted-host https://private-repo.microsoft.com --user
   ```

   > [!IMPORTANT]
   > For each release, the path to **mssqlctl** changes. Even if you previously installed **mssqlctl**, you must reinstall from the latest path before creating the new cluster.

1. Install the latest release using the instructions in the [Deploy section](#deploy) of this article. 

## <a id="troubleshoot"></a> Monitoring and troubleshooting

To monitor or troubleshoot a deployment, use **kubectl** to inspect the status of the cluster and to detect potential problems. At any time during a deployment, you can open a different command window to run the following tests.

1. Inspect the status of the pods in your cluster.

   ```cmd
   kubectl get pods -n <your-cluster-name>
   ```

   During deployment, pods with a **STATUS** of **ContainerCreating** are still coming up. If the deployment hangs for any reason, this can give you an idea where the problem might be. Also look at the **READY** column. This tells you how many containers have started in the pod. Note that deployments can take thirty minutes or more depending on your configuration and network. Much of this time is spent downloading the container images for different components. The following table shows example edited output of two containers during a deployment:

   ```output
   PS C:\> kubectl get pods -n sbdc8
   NAME                                     READY   STATUS              RESTARTS   AGE
   mssql-controller-h79ft                   4/4     Running             0          13m
   mssql-storage-pool-default-0             0/7     ContainerCreating   0          6m
   ```

1. Describe an individual pod for more details. The following command inspects the `mssql-storage-pool-default-0` pod.

   ```cmd
   kubectl describe pod mssql-storage-pool-default-0 -n <your-cluster-name>
   ```

   This outputs detailed information about the pod, including recent events. If an error has occurred, you can sometimes find the error here.

1. Retrieve the logs for containers running in a pod. The following command retrieves the logs for all containers running in the pod named `mssql-storage-pool-default-0` and outputs them to a file name `pod-logs.txt`:

   ```cmd
   kubectl logs mssql-storage-pool-default-0 --all-containers=true -n <your-cluster-name> > pod-logs.txt
   ```

1. Review the cluster services during and after a deployment with the following command:

   ```cmd
   kubectl get svc -n <your-cluster-name>
   ```

   These services support internal and external connections to the big data cluster. For external connections, the following services are used:

   | Service | Description |
   |---|---|
   | **endpoint-master-pool** | Provides access to the master instance.<br/>(**EXTERNAL-IP,31433** and the **SA** user) |
   | **endpoint-controller** | Supports tools and clients that manage the cluster. |
   | **endpoint-service-proxy** | Provides access to the [Cluster Administration Portal](cluster-admin-portal.md).<br/>(https://**EXTERNAL-IP**:30777/portal)|
   | **endpoint-security** | Provides access to the HDFS/Spark gateway.<br/>(**EXTERNAL-IP** and the **root** user) |

1. Use the [Cluster Administration Portal](cluster-admin-portal.md) to monitor the deployment on the **Deployment** tab. You have to wait for the **endpoint-service-proxy** service to start before accessing this portal, so it won't be available at the beginning of a deployment.

> [!TIP]
> For more information about troubleshooting the cluster, see [Kubectl commands for monitoring and troubleshooting SQL Server big data clusters](cluster-troubleshooting-commands.md).

## Next steps

Try out some of the new capabilities and learn [How to use notebooks in SQL Server 2019 preview](notebooks-guidance.md).
