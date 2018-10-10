---
title: Security for SQL Server machine learning | Microsoft Docs
description: Security overview for the extensibility framework in SQL Server Machine Learning Services. Security for login and user accounts, SQL Server Launchpad service, worker accounts, running multiple scripts, and file permissions.
ms.prod: sql
ms.technology: machine-learning

ms.date: 10/10/2018  
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Security overview for the extensibility framework in SQL Server Machine Learning Services

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes the overall security architecture that is used to connect the SQL Server database engine and related components to the extensibility framework. It describes component parts and interactions. 

<a name="launchpad"></a>

## SQL Server Launchpad service

To instantiate external processes, the database engine provides the SQL Server Launchpad service to create an R or Python session. A separate [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] service is created for the database engine instance to which you have added SQL Server machine learning (R or Python) integration, one service per instance.

By default, [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] is configured to run under **NT Service\MSSQLLaunchpad**, which is provisioned with all necessary permissions to run external scripts. Stripping permissions from this account can result in [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] failing to start or to access the SQL Server instance where external scripts should be run. Launchpad service is generally used as-is. For more information about configurable options, see [SQL Server Launchpad service configuration](../security/sql-server-launchpad-service-account.md).

<a name="sqlrusergroup"></a>

## SQLRUserGroup and worker accounts

External scripts run in external processes, under the identity of least-privilege local worker accounts, subject to the access control list (ACL) of the parent **SQLRUserGroup** (SQL restricted user group). 

**SQLRUserGroup** is created by SQL Server Setup and contains the pool of local Windows user accounts (MSSQLSERVER00-MSSQLSERVER20). When an external process is needed, Launchpad takes an available worker account and uses it to run a process. More specifically, Launchpad activates an available worker account, maps it to the identity of the calling user, and runs the script on behalf of the user. 

+ **SQLRUserGroup** is linked to a specific instance. A separate pool of worker accounts is needed for each instance on which machine learning has been enabled. Accounts cannot be shared between instances.

+ Worker account names in the pool are of the format SQLInstanceName*nn*. For example, if you are using the default instance for machine learning, the user account pool supports account names such as MSSQLSERVER01, MSSQLSERVER02, and so forth.

+ The size of the user account pool is static and the default value is 20, which supports 20 concurrent sessions. The number of external runtime sessions that can be launched simultaneously is limited by the size of this user account pool. 

Parallelized tasks do not consume additional accounts. For example, if a user runs a scoring task that uses parallel processing, the same worker account is reused for all threads. If you intend to make heavy use of machine learning, you can increase the number of accounts used to run external scripts. For more information, see [Modify the user account pool for machine learning](../../advanced-analytics/administration/modify-user-account-pool.md).

### Permissions granted to SQLRUserGroup

By default, **SQLRUserGroup** has read and execute permissions on executables in the SQL Server **Binn**, **R_SERVICES**, and **PYTHON_SERVICES** directories, wiht access to executables, libraries, and built-in datasets in the R and Python distributions installed with SQL Server. 

To protect sensitive resources on SQL Server, you can define an access control list (ACL) that denies access to **SQLRUserGroup**. Conversely, you could also grant permissions to local data resources that exist on host computer, apart from SQL Server itself. 

By design, this group does not have a database login or permissions to any data. Under certain circumstances, you might want to create a login to allow loop back connections, particularly when a trusted Windows identity is the calling user. This capability is called [*implied authentication*](#implied-authentication). For more information, see [Add SQLRUserGroup as a database user](../../advanced-analytics/security/add-sqlrusergroup-to-database.md).

::: moniker range=">=sql-server-ver15||=sqlallproducts-allversions"
### AppContainer isolation in SQL Server 2019

In SQL Server 2019, Setup no longer creates worker accounts for **SQLRUserGroup**. Instead, isolation is achieved through [AppContainers](https://docs.microsoft.com/windows/desktop/secauthz/appcontainer-isolation). At run time, when embedded script or code is detected in a stored procedure or query, SQL Server calls Launchpad with a request for an extension-specific launcher. Launchpad invokes the appropriate runtime environment in a process under its identity, and instantiates an AppContainer to contain it. This change is beneficial because local account and password management is no longer required. Also, on installations where local user accounts are prohibited, elimination of the local user account dependency means you can now use this feature.

As implemented by SQL Server, AppContainers are an internal mechanism. While you won't see physical evidence of AppContainers in Process Monitor, you can find them in outbound firewall rules created by Setup to prevent processes from making network calls.

> [!Note]
> In SQL Server 2019, **SQLRUserGroup** only has one member which is now the single SQL Server Launchpad service account instead of multiple worker accounts.
::: moniker-end

## User security

SQL Server's data security model of database logins and roles extend to R and Python script. As a database user, if you have data permissions to execute a particular query, any R or Python script that you run on SQL Server also has permission to retrieve the same data. 

You can use Windows authentication or SQL Server authentication. Permission requirements vary depending on whether you need to create and save database objects, or simply consume objects created by others. For more information, see [Give users permission to SQL Server Machine Learning Services](../../advanced-analytics/security/user-permission.md).

### Mapping user identities to worker accounts

When a session is started, Launchpad maps the identity of the calling user to a worker account. The mapping of an external Windows user or valid SQL login to a worker account is valid only for the lifetime of the SQL stored procedure that executes the external script. Parallel queries from the same login are mapped to the same user worker account.

During execution, Launchpad creates temporary folders to store session data, deleting them when the session concludes. The directories are access-restricted. For R, RLauncher performs this task. For Python, PythonLauncher performs this task. Each individual worker account is restricted to its own folder, and cannot access files in folders above its own level. However, the worker account can read, write, or delete children under the session working folder that was created. If you are an administrator on the computer, you can view the directories created for each process. Each directory is identified by its session GUID.

<a name="implied-authentication"></a>

### Implied authentication

**Implied authentication** describes the process under which SQL Server gets the user credentials and then executes all external script tasks on behalf of the users, assuming the user has the correct permissions in the database. Most of the time, implied authentication is an implementation detail, but in the case of loopback connections to SQL Server, you might need additional configuration to support that connection.

However, Implied authentication is important if the external script needs to make an ODBC call outside the SQL Server database. For example, the code might retrieve a shorter list of factors from a spreadsheet or other source.

For such loopback calls to succeed, the group that contains the worker accounts, SQLRUserGroup, must have "Allow Log on locally" permissions. By default, this right is given to all new local users, but in some organizations stricter group policies might be enforced.

> [!IMPORTANT]
> For implied authentication to succeed, **SQLRUserGroup** must have an [account in the master database](add-sqlrusergroup-to-database.md) for the instance, and this account must be given permissions to connect to the instance. 

#### How implied authentication works for R and Python sessions

The following diagrams shows the interaction of SQL Server components with the R runtime and how it does implied authentication for R.

![Implied authentication for R](../security/media/implied-auth-rsql.png)

The following diagrams shows the interaction of SQL Server components with the Python runtime and how it does implied authentication for Python.

![Implied authentication for Python](../security/media/implied-auth-python2.png)

#### Implied authentication for connecting back to SQL Server

If your script contains a connection string to SQL Server, the type of user identity and how it is specified has an impact on whether that connection succeeds. Using a SQL login with a user name and password providing on the connection string, the connection is expected to succeed. However, if you are using Windows integrated security (with **-T** on connection string) the connection will fail by default. The connection fails because the script is actually executing under the identity of a worker account, which is unknown to the server. 

In the extensibility framework, R or Python script executes as a worker account. Although the system maintains an association between the worker account and the database user invoking the script, the worker account itself does not have permission to loop back to database engine. To allow 

with SQL logins having a slight edge over Windows integrated security for scripts that connect back to SQL Server for data operations.

+ For SQL logins: Ensure that the login has appropriate permissions on the database where you are reading data. You can do this by adding *Connect to* and *SELECT* permissions, or by adding the login to the `db_datareader` role. To create objects, assign `DDL_admin` rights. If you must save data to tables, add to the `db_datawriter` role.

+ For Windows authentication: You might need to create an ODBC data source on the data science client that specifies the instance name and other connection information. For more information, see [ODBC data source administrator](../../odbc/admin/odbc-data-source-administrator.md).

## No support for Transparent Data Encryption at rest

[Transparent Data Encryption (TDE)](../../relational-databases/security/encryption/transparent-data-encryption.md) is not supported for data sent to or received from the external script runtime. The reason is that the external process (R or Python) runs outside the SQL Server process. Therefore, data used by the external runtime is not protected by the encryption features of the database engine. This behavior is no different than any other client running on the SQL Server computer that reads data from the database and makes a copy.

As a consequence, TDE **is not** applied to any data that you use in R or Python scripts, or to any data saved to disk, or to any persisted intermediate results. However, other types of encryption, such as Windows BitLocker encryption or third-party encryption applied at the file or folder level, still apply.

In the case of [Always Encrypted](../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md), external runtimes do not have access to the encryption keys. Therefore, data cannot be sent to the scripts.


## Next steps

In this article, you learned the components and interaction model of the security architecture built into the [extensibility framework](../../advanced-analytics/concepts/extensibility-framework.md). Key points covered in this article include the purpose of Launchpad, SQLRUserGroup and worker accounts, process isolation of R and Python, and how user identities are mapped to worker accounts. 

As a next step, review the instructions for [granting permissions](../../advanced-analytics/security/user-permission.md). For servers that use Windows authentication, you should also review [Add SQLRUserGroup to a database login](add-sqlrusergroup-to-database.md) to learn when additional configuration is required.