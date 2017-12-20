---
title: "Enable or disable R package management for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "10/05/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 6e384893-04da-43f9-b100-bfe99888f085
caps.latest.revision: 7
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
ms.workload: "Inactive"
---
# Enable or disable R package management for SQL Server

This article describes the process of enabling or disabling the new package management feature in SQL Server 2017. This feature allows the database administrator to control package installation on the instance. The feature relies on new database roles to grant users the ability to install the R packages they need, or share packages with other users.

By default, the external package management feature for SQL Server is disabled, even if machine learning features have been installed.

To [enable](#bkmk_enable) this feature is a two-step process and requires some help from a database administrator:

1.  Enable package management on the SQL Server instance (once per SQL Server
    instance)

2.  Enable package management on the SQL database (once per SQL Server database)

To [disable](#bkmk_disable) the package management feature, reverse the process to remove database-level packages and permissions, and then remove the roles from the server:

1.  Disable package management on each database (once per database)

2.  Disable package management on the SQL Server instance (once per instance)

## <a name="bkmk_enable"></a> Enable package management

To enable or disable package management requires the command-line utility **RegisterRExt.exe**, which is included with the **RevoScaleR** package.

1. Open an elevated command prompt and navigate to the folder containing the utility, RegisterRExt.exe. The default location is `<SQLInstancePath>\R_SERVICES\library\RevoScaleR\rxLibs\x64\RegisterRExe.exe`.

2. Run the following command, providing appropiate arguments for your environment:

    `RegisterRExt.exe /installpkgmgmt [/instance:name] [/user:username] [/password:*|password]`

    This command creates instance-level objects on the SQL Server computer that are required for package management. It also restarts the Launchpad for the instance.

    If you do not specify an instance, the default instance is used.

    If you do not specify a user, the current security context is used.

2.  To add package management at the database level, run the following command from an elevated command prompt:

    `RegisterRExt.exe /installpkgmgmt /database:databasename [/instance:name] [/user:username] [/password:*|password]`
   
    This command creates some database artifacts, including the following database roles that are used for controlling user permissions: `rpkgs-users`, `rpkgs-private`, and `rpkgs-shared`.

    If you do not specify a user, the current security context is used.

3. Repeat the command for each database where packages must be installed.

4.  To verify that the new roles have been successfully created, in SQL Server Management Studio, click the database, expand **Security**, and expand **Database Roles**.

    You can also run a query on sys.database_principals such as the following:

    ```SQL
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

4.  After the feature has been enabled, any user with the appropriate permissions can use the [CREATE EXTERNAL LIBRARY](https://docs.microsoft.com/sql/t-sql/statements/create-external-library-transact-sql) statement in T-SQL to add packages. For an example of how this works, see [Install additional packages on SQL Server](install-additional-r-packages-on-sql-server.md).

## <a name="bkmk_disable"></a> Disable package management

1.  From an elevated command prompt, run the following command to disable package management at the database level:

    `RegisterRExt.exe /uninstallpkgmgmt /database:databasename [/instance:name] [/user:username] [/password:*|password]`

    Run this command once for each database where package management was used. This command will remove database objects related to package management from the specified database. It will also remove all the packages that were installed from the secured file system location on the SQL Server computer.

2.  (Optional) After all databases have been cleared of packages using the preceding step, run the following command from an elevated command prompt:

    `RegisterRExt.exe /uninstallpkgmgmt [/instance:name] [/user:username] [/password:*|password]`

    This command removes the package management feature from the instance.

## See also

[R package management for SQL Server](r-package-management-for-sql-server-r-services.md)
