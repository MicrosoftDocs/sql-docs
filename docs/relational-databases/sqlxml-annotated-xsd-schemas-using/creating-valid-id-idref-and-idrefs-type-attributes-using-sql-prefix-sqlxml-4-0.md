---
title: "Create Valid ID-IDREF-IDREFS Type Attributes - sql:prefix (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "IDREFS relationships [SQLXML]"
  - "annotated XSD schemas, ID type attribute"
  - "IDREF type attribute [SQLXML]"
  - "annotated XSD schemas, IDREFS type attribute"
  - "ID type attribute [SQLXML]"
  - "sql:prefix"
  - "prefix annotation"
  - "IDREF relationships [SQLXML]"
  - "IDREFS type attribute [SQLXML]"
  - "annotated XSD schemas, IDREF type attribute"
  - "ID relationships [SQLXML]"
ms.assetid: 1c7f77d3-81f3-4820-bb63-c4aaa4ea9aa1
author: MightyPen
ms.author: douglasl
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Creating Valid ID, IDREF, and IDREFS Type Attributes Using sql:prefix (SQLXML 4.0)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  An attribute can be specified to be an ID type attribute. Attributes specified as IDREF or IDREFS can then be used to refer to the ID type attributes, enabling links between documents.  
  
 ID, IDREF, and IDREFS correspond to PK/FK (primary key/foreign key) relationships in the database, with few differences. In an XML document, the values of ID type attributes must be distinct. If **CustomerID** and **OrderID** attributes are specified as ID type in an XML document, these values must be distinct. However, in a database, CustomerID and OrderID columns can have the same values. (For example, CustomerID = 1 and OrderID = 1 are valid in the database).  
  
 For the ID, IDREF, and IDREFS attributes to be valid:  
  
-   The value of ID must be unique within the XML document.  
  
-   For every IDREF and IDREFS, the referenced ID values must be in the XML document.  
  
-   The value of an ID, IDREF, and IDREFS must be a named token. (For example, the integer value 101 cannot be an ID value.)  
  
-   The attributes of ID, IDREF, and IDREFS type cannot be mapped to columns of the type **text**, **ntext**, or **image** or any other binary data type (for example, **timestamp**).  
  
 If an XML document contains multiple IDs, use the **sql:prefix** annotation to ensure that the values are unique.  
  
 Note that **sql:prefix** annotation cannot be used with XSD fixed attribute.  
  
## Examples  
 To create working samples using the following examples, you must meet certain requirements. For more information, see [Requirements for Running SQLXML Examples](../../relational-databases/sqlxml/requirements-for-running-sqlxml-examples.md).  
  
### A. Specifying ID and IDREFS types  
 In the following schema, the **\<Customer>** element consists of the **\<Order>** child element. The **\<Order>** element also has a child element, the **\<OrderDetail>** element.  
  
 The **OrderIDList** attribute of **\<Customer>** is an IDREFS type attribute that refers to the **OrderID** attribute of the **\<Order>** element.  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
<xsd:annotation>  
  <xsd:appinfo>  
    <sql:relationship name="CustOrders"  
                 parent="Sales.Customer"  
                 parent-key="CustomerID"  
                 child="Sales.SalesOrderHeader"  
                 child-key="CustomerID" />  
    <sql:relationship name="OrderOrderDetail"  
                 parent="Sales.SalesOrderHeader"  
                 parent-key="SalesOrderID"  
                 child="Sales.SalesOrderDetail"  
                 child-key="SalesOrderID" />  
  </xsd:appinfo>  
</xsd:annotation>  
  <xsd:element name="Customer" sql:relation="Sales.Customer" >  
   <xsd:complexType>  
     <xsd:sequence>  
        <xsd:element name="Order" sql:relation="Sales.SalesOrderHeader"    
               sql:relationship="CustOrders" maxOccurs="unbounded" >  
          <xsd:complexType>  
              <xsd:sequence>  
                <xsd:element name="OrderDetail"   
                             sql:relation="Sales.SalesOrderDetail"   
                   sql:relationship="OrderOrderDetail"   
                   maxOccurs="unbounded" >  
                  <xsd:complexType>  
                   <xsd:attribute name="SalesOrderID" type="xsd:integer" />  
                   <xsd:attribute name="ProductID" type="xsd:string" />  
                   <xsd:attribute name="OrderQty" type="xsd:integer" />  
                  </xsd:complexType>  
               </xsd:element>  
             </xsd:sequence>  
             <xsd:attribute name="SalesOrderID"   
                            type="xsd:ID" sql:prefix="ord-" />  
             <xsd:attribute name="OrderDate" type="xsd:date" />  
             <xsd:attribute name="CustomerID" type="xsd:string" />  
          </xsd:complexType>  
      </xsd:element>  
    </xsd:sequence>  
    <xsd:attribute name="CustomerID" type="xsd:string" />  
    <xsd:attribute name="OrderIDList" type="xsd:IDREFS"   
                   sql:relation="Sales.SalesOrderHeader" sql:field="SalesOrderID"  
                   sql:relationship="CustOrders" sql:prefix="ord-">  
    </xsd:attribute>  
  </xsd:complexType>  
</xsd:element>  
</xsd:schema>  
```  
  
##### To test a sample XPath query against the schema  
  
1.  Copy the schema code above and paste it into a text file. Save the file as sqlPrefix.xml.  
  
2.  Copy the following template and paste it into a text file. Save the file as sqlPrefixT.xml in the same directory where you saved sqlPrefix.xml.  
  
    ```  
    <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
      <sql:xpath-query mapping-schema="sqlPrefix.xml">  
        /Customer[@CustomerID=1]  
      </sql:xpath-query>  
    </ROOT>  
    ```  
  
     The directory path specified for the mapping schema (sqlPrefix.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\SqlXmlTest\sqlPrefix.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  
  
     For more information, see [Using ADO to Execute SQLXML Queries](../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 This is the partial result:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <Customer CustomerID="1" OrderIDList="ord-43860 ord-44501 ord-45283 ord-46042">  
    <Order SalesOrderID="ord-43860" OrderDate="2001-08-01" CustomerID="1">  
      <OrderDetail SalesOrderID="43860" ProductID="729" OrderQty="1" />   
      <OrderDetail SalesOrderID="43860" ProductID="732" OrderQty="1" />   
      <OrderDetail SalesOrderID="43860" ProductID="738" OrderQty="1" />   
      <OrderDetail SalesOrderID="43860" ProductID="753" OrderQty="2" />   
      ...  
    </Order>  
    ...  
 </Customer>  
</ROOT>  
```  
  
  
