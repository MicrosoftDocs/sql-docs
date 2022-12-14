---
title: What is Java Language Extension?
titleSuffix: SQL Server Language Extensions
description: Java Language Extension is a feature of SQL Server used for executing external Java code. Relational data can be used in the external Java code using the extensibility framework.
author: rothja
ms.author: jroth
ms.date: 11/10/2020
ms.topic: overview
ms.service: sql
ms.subservice: language-extensions
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15"
ms.custom:
  - intro-overview
---
# What is Java Language Extension?
[!INCLUDE [SQL Server 2019 and later](../includes/applies-to-version/sqlserver2019.md)]

Java Language Extension is a feature of SQL Server used for executing external Java code. The relational data can be used in the external Java code using the [extensibility framework](concepts/extensibility-framework.md). The Java Language Extension is part of [SQL Server Language Extensions](language-extensions-overview.md).

The default Java runtime is Zulu Open JRE. You can also use another Java JRE or SDK.

## What you can do with the Java Language Extension

The Java Language Extension uses the extensibility framework for executing external Java code. Code execution is isolated from the core engine processes, but fully integrated with SQL Server query execution. You can execute Java code at the data's source, eliminating the need to pull data across the network.

The external Java language is defined with [CREATE EXTERNAL LANGUAGE](../t-sql/statements/create-external-language-transact-sql.md). The system stored procedure [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) is used as the interface for executing the Java code.

## Get started with Java Language Extension

1. [Install SQL Server Java Language Extension on Windows](install/windows-java.md) or [on Linux](../linux/sql-server-linux-setup-language-extensions-java.md).

1. Configure a development tools.

    + Use the IDE you prefer for developing Java code.
    + Install the [Microsoft Extensibility SDK for Java](how-to/extensibility-sdk-java-sql-server.md) to execute Java code on SQL Server.
    + Use [Azure Data Studio](../azure-data-studio/what-is-azure-data-studio.md) for executing external code on SQL Server.
    + Use the system stored procedure [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) to execute your Java code on SQL Server.

1. Write your first Java code.

    + [Tutorial: Regular expressions with Java](tutorials/search-for-string-using-regular-expressions-in-java.md)

## Limitations

The number of values in input and output buffers can't exceed `MAX_INT (2^31-1)` since that is the maximum number of elements that can be allocated in an array in Java.

## Next steps

+ Install the [SQL Server Java Language Extension on Windows](install/windows-java.md) or [on Linux](../linux/sql-server-linux-setup-language-extensions-java.md)
+ Install the [Microsoft Extensibility SDK for Java](how-to/extensibility-sdk-java-sql-server.md)
