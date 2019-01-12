---
title: "Creating CDATA Sections Using sql:use-cdata (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: 01/11/2019
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "markup characters [SQLXML]"
  - "special characters [SQLXML]"
  - "use-cdata annotation"
  - "plain text [SQLXML]"
  - "CDATA sections"
  - "escaping blocks of text [SQLXML]"
  - "annotated XSD schemas, CDATA sections"
  - "sql:use-cdata"
ms.assetid: 26d2b9dc-f857-44ff-bcd4-aaf64ff809d0
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Creating CDATA Sections Using sql:use-cdata (SQLXML 4.0)

[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  In XML, CDATA sections are used to escape blocks of text that contain characters that would otherwise be recognized as markup characters.  
  
 A database in Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can sometimes contain characters that are treated as markup characters by the XML parser; for example, angle brackets (< and >), the less-than-or-equal-to symbol (<=), and the ampersand (&) are treated as markup characters. However, you can wrap this type of special characters in a CDATA section to prevent them from being treated as markup characters. The text within the CDATA section is treated by the XML parser as plain text.  
  
 The **sql:use-cdata** annotation is used to specify that the data returned by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should be wrapped in a CDATA section (that is, it indicates whether the value from a column that is specified by **sql:field** should be enclosed in a CDATA section). The **sql:use-cdata** annotation can be specified only on elements that map to a database column.  
  
 The **sql:use-cdata** annotation takes a Boolean value (0 = false, 1 = true). The acceptable values are 0, 1, true, and false.  
  
 This annotation cannot be used with **sql:url-encode** or on the ID, IDREF, IDREFS, NMTOKEN, and NMTOKENS attribute types.  
  
## Examples  
 To create working samples using the following examples, you must meet certain requirements. For more information, see [Requirements for Running SQLXML Examples](../../relational-databases/sqlxml/requirements-for-running-sqlxml-examples.md).  
  
### A. Specifying sql:use-cdata on an element  
 In the following schema, **sql:use-cdata** is set to 1 (True) for the **\<AddressLine1>** within the **\<Address>** element. As a result, the data is returned in a CDATA section.  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
  <xsd:element name="Address"   
               sql:relation="Person.Address"   
               sql:key-fields="AddressID" >  
   <xsd:complexType>  
        <xsd:sequence>  
          <xsd:element name="AddressID"  type="xsd:string" />  
          <xsd:element name="AddressLine1" type="xsd:string"   
                       sql:use-cdata="1" />  
        </xsd:sequence>  
    </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
```  
  
##### To test a sample XPath query against the schema  
  
1.  Copy the schema code above and paste it into a text file. Save the file as UseCData.xml.  
  
2.  Copy the following template and paste it into a text file. Save the file as UseCDataT.xml in the same directory where you saved UseCData.xml.  
  
    ```  
    <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
        <sql:xpath-query mapping-schema="UseCData.xml">  
            /Address[AddressID < 11]  
        </sql:xpath-query>  
    </ROOT>  
    ```  
  
     The directory path specified for the mapping schema (UseCData.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\SqlXmlTest\UseCData.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 This is the partial result set:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">   
  <Address>   
    <AddressID>1</CustomerID>   
    <AddressLine1>   
      <![CDATA[ 1970 Napa Ct.  ]]>   
    </AddressLine1>   
  </Address>  
  <Address>  
    <AddressLine1>   
      <![CDATA[ 9833 Mt. Dias Blv. ]]>   
    </AddressLine1>   
  </Address>  
  ...  
</ROOT>  
```  
  
  
