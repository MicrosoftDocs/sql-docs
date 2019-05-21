---
title: Extensibility architecture in SQL Server Language Extensions
titleSuffix: SQL Server Language Extensions
description: External code support for the SQL Server database engine, with dual architecture for running external language on relational data.
author: dphansen
ms.author: davidph 
manager: cgronlun
ms.date: 05/22/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: language-extensions
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---
# Extensibility architecture in SQL Server Language Extensions
[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

SQL Server Language Extensions has an extensibility framework for running external code such as Java on the server. The code executes in a language runtime environment as an extension to the core database engine.

## Background

The purpose of the extensibility framework is to provide an interface between SQL Server and external languages such as Java. By executing a trusted language within a secure framework managed by SQL Server, database administrators can maintain security while allowing data scientists access to enterprise data.

<!-- We need to get a diagram like the one below.
The following diagram visually describes opportunities and benefits of the extensible architecture.

  ![Goals of integration with SQL Server](../media/ml-service-value-add.png "Machine Learning Services Value Add")
-->

Any supported external language can be run by calling a stored procedure, and the results are returned as tabular results directly to SQL Server, making it easy to use the external language from any application that can send a SQL query and handle the results.

## Architecture diagram

The architecture is designed such that external code run in a separate process from SQL Server, but with components that internally manage the chain of requests for data and operations on SQL Server. 

  ![Component architecture](../media/generic-architecture.png "Component architecture")

Components include a **Launchpad** service used to invoke language-specific launchers (for example,, Java), language and library-specific logic for loading interpreters and libraries. The Launcher loads a language run time, plus any proprietary modules. 

<a name="launchpad"></a>

## Launchpad

The [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] is a service that manages and executes external scripts, similar to the way that the full-text indexing and query service launches a separate host for processing full-text queries. The Launchpad service can start only trusted launchers that are published by Microsoft, or that have been certified by Microsoft as meeting requirements for performance and resource management.

| Trusted launchers | Extension | SQL Server versions |
|-------------------|-----------|---------------------|
| JavaLauncher.dll for Java | Java extension | SQL Server 2019 |

The [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] service runs under **SQLRUserGroup**. which uses [AppContainers](https://docs.microsoft.com/windows/desktop/secauthz/appcontainer-isolation) for execution isolation.

A separate [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] service is created for each database engine instance to which you have added SQL Server Machine Language Extensions. There is one Launchpad service for each database engine instance, so if you have multiple instances with external script support, you will have a Launchpad service for each one. A database engine instance is bound to the Launchpad service created for it. All invocations of external script in a stored procedure or T-SQL result in the SQL Server service calling the Launchpad service created for the same instance.

To execute tasks in a specific supported language, the Launchpad gets a secured worker account from the pool, and starts a satellite process to manage the external runtime. Each satellite process inherits the user account of the Launchpad and uses that worker account for the duration of script execution. If script uses parallel processes, they are created under the same, single worker account.

## BxlServer and SQL Satellite

**BxlServer** is an executable provided by Microsoft that manages communication between SQL Server and the external language. It creates the Windows job objects that are used to contain external script sessions, provisions secure working folders for each external script job, and uses SQL Satellite to manage data transfer between the external runtime and SQL Server. If you run [Process Explorer](https://technet.microsoft.com/sysinternals/processexplorer.aspx) while a job is running, you might see one or multiple instances of BxlServer.

In effect, BxlServer is a companion to a language run time environment that works with SQL Server to transfer data and manage tasks. BXL stands for Binary Exchange language and refers to the data format used to move data efficiently between SQL Server and external processes. 

**SQL Satellite** is an extensibility API, included in the database engine starting with SQL Server 2016, that supports external code or external runtimes implemented using C or C++.

BxlServer uses SQL Satellite for these tasks:

+ Reading input data
+ Writing output data
+ Getting input arguments
+ Writing output arguments
+ Error handling
+ Writing STDOUT and STDERR back to client

SQL Satellite uses a custom data format that is optimized for fast data transfer between SQL Server and external script languages. It performs type conversions and defines the schemas of the input and output datasets during communications between SQL Server and the external script runtime.

The SQL Satellite can be monitored by using windows extended events (xEvents).

## Communication channels between components

Communication protocols among components and data platforms are described in this section.

+ **TCP/IP**

  By default, internal communications between SQL Server and the SQL Satellite use TCP/IP.

+ **Named Pipes**

  Internal data transport between the BxlServer and SQL Server through SQL Satellite uses a proprietary, compressed data format to enhance performance. Data is exchanged between language run times and BxlServer in BXL format, using Named Pipes.

+ **ODBC**

  Communications between external data science clients and a remote SQL Server instance use ODBC. The account that sends the script jobs to SQL Server must have both permissions to connect to the instance and to run external scripts.

  Additionally, depending on the task, the account might need these permissions:

  + Read data used by the job
  + Write data to tables: for example, when saving results to a table
  + Create database objects: for example, if saving external script as part of a new stored procedure.

  When SQL Server is used as the compute context for script executed from a remote client, and the executable must retrieve data from an external source, ODBC is used for writeback. SQL Server maps the identity of the user issuing the remote command to the identity of the user on the current instance, and runs the ODBC command using that user's credentials. The connection string needed to perform this ODBC call is obtained from the client code.

+ **Other protocols**

  Processes that might need to work in "chunks" or transfer data back to a remote client can also use the [XDF file format](https://docs.microsoft.com/machine-learning-server/r/concept-what-is-xdf). Actual data transfer is via encoded blobs.

## Next steps

+ [What is Language Extensions?](../language-extensions-overview.md)