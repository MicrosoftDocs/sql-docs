---
title: "R tools included with SQL Server setup | Microsoft Docs"
ms.custom: ""
ms.date: "10/31/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 7c04ae30-d391-4369-9742-d2b275e14c0d
caps.latest.revision: 9
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "Inactive"
---
# R tools included with SQL Server setup

When you install R with SQL Server, you get the same R tools that are installed with any **base** installation of R, such as RGui, Rterm, and so forth. Therefore technically, you have all the tools you need to develop and test R code.

This topic lists the tools included with installation.

> [!TIP]
> 
> Typically it is easier to debug and test R code by using a dedicated development environment. You'll find it easier to run your R code in SQL Server if you test it beforehand in an external tool, so that you can read detailed error messages and debug the solution.
> 
> See this article for a list of free tools that you can use to develop R solutions, and how to configure them to work with SQL Server: [Set up a data science client](set-up-a-data-science-client.md)

**Applies to:** SQL Server 2016 R Services (In-Database) and Microsoft R Server (Standalone); SQL Server 2017 Machine Learning Services (In-Database) and Machine Learning Server (Standalone)

## R tools included with installation

The following standard R tools are included in a *base installation* of R, and therefore are installed by default.

+ **RTerm**: A command-line terminal for running R scripts

+ **RGui.exe**:  A simple interactive editor for R. The command-line arguments are the same for RGui.exe and RTerm.

+ **RScript**: A command-line tool for running R scripts in batch mode.

To locate these tools, determine the R library that was installed when you set up SQL Server or the standalone machine learning feature. For example, in a default installation, the R tools are located in these folders:

+ SQL Server 2016 R Services: `~\Program Files\Microsoft SQL Server\MSSQL13.<instancename>\R_SERVICES\bin\x64`
+ Microsoft R Server Standalone: `~\Program Files\Microsoft R\R_SERVER\bin\x64`
+ SQL Server 2017 Machine Learning Services: `~\Program Files\Microsoft SQL Server\MSSQL14.<instancename>\R_SERVICES\bin\x64`
+ Machine Learning Server (Standalone): `~\Program Files\Microsoft\ML Server\R_SERVER\bin\x64`

If you need help with the R tools, just open **RGui**, click **Help**, and select one of the options.

## Introducing Microsoft R Client

Microsoft R Client is a free download that gives you access to the RevoScaleR packages for development use. By installing R Client, you can create R solutions that can be run in all supported compute contexts, including SQL Server in-database analytics, and distributed R computing on Hadoop, Spark, or Linux using Machine Learning Server.

If you have already installed a different R development environment, such as RStudio, be sure to reconfigure the environment to use the libraries and executables provided by Microsoft R Client. By doing so you can use all the features of the RevoScaleR package, although performance will be limited.

For more information, see [What is Microsoft R Client?](https://docs.microsoft.com/machine-learning-server/r-client/what-is-microsoft-r-client)
