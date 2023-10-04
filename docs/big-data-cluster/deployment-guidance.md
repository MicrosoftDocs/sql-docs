---
title: Deployment guidance
titleSuffix: SQL Server Big Data Clusters
description: Learn how to deploy SQL Server Big Data Clusters on Kubernetes.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
ms.date: 06/22/2020
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.custom: intro-deployment
---

# How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

SQL Server Big Data Cluster is deployed as docker containers on a Kubernetes cluster. This is an overview of the setup and configuration steps:

- Set up a Kubernetes cluster on a single VM, cluster of VMs, in Azure Kubernetes Service (AKS), Red Hat OpenShift or in Azure Red Hat OpenShift (ARO).
- Install the cluster configuration tool [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] on your client machine.
- Deploy a SQL Server big data cluster in a Kubernetes cluster.

## Tested configurations

See [Tested configurations](release-notes-big-data-cluster.md#tested-configurations) for a complete list of the various Kubernetes platforms validated for deploying SQL Server Big Data Clusters.

### SQL Server editions

|Edition|Notes|
|---------|---------|
|Enterprise<br/>Standard<br/>Developer| Big Data Cluster edition is determined by the edition of SQL Server master instance. At deployment time Developer edition is deployed by default. You can change the edition after deployment. See [Configure SQL Server master instance](./configure-sql-server-master-instance.md). |

## <a id="prereqs"></a> Kubernetes

### <a id="kubernetes"></a> Kubernetes cluster setup

If you already have a Kubernetes cluster that meets the above prerequisites, then you can skip directly to the [deployment step](#deploy). This section assumes a basic understanding of Kubernetes concepts.  For detailed information on Kubernetes, see the [Kubernetes documentation](https://kubernetes.io/docs/home).

You can choose to deploy Kubernetes the following ways:

| Deploy Kubernetes on: | Description | Link |
|---|---|---|
| **Azure Kubernetes Services (AKS)** | A managed Kubernetes container service in Azure. | [Instructions](deploy-on-aks.md) |
| **Single or Multiple machines (`kubeadm`)** | A Kubernetes cluster deployed on physical or virtual machines using `kubeadm` | [Instructions](deploy-with-kubeadm.md) |
|**Azure Red Hat OpenShift** | A managed offering of OpenShift running in Azure. | [Instructions](deploy-openshift.md)|
|**Red Hat OpenShift**|A hybrid cloud, enterprise Kubernetes application platform.| [Instructions](deploy-openshift.md)|

> [!TIP]
> You can also script the deployment of AKS and a big data cluster in one step. For more information, see how to do this in a [python script](quickstart-big-data-cluster-deploy.md) or an Azure Data Studio [notebook](notebooks-deploy.md).

### Verify Kubernetes configuration

Run the `kubectl` command to view the cluster configuration. Ensure that kubectl is pointed to the correct cluster context.

```bash
kubectl config view
```

> [!Important] 
> If you are deploying on a multi node Kubernetes cluster that you bootstrapped using `kubeadm`, before starting the big data cluster deployment, ensure the clocks are synchronized across all the Kubernetes nodes the deployment is targeting. The big data cluster has built-in health properties for various services that are time sensitive and clock skews can result in incorrect status.

After you have configured your Kubernetes cluster, you can proceed with the deployment of a new SQL Server big data cluster. If you are upgrading from a previous release, please see [How to upgrade [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](deployment-upgrade.md).

## Ensure you have storage configured

Most big data cluster deployments should have persistent storage. At this time, you need to make sure you have a plan for how you're going to provide persistent storage on the Kubernetes cluster before you deploy.

 - If you deploy in AKS, no storage setup is necessary. AKS provides built-in storage classes with dynamic provisioning. You can customize the storage class (`default` or `managed-premium`) in the deployment configuration file. The built-in profiles use a `default` storage class. 
 - If you are deploying on a Kubernetes cluster you deployed using `kubeadm`, you'll need to ensure you have sufficient storage for a cluster of your desired scale available and configured for use. If you wish to customize how your storage is used, you should do this before proceeding. See [Data persistence with SQL Server big data cluster on Kubernetes](concept-data-persistence.md).

## Install SQL Server 2019 Big Data tools

Before deploying a SQL Server 2019 big data cluster, first [install the big data tools](deploy-big-data-tools.md):

- [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)]
- `kubectl`
- Azure Data Studio
- [Data Virtualization extension](../azure-data-studio/extensions/data-virtualization-extension.md) for Azure Data Studio
- [Azure CLI](/cli/azure/install-azure-cli), if deploying to AKS


## <a id="deploy"></a> Deployment overview

Most big data cluster settings are defined in a JSON deployment configuration file. You can use a default deployment profile for AKS and Kubernetes clusters created with `kubeadm` or you can customize your own deployment configuration file to use during setup. For security reasons, authentication settings are passed via environment variables.

The following sections provide more details on how to configure your big data cluster deployments as well as examples of common customizations. Also, you can always edit the custom deployment configuration file using an editor like VS Code for example.

## <a id="configfile"></a> Default configurations

Big data cluster deployment options are defined in JSON configuration files. You can start your customization of the cluster deployment from the built-in deployment profiles that are available in the [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)]. 

> [!NOTE]
> The container images required for the big data cluster deployment are hosted on Microsoft Container Registry (`mcr.microsoft.com`), in the `mssql/bdc` repository. By default, these settings are already included in the `control.json` configuration file in each of the deployment profiles included with [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)]. In addition, the container image tag for each release is also pre-populated in the same configuration file. If you need to pull the container images into your own private container registry and or modify the container registry/repository settings, follow the instructions in the [Offline installation article](deploy-offline.md)

Run this command to find the templates available:

```bash
azdata bdc config list -o table 
```

The following templates are available as of SQL Server 2019 CU5: 

| Deployment profile | Kubernetes environment |
|---|---|
| `aks-dev-test` | Deploy SQL Server Big Data Cluster on Azure Kubernetes Service (AKS)|
| `aks-dev-test-ha` | Deploy SQL Server Big Data Cluster on Azure Kubernetes Service (AKS). Mission critical services like SQL Server master and HDFS name node are configured for high availability.|
| `aro-dev-test`|Deploy SQL Server Big Data Cluster on Azure Red Hat OpenShift for development and testing. <br/><br/>Introduced in SQL Server 2019 CU 5.|
| `aro-dev-test-ha`|Deploy SQL Server Big Data Cluster with high availability on a Red Hat OpenShift cluster for development and testing. <br/><br/>Introduced in SQL Server 2019 CU 5.|
| `kubeadm-dev-test` | Deploy SQL Server Big Data Cluster on a Kubernetes cluster created with kubeadm using a single or multiple physical or virtual machines.|
| `kubeadm-prod`| Deploy SQL Server Big Data Cluster on a Kubernetes cluster created with kubeadm using a single or multiple physical or virtual machines. Use this template to enable big data cluster services to integrate with Active Directory. Mission critical services like SQL Server master instance and HDFS name node are deployed in a highly available configuration.  |
| `openshift-dev-test`|Deploy SQL Server Big Data Cluster on a Red Hat OpenShift cluster for development and testing. <br/><br/>Introduced in SQL Server 2019 CU 5.|
| `openshift-prod`|Deploy SQL Server Big Data Cluster with high availability on a Red Hat OpenShift cluster. <br/><br/>Introduced in SQL Server 2019 CU 5.|

You can deploy a big data cluster by running `azdata bdc create`. This prompts you to choose one of the default configurations and then guides you through the deployment.

The first time you run [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] you must include `--accept-eula=yes` to accept the end user license agreement (EULA).

```bash
azdata bdc create --accept-eula=yes
```

In this scenario, you are prompted for any settings that are not part of the default configuration, such as passwords. 

> [!IMPORTANT]
> The default name of the big data cluster is `mssql-cluster`. This is important to know in order to run any of the `kubectl` commands that specify the Kubernetes namespace with the `-n` parameter.

## <a id="customconfig"></a> Custom configurations

It is also possible to customize your deployment to accommodate the workloads you are planning to run. You cannot change the scale (number of replicas) or storage settings for big data cluster services post deployments, so you must plan your deployment configuration carefully to avoid capacity issues. To customize your deployment, follow these steps:

1. Start with one of the standard deployment profiles that match your Kubernetes environment. You can use the  `azdata bdc config list` command to list them:

   ```bash
   azdata bdc config list
   ```

1. To customize your deployment, create a copy of the deployment profile with the `azdata bdc config init` command. For example, the following command creates a copy of the `aks-dev-test` deployment configuration files in a target directory named `custom`:

   ```bash
   azdata bdc config init --source aks-dev-test --target custom
   ```

   >[!TIP]
   >`--target` specifies a directory that contains the configuration files, `bdc.json` and `control.json`, based on the `--source` parameter.

1. To customize settings in your deployment configuration profile, you can edit the deployment configuration file in a tool that is good for editing JSON files, such as VS Code. For scripted automation, you can also edit the custom deployment profile using `azdata bdc config` command. For example, the following command alters a custom deployment profile to change the name of the deployed cluster from the default (`mssql-cluster`) to `test-cluster`:  

   ```bash
   azdata bdc config replace --config-file custom/bdc.json --json-values "metadata.name=test-cluster"
   ```

   > [!TIP]
   > You can also pass in the cluster name at deployment time using the *--name* parameter for `azdata create bdc` command. The parameters in the command have precedence over the values in the configuration files.
   >
   > A useful tool for finding JSON paths is the [JSONPath Online Evaluator](https://jsonpath.com/).
   >
   In addition to passing key-value pairs, you can also provide inline JSON values or pass JSON patch files. For more information, see [Configure deployment settings for Big Data Cluster resources and services](deployment-custom-configuration.md).

1. Pass the custom configuration file to `azdata bdc create`. Note that you must set the required [environment variables](#env), otherwise the terminal prompts for the values:

   ```bash
   azdata bdc create --config-profile custom --accept-eula yes
   ```

> [!WARNING]
> The parameter ```imagePullPolicy``` is required to be set as ```"Always"``` in the deployment profile control.json file.

For more information on the structure of a deployment configuration file, see the [Deployment configuration file reference](reference-deployment-config.md). For more configuration examples, see [Configure deployment settings for Big Data Clusters](deployment-custom-configuration.md).

## <a id="env"></a> Environment variables

The following environment variables are used for security settings that are not stored in a deployment configuration file. Note that Docker settings except credentials can be set in the configuration file.

| Environment variable | Requirement |Description |
|---|---|---|
| `AZDATA_USERNAME` | Required |The username for SQL Server Big Data Cluster administrator. A sysadmin login with the same name is created in SQL Server master instance. As a security best practice, `sa` account is disabled. <br/><br/>[!INCLUDE [big-data-cluster-root-user](../includes/big-data-cluster-root-user.md)]|
| `AZDATA_PASSWORD` | Required |The password for the user accounts created above. On clusters deployed prior to SQL Server 2019 CU5, the same password is used for the `root` user, to secure Knox gateway and HDFS. |
| `ACCEPT_EULA`| Required for first use of [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)]| Set to "yes". When set as an environment variable, it applies EULA to both SQL Server and [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)]. If not set as environment variable, you can include `--accept-eula=yes` in the first use of [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] command.|
| `DOCKER_USERNAME` | Optional | The username to access the container images in case they are stored in a private repository. See the [Offline deployments](deploy-offline.md) topic for more details on how to use a private Docker repository for big data cluster deployment.|
| `DOCKER_PASSWORD` | Optional |The password to access the above private repository. |

These environment variables must be set prior to calling `azdata bdc create`. If any variable is not set, you are prompted for it.

The following example shows how to set the environment variables for Linux (bash) and Windows (PowerShell):

```bash
export AZDATA_USERNAME=admin
export AZDATA_PASSWORD=<password>
export ACCEPT_EULA=yes
```

```PowerShell
SET AZDATA_USERNAME=admin
SET AZDATA_PASSWORD=<password>
```

> [!NOTE]
> On clusters deployed prior to SQL Server 2019 CU 5, you must use `root` user for Knox gateway with the above password. `root` is the only user supported for in this basic authentication (username/password).
> [!INCLUDE [big-data-cluster-root-user](../includes/big-data-cluster-root-user.md)]
> To connect to SQL Server with basic authentication, use the same values as the AZDATA_USERNAME and AZDATA_PASSWORD [environment variables](#env). 

After setting the environment variables, you must run `azdata bdc create` to trigger the deployment. This example uses the cluster configuration profile created above:

```bash
azdata bdc create --config-profile custom --accept-eula yes
```

Please note the following guidelines:

- Make sure you wrap the password in double quotes if it contains any special characters. You can set the `AZDATA_PASSWORD` to whatever you like, but make sure the password is sufficiently complex and don't use the `!`, `&` or `'` characters. Note that double quotes delimiters work only in bash commands.
- The `AZDATA_USERNAME` login is a system administrator on the SQL Server master instance that gets created during setup. After creating your SQL Server container, the `AZDATA_PASSWORD` environment variable you specified is discoverable by running `echo $AZDATA_PASSWORD` in the container. For security purposes, change the password as a best practice.

## <a id="unattended"></a> Unattended install

For an unattended deployment, you must set all required environment variables, use a configuration file, and call `azdata bdc create` command with the `--accept-eula yes` parameter. The examples in the previous section demonstrate the syntax for an unattended installation.

## <a id="monitor"></a> Monitor the deployment

During cluster bootstrap, the client command window returns the deployment status. During the deployment process, you should see a series of messages where it is waiting for the controller pod:

```output
Waiting for cluster controller to start.
```

In 15 to 30 minutes, you should be notified that the controller pod is running:

```output
Cluster controller endpoint is available at 11.111.111.11:30080.
Cluster control plane is ready.
```

> [!IMPORTANT]
> The entire deployment can take a long time due to the time required to download the container images for the components of the big data cluster. However, it should not take several hours. If you are experiencing problems with your deployment, see [Monitoring and troubleshoot [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](cluster-troubleshooting-commands.md).

When the deployment finishes, the output notifies you of success:

```output
Cluster deployed successfully.
```

> [!TIP]
> The default name for the deployed big data cluster is `mssql-cluster` unless modified by a custom configuration.

## <a id="endpoints"></a> Retrieve endpoints

After the deployment script has completed successfully, you can obtain the addresses of the external endpoints for the big data cluster using the following steps.

1. After the deployment, find the IP address of the controller endpoint either from the deployment standard output or by looking at the EXTERNAL-IP output of the following `kubectl` command:

   ```bash
   kubectl get svc controller-svc-external -n <your-big-data-cluster-name>
   ```

   > [!TIP]
   > If you did not change the default name during deployment, use `-n mssql-cluster` in the previous command. `mssql-cluster` is the default name for the big data cluster.

1. Log in to the big data cluster with [azdata login](../azdata/reference/reference-azdata.md). Set the `--endpoint` parameter to the external IP address of the controller endpoint.

   ```bash
   azdata login --endpoint https://<ip-address-of-controller-svc-external>:30080 --username <user-name>
   ```

   Specify the username and password that you configured for the big data cluster admin (AZDATA_USERNAME and AZDATA_PASSWORD) during deployment.

   > [!TIP]
   > If you are the Kubernetes cluster administrator and have access to the cluster configuration file (kube config file), you can configure the current context to point to the targeted Kubernetes cluster. In this case, you can login with `azdata login -n <namespaceName>`, where `namespace` is the big data cluster name. You would be prompted for credentials if not specified within the login command.
   
1. Run [azdata bdc endpoint list](../azdata/reference/reference-azdata-bdc-endpoint.md) to get a list with a description of each endpoint and their corresponding IP address and port values. 

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

You can also get all the service endpoints deployed for the cluster by running the following `kubectl` command:

```bash
kubectl get svc -n <your-big-data-cluster-name>
```

## <a id="status"></a> Verify the cluster status

After deployment, you can check the status of the cluster with the [azdata bdc status show](../azdata/reference/reference-azdata-bdc-status.md) command.

```bash
azdata bdc status show
```

> [!TIP]
> To run the status commands, you must first log in with the `azdata login` command, which was shown in the previous endpoints section.

The following shows sample output from this command:

```output
Bdc: ready                                                                                                                                                                                                          Health Status:  healthy
 ===========================================================================================================================================================================================================================================
 Services: ready                                                                                                                                                                                                     Health Status:  healthy
 -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 Servicename    State    Healthstatus    Details

 sql            ready    healthy         -
 hdfs           ready    healthy         -
 spark          ready    healthy         -
 control        ready    healthy         -
 gateway        ready    healthy         -
 app            ready    healthy         -


 Sql Services: ready                                                                                                                                                                                                 Health Status:  healthy
 -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 Resourcename    State    Healthstatus    Details

 master          ready    healthy         StatefulSet master is healthy
 compute-0       ready    healthy         StatefulSet compute-0 is healthy
 data-0          ready    healthy         StatefulSet data-0 is healthy
 storage-0       ready    healthy         StatefulSet storage-0 is healthy


 Hdfs Services: ready                                                                                                                                                                                                Health Status:  healthy
 -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 Resourcename    State    Healthstatus    Details

 nmnode-0        ready    healthy         StatefulSet nmnode-0 is healthy
 storage-0       ready    healthy         StatefulSet storage-0 is healthy
 sparkhead       ready    healthy         StatefulSet sparkhead is healthy


 Spark Services: ready                                                                                                                                                                                               Health Status:  healthy
 -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 Resourcename    State    Healthstatus    Details

 sparkhead       ready    healthy         StatefulSet sparkhead is healthy
 storage-0       ready    healthy         StatefulSet storage-0 is healthy


 Control Services: ready                                                                                                                                                                                             Health Status:  healthy
 -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 Resourcename    State    Healthstatus    Details

 controldb       ready    healthy         -
 control         ready    healthy         -
 metricsdc       ready    healthy         DaemonSet metricsdc is healthy
 metricsui       ready    healthy         ReplicaSet metricsui is healthy
 metricsdb       ready    healthy         StatefulSet metricsdb is healthy
 logsui          ready    healthy         ReplicaSet logsui is healthy
 logsdb          ready    healthy         StatefulSet logsdb is healthy
 mgmtproxy       ready    healthy         ReplicaSet mgmtproxy is healthy


 Gateway Services: ready                                                                                                                                                                                             Health Status:  healthy
 -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 Resourcename    State    Healthstatus    Details

 gateway         ready    healthy         StatefulSet gateway is healthy


 App Services: ready                                                                                                                                                                                                 Health Status:  healthy
 -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 Resourcename    State    Healthstatus    Details

 appproxy        ready    healthy         ReplicaSet appproxy is healthy
```

You can also get more detailed status with the following commands:

- [azdata bdc control status show](../azdata/reference/reference-azdata-bdc-control-status.md) returns health status for all components associated with the control management service
```
azdata bdc control status show
```
Sample output:
```output
Control: ready                                                                                                                                                                                                      Health Status:  healthy
 ===========================================================================================================================================================================================================================================
 Resources: ready                                                                                                                                                                                                    Health Status:  healthy
 -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 Resourcename    State    Healthstatus    Details

 controldb       ready    healthy         -
 control         ready    healthy         -
 metricsdc       ready    healthy         DaemonSet metricsdc is healthy
 metricsui       ready    healthy         ReplicaSet metricsui is healthy
 metricsdb       ready    healthy         StatefulSet metricsdb is healthy
 logsui          ready    healthy         ReplicaSet logsui is healthy
 logsdb          ready    healthy         StatefulSet logsdb is healthy
 mgmtproxy       ready    healthy         ReplicaSet mgmtproxy is healthy
```

- `azdata bdc sql status show` returns health status for all resources that have a SQL Server service
```
azdata bdc sql status show
```
Sample output:
```output
Sql: ready                                                                                                                                                                                                          Health Status:  healthy
 ===========================================================================================================================================================================================================================================
 Resources: ready                                                                                                                                                                                                    Health Status:  healthy
 -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 Resourcename    State    Healthstatus    Details

 master          ready    healthy         StatefulSet master is healthy
 compute-0       ready    healthy         StatefulSet compute-0 is healthy
 data-0          ready    healthy         StatefulSet data-0 is healthy
 storage-0       ready    healthy         StatefulSet storage-0 is healthy
```

> [!IMPORTANT]
> When using `--all` parameter the output from these commands contain URLs to Kibana and Grafana dashboards for more detailed analysis.

In addition to using [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)], you can also use Azure Data Studio to find both endpoints and status information. For more information about viewing cluster status with [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] and Azure Data Studio, see [How to view the status of a big data cluster](view-cluster-status.md).

## <a id="connect"></a> Connect to the cluster

For more information on how to connect to the big data cluster, see [Connect to a SQL Server big data cluster with Azure Data Studio](connect-to-big-data-cluster.md).

## Next steps

To learn more about SQL Server Big Data Cluster deployment, see the following resources:

- [Configure deployment settings for big data clusters](deployment-custom-configuration.md)
- [Perform an offline deployment of a SQL Server big data cluster](deploy-offline.md)
- [Workshop: Microsoft [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] Architecture](https://github.com/microsoft/sqlworkshops-bdc)
- [Big Data Clusters FAQ](big-data-cluster-faq.yml)  
