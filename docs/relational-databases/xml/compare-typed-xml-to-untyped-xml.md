---
title: "Compare Typed XML to Untyped XML | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "xml data type [SQL Server], variables"
  - "parameters [XML in SQL Server]"
  - "facets [XML in SQL Server]"
  - "xml data type [SQL Server], columns"
  - "untyped XML"
  - "xml data type [SQL Server], typed xml"
  - "XML [SQL Server], typed"
  - "variables [XML in SQL Server], creating"
  - "xml data type [SQL Server], untyped xml"
  - "columns [XML in SQL Server], creating"
  - "typed XML"
  - "document mode processing [SQL Server]"
  - "XML [SQL Server], untyped"
  - "xml data type [SQL Server], parameters"
ms.assetid: 4bc50af9-2f7d-49df-bb01-854d080c72c7
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Compare Typed XML to Untyped XML
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  You can create variables, parameters, and columns of the **xml** type. You can optionally associate a collection of XML schemas with a variable, parameter, or column of **xml** type. In this case, the **xml** data type instance is called *typed*. Otherwise, the XML instance is called *untyped*.  
  
## Well-formed XML and the xml Data Type  
 The **xml** data type implements the ISO standard **xml** data type. Therefore, it can store well-formed XML version 1.0 documents and also so-called XML content fragments with text nodes and an arbitrary number of top-level elements in an untyped XML column. The system checks that the data is well-formed, does not require the column to be bound to XML schemas, and rejects data that is not well-formed in the extended sense. This is true also of untyped XML variables and parameters.  
  
## XML Schemas  
 An XML schema provides the following:  
  
-   **Validation constraints.** Whenever a typed xml instance is assigned to or modified, SQL Server validates the instance.  
  
-   **Data type information.** Schemas provide information about the types of attributes and elements in the **xml** data type instance. The type information provides more precise operational semantics to the values contained in the instance than is possible with untyped **xml**. For example, decimal arithmetic operations can be performed on a decimal value, but not on a string value. Because of this, typed XML storage can be made significantly more compact than untyped XML.  
  
## Choosing Typed or Untyped XML  
 Use untyped **xml** data type in the following situations:  
  
-   You do not have a schema for your XML data.  
  
-   You have schemas, but you do not want the server to validate the data. This is sometimes the case when an application performs client-side validation before storing the data at the server, or temporarily stores XML data that is invalid according to the schema, or uses schema components that are not supported at the server.  
  
 Use typed **xml** data type in the following situations:  
  
-   You have schemas for your XML data and you want the server to validate your XML data according to the XML schemas.  
  
-   You want to take advantage of storage and query optimizations based on type information.  
  
-   You want to take better advantage of type information during compilation of your queries.  
  
 Typed XML columns, parameters, and variables can store XML documents or content. However, you have to specify with a flag whether you are storing a document or content at the time of declaration. Additionally, you have to provide the collection of XML schemas. Specify DOCUMENT if each XML instance has exactly one top-level element. Otherwise, use CONTENT. The query compiler uses the DOCUMENT flag in type checks during query compilation to infer singleton top-level elements.  
  
## Creating Typed XML  
 Before you can create typed **xml** variables, parameters, or columns, you must first register the XML schema collection by using [CREATE XML SCHEMA COLLECTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-xml-schema-collection-transact-sql.md). You can then associate the XML schema collection with variables, parameters, or columns of the **xml** data type.  
  
 In the following examples, a two-part naming convention is used for specifying the XML schema collection name. The first part is the schema name, and the second part is the XML schema collection name.  
  
### Example: Associating a Schema Collection with an xml Type Variable  
 The following example creates an **xml** type variable and associates a schema collection with it. The schema collection specified in the example is already imported in the **AdventureWorks** database.  
  
```  
DECLARE @x xml (Production.ProductDescriptionSchemaCollection);   
```  
  
### Example: Specifying a Schema for an xml Type Column  
 The following example creates a table with an **xml** type column and specifies a schema for the column:  
  
```  
CREATE TABLE T1(  
 Col1 int,   
 Col2 xml (Production.ProductDescriptionSchemaCollection)) ;  
```  
  
### Example: Passing an xml Type Parameter to a Stored Procedure  
 The following example passes an **xml** type parameter to a stored procedure and specifies a schema for the variable:  
  
```  
CREATE PROCEDURE SampleProc   
  @ProdDescription xml (Production.ProductDescriptionSchemaCollection)   
AS   
...  
```  
  
 Note the following about the XML schema collection:  
  
-   An XML schema collection is available only in the database in which it was registered by using [Creating an XML Schema Collection](../../t-sql/statements/create-xml-schema-collection-transact-sql.md).  
  
-   If you cast from a string to a typed **xml** data type, the parsing also performs validation and typing, based on the XML schema namespaces in the collection specified.  
  
-   You can cast from a typed **xml** data type to an untyped **xml** data type, and vice versa.  
  
 For more information about other ways to generate XML in SQL Server, see [Create Instances of XML Data](../../relational-databases/xml/create-instances-of-xml-data.md). After XML is generated, it can be assigned either to an **xml** data type variable or stored in **xml** type columns for additional processing.  
  
 In the data type hierarchy, the **xml** data type appears below **sql_variant** and user-defined types, but above any of the built-in types.  
  
### Example: Specifying Facets to Constrain a Typed xml Column  
 For typed **xml** columns, you can constrain the column to allow only single, top-level elements for each instance stored in it. You do this by specifying the optional `DOCUMENT` facet when a table is created, as shown in the following example:  
  
```  
CREATE TABLE T(Col1 xml   
   (DOCUMENT Production.ProductDescriptionSchemaCollection));  
GO  
DROP TABLE T;  
GO  
```  
  
 By default, instances stored in the typed **xml** column are stored as XML content and not as XML documents. This allows for the following:  
  
-   Zero or many top-level elements  
  
-   Text nodes in top-level elements  
  
 You can also explicitly specify this behavior by adding `CONTENT` facet, as shown in the following example:  
  
```  
CREATE TABLE T(Col1 xml(CONTENT Production.ProductDescriptionSchemaCollection));  
GO -- Default  
```  
  
 Note that you can specify the optional DOCUMENT/CONTENT facets anywhere you define **xml** type (typed xml). For example, when you create a typed **xml** variable, you can add the DOCUMENT/CONTENT facet, as shown in the following:  
  
```  
declare @x xml (DOCUMENT Production.ProductDescriptionSchemaCollection);  
```  
  
## Document Type Definition (DTD)  
 The **xml** data type columns, variables, and parameters can be typed by using XML schema, but not by using DTD. However, inline DTD can be used for both untyped and typed XML to supply default values and to replace entity references with their expanded form.  
  
 You can convert DTDs to XML schema documents by using third-party tools, and load the XML schemas into the database.  
  
## Upgrading Typed XML from SQL Server 2005  
 [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] made several extensions to the XML Schema support, including support for lax validation, improved handling of **xs:date**, **xs:time** and **xs:dateTime** instance data, and added support for list and union types. In most cases the changes do not affect the upgrade experience. However if you used an XML Schema collection in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] that allowed values of type **xs:date**, **xs:time**, or **xs:dateTime** (or any subtype) then the following upgrade steps occur when you attach your [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database to a later version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
1.  For every XML column, that is typed with an XML Schema Collection that contains elements or attributes that are typed as either **xs:anyType**, **xs:anySimpleType**, **xs:date** or any of its subtypes, **xs:time** or any subtype thereof, or **xs:dateTime** or any of its subtypes, or are union or list types containing any of these types the following occurs:  
  
    1.  All XML indices on the column will be disabled.  
  
    2.  All [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] values will continue to be represented in the Z timezone, because they have been normalized to the Z timezone.  
  
    3.  Any **xs:date** or **xs:dateTime** values that are smaller than January 1st of year 1 will lead to a runtime error when the index gets rebuild or an XQuery or XML-DML statements gets executed against the XML data type containing that value.  
  
2.  Any negative years in **xs:date** or **xs:dateTime** facets or default values in an XML Schema collection will automatically be updated to the smallest value allowed by the base **xs:date** or **xs:dateTime** type (e.g., 0001-01-01T00:00:00.0000000Z for **xs:dateTime**).  
  
 Note that you can still use a simple SQL select statement to retrieve the whole XML data type, even if it contains negative years. It is recommended that you replace negative years with a year within the newly supported range or change the type of the element or attribute to **xs:string**.  
  
## See Also  
 [Create Instances of XML Data](../../relational-databases/xml/create-instances-of-xml-data.md)   
 [xml Data Type Methods](../../t-sql/xml/xml-data-type-methods.md)   
 [XML Data Modification Language &#40;XML DML&#41;](../../t-sql/xml/xml-data-modification-language-xml-dml.md)   
 [XML Data &#40;SQL Server&#41;](../../relational-databases/xml/xml-data-sql-server.md)  
  
  
