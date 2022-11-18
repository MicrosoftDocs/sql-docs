---
title: "Default XSD mapping to tables/columns (SQLXML)"
description: Learn how elements and attributes in an XSD schema are mapped by default to tables and columns in SQLXML 4.0.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/17/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "XSD schemas [SQLXML], mapping attributes and elements"
  - "mapping schema [SQLXML], default mapping"
  - "element mapping [SQLXML], default mapping"
  - "element mapping [SQLXML]"
  - "table mapping [SQLXML]"
  - "annotated XSD schemas, mapping attributes and elements"
  - "table/view mapping [SQLXML]"
  - "column mapping [SQLXML]"
  - "attribute mapping [SQLXML], default mapping"
  - "default schema mapping"
  - "table mapping [SQLXML], default mapping"
  - "testing XPath query against schema"
  - "xml data type [SQL Server], SQLXML"
  - "table/view mapping [SQLXML], default mapping"
  - "element/attribute mapping [SQLXML]"
ms.assetid: 9a18e92a-6cfb-4a14-993a-663a95aabb63
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Default Mapping of XSD Elements and Attributes to Tables and Columns (SQLXML 4.0)
[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]
  By default, an element of complex type in an XSD annotated schema maps to the table (view) with the same name in the specified database, and an element or attribute of simple type maps to the column with the same name in the table.  
  
## Examples  
 To create working samples using the following examples, you must meet certain requirements. For more information, see [Requirements for Running SQLXML Examples](../../relational-databases/sqlxml/requirements-for-running-sqlxml-examples.md).  
  
### A. Specifying default mapping  
 In this example, no annotations are specified in the XSD schema. The **\<Person.Contact>** element is of complex type and, therefore, maps by default to the Person.Contact table in the AdventureWorks database. All the attributes (ContactID, FirstName, LastName) of the **\<Person.Contact>** element are of simple type and map by default to columns with the same names in the Person.Contact table.  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"   
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
  <xsd:element name="Person.Contact" >  
     <xsd:complexType>  
       <xsd:attribute name="ContactID"  type="xsd:string" />   
       <xsd:attribute name="FirstName"   type="xsd:string" />   
       <xsd:attribute name="LastName"    type="xsd:string" />   
     </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
```  
  
##### To test a sample XPath query against the schema  
  
1.  Copy the schema code above and paste it into a text file. Save the file as MySchema.xml.  
  
2.  Copy the following template and paste it into a text file. Save the file as MySchemaT.xml in the same directory where you saved MySchema.xml.  
  
    ```  
    <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
        <sql:xpath-query mapping-schema="MySchema.xml">  
            /Person.Contact  
        </sql:xpath-query>  
    </ROOT>  
    ```  
  
     The directory path specified for the mapping schema (MySchema.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\SqlXmlTest\MySchema.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 Here is the partial result set:  
  
```  
<?xml version="1.0" encoding="UTF-8" ?>  
<ROOT>  
  <Person.Contact ContactID="1" FirstName="Gustavo" LastName="Achong"/>  
  <Person.Contact ContactID="2" FirstName="Catherine" LastName="Abel"/>  
   ...  
</ROOT>  
```  
  
### B. Mapping an XML element to a database column  
 In this example, default mapping also takes place because no annotations are used. The **\<Person.Contact>** element is of complex type and maps to the table with the same name in the database. The elements **\<FirstName>** and **\<LastName>** and the **EmployeeID** attribute are of simple type and, therefore, map to the columns with the same names. The only difference between this and the previous example are that elements are used for mapping the FirstName and LastName fields.  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"   
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
  <xsd:element name="Person.Contact">  
    <xsd:complexType>  
      <xsd:sequence>  
        <xsd:element name="FirstName" type="xsd:string" />   
        <xsd:element name="LastName" type="xsd:string" />   
      </xsd:sequence>  
      <xsd:attribute name="ContactID" type="xsd:integer" />   
    </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
```  
  
##### To test a sample XPath query against the schema  
  
1.  Copy the schema code above and paste it into a text file. Save the file as MySchemaElements.xml.  
  
2.  Create the following template (MySchemaElementsT.xml), and save it in the same directory used in the previous step.  
  
    ```  
    <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
        <sql:xpath-query mapping-schema="MySchemaElements.xml">  
            /Person.Contact  
        </sql:xpath-query>  
    </ROOT>  
    ```  
  
     The directory path specified for the mapping schema is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\SqlXmlTest\MySchemaElements.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 Here is the partial result set:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <Person.Contact ContactID="1">  
    <FirstName>Gustavo</FirstName>  
    <LastName>Achong</LastName>  
  </Person.Contact>  
   ...  
</ROOT>  
```  
  
### C. Mapping an XML element to an XML data type column  
 In this example, default mapping also takes place because no annotations are used. The **\<Production.ProductModel>** element is of complex type and maps to the table with the same name in the database. The **ProductModelID** attribute is of simple type and, therefore, map to the columns with the same names. The only difference between this and the previous examples is that the **\<Instructions>** element is mapping to a column that uses the **xml** data type by using the **xsd:anyType** type.  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"   
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
  <xsd:element name="Production.ProductModel">  
    <xsd:complexType>  
      <xsd:sequence>  
        <xsd:element name="Instructions" type="xsd:anyType" />   
      </xsd:sequence>  
      <xsd:attribute name="ProductModelID" type="xsd:integer" />   
    </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
```  
  
 The **xml** data type was introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].  
  
##### To test a sample XPath query against the schema  
  
1.  Copy the schema code above and paste it into a text file. Save the file as MySchemaXmlAnyElements.xml.  
  
2.  Create the following template (MySchemaXmlAnyElementsT.xml), and save it in the same directory used in the previous step.  
  
    ```  
    <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
        <sql:xpath-query mapping-schema="MySchemaXmlAnyElements.xml">  
            /Production.ProductModel[@ProductModelID=7]  
        </sql:xpath-query>  
    </ROOT>  
    ```  
  
     The directory path specified for the mapping schema is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\SqlXmlTest\MySchemaXmlAnyElements.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 Here is the partial result set:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <Production.ProductModel ProductModelID="7">  
    <Instructions>  
      <root xmlns="http:  
//schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstru  
ctions">  
...  
      </root>  
    <Instructions>  
  </Production.ProductModel>  
</ROOT>  
```  
  
## See Also  
 [Annotated Schema Security Considerations &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/security/annotated-schema-security-considerations-sqlxml-4-0.md)   
 [XML Data &#40;SQL Server&#41;](../../relational-databases/xml/xml-data-sql-server.md)   
 [xml Data Type Support in SQLXML 4.0](../../relational-databases/sqlxml/xml-data-type-support-in-sqlxml-4-0.md)  
  
  
