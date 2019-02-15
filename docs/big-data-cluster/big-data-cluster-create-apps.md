---
title: How to deploy an app
titleSuffix: SQL Server 2019 big data clusters
description: Deploy a Python or R script as an application on SQL Server 2019 big data cluster (preview). 
author: TheBharath 
ms.author: bharaths  
manager: craigg
ms.date: 12/11/2018
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
ms.custom: seodec18
---

# How to deploy an app on SQL Server 2019 big data cluster (preview)

This article describes how to deploy and manage R and Python script as an application inside a SQL Server 2019 big data cluster (preview).

R and Python applications are deployed and managed with the **mssqlctl-pre** command-line utility which is included in CTP 2.2. This article provides examples of how to deploy these R and Python scripts as apps from the command line.

## Prerequisites

You must have a SQL Server 2019 big data cluster configured. For more information, see [How to deploy SQL Server big data cluster on Kubernetes](deployment-guidance.md). 

## Installation

The **mssqlctl-pre** command-line utility is provided to preview the Python and R application deployment feature. Use the following command to install the utility:

```cmd
pip install -r https://private-repo.microsoft.com/python/ctp-2.2/mssqlctlpre/mssqlctlpre.txt --trusted-host https://private-repo.microsoft.com
```

## Capabilities

In CTP 2.2 you can create, delete, list, and run an R or Python application. The following table describes the application deployment commands that you can use with **mssqlctl-pre**.

| Command | Description |
|---|---|
| `mssqlctl-pre login` | Log into a SQL Server big data cluster |
| `mssqlctl-pre app create` | Create an app |
| `mssqlctl-pre app list` | List deployed apps |
| `mssqlctl-pre app delete` | Delete an app |
| `mssqlctl-pre app run` | List running apps |

You can get help with the `--help` parameter as in the following example:

```bash
mssqlctl-pre app create --help
```

The following sections describe these commands in more detail.

## Log in

Before configuring R and Python applications, first log into your SQL Server big data cluster with the `mssqlctl-pre login` command. Specify the external IP address of the `service-proxy-lb` or `service-proxy-nodeport` services (for example: `https://ip-address:30777`) along with the user name and password to the cluster.

You can get the IP address of the **service-proxy-lb** or **service-proxy-nodeport** service by running this command in a bash or cmd window:

```bash 
kubectl get svc service-proxy-lb -n <name of your cluster>
```

```bash
mssqlctl-pre login -e https://<ip-address-of-service-proxy-lb>:30777 -u <user-name> -p <password>
```

## Create an app

To create an application, you pass Python or R code files to **mssqlctl-pre** with the `app create` command. These files reside locally on the machine that you are creating the app from.

Use the following syntax to create a new app in your big data cluster:

```bash
mssqlctl-pre app create -n <app_name> -v <version_number> -r <runtime> -i <path_to_code_init> -c <path_to_code> --inputs <input_params> --outputs <output_params> 
```

The following command shows an example of what this command might look like:

```py
#add.py
def add(x,y):
        result = x+y
        return result;
result=add(x,y)
```
To try this, save the above lines of code to your local directory as `add.py` and run the command below

```bash
mssqlctl-pre app create --name add-app --version v1 --runtime Python --code ./add.py  --inputs x=int,y=int --outputs result=int 
```

You can check if the app is deployed using the list command:

```bash
mssqlctl-pre app list
```

If the deployment is not complete you should see the "state" show "Creating": 

```
[
  {
    "name": "add-app",
    "state": "Creating",
    "version": "v1"
  }
]
```

After the deployment is successful you should see the "state" change to "Ready" status:

```
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
mssqlctl-pre app list
```

If you specify a name and version, it will list that specific app and its state (Creating or Ready):

```bash
mssqlctl-pre app list --name <app_name> --version <app_version>
```

The following example demonstrates this command:

```bash
mssqlctl-pre app list --name add-app --version v1
```

You should see output similar to the following example:

```
[
  {
    "name": "add-app",
    "state": "Ready",
    "version": "v1"
  }
]
```

## Run an app

If the app is in a "Ready" state, you can use it by running it with your specified input parameters. Use the following syntax to run an app:

```bash
mssqlctl-pre app run --name <app_name> --version <app_version> --inputs <inputs_params>
```

The following example command demonstrates the run command:

```bash
mssqlctl-pre app run --name add-app --version v1 --inputs x=1,y=2
```

If the run was successful, you should see your output as specified when you created the app. The following is an example.

```
{
  "changedFiles": [],
  "consoleOutput": "",
  "errorMessage": "",
  "outputFiles": {},
  "outputParameters": {
    "result": 3
  },
  "success": true
}
```

## Delete an app

To delete an app from your big data cluster, use the following syntax:

```bash
mssqlctl-pre app delete --name add-app --version v1
```

## Next steps

You can also check out additional samples at [https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster). 

For more information about SQL Server big data clusters, see [What are SQL Server 2019 big data clusters?](big-data-cluster-overview.md).
