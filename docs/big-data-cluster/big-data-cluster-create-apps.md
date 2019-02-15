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
 

## What's new and improved 

- A single command-line utility to manage cluster and app.
- Simplified app deployment while providing granular control through spec files.
- Support hosting additional application types - SSIS and MLeap (new in CTP 2.3)
- [VS Code Extension](app-deployment-extension.md) to manage application deployment

Applications are deployed and managed using **mssqlctl** command-line utility. This article provides examples of how to deploy apps from the command line. To learn how to use this in VS Code refer to [VS Code Extension](app-deployment-extension.md)

Types of apps supported 
R and Python apps
MLeap Serving
SSIS as a fully containerzied app as a scheduled service

## Prerequisites

- [SQL Server 2019 big data cluster](deployment-guidance.md). 

- [mssqlctl command-line utility](deploy-install-mssqlctl.md).


## Capabilities

In CTP 2.3 you can create, delete, describe, initialize, list run and update your application. The following table describes the application deployment commands that you can use with **mssqlctl**.
| Command | Description |
|---|---|
| `mssqlctl login` | Log into a SQL Server big data cluster |
|  `mssqlctl create` | Create application. |
|  `mssqlctl delete` | Delete application. |
|  `mssqlctl describe` | Describe application. |
|  `mssqlctl init` | Kickstart new application skeleton. |
|  `mssqlctl list` | List application(s). |
|  `mssqlctl run` | Run application. |
|  `mssqlctl update`| Update application. |


You can get help with the `--help` parameter as in the following example:

```bash
mssqlctl app create --help
```

The following sections describe these commands in more detail.

## Log in

Before you deploy or intereact with applications, first log into your SQL Server big data cluster with the `mssqlctl login` command. Specify the external IP address of the `service-proxy-lb` or `service-proxy-nodeport` services (for example: `https://ip-address:30777`) along with the user name and password to the cluster.

You can get the IP address of the **service-proxy-lb** or **service-proxy-nodeport** service by running this command in a bash or cmd window:

```bash 
kubectl get svc service-proxy-lb -n <name of your cluster>
```

```bash
mssqlctl login -e https://<ip-address-of-service-proxy-lb>:30777 -u <user-name> -p <password>
```

## Create an app

To create an application, you use the **mssqlctl** with the `app create` command. These files reside locally on the machine that you are creating the app from.

Use the following syntax to create a new app in your big data cluster:

```bash
mssqlctl app create -n <app_name> -v <version_number> --spec <path to spec file>
```

The following command shows an example of what this command might look like:

This assumes that you have file called spec.yaml within the addpy folder. 
The addpy folder contains the **add.py** and  **spec.yaml**. 
The **spec.yaml** is a specification file for the **add.py** app. 

```py

This python app is defined as

#add.py
def add(x,y):
        result = x+y
        return result;
result=add(x,y)


the spec.yaml file will be something like

#spec.yaml
name: add-app #name of your python script
version: v1  #version of the app 
runtime: Python #the languge this app uses (R or Python)
src: ./add.py #full path to the loction of the app
entrypoint: add #the function that will be called upon execution
replicas: 1  #number of replicas needed
poolsize: 1  #the pool size that you need your app to scale
inputs:  #input parameters that the app expects and the type
  x: int
  y: int
output: #output parameter the app expects and the type
  result: int


```



To try this, copy the above lines of code into two files directory `addpy` as `add.py`  and `spec.yaml` and run the command below

```bash
mssqlctl app create --spec ./addpy
```

You can check if the app is deployed using the list command:

```bash
mssqlctl app list
```

If the deployment is not complete you should see the "state" show "WaitingforCreate": 

```
[
  {
    "name": "add-app",
    "state": "WaitingforCreate",
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
mssqlctl app list
```

If you specify a name and version, it will list that specific app and its state (Creating or Ready):

```bash
mssqlctl app list --name <app_name> --version <app_version>
```

The following example demonstrates this command:

```bash
mssqlctl app list --name add-app --version v1
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
mssqlctl app run --name <app_name> --version <app_version> --inputs <inputs_params>
```

The following example command demonstrates the run command:

```bash
mssqlctl app run --name add-app --version v1 --inputs x=1,y=2
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

## Describe an app

The describe command provides detailed information about the app including the end point in your cluster. This will be typically be used by an app developer to build an app using the swagger client and using the weservice to interact with the app in a restFul manner.

```
{
  "input_param_defs": [
    {
      "name": "x",
      "type": "int"
    },
    {
      "name": "y",
      "type": "int"
    }
  ],
  "links": {
    "app": "https://10.1.1.3:30777/api/app/add-app/v1",
    "swagger": "https://10.1.1.3:30777/api/app/add-app/v1/swagger.json"
  },
  "name": "add-app",
  "output_param_defs": [
    {
      "name": "result",
      "type": "int"
    }
  ],
  "state": "Ready",
  "version": "v1"
}

```


## Delete an app

To delete an app from your big data cluster, use the following syntax:

```bash
mssqlctl app delete --name add-app --version v1
```

## Next steps

You can also check out additional samples at [App Deploy Samples](https://aka.ms/sql-app-deploy).

For more information about SQL Server big data clusters, see [What are SQL Server 2019 big data clusters?](big-data-cluster-overview.md).
