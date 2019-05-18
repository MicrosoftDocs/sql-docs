---
title: What's new is Language Extensions?
titleSuffix: SQL Server language extensions
description: Learn about what's new SQL Server 2019 Language Extensions (preview). 
author: dphansen
ms.author: davidph 
manager: cgronlun
ms.date: 05/22/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: language-extensions
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---
# What new in SQL Server Language Extensions?
[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

Language extension capabilities are added to SQL Server in each release as we continue to expand, extend, and deepen the integration between external languages and the data platform. 

## New in SQL Server 2019 preview

This release adds the support for Language Extensions in SQL Server. For more information about all of the features in this release, see [What's New in SQL Server 2019](../sql-server/what-s-new-in-sql-server-ver15.md) and [Release Notes for SQL Server 2019](../sql-server/sql-server-ver15-release-notes.md).

| Release | Feature update |
|---------|----------------|
| CTP 3.0 | New [CREATE EXTERNAL LANGUAGE](../t-sql/statements/create-external-language-transact-sql.md) for registering external language (for example, Java) in SQL Server.
| | New supported [Java data types](how-to/java-to-sql-data-types.md). |
| CTP 2.5 | New Microsoft Extensibility SDK for Java. |
| CTP 2.4 | Linux support for [CREATE EXTERNAL LIBRARY (Transact-SQL)](../t-sql/statements/create-external-library-transact-sql.md). |
| | The environment variable that specifies the location of the Java interpreter has changed from `JAVA_HOME` to `JRE_HOME`. |
| CTP 2.3 | New supported [Java data types](how-to/java-to-sql-data-types.md). |
| | On Windows only, Java code can be accessed in an external library using the [CREATE EXTERNAL LIBRARY (Transact-SQL)](../t-sql/statements/create-external-library-transact-sql.md) statement. Learn more: [How to call Java from SQL Server](how-to/call-java-from-sql.md). |
| CTP 2.2 | No changes. |
| CTP 2.1 | No changes. |
| CTP 2.0 | [Java language extension](language-extensions-overview.md) on both Windows and Linux is new in SQL Server 2019 preview. You can make compiled Java code available to SQL Server by assigning permissions and setting the path. Client apps with access SQL Server can use data and run your code by calling [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql), the same procedure used for R and Python integration on SQL Server Machine Learning Services. |
|   | Failover cluster support is now supported on Windows and Linux, assuming SQL Server Launchpad service is started on all nodes. For more information, see [SQL Server failover cluster installation](../sql-server/failover-clusters/install/sql-server-failover-cluster-installation.md). |

## Next steps

+ Install [SQL Server Language Extensions on Windows](install/install-sql-server-language-extensions-on-windows.md) or [on Linux](../linux/sql-server-linux-setup-language-extensions.md)