---
title: Deployment guidance
titleSuffix: SQL Server big data clusters
description: Learn how to deploy SQL Server 2019 big data clusters (preview) on Kubernetes.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 04/23/2019
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
| **aks-dev-test.json** | Azure Kubernetes Service (AKS) |
| **kubeadm-dev-test.json** | Multiple machines (kubeadm) |
| **minikube-dev-test.json** | minikube |

You can deploy a big data cluster by running **mssqlctl cluster create**. This prompts you to choose one of the default configurations and then guides you through the deployment.

```bash
mssqlctl cluster create
```

> [!TIP]
> In this example, you are prompted for any settings that are not part of the default configuration, such as passwords. Note that the Docker information is provided to you by Microsoft as part of the SQL Server 2019 [Early Adoption Program](https://aka.ms/eapsignup).

## <a id="customconfig"></a> Custom configurations

It is also possible to customize your own deployment configuration file. You can do this with the following steps:

1. Start with one of the standard deployment profiles that match your Kubernetes environment. You can use the  **mssqlctl cluster config list** command to list them:

   ```bash
   mssqlctl cluster config list
   ```

1. To customize your deployment, create a copy of the deployment profile with the **mssqlctl cluster config init** command. For example, the following command creates a copy of the **aks-dev-test.json** deployment configuration file in the current directory:

   ```bash
   mssqlctl cluster config init --src aks-dev-test.json --target custom.json
   ```

1. To customize settings in your deployment configuration file, you can edit it in a tool that is good for editing json docs like VS Code. For scripted automation, you can edit the custom configuration file using **mssqlctl cluster config section set** command. For example, the following command alters a custom configuration file to change the name of the deployed cluster from the default (**mssql-cluster**) to **test-cluster**:  

   ```bash
   mssqlctl cluster config section set --config-file custom.json --json-values "metadata.name=test-cluster"
   ```

   > [!TIP]
   > A useful tool for finding JSON paths is the [JSONPath Online Evaluator](https://jsonpath.com/).

   In addition to passing key-value pairs, you can also provide inline JSON values or pass JSON patch files. For more information, see [Configure deployment settings for big data clusters](deployment-custom-configuration.md).

1. Then pass the custom configuration file to **mssqlctl cluster create**. Note that you must set the required [environment variables](#env), otherwise you will be prompted for the values:

   ```bash
   mssqlctl cluster create --config-file custom.json --accept-eula yes
   ```

> [!TIP]
> For more information on the structure of a deployment configuration file, see the [Deployment configuration file reference](reference-deployment-config.md). For more configuration examples, see [Configure deployment settings for big data clusters](deployment-custom-configuration.md).

## <a id="env"></a> Environment variables

The following environment variables are used for security settings that are not stored in a deployment configuration file. Note that Docker settings except credentials can be set in the configuration file.

| Environment variable | Description |
|---|---|---|---|
| **DOCKER_REGISTRY** | The private registry where the images used to deploy the cluster are stored. Use *private-repo.microsoft.com* for the ducration of gated public preview.|
| **DOCKER_REPOSITORY** | The private repository within the above registry where images are stored. Use *mssql-private-preview* for the duration of the gated public preview.|
| **DOCKER_USERNAME** | The username to access the container images in case they are stored in a private repository. |
| **DOCKER_PASSWORD** | The password to access the above private repository. |
| **DOCKER_IMAGE_TAG** | The label used to tag the images. Defaults to **latest**, but we recommend using the tag corresponding to the release to avoid version incompatibility issues. |
| **CONTROLLER_USERNAME** | The username for the cluster administrator. |
| **CONTROLLER_PASSWORD** | The password for the cluster administrator. |
| **KNOX_PASSWORD** | The password for Knox user. |
| **MSSQL_SA_PASSWORD** | The password of SA user for SQL master instance. |

These environment variables must be set prior to calling **mssqlctl cluster create**. If any variable is not set, you are prompted for it.

The following example shows how to set the environment variables for Linux (bash) and Windows (PowerShell):

```bash
export CONTROLLER_USERNAME=admin
export CONTROLLER_PASSWORD=<password>
export MSSQL_SA_PASSWORD=<password>
export KNOX_PASSWORD=<password>
export DOCKER_REGISTRY=private-repo.microsoft.com
export DOCKER_REPOSITORY=mssql-private-preview
export DOCKER_USERNAME=<docker-username>
export DOCKER_PASSWORD=<docker-password>
export DOCKER_IMAGE_TAG=ctp2.5
```

```PowerShell
SET CONTROLLER_USERNAME=admin
SET CONTROLLER_PASSWORD=<password>
SET MSSQL_SA_PASSWORD=<password>
SET KNOX_PASSWORD=<password>
SET DOCKER_REGISTRY=private-repo.microsoft.com
SET DOCKER_REPOSITORY=mssql-private-preview
SET DOCKER_USERNAME=<docker-username>
SET DOCKER_PASSWORD=<docker-password>
SET DOCKER_IMAGE_TAG=ctp2.5
```

Upon setting the environment variables, you must run `mssqlctl cluster create` to trigger the deployment. This example uses the cluster configuration file created above:

```
mssqlctl cluster create --config-file custom.json --accept-eula yes
```

Please note the following guidelines:

- At this time, credentials for the private Docker registry will be provided to you upon triaging your [Early Adoption Program registration](https://aka.ms/eapsignup). Early Adoption Program registration is required to test SQL Server big data clusters.
- Make sure you wrap the passwords in double quotes if it contains any special characters. You can set the **MSSQL_SA_PASSWORD** to whatever you like, but make sure the password is sufficiently complex and don't use the `!`, `&` or `'` characters. Note that double quotes delimiters work only in bash commands.
- The **SA** login is a system administrator on the SQL Server master instance that gets created during setup. After creating your SQL Server container, the **MSSQL_SA_PASSWORD** environment variable you specified is discoverable by running echo $MSSQL_SA_PASSWORD in the container. For security purposes, change your SA password as per best practices documented [here](../linux/quickstart-install-connect-docker.md#sapassword).
- The **DOCKER_IMAGE_TAG** in this example controls which release you are installing. In this example, it is the CTP 2.5 release.

## <a id="unattended"></a> Unattended install

For an unattended deployment, you must set all required environment variables, use a configuration file, and call `mssqlctl cluster create` command with the `--accept-eula yes` parameter. The examples in the previous section demonstrate the syntax for an unattended installation.

## <a id="monitor"></a> Monitor the deployment

During cluster bootstrap, the client command window will output the deployment status. During the deployment process, you should see a series of messages where it is waiting for the controller pod:

```output
2019-04-12 14:40:10.0129 UTC | INFO | Waiting for controller pod to be up...
```

In less than 15 to 30 minutes, you should be notified that the controller pod is running:

```output
2019-04-12 15:01:10.0809 UTC | INFO | Waiting for controller pod to be up. Checkthe mssqlctl.log file for more details.
2019-04-12 15:01:40.0861 UTC | INFO | Controller pod is running.
2019-04-12 15:01:40.0884 UTC | INFO | Controller Endpoint: https://<ip-address>:30080
```

> [!IMPORTANT]
> The entire deployment can take a long time due to the time required to download the container images for the components of the big data cluster. However, it should not take several hours. If you are experiencing problems with your deployment, see [Monitoring and troubleshoot SQL Server big data clusters](cluster-troubleshooting-commands.md).

When the deployment finishes, the output notifies you of success:

```output
2019-04-12 15:37:18.0271 UTC | INFO | Monitor and track your cluster at the Portal Endpoint: https://<ip-address>:30777/portal/
2019-04-12 15:37:18.0271 UTC | INFO | Cluster deployed successfully.
```

Note the URL of the **Portal Endpoint** in the previous output for use in the next section.

> [!TIP]
> The default name for the deployed big data cluster is `mssql-cluster` unless modified by a custom configuration.

## <a id="endpoints"></a> Retrieve endpoints

After the deployment script has completed successfully, you can obtain the IP addresses of the external endpoints for the big data cluster using the following steps.

1. From the deployment output, copy the **Portal Endpoint** and remove the `/portal/` at the end. This is the URL of the Management Proxy (for example, `https://<ip-address>:30777`).

   > [!TIP]
   > If you do not have your deployment output, you can get the IP address for the Management Proxy by looking at the EXTERNAL-IP output of the following **kubectl** command:
   >
   > ```bash
   > kubectl get svc mgmtproxy-svc-external -n <your-cluster-name>
   > ```

1. Log in to the big data cluster with **mssqlctl login**. Set the **--endpoint** parameter to the Management Proxy.

   ```bash
   mssqlctl login --endpoint https://<ip-address>:30777
   ```

   Specify the username and password that you configured for the controller (CONTROLLER_USERNAME and CONTROLLER_PASSWORD) during deployment.

1. Run **mssqlctl cluster endpoints list** to get a list with a description of each endpoint and their corresponding IP address and port values. For example, the following displays the output for the Management Portal endpoint:

   ```output
   {
     "description": "Management Portal",
     "endpoint": "https://<ip-address>:30777/portal",
     "ip": "<ip-address>",
     "name": "portal",
     "port": 30777,
     "protocol": "https"
   },
   ```

1. All cluster endpoints are also outlined in the **Service Endpoints** tab in the Cluster Administration Portal. You can access the portal using the Management Portal endpoint in the previous step (for example, `https://<ip-address>:30777/portal`). The credentials for accessing the administration portal are the values for the controller username and password that you specified during deployment. You can also use the Cluster Administration Portal to monitor the deployment.

### Minikube

If you are using minikube, you need to run the following command to get the IP address you need to connect to. In addition to the IP, specify the port for the endpoint you need to connect to.

```bash
minikube ip
```

Irrespective of the platform you are running your Kubernetes cluster on, to get all the service endpoints deployed for the cluster, run following command:

```bash
kubectl get svc -n <your-cluster-name>
```

## <a id="connect"></a> Connect to the cluster

For more information on how to connect to the big data cluster, see [Connect to a SQL Server big data cluster with Azure Data Studio](connect-to-big-data-cluster.md).

## Next steps

To learn more about big data cluster deployment, see the following resources:

- [Configure deployment settings for big data clusters](deployment-custom-configuration.md)
- [Perform an offline deployment of a SQL Server big data cluster](deploy-offline.md)
- [Workshop: Microsoft SQL Server big data clusters Architecture](https://github.com/Microsoft/sqlworkshops/tree/master/sqlserver2019bigdataclusters)
