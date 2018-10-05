---
title: Modify the user account pool for SQL Server machine learning | Microsoft Docs
description: How to modify the user account pool for SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning

ms.date: 09/26/2018  
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Security for SQLRUserGroup
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

As part of the installation process for [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)], a new Windows *user account pool* is created to support execution of tasks by the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service. The purpose of these worker accounts is to isolate concurrent execution of external scripts by different SQL users.

This article describes the security considerations for the worker accounts, and how to change the default configuration.

Each **SQLRUserGroup** user group is associated with the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service on a specific instance and cannot support R jobs that run on other instances.

For each worker account, while the session is active, a temporary folder is created to store the script objects, intermediate results, and other information used by R or Python and SQL Server during external script execution. These working files, located under the ExtensibilityData folder, are access-restricted to administrators, and are cleaned up by SQL Server after the script completes.

For more information, see [Security Overview](../../advanced-analytics/concepts/security.md).

**Applies to:** [!INCLUDE[sssql15-md](../../includes/sssql15-md.md)] [!INCLUDE[rsql-productname-md](../../includes/rsql-productname-md.md)], [!INCLUDE[sscurrent-md](../../includes/sscurrent-md.md)] [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)]

## Enable implied authentication for SQL Restricted User Group (SQLRUserGroup) account group

If you need to run scripts from a remote data science client, and you are using Windows authentication, additional configuration is required to give worker accounts running R and Python processes permission to sign in to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance on your behalf. This behavior is called *implied authentication*, and is implemented by the database engine to support secure execution of external scripts in SQL Server 2016 and SQL Server 2017.

> [!NOTE]
> If you use a **SQL login** for running scripts in a SQL Server compute context, this extra step is not required.

1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], in Object Explorer, expand **Security**. Then right-click **Logins**, and select **New Login**.
2. In the **Login - New** dialog box, select **Search**.
3. Select **Object Types**, and select **Groups**. Clear everything else.
4. In **Enter the object name to select**, type *SQLRUserGroup*,  and select **Check Names**.
5. The name of the local group associated with the instance's Launchpad service should resolve to something like *instancename\SQLRUserGroup*. Select **OK**.
6. By default, the group is assigned to the **public** role, and has permission to connect to the database engine.
7. Select **OK**.

In SQL Server 2017 and earlier, a number of local Windows user accounts are created for the purpose of running tasks under the security token of the [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] service. You can view these accounts in the Windows user group, **SQLRUserGroup**. By default, 20 worker accounts are created, which is usually more than enough for running external script jobs. 

These accounts are used as follows. When a user sends a Python or R script from an external client, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] activates an available worker account, maps it to the identity of the calling user, and runs the script on behalf of the user. If the script, which is executing external to SQL Server, has to retrieve data or resources from SQL Server, the connection back to SQL Server requires a log in. Creating a database login for **SQLRUserGroup** also the connection to succeed.

> [!NOTE]
> In SQL Server 2019, worker accounts are replaced with AppContainers, with processes executing under the SQL Server Launchpad service. Although the worker accounts are no longer used, you are still required to add a database login for **SQLRUsergroup** if implied authentication is needed. Just as the worker accounts did not have login permission, the Launchpad service identity does not either. Creating a login for **SQLRUserGroup**, which consists of the Launchpad service in this release, allows implied authentication to work.

<a name="bkmk_EnforcePolicy"></a>

## Enforcing password policy

If your organization has a policy that requires changing passwords on a regular basis,  you may need to force the Launchpad service to regenerate the encrypted passwords that Launchpad maintains for its worker accounts.

To enable this setting and force password refresh, open the **Properties** pane for the Launchpad service in SQL Server Configuration Manager, click **Advanced**, and change **Reset External Users Password** to **Yes**. When you apply this change, the passwords will immediately be regenerated for all user accounts. To use R script after this change, you must restart the Launchpad service, at which time it will read the newly generated passwords. 

To reset passwords at regular intervals, you can either set this flag manually or use a script.

## Additional permission required to support remote compute contexts

By default, the group of worker accounts does **not** have login permissions on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance with which it is associated. This can be a problem if any R users connect to SQL Server from a remote client to run R scripts, or if a script uses ODBC to get additional data.

To ensure that these scenarios are supported, the database administrator must provide the group of worker accounts with permission to log into the SQL Server instance where the external scripts will be run (**Connect to** permissions). This is referred to as *implied authentication*, and enables SQL Server to run the R scripts using the credentials of the remote user.

> [!NOTE]
> This limitation does not apply if you use SQL logins to run R scripts from a remote workstation, because the SQL login credentials are explicitly passed from the R client to the SQL Server instance and then to ODBC.


### How to enable implied authentication

1. Open SQL Server Management Studio as an administrator on the instance where you will run R or Python code.

2. Run the following script. Be sure to edit the user group name, if you changed the default, and the computer and instance name.

    ```sql
    USE [master]
    GO
    
    CREATE LOGIN [computername\SQLRUserGroup] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[language]
    GO
    ````

