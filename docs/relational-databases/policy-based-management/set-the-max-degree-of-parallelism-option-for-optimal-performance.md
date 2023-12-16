---
title: "Max degree of parallelism & Policy-Based Management"
description: Describes configuring a policy to verify the value of max degree of parallelism for Policy-Based Management for SQL Server.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Set the max degree of parallelism option for optimal performance

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rule determines whether the max degree of parallelism (MAXDOP) option for a value greater than 8. Setting this option to a larger value often causes unwanted resource consumption and performance degradation.

## Best practice recommendations

The max degree of parallelism (MAXDOP) configuration option controls the number of processors that are used for the execution of a query in a parallel plan. This option determines the number of threads that are used for the query plan operators that perform the work in parallel. Depending on whether SQL Server is set up on a symmetric multiprocessing (SMP) computer, a non-uniform memory access (NUMA) computer, or simultaneous multithreading (SMT) enabled processors, you have to configure the **max degree of parallelism** option appropriately.

Recommendations to configure MAXDOP depend on the version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] being used. For version specific guidelines, see [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md#recommendations), and configure the policy to verify the value of max degree of parallelism accordingly.

## Related content

- [Configure the max degree of parallelism (server configuration option)](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md)
- [sp_configure (Transact-SQL)](../system-stored-procedures/sp-configure-transact-sql.md)
