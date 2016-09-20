---
title: "ALTER SCHEMA (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: be6d3f12-898e-4cc0-af44-8d6b5c32c1e6
caps.latest.revision: 10
author: BarbKess
---
# ALTER SCHEMA (SQL Server PDW)
Transfers a securable between schemas.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ALTER SCHEMA schema_name   
   TRANSFER [ OBJECT :: ] securable_name   
[;]  
```  
  
## Arguments  
*schema_name*  
Is the name of a schema in the current database, into which the securable will be moved. Cannot be SYS or INFORMATION_SCHEMA.  
  
OBJECT  
Is the class of the entity for which the owner is being changed. Object is the only class supported in PDW. Tables, views, and procedures are all in the object class.  
  
*securable_name*  
Is the one-part or two-part name of a schema-contained securable to be moved into the schema.  
  
## Remarks  
Users and schemas are completely separate.  
  
ALTER SCHEMA can only be used to move securables between schemas in the same database. To change or drop a securable within a schema, use the ALTER or DROP statement specific to that securable.  
  
If a one-part name is used for *securable_name*, the name-resolution rules currently in effect will be used to locate the securable.  
  
All permissions associated with the securable will be dropped when the securable is moved to the new schema. If the owner of the securable has been explicitly set, the owner will remain unchanged. If the owner of the securable has been set to SCHEMA OWNER, the owner will remain SCHEMA OWNER; however, after the move SCHEMA OWNER will resolve to the owner of the new schema. The principal_id of the new owner will be NULL.  
  
## Permissions  
To transfer a securable from another schema, the current user must have CONTROL permission on the securable (not schema) and ALTER permission on the target schema.  
  
If the securable has an EXECUTE AS OWNER specification on it and the owner is set to SCHEMA OWNER, the user must also have IMPERSONATION permission on the owner of the target schema.  
  
All permissions associated with the securable that is being transferred are dropped when it is moved.  
  
## Locking  
Takes an exclusive lock on the SCHEMA. Takes a shared lock on the DATABASE object.  
  
## Examples  
  
### A. Transferring ownership of a table  
The following example creates a table `Region` in the `dbo` schema, creates a `Sales` schema, and then moves the `Region` table from the `dbo` schema to the `Sales` schema.  
  
```  
CREATE TABLE dbo.Region   
    (Region_id int NOT NULL,  
    Region_Name char(5) NOT NULL)  
WITH (DISTRIBUTION = REPLICATE);  
GO  
  
CREATE SCHEMA Sales;  
GO  
  
ALTER SCHEMA Sales TRANSFER OBJECT::dbo.Region;  
GO  
```  
  
## See Also  
[CREATE SCHEMA &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-schema-sql-server-pdw.md)  
[DROP SCHEMA &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/drop-schema-sql-server-pdw.md)  
[sys.schemas &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-schemas-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
