---
title: Enable or disable remote R package management - SQL Server Machine Learning Services
description: Enable remote R package management on SQL Server 2016 R Services or SQL Server 2017 Machine Learning Services (In-Database)
ms.prod: sql
ms.technology: machine-learning

ms.date: 05/10/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Enable or disable remote package management for SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes how to enable remote management of R packages from a client workstation or a different Machine Learning Server. After the package management feature has been enabled on SQL Server, you can use RevoScaleR commands on a client to install packages on SQL Server.

> [!NOTE]
> Currently management of R libraries is supported; support for Python is on the roadmap.

By default, the external package management feature for SQL Server is disabled. You must run a separate script to enable the feature as described in the next section.

## Overview of process and tools

To enable or disable package management on SQL Server, use the command-line utility **RegisterRExt.exe**, which is included with the **RevoScaleR** package.

[Enabling](#bkmk_enable) this feature is a two-step process, requiring a database administrator: you enable package management on the SQL Server instance (once per SQL Server instance), and then enable package management on the SQL database (once per SQL Server database).

[Disabling](#bkmk_disable) the package management feature also requires multipel steps: you remove database-level packages and permissions (once per database), and then remove the roles from the server (once per instance).

## <a name="bkmk_enable"></a> Enable package management

1. On SQL Server, open an elevated command prompt and navigate to the folder containing the utility, RegisterRExt.exe. The default location is `<SQLInstancePath>\R_SERVICES\library\RevoScaleR\rxLibs\x64\RegisterRExe.exe`.

2. Run the following command, providing appropriate arguments for your environment:

    `RegisterRExt.exe /install pkgmgmt [/instance:name] [/user:username] [/password:*|password]`

    This command creates instance-level objects on the SQL Server computer that are required for package management. It also restarts the Launchpad for the instance.

    If you do not specify an instance, the default instance is used. If you do not specify a user, the current security context is used. For example, the following command enables package management on the instance in the path of RegisterRExt.exe, using the credentials of the user who opened the command prompt:

    `REgisterRExt.exe /install pkgmgmt`

3. To add package management to a specific database, run the following command from an elevated command prompt:

    `RegisterRExt.exe /install pkgmgmt /database:databasename [/instance:name] [/user:username] [/password:*|password]`
   
    This command creates some database artifacts, including the following database roles that are used for controlling user permissions: `rpkgs-users`, `rpkgs-private`, and `rpkgs-shared`.

    For example, the following command enables package management on the database, on the instance where RegisterRExt is run. If you do not specify a user, the current security context is used.

    `RegisterRExt.exe /install pkgmgmt /database:TestDB`

4. Repeat the command for each database where packages must be installed.

5. To verify that the new roles have been successfully created, in SQL Server Management Studio, click the database, expand **Security**, and expand **Database Roles**.

    You can also run a query on sys.database_principals such as the following:

    ```sql
    SELECT pr.principal_id, pr.name, pr.type_desc,   
        pr.authentication_type_desc, pe.state_desc,   
        pe.permission_name, s.name + '.' + o.name AS ObjectName  
    FROM sys.database_principals AS pr  
    JOIN sys.database_permissions AS pe  
        ON pe.grantee_principal_id = pr.principal_id  
    JOIN sys.objects AS o  
        ON pe.major_id = o.object_id  
    JOIN sys.schemas AS s  
        ON o.schema_id = s.schema_id;
    ```

After you have enabled this feature, you can use RevoScaleR function to install or uninstall packages from a remote R client.

## <a name="bkmk_disable"></a> Disable package management

1. From an elevated command prompt, run the RegisterRExt utility again, and disable package management at the database level:

    `RegisterRExt.exe /uninstall pkgmgmt /database:databasename [/instance:name] [/user:username] [/password:*|password]`

    This command removes database objects related to package management from the specified database. It also removes all the packages that were installed from the secured file system location on the SQL Server computer.

2. Repeat this command on each database where package management was used.

3.  (Optional) After all databases have been cleared of packages using the preceding step, run the following command from an elevated command prompt:

    `RegisterRExt.exe /uninstall pkgmgmt [/instance:name] [/user:username] [/password:*|password]`

    This command removes the package management feature from the instance. You might need to manually restart the Launchpad service once more to see changes.

## Next steps

+ [Use RevoScaleR to install new R packages](use-revoscaler-to-manage-r-packages.md)
+ [Tips for installing R packages](packages-installed-in-user-libraries.md)
+ [Default packages](installing-and-managing-r-packages.md)
