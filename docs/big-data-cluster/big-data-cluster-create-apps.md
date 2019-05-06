---
title: Deploy applications using mssqlctl
titleSuffix: SQL Server big data clusters
description: Deploy a Python or R script as an application on SQL Server 2019 big data cluster (preview).
author: jeroenterheerdt 
ms.author: jterh
ms.reviewer: jroth
manager: craigg
ms.date: 04/23/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
ms.custom: seodec18
---

# How to deploy an app on SQL Server big data cluster (preview)

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article describes how to deploy and manage R and Python script as an application inside a SQL Server 2019 big data cluster (preview).

## What's new and improved

- A single command-line utility to manage cluster and app.
- Simplified app deployment while providing granular control through spec files.
- Support hosting additional application types - SSIS and MLeap (new in CTP 2.3)
- [VS Code Extension](app-deployment-extension.md) to manage application deployment

Applications are deployed and managed using `mssqlctl` command-line utility. This article provides examples of how to deploy apps from the command line. To learn how to use this in Visual Studio Code refer to [VS Code Extension](app-deployment-extension.md).

The following types of apps are supported:
- R and Python apps (functions, models and apps)
- MLeap Serving
- SQL Server Integration Services (SSIS)

## Prerequisites

- [SQL Server 2019 big data cluster](deployment-guidance.md)
- [mssqlctl command-line utility](deploy-install-mssqlctl.md)

## Capabilities

In SQL Server 2019 (preview) CTP 2.5 you can create, delete, describe, initialize, list run and update your application. The following table describes the application deployment commands that you can use with **mssqlctl**.

|Command |Description |
|:---|:---|
|`mssqlctl login` | Sign into a SQL Server big data cluster |
|`mssqlctl app create` | Create application. |
|`mssqlctl app delete` | Delete application. |
|`mssqlctl app describe` | Describe application. |
|`mssqlctl app init` | Kickstart new application skeleton. |
|`mssqlctl app list` | List application(s). |
|`mssqlctl app run` | Run application. |
|`mssqlctl app update`| Update application. |

You can get help with the `--help` parameter as in the following example:

```bash
mssqlctl app create --help
```

The following sections describe these commands in more detail.

## Sign in

Before you deploy or interact with applications, first sign in to your SQL Server big data cluster with the `mssqlctl login` command. Specify the external IP address of the `mgmtproxy-svc-external` service (for example: `https://ip-address:30777`) along with the user name and password to the cluster.

```bash
mssqlctl login -e https://<ip-address-of-mgmtproxy-svc-external>:30777 -u <user-name> -p <password>
```

## AKS

If you are using AKS, you need to run the following command to get the IP address of the `mgmtproxy-svc-external` service by running this command in a bash or cmd window:


```bash
kubectl get svc mgmtproxy-svc-external -n <name of your cluster>
```

## Kubeadm or Minikube

If you are using Kubeadm or Minikube run the following command to get the IP address to login in to the cluster

```bash
kubectl get node --selector='node-role.kubernetes.io/master'
```

## Create an app

To create an application, you use `mssqlctl` with the `app create` command. These files reside locally on the machine that you are creating the app from.

Use the following syntax to create a new app in big data cluster:

```bash
mssqlctl app create --spec <directory containing spec file>
```

The following command shows an example of what this command might look like:

```bash
mssqlctl app create --spec ./addpy
```

This assumes that you have your application stored in the `addpy` folder. This folder should also contain a specification file for the application, called called `spec.yaml`. Please see [the Application Deployment page](concept-application-deployment.md) for more information on the `spec.yaml` file.

To deploy this app sample app, create the following files in a directory called `addpy`:

- `add.py`. Copy the following Python code into this file:
   ```py
   #add.py
   def add(x,y):
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
mssqlctl app create --spec ./addpy
```

You can check if the app is deployed using the list command:

```bash
mssqlctl app list
```

If the deployment is not complete you should see the `state` show `WaitingforCreate` as the following example:

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
mssqlctl app list
```

If you specify a name and version, it lists that specific app and its state (Creating or Ready):

```bash
mssqlctl app list --name <app_name> --version <app_version>
```

The following example demonstrates this command:

```bash
mssqlctl app list --name add-app --version v1
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

## Run an app

If the app is in a `Ready` state, you can use it by running it with your specified input parameters. Use the following syntax to run an app:

```bash
mssqlctl app run --name <app_name> --version <app_version> --inputs <inputs_params>
```

The following example command demonstrates the run command:

```bash
mssqlctl app run --name add-app --version v1 --inputs x=1,y=2
```

If the run was successful, you should see your output as specified when you created the app. The following is an example.

```json
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

## Create an app skeleton

The init command provides a scaffold with the relevant artifacts that is required for deploying an app. The example below creates  hello you can do this by running the following command.

```bash
mssqlctl app init --name hello --version v1 --template python
```

This will create a folder called hello.  You can `cd` into the directory and inspect the generated files in the folder. spec.yaml defines the app, such as name, version and source code. You can edit the spec to change name, version, input and outputs.

Here is a sample output from the init command that you will see in the folder

```
hello.py
README.md
run-spec.yaml
spec.yaml

```

## Describe an app

The describe command provides detailed information about the app including the end point in your cluster. This is typically used by an app developer to build an app using the swagger client and using the webservice to interact with the app in a RESTful manner. See [Consume applications on big data clusters](big-data-cluster-consume-apps.md) for more information.

```json
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

Explore how to integrate apps deployed on SQL Server big data clusters in your own applications at [Consume applications on big data clusters](big-data-cluster-consume-apps.md) for more information. You can also check out additional samples at [App Deploy Samples](https://aka.ms/sql-app-deploy).

For more information about SQL Server big data clusters, see [What are SQL Server 2019 big data clusters?](big-data-cluster-overview.md).
