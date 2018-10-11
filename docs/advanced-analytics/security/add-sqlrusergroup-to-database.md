---
title: Add SQLRUserGroup as a database user (SQL Server Machine Learning) | Microsoft Docs
description: How to add SQLRUserGroup as a database user for SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning

ms.date: 10/10/2018  
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Add SQLRUserGroup as a database user
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Create a database login for the [SQLRUserGroup](../concepts/security.md#sqlrusergroup) to allow trusted connections originating from R and Python scripts when the target is data or operations on the SQL Server instance. 

For scripts containing connection strings with SQL Server logins or a fully-specified user name and password, creating a login is not required.

## When a login is required

If R or Python script includes a connection string specifying a trusted connection (for example, "Trusted_Connection=True"), additional configuration is necessary for the correct presentation of the user identity to SQL Server. For external processes running under a **SQLRUserGroup** worker account, such as MSSQLSERVER01, the trusted user is presented as the worker identity. Because this identity has no login rights to SQL Server, trusted connections will fail unless you add **SQLRUserGroup** as a database user. For more information, see [*implied authentication*](../../advanced-analytics/concepts/security.md#implied-authentication).

Recall that Launchpad retains a mapping of the original user who invoked the script and the worker account running the process. Once the trusted connection succeeds for the worker account, the identity of the original calling user takes over and is used to retrieve the data. You do not need to grant db_datareader permissions to **SQLRUserGroup**.

> [!Note]
>  Make sure that **SQLRUserGroup** has "Allow Log on locally" permissions. By default, this right is given to all new local users, but in some organizations stricter group policies might be enforced.

## Create a login

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