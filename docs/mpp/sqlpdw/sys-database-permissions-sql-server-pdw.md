---
title: "sys.database_permissions (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: c00b020f-ffe1-478a-9c6f-742a2b11fdbe
caps.latest.revision: 11
author: BarbKess
---
# sys.database_permissions (SQL Server PDW)
Returns a row for every permission or column-exception permission in the database. For columns, there is a row for every permission that is different from the corresponding object-level permission. If the column permission is the same as the corresponding object permission, there will be no row for it and the actual permission used will be that of the object.  
  
> [!IMPORTANT]  
> Column-level permissions override object-level permissions on the same entity.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**class**|**tinyint**|Identifies class on which permission exists.<br /><br />0 = Database<br /><br />1 = Object or Column<br /><br />3 = Schema<br /><br />4 = Database Principal<br /><br />5 = Assembly (Not available in PDW)<br /><br />6 = Type<br /><br />10 = XML Schema Collection (Not available in PDW) (Not available in PDW)<br /><br />15 = Message Type (Not available in PDW)<br /><br />16 = Service Contract (Not available in PDW)<br /><br />17 = Service (Not available in PDW)<br /><br />18 = Remote Service Binding (Not available in PDW)<br /><br />19 = Route (Not available in PDW)<br /><br />23 = Full-Text Catalog (Not available in PDW)<br /><br />24 = Symmetric Key (Not available in PDW)<br /><br />25 = Certificate (Not available in PDW)<br /><br />26 = Asymmetric Key (Not available in PDW)|  
|**class_desc**|**nvarchar(60)**|Description of class on which permission exists.<br /><br />DATABASE<br /><br />OBJECT_OR_COLUMN<br /><br />SCHEMA<br /><br />DATABASE_PRINCIPAL<br /><br />ASSEMBLY<br /><br />TYPE<br /><br />XML_SCHEMA_COLLECTION<br /><br />MESSAGE_TYPE<br /><br />SERVICE_CONTRACT<br /><br />SERVICE<br /><br />REMOTE_SERVICE_BINDING<br /><br />ROUTE<br /><br />FULLTEXT_CATALOG<br /><br />SYMMETRIC_KEY<br /><br />CERTIFICATE<br /><br />ASYMMETRIC_KEY|  
|**major_id**|**int**|ID of thing on which permission exists, interpreted according to class. For most, this is simply the kind of ID that applies to what the class represents. Interpretation for nonstandard is as follows:<br /><br />0 = Always 0<br /><br />1 = Object-ID<br /><br />Negative IDs are assigned to system objects.|  
|**minor_id**|**int**|Secondary-ID of thing on which permission exists, interpreted according to class. For most, this is zero. Otherwise, it is the following:<br /><br />1 = Column-ID if a column. Otherwise, it is 0 if an object.|  
|**grantee_principal_id**|**int**|Database principal ID to which the permissions are granted.|  
|**grantor_principal_id**|**int**|Database principal ID of the grantor of these permissions.|  
|**type**|**char(4)**|Database permission type. For a list of permission types, see the next table.|  
|**permission_name**|**nvarchar(128)**|Permission name.|  
|**state**|**char(1)**|Permission state:<br /><br />D = Deny<br /><br />R = Revoke<br /><br />G = Grant<br /><br />W = Grant With Grant Option|  
|**state_desc**|**nvarchar(60)**|Description of permission state:<br /><br />DENY<br /><br />REVOKE<br /><br />GRANT<br /><br />GRANT_WITH_GRANT_OPTION|  
  
|Permission type|Permission name|Applies to securable|  
|-------------------|-------------------|------------------------|  
|AL|ALTER|DATABASE, OBJECT, ROLE, SCHEMA, USER|  
|ALDS|ALTER ANY DATASPACE|DATABASE|  
|ALED|ALTER ANY DATABASE EVENT NOTIFICATION|DATABASE|  
|ALRL|ALTER ANY ROLE|DATABASE|  
|ALUS|ALTER ANY USER|DATABASE|  
|BADB|BACKUP DATABASE|DATABASE|  
|CL|CONTROL|DATABASE, OBJECT, ROLE, SCHEMA, USER|  
|CO|CONNECT|DATABASE|  
|CRPR|CREATE PROCEDURE|DATABASE|  
|CRRL|CREATE ROLE|DATABASE|  
|CRTB|CREATE TABLE|DATABASE|  
|CRVW|CREATE VIEW|DATABASE|  
|DL|DELETE|DATABASE, OBJECT, SCHEMA|  
|EX|EXECUTE|DATABASE, OBJECT, SCHEMA|  
|IM|IMPERSONATE|USER|  
|IN|INSERT|DATABASE, OBJECT, SCHEMA|  
|RC|RECEIVE|OBJECT|  
|SL|SELECT|DATABASE, OBJECT, SCHEMA|  
|SPLN|SHOWPLAN|DATABASE|  
|TO|TAKE OWNERSHIP|DATABASE, OBJECT, ROLE, SCHEMA|  
|UP|UPDATE|DATABASE, OBJECT, SCHEMA|  
|VW|VIEW DEFINITION|DATABASE, OBJECT, ROLE, SCHEMA, USER|  
  
## Permissions  
Any user can see his/her own permissions. To see permissions for other users requires VIEW DEFINITION, ALTER ANY USER, or any permission on a user. To see user-defined roles requires ALTER ANY ROLE, or membership in the role (such as public).  
  
## Examples  
  
### A: Listing all the permissions of database principals  
The following query lists the permissions explicitly granted or denied to database principals.  
  
> [!IMPORTANT]  
> The permissions of fixed database roles do not appear in sys.database_permissions. Therefore, database principals may have additional permissions not listed here.  
  
```  
SELECT pr.principal_id, pr.name, pr.type_desc,   
    pr.authentication_type_desc, pe.state_desc, pe.permission_name  
FROM sys.database_principals AS pr  
JOIN sys.database_permissions AS pe  
    ON pe.grantee_principal_id = pr.principal_id;  
```  
  
### B: Listing permissions on schema objects within a database  
The following query joins sys.database_principals and sys.database_permissions to sys.objects and sys.schemas to list permissions granted or denied to specific schema objects.  
  
```  
SELECT pr.principal_id, pr.name, pr.type_desc,   
    pr.authentication_type_desc, pe.state_desc,   
    pe.permission_name, s.name + '.' + o.name AS ObjectName  
FROM sys.database_principals AS pr  
JOIN sys.database_permissions AS pe  
    ON pe.grantee_principal_id = pr.principal_id  
JOIN sys.objects AS o  
    ON pe.major_id = o.object_id  
JOIN sys.schemas AS s  
    ON o.schema_id = s.schema_id;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[sys.database_principals &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-database-principals-sql-server-pdw.md)  
[sys.server_permissions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-server-permissions-sql-server-pdw.md)  
[sys.server_principals &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-server-principals-sql-server-pdw.md)  
  
