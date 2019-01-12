---
title: "CREATE XML SCHEMA COLLECTION (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/25/2015"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CREATE XML SCHEMA COLLECTION"
  - "CREATE_XML_SCHEMA_COLLECTION_TSQL"
  - "CREATE XML SCHEMA"
  - "CREATE_XML_SCHEMA_TSQL"
  - "COLLECTION"
  - "COLLECTION_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CREATE XML SCHEMA COLLECTION statement"
  - "importing schema components"
  - "schema collections [SQL Server], creating"
  - "multiple schema namespaces"
  - "XML schema collections [SQL Server], creating"
ms.assetid: 350684e8-b3f6-4b58-9dbc-0f05cc776ebb
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# CREATE XML SCHEMA COLLECTION (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Imports the schema components into a database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
CREATE XML SCHEMA COLLECTION [ <relational_schema>. ]sql_identifier AS Expression  
```  
  
## Arguments  
 *relational_schema*  
 Identifies the relational schema name. If not specified, default relational schema is assumed.  
  
 *sql_identifier*  
 Is the SQL identifier for the XML schema collection.  
  
 *Expression*  
 Is a string constant or scalar variable. Is **varchar**, **varbinary**, **nvarchar**, or **xml** type.  
  
## Remarks  
 You can also add new namespaces to the collection or add new components to existing namespaces in the collection by using [ALTER XML SCHEMA COLLECTION](../../t-sql/statements/alter-xml-schema-collection-transact-sql.md).  
  
 To remove collections, use [DROP XML SCHEMA COLLECTION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-xml-schema-collection-transact-sql.md).  
  
## Permissions  
 To create an XML SCHEMA COLLECTION requires at least one of the following sets of permissions:  
  
-   CONTROL permission on the server  
  
-   ALTER ANY DATABASE permission on the server  
  
-   ALTER permission on the database  
  
-   CONTROL permission in the database  
  
-   ALTER ANY SCHEMA permission and CREATE XML SCHEMA COLLECTION permission in the database  
  
-   ALTER or CONTROL permission on the relational schema and CREATE XML SCHEMA COLLECTION permission in the database  
  
## Examples  
  
### A. Creating XML schema collection in the database  
 The following example creates the XML schema collection `ManuInstructionsSchemaCollection`. The collection has only one schema namespace.  
  
```  
-- Create a sample database in which to load the XML schema collection.  
CREATE DATABASE SampleDB;  
GO  
USE SampleDB;  
GO  
CREATE XML SCHEMA COLLECTION ManuInstructionsSchemaCollection AS  
N'<?xml version="1.0" encoding="UTF-16"?>  
<xsd:schema targetNamespace="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions"   
   xmlns          ="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions"   
   elementFormDefault="qualified"   
   attributeFormDefault="unqualified"  
   xmlns:xsd="http://www.w3.org/2001/XMLSchema" >  
  
    <xsd:complexType name="StepType" mixed="true" >  
        <xsd:choice  minOccurs="0" maxOccurs="unbounded" >   
            <xsd:element name="tool" type="xsd:string" />  
            <xsd:element name="material" type="xsd:string" />  
            <xsd:element name="blueprint" type="xsd:string" />  
            <xsd:element name="specs" type="xsd:string" />  
            <xsd:element name="diag" type="xsd:string" />  
        </xsd:choice>   
    </xsd:complexType>  
  
    <xsd:element  name="root">  
        <xsd:complexType mixed="true">  
            <xsd:sequence>  
                <xsd:element name="Location" minOccurs="1" maxOccurs="unbounded">  
                    <xsd:complexType mixed="true">  
                        <xsd:sequence>  
                            <xsd:element name="step" type="StepType" minOccurs="1" maxOccurs="unbounded" />  
                        </xsd:sequence>  
                        <xsd:attribute name="LocationID" type="xsd:integer" use="required"/>  
                        <xsd:attribute name="SetupHours" type="xsd:decimal" use="optional"/>  
                        <xsd:attribute name="MachineHours" type="xsd:decimal" use="optional"/>  
                        <xsd:attribute name="LaborHours" type="xsd:decimal" use="optional"/>  
                        <xsd:attribute name="LotSize" type="xsd:decimal" use="optional"/>  
                    </xsd:complexType>  
                </xsd:element>  
            </xsd:sequence>  
        </xsd:complexType>  
    </xsd:element>  
</xsd:schema>' ;  
GO  
-- Verify - list of collections in the database.  
SELECT *  
FROM sys.xml_schema_collections;  
-- Verify - list of namespaces in the database.  
SELECT name  
FROM sys.xml_schema_namespaces;  
  
-- Use it. Create a typed xml variable. Note collection name specified.  
DECLARE @x xml (ManuInstructionsSchemaCollection);  
GO  
--Or create a typed xml column.  
CREATE TABLE T (  
        i int primary key,   
        x xml (ManuInstructionsSchemaCollection));  
GO  
-- Clean up  
DROP TABLE T;  
GO  
DROP XML SCHEMA COLLECTION ManuInstructionsSchemaCollection;  
Go  
USE master;  
GO  
DROP DATABASE SampleDB;  
```  
  
 Alternatively, you can assign the schema collection to a variable and specify the variable in the `CREATE XML SCHEMA COLLECTION` statement as follows:  
  
```  
DECLARE @MySchemaCollection nvarchar(max)  
Set @MySchemaCollection  = N' copy the schema collection here'  
CREATE XML SCHEMA COLLECTION MyCollection AS @MySchemaCollection   
```  
  
 The variable in the example is of `nvarchar(max)` type. The variable can also be of **xml** data type, in which case, it is implicitly converted to a string.  
  
 For more information, see [View a Stored XML Schema Collection](../../relational-databases/xml/view-a-stored-xml-schema-collection.md).  
  
 You may store schema collections in an **xml** type column. In this case, to create XML schema collection, perform the following:  
  
1.  Retrieve the schema collection from the column by using a SELECT statement and assign it to a variable of **xml** type, or a **varchar** type.  
  
2.  Specify the variable name in the CREATE XML SCHEMA COLLECTION statement.  
  
 The CREATE XML SCHEMA COLLECTION stores only the schema components that SQL Server understands; everything in the XML schema is not stored in the database. Therefore, if you want the XML schema collection back exactly the way it was supplied, we recommend that you save your XML schemas in a database column or some other folder on your computer.  
  
### B. Specifying multiple schema namespaces in a schema collection  
 You can specify multiple XML schemas when you create an XML schema collection. For example:  
  
```  
CREATE XML SCHEMA COLLECTION MyCollection AS N'  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema">  
<!-- Contents of schema here -->    
</xsd:schema>  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema">  
<!-- Contents of schema here -->  
</xsd:schema>';  
```  
  
 The following example creates the XML schema collection `ProductDescriptionSchemaCollection` that includes two XML schema namespaces.  
  
```  
CREATE XML SCHEMA COLLECTION ProductDescriptionSchemaCollection AS   
'<xsd:schema targetNamespace="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain"  
    xmlns="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain"   
    elementFormDefault="qualified"   
    xmlns:xsd="http://www.w3.org/2001/XMLSchema" >  
    <xsd:element name="Warranty"  >  
        <xsd:complexType>  
            <xsd:sequence>  
                <xsd:element name="WarrantyPeriod" type="xsd:string"  />  
                <xsd:element name="Description" type="xsd:string"  />  
            </xsd:sequence>  
        </xsd:complexType>  
    </xsd:element>  
</xsd:schema>  
 <xs:schema targetNamespace="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription"   
    xmlns="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription"   
    elementFormDefault="qualified"   
    xmlns:mstns="https://tempuri.org/XMLSchema.xsd"   
    xmlns:xs="http://www.w3.org/2001/XMLSchema"  
    xmlns:wm="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain" >  
    <xs:import   
namespace="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain" />  
    <xs:element name="ProductDescription" type="ProductDescription" />  
        <xs:complexType name="ProductDescription">  
            <xs:sequence>  
                <xs:element name="Summary" type="Summary" minOccurs="0" />  
            </xs:sequence>  
            <xs:attribute name="ProductModelID" type="xs:string" />  
            <xs:attribute name="ProductModelName" type="xs:string" />  
        </xs:complexType>  
        <xs:complexType name="Summary" mixed="true" >  
            <xs:sequence>  
                <xs:any processContents="skip" namespace="http://www.w3.org/1999/xhtml" minOccurs="0" maxOccurs="unbounded" />  
            </xs:sequence>  
        </xs:complexType>  
</xs:schema>'  
;  
GO -- Clean up  
DROP XML SCHEMA COLLECTION ProductDescriptionSchemaCollection;  
GO  
```  
  
### C. Importing a schema that does not specify a target namespace  
 If a schema that does not contain a **targetNamespace** attribute is imported in a collection, its components are associated with the empty string target namespace as shown in the following example. Note that not associating one or more schemas imported in the collection causes multiple schema components (potentially unrelated) to be associated with the default empty string namespace.  
  
```  
-- Create a collection that contains a schema with no target namespace.  
CREATE XML SCHEMA COLLECTION MySampleCollection AS '  
<schema xmlns="http://www.w3.org/2001/XMLSchema"  xmlns:ns="http://ns">  
<element name="e" type="dateTime"/>  
</schema>';  
go  
-- Query will return the names of all the collections that   
--contain a schema with no target namespace.  
SELECT sys.xml_schema_collections.name   
FROM   sys.xml_schema_collections   
JOIN   sys.xml_schema_namespaces   
ON     sys.xml_schema_collections.xml_collection_id =   
       sys.xml_schema_namespaces.xml_collection_id   
WHERE  sys.xml_schema_namespaces.name='';  
```  
  
### D. Using an XML schema collection and batches  
 A schema collection cannot be referenced in the same batch where it is created. If you try to reference a collection in the same batch where it was created, you will get an error saying the collection does not exist. The following example works; however, if you remove `GO` and, therefore, try to reference the XML schema collection to type an `xml` variable in the same batch, it will return an error.  
  
```  
CREATE XML SCHEMA COLLECTION mySC AS '  
<schema xmlns="http://www.w3.org/2001/XMLSchema">  
      <element name="root" type="string"/>  
</schema>  
';  
GO  
CREATE TABLE T (Col1 xml (mySC));  
GO  
```  
  
## See Also  
 [ALTER XML SCHEMA COLLECTION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-xml-schema-collection-transact-sql.md)   
 [DROP XML SCHEMA COLLECTION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-xml-schema-collection-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [Compare Typed XML to Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md)   
 [DROP XML SCHEMA COLLECTION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-xml-schema-collection-transact-sql.md)   
 [Requirements and Limitations for XML Schema Collections on the Server](../../relational-databases/xml/requirements-and-limitations-for-xml-schema-collections-on-the-server.md)  
  
  
