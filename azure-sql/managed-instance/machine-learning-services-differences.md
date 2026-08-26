---
title: Key Differences for Machine Learning Services
description: This article describes key differences between Machine Learning Services in Azure SQL Managed Instance and SQL Server Machine Learning Services.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: wiassaf, randolphwest
ms.date: 01/26/2026
ms.service: azure-sql-managed-instance
ms.subservice: machine-learning
ms.topic: concept-article
---

# Key differences between Machine Learning Services in Azure SQL Managed Instance and SQL Server

This article describes the few, key differences in functionality between [Machine Learning Services in Azure SQL Managed Instance](machine-learning-services-overview.md) and [SQL Server Machine Learning Services](/sql/advanced-analytics/what-is-sql-server-machine-learning).

## Language support

Machine Learning Services in both SQL Managed Instance and SQL Server support the Python and R [extensibility framework](/sql/machine-learning/concepts/extensibility-framework). A key difference in SQL Managed Instance is that only Python and R are supported, and external languages such as Java can't be added.

The initial versions of Python and R are different in SQL Managed Instance and SQL Server:

| Platform | Python runtime version | R runtime versions |
| --- | --- | --- |
| Azure SQL Managed Instance | 3.7.2 | 3.5.2 |
| SQL Server 2022 <sup>1</sup> | - | - |
| SQL Server 2019 | 3.7.1 | 3.5.2 |
| SQL Server 2017 | 3.5.2 and 3.7.2 (CU22 and later) | 3.3.3 and 3.5.2 (CU22 and later) |
| SQL Server 2016 | Not available | 3.2.2 and 3.5.2 (SP2 CU14 and later) |

<sup>1</sup> Beginning with SQL Server 2022, runtimes for R, Python, and Java, are no longer shipped or installed within SQL Setup. Instead, install your desired R and/or Python custom runtime(s) and packages. For more information, see [Install SQL Server 2022 Machine Learning Services (Python and R) on Windows](/sql/machine-learning/install/sql-machine-learning-services-windows-install-sql-2022).

## Python and R Packages

There's no support in SQL Managed Instance for packages that depend on external runtimes (like Java) or need access to OS APIs for installation or usage.

For more information about managing Python and R packages, see:

- [Get Python package information](/sql/machine-learning/package-management/python-package-information?context=%2fazure%2fazure-sql%2fmanaged-instance%2fcontext%2fml-context&preserve-view=true&view=azuresqldb-mi-current)
- [Get R package information](/sql/machine-learning/package-management/r-package-information?context=%2fazure%2fazure-sql%2fmanaged-instance%2fcontext%2fml-context&preserve-view=true&view=azuresqldb-mi-current)

### Manage packages via sqlmlutils

You can install binary packages with `sqlmlutils`, if you can build locally to produce a binary package as output. For examples, see [Install R packages with sqlmlutils](/sql/machine-learning/package-management/install-additional-r-packages-on-sql-server).

## Resource governance

In SQL Managed Instance, it's not possible to limit R resources through [Resource Governor](/sql/relational-databases/resource-governor/resource-governor?view=azuresqldb-mi-current&preserve-view=true), and external resource pools aren't supported.

By default, R resources are set to a maximum of 20% of the available SQL Managed Instance resources when extensibility is enabled. To change this default percentage, create an [Azure support ticket](https://azure.microsoft.com/support/create-ticket/).

Extensibility is enabled with the following SQL commands (SQL Managed Instance will restart and be unavailable for a few seconds):

```sql
sp_configure 'external scripts enabled', 1;
RECONFIGURE WITH OVERRIDE;
```

To disable extensibility and restore 100% of memory and CPU resources to SQL Server, use the following commands:

```sql
sp_configure 'external scripts enabled', 0;
RECONFIGURE WITH OVERRIDE;
```

The total resources available to SQL Managed Instance depend on which service tier you choose. For more information, see [Compare vCore and DTU-based purchasing models of Azure SQL Database](../database/purchasing-models.md).

### Insufficient memory error

Memory usage depends on how much is used in your R scripts and the number of parallel queries being executed. If there's insufficient memory available for R, you'll get an error message. Common error messages are:

- `Unable to communicate with the runtime for 'R' script for request id: *******. Please check the requirements of 'R' runtime`
- `'R' script error occurred during execution of 'sp_execute_external_script' with HRESULT 0x80004004. ...an external script error occurred: "..could not allocate memory (0 Mb) in C function 'R_AllocStringBuffer'"`
- `An external script error occurred: Error: cannot allocate vector of size.`

If you receive one of these errors, you can resolve it by scaling your database to a higher service tier.

If you encounter out of memory errors in Azure SQL Managed Instance, review [sys.dm_os_out_of_memory_events](/sql/relational-databases/system-dynamic-management-views/sys-dm-os-out-of-memory-events).

## Update policy

Machine Learning Services is available only on SQL managed instances configured with the [**SQL Server 2022** update policy](update-policy.md#sql-server-2022-update-policy). It isn't available with the **SQL Server 2025** or **Always-up-to-date** update policy. The SQL Server 2022 update policy is available until [mainstream support for SQL Server 2022 ends](/lifecycle/products/sql-server-2022). At that time, instances automatically move to the update policy for the latest major SQL Server release, and Machine Learning Services becomes unavailable. To call an externally hosted machine learning model from those instances, consider using [`sp_invoke_external_rest_endpoint`](/sql/relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql).

## SQL Managed Instance pools

Machine Learning Services is currently not supported on [Azure SQL Managed Instance pools](instance-pools-overview.md).

## Outbound network access

Network access is disallowed or blocked and can't be enabled. The outbound network connection for Azure SQL Managed Instance isn't available for Machine Learning Services.

## Related content

- [Machine Learning Services in Azure SQL Managed Instance](machine-learning-services-overview.md)
- [Run Python scripts](/sql/machine-learning/tutorials/quickstart-python-create-script?context=/azure/azure-sql/managed-instance/context/ml-context&view=azuresqldb-mi-current&preserve-view=true)
- [Run R scripts](/sql/machine-learning/tutorials/quickstart-r-create-script?context=/azure/azure-sql/managed-instance/context/ml-context&view=azuresqldb-mi-current&preserve-view=true)
