---
title: Deployment guidance
titleSuffix: SQL Server big data clusters
description: Learn how to deploy SQL Server 2019 big data clusters (preview) on Kubernetes.
author: rothja 
ms.author: jroth 
manager: jroth
ms.date: 06/26/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
ms.custom: seodec18
---

# How to deploy SQL Server big data clusters on Kubernetes

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

SQL Server big data cluster is deployed as docker containers on a Kubernetes cluster. This is an overview of the setup and configuration steps:

- Set up a Kubernetes cluster on a single VM, cluster of VMs, or in Azure Kubernetes Service (AKS).
- Install the cluster configuration tool **mssqlctl** on your client machine.
- Deploy a SQL Server big data cluster in a Kubernetes cluster.

[!INCLUDE [Limited public preview note](../includes/big-data-cluster-preview-note.md)]

## Install SQL Server 2019 big data tools

Before deploying a SQL Server 2019 big data cluster, first [install the big data tools](deploy-big-data-tools.md):

- **mssqlctl**
- **kubectl**
- **Azure Data Studio**
- **SQL Server 2019 extension**

## <a id="prereqs"></a> Kubernetes prerequisites

SQL Server big data clusters require a minimum Kubernetes version of at least v1.10 for both server and client (kubectl).

> [!NOTE]
> Note that the client and server Kubernetes versions should be within +1 or -1 minor version. For more information, see [Kubernetes supported releases and component skew](https://github.com/kubernetes/community/blob/master/contributors/design-proposals/release/versioning.md#supported-releases-and-component-skew).

### <a id="kubernetes"></a> Kubernetes cluster setup

If you already have a Kubernetes cluster that meets the above prerequisites, then you can skip directly to the [deployment step](#deploy). This section assumes a basic understanding of Kubernetes concepts.  For detailed information on Kubernetes, see the [Kubernetes documentation](https://kubernetes.io/docs/home).

You can choose to deploy Kubernetes in any of three ways:

| Deploy Kubernetes on: | Description | Link |
|---|---|---|
| **Azure Kubernetes Services (AKS)** | A managed Kubernetes container service in Azure. | [Instructions](deploy-on-aks.md) |
| **Multiple machines (kubeadm)** | A Kubernetes cluster deployed on physical or virtual machines using **kubeadm** | [Instructions](deploy-with-kubeadm.md) |
| **Minikube** | A single-node Kubernetes cluster in a VM. | [Instructions](deploy-on-minikube.md) |

> [!TIP]
> For a sample python script that deploys both AKS and a SQL Server big data cluster in one step, see [Quickstart: Deploy SQL Server big data cluster on Azure Kubernetes Service (AKS)](quickstart-big-data-cluster-deploy.md).

### Verify Kubernetes configuration

Run the **kubectl** command to view the cluster configuration. Ensure that kubectl is pointed to the correct cluster context.

```bash
kubectl config view
```

After you have configured your Kubernetes cluster, you can proceed with the deployment of a new SQL Server big data cluster. If you are upgrading from a previous release, please see [How to upgrade SQL Server big data clusters](deployment-upgrade.md).

## <a id="deploy"></a> Deployment overview

Starting in CTP 2.5, most big data cluster settings are defined in a JSON deployment configuration file. You can use a default deployment profile for AKS, kubeadm, or minikube or you can customize your own deployment configuration file to use during setup. For security reasons, authentication settings are passed via environment variables.

The following sections provide more details on how to configure your big data cluster deployments as well as examples of common customizations. Also, you can always edit the custom deployment configuration file using an editor like VSCode for example.

## <a id="configfile"></a> Default configurations

Big data cluster deployment options are defined in JSON configuration files. There are three standard deployment profiles with default settings for dev/test environments:

| Deployment profile | Kubernetes environment |
|---|---|
| **aks-dev-test** | Azure Kubernetes Service (AKS) |
| **kubeadm-dev-test** | Multiple machines (kubeadm) |
| **minikube-dev-test** | minikube |

You can deploy a big data cluster by running **mssqlctl bdc create**. This prompts you to choose one of the default configurations and then guides you through the deployment.

```bash
mssqlctl bdc create
```

In this scenario, you are prompted for any settings that are not part of the default configuration, such as passwords. Note that the Docker information is provided to you by Microsoft as part of the SQL Server 2019 [Early Adoption Program](https://aka.ms/eapsignup).

> [!IMPORTANT]
> The default name of the big data cluster is **mssql-cluster**. This is important to know in order to run any of the **kubectl** commands that specify the Kubernetes namespace with the `-n` parameter.

## <a id="customconfig"></a> Custom configurations

It is also possible to customize your own deployment configuration profile. You can do this with the following steps:

1. Start with one of the standard deployment profiles that match your Kubernetes environment. You can use the  **mssqlctl bdc config list** command to list them:

   ```bash
   mssqlctl bdc config list
   ```

1. To customize your deployment, create a copy of the deployment profile with the **mssqlctl bdc config init** command. For example, the following command creates a copy of the **aks-dev-test** deployment configuration file in a target directory named `custom`:

   ```bash
   mssqlctl bdc config init --source aks-dev-test --target custom
   ```

   > [!TIP]
   > The `--target` specifies a directory that contains the configuration file based on the `--source` parameter.

1. To customize settings in your deployment configuration profile, you can edit the deployment configuration file in a tool that is good for editing JSON files, such as VS Code. For scripted automation, you can also edit the custom deployment profile using **mssqlctl bdc config section set** command. For example, the following command alters a custom deployment profile to change the name of the deployed cluster from the default (**mssql-cluster**) to **test-cluster**:  

   ```bash
   mssqlctl bdc config section set --config-profile custom --json-values "metadata.name=test-cluster"
   ```

   > [!TIP]
   > The `--config-profile` specifies a directory name for your custom deployment profile, but the actual modifications happen on the deployment configuration JSON file within that directory. A useful tool for finding JSON paths is the [JSONPath Online Evaluator](https://jsonpath.com/).

   In addition to passing key-value pairs, you can also provide inline JSON values or pass JSON patch files. For more information, see [Configure deployment settings for big data clusters](deployment-custom-configuration.md).

1. Then pass the custom configuration file to **mssqlctl bdc create**. Note that you must set the required [environment variables](#env), otherwise you will be prompted for the values:

   ```bash
   mssqlctl bdc create --config-profile custom --accept-eula yes
   ```

> [!TIP]
> For more information on the structure of a deployment configuration file, see the [Deployment configuration file reference](reference-deployment-config.md). For more configuration examples, see [Configure deployment settings for big data clusters](deployment-custom-configuration.md).

## <a id="env"></a> Environment variables

The following environment variables are used for security settings that are not stored in a deployment configuration file. Note that Docker settings except credentials can be set in the configuration file.

| Environment variable | Description |
|---|---|---|---|
| **DOCKER_USERNAME** | The username to access the container images in case they are stored in a private repository. |
| **DOCKER_PASSWORD** | The password to access the above private repository. |
| **CONTROLLER_USERNAME** | The username for the cluster administrator. |
| **CONTROLLER_PASSWORD** | The password for the cluster administrator. |
| **KNOX_PASSWORD** | The password for Knox user. |
| **MSSQL_SA_PASSWORD** | The password of SA user for SQL master instance. |

These environment variables must be set prior to calling **mssqlctl bdc create**. If any variable is not set, you are prompted for it.

The following example shows how to set the environment variables for Linux (bash) and Windows (PowerShell):

```bash
export CONTROLLER_USERNAME=admin
export CONTROLLER_PASSWORD=<password>
export MSSQL_SA_PASSWORD=<password>
export KNOX_PASSWORD=<password>
export DOCKER_USERNAME=<docker-username>
export DOCKER_PASSWORD=<docker-password>
```

```PowerShell
SET CONTROLLER_USERNAME=admin
SET CONTROLLER_PASSWORD=<password>
SET MSSQL_SA_PASSWORD=<password>
SET KNOX_PASSWORD=<password>
SET DOCKER_USERNAME=<docker-username>
SET DOCKER_PASSWORD=<docker-password>
```

After setting the environment variables, you must run `mssqlctl bdc create` to trigger the deployment. This example uses the cluster configuration profile created above:

```
mssqlctl bdc create --config-profile custom --accept-eula yes
```

Please note the following guidelines:

- At this time, credentials for the private Docker registry will be provided to you upon triaging your [Early Adoption Program registration](https://aka.ms/eapsignup). Early Adoption Program registration is required to test SQL Server big data clusters.
- Make sure you wrap the passwords in double quotes if it contains any special characters. You can set the **MSSQL_SA_PASSWORD** to whatever you like, but make sure the password is sufficiently complex and don't use the `!`, `&` or `'` characters. Note that double quotes delimiters work only in bash commands.
- The **SA** login is a system administrator on the SQL Server master instance that gets created during setup. After creating your SQL Server container, the **MSSQL_SA_PASSWORD** environment variable you specified is discoverable by running echo $MSSQL_SA_PASSWORD in the container. For security purposes, change your SA password as per best practices documented [here](../linux/quickstart-install-connect-docker.md#sapassword).

## <a id="unattended"></a> Unattended install

For an unattended deployment, you must set all required environment variables, use a configuration file, and call `mssqlctl bdc create` command with the `--accept-eula yes` parameter. The examples in the previous section demonstrate the syntax for an unattended installation.

## <a id="monitor"></a> Monitor the deployment

During cluster bootstrap, the client command window will output the deployment status. During the deployment process, you should see a series of messages where it is waiting for the controller pod:

```output
Waiting for cluster controller to start.
```

In less than 15 to 30 minutes, you should be notified that the controller pod is running:

```output
Cluster controller endpoint is available at 11.111.111.11:30080.
Cluster control plane is ready.
```

> [!IMPORTANT]
> The entire deployment can take a long time due to the time required to download the container images for the components of the big data cluster. However, it should not take several hours. If you are experiencing problems with your deployment, see [Monitoring and troubleshoot SQL Server big data clusters](cluster-troubleshooting-commands.md).

When the deployment finishes, the output notifies you of success:

```output
Cluster deployed successfully.
```

> [!TIP]
> The default name for the deployed big data cluster is `mssql-cluster` unless modified by a custom configuration.

## <a id="endpoints"></a> Retrieve endpoints

After the deployment script has completed successfully, you can obtain the IP addresses of the external endpoints for the big data cluster using the following steps.

1. After the deployment, find the IP address of the controller endpoint by looking at the EXTERNAL-IP output of the following **kubectl** command:

   ```bash
   kubectl get svc controller-svc-external -n <your-big-data-cluster-name>
   ```

   > [!TIP]
   > If you did not change the default name during deployment, use `-n mssql-cluster` in the previous command. **mssql-cluster** is the default name for the big data cluster.

1. Log in to the big data cluster with [mssqlctl login](reference-mssqlctl.md). Set the **--controller-endpoint** parameter to the external IP address of the controller endpoint.

   ```bash
   mssqlctl login --controller-endpoint https://<ip-address-of-controller-svc-external>:30080 --controller-username <user-name>
   ```

   Specify the username and password that you configured for the controller (CONTROLLER_USERNAME and CONTROLLER_PASSWORD) during deployment.

1. Run [mssqlctl bdc endpoint list](reference-mssqlctl-bdc-endpoint.md) to get a list with a description of each endpoint and their corresponding IP address and port values. 

   ```bash
   mssqlctl bdc endpoint list -o table
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

You can also get all the service endpoints deployed for the cluster by running the following **kubectl** command:

```bash
kubectl get svc -n <your-big-data-cluster-name>
```

### Minikube

If you are using minikube, you need to run the following command to get the IP address you need to connect to. In addition to the IP, specify the port for the endpoint you need to connect to.

```bash
minikube ip
```

## <a id="status"></a> Verify the cluster status

After deployment, you can check the status of the cluster with the [mssqlctl bdc status show](reference-mssqlctl-bdc-status.md) command.

```bash
mssqlctl bdc status show -o table
```

> [!TIP]
> To run the status commands, you must first log in with the **mssqlctl login** command, which was shown in the previous endpoints section.

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

In addition to this summary status, you can also get more detailed status with the following commands:

- [mssqlctl bdc control status](reference-mssqlctl-bdc-control-status.md)
- [mssqlctl bdc pool status](reference-mssqlctl-bdc-pool-status.md)

The output from these commands contain URLs to Kibana and Grafana dashboards for more detailed analysis. 

In addition to using **mssqlctl**, you can also use Azure Data Studio to find both endpoints and status information. For more information about viewing cluster status with **mssqlctl** and Azure Data Studio, see [How to view the status of a big data cluster](view-cluster-status.md).

## <a id="connect"></a> Connect to the cluster

For more information on how to connect to the big data cluster, see [Connect to a SQL Server big data cluster with Azure Data Studio](connect-to-big-data-cluster.md).

## Next steps

To learn more about big data cluster deployment, see the following resources:

- [Configure deployment settings for big data clusters](deployment-custom-configuration.md)
- [Perform an offline deployment of a SQL Server big data cluster](deploy-offline.md)
- [Workshop: Microsoft SQL Server big data clusters Architecture](https://github.com/Microsoft/sqlworkshops/tree/master/sqlserver2019bigdataclusters)
