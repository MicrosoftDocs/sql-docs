---
title: "Specifying a Node Test in the Location Path (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "XPath queries [SQLXML], location paths"
  - "principal node types [SQLXML]"
  - "node tests [SQLXML]"
  - "location path for XPath query"
ms.assetid: f46c30bf-1e24-4435-9ac2-f8ba43a8ff94
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Specifying a Node Test in the Location Path (SQLXML 4.0)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  A node test specifies the node type selected by the location step. Every axis (**child**, **parent**, **attribute**, or **self**) has a principal node type. For the **attribute** axis, the principal node type is **\<attribute>**. For the **parent**, **child**, and **self** axes, the principal node type is **\<element>**.  
  
> [!NOTE]  
>  The wildcard node test * (for example, `child::*`) is not supported.  
  
## Node Test: Example 1  
 The location path `child::Customer` selects **\<Customer>** element children of the context node.  
  
 In this example, `child` is the axis and `Customer` is the node test. The principal node type for the **child** axis is **\<element>**. Therefore, the node test is TRUE if the **\<Customer>** node is an **\<element>** node. If the context node has no **\<Customer>** children, an empty set of nodes is returned.  
  
## Node Test: Example 2  
 The location path `attribute::CustomerID` selects the **CustomerID** attribute of the context node.  
  
 In the example, `attribute` is the axis and `CustomerID` is the node test. The principal node type of the **attribute** axis is **\<attribute>**. Therefore, the node test is TRUE if **CustomerID** is an **\<attribute>** node. If the context node has no **CustomerID**, an empty set of nodes is returned.  
  
> [!NOTE]  
>  In this implementation of XPath, if a location step refers to an **\<element>** or an **\<attribute>** type that is not declared in the schema, an error is generated. This is different from the implementation of XPath in MSXML, which returns an empty node set.  
  
## Abbreviated Syntax for the Axes  
 The following abbreviated syntax for the location path is supported:  
  
-   `attribute::` can be abbreviated to `@`.  
  
     The location path `Customer[@CustomerID="ALFKI"]` is the same as `child::Customer[attribute::CustomerID="ALFKI"]`.  
  
-   `child::` can be omitted from a location step.  
  
     Thus, **child** is the default axis. The location path `Customer/Order` is the same as `child::Customer/child::Order`.  
  
-   `self::node()` can be abbreviated to one period (.), and `parent::node()` can be abbreviated to two periods (..).  
  
  
