---
title: What's new in SQL Server Language Extensions?
titleSuffix: 
description: Learn about what's new SQL Server Language Extensions. 
author: dphansen
ms.author: davidph 
ms.date: 11/05/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: language-extensions
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# What's new in SQL Server Language Extensions?
[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]

[Language Extension](language-extensions-overview.md) capabilities are added to SQL Server in each release as we continue to expand, extend, and deepen the integration between external languages and the data platform. 

## New in SQL Server 2019 

This release adds the support for Language Extensions in SQL Server. For more information about all of the features in this release, see [What's New in SQL Server 2019](../sql-server/what-s-new-in-sql-server-ver15.md) and [Release Notes for SQL Server 2019](../sql-server/sql-server-ver15-release-notes.md).

- The default Java Runtime on Windows and Linux is Open Zulu JRE and is included with the [SQL Server Language Extensions installation on Windows](install/install-sql-server-language-extensions-on-windows.md) and [SQL Server Language Extensions installation on Linux](../linux/sql-server-linux-setup-language-extensions.md).
- Supported [Java data types](how-to/java-to-sql-data-types.md).
- [CREATE EXTERNAL LANGUAGE](../t-sql/statements/create-external-language-transact-sql.md) for registering external language (for example, Java) in SQL Server.
- [Microsoft Extensibility SDK for Java](how-to/extensibility-sdk-java-sql-server.md).
- On Windows and Linux, Java code can be accessed in an external library using the [CREATE EXTERNAL LIBRARY (Transact-SQL)](../t-sql/statements/create-external-library-transact-sql.md) statement. Learn more: [How to call Java from SQL Server](how-to/call-java-from-sql.md).
- [Java language extension](language-extensions-overview.md) on  Windows and Linux. You can make compiled Java code available to SQL Server by assigning permissions and setting the path. Client apps with access SQL Server can use data and run your code by calling [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql), the same procedure used for R and Python integration on SQL Server Machine Learning Services.

## Next steps

+ Install [SQL Server Language Extensions on Windows](install/install-sql-server-language-extensions-on-windows.md) or [on Linux](../linux/sql-server-linux-setup-language-extensions.md)
