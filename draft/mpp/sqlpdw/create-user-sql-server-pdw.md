---
title: "CREATE USER (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 3e38a06e-8f1a-4383-8a6a-d2f8cfdd861f
caps.latest.revision: 13
author: BarbKess
---
# CREATE USER (SQL Server PDW)
Adds a user to the current SQL Server PDW database.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
CREATE USER user_name   
    [ { { FOR | FROM }  
      {   
        LOGIN login_name   
      }   
      | WITHOUT LOGIN  
    ]   
    [ WITH DEFAULT_SCHEMA = schema_name ]  
[;]  
```  
  
## Arguments  
*user_name*  
Specifies the name by which the user is identified inside this database. *user_name* can be up to 128 characters long. When creating a user based on a Windows principal, the Windows principal name becomes the user name unless another user name is specified.  
  
LOGIN *login_name*  
Specifies the login for which the database user is being created. *login_name* must be a valid login in the server. Can be a login based on a Windows principal (user or group), or a login created by using SQL Server authentication. When this login enters the database it will acquire the name and ID of the database user that is being created. When creating a login mapped from a Windows principal, use the format **[***<domainName>***\\***<loginName>***]**.  
  
WITHOUT LOGIN  
Specifies that the user should not be mapped to an existing login.  
  
WITH DEFAULT_SCHEMA = *schema_name*  
Specifies the first schema that will be searched by the server when it resolves the names of objects for this database user.  
  
## Remarks  
If FOR LOGIN is omitted, the new database user will be mapped to the SQL Server PDW login with the same name.  
  
The default schema will be the first schema that will be searched by the server when it resolves the names of objects for this database user. Unless otherwise specified, the default schema will be the owner of objects created by this database user.  
  
If the user has a default schema, that default schema will used. If the user does not have a default schema, but the user is a member of a group that has a default schema, the default schema of the group will be used. If the user does not have a default schema, and is a member of more than one group, the default schema for the user will be that of the Windows group with the lowest principal_id and an explicitly set default schema. (It is not possible to explicitly select one of the available default schemas as the preferred schema.) If no default schema can be determined for a user, the **dbo** schema will be used.  
  
DEFAULT_SCHEMA can be set before the schema that it points to is created.  
  
The value of DEFAULT_SCHEMA is ignored if the user is a member of the sysadmin fixed server role. All members of the sysadmin fixed server role have a default schema of `dbo`.  
  
The WITHOUT LOGIN clause creates a user that is not mapped to a SQL Server PDW login. It can connect to other databases as guest.  
  
The names of users that are mapped to SQL Server PDW logins cannot contain the backslash character (\\).  
  
CREATE USER cannot be used to create a guest user because the guest user already exists inside every database. You can enable the guest user by granting it CONNECT permission, as shown:  
  
```  
GRANT CONNECT TO guest;  
GO  
```  
  
Information about database users is visible in the [sys.database_principals &#40;SQL Server PDW&#41;](../sqlpdw/sys-database-principals-sql-server-pdw.md) catalog view.  
  
## Permissions  
Requires ALTER ANY USER permission on the database.  
  
## Locking  
Takes an exclusive lock on the SCHEMARESOLUTION object. If the default schema clause is specified, takes an exclusive lock on the DATABASE object. If the default schema clause is not specified, takes a shared lock on the DATABASE object.  
  
## Examples  
  
### A. Creating a database user  
The following example first creates a server login named `AbolrousHazem` with a password, and then creates a corresponding database user `AbolrousHazem` in `AdventureWorks2008R2`.  
  
```  
CREATE LOGIN AbolrousHazem   
    WITH PASSWORD = '340$Uuxwp7Mcxo7Khy';  
USE AdventureWorks2008R2;  
CREATE USER AbolrousHazem FOR LOGIN AbolrousHazem;  
GO  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[ALTER USER &#40;SQL Server PDW&#41;](../sqlpdw/alter-user-sql-server-pdw.md)  
[DROP USER &#40;SQL Server PDW&#41;](../sqlpdw/drop-user-sql-server-pdw.md)  
[CREATE LOGIN &#40;SQL Server PDW&#41;](../sqlpdw/create-login-sql-server-pdw.md)  
  
