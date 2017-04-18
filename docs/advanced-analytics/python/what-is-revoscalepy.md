---
title: "What is revoscalepy | Microsoft Docs"
ms.custom: ""
ms.date: "04/16/2017"
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
# What is revoscalepy

**revoscalepy** (note: all lower case) is a new API provided by Microsoft to support distributed computing, remote compute contexts, and high-performance algorithms for Python.

It is based on the **RevoScaleR** package for R, which was provided in Microsoft R Server and SQL Server R Services. It provides much the same features:

+ Multiple compute contexts
+ Functions equivalent to those in RevoScaleR for data transformation and visualization
+ Scalable, fast machine learning algorithms that provide distributed or parallel processing
+ Use of the Intel math libraries

> [!NOTE]
> Python support is a new feature in SQL Server vNext and is in prerelease.

## Versions and Supported Platforms

The **revoscalepy** library is available only when you install one of the following Microsoft products:

+ Machine Learning Services, in SQL Server vNext CTP 2.0
+ Microsoft Machine Learning Server 9.1.0, using SQL Server vNext CTP 2.0 setup

## Examples

You can run code that includes **revoscalepy** functions in two scenarios:

+ Execute a Python script from the command line, or from a Python development environment, and call a remote compute context
+ Embed Python code in the stored procedure, sp_execute_externa_script, and call the stored procedure using T-SQL

[!NOTE]
> To run Python code in SQL Server, you must have installed SQL Server vNext and enabled the feature **Machine Learning Services** with Python. Other versions of SQL Server do not support Python integration. 
>
> Open source distributions of Python do not support SQL Server integration. However, you can install Microsoft Machine Learning Server to publish and consume Python applications from Windows without installing SQL Server. For more information, see [Create a Standalone R Server](../r/create-a-standalone-r-server.md)

### Using remote compute contexts

This example demonstrates how to run Python using an instance of SQL server as the compute context.

[Create a Model using revoscalepy](../tutorials/use-python-revoscalepy-to-create-model.md)

### Using T-SQL

This example demonstrates how to run Python using an instance of SQL server as the compute context.

[Run Python using T-SQL](../tutorials/run-python-using-t-sql.md)

## Function List

More information on specific functions will be published soon.
