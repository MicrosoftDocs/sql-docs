---
title: How to deploy an app on SQL Server big data cluster | Microsoft Docs
description: Deploy a Python or R script as an application on SQL Server 2019 big data cluster (preview). 
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 10/25/2018
ms.topic: conceptual
ms.prod: sql
---

# How to deploy an app on SQL Server 2019 big data cluster (preview)

This article describes how to deploy and manage R and Python applications inside a SQL Server 2019 big data cluster (preview). 

R and Python applications are deployed and managed with the **mssqlctl-ctp** command-line utility. This article provides examples of how to perform common tasks for big data cluster apps from the command-line.

## Prerequisites

You must have a SQL Server 2019 big data cluster configured. For more information, see [How to deploy SQL Server big data cluster on Kubernetes](deployment-guidance.md).

## mssqlctl-ctp installation

The **mssqlctl-ctp** can be installed with the following command:

```bash
pip3 install --index-url https://private-repo.microsoft.com/python/ctp-2.0 mssqlctl-ctp
```

This command requires the same Python prerquisites as the **mssqlctl** tool. For more details about the prerequisites, see [Install mssqlctl](deployment-guidance.md#mssqlctl).


## Log in

Before configuring R and Python applications, first log into your SQL Server big data cluster with the `mssqlctl-ctp login` command. Specify the IP address of the HDFS/Spark gateway as well as your user name and password to the cluster.

```bash
mssqlctl-ctp login -e https://<ip-address-of-hdfs-spark-gateway> -u <user-name> -p <password>
```

## Fileshare 

Fileshare Register


Go to Azure Portal and create a new file share to use for associated apps

Azure Portal > Storage Accounts > New Storage Account

Once the storage account is created, you can go to the Storage Explorer and create a new File Share

Now you can test out registering this File Share for a specific app that you intend to publish

You can see all details for the Azure File Share registration with --help

mssqlctl-ctp file-share register --help
To register:

mssqlctl-ctp file-share register -app <app_name> --secret <file_share_secret> --sharename <file_share_directory_name> --type AzureFiles
Example (feel free to use my fileshare, it has mtcars.csv in it):

sq file-share register --app testapp --secret arise2e --type AzureFiles --sharename testshare1
App Create

You can see all details for app create with --help

mssqlctl-ctp app create --help 
You can either use your Azure File Storage spec that you registered above so you can reference those files in your code or not.

mssqlctl-ctp app create -n <app_name> -v <version_number> -r <runtime> -i <path_to_code_init> -c <path_to_code> --inputs <input_params> --outputs <output_params> --fileshare 
Example:

sq app create --name testapp --version v1 --runtime Python --code ./testapp.py --init ./init.py --inputs x=float,y=float --outputs result=float --fileshare
App List

If the app was successfully created, you should now be able to list it:

If you do not specify a name and version, it will list all available apps

mssqlctl-ctp app list
If you specify a name and version, it will list that specific app and its state

mssqlctl-ctp app list --name <app_name> --version <app_version>
Example:

sq app list --name testapp --version v1
App Run

If the app is in a "Ready" state, you should now be able to consume it with run by giving it your specified input parameters

mssqlctl-ctp app run --name <app_name> --version <app_version> --inputs <inputs_params>
Example:

sq app run --name testapp --version v1 --inputs x=1,y=2
If the run was successful, you should see your output as specified when you created the app

App Delete

You can now delete your app if you choose to:

mssqlctl-ctp app delete --name <app_name> --version <app_version>
Example:

sq app delete --name testapp --version v1