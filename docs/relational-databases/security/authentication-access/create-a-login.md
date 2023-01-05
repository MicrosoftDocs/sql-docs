---
title: "Create a Login | Microsoft Docs"
description: Learn how to create a login in SQL Server or Azure SQL Database by using SQL Server Management Studio or Transact-SQL.
ms.custom: ""
ms.date: "03/31/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.login.status.f1"
  - "sql13.swb.login.effectivepermissions.f1"
  - "sql13.swb.login.general.f1"
  - "sql13.swb.login.databaseaccess.f1"
  - "sql13.swb.login.serverroles.f1"
helpviewer_keywords: 
  - "authentication [SQL Server], logins"
  - "logins [SQL Server], creating"
  - "creating logins with Management Studio"
  - "Create login [SQL Server]"
  - "SQL Server logins"
ms.assetid: fb163e47-1546-4682-abaa-8c9494e9ddc7
author: VanMSFT
ms.author: vanto
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create a Login
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  This article describes how to create a login in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] or Azure [!INCLUDE[ssSDS](../../../includes/sssds-md.md)] by using [SQL Server Management Studio (SSMS)](../../../ssms/download-sql-server-management-studio-ssms.md) or [!INCLUDE[tsql](../../../includes/tsql-md.md)]. A login is the identity of the person or process that is connecting to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
##  <a name="Background"></a> Background  
 A login is a security principal, or an entity that can be authenticated by a secure system. Users need a login to connect to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. You can create a login based on a Windows principal (such as a domain user or a Windows domain group) or you can create a login that isn't based on a Windows principal (such as an [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login).  
  
> [!NOTE]
> To use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication, the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] must use mixed mode authentication. For more information, see [Choose an Authentication Mode](../../../relational-databases/security/choose-an-authentication-mode.md).
>
> Azure SQL has introduced [Azure Active Directory server principals (logins)](/azure/azure-sql/database/authentication-azure-ad-logins) to be used to authenticate to Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics (dedicated SQL pools only).
  
 As a security principal, permissions can be granted to logins. The scope of a login is the whole [!INCLUDE[ssDE](../../../includes/ssde-md.md)]. To connect to a specific database on the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], a login must be mapped to a database user. Permissions inside the database are granted and denied to the database user, not the login. Permissions that have the scope of the whole instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] (for example, the **CREATE ENDPOINT** permission) can be granted to a login.  
  
> [!NOTE]
> When a login connects to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], the identity is validated at the master database. Use contained database users to authenticate [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDS](../../../includes/sssds-md.md)] connections at the database level. When using contained database users, a login is not necessary. A contained database is a database that is isolated from other databases and from the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] or [!INCLUDE[ssSDS](../../../includes/sssds-md.md)] (and the master database) that hosts the database. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports contained database users for both Windows and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] authentication. When using [!INCLUDE[ssSDS](../../../includes/sssds-md.md)], combine contained database users with database level firewall rules. For more information, see [Contained Database Users - Making Your Database Portable](../../../relational-databases/security/contained-database-users-making-your-database-portable.md).  
  
##  <a name="Security"></a> Security  

 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] requires **ALTER ANY LOGIN** or **ALTER LOGIN** permission on the server.  
  
 [!INCLUDE[ssSDS](../../../includes/sssds-md.md)] requires membership in the **loginmanager** role.  
  
##  <a name="SSMSProcedure"></a> Create a login using SSMS for SQL Server
  
1.  In Object Explorer, expand the folder of the server instance in which you want to create the new login.  
  
2.  Right-click the **Security** folder, point to **New**, and select **Login...**.  
  
3.  In the **Login - New** dialog box, on the **General** page, enter the name of a user in the **Login name** box. Alternately, select **Search...** to open the **Select User or Group** dialog box.  
  
     If you select **Search...**:  
  
    1.  Under **Select this object type**, select **Object Types...** to open the **Object Types** dialog box and select any or all of the following: **Built-in security principals**, **Groups**, and **Users**. **Built-in security principals** and **Users** are selected by default. When finished, select **OK**.  
  
    2.  Under **From this location**, select **Locations...** to open the **Locations** dialog box and select one of the available server locations. When finished, select **OK**.  
  
    3.  Under **Enter the object name to select (examples)**, enter the user or group name that you want to find. For more information, see [Select Users, Computers, or Groups Dialog Box](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/cc771712(v=ws.11)).  
  
    4.  Select **Advanced...** for more advanced search options. For more information, see [Select Users, Computers, or Groups Dialog Box - Advanced Page](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/cc733110(v=ws.11)).  
  
    5.  Select **OK**.  
  
4.  To create a login based on a Windows principal, select **Windows authentication**. This is the default selection.  
  
5.  To create a login that is saved on a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database, select **SQL Server authentication**.  
  
    1.  In the **Password** box, enter a password for the new user. Enter that password again into the **Confirm Password** box.  
  
    2.  When changing an existing password, select **Specify old password**, and then type the old password in the **Old password** box.  
  
    3.  To enforce password policy options for complexity and enforcement, select **Enforce password policy**. For more information, see [Password Policy](../../../relational-databases/security/password-policy.md). This is a default option when **SQL Server authentication** is selected.  
  
    4.  To enforce password policy options for expiration, select **Enforce password expiration**. **Enforce password policy** must be selected to enable this checkbox. This is a default option when **SQL Server authentication** is selected.  
  
    5.  To force the user to create a new password after the first time the login is used, select **User must change password at next login**. **Enforce password expiration** must be selected to enable this checkbox. This is a default option when **SQL Server authentication** is selected.  
  
6.  To associate the login with a stand-alone security certificate, select **Mapped to certificate** and then select the name of an existing certificate from the list.  
  
7.  To associate the login with a stand-alone asymmetric key, select **Mapped to asymmetric key** to, and then select the name of an existing key from the list.  
  
8.  To associate the login with a security credential, select the **Mapped to Credential** check box, and then either select an existing credential from the list or select **Add** to create a new credential. To remove a mapping to a security credential from the login, select the credential from **Mapped Credentials** and select **Remove**. For more information about credentials in general, see [Credentials &#40;Database Engine&#41;](../../../relational-databases/security/authentication-access/credentials-database-engine.md).  
  
9. From the **Default database** list, select a default database for the login. **Master** is the default for this option.  
  
10. From the **Default language** list, select a default language for the login.  
  
11. Select **OK**.

### Additional options  
 The **Login - New** dialog box also offers options on four additional pages: **Server Roles**, **User Mapping**, **Securables**, and **Status**.  
  
### Server roles

> [!NOTE]
> These server roles are not available for SQL Database.

 The **Server Roles** page lists all possible roles that can be assigned to the new login. The following options are available:  
  
 **bulkadmin** check box  
 Members of the **bulkadmin** fixed server role can run the BULK INSERT statement.  
  
 **dbcreator** check box  
 Members of the **dbcreator** fixed server role can create, alter, drop, and restore any database.  
  
 **diskadmin** check box  
 Members of the **diskadmin** fixed server role can manage disk files.  
  
 **processadmin** check box  
 Members of the **processadmin** fixed server role can terminate processes running in an instance of the [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
 **public** check box  
 All SQL Server users, groups, and roles belong to the **public** fixed server role by default.  
  
 **securityadmin** check box  
 Members of the **securityadmin** fixed server role manage logins and their properties. They can GRANT, DENY, and REVOKE server-level permissions. They can also GRANT, DENY, and REVOKE database-level permissions. Additionally, they can reset passwords for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] logins.  
  
 **serveradmin** check box  
 Members of the **serveradmin** fixed server role can change server-wide configuration options and shut down the server.  
  
 **setupadmin** check box  
 Members of the **setupadmin** fixed server role can add and remove linked servers, and they can execute some system stored procedures.  
  
 **sysadmin** check box  
 Members of the **sysadmin** fixed server role can perform any activity in the [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
### User mapping  
 The **User Mapping** page lists all possible databases and the database role memberships on those databases that can be applied to the login. The databases selected determine the role memberships that are available for the login. The following options are available on this page:  
  
 **Users mapped to this login**  
 Select the databases that this login can access. When you select a database, its valid database roles are displayed in the **Database role membership for:** _database_name_ pane.  
  
 **Map**  
 Allow the login to access the databases listed below.  
  
 **Database**  
 Lists the databases available on the server.  
  
 **User**  
 Specify a database user to map to the login. By default, the database user has the same name as the login.  
  
 **Default Schema**  
 Specifies the default schema of the user. When a user is first created, its default schema is **dbo**. It's possible to specify a default schema that does not yet exist. You can't specify a default schema for a user that is mapped to a Windows group, a certificate, or an asymmetric key.  
  
 **Guest account enabled for:**  _database_name_  
 Read-only attribute indicating whether the Guest account is enabled on the selected database. Use the **Status** page of the **Login Properties** dialog box of the Guest account to enable or disable the Guest account.  
  
 **Database role membership for:**  _database_name_  
 Select the roles for the user in the specified database. All users are members of the **public** role in every database and can't be removed. For more information about database roles, see [Database-Level Roles](../../../relational-databases/security/authentication-access/database-level-roles.md).  
  
### Securables  
 The **Securables** page lists all possible securables and the permissions on those securables that can be granted to the login. The following options are available on this page:  
  
 **Upper Grid**  
 Contains one or more items for which permissions can be set. The columns that are displayed in the upper grid vary depending on the principal or securable.  
  
 To add items to the upper grid:  
  
1.  Select **Search**.  
  
2.  In the **Add Objects** dialog box, select one of the following options: **Specific objects...**, **All objects of the types...**, or **The server**_server\_name_. Select **OK**.
  
    > [!NOTE]  
    > Selecting **The server**_server\_name_ automatically fills the upper grid with all of that servers' securable objects.  
  
3.  If you select **Specific objects...**:  
  
    1.  In the **Select Objects** dialog box, under **Select these object types**, select **Object Types...**.  
  
    2.  In the **Select Object Types** dialog box, select any or all of the following object types: **Endpoints**, **Logins**, **Servers**, **Availability Groups**, and **Server roles**. Select **OK**.
  
    3.  Under **Enter the object names to select (examples)**, select **Browse...**.  
  
    4.  In the **Browse for Objects** dialog box, select any of the available objects of the type that you selected in the **Select Object Types** dialog box, and then select **OK**.  
  
    5.  In the **Select Objects** dialog box, select **OK**.  
  
4.  If you select **All objects of the types...**, in the **Select Object Types** dialog box, select any or all of the following object types: **Endpoints**, **Logins**, **Servers**, **Availability Groups**, and **Server roles**. Select **OK**.
  
 **Name**  
 The name of each principal or securable that is added to the grid.  
  
 **Type**  
 Describes the type of each item.  
  
 **Explicit Tab**  
 Lists the possible permissions for the securable that are selected in the upper grid. Not all options are available for all explicit permissions.  
  
 **Permissions**  
 The name of the permission.  
  
 **Grantor**  
 The principal that granted the permission.  
  
 **Grant**  
 Select to grant this permission to the login. Clear to revoke this permission.  
  
 **With Grant**  
 Reflects the state of the WITH GRANT option for the listed permission. This box is read-only. To apply this permission, use the [GRANT](../../../t-sql/statements/grant-transact-sql.md) statement.  
  
 **Deny**  
 Select to deny this permission to the login. Clear to revoke this permission.  
  
### Status  
 The **Status** page lists some of the authentication and authorization options that can be configured on the selected [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login.  
  
 The following options are available on this page:  
  
 **Permission to connect to database engine**  
 When you work with this setting, you should think of the selected login as a principal that can be granted or denied permission on a securable.  
  
 Select **Grant** to grant CONNECT SQL permission to the login. Select **Deny** to deny CONNECT SQL to the login.  
  
 **Login**  
 When you work with this setting, you should think of the selected login as a record in a table. Changes to the values listed here will be applied to the record.  
  
 A login that has been disabled continues to exist as a record. But if it tries to connect to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], the login won't be authenticated.  
  
 Select this option to enable or disable this login. This option uses the ALTER LOGIN statement with either the ENABLE or DISABLE option.  
  
 **SQL Server Authentication**  
 The check box **Login is locked out** is only available if the selected login connects using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication and the login has been locked out. This setting is read-only. To unlock a login that is locked out, execute ALTER LOGIN with the UNLOCK option.  
  
##  <a name="TsqlProcedure"></a> Create a login using Windows Authentication with T-SQL  
  
 
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2.  On the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**.  
  
    ```  
    -- Create a login for SQL Server by specifying a server name and a Windows domain account name.  
  
    CREATE LOGIN [<domainName>\<loginName>] FROM WINDOWS;  
    GO  
  
    ```  
  
## Create a login using SQL Server Authentication with T-SQL
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2.  On the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**.  
  
    ```  
    -- Creates the user "shcooper" for SQL Server using the security credential "RestrictedFaculty"   
    -- The user login starts with the password "Baz1nga," but that password must be changed after the first login.  
  
    CREATE LOGIN shcooper   
       WITH PASSWORD = 'Baz1nga' MUST_CHANGE,  
       CREDENTIAL = RestrictedFaculty;  
    GO  
    ```  
  
 For more information, see [CREATE LOGIN &#40;Transact-SQL&#41;](../../../t-sql/statements/create-login-transact-sql.md).  
  
##  <a name="FollowUp"></a> Follow up: Steps to take after you create a login  
 After creating a login, the login can connect to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], but doesn't necessarily have sufficient permission to perform any useful work. The following list provides links to common login actions.  
  
-   To have the login join a role, see [Join a Role](../../../relational-databases/security/authentication-access/join-a-role.md).  
  
-   To authorize a login to use a database, see [Create a Database User](../../../relational-databases/security/authentication-access/create-a-database-user.md).  
  
-   To grant a permission to a login, see [Grant a Permission to a Principal](../../../relational-databases/security/authentication-access/grant-a-permission-to-a-principal.md).  
  
## See also

- [Security Center for SQL Server Database Engine and Azure SQL Database](../../../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md)  
- [Azure Active Directory server principals (logins)](/azure/azure-sql/database/authentication-azure-ad-logins)
- [Tutorial: Create and utilize Azure Active Directory server logins](/azure/azure-sql/database/authentication-azure-ad-logins-tutorial)
