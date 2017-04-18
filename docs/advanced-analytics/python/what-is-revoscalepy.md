---
title: "What is RevoScalePy | Microsoft Docs"
ms.custom: ""
ms.date: "03/30/2017"
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

The **revoscalepy** package for Python contains objects, transformation, and algorithms similar to those provided for R in the RevoScaleR package. With this library, you can create a compute context, move data between compute contexts, transform data, and train predictive models using popular algorithms such as logistic and linear regression, decision trees, and more.

> [!NOTE]
> This section is under development.
> 
> Python support is a new feature in SQL Server vNext and is in prerelease. Look for more information soon.

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

> [!NOTE]
> More information on specific functions will be published soon.