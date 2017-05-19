---
title: "How to Enable or Disable R Package Management | Microsoft Docs"
ms.custom: ""
ms.date: "12/21/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 6e384893-04da-43f9-b100-bfe99888f085
caps.latest.revision: 7
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# R Package - How to Enable or Disable

By default, package management is disabled on a SQL Server instance, even if R Services is installed. To enable this feature is a two-step process which must be performed by a database administrator: 

1. Enable package management on the SQL Server instance (once per SQL Server instance) 
2. Enable package management on the SQL database (once per SQL Server database) 


When disabling the package management feature, reverse the process to remove database-level packages and permissions, and then remove the roles from the server:
 
1. Disable package management on each database (once per database) 
2. Disable package management on the SQL Server instance (once per instance) 

> [!IMPORTANT]
> This feature is in development. Please be aware that the syntax or functionality might change in later releases. 

### To enable package management

To enable or disable package management requires the command-line utility **RegisterRExt.exe**, which is included with the **RevoScaleR** package installed with SQL Server R Services. the default location is:

`<SQLInstancePath>\R_SERVICES\library\RevoScaleR\rxLibs\x64\RegisterRExe.exe` 
    
1. Open an elevated command prompt and use the following command:

    `RegisterRExt.exe /installpkgmgmt [/instance:name] [/user:username] [/password:*|password]`

    This command creates instance-level artifacts on the SQL Server computer that are required for package management. 

2. To add package management at the database level, for each database where packages must be installed, run the following command from an elevated command prompt: 

    `RegisterRExt.exe /installpkgmgmt /database:databasename [/instance:name] [/user:username] [/password:*|password]` 

    This command creates some database artifacts, including the following database roles that are required for controlling user permissions: **rpkgs-users**, **rpkgs-private**, and **rpkgs-shared** 

### To disable package management 

1. From an elevated command prompt, run the following command to disable package management at the database level:

   `RegisterRExt.exe /uninstallpkgmgmt /database:databasename [/instance:name] [/user:username] [/password:*|password]` 

    This command will remove database artifacts related to package management from the specified database.  The command will also remove all the packages that were installed per database from the secured file system location on the SQL Server computer.
    
    The command must be run once for each database where package management was used.
 
2. (Optional) To entirely remove the package management feature from the instance, after all databases have been cleared of packages using the preceding step, run the following command from an elevated command prompt:

    `RegisterRExt.exe /uninstallpkgmgmt [/instance:name] [/user:username] [/password:*|password]`

    This command removes the instance-level artifacts used by package management from the SQL Server instance. 


## See Also
[R Package Management for SQL Server R Services](../../advanced-analytics/r-services/r-package-management-for-sql-server-r-services.md)
