---
title: "Explicit Mapping XSD Elements and Attributes to Tables and Columns | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "explicit schema mapping [SQLXML]"
  - "XPath queries [SQLXML], annotated XSD schemas in queries"
  - "sql:field"
  - "row mapping [SQLXML]"
  - "attribute mapping [SQLXML], explicit mapping"
  - "field annotation"
  - "XSD schemas [SQLXML], mapping attributes and elements"
  - "names [SQLXML]"
  - "relation annotation"
  - "table/view mapping [SQLXML], explicit mapping"
  - "sql:relation"
  - "mapping schema [SQLXML], explicit mapping"
  - "annotated XSD schemas, mapping attributes and elements"
  - "column mapping [SQLXML]"
  - "element mapping [SQLXML], explicit mapping"
  - "table mapping [SQLXML], explicit mapping"
  - "element/attribute mapping [SQLXML]"
ms.assetid: 7a5ebeb6-7322-4141-a307-ebcf95976146
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Explicit Mapping XSD Elements and Attributes to Tables and Columns
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  When using an XSD schema to provide an XML view of the relational database , the elements and attributes of the schema must be mapped to tables and columns of the database. The rows in the database table/view will map to elements in the XML document. The column values in the database map to attributes or elements.  
  
 When XPath queries are specified against the annotated XSD schema, the data for the elements and attributes in the schema is retrieved from the tables and columns to which they map. To obtain a single value from the database, the mapping specified in the XSD schema must have both relation and field specification. If the name of an element/attribute is not the same name as the table/view or column name to which it maps, the **sql:relation** and **sql:field** annotations are used to specify the mapping between an element or attribute in an XML document and the table (view) or column in a database.  
  
## sql-relation  
 The **sql:relation** annotation is added to map an XML node in the XSD schema to a database table. The name of a table (view) is specified as the value of the **sql:relation** annotation.  
  
 When **sql:relation** is specified on an element, the scope of this annotation applies to all attributes and child elements that are described in the complex type definition of that element, therefore providing a shortcut in writing annotations.  
  
 The **sql:relation** annotation is also useful when identifiers that are valid in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are not valid in XML. For example, "Order Details" is a valid table name in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] but not in XML. In such cases, the **sql:relation** annotation can be used to specify the mapping, for example:  
  
```  
<xsd:element name="OD" sql:relation="[Order Details]">  
```  
  
## sql-field  
 The **sql-field** annotation maps an element or attribute to a database column. The **sql:field** annotation is added to map an XML node in the schema to a database column. You cannot specify **sql:field** on an empty content element.  
  
## Examples  
 To create working samples using the following examples, you must meet certain requirements. For more information, see [Requirements for Running SQLXML Examples](../../relational-databases/sqlxml/requirements-for-running-sqlxml-examples.md).  
  
### A. Specifying the sql:relation and sql:field annotations  
 In this example, the XSD schema consists of an **\<Contact>** element of complex type with **\<FName>** and **\<LName>** child elements and the **ContactID** attribute.  
  
 The **sql:relation** annotation maps the **\<Contact>** element to the Person.Contact table in the AdventureWorks database. The **sql:field** annotation maps the **\<FName>** element to the FirstName column and the **\<LName>** element to the LastName column.  
  
 No annotation is specified for the **ContactID** attribute. This results in a default mapping of the attribute to the column with the same name.  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
  <xsd:element name="Contact" sql:relation="Person.Contact" >  
   <xsd:complexType>  
     <xsd:sequence>  
        <xsd:element name="FName"  
                     sql:field="FirstName"   
                     type="xsd:string" />   
        <xsd:element name="LName"    
                     sql:field="LastName"    
                     type="xsd:string" />  
     </xsd:sequence>  
        <xsd:attribute name="ContactID"   
                       type="xsd:integer" />  
    </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
```  
  
##### To test a sample XPath query against the schema  
  
1.  Copy the schema code above and paste it into a text file. Save the file as MySchema-annotated.xml.  
  
2.  Copy the following template below and paste it into a text file. Save the file as MySchema-annotatedT.xml in the same directory where you saved MySchema-annotated.xml.  
  
    ```  
    <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
      <sql:xpath-query mapping-schema="MySchema-annotated.xml">  
        /Contact  
      </sql:xpath-query>  
    </ROOT>  
    ```  
  
     The directory path specified for the mapping schema (MySchema-annotated.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\SqlXmlTest\MySchema-annotated.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  
  
     For more information, see [Using ADO to Execute SQLXML Queries](../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 Here is the partial result set:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">   
 <Contact ContactID="1">   
    <FName>Gustavo</FName>   
    <LName>Achong</LName>   
 </Contact>   
  .....  
</ROOT>  
```  
  
  
