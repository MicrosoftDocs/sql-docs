---
title: "Introduction to Using XPath Queries (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "XPath queries [SQLXML], about XPath queries"
  - "W3C XPath specification"
  - "XPath queries [SQLXML], functionality"
ms.assetid: 01050a8e-0ccc-4a02-a4eb-b48be5c3f4f3
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Introduction to Using XPath Queries (SQLXML 4.0)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  An XML Path Language (XPath) query can be specified as part of a URL or within a template. The mapping schema determines the structure of this resulting fragment, and the values are retrieved from the database. This process is conceptually similar to creating views using the CREATE VIEW statement and writing SQL queries against them.  
  
> [!NOTE]  
>  To understand XPath queries in SQLXML 4.0, you must be familiar with XML views and related concepts such as templates and mapping schema. For more information, see [Introduction to Annotated XSD Schemas &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml/annotated-xsd-schemas/introduction-to-annotated-xsd-schemas-sqlxml-4-0.md), and the XPath standard defined by the World Wide Web Consortium (W3C).  
  
 An XML document consists of nodes such as an element node, attribute node, text node, and so on. For example, consider this XML document:  
  
```  
<root>  
  <Customer cid= "C1" name="Janine" city="Issaquah">  
      <Order oid="O1" date="1/20/1996" amount="3.5" />  
      <Order oid="O2" date="4/30/1997" amount="13.4">Customer was  
          very satisfied</Order>  
   </Customer>  
   <Customer cid="C2" name="Ursula" city="Oelde" >  
      <Order oid="O3" date="7/14/1999" amount="100" note="Wrap it blue white red">  
          <Urgency>Important</Urgency>  
      </Order>  
      <Order oid="O4" date="1/20/1996" amount="10000"/>  
   </Customer>  
</root>  
```  
  
 In this document, **\<Customer>** is an element node, **cid** is an attribute node, and **"Important"** is a text node.  
  
 XPath is a graph navigation language used to select a set of nodes from an XML document. Each XPath operator selects a node-set based on a node-set selected by a previous XPath operator. For example, given a set of **\<Customer>** nodes, XPath can select all **\<Order>** nodes with the **date** attribute value of **"7/14/1999"**. The resulting node-set contains all the orders with order date 7/14/1999.  
  
 The XPath language is defined by the World Wide Web Consortium (W3C) as a standard navigation language. SQLXML 4.0 implements a subset of the W3C XPath specification, which is located at http://www.w3.org/TR/1999/PR-xpath-19991008.html.  
  
 The following are key differences between the W3C XPath implementation and the SQLXML 4.0 implementation.  
  
-   **Root queries**  
  
     SQLXML 4.0 does not support the root query (/). Every XPath query must begin at a top-level **\<ElementType>** in the schema.  
  
-   **Reporting errors**  
  
     The W3C XPath specification defines no error conditions. XPath queries that fail to select any nodes return an empty node-set. In SQLXML 4.0, a query can return many types of error messages.  
  
-   **Document order**  
  
     In SQLXML 4.0, document order is not always determined. Therefore, numeric predicates and axes that use document order (such as **following**) are not implemented.  
  
     The lack of document order also means that the string value of a node can be evaluated only when that node maps to a single column in a single row. An element with child elements or an IDREFS or NMTOKENS node cannot be converted to string.  
  
    > [!NOTE]  
    >  In some cases, the **key-fields** annotation or keys from the **relationship** annotation can result in a deterministic document order. However, this is not the primary use of these annotations For more information, see [Identifying Key Columns Using sql:key-fields &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-using/identifying-key-columns-using-sql-key-fields-sqlxml-4-0.md) and [Specifying Relationships Using sql:relationship &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-using/specifying-relationships-using-sql-relationship-sqlxml-4-0.md).  
  
-   **Data types**  
  
     SQLXML 4.0 has limitations in implementing the XPath **string**, **number**, and **boolean** data types. For more information, see [XPath Data Types &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/xpath-data-types-sqlxml-4-0.md).  
  
-   **Cross-product queries**  
  
     SQLXML 4.0 does not support cross-product XPath queries, such as `Customers[Order/@OrderDate=Order/@ShipDate]`. This query selects all Customers with any Order for which the OrderDate equals the ShipDate of any Order.  
  
     However, SQLXML 4.0 does support queries such as `Customer[Order[@OrderDate=@ShippedDate]]`, which selects Customers with any Order for which the OrderDate equals its ShipDate.  
  
-   **Error handling and security**  
  
     Depending on the schema and XPath query expression that are used, [!INCLUDE[tsql](../../includes/tsql-md.md)] errors could be exposed to users under certain conditions.  
  
 The tables in the following sections provide details about how the implementation of XPath queries in SQLXML 4.0 differs from the W3C specification in these areas.  
  
## Supported Functionality  
 The following table shows the features of the XPath language that are implemented in SQLXML 4.0.  
  
|Feature|Item|Link to sample queries|  
|-------------|----------|----------------------------|  
|Axes|**attribute**, **child**, **parent**, and **self** axes|[Specifying Axes in XPath Queries &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/samples/specifying-axes-in-xpath-queries-sqlxml-4-0.md)|  
|Boolean-valued predicates including successive and nested predicates||[Specifying Arithmetic Operators in XPath Queries &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/samples/specifying-arithmetic-operators-in-xpath-queries-sqlxml-4-0.md)|  
|All relational operators|=, !=, <, \<=, >, >=|[Specifying Relational Operators in XPath Queries &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/samples/specifying-relational-operators-in-xpath-queries-sqlxml-4-0.md)|  
|Arithmetic operators|+, -, *, div|[Specifying Arithmetic Operators in XPath Queries &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/samples/specifying-arithmetic-operators-in-xpath-queries-sqlxml-4-0.md)|  
|Explicit conversion functions|**number()**, **string()**, **Boolean()**|[Specifying Explicit Conversion Functions in XPath Queries &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/samples/specifying-explicit-conversion-functions-in-xpath-queries-sqlxml-4-0.md)|  
|Boolean operators|AND, OR|[Specifying Boolean Operators in XPath Queries &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/samples/specifying-boolean-operators-in-xpath-queries-sqlxml-4-0.md)|  
|Boolean functions|**true()**, **false()**, **not()**|[Specifying Boolean Functions in XPath Queries &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/samples/specifying-boolean-functions-in-xpath-queries-sqlxml-4-0.md)|  
|XPath variables||[Specifying XPath Variables in XPath Queries &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/samples/specifying-xpath-variables-in-xpath-queries-sqlxml-4-0.md)|  
  
## Unsupported Functionality  
 The following table shows the features of the XPath language that are not implemented in SQLXML 4.0.  
  
|Feature|Item|  
|-------------|----------|  
|Axes|**ancestor**, **ancestor-or-self**, **descendant**, **descendant-or-self (//)**, **following**, **following-sibling**, **namespace**, **preceding**, **preceding-sibling**|  
|Numeric-valued predicates||  
|Arithmetic operators|mod|  
|Node functions|**ancestor**, **ancestor-or-self**, **descendant**, **descendant-or-self (//)**, **following**, **following-sibling**, **namespace**, **preceding**, **preceding-sibling**|  
|String functions|**string()**, **concat()**, **starts-with()**, **contains()**, **substring-before()**, **substring-after()**, **substring()**, **string-length()**, **normalize()**, **translate()**|  
|Boolean functions|**lang()**|  
|Numeric functions|**sum()**, **floor()**, **ceiling()**, **round()**|  
|Union operator|&#124;|  
  
 When you specify XPath queries in a template, note the following behavior:  
  
-   XPath can contain characters such as < or & that have special meanings in XML (and template is an XML document). You must escape these characters using XML &-encoding, or specify the XPath in the URL.  
  
## See Also  
 [Using XPath Queries in SQLXML 4.0](../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/using-xpath-queries-in-sqlxml-4-0.md)  
  
  
