---
title: Create login and permissions in Pacemaker for SQL Server on Linux | Microsoft Docs
description: How to use Pacemaker to create login and permissions for SQL Server on Linux.
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 12/4/2017
ms.topic: article
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine"
ms.service: ""
ms.component: "linux"
ms.suite: "sql"
ms.custom: ""
ms.technology: database-engine
ms.workload: "On Demand"
---

# Create the SQL Server login and permissions in Pacemaker

[!INCLUDE[tsql-appliesto-sslinux-only](../includes/tsql-appliesto-sslinux-only.md)]

An underlying Pacemaker cluster needs access to the SQL Server instance as well as permissions on the availability group (AG) itself. These steps create the login and the associated permissions, along with a file that tells Pacemaker how to log into SQL Server.

1.  In a query window connected to the first replica, execute the following:

    ```t-sql
    CREATE LOGIN PMLogin WITH PASSWORD '<StrongPassword>';
    
    GO
    
    GRANT VIEW SERVER STATE TO PMLogin;
    
    GO
    
    GRANT ALTER, CONTROL, VIEW DEFINITION ON AVAILABLITY GROUP::<AGThatWasCreated> TO PMLogin;
    
    GO
    ```
    
2.  On Node 1, enter the command 
    ```bash
    sudo emacs /var/opt/mssql/secrets/passwd
    ```
    
    This will open the Emacs editor.
    
3.  Enter the following two lines into the editor:

    ```
    PMLogin
    <StrongPassword>
    ```
    
4.  Hold down the CTRL key and then press X, then C, to exit and save the file.

5.  Execute 
    ```bash
    sudo chmod 400 /var/opt/mssql/secrets/passwd
    ```
    
    to lock down the file.

6.  Repeat Steps 1-5 on the other servers that will serve as replicas.

