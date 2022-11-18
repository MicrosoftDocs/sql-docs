---
title: "Revoke Permissions on an XML Schema Collection"
description: Learn how to revoke permissions on an XML schema collection.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "revoking permissions [SQL Server]"
author: MikeRayMSFT
ms.author: mikeray
---
# Revoke permissions on an XML schema collection

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The permission to create an XML schema collection can be revoked by using one of the following:

- Revoke the ALTER permission for the relational schema. Then, the principal can't create an XML schema collection in the relational schema. However, the principal can still do so in other relational schemas in the same database.

- Revoke the ALTER ANY SCHEMA permission on the database for the principal. Then, the principal can't create an XML schema collection anywhere in the database.

- Revoke the CREATE XML SCHEMA COLLECTION or ALTER XML SCHEMA COLLECTION permission on the database for the principal. This prevents the principal from importing an XML schema collection within the database. Revoking the ALTER or CONTROL permission on the database has the same effect.

## Revoke permissions on an existing XML schema collection object

Following are the permissions that can be revoked on an XML schema collection and the results:

- Revoking the ALTER permission revokes a principal's ability to modify the content of the XML schema collection.

- Revoking the TAKE OWNERSHIP permission revokes a principal's ability to transfer ownership of the XML schema collection.

- Revoking the REFERENCES permission revokes a principal's ability to use the XML schema collection for typing or constraining xml type columns, in tables and views, and parameters. It also revokes the permission to refer to this schema collection from other XML schema collections.

- Revoking the VIEW DEFINITION permission revokes a principal's ability to view the contents of an XML schema collection.

- Revoking the EXECUTE permission revokes a principal's ability to insert or update values in columns, variables, and parameters that are typed or constrained by the XML collection. It also revokes the ability to query such **xml** type columns, variables, or parameters.

## Examples

The scenarios in the following examples illustrate how XML schema permissions work. Each example creates the necessary test database, relational schemas, and logins. These logins are granted the necessary XML schema collection permissions. Each example does the necessary cleanup at the end.

### A. Revoke permissions to create an XML schema collection

This example creates a login and a sample database. It also adds a relational schema in the database. Initially, the login is granted ALTER permission on both relational schemas and other necessary permissions to create XML schema collections. The example then revokes the ALTER permission on one of the relational schemas in the database. This prevents the login from creating an XML schema collection.

```sql
SETUSER;
GO
CREATE LOGIN TestLogin1 with password='SQLSvrPwd1';
GO
CREATE DATABASE SampleDBForSchemaPermissions;
GO
use SampleDBForSchemaPermissions;
GO
-- Create another relational schema in the db (in addition to dbo schema)
CREATE SCHEMA myOtherDBSchema;
GO
CREATE USER TestLogin1;
GO
-- For TestLogin1 to create/import XML schema collection, following
-- permission needed
-- CREATE XML SCHEMA is a database level permission
GRANT CREATE XML SCHEMA COLLECTION TO TestLogin1;
GO
GRANT ALTER ON SCHEMA::myOtherDBSchema TO TestLogin1;
GO
GRANT ALTER ON SCHEMA::dbo TO TestLogin1;
GO
-- Now TestLogin1 can import an XML schema collection in both relational schemas.
SETUSER 'TestLogin1';
GO
CREATE XML SCHEMA COLLECTION dbo.myTestSchemaCollection AS '<?xml version="1.0" encoding="UTF-8" ?>
<xsd:schema targetNamespace="https://schemas.adventure-works.com/Additional/ContactInfo"
            xmlns:xsd="http://www.w3.org/2001/XMLSchema"
elementFormDefault="qualified">
<xsd:element name="telephone" type="xsd:string" />
</xsd:schema>';
GO
-- TestLogin1 can create XML schema collection in myOtherDBSchema relational schema
CREATE XML SCHEMA COLLECTION myOtherDBSchema.myTestSchemaCollection AS '<?xml version="1.0" encoding="UTF-8" ?>
<xsd:schema targetNamespace="https://schemas.adventure-works.com/Additional/ContactInfo"
            xmlns:xsd="http://www.w3.org/2001/XMLSchema"
elementFormDefault="qualified">
<xsd:element name="telephone" type="xsd:string" />
</xsd:schema>';
GO
-- Let us drop XML schema collections from both relational schemas
DROP XML SCHEMA COLLECTION myOtherDBSchema.myTestSchemaCollection;
GO
DROP XML SCHEMA COLLECTION dbo.myTestSchemaCollection;
GO
-- now REVOKE permission from TestLogin1 to alter myOtherDBSchema
SETUSER;
GO
REVOKE ALTER ON SCHEMA::myOtherDBSchema FROM TestLogin1;
GO
-- now TestLogin1 cannot create xml schema collection in myOtherDBSchema
SETUSER 'TestLogin1';
GO
CREATE XML SCHEMA COLLECTION myOtherDBSchema.myTestSchemaCollection AS '<?xml version="1.0" encoding="UTF-8" ?>
<xsd:schema targetNamespace="https://schemas.adventure-works.com/Additional/ContactInfo"
            xmlns:xsd="http://www.w3.org/2001/XMLSchema"
elementFormDefault="qualified">
<xsd:element name="telephone" type="xsd:string" />
</xsd:schema>';
GO

-- TestLogin1 can still create XML schema collections in dbo
-- It cannot create XML schema collections anywhere in the database
-- if we REVOKE CREATE XML SCHEMA COLLECTION permission
SETUSER;
GO
REVOKE CREATE XML SCHEMA COLLECTION FROM TestLogin1;
GO

SETUSER 'TestLogin1';
GO
-- the following now should fail
CREATE XML SCHEMA COLLECTION dbo.myTestSchemaCollection AS '<?xml version="1.0" encoding="UTF-8" ?>
<xsd:schema targetNamespace="https://schemas.adventure-works.com/Additional/ContactInfo"
            xmlns:xsd="http://www.w3.org/2001/XMLSchema"
elementFormDefault="qualified">
<xsd:element name="telephone" type="xsd:string" />
</xsd:schema>';
GO

-- Final cleanup
SETUSER;
GO
USE master;
GO
DROP DATABASE SampleDBForSchemaPermissions;
GO
DROP LOGIN TestLogin1;
GO
```

## See also

- [XML Data &#40;SQL Server&#41;](../../relational-databases/xml/xml-data-sql-server.md)
- [Compare Typed XML to Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md)
- [XML Schema Collections &#40;SQL Server&#41;](../../relational-databases/xml/xml-schema-collections-sql-server.md)
- [Requirements and Limitations for XML Schema Collections on the Server](../../relational-databases/xml/requirements-and-limitations-for-xml-schema-collections-on-the-server.md)
