---
title: "Data Type Coercions and the sql:datatype Annotation (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "mapping data types [SQLXML]"
  - "type attribute"
  - "sql:datatype"
  - "data types [SQLXML], converting"
  - "annotated XSD schemas, mapping data types"
  - "xsd:type"
  - "datatype annotation"
  - "converting data types [SQLXML]"
  - "data types [SQLXML], mapping data types"
  - "XSD schemas [SQLXML], mapping data types"
ms.assetid: db192105-e8aa-4392-b812-9d727918c005
author: MightyPen
ms.author: douglasl
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Data Type Coercions and the sql:datatype Annotation (SQLXML 4.0)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  In an XSD schema, the **xsd:type** attribute specifies the XSD data type of an element or attribute. When an XSD schema is used to extract data from the database, the data type specified is used to format the data.  
  
 In addition to specifying an XSD type in a schema, you can also specify a Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type by using the **sql:datatype** annotation. The **xsd:type** and **sql:datatype** attributes control the mapping between XSD data types and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types.  
  
## xsd:type Attribute  
 You can use the **xsd:type** attribute to specify the XML data type of an attribute or element that maps to a column. The **xsd:type** affects the document that is returned from the server and also the XPath query that is executed. When an XPath query is executed against a mapping schema that contains **xsd:type**, XPath uses the specified data type when it processes the query. For more information about how XPath uses **xsd:type**, see [Mapping XSD Data Types to XPath Data Types &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-using/mapping-xsd-data-types-to-xpath-data-types-sqlxml-4-0.md).  
  
 In a returned document, all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types are converted into string representations. Some data types require additional conversions. The following table lists the conversions that are used for various **xsd:type** values.  
  
|XSD data type|SQL Server conversion|  
|-------------------|---------------------------|  
|Boolean|CONVERT(bit, COLUMN)|  
|Date|LEFT(CONVERT(nvarchar(4000), COLUMN, 126), 10)|  
|decimal|CONVERT(money, COLUMN)|  
|id/idref/idrefs|id-prefix + CONVERT(nvarchar(4000), COLUMN, 126)|  
|nmtoken/nmtokens|id-prefix + CONVERT(nvarchar(4000), COLUMN, 126)|  
|Time|SUBSTRING(CONVERT(nvarchar(4000), COLUMN, 126), 1+CHARINDEX(N'T', CONVERT(nvarchar(4000), COLUMN, 126)), 24)|  
|All others|No additional conversion|  
  
> [!NOTE]  
>  Some of the values returned by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might not be compatible with the XML data types that are specified by using **xsd:type**, either because the conversion is not possible (for example, converting "XYZ" to a **decimal** data type) or because the value exceeds the range of that data type (for example, -100000 converted to an **UnsignedShort** XSD type). Incompatible type conversions might result in XML documents that are not valid, or in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] errors.  
  
## Mapping from SQL Server Data Types to XSD Data Types  
 The following table shows an obvious mapping from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types to XSD data types. If you know the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type, this table provides the corresponding XSD type that you can specify in the XSD schema.  
  
|SQL Server data type|XSD data type|  
|--------------------------|-------------------|  
|**bigint**|**long**|  
|**binary**|**base64Binary**|  
|**bit**|**boolean**|  
|**char**|**string**|  
|**datetime**|**dateTime**|  
|**decimal**|**decimal**|  
|**float**|**double**|  
|**image**|**base64Binary**|  
|**int**|**int**|  
|**money**|**decimal**|  
|**nchar**|**string**|  
|**ntext**|**string**|  
|**nvarchar**|**string**|  
|**numeric**|**decimal**|  
|**real**|**float**|  
|**smalldatetime**|**dateTime**|  
|**smallint**|**short**|  
|**smallmoney**|**decimal**|  
|**sql_variant**|**string**|  
|**sysname**|**string**|  
|**text**|**string**|  
|**timestamp**|**dateTime**|  
|**tinyint**|**unsignedByte**|  
|**varbinary**|**base64Binary**|  
|**varchar**|**string**|  
|**uniqueidentifier**|**string**|  
  
## sql:datatype Annotation  
 The **sql:datatype** annotation is used to specify the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type; this annotation must be specified when:  
  
-   You are bulk loading into a **dateTime**[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] column from an XSD **dateTime**, **date**, or **time** type. In this case, you must identify the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] column data type by using **sql:datatype="dateTime"**. This rule also applies to updategrams.  
  
-   You are bulk loading into a column of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **uniqueidentifier** type and the XSD value is a GUID that includes braces ({ and }). When you specify **sql:datatype="uniqueidentifier"**, the braces are removed from the value before it is inserted in the column. If **sql:datatype** is not specified, the value is sent with the braces, and the insert or update fails.  
  
-   The XML data type **base64Binary** maps to various [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types (**binary**, **image**, or **varbinary**). To map the XML data type **base64Binary** to a specific [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type, use the **sql:datatype** annotation. This annotation specifies the explicit [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type of the column to which the attribute maps. This is useful when data is being stored in the databases. By specifying the **sql:datatype** annotation, you can identify the explicit [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.  
  
 It is generally recommended that you specify **sql:datatype** in the schema.  
  
## Examples  
 To create working samples using the following examples, you must meet certain requirements. For more information, see [Requirements for Running SQLXML Examples](../../relational-databases/sqlxml/requirements-for-running-sqlxml-examples.md).  
  
### A. Specifying xsd:type  
 This example shows how an XSD **date** type that is specified by using the **xsd:type** attribute in the schema affects the resulting XML document. The schema provides an XML view of the Sales.SalesOrderHeader table in the AdventureWorks database.  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"   
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
  <xsd:element name="Order" sql:relation="Sales.SalesOrderHeader">  
     <xsd:complexType>  
       <xsd:attribute name="SalesOrderID" type="xsd:string" />   
       <xsd:attribute name="CustomerID"   type="xsd:string" />   
       <xsd:attribute name="OrderDate"    type="xsd:date" />   
       <xsd:attribute name="DueDate"  />   
       <xsd:attribute name="ShipDate"  type="xsd:time" />   
     </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
```  
  
 In this XSD schema, there are three attributes that return a date value from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. When the schema:  
  
-   Specifies **xsd:type=date** on the **OrderDate** attribute, the date part of the value returned by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for the **OrderDate** attribute is displayed.  
  
-   Specifies **xsd:type=time** on the **ShipDate** attribute, the time part of the value that is returned by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for the **ShipDate** attribute is displayed.  
  
-   Does not specify **xsd:type** on the **DueDate** attribute, the same value that is returned by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is displayed.  
  
##### To test a sample XPath query against the schema  
  
1.  Copy the schema code above and paste it into a text file. Save the file as xsdType.xml.  
  
2.  Copy the following template and paste it into a text file. Save the file as xsdTypeT.xml in the same directory where you saved xsdType.xml.  
  
    ```  
    <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
      <sql:xpath-query mapping-schema="xsdType.xml">  
        /Order  
      </sql:xpath-query>  
    </ROOT>  
    ```  
  
     The directory path specified for the mapping schema (xsdType.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\SqlXmlTest\xsdType.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 Here is the partial result set:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <Order SalesOrderID="43659"   
         CustomerID="676"   
         OrderDate="2001-07-01"   
         DueDate="2001-07-13T00:00:00"   
         ShipDate="00:00:00" />   
  <Order SalesOrderID="43660"   
         CustomerID="117"   
         OrderDate="2001-07-01"   
         DueDate="2001-07-13T00:00:00"   
         ShipDate="00:00:00" />   
 ...  
</ROOT>  
```  
  
 This is the equivalent XDR schema:  
  
```  
<?xml version="1.0" ?>  
<Schema xmlns="urn:schemas-microsoft-com:xml-data"  
        xmlns:dt="urn:schemas-microsoft-com:datatypes"  
        xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  
<ElementType name="Order" sql:relation="Sales.SalesOrderHeader">  
    <AttributeType name="SalesOrderID" />  
    <AttributeType name="CustomerID"  />  
    <AttributeType name="OrderDate" dt:type="date" />  
    <AttributeType name="DueDate" />  
    <AttributeType name="ShipDate" dt:type="time" />  
  
    <attribute type="SalesOrderID" sql:field="OrderID" />  
    <attribute type="CustomerID" sql:field="CustomerID" />  
    <attribute type="OrderDate" sql:field="OrderDate" />  
    <attribute type="DueDate" sql:field="DueDate" />  
    <attribute type="ShipDate" sql:field="ShipDate" />  
</ElementType>  
</Schema>  
```  
  
### B. Specifying SQL data type using sql:datatype  
 For a working sample, see Example G in [XML Bulk Load Examples &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/bulk-load-xml/xml-bulk-load-examples-sqlxml-4-0.md). In this example, a GUID value including "{" and "}" is bulk loaded. The schema in this example specifies **sql:datatype** to identify the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type as **uniqueidentifier**. This example illustrate when **sql:datatype** must be specified in the schema.  
  
  
