---
title: "Deploy and consume analytics using mrsdeploy | Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2017"
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

# Deploy and consume analytics using mrsdeploy

Microsoft R Server includes an operationalization feature, **mrsdeploy**, that supports these tasks:

+ Publishing and managing R and Python models and code in the form of web services
+ Consuming these services within client applications

This topic provides information about how to enable and configure the feature.

For general information about scenarios supported by operationalization using Microsoft R Server, see [Operationalization with R Server](https://docs.microsoft.com/r-server/what-is-operationalization.

## Using mrsdeploy for operationalization

The word *operationalization* can mean many things:

+ The ability to publish models to a web service for use by applications
+ Support for scalable or distributed computing
+ Develop once, deploy many times
+ Fast scoring, for both single-row and batch scoring

If you have installed Machine Learning Services with SQL Server, *operationalization* is a matter of wrapping your R or Python code in a stored procedure. Any application can then call the stored procedure to retrain a model, generate scores, or create reports. You can also automate jobs using existing scheduling mechanisms in SQL Server.

However, Microsoft R Server provides a different mechanism for deployment support, using web services that support publishing of R jobs, and an administrative utility for running distributed R jobs. Microsoft R Server uses the functions in the **mrsdeploy** package to establish a session with remote compute nodes and execute R code in a console application. 

This deployment feature of R Server provides these benefits:

+ Role-based access control to analytical web services

    Determine who can publish, update, and delete their own web services, those who can also update and delete the web services published by other users, and who can only list and consume web services. Learn more about [roles](https://msdn.microsoft.com/microsoft-r/operationalize/security-roles.md).

+ Faster scoring
  
  You can use realtime scoring with a supported R model object to improve the speed of scoring operations.

+ Publish Python code as a web service

  For examples, see [Publish and consume Python code](./python/publish-consume-python-code.md).

+ Asynchronous batch consumption

  Web services that call for large input data can now be consumed asynchronously via batch execution.

+ Autoscaling of a grid of web and compute nodes

  A script template is provided to let you easily spin up a set of R Server VMs in Azure, and then configure them as a grid for operationalizing analytics and remote execution. This grid can be scaled up or down based on CPU usage.

+ Asynchronous remote execution

    Now supported using the **mrsdeploy** R package. To continue working in your development environment during remote script execution, execute your R script asynchronously using the `async` parameter. This is particularly useful when you are running scripts that have long execution times.

## Requirements and configuration

SQL Server 2017 CTP 2.0 and later includes this feature, which was previously available only with R Server, and not installed with SQL Server R Services. The **mrsdeploy** package is installed on the SQL Server computer, if you select the option to install **Microsoft Machine Learning Server**, from the **Shared Features** section of SQL Server setup.

Typically, we do not recommend that you install Machine Learning Server on the same computer that is running SQL Server Machine Learning Services. We recommend that you install **Microsoft Machine Learning Server** on a separate computer from SQL Server, and then configure the operationalization features on that computer. 

However, if you need to install them together, follow these additional steps to successfully configure the service.

1. Install DotNetCore 1.1

    If .NET Core was not installed as part of SQL Server, you must install it before beginning R Server setup.

2. Install Microsoft Machine Learning Server.

3. After completing setup of **Microsoft Machine Learning Server**, manually add the following registry key for  **mrsdeploy**, which specifies the base folder for the R_SERVER files. 

    + Create a new registry key `H_KEY_LOCAL_MACHINE\SOFTWARE\R Server\Path`
    + Set the value of the key to `"C:\Program Files\Microsoft SQL Server\140\R_SERVER"`.

4. When done, open the [Administrator Utility](https://docs.microsoft.com/r-server/operationalize/configure-use-admin-utility). 

5. Continue to configure the **mrsdeploy** service as described here: [Configuration for administrators](https://docs.microsoft.com/r-server/operationalize/configure-start-for-administrators)

6. For more information, see [mrsdeploy functions](https://docs.microsoft.com/r-server/r-reference/mrsdeploy/mrsdeploy-package).