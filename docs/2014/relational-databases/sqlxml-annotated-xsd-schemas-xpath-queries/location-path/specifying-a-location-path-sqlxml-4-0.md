---
title: "Specifying a Location Path (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "absolute location path"
  - "node-set [SQLXML]"
  - "XPath queries [SQLXML], location paths"
  - "relative location path [SQLXML]"
  - "location path for XPath query"
ms.assetid: a23a2b75-bc69-49f0-99db-05e14dc15bc0
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Specifying a Location Path (SQLXML 4.0)
  XPath queries are specified in the form of an expression. There are various kinds of expressions. A location path is an expression that selects a set of nodes relative to the context node. The result of evaluating a location path is a node-set.  
  
## Types of Location Paths  
 A location path can take either of these forms:  
  
-   **Absolute location path**  
  
     An absolute location path starts at the root node of the document. It consists of a slash mark (/) optionally followed by a relative location path. The slash mark (/) selects the root node of the document.  
  
-   **Relative location path**  
  
     A relative location path starts at the context node in the document. A location path consists of a sequence of one or more location steps separated by a slash mark (/). Each step selects a set of nodes relative to the context node. The initial sequence of steps selects a set of nodes relative to a context node. Each node in that set is used as a context node for the following step. The sets of nodes identified by that step are joined. For example, **child::Order/child::OrderDetail** selects the **\<OrderDetail>** element children of the **\<Order>** element children of the context node.  
  
    > [!NOTE]  
    >  In the SQLXML 4.0 implementation of XPath, every XPath query begins at the root context, even if the XPath is not explicitly absolute. For example, an XPath query beginning with "Customer" is treated as "/Customer". In the XPath query **Customer[Order]**, Customer begins at the root context, but Order begins at the Customer context. For more information, see [Introduction to Using XPath Queries &#40;SQLXML 4.0&#41;](../introduction-to-using-xpath-queries-sqlxml-4-0.md).  
  
## Location Steps  
 A location path (absolute or relative) is composed of location steps that contain three parts:  
  
-   **Axis**  
  
     The axis specifies the tree relationship between the nodes selected by the location step and the context node. The `parent`, `child`, `attribute`, and `self` axes are supported. If a `child` axis is specified in the location path, all the nodes selected by the query are the children of the context node. If a `parent` axis is specified, the node selected is the parent node of the context node. If an `attribute` axis is specified, the nodes selected are the attributes of the context node.  
  
-   **Node test**  
  
     A node test specifies the node type selected by the location step. Every axis (`child`, `parent`, `attribute`, and `self`) has a principal node type. For the `attribute` axis, the principal node type is **\<attribute>**. For the `parent`, `child`, and `self` axes, the principal node type is **\<element>**.  
  
     For example, if the location path specifies **child::Customer**, the **\<Customer>** element children of the context node are selected. Because the `child` axis has **\<element>** as its principal node type, the node test, Customer, is TRUE if Customer is an **\<element>** node.  
  
-   **Selection predicates (zero or more)**  
  
     A predicate filters a node-set with respect to an axis. Specifying selection predicates in an XPath expression is similar to specifying a WHERE clause in a SELECT statement. The predicate is specified between brackets. Applying the test specified in the selection predicates filters the nodes returned by the node test. For each node in the node-set to be filtered, the predicate expression is evaluated with that node as the context node, with the number of nodes in the node-set as context size. If the predicate expression evaluates to TRUE for that node, the node is included in the resulting node-set.  
  
     The syntax for a location step is the axis name and node test separated by two colons (::), followed by zero or more expressions, each in square brackets. For example, the XPath expression (location path) **child::Customer[@CustomerID='ALFKI']** selects all the **\<Customer>** element children of the context node. Then the test in the predicate is applied to the node-set, which returns only the **\<Customer>** element nodes with attribute value 'ALFKI' for its **CustomerID** attribute.  
  
## In This Section  
 [Specifying an Axis &#40;SQLXML 4.0&#41;](specifying-an-axis-sqlxml-4-0.md)  
 Provides examples of specifying an axis.  
  
 [Specifying a Node Test in the Location Path &#40;SQLXML 4.0&#41;](specifying-a-node-test-in-the-location-path-sqlxml-4-0.md)  
 Provides examples of specifying a node test.  
  
 [Specifying Selection Predicates in the Location Path &#40;SQLXML 4.0&#41;](specifying-selection-predicates-in-the-location-path-sqlxml-4-0.md)  
 Provides examples of specifying selection predicates.  
  
  
