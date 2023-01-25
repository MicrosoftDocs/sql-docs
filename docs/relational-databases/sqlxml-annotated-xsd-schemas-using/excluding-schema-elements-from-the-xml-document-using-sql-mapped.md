---
title: "Exclude schema elements from XML Doc with sql:mapped"
description: "Learn how to use the sql:mapped annotation to create an element in the XSD schema that does not map to a database table (view) or column."
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/17/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "element does not map [SQLXML]"
  - "annotated XSD schemas, excluding schema elements"
  - "mapped annotation"
  - "table mapping [SQLXML], excluding schema elements"
ms.assetid: 7d2649dd-0038-4a2c-b16d-f80f7c306966
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Excluding Schema Elements from the XML Document Using sql:mapped
[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]
  Every element and attribute in the XSD schema maps to a database table/view and column because of the default mapping. If you want to create an element in the XSD schema that does not map to any database table (view) or column and that does not appear in the XML, you can specify the **sql:mapped** annotation.  
  
 The **sql:mapped** annotation is especially useful if the schema cannot be modified or if the schema is used to validate XML from other sources and yet contains data that is not stored in your database. The **sql:mapped** annotation differs from **sql:is-constant** in that the unmapped elements and attributes do not appear in the XML document.  
  
 The **sql:mapped** annotation takes a Boolean value (0 = false, 1 = true). The acceptable values are 0, 1, true, and false.  
  
## Examples  
 To create working samples using the following examples, you must meet certain requirements. For more information, see [Requirements for Running SQLXML Examples](../../relational-databases/sqlxml/requirements-for-running-sqlxml-examples.md).  
  
### A. Specifying the sql:mapped annotation  
 Assume you have an XSD schema from some other source. This XSD schema consists of an **\<Person.Contact>** element with **ContactID**, **FirstName**, **LastName**, and **HomeAddress** attributes.  
  
 In mapping this XSD schema to the Person.Contact table in the AdventureWorks database, **sql:mapped** is specified on the **HomeAddress** attribute because the Employees table does not store the home addresses of employees. As a result, this attribute is not mapped to the database and is not returned in the resulting XML document when an XPath query is specified against the mapping schema.  
  
 Default mapping takes place for the rest of the schema. The **\<Person.Contact>** element maps to the Person.Contact table, and all the attributes map to the columns with the same name in the Person.Contact table.  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
  <xsd:element name="Person.Contact">  
    <xsd:complexType>  
      <xsd:attribute name="ContactID"   type="xsd:string"/>  
      <xsd:attribute name="FirstName"    type="xsd:string" />  
      <xsd:attribute name="LastName"     type="xsd:string" />  
      <xsd:attribute name="HomeAddress" type="xsd:string"   
                     sql:mapped="false" />  
    </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
```  
  
##### To test a sample XPath query against the schema  
  
1.  Copy the schema code above and paste it into a text file. Save the file as sql-mapped.xml.  
  
2.  Copy the following template and paste it into a text file. Save the file as sql-mappedT.xml in the same directory where you saved sql-mapped.xml.  
  
    ```  
    <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
        <sql:xpath-query mapping-schema="sql-mapped.xml">  
            /Person.Contact[@ContactID < 10]  
        </sql:xpath-query>  
    </ROOT>  
    ```  
  
     The directory path specified for the mapping schema (MySchema.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\MyDir\sql-mapped.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  

     For more information, see [Using ADO to Execute SQLXML Queries](../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 This is the result set:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <Person.Contact ContactID="1" FirstName="Gustavo" LastName="Achong" />   
  <Person.Contact ContactID="2" FirstName="Catherine" LastName="Abel" />   
  <Person.Contact ContactID="3" FirstName="Kim" LastName="Abercrombie" />   
  <Person.Contact ContactID="4" FirstName="Humberto" LastName="Acevedo" />   
  <Person.Contact ContactID="5" FirstName="Pilar" LastName="Ackerman" />   
  <Person.Contact ContactID="6" FirstName="Frances" LastName="Adams" />   
  <Person.Contact ContactID="7" FirstName="Margaret" LastName="Smith" />   
  <Person.Contact ContactID="8" FirstName="Carla" LastName="Adams" />   
  <Person.Contact ContactID="9" FirstName="Jay" LastName="Adams" />   
</ROOT>  
```  
  
 Note that the ContactID, FirstName, and LastName are present, but HomeAddress is not because the mapping schema specified 0 for the **sql:mapped** attribute.  
  
## See Also  
 [Default Mapping of XSD Elements and Attributes to Tables and Columns &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-using/default-mapping-of-xsd-elements-and-attributes-to-tables-and-columns-sqlxml-4-0.md)  
  
  
