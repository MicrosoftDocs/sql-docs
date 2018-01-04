---
title: "Add SQLRUserGroup as a database user | Microsoft Docs"
ms.custom: ""
ms.date: "12/21/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
keywords: 
  - "implied authentication"
  - "SQLRUserGroup"
ms.assetid: 4d773c74-c779-4fc2-b1b6-ec4b4990950d
caps.latest.revision: 1
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "Active"
---
# Add SQLRUserGroup as a database user

This article explains how to give the group of worker accounts used by machine learning services in SQL Server the permissions required to connect to the database and run R or Python jobs on behalf of the user.

## What is SQLRUserGroup?

During setup of [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)] or [!INCLUDE[rsql-productname-md](../../includes/rsql-productname-md.md)], new Windows user accounts are created to support execution of R or Python script tasks under the security token of the [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] service.

You can view these accounts in the Windows user group **SQLRUserGroup**. By default, 20 worker accounts are created, which is usually more than enough for running machine learning jobs.

When a user sends a machine learning script from an external client, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] activates an available worker account, maps it to the identity of the calling user, and runs the script on behalf of the user. This new service of the database engine supports the secure execution of external scripts, called *implied authentication*.

However, if you need to run R or Python scripts from a remote data science client, and you are using Windows authentication, you must give these worker accounts permission to sign in to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance on your behalf.

## Add SQLRUserGroup as a SQL Server login

1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], in Object Explorer, expand **Security**, right-click **Logins**, and select **New Login**.

2. In the **Login - New** dialog box, select **Search**. (Don't type anything in the box yet.)
    
     ![Click search to add new login for machine learning](media/implied-auth-login1.png "Click search to add new login for machine learning")

3. In the **Select User or Group** box, click the **Object Types** button.

     ![Search object types to add new login for machine learning](media/implied-auth-login2.png "Search object types to add new login for machine learning")

4. In the **Object Types** dialog box, select **Groups**. Clear all other check boxes.

     ![Select Groups in Object Types dialog box](media/implied-auth-login3.png "Select Groups in Object Types dialog box")

4. Click **Advanced**, verify that the location to search is the current computer, and then click **Find Now**.

     ![Click Find Now to get list of groups](media/implied-auth-login4.png "Click Find Now to get list of groups")

5. Scroll through the list of group accounts on the server until you find one beginning with `SQLRUserGroup`.
    
    + The name of the group that's associated with the Launchpad service for the _default instance_ is always **SQLRUserGroup**, regardless of whether you installed R or Python or both. Select this account for the default instance only.
    + If you are using a _named instance_, the instance name is appended to the name of the default worker group name, `SQLRUserGroup`. Hence, if your instance is named "MLTEST", the default user group name for this instance would be **SQLRUserGroupMLTest**.
 
     ![Example of groups on server](media/implied-auth-login5.png "Example of groups on server")
   
5. Click **OK** to close the advanced search dialog box.

    > [!IMPORTANT]
    > Be sure you've selected the correct account for the instance. Each instance can use only its own Launchpad service and the group created for that service. Instances cannot share a Launchpad service or worker accounts.

6. Click **OK** once more to close the **Select User or Group** dialog box.

7. In the **Login - New** dialog box, click **OK**. By default, the login is assigned to the **public** role and has permission to connect to the database engine.

## Change the number of worker accounts in SQLRUserGroup

If you intend to make heavy use of machine learning, you can increase the number of accounts used to run external scripts, as described in this article: 

+ [Modify the user account pool for machine learning](modify-the-user-account-pool-for-sql-server-r-services.md)

By default, 20 accounts are created, which supports 20 concurrent sessions. Parallelized tasks do not consume additional accounts. For example, if a user runs a scoring task that uses parallel processing, the same worker account is reused for all threads.
