---
title: Deploy applications with azdata
titleSuffix: SQL Server Big Data Clusters
description: Deploy a Python or R script as an application on SQL Server 2019 big data cluster.
author: cloudmelon 
ms.author: melqin
ms.reviewer: mikeray
ms.metadata: seo-lt-2019
ms.date: 12/13/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# How to deploy an app on SQL Server Big Data Clusters

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

Applications deployed on SQL Server Big Data Clusters (BDC) not only benefit from many advantages such as the computational power of the cluster but also access massive data that is available on the cluster. It dramatically improve the performance since your app sits in the same cluster where the data lives.

This article describes how to deploy and manage R and Python script as an application inside a SQL Server Big Data Cluster.

## What's new and improved

- A single command-line utility to manage cluster and app.
- Simplified app deployment while providing granular control through spec files.
- Support hosting additional application types - SQL Server Integration Services (SSIS) and MLeap.
- [Visual Studio Code Extension](app-deployment-extension.md) to manage application deployment.

Applications are deployed and managed using `azdata` command-line utility. This article provides examples of how to deploy apps from the command line. To learn how to use this in Visual Studio Code refer to [Visual Studio Code Extension](app-deployment-extension.md).

The following types of apps are supported:

- **Python** - One of the most popular general programming languages for various personas such as Data Engineers, Data Scientists or DevOps engineers, supports numerous scenarios such as data wrangling,  automation, prototyping, to some extent,  it also increasingly used to program enterprise-grade application working in conjunction with web development frameworks such as Flask and Django to address different business requirements.  
- **R** – Another popular programming language for Data Engineering and Data Scientists. Compared to Python, R is a programming language with more specific focus on statistical computing and graphics.  
- **SQL Server Integration Services (SSIS)**  -  high-performance data integration solutions for building and debugging ETL packages, it uses Data Transformation Services Package File Format (DTSX) which is an XML-based file format that stores the instructions for the processing of migrating data between databases and the integration of external data sources.   
- **MLeap** - is a common serialization format and provides everything needed to execute and serialize SparkML pipelines and others that can then be loaded at runtime to process ML scoring tasks in near real-time and close to the data.  

## Prerequisites

- [SQL Server 2019 big data cluster](deployment-guidance.md)
- [azdata command-line utility](deploy-install-azdata.md)

## Capabilities

In SQL Server 2019 you can create, delete, describe, initialize, list run and update your application. The following table describes the application deployment commands that you can use with **azdata**.

|Command |Description |
|:---|:---|
|`azdata login` | Sign into a SQL Server big data cluster |
|`azdata app create` | Create application. |
|`azdata app delete` | Delete application. |
|`azdata app describe` | Describe application. |
|`azdata app init` | Kickstart new application skeleton. |
|`azdata app list` | List application(s). |
|`azdata app run` | Run application. |
|`azdata app update`| Update application. |

You can get help with the `--help` parameter as in the following example:

```bash
azdata app create --help
```

The following sections describe these commands in more detail.

## Sign in

Before you deploy or interact with applications, first sign in to your SQL Server big data cluster with the `azdata login` command. Specify the external IP address of the `controller-svc-external` service (for example: `https://ip-address:30080`) along with the user name and password to the cluster.

```bash
azdata login --controller-endpoint https://<ip-address-of-controller-svc-external>:30080 --controller-username <user-name>
```

## Azure Kubernetes Service (AKS)

If you're using AKS, you need to run the following command to get the IP address of the `controller-svc-external` service by running this command in a bash or cmd window:


```bash
kubectl get svc controller-svc-external -n <name of your big data cluster>
```

## Kubernetes clusters created with kubeadm

Run the following command to get the IP address to sign in to the cluster

```bash
kubectl get node --selector='node-role.kubernetes.io/master'
```

## Create an app skeleton

The **azdata app init** command provides a scaffold with the relevant artifacts that is required for deploying an app. The example below creates add-app you can do this by running the following command.

```bash
azdata app init --name add-app --version v1 --template python
```

This will create a folder called hello.  You can use `cd` command into the directory and inspect the generated files in the folder. spec.yaml defines the app, such as name, version, and source code. You can edit the spec to change name, version, input, and outputs.

Here is a sample output from the init command that you'll see in the folder

```
add-app.py
run-spec.yaml
spec.yaml
```

## Create an app

To create an application, you use `azdata` with the `app create` command. These files reside locally on the machine that you're creating the app from.

Use the following syntax to create a new app in big data cluster:

```bash
azdata app create --spec <directory containing spec file>
```

The following command shows an example of what this command might look like:

```bash
azdata app create --spec ./addpy
```

This assumes that you have your application stored in the `addpy` folder. This folder should also contain a specification file for the application, called `spec.yaml`. For more information, see [the Application Deployment page](concept-application-deployment.md)  on the `spec.yaml` file.

To deploy this app sample app, create the following files in a directory called `addpy`:

- `add.py`. Copy the following Python code into this file:
   ```py
   #add.py
  def add(x, y):
    result = x+y
    return result
  result=add(x,y)
   ```
- `spec.yaml`. Copy the following code into this file:
   ```yaml
   #spec.yaml
   name: add-app #name of your python script
   version: v1  #version of the app
   runtime: Python #the language this app uses (R or Python)
   src: ./add.py #full path to the location of the app
   entrypoint: add #the function that will be called upon execution
   replicas: 1  #number of replicas needed
   poolsize: 1  #the pool size that you need your app to scale
   inputs:  #input parameters that the app expects and the type
     x: int
     y: int
   output: #output parameter the app expects and the type
     result: int
   ```

Then, run the command below:

```bash
azdata app create --spec ./addpy
```

You can check if the app is deployed using the list command:

```bash
azdata app list
```

If the deployment isn't completed, you should see the `state` show `WaitingforCreate` as the following example:

```json
[
  {
    "name": "add-app",
    "state": "WaitingforCreate",
    "version": "v1"
  }
]
```

After the deployment is successful, you should see the `state` change to `Ready` status:

```json
[
  {
    "name": "add-app",
    "state": "Ready",
    "version": "v1"
  }
]
```

## List an app

You can list any apps that were successfully created with the `app list` command.

The following command lists all available applications in your big data cluster:

```bash
azdata app list
```

If you specify a name and version, it lists that specific app and its state (Creating or Ready):

```bash
azdata app list --name <app_name> --version <app_version>
```

The following example demonstrates this command:

```bash
azdata app list --name add-app --version v1
```

You should see output similar to the following example:

```json
[
  {
    "name": "add-app",
    "state": "Ready",
    "version": "v1"
  }
]
```

## Delete an app

To delete an app from your big data cluster, use the following syntax:

```bash
azdata app delete --name add-app --version v1
```

## Next steps

Explore how to integrate apps deployed on [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in your own applications at [Run applications on big data clusters](app-run.md) and [Consume applications on big data clusters](app-consume.md) for more information. You can also check out additional samples at [App Deploy Samples](https://aka.ms/sql-app-deploy).

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).
