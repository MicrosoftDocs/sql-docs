---
title: How to deploy an app on SQL Server big data cluster | Microsoft Docs
description: Deploy a Python or R script as an application on SQL Server 2019 big data cluster (preview). 
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 11/01/2018
ms.topic: conceptual
ms.prod: sql
---

# How to deploy an app on SQL Server 2019 big data cluster (preview)

This article describes how to deploy and manage R and Python applications inside a SQL Server 2019 Big Data Cluster (preview). 

R and Python applications are deployed and managed with the `mssqlctl-pre` command-line utility, which is included in CTP 2.1. This article provides examples of how to perform common tasks for big data cluster apps from the command line.



## Prerequisites

-	You must have a SQL Server 2019 big data cluster configured. For more information, see [How to deploy SQL Server big data cluster on Kubernetes](deployment-guidance.md). 

<!--
-	Install the latest version of Python. This dependency is the same as for the [mssqlctl utility](deployment-guidance.md#mssqlctl). 

	- On a Windows client, download the necessary Python package from [https://www.python.org/downloads/](https://www.python.org/downloads/). During installation, select to add Python to your path.

   - On Linux, install the **python3** and **python3-pip** packages. Then install **pip3** with `sudo pip3 install --upgrade pip`.

-->

## Installation

The `mssqlctl-pre` command-line utility is provided to preview the Python and R application deployment feature. 

<!--Because it is a preview, we recommend that you install it to a Python virtual environment using the following instructions:

1.	From a Windows PowerShell or bash command-line, install **virtualenv** with the following command (add `sudo` to the command on Linux):

   ```cmd
   pip install virtualenv
   ```

1.	Create a new directory for the virtual environment.

   ```cmd
   mkdir mssqlctl-pre-env
   cd mssqlctl-pre-env
   ```

1.	Create and activate the virtual environment in this directory.

   On Windows, run the following command:

   ```PowerShell
   python -m venv env
   ./env/scripts/activate
   ```

   On Linux, run the following command:

   ```bash
   python3 -m venv env
   source ./env/bin/activate
   ```
-->

## Install the mssqlctl-pre utility:

   ```cmd
   pip3 install --index-url https://private-repo.microsoft.com/python/ctp-2.0 mssqlctl-pre
   ```

## Capabilities

In CTP 2.1 you can  create, delete, list, and run an R or Python application.

## Log in

Before configuring R and Python applications, first log into your SQL Server big data cluster with the `mssqlctl-pre login` command. Specify the IP address of the [service-proxy-lb], (for example, https://<ip-address>:30777) along with the user name and password to the cluster.

You can get the IP address of the service-proxy-lb service by running this command in a bash or cmd window:

```bash
kubectl get svc service-proxy-lb -n <name of your cluster>
```

```bash
mssqlctl-pre login -e https://<ip-address-of-service-proxy-lb> -u <user-name> -p <password>
```
<!--
## Register a file share

You can optionally register a file share on Azure to use with an application that you want to publish. If you do not register a file share, you can reference code files locally when you create an application.

To register an Azure file share, first create the file share in the Azure portal with the following steps:

1. Log into the Azure portal.

2. If you do not have a storage account, create one by selecting **Storage accounts**, and then clicking the **Add** button.

3. After creating the storage account, select it to view the details.

4. On the left pane, select **Storage Exporer (preview)** to bring up the Storage Explorer for the target storage account.

5. Right-click on **File Shares**, and click **Create File Share**. Give it a name and then click **Create** to create the new share.

Now you can register this file share for a specific app that you intend to publish within a SQL Server big date cluster. From the command line, run the following command to register the file share:

```cmd
mssqlctl-pre file-share register -app <app_name> --secret <file_share_secret> --sharename <file_share_directory_name> --type AzureFiles
```

You can get help for Azure file share registration with the `--help` parameter:

```cmd
mssqlctl-pre file-share register --help
```
-->

## Command examples

- Create app

  ```cmd
  mssqlctl-pre app create
  ```

- List deployed apps

  ```cmd
  mssqlctl-pre app list
  ```

- Delete app

  ```cmd
  mssqlctl-pre app delete
  ```

- List running apps

  ```cmd
  mssqlctl-pre app run
  ```

Get help for app create with the `--help` parameter:

```cmd
mssqlctl-pre app create --help
```

>[!NOTE]
>Not all commands are supported in CTP 2.1

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

## Run an app

If the app is in a "Ready" state, you can use it by running it with your specified input parameters. Use the following syntax to run an app:

```cmd
mssqlctl-pre app run --name <app_name> --version <app_version> --inputs <inputs_params>
```

The following example command demonstrates the run command:

```cmd
mssqlctl-pre app run --name testapp --version v1 --inputs x=1,y=2
```

If the run was successful, you should see your output as specified when you created the app.

## Delete an app

To delete an app from your big data cluster, use the following syntax:

```cmd
mssqlctl-pre app delete --name <app_name> --version <app_version>
```

## Next steps

For more information about SQL Server big data clusters, see [What are SQL Server 2019 big data clusters?](big-data-cluster-overview.md).
