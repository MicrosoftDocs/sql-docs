---
title: "Specifying a Target Namespace Using the targetNamespace Attribute (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "namespaces [SQLXML], annotated XSD schemas"
  - "targetNamespace attribute"
  - "XSD schemas [SQLXML], target namespaces"
  - "annotated XSD schemas, target namespaces"
  - "xsd:targetNamespace"
  - "attributeFormDefault attribute"
  - "elementFormDefault attribute"
  - "target namespaces [SQLXML]"
ms.assetid: f3df9877-6672-4444-8245-2670063c9310
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Specifying a Target Namespace Using the targetNamespace Attribute (SQLXML 4.0)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  In writing XSD schemas, you can use the XSD **targetNamespace** attribute to specify a target namespace. This topic describes how the XSD **targetNamespace**, **elementFormDefault**, and **attributeFormDefault** attributes work, how they affect the XML instance that is generated, and how XPath queries are specified with namespaces.  
  
 You can use the **xsd:targetNamespace** attribute to place elements and attributes from the default namespace into a different namespace. You can also specify whether the locally declared elements and attributes of the schema should appear qualified by a namespace, either explicitly by using a prefix or implicitly by default. You can use the **elementFormDefault** and **attributeFormDefault** attributes on the **\<xsd:schema>** element to globally specify the qualification of local elements and attributes, or you can use the **form** attribute to specify individual elements and attributes separately.  
  
## Examples  
 To create working samples using the following examples, you must meet certain requirements. For more information, see [Requirements for Running SQLXML Examples](../../relational-databases/sqlxml/requirements-for-running-sqlxml-examples.md).  
  
### A. Specifying a target namespace  
 The following XSD schema specifies a target namespace by using the **xsd:targetNamespace** attribute. The schema also sets the **elementFormDefault** and **attributeFormDefault** attribute values to **"unqualified"** (the default value for these attributes). This is a global declaration and affects all the local elements (**\<Order>** in the schema) and attributes (**CustomerID**, **ContactName**, and **OrderID** in the schema).  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema"  
            xmlns:CO="urn:MyNamespace"   
            targetNamespace="urn:MyNamespace" >  
<xsd:annotation>  
  <xsd:appinfo>  
    <sql:relationship name="CustOrders"  
          parent="Sales.Customer"  
          parent-key="CustomerID"  
          child="Sales.SalesOrderHeader"  
          child-key="CustomerID" />  
  </xsd:appinfo>  
</xsd:annotation>  
  
  <xsd:element name="Customer"   
               sql:relation="Sales.Customer"   
               type="CO:CustomerType" />  
  
  <xsd:complexType name="CustomerType" >  
     <xsd:sequence>  
        <xsd:element name="Order"   
                     sql:relation="Sales.SalesOrderHeader"  
                     sql:relationship="CustOrders"  
                     type="CO:OrderType" />  
     </xsd:sequence>  
        <xsd:attribute name="CustomerID"   type="xsd:string" />   
        <xsd:attribute name="SalesPersonID"  type="xsd:string" />  
  </xsd:complexType>  
  <xsd:complexType name="OrderType" >  
     <xsd:attribute name="SalesOrderID" type="xsd:integer" />  
     <xsd:attribute name="CustomerID" type="xsd:string" />  
  </xsd:complexType>  
</xsd:schema>  
```  
  
 In the schema:  
  
-   The **CustomerType** and **OrderType** type declarations are global and, therefore, are included in the schema's target namespace. As a result, when these types are referenced in the declaration of **\<Customer>** element and its **\<Order>** child element, a prefix is specified that is associated with the target namespace.  
  
-   The **\<Customer>** element is also included in the target namespace of the schema because it is a global element in the schema.  
  
 Execute the following XPath query against the schema:  
  
```  
(/CO:Customer[@CustomerID=1)   
```  
  
 The XPath query generates this instance document (only a few of the orders are shown):  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <y0:Customer xmlns:y0="urn:MyNamespace"   
      CustomerID="ALFKI" ContactName="Maria Anders">  
        <Order CustomerID="ALFKI" OrderID="10643" />   
        <Order CustomerID="ALFKI" OrderID="10692" />   
        ...  
  </y0:Customer>  
  </ROOT>  
```  
  
 This instance document defines the urn:MyNamespace namespace and associates a prefix (y0) to it. The prefix is applied only to the **\<Customer>** global element. (The element is global because it is declared as a child of **\<xsd:schema>** element in the schema.)  
  
 The prefix is not applied to the local elements and attributes because the value of **elementFormDefault** and **attributeFormDefault** attributes is set to **"unqualified"** in the schema. Note that the **\<Order>** element is local because its declaration appears as a child of the **\<complexType>** element that defines the **\<CustomerType>** element. Similarly, the attributes (**CustomerID**, **OrderID**, and **ContactName**) are local, not global.  
  
##### To create a working sample of this schema  
  
1.  Copy the schema code above and paste it into a text file. Save the file as targetNameSpace.xml.  
  
2.  Copy the following template and paste it into a text file. Save the file as targetNameSpaceT.xml in the same directory where you saved targetNamespace.xml.  
  
    ```  
    <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
      <sql:xpath-query mapping-schema="targetNamespace.xml"  
                       xmlns:CO="urn:MyNamespace" >  
        /CO:Customer[@CustomerID=1]  
      </sql:xpath-query>  
    </ROOT>  
    ```  
  
     The XPath query in the template returns the **\<Customer>** element for the customer with a CustomerID of 1. Note that the XPath query specifies the namespace prefix for the element in the query and not for the attribute. (Local attributes are not qualified, as specified in the schema.)  
  
     The directory path specified for the mapping schema (targetNamespace.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\MyDir\targetNamepsace.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  
  
     For more information, see [Using ADO to Execute SQLXML Queries](../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 If the schema specifies **elementFormDefault** and **attributeFormDefault** attributes with value **"qualified"**, the instance document will have all of the local elements and attributes qualified. You can change the previous schema to include these attributes in the **\<xsd:schema>** element and execute the template again. Because the attributes are now also qualified in the instance, the XPath query will change to include the namespace prefix.  
  
 This is the revised XPath query:  
  
```  
/CO:Customer[@CO:CustomerID=1]  
```  
  
 This is the XML document that is returned:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
   <y0:Customer xmlns:y0="urn:MyNamespace" CustomerID="1" SalesPersonID="280">  
      <Order SalesOrderID="43860" CustomerID="1" />   
      <Order SalesOrderID="44501" CustomerID="1" />   
      <Order SalesOrderID="45283" CustomerID="1" />   
      <Order SalesOrderID="46042" CustomerID="1" />   
   </y0:Customer>  
</ROOT>  
```  
  
  
