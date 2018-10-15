---
title: Deploy SQL Server big data cluster | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 10/01/2018
ms.topic: quickstart
ms.prod: sql
---

# Quickstart: Deploy SQL Server big data cluster on Azure Kubernetes Service (AKS)

Install SQL Server big data cluster on AKS in a default configuration suitable for dev/test environments. In addition to a SQL Master instance, the cluster includes one compute pool instance, one data pool instance, and two storage pool instances. Data is persisted using Kubernetes persistent volumes that use AKS default storage classes. To further customize your configuration, see the environment variables at [deployment guidance](deployment-guidance.md).

[!INCLUDE [Limited public preview note](../includes/big-data-cluster-preview-note.md)]

## Prerequisites

This quickstart requires that you have already configured an AKS cluster with a minimum version of v1.10. For more information, see the [deploy on AKS](deploy-on-aks.md) guide.

On the computer you are using to run the commands to install the SQL Server big data cluster, install [kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/). SQL Server big data cluster requires a minimum 1.10 version for Kubernetes, for both server and client (kubectl). To install kubectl, see [Install kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-kubectl). 

To install the `mssqlctl` CLI tool to manage the SQL Server Big Data cluster on your client machine, you must first install [Python](https://www.python.org/downloads/) minimum version v3.0 and [pip3](https://pip.pypa.io/en/stable/installing/). `pip` is already installed if you are using a Python version of at least 3.4 downloaded from [python.org](https://www.python.org/).

If your Python installation is missing the `requests` package, you must install `requests` using `python -m pip install requests`. If you already have a `requests` package, upgrade it to latest version using `python -m pip install requests --upgrade`.

## Verify AKS configuration

Once you have the AKS cluster deployed, you can execute the below kubectl command to view the cluster configuration. Ensure that kubectl is pointed to the correct cluster context.

```bash
kubectl config view
```

## Install mssqlctl CLI management tool

Run below command to install `mssqlctl` tool on your client machine. The command works from both a Windows and a Linux client, but make sure you are running it from a cmd window that runs with administrative privileges on Windows or prefix it with `sudo` on Linux:

```
pip3 install --index-url https://private-repo.microsoft.com/python/ctp-2.0 mssqlctl  
```

## Define environment variables

Setting the environment variables required for deploying big data cluster slightly differs depending on whether you are using Windows or Linux/macOS client.  Choose the steps below depending on which operating system you are using.

Before continuing, note the following important guidelines:

- In the [Command Window](http://docs.microsoft.com/visualstudio/ide/reference/command-window), quotes are included in the environment variables. If you use quotes to wrap a password, the quotes are included in the password.
- In bash, quotes are not included in the variable. Our examples use double quotes `"`.
- You can set the password environment variables to whatever you like, but make sure they are sufficiently complex and don’t use the `!`, `&`, or `'` characters.
- For the CTP 2.0 release, do not change the default ports.
- The `sa` account is a system administrator on the SQL Server Master instance that gets created during setup. After creating your SQL Server container, the `MSSQL_SA_PASSWORD` environment variable you specified is discoverable by running `echo $MSSQL_SA_PASSWORD` in the container. For security purposes, change your `sa` password as per best practices documented [here](https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-2017#change-the-sa-password).

Initialize the following environment variables.  They are required for deploying a big data cluster:

### Windows

Using a Command Window (not PowerShell), configure the following environment variables:

```cmd
SET ACCEPT_EULA=Y
SET CLUSTER_PLATFORM=aks

SET CONTROLLER_USERNAME=<controller_admin_name – can be anything>
SET CONTROLLER_PASSWORD=<controller_admin_password – can be anything, password complexity compliant>
SET KNOX_PASSWORD=<knox_password – can be anything, password complexity compliant>
SET MSSQL_SA_PASSWORD=<sa_password_of_master_sql_instance, password complexity compliant>

SET DOCKER_REGISTRY=private-repo.microsoft.com
SET DOCKER_REPOSITORY=mssql-private-preview
SET DOCKER_USERNAME=<your username, credentials provided by Microsoft>
SET DOCKER_PASSWORD=<your password, credentials provided by Microsoft>
SET DOCKER_EMAIL=<your Docker email, use the username provided by Microsoft>
SET DOCKER_PRIVATE_REGISTRY="1"
```

### Linux/macOS

Initialize the following environment variables:

```bash
export ACCEPT_EULA="Y"
export CLUSTER_PLATFORM="aks"

export CONTROLLER_USERNAME="<controller_admin_name – can be anything>"
export CONTROLLER_PASSWORD="<controller_admin_password – can be anything, password complexity compliant>"
export KNOX_PASSWORD="<knox_password – can be anything, password complexity compliant>"
export MSSQL_SA_PASSWORD="<sa_password_of_master_sql_instance, password complexity compliant>"

export DOCKER_REGISTRY="private-repo.microsoft.com"
export DOCKER_REPOSITORY="mssql-private-preview"
export DOCKER_USERNAME="<your username, credentials provided by Microsoft>"
export DOCKER_PASSWORD="<your password, credentials provided by Microsoft>"
export DOCKER_EMAIL="<your Docker email, use the username provided by Microsoft>"
export DOCKER_PRIVATE_REGISTRY="1"
```

> [!NOTE]
> During the limited public preview, Docker credentials to download the SQL Server Big Data cluster images are provided to each customer by Microsoft. To request access, register [here](https://aka.ms/eapsignup), and specify your interest to try SQL Server big data clusters.

## Deploy a big data cluster

To deploy a SQL Server 2019 CTP 2.0 big data cluster on your Kubernetes cluster, run the following command:

```bash
mssqlctl create cluster <name of your cluster>
```

> [!NOTE]
> The name of your cluster needs to be only lower case alpha-numeric characters, no spaces. All Kubernetes artifacts for the big data cluster will be created in a namespace with same name as the cluster name specified.


The command window or shell returns the deployment status. You can also check the deployment status by running these commands in a different cmd window:

```bash
kubectl get all -n <name of your cluster>
kubectl get pods -n <name of your cluster>
kubectl get svc -n <name of your cluster>
```

You can see a more granular status and configuration for each pod by running:
```bash
kubectl describe pod <pod name> -n <name of your cluster>
```

Once the Controller pod is running, you can use the Cluster Administration Portal to monitor the deployment. You can access the portal using the external IP address and port number for the `service-proxy-lb` (for example: **https://\<ip-address\>:30777**). Credentials for accessing the admin portal are the values of `CONTROLLER_USERNAME` and `CONTROLLER_PASSWORD` environment variables provided above.

You can get the IP address of the service-proxy-lb service by running this command in a bash or cmd window:

```bash
kubectl get svc service-proxy-lb -n <name of your cluster>
```

> [!NOTE]
> You will see a security warning when accessing the web page since we are using auto-generated SSL certificates. In future releases, we will provide the capability to provide your own signed certificates.
 

## Connect to the big data cluster

After the deployment script has completed successfully, you can obtain the IP address of the SQL Server master instance and the Spark/HDFS end points using the steps outlined below. All cluster endpoints are displayed in the Service Endpoints section in the Cluster Administration Portal as well for easy reference.

Azure provides the Azure LoadBalancer service to AKS. Run following command in a cmd or bash window:

```bash
kubectl get svc service-master-pool-lb -n <name of your cluster>
kubectl get svc service-security-lb -n <name of your cluster>
```

Look for the **External-IP** value that is assigned to the services. Connect to the SQL Server master instance using the IP address for the `service-master-pool-lb` at port 31433 (Ex: **\<ip-address\>,31433**) and to the SQL Server big data cluster endpoint using the external-IP for the `service-security-lb` service.   That big data cluster end point is where you can interact with HDFS and submit Spark jobs through Knox.

## Next steps

Now that the SQL Server big data cluster is deployed, try out some of the new capabilities:

> [!div class="nextstepaction"]
> [How to use notebooks in SQL Server 2019 preview](notebooks-guidance.md)
