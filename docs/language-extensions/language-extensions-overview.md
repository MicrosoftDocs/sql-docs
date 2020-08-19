---
title: What is SQL Server Language Extensions?
titleSuffix: 
description: Language Extensions is a feature of SQL Server used for executing external code. In SQL Server 2019, Java is supported. The relational data can be used in the external code using the extensibility framework.
author: dphansen
ms.author: davidph 
ms.date: 08/19/2020
ms.topic: overview
ms.prod: sql
ms.technology: language-extensions
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# What is SQL Server Language Extensions?
[!INCLUDE [SQL Server 2019 and later](../includes/applies-to-version/sqlserver2019.md)]

Language Extensions is a feature of SQL Server used for executing external code. The relational data can be used in the external code using the [extensibility framework](concepts/extensibility-framework.md).

In SQL Server 2019, Java is supported. The default Java runtime is Zulu Open JRE. You can also use another Java JRE or SDK.

> [!NOTE]
> For executing Python or R code in SQL Server, see the [Machine Learning Services](../machine-learning/sql-server-machine-learning-services.md) documentation.

## What you can do with Language Extensions

Language Extensions uses the extensibility framework for executing external code. Code execution is isolated from the core engine processes, but fully integrated with SQL Server query execution. They let you execute code where the data resides, eliminating the need to pull data across the network.

External languages are defined with [CREATE EXTERNAL LANGUAGE](https://docs.microsoft.com/sql/t-sql/statements/create-external-language-transact-sql). The system stored procedure [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) is used as the interface for executing the code.

Language Extensions provides multiple advantages:

+ Data security. Bringing external language execution closer to the source of data avoids wasteful or insecure data movement.
+ Speed. Databases are optimized for set-based operations. Recent innovations in databases such as in-memory tables make summaries and aggregations lightning, and are a perfect complement to data science.
+ Ease of deployment and integration. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is the central point of operations for many other data management tasks and applications. By using data that resides in the database, you ensure that the data used by Java is consistent and up-to-date.

## How to get started

### Step 1: Install the software

+ [SQL Server Language Extensions on Windows](install/install-sql-server-language-extensions-on-windows.md)
+ [SQL Server Language Extensions on Linux](../linux/sql-server-linux-setup-language-extensions.md)

### Step 2: Configure a development tool

Developers typically write code on their own laptop or development workstation. With language extensions in SQL Server, there is no need to change this process. After installation is complete, you can run Java code on SQL Server.

+ **Use the IDE you prefer** for developing Java code.

+ **Install the [Microsoft Extensibility SDK for Java](how-to/extensibility-sdk-java-sql-server.md)** to execute Java code on SQL Server

+ **Use [Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/what-is) or [SQL Server Management Studio](https://docs.microsoft.com/sql/ssms/sql-server-management-studio-ssms)** for executing external code on SQL Server

+ **Use the system stored procedure [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql)** to execute your Java code on SQL Server.

### Step 3: Write your first code

Execute Java code from within T-SQL script:

+ [Tutorial: Regular expressions with Java](tutorials/search-for-string-using-regular-expressions-in-java.md)

## Limitations

+ The number of values in input and output buffers cannot exceed `MAX_INT (2^31-1)` since that is the maximum number of elements that can be allocated in an array in Java.

## Next steps

+ Install [SQL Server Language Extensions on Windows](install/install-sql-server-language-extensions-on-windows.md) or [on Linux](../linux/sql-server-linux-setup-language-extensions.md)
+ Install the [Microsoft Extensibility SDK for Java](how-to/extensibility-sdk-java-sql-server.md)
