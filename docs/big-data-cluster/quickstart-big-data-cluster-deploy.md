---
title: Deploy SQL Server Big Data Cluster | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 08/27/2018
ms.topic: quickstart
ms.prod: sql
---

# Quickstart: Deploy SQL Server Big Data Cluster on Kubernetes

In this quickstart, you will install SQL Server Big Data Cluster on Kubernetes in a default configuration suitable for dev/test environments. Here is the list of environment variables and their default values you can update to customize your cluster configuration:
TBD


## Prerequisites

This quickstart requires that you have already configured a Kubernetes cluster. For more information, see the Kubernetes section in the deployment guide:

- [Configure a Kubernetes cluster](deployment-guidance.md#kubernetes).


## Verify kubernetes configuration

Execute the below kubectl command to view the cluster configuration. Ensure that kubectl is pointed to the correct cluster context.

```bash
kubectl config view
```

## Install mssqlctl CLI management tool

TBD

## Define environment variables

Setting the environment variables required for deploying Big Data Cluster differs depending on whether you are using Windows or Linux client.  Choose the steps below depending on which operating system you are using.

> [!IMPORTANT]
> Make sure you wrap the passwords in double quotes if it contains any special characters.
>
> You can set the MSSQL passwords to whatever you like, but make sure they are sufficiently complex and don’t use the ! & or ‘ characters.

> [!NOTE]
> For the CTP 2.0 release do not change the default ports.

Initialize the following environment variables, they are required for deploying the Big Data Cluster:

### Windows

Using a CMD window (not PowerShell), configure the following environment variables:

```cmd
SET CLUSTER_PLATFORM=<minikube or acs or aks>
SET CLUSTER_NODE_REPLICAS=<number_of_nodes_excluding_master>

SET CONTROLLER_USERNAME=<controller_admin_name – can be anything>
SET CONTROLLER_PASSWORD=<controller_admin_password – can be anything, password complexity compliant>

SET DOCKER_REGISTRY=private-repo.microsoft.com
SET DOCKER_REPOSITORY=mssql-private-preview

SET MSSQL_SA_PASSWORD=<sa_password_of_master_sql_instances>

SET MASTER_SQL_PORT=31433
SET KNOX_PORT=30443
SET RANGER_PORT=30680
```

If you are using ACS or AKS, then set the following environment variable also:

```cmd
SET DOCKER_IMAGE_POLICY=IfNotPresent
```

### Linux

Initialize the following environment variables:

```bash
export CLUSTER_PLATFORM=<minikube or acs or aks>
export CLUSTER_NODE_REPLICAS=<number_of_nodes_excluding_master>

export CONTROLLER_USERNAME=<controller_admin_name – can be anything>
export CONTROLLER_PASSWORD=<controller_admin_password – can be anything, password complexity compliant>

export DOCKER_REGISTRY=private-repo.microsoft.com
export DOCKER_REPOSITORY=mssql-private-preview

export MSSQL_SA_PASSWORD=<sa_password_of_master_sql_instances>

export MASTER_SQL_PORT=31433
export KNOX_PORT=30443
export RANGER_PORT=30680
```

If you are using ACS or AKS, then set the following environment variable also:

```bash
export DOCKER_IMAGE_POLICY=IfNotPresent
```

## Deploy SQL Server 2019 CTP 2.0

To deploy SQL Server 2019 CTP 2.0 on your Kubernetes cluster, run the following command:

```bash
python mssqlctl.py create cluster <name of your cluster>
```

> [!NOTE]
> The name of your cluster needs to be only lower case alpha-numeric characters, no spaces. All Kubernetes artifacts for the Big Data Cluster will be created in a namespace with same name as the cluster name specified.


The command window will ouput the deployment status. You can also check the deployment status by running these commands in a different cmd window:

```bash
kubectl get all -n <name of your cluster>
kubectl get pods -n <name of your cluster>
kubectl get svc -n <name of your cluster>
```

You can see a more granular status and configuration for each pod by runnnig:
```bash
kubectl describe pod <pod name> -n <name of your cluster>
```

Once the Controller pod is running, you can use the Cluster Administration Portal to monitor the deployment. The portal will be launched automatically. 

## <a id="masterip"></a> Get the master instance IP address

After the deployment script has completed successfully, you can obtain the IP address of the SQL Server master instance using the steps outlined below. You will use this IP address and port number 31433 to connect to the SQL Server master instance (for example: **\<ip-address\>,31433**). Similarly, for the Knox Gateway endpoint. All cluster endpoints are outlined in the Service Endpoints tab in the Cluster Admin Portal as well.

### ACS/AKS

If you are using ACS/AKS, Azure provides the Azure LoadBalancer service to ACS. Run following command:

```bash
kubectl get svc service-master-lb -n <name of your cluster>
kubectl get svc service-security-lb -n <name of your cluster>
```

Look for the **External-IP** value that is assigned to the service. Then, connect to the SQL Server master instance using the IP address at port 31433 (Ex: **\<ip-address\>,31433**) and to Knox Gateway endpoint using the external-IP for `service-security-lb` service. 

### Minikube

If you are using Minikube, you need to run the following command to get the IP address you need to connect to. In addition to the IP, specify the port for the endpoint you need to connect to.

```bash
minikube ip
```

# Next steps

Now that SQL Server Big Data Cluster is deployed, try out some of the new capabilities:

> [!div class="nextstepaction"]
> [Get started with SQL Server Big Data Cluster](quickstart-big-data-cluster-get-started.md)
