---
title: "Requesting URL References to BLOB Data Using sql:encode (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "sql:encode"
  - "encode annotation"
  - "URL references to BLOB data [SQLXML]"
  - "references to BLOB data [SQLXML]"
  - "annotated XSD schemas, URL references to BLOB data"
  - "requesting URL references to BLOB data"
  - "BLOBs, URL references"
  - "Base 64-encoded format"
ms.assetid: 2f8cd93b-c636-462b-8291-167197233ee0
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Requesting URL References to BLOB Data Using sql:encode (SQLXML 4.0)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  In an annotated XSD schema, when an attribute (or element) is mapped to a BLOB column in Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the data is returned in Base 64-encoded format within XML.  
  
 If you want a reference to the data (a URI) to be returned that can be used later to retrieve the BLOB data in a binary format, specify the **sql:encode** annotation. You can specify **sql:encode** on an attribute or element of simple type.  
  
 Specify the **sql:encode** annotation to indicate that a URL to the field should be returned instead of the value of the field. **sql:encode** depends on the primary key to generate a singleton select in the URL. The primary key can be specified using the **sql:key-fields** annotation.  
  
 The **sql:encode** annotation can be assigned the "url" or the "default" value. A value of "default" returns data in Base 64-encoded format.  
  
 The **sql:encode** annotation cannot be used with **sql:use-cdata** or on the ID, IDREF, IDREFS, NMTOKEN, or NMTOKENS attribute types. It can also not be used with XSD **fixed** attribute.  
  
> [!NOTE]  
>  BLOB-type columns cannot be used as a part of a key or foreign key.  
  
## Examples  
 To create working samples using the following examples, you must meet certain requirements. For more information, see [Requirements for Running SQLXML Examples](../../relational-databases/sqlxml/requirements-for-running-sqlxml-examples.md).  
  
### A. Specifying sql:encode to obtain a URL reference to BLOB data  
 In this example, the mapping schema specifies **sql:encode** on the **LargePhoto** attribute to retrieve the URI reference to a specific product photo (instead of retrieving the binary data in Base 64-encoded format).  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
  
  <xsd:element name="ProductPhoto" sql:relation="Production.ProductPhoto"   
               sql:key-fields="ProductPhotoID" >  
   <xsd:complexType>  
      <xsd:attribute name="ProductPhotoID"  type="xsd:int"  />  
     <xsd:attribute name="LargePhoto" type="xsd:string" sql:encode="url" />  
    </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
```  
  
##### To test a sample XPath query against the schema  
  
1.  Copy the schema code above and paste it into a text file. Save the file as sqlEncode.xml.  
  
2.  Copy the following template and paste it into a text file. Save the file as sqlEncodeT.xml in the same directory where you saved sqlEncode.xml.  
  
    ```  
    <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
        <sql:xpath-query mapping-schema="sqlEncode.xml">  
            /ProductPhoto[@ProductPhotoID=100]  
        </sql:xpath-query>  
    </ROOT>  
    ```  
  
     The directory path specified for the mapping schema (sqlEncode.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\SqlXmlTest\sqlEncode.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 This is the result:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
   <ProductPhoto ProductPhotoID="100"  
                 LargePhoto="dbobject/Production.ProductPhoto[@ProductPhotoID="100"]/@LargePhoto" />   
</ROOT>  
```  
  
  
