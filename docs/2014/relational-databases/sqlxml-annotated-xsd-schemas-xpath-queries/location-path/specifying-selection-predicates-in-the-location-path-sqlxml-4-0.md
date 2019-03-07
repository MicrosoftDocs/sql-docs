---
title: "Specifying Selection Predicates in the Location Path (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "XPath queries [SQLXML], predicates"
  - "predicates [SQLXML]"
  - "position-based filtering [SQLXML]"
  - "XPath queries [SQLXML], location paths"
  - "filtering [SQLXML]"
  - "location path for XPath query"
ms.assetid: dbef4cf4-a89b-4d7e-b72b-4062f7b29a80
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Specifying Selection Predicates in the Location Path (SQLXML 4.0)
  A predicate filters a node-set with respect to an axis (similar to a WHERE clause in a SELECT statement). The predicate is specified between brackets. For each node in the node-set to be filtered, the predicate expression is evaluated with that node as the context node, with the number of nodes in the node-set as context size. If the predicate expression evaluates to TRUE for that node, the node is included in the resulting node-set.  
  
 XPath also allows position-based filtering. A predicate expression evaluating to a number selects that ordinal node. For example, the location path `Customer[3]` returns the third customer. Such numeric predicates are not supported. Only predicate expressions that return a Boolean result are supported.  
  
> [!NOTE]  
>  For information about the limitations of this XPath implementation of XPath and the differences between it and the W3C specification, see [Introduction to Using XPath Queries &#40;SQLXML 4.0&#41;](../introduction-to-using-xpath-queries-sqlxml-4-0.md).  
  
## Selection Predicate: Example 1  
 The following XPath expression (location path) selects from the current context node all the **\<Customer>** element children that have the **CustomerID** attribute with value of ALFKI:  
  
```  
/child::Customer[attribute::CustomerID="ALFKI"]  
```  
  
 In this XPath query, `child` and `attribute` are axis names. `Customer` is the node test (TRUE if `Customer` is an **\<element node>**, because **\<element>** is the principal node type for the `child` axis). `attribute::CustomerID="ALFKI"` is the predicate. In the predicate, `attribute` is the axis and `CustomerID` is the node test (TRUE if **CustomerID** is an attribute of the context node, because **\<attribute>** is the principal node type of `attribute` axis).  
  
 Using the abbreviated syntax, the XPath query can also be specified as:  
  
```  
/Customer[@CustomerID="ALFKI"]  
```  
  
## Selection Predicate: Example 2  
 The following XPath expression (location path) selects from the current context node all the **\<Order>** grandchildren that have the **SalesOrderID** attribute with the value 1:  
  
```  
/child::Customer/child::Order[attribute::SalesOrderID="1"]  
```  
  
 In this XPath expression, `child` and `attribute` are the axis names. `Customer`, `Order`, and `SalesOrderID` are the node tests. `attribute::OrderID="1"` is the predicate.  
  
 Using the abbreviated syntax, the XPath query can also be specified as:  
  
```  
/Customer/Order[@SalesOrderID="1"]  
```  
  
## Selection Predicate: Example 3  
 The following XPath expression (location path) selects from the current context node all the **\<Customer>** children that have one or more **\<ContactName>** children:  
  
```  
child::Customer[child::ContactName]  
```  
  
 This example assumes that the **\<ContactName>** is a child element of the **\<Customer>** element in the XML document, which is referred to as *element-centric mapping* in an annotated XSD schema.  
  
 In this XPath expression, `child` is the axis name. `Customer` is the node test (TRUE if `Customer` is an **\<element>** node, because **\<element>** is the principal node type for `child` axis). `child::ContactName` is the predicate. In the predicate, `child` is the axis and `ContactName` is the node test (TRUE if `ContactName` is an **\<element>** node).  
  
 This expression returns only the **\<Customer>** element children of the context node that have **\<ContactName>** element children.  
  
 Using the abbreviated syntax, the XPath query can also be specified as:  
  
```  
Customer[ContactName]  
```  
  
## Selection Predicate: Example 4  
 The following XPath expression selects **\<Customer>** element children of the context node that do not have **\<ContactName>** element children:  
  
```  
child::Customer[not(child::ContactName)]  
```  
  
 This example assumes that **\<ContactName>** is a child element of the **\<Customer>** element in the XML document, and the ContactName field is not required in the database.  
  
 In this example, `child` is the axis. `Customer` is the node test (TRUE if `Customer` is an \<element> node). `not(child::ContactName)` is the predicate. In the predicate, `child` is the axis and `ContactName` is the node test (TRUE if `ContactName` is an \<element> node).  
  
 Using the abbreviated syntax, the XPath query can also be specified as:  
  
```  
Customer[not(ContactName)]  
```  
  
## Selection Predicate: Example 5  
 The following XPath expression selects from the current context node all the **\<Customer>** children that have the **CustomerID** attribute:  
  
```  
child::Customer[attribute::CustomerID]  
```  
  
 In this example, `child` is the axis and `Customer` is node test (TRUE if `Customer` is an \<element> node). `attribute::CustomerID` is the predicate. In the predicate, `attribute` is the axis and `CustomerID` is the predicate (TRUE if `CustomerID` is an **\<attribute>** node).  
  
 Using the abbreviated syntax, the XPath query can also be specified as:  
  
```  
Customer[@CustomerID]  
```  
  
## Selection Predicate: Example 6  
 [!INCLUDE[msCoName](../../../includes/msconame-md.md)] SQLXML 4.0 includes support for XPath queries that contain a cross-product in the predicate, as shown in the following example:  
  
```  
Customer[Order/@OrderDate=Order/@ShipDate]  
```  
  
 This query selects all customers with any `Order` for which the `OrderDate` equals the `ShipDate` of any `Order`.  
  
## See Also  
 [Introduction to Annotated XSD Schemas &#40;SQLXML 4.0&#41;](../../sqlxml/annotated-xsd-schemas/introduction-to-annotated-xsd-schemas-sqlxml-4-0.md)   
 [Client-side XML Formatting &#40;SQLXML 4.0&#41;](../../sqlxml/formatting/client-side-xml-formatting-sqlxml-4-0.md)  
  
  
