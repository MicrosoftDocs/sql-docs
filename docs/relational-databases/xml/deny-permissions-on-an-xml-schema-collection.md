---
title: "Deny Permissions on an XML Schema Collection | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "denying permissions [SQL Server], XML server collections"
ms.assetid: e2b300b0-e734-4c43-a4da-c78e6e5d4fba
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Deny Permissions on an XML Schema Collection
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  Permission can be denied to either create a new XML schema collection or use an existing one.  
  
## Denying Permission to Create an XML Schema Collection  
 You can deny permission to create an XML schema collection in the following ways:  
  
-   Deny ALTER permission on the relational schema.  
  
-   Deny CONTROL on the relational schema to deny all permissions on the relational schema and on all the contained objects.  
  
-   Deny ALTER ANY SCHEMA on the database. In this case, the principal cannot create an XML schema collection anywhere in the database. Note also that denying ALTER or CONTROL permission on the database denies all permissions on all objects in the database.  
  
## Denying Permissions on an XML Schema Collection Object  
 Following are the permission that can be denied on an existing XML schema collection and the results:  
  
-   Denying the ALTER permission denies the principal the ability to modify the contents of the XML schema collection.  
  
-   Denying the CONTROL permission denies the principal the ability to perform any operation on the XML schema collection.  
  
-   Denying the REFERENCES permission denies the principal the ability to type or constrain xml type columns and parameters using the XML schema collection. It also denies the principal the ability to refer to this XML schema collection from other XML schema collections.  
  
-   Denying the VIEW DEFINITION permission denies the principal the ability to view the contents of an XML schema collection.  
  
-   Denying the EXECUTE permission denies the principal the ability to insert or update the values in columns, variables, and parameters that are typed or constrained by the XML schema collection. It also denies the principal the ability to query the values in those same xml type columns and variables.  
  
## Examples  
 The scenarios in the following examples show how XML schema permissions work. Each example creates the necessary test database, relational schemas, and logins. These logins are granted the necessary XML schema collection permissions. Each example does the necessary cleanup at the end.  
  
### A. Preventing a user from creating an XML schema collection  
 One of the ways to prevent a user from creating an XML schema collection is by denying the ALTER permission on a relational schema. This is shown in the following example.  
  
 The example creates a user, `TestLogin1`, and a database. It also creates a relational schema, in addition to the `dbo` schema, in the database. Initially, the `CREATE XML SCHEMA` permission allows the user to create a schema collection anywhere in the database. The example then denies `ALTER` permission to the user on one of the relational schemas. This prevents the user from creating an XML schema collection in that relational schema.  
  
```  
CREATE LOGIN TestLogin1 WITH password='SQLSvrPwd1'  
GO  
CREATE DATABASE SampleDBForSchemaPermissions  
GO  
USE SampleDBForSchemaPermissions  
GO  
-- Create another relational schema in the database.  
CREATE SCHEMA myOtherDBSchema  
GO  
CREATE USER TestLogin1  
GO  
-- For TestLogin1 to create/import XML schema collection, following  
-- permission needed.  
-- Database-level permissions  
GRANT CREATE XML SCHEMA COLLECTION TO TestLogin1  
GO  
GRANT ALTER ANY SCHEMA TO TestLogin1  
GO  
-- Now TestLogin1 can import an XML schema collection.  
SETUSER 'TestLogin1'  
GO  
CREATE XML SCHEMA COLLECTION myOtherDBSchema.myTestSchemaCollection AS '<?xml version="1.0" encoding="UTF-8" ?>  
<xsd:schema targetNamespace="https://schemas.adventure-works.com/Additional/ContactInfo"   
            xmlns:xsd="http://www.w3.org/2001/XMLSchema"   
elementFormDefault="qualified">  
<xsd:element name="telephone" type="xsd:string" />  
</xsd:schema>'  
GO  
DROP XML SCHEMA COLLECTION myOtherDBSchema.myTestSchemaCollection  
GO  
-- Now deny permission from TestLogin1 to alter myOtherDBSchema.  
setuser  
GO  
DENY ALTER ON SCHEMA::myOtherDBSchema TO TestLogin1  
GO  
-- Now TestLogin1 cannot create xml schema collection.  
SETUSER 'TestLogin1'  
GO  
CREATE XML SCHEMA COLLECTION myOtherDBSchema.myTestSchemaCollection AS '<?xml version="1.0" encoding="UTF-8" ?>  
<xsd:schema targetNamespace="https://schemas.adventure-works.com/Additional/ContactInfo"   
            xmlns:xsd="http://www.w3.org/2001/XMLSchema"   
elementFormDefault="qualified">  
<xsd:element name="telephone" type="xsd:string" />  
</xsd:schema>'  
GO  
-- Final cleanup  
SETUSER  
GO  
USE master  
GO  
DROP DATABASE SampleDBForSchemaPermissions  
GO  
DROP LOGIN TestLogin1  
GO  
```  
  
### B. Denying permissions on an XML schema collection  
 The following example shows how a specific permission on an existing XML schema collection can be denied to a login. In this example, a test login is denied REFERENCES permission on an existing XML schema collection.  
  
 The example creates a user, `TestLogin1`, and a database. It also creates a relational schema, in addition to the `dbo` schema, in the database. Initially, the `CREATE XML SCHEMA` permission allows the user to create a schema collection anywhere in the database.  
  
 The `REFERENCES` permission on the XML schema collection lets `TestLogin1` use the schema in creating a typed `xml` column in a table. If the `REFERENCES` permission on the XML schema collection is denied, it prevents the `TestLogin1` from using the XML schema collection.  
  
```  
CREATE LOGIN TestLogin1 WITH password='SQLSvrPwd1'  
GO  
CREATE DATABASE SampleDBForSchemaPermissions  
GO  
USE SampleDBForSchemaPermissions  
GO  
-- Create another relational schema in the database.  
CREATE SCHEMA myOtherDBSchema  
GO  
CREATE USER TestLogin1  
GO  
-- For TestLogin1 to create/import XML schema collection, the following  
-- permission is required.  
-- Database-level permissions  
GRANT CREATE XML SCHEMA COLLECTION TO TestLogin1  
GO  
GRANT ALTER ANY SCHEMA TO TestLogin1  
GO  
-- Now TestLogin1 can import an XML schema collection.  
SETUSER 'TestLogin1'  
GO  
CREATE XML SCHEMA COLLECTION myOtherDBSchema.myTestSchemaCollection AS '<?xml version="1.0" encoding="UTF-8" ?>  
<xsd:schema targetNamespace="https://schemas.adventure-works.com/Additional/ContactInfo"   
            xmlns:xsd="http://www.w3.org/2001/XMLSchema"   
elementFormDefault="qualified">  
<xsd:element name="telephone" type="xsd:string" />  
</xsd:schema>'  
GO  
-- Grant permission to TestLogin1 to create a table and reference the XML schema collection.  
SETUSER  
GO  
GRANT CREATE TABLE TO TestLogin1  
GO  
-- The user also needs REFERENCES permission to use the XML schema collection  
-- to create a typed XML column (REFERENCES permission on the schema   
-- collection is not needed).  
GRANT REFERENCES ON XML SCHEMA COLLECTION::myOtherDBSchema.myTestSchemaCollection   
TO TestLogin1  
GO  
  
--TestLogin1 can use the schema.  
CREATE TABLE T(i int, x xml (myOtherDBSchema.myTestSchemaCollection))  
GO  
-- Drop the table.  
DROP TABLE T  
GO  
-- Now deny REFERENCES permission to TestLogin1 on the schema created previously.  
SETUSER  
GO  
DENY REFERENCES ON XML SCHEMA COLLECTION::myOtherDBSchema.myTestSchemaCollection TO TestLogin1  
  
GO  
-- Now TestLogin1 cannot create xml schema collection  
SETUSER 'TestLogin1'  
GO  
-- Following statement fails. TestLogin1 does not have REFERENCES   
-- permission on the XML schema collection.  
CREATE TABLE T(i int, x xml (myOtherDBSchema.myTestSchemaCollection))  
GO  
  
-- Final cleanup  
SETUSER  
GO  
USE master  
GO  
DROP DATABASE SampleDBForSchemaPermissions  
GO  
DROP LOGIN TestLogin1  
GO  
```  
  
## See Also  
 [Compare Typed XML to Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md)   
 [XML Schema Collections &#40;SQL Server&#41;](../../relational-databases/xml/xml-schema-collections-sql-server.md)   
 [Requirements and Limitations for XML Schema Collections on the Server](../../relational-databases/xml/requirements-and-limitations-for-xml-schema-collections-on-the-server.md)   
 [DENY Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-object-permissions-transact-sql.md)   
 [GRANT Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-object-permissions-transact-sql.md)   
 [XML Data &#40;SQL Server&#41;](../../relational-databases/xml/xml-data-sql-server.md)  
  
  
