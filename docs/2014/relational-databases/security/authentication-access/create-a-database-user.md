---
title: "Create a Database User | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
f1_keywords: 
  - "SQL12.SWB.DATABASEUSER.GENERAL.F1"
  - "sql12.swb.user.securables.f1"
helpviewer_keywords: 
  - "database users, creating"
  - "creating users with Management Studio"
  - "mapping users"
  - "users [SQL Server], creating"
  - "database user additions [SQL Server]"
  - "database users, mapping"
  - "CREATE USER [Management Studio]"
  - "users [SQL Server], adding"
  - "mapping database users"
ms.assetid: 782798d3-9552-4514-9f58-e87be4b264e4
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Create a Database User
  This topic describes how to create a database user mapped to a login in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)]. The database user is the identity of the login when it is connected to a database. The database user can use the same name as the login, but that is not required. This topic assumes that a login already exists in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. For information about how to create a login, see [Create a Login](create-a-login.md).  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Background](#Restrictions)  
  
     [Security](#Security)  
  
-   **To create a database user, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Background  
 A user is a database level security principal. Logins must be mapped to a database user to connect to a database. A login can be mapped to different databases as different users but can only be mapped as one user in each database. In a partially contained database, a user can be created that does not have a login. For more information about contained database users, see [CREATE USER &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-user-transact-sql). If the guest user in a database is enabled, a login that is not mapped to a database user can enter the database as the guest user.  
  
> [!IMPORTANT]  
>  The guest user is ordinarily disabled. Do not enable the guest user unless it is necessary.  
  
 As a security principal, permissions can be granted to users. The scope of a user is the database. To connect to a specific database on the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], a login must be mapped to a database user. Permissions inside the database are granted and denied to the database user, not the login.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires `ALTER ANY USER` permission on the database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
##### To create a database user  
  
1.  In Object Explorer, expand the **Databases** folder.  
  
2.  Expand the database in which to create the new database user.  
  
3.  Right-click the **Security** folder, point to **New**, and select **User...**.  
  
4.  In the **Database User - New** dialog box, on the **General** page, select one of the following user types from the **User type** list: **SQL user with login**, **SQL user without login**, **User mapped to a certificate**, **User mapped to an asymmetric key**, or **Windows user**.  
  
5.  In the **User name** box, enter a name for the new user. If you have chosen **Windows user** from the **User type** list, you can also click the ellipsis **(...)** to open the **Select User or Group** dialog box.  
  
6.  In the **Login name** box, enter the login for the user. Alternately, click the ellipsis **(...)** to open the **Select Login** dialog box. **Login name** is available if you select either **SQL user with login** or **Windows user** from the **User type** list.  
  
7.  In the **Default schema** box, specifies the schema that will own objects created by this user. Alternately, click the ellipsis **(...)** to open the **Select Schema** dialog box. **Default schema** is available if you select either **SQL user with login**, **SQL user without login**, or **Windows user** from the **User type** list.  
  
8.  In the **Certificate name** box, enter the certificate to be used for the database user. Alternately, click the ellipsis **(...)** to open the **Select Certificate** dialog box. **Certificate name** is available if you select **User mapped to a certificate** from the **User type** list.  
  
9. In the **Asymmetric key name**  box, enter the key to be used for the database user. Alternately, click the ellipsis **(...)** to open the **Select Asymmetric Key** dialog box. **Asymmetric key name** is available if you select **User mapped to an asymmetric key** from the **User type** list.  
  
10. [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
### Additional Options  
 The **Database User - New** dialog box also offers options on four additional pages: **Owned Schemas**, **Membership**, **Securables**, and **Extended Properties**.  
  
-   The **Owned Schemas** page lists all possible schemas that can be owned by the new database user. To add schemas to or remove them from a database user, under **Schemas owned by this user**, select or clear the check boxes next to the schemas.  
  
-   The **Membership** page lists all possible database membership roles that can be owned by the new database user. To add roles to or remove them from a database user, under **Database role membership**, select or clear the check boxes next to the roles.  
  
-   The **Securables** page lists all possible securables and the permissions on those securables that can be granted to the login.  
  
-   The **Extended properties** page allows you to add custom properties to database users. The following options are available on this page.  
  
     **Database**  
     Displays the name of the selected database. This field is read-only.  
  
     **Collation**  
     Displays the collation used for the selected database. This field is read-only.  
  
     **Properties**  
     View or specify the extended properties for the object. Each extended property consists of a name/value pair of metadata associated with the object.  
  
     **Ellipsis (...)**  
     Click the ellipsis **(...)** after **Value** to open the **Value for Extended Property** dialog box. Type or view the value of the extended property in this larger location. For more information, see [Value for Extended Property Dialog Box](../../databases/value-for-extended-property-dialog-box.md).  
  
     **Delete**  
     Removes the selected extended property.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To create a database user  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- Creates the login AbolrousHazem with password '340$Uuxwp7Mcxo7Khy'.  
    CREATE LOGIN AbolrousHazem   
        WITH PASSWORD = '340$Uuxwp7Mcxo7Khy';  
    GO  
  
    -- Creates a database user for the login created above.  
    CREATE USER AbolrousHazem FOR LOGIN AbolrousHazem;  
    GO  
    ```  
  
 For more information, see [CREATE USER &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-user-transact-sql).  
  
## See Also  
 [Principals &#40;Database Engine&#41;](principals-database-engine.md)  
  
  
