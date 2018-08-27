---
title: Deploy SQL Server Aris | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 08/27/2018
ms.topic: quickstart
ms.prod: sql
---

# Quickstart: Deploy SQL Server Aris on Kubernetes

In this quickstart, you will install SQL Server Aris on Kubernetes.

## Prerequisites

This quickstart requires that you have already configured a Kubernetes cluster. For more information, see the Kubernetes section in the deployment guide:

- [Configure a Kubernetes cluster](sql-server-aris-deployment-guidance.md#kubernetes).

## Clone the deployment project

1. If not already installed, install git locally on [Windows](https://git-for-windows.github.io/), [Linux, or Mac](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git).

1. First, in PowerShell, cmd, or a terminal window, navigate to the directory that you want to contain the deployment project files.

1. Then, run the following command to clone the files from GitHub to your machine:

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
> Make sure you wrap the passwords in double quotes if it contains any special characters.
>
> You can set the MSSQL passwords to whatever you like, but make sure they are sufficiently complex and don’t use the ! & or ‘ characters.

> [!NOTE]
> For the CTP 2.0 release do not change the default ports.

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

After the deployment script has completed successfully, you can obtain the IP address of the SQL Server master instance using the steps outlined below. You will use this IP address and port number 31433 to connect to the SQL Server master instance (for example: **\<ip-address\>,31433**).

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

# Next steps

Now that SQL Server Aris is deployed, try out some of the new capabilities:

> [!div class="nextstepaction"]
> [Get started with SQL Server Aris](quickstart-sql-server-aris-get-started.md)