---
title: "Path Expressions (XQuery) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "axis step [XQuery]"
  - "path expressions [XQuery]"
  - "expressions [XQuery], path"
ms.assetid: b93fa36c-bf69-46b9-b137-f597d66fd0c0
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Path Expressions (XQuery)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  XQuery path expressions locate nodes, such as element, attribute, and text nodes, in a document. The result of a path expression always occurs in document order without duplicate nodes in the result sequence. In specifying a path, you can use either unabbreviated or abbreviated syntax. The following information focuses on the unabbreviated syntax. Abbreviated syntax is described later in this topic.  
  
> [!NOTE]  
>  Because the sample queries in this topic are specified against the **xml** type columns, **CatalogDescription** and **Instructions**, in the **ProductModel** table, you should familiarize yourself with the contents and structure of the XML documents stored in these columns.  
  
 A path expression can be relative or absolute. Following is a description of both of these:  
  
-   A relative path expression is made up of one or more steps separated by one or two slash marks (/ or //). For example, `child::Features` is a relative path expression, where `Child` refers only to child nodes of the context node. This is the node that is currently being processed. The expression retrieves the \<Features> element node children of the context node.  
  
-   An absolute path expression starts with one or two slash marks (/ or //), followed by an optional, relative path. For example, the initial slash mark in the expression, `/child::ProductDescription`, indicates that it is an absolute path expression. Because a slash mark at the start of an expression returns the document root node of the context node, the expression returns all the \<ProductDescription> element node children of the document root.  
  
     If an absolute path starts with a single slash mark, it may or may not be followed by a relative path. If you specify only a single slash mark, the expression returns the root node of the context node. For an XML data type, this is its document node.  
  
 A typical path expression is made up of steps. For example, the absolute path expression, `/child::ProductDescription/child::Summary`,contains two steps separated by a slash mark.  
  
-   The first step retrieves the \<ProductDescription> element node children of the document root.  
  
-   The second step retrieves the \<Summary> element node children for each retrieved \<ProductDescription> element node, which in turn becomes the context node.  
  
 A step in a path expression can be an axis step or a general step.  
  
## Axis Step  
 An axis step in a path expression has the following parts.  
  
 [axis](../xquery/path-expressions-specifying-axis.md)  
 Defines the direction of movement. An axis step in a path expression that starts at the context node and navigates to those nodes that are reachable in the direction specified by the axis.  
  
 [node test](../xquery/path-expressions-specifying-node-test.md)  
 Specifies the node type or node names to be selected.  
  
 Zero or more optional predicates  
 Filters the nodes  by selecting some and discarding others.  
  
 The following examples use an **axisstep** in the path expressions:  
  
-   The absolute path expression, `/child::ProductDescription`, contains only one step. It specifies an axis (`child`) and a node test (`ProductDescription`).  
  
-   The relative path expression, `child::ProductDescription/child::Features`, contains two steps separated by a slash mark. Both steps specify a child axis. ProductDescription and Features are node tests.  
  
-   The relative path expression, `child::root/child::Location[attribute::LocationID=10]`,contains two steps separated by a slash mark. The first step specifies an axis (`child`) and a node test (`root`). The second step specifies all three components of an axis step: an axis (child), a node test (`Location`), and a predicate (`[attribute::LocationID=10]`).  
  
 For more information about the components of an axis step, see [Specifying Axis in a Path Expression Step](../xquery/path-expressions-specifying-axis.md), [Specifying Node Test in a Path Expression Step](../xquery/path-expressions-specifying-node-test.md), and [Specifying Predicates in a Path Expression Step](../xquery/path-expressions-specifying-predicates.md).  
  
## General Step  
 A general step is just an expression that must evaluate to a sequence of nodes.  
  
 The XQuery implementation in SQL Server supports a general step as the first step in a path expression. Following are examples of path expressions that use general steps.  
  
```  
(/a, /b)/c  
id(/a/b)  
```  
  
 For more information about the id function see, [id Function &#40;XQuery&#41;](../xquery/functions-on-sequences-id.md).  
  
## In This Section  
 [Specifying Axis in a Path Expression Step](../xquery/path-expressions-specifying-axis.md)  
 Describes working with the axis step in a path expression.  
  
 [Specifying Node Test in a Path Expression Step](../xquery/path-expressions-specifying-node-test.md)  
 Describes working with node tests in a path expression.  
  
 [Specifying Predicates in a Path Expression Step](../xquery/path-expressions-specifying-predicates.md)  
 Describes working with predicates in a path expression.  
  
 [Using Abbreviated Syntax in a Path Expression](../xquery/path-expressions-using-abbreviated-syntax.md)  
 Describes working with abbreviated syntax in a path expression.  
  
  
