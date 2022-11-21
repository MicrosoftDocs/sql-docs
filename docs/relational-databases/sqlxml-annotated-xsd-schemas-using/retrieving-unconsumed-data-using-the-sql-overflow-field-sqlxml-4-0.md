---
title: "Get unconsumed data with sql:overflow-field (SQLXML)"
description: "Learn how to use the sql:overflow-field in SQLXML 4.0 to retrieve data that was unconsumed by the OPENXML function."
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/17/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "unconsumed data"
  - "storing unconsumed data"
  - "retrieving unconsumed data"
  - "annotated XSD schemas, unconsumed data"
  - "overflow data [SQLXML]"
ms.assetid: 8526998d-b47d-4a32-8dc2-7f50a8d11097
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Retrieving Unconsumed Data Using the sql:overflow-field (SQLXML 4.0)
[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]
  When records are inserted in a database from an XML document by using the [!INCLUDE[tsql](../../includes/tsql-md.md)] OPENXML function, all the unconsumed data from the source XML document can be stored in a column. When you retrieve data from a database by using annotated schemas, you can specify the **sql:overflow-field** attribute to identify the column in the table in which the overflow data is stored. The **sql:overflow-field** attribute can be specified on **\<element>**.  
  
 This data is then retrieved in these ways:  
  
-   Attributes stored in the overflow column are added to the element that contains the **sql:overflow-field** annotation.  
  
-   The child elements and their descendents, stored in the overflow column in the database, are added as child elements following the content that is explicitly specified in the schema. (No order is preserved.)  
  
## Examples  
 To create working samples using the following examples, you must meet certain requirements. For more information, see [Requirements for Running SQLXML Examples](../../relational-databases/sqlxml/requirements-for-running-sqlxml-examples.md).  
  
### A. Specifying sql:overflow-field for an element  
 This example assumes that the following script has been run so that a table named Customers2 exists in the tempdb database:  
  
```  
USE tempdb  
CREATE TABLE Customers2 (  
CustomerID       VARCHAR(10),   
ContactName    VARCHAR(30),   
AddressOverflow    NVARCHAR(500))  
  
GO  
INSERT INTO Customers2 VALUES (  
'ALFKI',   
'Joe',  
'<Address>  
  <Address1>Maple St.</Address1>  
  <Address2>Apt. E105</Address2>  
  <City>Seattle</City>  
  <State>WA</State>  
  <Zip>98147</Zip>  
 </Address>')  
GO  
```  
  
 In addition, you must create a virtual directory for the tempdb database-and a template virtual name of **template** type named "template".  
  
 In the following example, the mapping schema retrieves the unconsumed data that is stored in the AddressOverflow column of the Customers2 table:  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
  
  <xsd:element name="Customers2" sql:overflow-field="AddressOverflow" >  
    <xsd:complexType>  
      <xsd:attribute name="CustomerID"  type="xsd:integer"/>  
      <xsd:attribute name="ContactName"  type="xsd:string" />  
    </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
```  
  
##### To test a sample XPath query against the schema  
  
1.  Copy the schema code above and paste it into a text file. Save the file as Overflow.xml.  
  
2.  Copy the following template and paste it into a text file. Save the file as OverflowT.xml in the same directory where you saved Overflow.xml. The query in the template selects the records in the Customers2 table.  
  
    ```  
    <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
        <sql:xpath-query mapping-schema="Overflow.xml">  
            /Customers2  
        </sql:xpath-query>  
    </ROOT>  
    ```  
  
     The directory path specified for the mapping schema (Overflow.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\SqlXmlTest\Overflow.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  

     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 Here is the result set:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <Customers2 CustomerID="ALFKI" ContactName="Joe">  
    <Address1>Maple St.</Address1>   
    <Address2>Apt. E105</Address2>   
    <City>Seattle</City>   
    <State>WA</State>   
    <Zip>98147</Zip>   
  </Customers2>  
</ROOT>  
```  
  
  
