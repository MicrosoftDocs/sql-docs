---
title: "Deploy and Consume Analytics | Microsoft Docs"
ms.custom: ""
ms.date: "04/12/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---

# Deploy and Consume Analytics


Microsoft R Server includes an operationalization feature that make it easier to:

+ Publish and manage R and Python models and code in the form of web services
+ Consume these services within client applications to affect business results

SQL Server 2017 CP 2.0 now includes this feature as an option, although it was not installed in previous versions of SQL Server R Services. This topic provides information about how to enable and configure the feature.
This topic provides information about how to enable and configure the feature.

## What's New in This Feature

+ Role-based access control to analytical web services.  These roles determine who can publish, update, and delete their own web services, those who can also update and delete the web services published by other users, and who can only list and consume web services. Learn more about [roles](https://msdn.microsoft.com/microsoft-r/operationalize/security-roles.md).

+ Scoring performance
  
  Use [real time scoring of web services](https://msdn.microsoft.com/microsoft-r/operationalize/data-scientist-manage-services.md#realtime) with a supported R model object to improve the speeed of scoring operations

+ Publish Python code as a web service

  For more information, see [Publish and consume Python code](./python/publish-consume-python-code.md).

+ [Asynchronous batch consumption](https://msdn.microsoft.com/microsoft-r/operationalize/data-scientist-batch-mode.md) for large input data

  Web services can now be consumed asynchronously via batch execution. 

+ Autoscaling of a grid of web and compute nodes on Azure

  A script template is provided to let you easily spin up a set of R Server VMs in Azure, and then configure them as a grid for operationalizing analytics and remote execution. This grid can be scaled up or down based on CPU usage.

+ [Asynchronous remote execution](https://msdn.microsoft.com/microsoft-r/operationalize/remote-execution.md#async)  now supported using the `mrsdeploy` R package

  To continue working in your development environment during the remote script execution, execute your R script asynchronously using the `async` parameter. This is particularly useful when you are running scripts that have long execution times.

## Background

The word *operationalization* can mean many things:

+ The ability to publish models to a web service for use by applications
+ Support for scalable or distributed computing
+ Develop once, deploy many times
+ Fast scoring, for both single-row and batch scoring

If you have installed Machine Learning Services with SQL Server, *operationalization* is a matter of wrapping your R or Python code in a stored procedure. Any application can then call the stored procedure to retrain a model, generate scores, or create reports. YOu can also automate jobs using existing scheduling mechanisms in SQL Server.

However, Microsoft Machine Learning Server provides support for deployment through web services that support publishing of R jobs, and an administrative utility for running distributed R jobs.

Typically, you would not install Machine Learning Server on the same computer that is running SQL Server Machine Learning Services. It is possible, but we recommend that you keep them separate. In other words, install **Microsoft Machine Learning Server** on a separate computer form SQL Server, and then configure the operationalization features on that computer.

For general information about scenarios supported by operationalization, see [Operationalization with R Server](https://msdn.microsoft.com/microsoft-r/operationalize/about).

**Deploying and consuming web services**
[For the Data Scientist](https://msdn.microsoft.com/microsoft-r/operationalize/data-scientist-get-started)
[For the Application Developer](https://msdn.microsoft.com/microsoft-r/operationalize/app-developer-get-started)
[For the Administrator](https://msdn.microsoft.com/microsoft-r/operationalize/admin-get-started)


## Requirements

The **mrsdeploy** package is installed when you use the option to install **Microsoft Machine Learning Server**, from the **Shared Features** section of SQL Server setup.

Microsoft R Server uses the functions in the **mrsdeploy** package to establish a session with remote compute nodes and execute R code in a console application. For more information, see [mrsdeploy functions](https://msdn.microsoft.com/microsoft-r/mrsdeploy/mrsdeploy).

To actually use the feature, some additional steps are required.

1. Install DotNetCore 1.1
    If .NET Core was not installed as part of SQL Server, you must install it before beginning R Server setup.
2. Install Microsoft Machine Learning Server.
3. After completing setup of **Microsoft Machine Learning Server**, you must manually add a registry key for use by **mrsdeploy**, that specifies the base folder for the R_SERVER files. In a default installation, that path is as follows: 
    `C:\Program Files\Microsoft SQL Server\140\R_SERVER`
    1. Create a new registry key as follows:
    `H_KEY_LOCAL_MACHINE\SOFTWARE\R Server\Path`
    2. Set the value of the key to `"C:\Program Files\Microsoft SQL Server\140\R_SERVER"`.
4. When done, open the Administrator Utility. For help, see [R Server help](https://msdn.microsoft.com/microsoft-r/operationalize/admin-utility#launch)
5. Configure the service as described here: [Configuring R Server for Operationalization](https://msdn.microsoft.com/en-us/microsoft-r/operationalize/configure-enterprise)

