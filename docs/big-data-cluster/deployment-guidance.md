---
title: Deployment guidance
titleSuffix: SQL Server big data clusters
description: Learn how to deploy SQL Server 2019 big data clusters (preview) on Kubernetes.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 04/24/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
ms.custom: seodec18
---

# How to deploy SQL Server big data clusters on Kubernetes

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

SQL Server big data cluster can be deployed as docker containers on a Kubernetes cluster. This is an overview of the setup and configuration steps:

- Set up Kubernetes cluster on a single VM, cluster of VMs, or in Azure Kubernetes Service (AKS).
- Install the cluster configuration tool **mssqlctl** on your client machine.
- Deploy SQL Server big data cluster in a Kubernetes cluster.

[!INCLUDE [Limited public preview note](../includes/big-data-cluster-preview-note.md)]

## Deploy SQL Server 2019 big data tools

Before deploying SQL Server 2019 big data cluster, first [install the big data tools](deploy-big-data-tools.md):

- **mssqlctl**
- **kubectl**
- **Azure Data Studio**
- **SQL Server 2019 extension**

## <a id="prereqs"></a> Kubernetes cluster prerequisites

SQL Server big data clusters require a minimum Kubernetes version of at least v1.10 for both server and client (kubectl).

> [!NOTE]
> Note that the client and server Kubernetes versions should be +1 or -1 minor version. For more information, see [Kubernetes supported releases and component skew](https://github.com/kubernetes/community/blob/master/contributors/design-proposals/release/versioning.md#supported-releases-and-component-skew).

## <a id="kubernetes"></a> Kubernetes cluster setup

If you already have a Kubernetes cluster that meets above prerequisites, then you can skip directly to the [deployment step](#deploy). This section assumes a basic understanding of Kubernetes concepts.  For detailed information on Kubernetes, see the [Kubernetes documentation](https://kubernetes.io/docs/home).

You can choose to deploy Kubernetes in any of three ways:

| Deploy Kubernetes on: | Description | Link |
|---|---|---|
| **Azure Kubernetes Services (AKS)** | A managed Kubernetes container service in Azure. | [Instructions](deploy-on-aks.md) |
| **Multiple machines (kubeadm)** | A Kubernetes cluster deployed on physical or virtual machines using **kubeadm** | [Instructions](deploy-with-kubeadm.md) |
| **Minikube** | A single-node Kubernetes cluster in a VM. | [Instructions](deploy-on-minikube.md) |

> [!TIP]
> For a sample python script that deploys both AKS and a SQL Server big data cluster in one step, see [Quickstart: Deploy SQL Server big data cluster on Azure Kubernetes Service (AKS)](quickstart-big-data-cluster-deploy.md).

## Verify Kubernetes configuration

Run the **kubectl** command to view the cluster configuration. Ensure that kubectl is pointed to the correct cluster context.

```bash
kubectl config view
```

After you have configured your Kubernetes cluster, you can proceed with the deployment of a new SQL Server big data cluster. If you are upgrading from a previous release, please see [How to upgrade SQL Server big data clusters](deployment-upgrade.md).

## <a id="deploy"></a> Default deployments

Use the following steps to deploy a big data cluster with default configuration settings. If you would rather customize your deployment, see the sections on [customized deployments](#configfile) and [unattended deployments](#unattended) in this article.

1. From the command line, start the deployment with `mssqlctl cluster create`.

   ```bash
   mssqlctl cluster create
   ```

1. After accepting the licensing terms, enter the configuration that corresponds to your Kubernetes environment:

   | Configuration | Description |
   |---|---|
   | **1. aks-dev-test.json** | Azure Kubernetes Service (AKS) |
   | **2. kubeadm-dev-test.json** | Multiple machines (kubeadm) |
   | **3. minikube-dev-test.json** | minikube |

   These configuration files contain default values that are appropriate for most dev-test scenarios. If you want more control over the deployment, see the [customized deployments](#configfile) section.

1. Follow the prompts to enter credentials for the cluster.

   > [!NOTE]
   > The Docker information is provided to you by Microsoft as part of the SQL Server 2019 Early Adoption Program. To request access, register [here](https://aka.ms/eapsignup), and specify your interest to try SQL Server big data clusters. Microsoft will triage all requests and respond as soon as possible.

After the deployment starts successfully, proceed to [monitoring your deployment](#monitor).

## <a id="configfile"></a> Customized deployments

Starting with CTP 2.5, most deployment settings are now configured in a JSON deployment configuration file. There are default deployment profiles for AKS, kubeadm, and minikube.

### List default deployment profiles

First, find the name of one of these standard deployment profiles using the `mssqlctl cluster config list` command:

```bash
mssqlctl cluster config list
```

This command returns configuration files such as the following:
- **aks-dev-test.json**
- **kubeadm-dev-test.json**
- **minikube-dev-test.json**

### Create a copy of a deployment profile

To customize your deployment, start with one of these default configurations that match your Kubernetes environment. Create a copy of the deployment profile with the `mssqlctl cluster config init` command. For example, the following command creates a copy of the **aks-dev-test.json** deployment configuration file in the current directory:

```bash
mssqlctl cluster config init --type aks-dev-test.json --name aks-customized.json
```

### Customize settings

To customize settings in your deployment configuration file, it is best to use the `mssqlctl cluster config section set` command rather than manually editing the file. For example, the following command alters a custom configuration file to change the name of the deployed cluster from the default (**mssql-cluster**) to **test-cluster**:

```bash
mssqlctl cluster config section set --config-file aks-customized.json --json-values "metadata.name=test-cluster"
```

In addition to passing key-value pairs, you can also provide inline JSON values or a path to a JSON file with the values. You can also use the `--patch-file` parameter to provide path to a JSON patch file. For more information, see [JSON Patches in Python](https://github.com/stefankoegl/python-json-patch) and the [JSONPath Online Evaluator](https://jsonpath.com/).

### Deploy big data cluster

To deploy the big data cluster, pass the deployment configuration file to the `mssqlctl cluster create` command. This initializes the Kubernetes namespace and deploys all the application pods into the namespace.

```bash
mssqlctl cluster create --configfile <deployment-profile.json> --accept-eula yes
```

During the deployment, you are prompted for any settings that were not in the deployment profile for security reasons. After the deployment starts successfully, proceed to [monitoring your deployment](#monitor).

## <a id="unattended"></a> Unattended deployments

A big data cluster typically involves several prompts for more information. For an unattended installation, you can pass a configuration file and use the following environment variables:

| Environment variable | Description |
|---|---|---|---|
| **DOCKER_REGISTRY** | The private registry where the images used to deploy the cluster are stored. |
| **DOCKER_REPOSITORY** | The private repository within the above registry where images are stored. |
| **DOCKER_USERNAME** | The username to access the container images in case they are stored in a private repository. |
| **DOCKER_PASSWORD** | The password to access the above private repository. |
| **DOCKER_IMAGE_TAG** | The label used to tag the images. Defaults to `latest`. |
| **CONTROLLER_USERNAME** | The username for the cluster administrator. |
| **CONTROLLER_PASSWORD** | The password for the cluster administrator. |
| **KNOX_PASSWORD** | The password for Knox user. |
| **MSSQL_SA_PASSWORD** | The password of SA user for SQL master instance. |

> [!IMPORTANT]
>- For the duration of the limited private preview, credentials for the private Docker registry will be provided to you upon triaging your [EAP registration](https://aka.ms/eapsignup).
>- Make sure you wrap the passwords in double quotes if it contains any special characters. You can set the MSSQL_SA_PASSWORD to whatever you like, but make sure they are sufficiently complex and don't use the `!`, `&` or `'` characters. Note that double quotes delimiters work only in bash commands.
>- The name of your cluster must be only lower case alpha-numeric characters, no spaces. All Kubernetes artifacts (containers, pods, statefull sets, services) for the cluster will be created in a namespace with same name as the cluster name specified.
>- The **SA** account is a system administrator on the SQL Server master instance that gets created during setup. After creating your SQL Server container, the MSSQL_SA_PASSWORD environment variable you specified is discoverable by running echo $MSSQL_SA_PASSWORD in the container. For security purposes, change your SA password as per best practices documented [here](../linux/quickstart-install-connect-docker.md#sapassword).

To perform an unattended installation, provide a deployment configuration file (default or custom) and specify the environment variable with the `--env-var` parameter. The following `mssqlctl cluster create` command uses the default **aks-dev-test.json** configuration and shows the format for specifying the environment variables:

```bash
mssqlctl cluster create --config-file aks-dev-test.json --accept-eula yes --env-var CONTROLLER_USERNAME=admin,CONTROLLER_PASSWORD=<password>,DOCKER_REGISTRY=<docker-registry>,DOCKER_REPOSITORY=<docker-repository>,MSSQL_SA_PASSWORD=<password>,KNOX_PASSWORD=<password>,DOCKER_USERNAME=<docker-username>,DOCKER_PASSWORD=<docker-password>,DOCKER_IMAGE_TAG=latest
```

## <a id="monitor"></a> Monitor the deployment

During cluster bootstrap, the client command window will output the deployment status. During the deployment process, you should see a series of messages where it is waiting for the controller pod:

```output
2019-04-12 14:40:10.0129 UTC | INFO | Waiting for controller pod to be up...
```

After 15 to 30 minutes, you should be notified that the controller pod is running:

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

1. Log in to the big data cluster with `mssqlctl login`. Set the `--endpoint` parameter to the Management Proxy.

   ```bash
   mssqlctl login --endpoint https://<ip-address>:30777
   ```

   Specify the username and password that you configured for the controller during deployment.

1. Run `mssqlctl cluster endpoints list` to get a list with a description of each endpoint and their corresponding IP address and port values. For example, the following displays the output for the Management Portal endpoint:

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

1. All cluster endpoints are also outlined in the **Service Endpoints** tab in the Cluster Administration Portal. You can access the portal using the Management Portal endpoint in the previous step (for example, `https://\<ip-address>:30777/portal`). The credentials for accessing the administration portal are the values for the controller username and password that you specified during deployment. You can also use the Cluster Administration Portal to monitor the deployment.

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

To learn more about the SQL Server big data clusters, see the following resources:

- [What are SQL Server 2019 big data clusters?](big-data-cluster-overview.md)
- [Workshop: Microsoft SQL Server big data clusters Architecture](https://github.com/Microsoft/sqlworkshops/tree/master/sqlserver2019bigdataclusters)