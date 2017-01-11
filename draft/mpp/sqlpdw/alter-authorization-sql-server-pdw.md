---
title: "ALTER AUTHORIZATION (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 1ee55c78-2826-4ad4-8536-9d9f41c23686
caps.latest.revision: 31
author: BarbKess
---
# ALTER AUTHORIZATION (SQL Server PDW)
Changes the owner of a database, schema, owner or table owner to a different SQL Server PDW security principal. Use this statement when managing security on databases and tables.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ALTER AUTHORIZATION ON  
    [ <class_type> :: ] <entity_name>   
    TO { principal_name | SCHEMA OWNER }  
[;]  
  
<class_type> ::= {  
    DATABASE   
    | SCHEMA   
    | OBJECT   
}  
  
<entity_name> ::=  
{  
    database_name    | schema_name  
    | [ schema_name. ] object_name  
}  
```  
  
## Arguments  
<class_type>  
The class of the securable that is changing ownership. The class is either DATABASE, SCHEMA, or OBJECT. The default is OBJECT. Using OBJECT is optional since it is the default. If the class type is DATABASE or SCHEMA, it must be specified. The object can be a table, view, or procedure.  
  
<entity_name>  
The name of the entity that will have the ownership change. The entity is either a database or a table.  
  
*database_name*  
The name of the database that will have the ownership change.  
  
[ schema_name. ] *object_name*  
The one or two-part name for the table, view, or procedure that will have the ownership change.  
  
*principal_name* | SCHEMA OWNER  
Name of the security principal that will own the entity. Database objects must be owned by a database principal; a database user or role. Server objects (such as databases) must be owned by a server principal (a login). Specify **SCHEMA OWNER** as the *principal_name* to indicate that the object should be owned by the principal that owns the schema of the object.  
  
## Permissions  
Requires **TAKE OWNERSHIP** permission on the entity. If the new owner is not the user that is executing this statement, also requires either, 1) **IMPERSONATE** permission on the new owner if it is a user or login; or 2) if the new owner is a role, membership in the role, or **ALTER** permission on the role;  
  
## Examples  
  
### A. Changing the owner of a table  
Each of the following examples changes the owner of the `Sprockets` table in the `Parts` database to the database user `MichikoOsada`.  
  
```  
ALTER AUTHORIZATION ON Sprockets TO MichikoOsada;  
ALTER AUTHORIZATION ON dbo.Sprockets TO MichikoOsada;  
ALTER AUTHORIZATION ON OBJECT::Sprockets TO MichikoOsada;  
ALTER AUTHORIZATION ON OBJECT::dbo.Sprockets TO MichikoOsada;  
```  
  
### B. Changing the owner of a database  
The following example change the owner of the `Parts` database to the login `MichikoOsada`.  
  
```  
ALTER AUTHORIZATION ON DATABASE::Parts TO MichikoOsada;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Permissions: GRANT, DENY, REVOKE &#40;SQL Server PDW&#41;](../sqlpdw/permissions-grant-deny-revoke-sql-server-pdw.md)  
  
