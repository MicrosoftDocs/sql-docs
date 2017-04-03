---
title: "Architecture | Microsoft Docs"
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

# Architecture Overview - Machine Learning Services with Python 

This topic provides an overview of how Python is integrated with SQL Server, including the security model, the components in the database engine tat support external script execution, and new components that enable interoperability of Python with SQL Server. For details see the linked topics.

> [!IMPORTANT]
> Support for Python is available beginning with SQL Server vNext.

## Python Interoperability

SQL Server Machine Learning Services (In-Database) installs the Anaconda distribution of Python, and the Python 3.5 runtime and interpreter. This ensure near-complete compatibility with standard Python solutions. Python runs in a separate process from SQL Server, to ensure that database operations are not compromised.

For more information about the interaction of SQL Server with Python, see [Python Interoperability](../../advanced-analytics/python/python-interoperability.md)

## New Components that Support Python Integration

The extensibility framework introduced in SQL Server 2016 now supports execution of Python script, through the addition of new language-specific components. These components improve data excahgne speed and compression, while providing a secure, high-performance platform for running external scripts.

For detailed description of the components that support Python, such as the [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] and PythonLauncher, see [New Components](../../advanced-analytics/python/new-components-in-sql-server-to-support-python-integration.md).

## Security 

R tasks execute outside the SQL Server process, to provide security and greater manageability.

All tasks are secured by Windows job objects or SQL Server security. Data is kept within the compliance boundary by enforcing SQL Server security at the table, database, and instance level. The database administrator can control who has the ability to run Python jobs, and monitor the use of Python scripts by users, and the resources consumed.

For details, see [Security for Python](../../advanced-analytics/python/security-overview-sql-server-python-services.md)

## Resource Governance

In SQL Server Enterprise Edition, you can use Resource governor to manage and monitor resource use of external script operations, including R script and Python script. 

For more information, see [Resource Governance for  Python](../../advanced-analytics/python/resource-governance-for-python).

## See Also

