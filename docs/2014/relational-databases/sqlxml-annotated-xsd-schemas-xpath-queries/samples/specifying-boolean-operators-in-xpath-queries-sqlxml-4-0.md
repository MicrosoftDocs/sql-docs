---
title: "Specifying Boolean Operators in XPath Queries (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "XPath operators [SQLXML]"
  - "OR operator"
  - "Boolean operators"
  - "XPath queries [SQLXML], Boolean operators"
  - "operators [SQLXML]"
ms.assetid: 9928cff5-62ac-42aa-96bf-2e09a1df0bc3
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Specifying Boolean Operators in XPath Queries (SQLXML 4.0)
  The following example shows how Boolean operators are specified in XPath queries. The XPath query in this example is specified against the mapping schema contained in SampleSchema1.xml. For information about this sample schema, see [Sample Annotated XSD Schema for XPath Examples &#40;SQLXML 4.0&#41;](sample-annotated-xsd-schema-for-xpath-examples-sqlxml-4-0.md).  
  
## Examples  
  
### A. Specify the OR Boolean operator  
 This XPath query returns the **\<Customer>** element children of the context node with the **CustomerID** attribute value of 13 or 31:  
  
```  
/child::Customer[attribute::CustomerID="13" or attribute::CustomerID="31"]  
```  
  
 A shortcut to the `attribute` axis (@) can be specified, and because the `child` axis is the default, it can be omitted:  
  
```  
/Customer[@CustomerID="13" or @CustomerID="31"]  
```  
  
 In the predicate, `attribute` is the axis and `CustomerID` is the node test (TRUE if **CustomerID** is an **\<attribute>** node, because the **\<attribute>** node is the primary node for the `attribute` axis). The predicate filters the **\<Customer>** elements and returns only those that satisfy the condition specified in the predicate.  
  
##### To test the XPath queries against the mapping schema  
  
1.  Copy the [sample schema code](sample-annotated-xsd-schema-for-xpath-examples-sqlxml-4-0.md) and paste it into a text file. Save the file as SampleSchema1.xml.  
  
2.  Create the following template (BooleanOperatorsA.xml) and save it in the directory where SampleSchema1.xml is saved.  
  
    ```  
    <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
      <sql:xpath-query mapping-schema="SampleSchema1.xml">  
        /Customer[@CustomerID="13" or @CustomerID="31"]  
      </sql:xpath-query>  
    </ROOT>  
    ```  
  
     The directory path specified for the mapping schema (SampleSchema1.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\MyDir\SampleSchema1.xml"  
    ```  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 Here is the result set of the template execution:  
  
```  
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <Customer CustomerID="13" SalesPersonID="286" TerritoryID="7" AccountNumber="13" CustomerType="S" />   
  <Customer CustomerID="31" SalesPersonID="286" TerritoryID="7" AccountNumber="31" CustomerType="S" Orders="Ord-51803 Ord-69427">  
    <Order SalesOrderID="Ord-51803" SalesPersonID="286" OrderDate="2003-08-01T00:00:00" DueDate="2003-08-13T00:00:00" ShipDate="2003-08-08T00:00:00">  
      <OrderDetail ProductID="Prod-718" UnitPrice="1059.31" OrderQty="1" UnitPriceDiscount="0" />   
      <OrderDetail ProductID="Prod-838" UnitPrice="1059.31" OrderQty="1" UnitPriceDiscount="0" />   
    </Order>  
    <Order SalesOrderID="Ord-69427" SalesPersonID="286" OrderDate="2004-05-01T00:00:00" DueDate="2004-05-13T00:00:00" ShipDate="2004-05-08T00:00:00">  
      <OrderDetail ProductID="Prod-835" UnitPrice="440.1742" OrderQty="1" UnitPriceDiscount="0" />   
    </Order>  
  </Customer>  
</ROOT>  
```  
  
  
