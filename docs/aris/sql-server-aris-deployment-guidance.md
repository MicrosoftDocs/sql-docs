---
title: How to deploy SQL Server Aris on Kubernetes | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 08/06/2018
ms.topic: conceptual
ms.prod: sql
---

# How to deploy SQL Server Aris on Kubernetes

SQL Server vNext can be deployed as docker containers on a Kubernetes cluster. This is an overview of the setup and configuration steps:

- Setup Kubernetes cluster on a single VM, cluster of VMs or in Azure Container Service
- Deploy SQL Server vNext CTP1.8 in a Kubernetes cluster
- Configure the SQL Server master instance

## Kubernetes prerequisistes

SQL Server vNext requires a minimum 1.10 version for Kubernetes, for both server and client. To install a specific version on kubectl client, see [Install kubectl binary via curl](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-kubectl).  Latest versions of minikube and ACS are at least 1.10. For ACS you will need to use --orchestrator-version parameter to specify a version different than default.

Also, note that the client/server version skew that is supported is +/-1 minor version. The Kubernetes documentation states that  "a client should be skewed no more than one minor version from the master, but may lead the master by up to one minor version. For example, a v1.3 master should work with v1.1, v1.2, and v1.3 nodes, and should work with v1.2, v1.3, and v1.4 clients." For more information, see [Kubernetes supported releases and component skew](https://github.com/kubernetes/community/blob/master/contributors/design-proposals/release/versioning.md#supported-releases-and-component-skew).

## <a id="kubernetes"></a> Kubernetes cluster setup

If you already have a Kubernetes cluster, then you can skip directly to the [deployment step](#deploy). This section assumes a basic understanding of Kubernetes concepts.  For detailed information on Kubernetes, see the [Kubernetes documentation](https://kubernetes.io/docs/home).

You can choose to deploy Kubernetes in any of three ways:

| Deploy Kubernetes on: | Description |
|---|---|
| **Minikube** | A single-node Kubernetes cluster in a VM. |
| **Azure Container Services (ACS)** | A managed Kubernetes cluster in Azure. |
| **Multiple VMs** | Please refer to **“/CTP1.8/documentation/k8s-deployment-multiple-vms.docx”** document in GitHub for instructions. |

For guidance on configuring one of these Kubernetes cluster options for SQL Server vNext, see one of the followin articles:

   - [Configure Minikube](sql-server-aris-deploy-on-minikube.md)
   - [Configure Kubernetes on Azure Container Service](sql-server-aris-deploy-on-acs.md)
   - [Configure Kubernetes on multiple VMs](sql-server-aris-deploy-on-vms.md)

## <a id="deploy"></a> Deploy SQL Server vNext

After you have configured your Kubernetes cluster, you can proceed with the deployment for SQL Server vNext. The deployment steps in the following sections can be repeated to recreate the environment if needed.

## Clone the deployment project

First, in PowerShell, cmd, or a terminal window, navigate to the directory that you want to contain the deployment project files.

Then, run the following command to clone the files from GitHub to your machine:

```bash
git clone https://github.com/Microsoft/sqlservervnext.git
```

## Verify kubernetes configuration

Execute the below kubectl command to view the cluster configuration. Ensure that kubectl is pointed to the correct cluster context.

```bash
kubectl config view
```

## Define environment variables

The deployment commands for SQL Server vNext are slightly different depending on whether you are using Windows or Linux.  Choose the steps below depending on which operating system you are using.

Initialize the following environment variables first. Enter the Docker username and password that have been provided to you.

> [!IMPORTANT]
> Make sure you wrap the passwords in double quotes if it containes any special characters.
>
> You can set the MSSQL passwords to whatever you like, but make sure they are sufficiently complex and don’t use the ! & or ‘ characters.

> [!NOTE]
> For the CTP 1.8 release do not change the default ports.

### Windows

Using a CMD window (not PowerShell), navigate to the **CTP1.8\kube\deployment\mssql-data-pool folder**. Configure the following environment variables:

```cmd
SET CLUSTER_PLATFORM=minikube or acs (or aks in future, but not this release)
SET CLUSTER_NODE_REPLICAS=<number_of_nodes_excluding_master>

SET CONTROLLER_USERNAME=<controller_admin_name – can be anything>
SET CONTROLLER_PASSWORD=<controller_admin_password – can be anything>

SET DOCKER_REGISTRY=private-repo.microsoft.com
SET DOCKER_REPOSITORY=mssql-private-preview
SET DOCKER_USERNAME=<your_user_name>
SET DOCKER_PASSWORD=<your_password>
SET DOCKER_EMAIL=<your_email>
SET DOCKER_PRIVATE_REGISTRY="1"

SET HADOOP_IMAGE=<mssql-hadoop (Default, Apache Spark) or mssql-hadoop-dbr (Databricks)>

SET MSSQL_SA_PASSWORD=<sa_password_of_sql_instances>
SET MSSQL_SECURITY_SA_PASSWORD=<sa_password_of_internal_sql_instance>
SET MSSQL_IMPORT_PASSWORD=<import_account_password_of_sql_instances>
SET MSSQL_EXTERNAL_PASSWORD=<external_account_password_of_sql_instances>

SET MASTER_SQL_PORT=31433
SET KNOX_PORT=30443
SET RANGER_PORT=30680
```

If you are using ACS, then set the following environment variable also:

```cmd
SET DOCKER_IMAGE_POLICY=IfNotPresent
```

### Linux

Navigate to the **CTP1.8/kube/deployment/mssql-data-pool** folder. Initialize the following environment variables:

```bash
export CLUSTER_PLATFORM=minikube or acs (or aks in future, but not this release)
export CLUSTER_NODE_REPLICAS=<number_of_nodes_excluding_master>

export CONTROLLER_USERNAME=<controller_admin_name – can be anything>
export CONTROLLER_PASSWORD=<controller_admin_password – can be anything>

export DOCKER_REGISTRY=private-repo.microsoft.com
export DOCKER_REPOSITORY=mssql-private-preview
export DOCKER_USERNAME=<your_user_name>
export DOCKER_PASSWORD=<your_password>
export DOCKER_EMAIL=<your_email>
export DOCKER_PRIVATE_REGISTRY="1"

export HADOOP_IMAGE=<mssql-hadoop (Default) or mssql-hadoop-dbr (Databricks)>
export MSSQL_SA_PASSWORD=<sa_password_of_sql_instances>
export MSSQL_SECURITY_SA_PASSWORD=<sa_password_of_internal_sql_instance>
export MSSQL_IMPORT_PASSWORD=<import_account_password_of_sql_instances>
export MSSQL_EXTERNAL_PASSWORD=<external_account_password_of_sql_instances>

export MASTER_SQL_PORT=31433
export KNOX_PORT=30443
export RANGER_PORT=30680
```

If you are using ACS, then set the following environment variable also:

```bash
export DOCKER_IMAGE_POLICY=IfNotPresent
```

## Deploy SQL Server vNext

To deploy SQL Server vNext on your Kubernetes cluster, run the following command:

```bash
python mssqlctl.py create cluster <name of your cluster>
```

> [!NOTE]
> The name of your cluster needs to be only lower case alpha-numeric characters, no spaces.

You can ignore the initial error messages about various resources not being found. This step can take several minutes to run.

While the command window is showing the status, you can check the deployment status by running these commands in a different cmd window:

```bash
kubectl describe pod mssql-data-pool-master-0 -n <name of your cluster>
kubectl describe pod mssql-data-pool-node-0 -n <name of your cluster
```

There will probably be multiple data pool pods number 0-n.  You can run the above command to see the status of each of them as they are deploying by changing the number at the end.

## <a id="masterip"></a> Get the master instance IP address

After the deployment script has completed successfully, you can obtain the IP address of the SQL Server master instance using the steps outlined below.

### ACS

If you are using ACS, Azure provides the Azure LoadBalancer service to ACS. Run following command:

```bash
kubectl get svc service-master-lb -n <name of your cluster>
```

Look for the **External-IP** value that is assigned to the service. Then, connect to the SQL Server master instance using the IP address at port 31433 (Ex: **\<ip-address\>,31433**).

### Minikube

If you are using Minikube, you need to run the following command to get the IP address you need to connect to.

```bash
minikube ip
```

You will use this IP address and port number 31433 to connect to the SQL Server master instance (Ex: **\<ip-address\>,31433**).

## Next steps

After successfully deploying SQL Server vNext to Kubernetes, [install the big data tools](sql-server-aris-install-big-data-tools.md) and learn more in the [getting started quickstart](quickstart-sql-server-aris-get-started.md).