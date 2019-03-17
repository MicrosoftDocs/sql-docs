---
title: "Using Annotated XSD Schemas in Queries (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "queries [SQLXML]"
  - "inline schemas [SQLXML]"
  - "XPath queries [SQLXML], annotated XSD schemas in queries"
  - "queries [SQLXML], annotated XSD schemas"
  - "retrieving data"
  - "mapping schema [SQLXML], queries"
  - "multiple inline schemas"
  - "annotated XSD schemas, queries"
  - "XSD schemas [SQLXML], queries"
  - "templates [SQLXML], annotated XSD schemas in queries"
ms.assetid: 927a30a2-eae8-420d-851d-551c5f884f3c
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Using Annotated XSD Schemas in Queries (SQLXML 4.0)
  You can specify queries against an annotated schema to retrieve data from the database by specifying XPath queries in a template against the XSD schema.  
  
 The **\<sql:xpath-query>** element allows you to specify an XPath query against the XML view that is defined by the annotated schema. The annotated schema against which the XPath query is to be executed is identified by using the `mapping-schema` attribute of the **\<sql:xpath-query>** element.  
  
 Templates are valid XML documents that contain one or more queries. The FOR XML and XPath queries return a document fragment. Templates act as containers for the document fragments; templates thus provide a way to specify a single, top-level element.  
  
 The examples in this topic use templates to specify an XPath query against an annotated schema to retrieve data from the database.  
  
 For example, consider this annotated schema:  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"   
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
  <xsd:element name="Person.Contact" >  
     <xsd:complexType>  
       <xsd:attribute name="ContactID" type="xsd:string" />   
       <xsd:attribute name="FirstName" type="xsd:string" />   
       <xsd:attribute name="LastName"  type="xsd:string" />   
     </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
```  
  
 For the purpose of illustration, this XSD schema is stored in file named Schema2.xml. You could then have an XPath query against the annotated schema specified in the following template file (Schema2T.xml):  
  
```  
<sql:xpath-query   
     xmlns:sql="urn:schemas-microsoft-com:xmlsql"  
     >  
          Person.Contact[@ContactID="1"]  
</sql:xpath-query>  
```  
  
 You can then create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the query as part of a template file. For more information, see [Annotated XDR Schemas &#40;Deprecated in SQLXML 4.0&#41;](annotated-xdr-schemas-deprecated-in-sqlxml-4-0.md).  
  
## Using Inline Mapping Schemas  
 An annotated schema can be included directly in a template, and then an XPath query can be specified in the template against the inline schema. The template can also be an updategram.  
  
 A template can include multiple inline schemas. To use an inline schema that is included in a template, specify the **id** attribute with a unique value on the **\<xsd:schema>** element, and then use **#idvalue** to reference the inline schema. The **id** attribute is identical in behavior to the **sql:id** ({urn:schemas-microsoft-com:xml-sql}id) used in XDR schemas.  
  
 For example, the following template specifies two inline-annotated schemas:  
  
```  
<ROOT xmlns:sql='urn:schemas-microsoft-com:xml-sql'>  
<xsd:schema xmlns:xsd='http://www.w3.org/2001/XMLSchema'  
        xmlns:ms='urn:schemas-microsoft-com:mapping-schema'  
        id='InLineSchema1' sql:is-mapping-schema='1'>  
  <xsd:element name='Employees' ms:relation='HumanResources.Employee'>  
    <xsd:complexType>  
      <xsd:attribute name='LoginID'   
                     type='xsd:string'/>  
      <xsd:attribute name='Title'   
                     type='xsd:string'/>  
    </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
  
<xsd:schema xmlns:xsd='http://www.w3.org/2001/XMLSchema'  
        xmlns:ms='urn:schemas-microsoft-com:mapping-schema'  
        id='InLineSchema2' sql:is-mapping-schema='1'>  
  <xsd:element name='Contacts' ms:relation='Person.Contact'>  
    <xsd:complexType>  
  
      <xsd:attribute name='ContactID'   
                     type='xsd:string' />  
      <xsd:attribute name='FirstName'   
                     type='xsd:string' />  
      <xsd:attribute name='LastName'   
                     type='xsd:string' />  
    </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
  
<sql:xpath-query xmlns:sql='urn:schemas-microsoft-com:xml-sql'   
        mapping-schema='#InLineSchema1'>  
    /Employees[@LoginID='adventure-works\guy1']  
</sql:xpath-query>  
  
<sql:xpath-query xmlns:sql='urn:schemas-microsoft-com:xml-sql'   
        mapping-schema='#InLineSchema2'>  
    /Contacts[@ContactID='1']  
</sql:xpath-query>  
</ROOT>  
```  
  
 The template also specifies two XPath queries. Each of the **\<xpath-query>** elements uniquely identifies the mapping schema by specifying the `mapping-schema` attribute.  
  
 When you specify an inline schema in the template, the `sql:is-mapping-schema` annotation must also be specified on the **\<xsd:schema>** element. The `sql:is-mapping-schema` takes a Boolean value (0=false, 1=true). An inline schema with **sql:is-mapping-schema="1"** is treated as inline annotated schema and is not returned in the XML document.  
  
 The `sql:is-mapping-schema` annotation belongs to the template namespace `urn:schemas-microsoft-com:xml-sql`.  
  
 To test this example, save the template (InlineSchemaTemplate.xml) in a local directory and then create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template. For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 In addition to specifying the `mapping-schema` attribute on the **\<sql:xpath-query>** element in a template (when there is an XPath query), or on **\<updg:sync>** element in an updategram, you can do the following:  
  
-   Specify the `mapping-schema` attribute on the **\<ROOT>** element (global declaration) in the template. This mapping schema then becomes the default schema that will be used by all XPath and updategram nodes that have no explicit `mapping-schema` annotation.  
  
-   Specify the `mapping schema` attribute by using the ADO `Command` object.  
  
 The `mapping-schema` attribute that is specified on the **\<xpath-query>** or **\<updg:sync>** element has the highest precedence; the ADO `Command` object has the lowest precedence.  
  
 Note that if you specify an XPath query in a template and do not specify a mapping schema against which the XPath query is executed, the XPath query is treated as a **dbobject** type query. For example, consider this template:  
  
```  
<sql:xpath-query   
     xmlns:sql="urn:schemas-microsoft-com:xmlsql">  
          Production.ProductPhoto[@ProductPhotoID='100']/@LargePhoto  
</sql:xpath-query>  
```  
  
 The template specifies an XPath query but it does not specify a mapping schema. Therefore, this query is treated as a **dbobject** type query in which Production.ProductPhoto is the table name and @ProductPhotoID='100' is a predicate that finds a product photo with the ID value of 100. @LargePhoto is the column from which to retrieve the value.  
  
  
