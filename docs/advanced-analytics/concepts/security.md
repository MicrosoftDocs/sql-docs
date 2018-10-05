---
title: Security for SQL Server machine learning | Microsoft Docs
description: Security for SQL Server machine learning.
ms.prod: sql
ms.technology: machine-learning

ms.date: 09/21/2018  
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Security overview for the extensibility framework in SQL Server Machine Learning Services

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes the overall security architecture that is used to connect the SQL Server database engine and related components to the extensibility framework. Examples of the security process are provided for these common scenarios for using machine learning extensions in an enterprise environment:

+ Executing RevoScaleR or MicrosoftML functions in SQL Server from a data science client
+ Running external scripts (such as R or Python) directly from SQL Server using stored procedures
+ Running Python with the SQL Server as the remote compute context

## Security overview

A SQL Server login or Windows user account is required to run external scripts that use SQL Server data or that run with SQL Server as the compute context.

The login or user account identifies the *security principal*, who might need multiple levels of access, depending on the external script requirements:

+ Permission to access the database where external scripts are enabled.
+ Permissions to read data from secured objects such as tables.
+ The ability to write new data to a table, such as a model, or scoring results.
+ The ability to create new objects, such as tables, stored procedures that use the external script, or custom functions that use R or Python job.
+ The right to install new packages on the SQL Server computer, or use packages provided to a group of users.

Therefore, each person who runs an external script using SQL Server as the execution context must be mapped to a login in the database. Under SQL Server security, it is easiest to create roles to manage sets of permissions, and assign users to those roles, rather than individually set user permissions.

Even users who are using R or Python in an external tool must be mapped to a login or account in the database if the user needs to run an external script in-database, or access database objects and data. The same permissions are required whether the external script is sent from a remote data science client or started using a T-SQL stored procedure.

For example, assume that you created an external script that runs on your local computer, and you want to run that code on SQL Server. You must ensure that the following conditions are met:

+ The database allows remote connections.
+ The SQL login or Windows account that you used for database access has been added to the SQL Server at the instance level.
+ The SQL login or Windows user must have the permission to execute external scripts. Generally, this permission can only be added by a database administrator.
+ The SQL login or Window user must be added as a user, with appropriate permissions, in each database where the external script performs any of these operations:
  + Retrieving data.
  + Writing or updating data.
  + Creating new objects, such as tables or stored procedures.

After the login or Windows user account has been provisioned and given the necessary permissions, you can run an external script on SQL Server by using a data source object in R or the **revoscalepy** library in Python, or by calling a stored procedure that contains the external script.

Whenever an external script is launched from SQL Server, the database engine security gets the security context of the user who started the job, and manages the mappings of the user or login to securable objects.

Therefore, all external scripts that are initiated from a remote client must specify the login or user information as part of the connection string.

## SQL Server Launchpad service

A separate [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] service is created for database engine instance to which you have added SQL Server machine learning (R or Python) integration.

By default, [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] is configured to run under **NT Service\MSSQLLaunchpad**, which is provisioned with all necessary permissions to run external scripts. Stripping permissions from this account can result in [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] failing to start or to access the SQL Server instance where external scripts should be run.

For how to configure the [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] service, see [[!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] service configuration](../security/sql-server-launchpad-service-account.md).

## Interaction of SQL Server security and Launchpad security

When an external script is executed in the context of the SQL Server computer, the [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] service gets an available worker account (a local user account) from a pool of worker accounts established for external processes and uses that worker account to perform the related tasks.

For example, assume you launch an external script under your Windows domain credentials. SQL Server gets your credentials and maps the task to an available Launchpad worker account, such as *SQLRUser01*.

> [!NOTE]
> The name of the group of worker accounts is the same regardless of whether you are using R or Python. However, a separate group is created for each instance where you enable any external language.

After mapping to a worker account, [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] creates a user token that is used to start processes.

When all SQL Server operations are completed, the user worker account is marked as free and returned to the pool.

For more information about the service, see [Extensibility framework](../concepts/extensibility-framework.md).

### What is SQLRUserGroup?

In SQL Server 2016 and 2017, during setup of Machine Learning Services, new local Windows user accounts (MSSQLSERVER00-MSSQLSERVER20) are created and used for isolating and running external processes under the security token of the [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] service. When an external process was needed, SQL Server Launchpad service would take an available account and use it to run a process.

You can view these accounts in the Windows user group **SQLRUserGroup**. By default, 20 accounts are created, which supports 20 concurrent sessions. Parallelized tasks do not consume additional accounts. For example, if a user runs a scoring task that uses parallel processing, the same worker account is reused for all threads. If you intend to make heavy use of machine learning, you can increase the number of accounts used to run external scripts. For more information, see [Modify the user account pool for machine learning](../../advanced-analytics/security/modify-user-account-pool.md).

When a user sends a machine learning script from an external client, SQL Server activates an available worker account, maps it to the identity of the calling user, and runs the script on behalf of the user. This new service of the database engine supports the secure execution of external scripts, called **implied authentication**.

However, if you need to run R or Python scripts from a remote data science client, and you are using Windows authentication, you must give these worker accounts permission to sign in to the SQL Server instance on your behalf. For more information about how to give permission, see [Add SQLRUserGroup as a database user](../../advanced-analytics/security/add-sqlrusergroup-to-database.md).

> [!Note]
> In SQL Server 2019, **SQLRUserGroup** only has one member which is now the single SQL Server Launchpad service account instead of multiple worker accounts.

### AppContainer isolation in SQL Server 2019

In SQL Server 2019, Setup no longer creates local Windows user accounts. Instead, isolation is achieved through [AppContainers](https://docs.microsoft.com/windows/desktop/secauthz/appcontainer-isolation). At run time, when embedded script or code is detected in a stored procedure or query, SQL Server calls Launchpad with a request for an extension-specific launcher. Launchpad invokes the appropriate runtime environment in a process under its identity, and instantiates an AppContainer to contain it. This change is beneficial because local account and password management is no longer required. Also, on installations where local user accounts are prohibited, elimination of the local user account dependency means you can now use this feature.

As implemented by SQL Server, AppContainers are an internal mechanism. While you won't see physical evidence of AppContainers in Process Monitor, you can find them in outbound firewall rules created by Setup to prevent processes from making network calls.

### Implied authentication

**Implied authentication** is the term used for the process under which SQL Server gets the user credentials and then executes all external script tasks on behalf of the users, assuming the user has the correct permissions in the database. Implied authentication is important if the external script needs to make an ODBC call outside the SQL Server database. For example, the code might retrieve a shorter list of factors from a spreadsheet or other source.

For such loopback calls to succeed, the group that contains the worker accounts, SQLRUserGroup, must have "Allow Log on locally" permissions. By default, this right is given to all new local users, but in some organizations stricter group policies might be enforced.

> [!IMPORTANT]
> For implied authentication to succeed, **SQLRUserGroup** must have an account in the master database for the instance, and this account must be given permissions to connect to the instance.

#### Implied authentication for R

![Implied authentication for R](../media/implied-auth-rsql.png)

#### Implied authentication for Python

![Implied authentication for Python](../media/implied-auth-python2.png)

## Security of worker accounts

The mapping of an external Windows user or valid SQL login to a worker account is valid only for the lifetime of the SQL stored procedure that executes the external script.

Parallel queries from the same login are mapped to the same user worker account.

The directories used for the processes are managed by the [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)], and directories are access-restricted. For R, RLauncher performs this task. For Python, PythonLauncher performs this task. Each individual worker account is restricted to its own folder, and cannot access files in folders above its own level. However, the worker account can read, write, or delete children under the session working folder that was created.

For more information about how to change the number of worker accounts, account names, or account passwords, see [Modify the user account pool for SQL Server machine learning](../../advanced-analytics/security/modify-user-account-pool.md).

## Security isolation for multiple external scripts

The isolation mechanism is based on physical user accounts. As satellite processes are started for a specific language runtime, each satellite task uses the worker account specified by the [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)]. If a task requires multiple satellites, for example, in the case of parallel queries, a single worker account is used for all related tasks.

No worker account can see or manipulate files used by other worker accounts.

If you are an administrator on the computer, you can view the directories created for each process. Each directory is identified by its session GUID.

## No support for encryption at rest

[Transparent Data Encryption (TDE)](../../relational-databases/security/encryption/transparent-data-encryption.md) is not supported for data sent to or received from the external script runtime. The reason is that the external process (R or Python) runs outside the SQL Server process. Therefore, data used by the external runtime is not protected by the encryption features of the database engine. This behavior is no different than any other client running on the SQL Server computer that reads data from the database and makes a copy.

As a consequence, TDE **is not** applied to any data that you use in R or Python scripts, or to any data saved to disk, or to any persisted intermediate results. However, other types of encryption, such as Windows BitLocker encryption or third-party encryption applied at the file or folder level, still apply.

In the case of [Always Encrypted](../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md), external runtimes do not have access to the encryption keys. Therefore, data cannot be sent to the scripts.

## Program file permissions

**SQLRUserGroup** provide read and execute permissions on executables in the SQL Server **Binn**, **R_SERVICES**, and **PYTHON_SERVICES** directories.

> [!Note]
> In SQL Server 2019, the only member of **SQLRUserGroup** is the SQL Server Launchpad service account. When Launchpad service starts an R, Python, or Java execution environment, the process runs as LaunchPad service.

## See Also

[Extensibility architecture](../../advanced-analytics/concepts/extensibility-framework.md)
[Configure and manage Advanced Analytics Extensions](../../advanced-analytics/r/configure-and-manage-advanced-analytics-extensions.md)