---
title: How to deploy an app on SQL Server big data cluster | Microsoft Docs
description: Deploy a Python or R script as an application on SQL Server 2019 big data cluster (preview). 
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 11/02/2018
ms.topic: conceptual
ms.prod: sql
---

# How to deploy an app on SQL Server 2019 big data cluster (preview)

This article describes how to deploy and manage R and Python applications inside a SQL Server 2019 Big Data Cluster (preview). 

R and Python applications are deployed and managed with the **mssqlctl-pre** command-line utility, which is included in CTP 2.1. This article provides examples of how to perform common tasks for big data cluster apps from the command line.

## Prerequisites

-	You must have a SQL Server 2019 big data cluster configured. For more information, see [How to deploy SQL Server big data cluster on Kubernetes](deployment-guidance.md). 

## Installation

Install the **mssqlctl-pre** tool with the following command:

   ```cmd
   pip3 install --index-url https://private-repo.microsoft.com/python/ctp-2.1 mssqlctl-pre
   ```

## Capabilities

In CTP 2.1 you can  create, delete, list, and run an R or Python application. The following table describes the application deployment commands that you can use with **mssqlctl-pre**.

| Command | Description |
|---|---|
| `mssqlctl-pre login` | Log into a SQL Server big data cluster |
| `mssqlctl-pre app create` | Create an app |
| `mssqlctl-pre app list` | List deployed apps |
| `mssqlctl-pre app delete` | Delete an app |
| `mssqlctl-pre app run` | List running apps |

> [!NOTE]
> Some commands are not supported in CTP 2.1.

You can get help for any command with the `--help` parameter. The following example displays help for the `app create` command:

```cmd
mssqlctl-pre app create --help
```

The following sections provide more details on each command.

## Log in

Before configuring R and Python applications, first log into your SQL Server big data cluster with the `mssqlctl-pre login` command. Specify the IP address of the service-proxy-lb (for example, `https://<ip-address>:30777`) along with the user name and password to the cluster.

1. Obtain the IP address of the service-proxy-lb service by running the following command in a bash or cmd window:

   ```bash
   kubectl get svc service-proxy-lb -n <name of your cluster>
   ```

1. Call  `mssqlctl-pre login` with the service-proxy-lb. Specify your user name and password for the big data cluster you are connecting to.

   ```bash
   mssqlctl-pre login -e https://<ip-address-of-service-proxy-lb> -u <user-name> -p <password>
   ```

## Create an app

To create an application, you pass Python or R code files to `mssqlctl-pre` with the `app create` command. These files reside locally on the machine that you are creating the app from.

Use the following syntax to create a new app in your big data cluster:

```cmd
mssqlctl-pre app create -n <app_name> -v <version_number> -r <runtime> -i <path_to_code_init> -c <path_to_code> --inputs <input_params> --outputs <output_params> --fileshare
```

The following command shows an example of what this command might look like:

```cmd
mssqlctl-pre app create --name testapp --version v1 --runtime Python --code ./testapp.py --init ./init.py --inputs x=float,y=float --outputs result=float 
```

## List an app

You can list any apps that were successfully created with the `app list` command.

The following command lists all available applications in your big data cluster:

```cmd
mssqlctl-pre app list
```

If you specify a name and version, it will list that specific app and its state (Creating or Ready):

```cmd
mssqlctl-pre app list --name <app_name> --version <app_version>
```

The following example demonstrates this command:

```cmd
mssqlctl-pre app list --name testapp --version v1
```

## Delete an app

To delete an app from your big data cluster, use the following syntax:

```cmd
mssqlctl-pre app delete --name <app_name> --version <app_version>
```

## Run an app

If the app is in a "Ready" state, you can use it by running it with your specified input parameters. Use the `app run` command with the following syntax to run an app:

```cmd
mssqlctl-pre app run --name <app_name> --version <app_version> --inputs <inputs_params>
```

The following example command demonstrates the run command:

```cmd
mssqlctl-pre app run --name testapp --version v1 --inputs x=1,y=2
```

If the run was successful, you should see your output as specified when you created the app.

## Next steps

For more information about SQL Server big data clusters, see [What are SQL Server 2019 big data clusters?](big-data-cluster-overview.md).
