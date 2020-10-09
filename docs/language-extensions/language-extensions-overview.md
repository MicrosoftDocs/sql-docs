---
title: What is SQL Server Language Extensions?
titleSuffix: 
description: Language Extensions is a feature of SQL Server used for executing external code. In SQL Server 2019, Java, Python, and R are supported. The relational data can be used in the external code using the extensibility framework.
author: dphansen
ms.author: davidph 
ms.date: 10/07/2020
ms.topic: overview
ms.prod: sql
ms.technology: language-extensions
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# What is SQL Server Language Extensions?
[!INCLUDE [SQL Server 2019 and later](../includes/applies-to-version/sqlserver2019.md)]

Language Extensions is a feature of SQL Server used for executing external code. The relational data can be used in the external code using the [extensibility framework](concepts/extensibility-framework.md).

In SQL Server 2019, Java, Python, and R are supported.

> [!NOTE]
> For executing Python or R in SQL Server, see the [SQL machine learning](../machine-learning/index.yml) documentation. With SQL Server 2019 and later, you can use a custom Python and R runtime with Language Extensions. For more information, see the [Python custom runtime](../machine-learning/install/custom-runtime-python.md) and the [R custom runtime](../machine-learning/install/custom-runtime-r.md).

## What you can do with Language Extensions

Language Extensions uses the extensibility framework for executing external code. Code execution is isolated from the core engine processes, but fully integrated with SQL Server query execution. You can execute code at the data's source, eliminating the need to pull data across the network.

External languages are defined with [CREATE EXTERNAL LANGUAGE](../t-sql/statements/create-external-language-transact-sql.md). The system stored procedure [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) is used as the interface for executing the code.

Language Extensions provides multiple advantages:

+ Data security. Bringing external language execution closer to the source of data avoids wasteful or insecure data movement.
+ Speed. Databases are optimized for set-based operations. Recent innovations in databases such as in-memory tables make summaries and aggregations lightning, and are a perfect complement to data science.
+ Ease of deployment and integration. [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] is the central point of operations for many other data management tasks and applications. By using data in the database, you ensure that the data used by the language extension is consistent and up-to-date.

## Next steps

+ Install the [Python custom runtime for SQL Server](../machine-learning/install/custom-runtime-python.md)
+ Install the [R custom runtime for SQL Server](../machine-learning/install/custom-runtime-r.md)
+ Install the [SQL Server Language Extensions on Windows](install/install-sql-server-language-extensions-on-windows.md) or [on Linux](../linux/sql-server-linux-setup-language-extensions.md)
+ Install the [Microsoft Extensibility SDK for Java](how-to/extensibility-sdk-java-sql-server.md)
