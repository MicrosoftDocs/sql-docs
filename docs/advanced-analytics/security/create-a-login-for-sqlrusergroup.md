---
title: Create a login for SQLRUserGroup - SQL Server Machine Learning Services
description: For loopback connections using implied authentication, create a login in SQL Server for SQLRUserGroup, so that a worker account can log in to the server, for identity conversion back to the calling user.
ms.prod: sql
ms.technology: machine-learning

ms.date: 01/25/2019  
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Create a login for SQLRUserGroup
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Create a [login in SQL Server](https://docs.microsoft.com/sql/relational-databases/security/authentication-access/create-a-login) for [SQLRUserGroup](../concepts/security.md#sqlrusergroup) when a [loop back connection](../../advanced-analytics/concepts/security.md#implied-authentication) in your script specifies a *trusted connection*, and the identity used to execute an object contains your code is a Windows user account.

Trusted connections are those having `Trusted_Connection=True` in the connection string. When SQL Server receives a request specifying a trusted connection, it checks whether the identity of the current Windows user has a login. For external processes executing as a worker account (such as MSSQLSERVER01 from **SQLRUserGroup**), the request fails because those accounts do not have a login by default.

You can work around the connection error by creating a login for  **SQLServerRUserGroup**. For more information about identities and external processes, see [Security overview for the extensibility framework](../concepts/security.md).

> [!Note]
> Make sure that **SQLRUserGroup** has "Allow Log on locally" permissions. By default, this right is given to all new local users, but some organizations stricter group policies might disable this right.

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
    + If you are using a _named instance_, the instance name is appended to the name of the default worker group name, `SQLRUserGroup`. For example, if your instance is named "MLTEST", the default user group name for this instance would be **SQLRUserGroupMLTest**.
 
    ![Example of groups on server](media/implied-auth-login5.png "Example of groups on server")
   
5. Click **OK** to close the advanced search dialog box.

    > [!IMPORTANT]
    > Be sure you've selected the correct account for the instance. Each instance can use only its own Launchpad service and the group created for that service. Instances cannot share a Launchpad service or worker accounts.

6. Click **OK** once more to close the **Select User or Group** dialog box.

7. In the **Login - New** dialog box, click **OK**. By default, the login is assigned to the **public** role and has permission to connect to the database engine.

## Next steps

+ [Security overview](../concepts/security.md)
+ [Extensibility framework](../concepts/extensibility-framework.md)
