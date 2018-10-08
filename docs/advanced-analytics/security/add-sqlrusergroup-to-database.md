---
title: Add SQLRUserGroup as a database user (SQL Server Machine Learning) | Microsoft Docs
description: How to add SQLRUserGroup as a database user for SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning

ms.date: 09/26/2018  
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Add SQLRUserGroup as a database user
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article explains how to give the group of worker accounts used by machine learning services in SQL Server the permissions required to connect to the database and run R or Python jobs on behalf of the user.

As part of the installation process for [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)], a new Windows *user account pool* is created to support execution of tasks by the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service. The purpose of these worker accounts is to isolate concurrent execution of external scripts by different SQL users.

For more information, see the Launchpad section in [Security overview](../../advanced-analytics/concepts/security.md#launchpad).

## Add SQLRUserGroup as a SQL Server login

By default, the group of worker accounts does **not** have login permissions on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance with which it is associated. This can be a problem if any R or Python users connect to SQL Server from a remote client to run external scripts, or if a script uses ODBC to get additional data.

If you need to run scripts from a remote data science client, and you are using Windows authentication, additional configuration is required to give worker accounts running R and Python processes permission to sign in to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance on your behalf. 

> [!NOTE]
> If you use a **SQL login** for running scripts in a SQL Server compute context, this extra step is not required.

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

+ [Modify the user account pool for machine learning](../../advanced-analytics/administration/modify-user-account-pool.md)

By default, 20 accounts are created, which supports 20 concurrent sessions. Parallelized tasks do not consume additional accounts. For example, if a user runs a scoring task that uses parallel processing, the same worker account is reused for all threads.