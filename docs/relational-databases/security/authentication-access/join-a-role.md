---
title: "Join a Role"
description: Learn how to assign roles to logins and database users in SQL Server by using SQL Server Management Studio or Transact-SQL. Use roles to manage permissions.
author: VanMSFT
ms.author: vanto
ms.date: 11/05/2024
ms.service: sql
ms.subservice: security
ms.topic: how-to
ms.custom:
  - ignite-2025
f1_keywords:
  - "SQL13.SWB.DATABASEUSER.MEMBERSHIP.F1"
helpviewer_keywords:
  - "adding a member to a role"
  - "join a role"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Join a Role

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

This article describes how to assign roles to logins and database users in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)]. Use roles in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to efficiently manage permissions. Assign permissions to roles, and then add and remove users and logins to the roles. By using roles, permissions do not have to be individually maintained for each user.  
  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports four types of roles.  
  
-   Fixed server roles  
  
-   User-defined server roles  
  
-   Fixed database roles  
  
-   User-defined database roles  
  
 The fixed roles are automatically available in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Fixed roles have the necessary permissions to accomplish common tasks. For more information about fixed roles, see the following links. User-defined roles are created by you, and can be customized with the permissions that you select. For more information about user-defined roles, see the following links.  
  
<a name="SSMSProcedure"></a>

## Use SQL Server Management Studio

> [!NOTE]
> The two procedures in this section only apply to [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)].

<a id="to-add-a-member-to-a-fixed-server-role"></a>

#### Add a member to a fixed server role
  
1. In Object Explorer, expand the server in which you want to edit a fixed server role.  
  
1. Expand the **Security** folder.  
  
1. Expand the **Server Roles** folder.
  
1. Right-click the role you want to edit and select **Properties**.  
  
1. In the **Server Role Properties** dialog box, select the **Members** page, select **Add**.  
  
1. In the **Select Server Login or Role** dialog box, under **Enter the object names to select (examples)**, enter the login or server role to add to this server role. Alternately, select **Browse...** and select any or all of the available objects in the **Browse for Objects** dialog box. Select **OK** to return to the **Server Role Properties** dialog box.  
  
1. Select **OK**.
  
<a id="to-add-a-member-to-a-user-defined-database-role"></a>

#### Add a member to a user-defined database role
  
1. In Object Explorer, expand the server in which you want to edit a user-defined database role.  
  
1. Expand the **Databases** folder.  
  
1. Expand the database in which you want to edit a user-defined database role.  
  
1. Expand the **Security** folder.  
  
1. Expand the **Roles** folder.  
  
1. Expand the **Database Roles** folder.  
  
1. Right-click the role you want to edit and select **Properties**.  
  
1. In the **Database Role Properties -**_database\_role\_name_ dialog box, in the **General** page, select **Add**.  
  
1. In the **Select Database User or Role** dialog box, under **Enter the object names to select (examples)**, enter the login or database role to add to this database role. Alternately, select **Browse...** and select any or all of the available objects in the **Browse for Objects** dialog box. Select **OK** to return to the **Database Role Properties -**_database\_role\_name_ dialog box.  
  
1. Select **OK**.
  
<a id="TsqlProcedure"></a>

## Use Transact-SQL
  
<a id="to-add-a-member-to-a-fixed-server-role"></a>

#### Add a member to a fixed server role
  
1. In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
1. On the Standard bar, select **New Query**.  
  
1. Copy and paste the following example into the query window and select **Execute**.  
  
    ```sql
    ALTER SERVER ROLE diskadmin ADD MEMBER [Domain\Juan] ;  
    GO  
    ```  
  
 For more information, see [ALTER SERVER ROLE (Transact-SQL)](../../../t-sql/statements/alter-server-role-transact-sql.md).  
  
<a id="to-add-a-member-to-a-user-defined-database-role"></a>

#### Add a member to a user-defined database role
  
1. In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
1. On the Standard bar, select **New Query**.  
  
1. Copy and paste the following example into the query window and select **Execute**.  
  
    ```sql
    ALTER ROLE Marketing ADD MEMBER [Domain\Juan] ;  
    GO  
    ```  
  
 For more information, see [ALTER ROLE (Transact-SQL)](../../../t-sql/statements/alter-role-transact-sql.md).  

#### Permissions

 Requires **ALTER ANY ROLE** permission on the database, **ALTER** permission on the role, or membership in **db_securityadmin**.  

 In [!INCLUDE [fabric-sqldb](../../../includes/fabric-sqldb.md)], users/apps with the Write item permission in Fabric can grant any permissions.

<a name="Restrictions"></a> 

## Limitations
  
- Changing the name of a database role does not change ID number, owner, or permissions of the role.
- Database roles are visible in the `sys.database_role_members` and `sys.database_principals` catalog views.
- In [!INCLUDE [fabric-sqldb](../../../includes/fabric-sqldb.md)], only database-level users and roles are supported. In [!INCLUDE [fabric-sqldb](../../../includes/fabric-sqldb.md)], Microsoft Entra ID for database users is the only supported authentication method. For more information, see [Authorization in SQL database in Microsoft Fabric](/fabric/database/sql/authorization).
  
## Related content

- [Server-level roles](server-level-roles.md)
- [Database-level roles](database-level-roles.md)
- [Application Roles](application-roles.md)
