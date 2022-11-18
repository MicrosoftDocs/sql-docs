---
title: Compare SQL Server Language Extensions to SQL CLR
titleSuffix:
description: "What's the difference between SQL Server Language Extensions and SQL Common Language Runtime (CLR)? This article compares the two."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 09/21/2021
ms.topic: conceptual
ms.service: sql
ms.subservice: language-extensions
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15"
---

# Compare SQL Server Language Extensions to SQL CLR

[!INCLUDE [SQL Server 2019 and later](../../includes/applies-to-version/sqlserver2019.md)]

This article compares [SQL Server Language Extensions](../language-extensions-overview.md) and the native [common language runtime (CLR)](../../relational-databases/clr-integration/common-language-runtime-integration-overview.md). It identifies the key differences between them and helps you decide which one to use.

[SQL Server Language Extensions](../language-extensions-overview.md) is a feature of SQL Server used for executing external code. The relational data can be used in the external code using the extensibility framework.

The native [common language runtime (CLR)](../../relational-databases/clr-integration/common-language-runtime-integration-overview.md) allows you to implement some of the functionalities of SQL Server with .NET languages. The CLR supplies managed code with services such as cross-language integration, code access security, object lifetime management, and debugging and profiling support.

The languages available in SQL Server Language Extensions are not a direct replacement for SQL CLR. The extensibility framework and the language extensions extend the surface area of SQL Server and provide an execution environment for runtimes closer to the database engine. They also provide an open-source framework that can be used to onboard new runtimes without engine changes. Currently the surface area is restricted to the system stored procedure, `sp_execute_external_script`.

The following are some of the key differences between SQL Language Extensions and SQL CLR:

| Feature                 | SQL CLR            | SQL Language Extensions |
| ----------------------- | ------------------ | ----------------------- |
| Platform support        | Windows & Linux - Linux supports only SAFE assemblies. | Windows & Linux - full parity in terms of functionality. |
| Mode of execution       | In-proc            | Out-of-proc |
| Isolation               | CLR code executes within engine process, instance administrator needs to trust all assemblies/code. | Runtime execution is outside of the engine process. Further isolation is provided using App Containers in Windows or Namespaces in Linux. External network communication is also disabled by default. |
| Declarative syntax (T-SQL) | User-defined types, user-defined aggregates, functions, procedures, triggers. | Only ad-hoc execution using sp_execute_external_script. |
| DDL support             | CREATE ASSEMBLY to upload user code and define other objects (functions, procs, triggers UDTs, UDAggs). | CREATE EXTERNAL LANGUAGE, EXTERNAL LIBRARY to manage extensions and libraries. |
| Library support         | Achieved via assemblies. | Libraries for specific runtime can be used (Ex: R or Python packages, Java libraries). |
| Runtimes supported      | .NET Framework     | R, Python, Java, C# or Bring your own runtime (BYOR). |
| OSS framework           | N/A - can be extended via user-defined .NET Assemblies. | Yes - extension SDK provides authoring of new extensions or integration with runtimes without engine changes. |
| QO integration          | Operator level integration for various syntaxes including parallelism. | Single external script operator to send/receive results and data from runtimes, this operator supports batch mode execution and parallelism. |
| Resource Governance     | None - few knobs outside of resource governor. | Provides EXTERNAL RESOURCE POOL object as a separate mechanism to govern resources consumed by the runtime/external scripts, policies can be defined for external runtimes in addition to the SQL workload. |
| Permission model        | Instance level control with db scoped objects. | Instance level control with db scoped objects. |
| Performance             | SQL CLR code will typically outperform Extensibility due to nature of execution. | Ideal for batch-oriented execution. |
| Monitoring capabilities | `sys.dm_clr*` DMVs & limited SQL CLR-specific perfmon counter. | `sys.dm_external*` DMVs, external resource pool DMVs, Windows Jobobject perfmon counters. |

## When to use each

Whether you use Language Extensions or CLR depends on your scenarios and goals. For example, if you need to extend the T-SQL surface area with your own aggregates or types, then CLR is the best choice since type or aggregate cannot be defined using the extensibility mechanism. On the other hand, if you want to leverage existing data science expertise in your organization or team, then using R/Python integration with extensibility is the best choice.

Similarly, your performance goals may determine your decision. Implementing a regex function in C# and using CLR will be much faster than using extensibility to invoke a Python script that performs the same regex functionality.

## Next steps

+ [What is Language Extensions?](../language-extensions-overview.md)
+ [Common Language Runtime Integration](../../relational-databases/clr-integration/common-language-runtime-integration-overview.md)
+ [Extensibility architecture in SQL Server Language Extensions](extensibility-framework.md)