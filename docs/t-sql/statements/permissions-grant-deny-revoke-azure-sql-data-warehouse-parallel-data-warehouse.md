---
title: "GRANT-DENY-REVOKE Perms-Azure SQL Data and Parallel Data Warehouses | Microsoft Docs"
ms.custom: ""
ms.date: "08/10/2017"
ms.prod: sql
ms.prod_service: "sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 5a3b7424-408e-4cb0-8957-667ebf4596fc
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# Permissions: GRANT, DENY, REVOKE (Azure SQL Data Warehouse, Parallel Data Warehouse)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]

  Use [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]**GRANT** and **DENY** statements to grant or deny a permission (such as **UPDATE**) on a securable (such as a database, table, view, etc.) to a security principal (a login, a database user, or a database role). Use **REVOKE** to remove the grant or deny of a permission.  
  
 Server level permissions are applied to logins. Database level permissions are applied to database users and database roles.  
  
 To see what permissions have been granted and denied, query the sys.server_permissions and sys.database_permissions views. Permissions that are not explicitly granted or denied to a security principal can be inherited by having membership in a role that has permissions. The permissions of the fixed database roles cannot be changed and do not appear in the sys.server_permissions and sys.database_permissions views.  
  
-   **GRANT** explicitly grants one or more permissions.  
  
-   **DENY** explicitly denies the principal from having one or more permissions.  
  
-   **REVOKE** removes existing **GRANT** or **DENY** permissions.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Azure SQL Data Warehouse and Parallel Data Warehouse  
GRANT   
    <permission> [ ,...n ]  
    [ ON [ <class_type> :: ] securable ]   
    TO principal [ ,...n ]  
    [ WITH GRANT OPTION ]  
[;]  
  
DENY   
    <permission> [ ,...n ]  
    [ ON [ <class_type> :: ] securable ]   
    TO principal [ ,...n ]  
    [ CASCADE ]  
[;]  
  
REVOKE   
    <permission> [ ,...n ]  
    [ ON [ <class_type> :: ] securable ]   
    [ FROM | TO ] principal [ ,...n ]  
    [ CASCADE ]  
[;]  
  
<permission> ::=  
{ see the tables below }  
  
<class_type> ::=  
{  
      LOGIN  
    | DATABASE  
    | OBJECT  
    | ROLE  
    | SCHEMA  
    | USER  
}  
```  
  
## Arguments  
 \<permission>[ **,**...*n* ]  
 One or more permissions to grant, deny, or revoke.  
  
 ON [ \<class_type> :: ] *securable* 
 The **ON** clause describes the securable parameter on which to grant, deny, or revoke permissions.  
  
 \<class_type> 
 The class type of the securable. This can be **LOGIN**, **DATABASE**, **OBJECT**, **SCHEMA**, **ROLE**, or **USER**. Permissions can also be granted to the **SERVER**_class\_type_, but **SERVER** is not specified for those permissions. **DATABASE** is not specified when the permission includes the word **DATABASE** (for example **ALTER ANY DATABASE**). When no *class_type* is specified and the permission type is not restricted to the server or database class, the class is assumed to be **OBJECT**.  
  
 *securable*  
 The name of the login, database, table, view, schema, procedure, role, or user on which to grant, deny, or revoke permissions. The object name can be specified with the three-part naming rules that are described in [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).  
  
 TO *principal* [ **,**...*n* ]  
 One or more principals being granted, denied, or revoked permissions. Principal is the name of a login, database user, or database role.  
  
 FROM *principal* [ **,**...*n* ]  
 One or more principals to revoke permissions from.  Principal is the name of a login, database user, or database role. **FROM** can only be used with a **REVOKE** statement. **TO** can be used with **GRANT**, **DENY**, or **REVOKE**.  
  
 WITH GRANT OPTION  
 Indicates that the grantee will also be given the ability to grant the specified permission to other principals.  
  
 CASCADE  
 Indicates that the permission is denied or revoked to the specified principal and to all other principals to which the principal granted the permission. Required when the principal has the permission with **GRANT OPTION**.  
  
 GRANT OPTION FOR  
 Indicates that the ability to grant the specified permission will be revoked. This is required when you are using the **CASCADE** argument.  
  
> [!IMPORTANT]  
>  If the principal has the specified permission without the **GRANT** option, the permission itself will be revoked.  
  
## Permissions  
 To grant a permission, the grantor must have either the permission itself with the **WITH GRANT OPTION**, or must have a higher permission that implies the permission being granted.  Object owners can grant permissions on the objects they own. Principals with **CONTROL** permission on a securable can grant permission on that securable.  Members of the **db_owner** and **db_securityadmin** fixed database roles can grant any permission in the database.  
  
## General Remarks  
 Denying or revoking permissions to a principal will not affect requests that have passed authorization and are currently running. To restrict access immediately, you must cancel active requests or kill current sessions.  
  
> [!NOTE]  
>  Most fixed server roles are not available in this release. Use user-defined database roles instead. Logins cannot be added to the **sysadmin** fixed server role. Granting the **CONTROL SERVER** permission approximates membership in the **sysadmin** fixed server role.  
  
 Some statements require multiple permissions. For example, to create a table requires the **CREATE TABLE** permissions in the database, and the **ALTER SCHEMA** permission for the table that will contain the table.  
  
 PDW sometimes executes stored procedures to distribute user actions to the compute nodes. Therefore, the execute permission for an entire database cannot be denied. (For example `DENY EXECUTE ON DATABASE::<name> TO <user>;` will fail.) As a work around, deny the execute permission to user-schemas or specific objects (procedures).  
  
### Implicit and Explicit Permissions  
 An *explicit permission* is a **GRANT** or **DENY** permission given to a principal by a **GRANT** or **DENY** statement.  
  
 An *implicit permission* is a **GRANT** or **DENY** permission that a principal (login, user, or database role) has inherited from another database role.  
  
 An implicit permission can also be inherited from a covering or parent permission. For example, **UPDATE** permission on a table can be inherited by having **UPDATE** permission on the schema that contains the table, or **CONTROL** permission on the table.  
  
### Ownership Chaining  
 When multiple database objects access each other sequentially, the sequence is known as a *chain*. Although such chains do not independently exist, when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] traverses the links in a chain, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] evaluates permissions on the constituent objects differently than it would if it were accessing the objects separately. Ownership chaining has important implications for managing security. For more information about ownership chains, see [Ownership Chains](https://msdn.microsoft.com/library/ms188676\(v=sql11\).aspx) and [Tutorial: Ownership Chains and Context Switching](../../relational-databases/tutorial-ownership-chains-and-context-switching.md).  
  
## Permission List  
  
### Server Level Permissions  
 Server level permissions can be granted, denied, and revoked from logins.  
  
 **Permissions that apply to servers**  
  
-   CONTROL SERVER  
  
-   ADMINISTER BULK OPERATIONS  
  
-   ALTER ANY CONNECTION  
  
-   ALTER ANY DATABASE  
  
-   CREATE ANY DATABASE  
  
-   ALTER ANY EXTERNAL DATA SOURCE  
  
-   ALTER ANY EXTERNAL FILE FORMAT  
  
-   ALTER ANY LOGIN  
  
-   ALTER SERVER STATE  
  
-   CONNECT SQL  
  
-   VIEW ANY DEFINITION  
  
-   VIEW ANY DATABASE  
  
-   VIEW SERVER STATE  
  
 **Permissions that apply to logins**  
  
-   CONTROL ON LOGIN  
  
-   ALTER ON LOGIN  
  
-   IMPERSONATE ON LOGIN  
  
-   VIEW DEFINITION  
  
### Database Level Permissions  
 Database level permissions can be granted, denied, and revoked from database users and user-defined database roles.  
  
 **Permissions that apply to all database classes**  
  
-   CONTROL  
  
-   ALTER  
  
-   VIEW DEFINITION  
  
 **Permissions that apply to all database classes except users**  
  
-   TAKE OWNERSHIP  
  
 **Permissions that apply only to databases**  
  
-   ALTER ANY DATABASE  
  
-   ALTER ON DATABASE  
  
-   ALTER ANY DATASPACE  
  
-   ALTER ANY ROLE  
  
-   ALTER ANY SCHEMA  
  
-   ALTER ANY USER  
  
-   BACKUP DATABASE  
  
-   CONNECT ON DATABASE  
  
-   CREATE PROCEDURE  
  
-   CREATE ROLE  
  
-   CREATE SCHEMA  
  
-   CREATE TABLE  
  
-   CREATE VIEW  
  
-   SHOWPLAN  
  
 **Permissions that apply only to users**  
  
-   IMPERSONATE  
  
 **Permissions that apply to databases, schemas, and objects**  
  
-   ALTER  
  
-   DELETE  
  
-   EXECUTE  
  
-   INSERT  
  
-   SELECT  
  
-   UPDATE  
  
-   REFERENCES  
  
 For a definition of each type of permission, see [Permissions (Database Engine)](https://msdn.microsoft.com/library/ms191291.aspx).  
  
### Chart of Permissions  
 All permissions are graphically represented on this poster. This is the easiest way to see nested hierarchy of permissions. For example the **ALTER ON LOGIN** permission can be granted by itself, but it is also included if a login is granted the **CONTROL** permission on that login, or if a login is granted the **ALTER ANY LOGIN** permission.  
  
 ![APS security permissions poster](../../t-sql/statements/media/aps-security-perms-poster.png "APS security permissions poster")  
  
 To download a full size version of this poster, see [SQL Server PDW Permissions](https://go.microsoft.com/fwlink/?LinkId=244249)in the files section of the APS Yammer site (or request by e-mail from **apsdoc@microsoft.com**.  
  
## Default Permissions  
 The following list describes the default permissions:  
  
-   When a login is created by using the **CREATE LOGIN** statement the new login receives the **CONNECT SQL** permission.  
  
-   All logins are members of the **public** server role and cannot be removed from **public**.  
  
-   When a database user is created by using the **CREATE USER** permission, the database user receives the **CONNECT** permission in the database.  
  
-   All principals, including the **public** role, have no explicit or implicit permissions by default.  
  
-   When a login or user becomes the owner of a database or object, the login or user always has all permissions on the database or object. The ownership permissions cannot be changed and are not visible as explicit permissions. The **GRANT**, **DENY**, and **REVOKE** statements have no effect on owners.  
  
-   The **sa** login has all permissions on the appliance. Similar to ownership permissions, the **sa** permissions cannot be changed and are not visible as explicit permissions. The **GRANT**, **DENY**, and **REVOKE** statements have no effect on **sa** login. The **sa** login cannot be renamed.  
  
-   The **USE** statement does not require permissions. All principals can run the **USE** statement on any database.  
  
##  <a name="Examples"></a> Examples: [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### A. Granting a server level permission to a login  
 The following two statements grant a server level permission to a login.  
  
```  
GRANT CONTROL SERVER TO [Ted];  
```  
  
```  
GRANT ALTER ANY DATABASE TO Mary;  
```  
  
### B. Granting a server level permission to a login  
 The following example grants a server level permission on a login to a server principal (another login).  
  
```  
GRANT  VIEW DEFINITION ON LOGIN::Ted TO Mary;  
```  
  
### C. Granting a database level permission to a user  
 The following example grants a database level permission on a user to a database principal (another user).  
  
```  
GRANT VIEW DEFINITION ON USER::[Ted] TO Mary;  
```  
  
### D. Granting, denying, and revoking a schema permission  
 The following **GRANT** statement grants Yuen the ability to select data from any table or view in the dbo schema.  
  
```  
GRANT SELECT ON SCHEMA::dbo TO [Yuen];  
```  
  
 The following **DENY** statement prevents Yuen from selecting data from any table or view in the dbo schema. Yuen cannot read the data even if he has permission in some other way, such as through a role membership.  
  
```  
DENY SELECT ON SCHEMA::dbo TO [Yuen];  
```  
  
 The following **REVOKE** statement removes the **DENY** permission. Now Yuen's explicit permissions are neutral. Yuen might be able to select data from any table through some other implicit permission such as a role membership.  
  
```  
REVOKE SELECT ON SCHEMA::dbo TO [Yuen];  
```  
  
### E. Demonstrating the optional OBJECT:: clause  
 Because OBJECT is the default class for a permission statement, the following two statements are the same. The **OBJECT::** clause is optional.  
  
```  
GRANT UPDATE ON OBJECT::dbo.StatusTable TO [Ted];  
```  
  
```  
GRANT UPDATE ON dbo.StatusTable TO [Ted];  
```  
  
  

