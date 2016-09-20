---
title: "ALTER USER (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 9a57a6cf-8e1e-4f26-bf39-bd8866b90589
caps.latest.revision: 15
author: BarbKess
---
# ALTER USER (SQL Server PDW)
Renames a database user or changes its default schema in SQL Server PDW.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ALTER USER userName    
     WITH <set_item> [ ,...n ]  
  
<set_item> ::=   
     NAME =newUserName   
     | LOGIN =loginName  
     | DEFAULT_SCHEMA = schema_name  
[;]  
```  
  
## Arguments  
*userName*  
Specifies the name by which the user is identified inside this database.  
  
NAME =*newUserName*  
Specifies the new name for this user. *newUserName* must not already occur in the current database.  
  
LOGIN =*loginName*  
Re-maps a user to another login by changing the user's Security Identifier (SID) to match the login's SID.  
  
DEFAULT_SCHEMA = *schema_name*  
Specifies the first schema that will be searched by the server when it resolves the names of objects for this database user.  
  
## Remarks  
The WITH LOGIN clause enables the remapping of a user to a different login.  
  
The name of the user will be automatically renamed to the login name if the following conditions are true.  
  
-   No new name was specified.  
  
-   The current name differs from the login name.  
  
Otherwise, the user will not be renamed unless the caller additionally invokes the NAME clause.  
  
The name of a user mapped to a SQL Server login cannot contain the backslash character (\\).  
  
> [!NOTE]  
> In SQL Server a default schema can be explicitly set to NULL for a database user that belongs to a Windows group. This is not possible in PDW. To work around this issue, either drop and recreate the user without specifying a default schema (preferred), or set the default schema of the user to a schema that does not exist.  
  
## Permissions  
To change the name of a user requires the **ALTER ANY USER** permission.  
  
To change the user name, target login, or SID of a user having **CONTROL** permission on the database requires the **CONTROL** permission on the database.  
  
To change the default schema requires **ALTER** permission on the user. All users can change their own default schema.  
  
## Locking  
Takes an exclusive lock on the SCHEMARESOLUTION object. If the default schema clause is specified, takes an exclusive lock on the DATABASE object. If the default schema clause is not specified, takes a shared lock on the DATABASE object.  
  
## Examples  
  
### A. Changing the name of a database user  
The following example changes the name of the database user `Mary5` to `Mary51`.  
  
```  
USE AdventureWorks2008R2;  
ALTER USER Mary5 WITH NAME = Mary51;  
GO  
```  
  
### B. Changing the default schema  
The following example changes the default schema of the database user `Contoso\Mary` to `Sales`.  
  
```  
ALTER USER [Contoso\Mary] WITH DEFAULT_SCHEMA = Sales;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CREATE USER &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-user-sql-server-pdw.md)  
[DROP USER &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/drop-user-sql-server-pdw.md)  
[sys.database_principals &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-database-principals-sql-server-pdw.md)  
  
