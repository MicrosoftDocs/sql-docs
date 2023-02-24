---
title: "XQuery Language Reference (SQL Server) | Microsoft Docs"
description: Learn about the XQuery language for SQL Server and view a complete language reference.
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
helpviewer_keywords: 
  - "XQuery"
  - "XQuery, about XQuery"
  - "xml data type [SQL Server], XQuery"
  - "XML [SQL Server], XQuery"
  - "queries [XML in SQL Server], XQuery"
ms.assetid: 8a69344f-2990-4357-8160-cb26aac95b91
author: "rothja"
ms.author: "jroth"
---
# XQuery Language Reference (SQL Server)
[!INCLUDE [SQL Server Azure SQL Database ](../includes/applies-to-version/sqlserver.md)]

  [!INCLUDE[tsql](../includes/tsql-md.md)] supports a subset of the XQuery language that is used for querying the **xml** data type. This XQuery implementation is aligned with the July 2004 Working Draft of XQuery. The language is under development by the World Wide Web Consortium (W3C), with the participation of all major database vendors and also Microsoft. Because the W3C specifications may undergo future revisions before becoming a W3C recommendation, this implementation may be different from the final recommendation. This topic outlines the semantics and syntax of the subset of XQuery that is supported in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 For more information, see the [W3C XQuery 1.0 Language Specification](https://go.microsoft.com/fwlink/?LinkId=48846).  
  
 XQuery is a language that can query structured or semi-structured XML data. With the **xml** data type support provided in the [!INCLUDE[ssDE](../includes/ssde-md.md)], documents can be stored in a database and then queried by using XQuery.  
  
 XQuery is based on the existing XPath query language, with support added for better iteration, better sorting results, and the ability to construct the necessary XML. XQuery operates on the XQuery Data Model. This is an abstraction of XML documents, and the XQuery results that can be typed or untyped. The type information is based on the types provided by the W3C XML Schema language. If no typing information is available, XQuery handles the data as untyped. This is similar to how XPath version 1.0 handles XML.  
  
 To query an XML instance stored in a variable or column of **xml** type, you use the [xml Data Type Methods](../t-sql/xml/xml-data-type-methods.md). For example, you can declare a variable of **xml** type and query it by using the **query()** method of the **xml** data type.  
  
```sql
DECLARE @x xml  
SET @x = '<ROOT><a>111</a></ROOT>'  
SELECT @x.query('/ROOT/a')  
```  
  
 In the following example, the query is specified against the Instructions column of **xml** type in ProductModel table in the AdventureWorks database.  
  
```sql
SELECT Instructions.query('declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";           
    /AWMI:root/AWMI:Location[@LocationID=10]  
') as Result   
FROM  Production.ProductModel  
WHERE ProductModelID=7  
```  
  
 The XQuery includes the namespace declaration, `declare namespace``AWMI=...`, and the query expression, `/AWMI:root/AWMI:Location[@LocationID=10]`.  
  
 Note that the XQuery is specified against the Instructions column of **xml** type. The [query() method](../t-sql/xml/query-method-xml-data-type.md) of the xml data type is used to specify the XQuery.  
  
 The following table lists the related topics that can help in understanding the implementation of XQuery in the [!INCLUDE[ssDE](../includes/ssde-md.md)].  
  
|Topic|Description|  
|-----------|-----------------|  
|[XML Data &#40;SQL Server&#41;](../relational-databases/xml/xml-data-sql-server.md)|Explains the support for the **xml**data type in the [!INCLUDE[ssDE](../includes/ssde-md.md)] and the methods you can use against this data type. The **xml** data type forms the input XQuery data model on which the XQuery expressions are executed.|  
|[XML Schema Collections &#40;SQL Server&#41;](../relational-databases/xml/xml-schema-collections-sql-server.md)|Describes how the XML instances stored in a database can be typed. This means you can associate an XML schema collection with the **xml** type column. All the instances stored in the column are validated and typed against the schema in the collection and provide the type information for XQuery.|  
  
> [!NOTE]  
>  The organization of this section is based on the World Wide Web Consortium (W3C) XQuery working draft specification. Some of the diagrams provided in this section are taken from that specification. This section compares the Microsoft XQuery implementation to the W3C specification, describes how Microsoft XQuery is different from the W3C and indicates what W3C features are not supported. The W3C specification is available at [http://www.w3.org/TR/2004/WD-xquery-20040723](https://go.microsoft.com/fwlink/?LinkId=48846).  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[XQuery Basics](../xquery/xquery-basics.md)|Provides a basic overview of XQuery concepts, and also the expression evaluation (static and dynamic context), atomization, effective Boolean value, XQuery type system, sequence type matching, and error handling.|  
|[XQuery Expressions](../xquery/xquery-expressions.md)|Describes XQuery primary expressions, path expressions, sequence expressions, arithmetic comparison and logical expressions, XQuery construction, FLWOR expression, conditional and quantified expressions, and various expressions on sequence types.|  
|[Modules and Prologs &#40;XQuery&#41;](../xquery/modules-and-prologs-xquery.md)|Describes XQuery prolog.|  
|[XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)|Describes a list of the XQuery functions that are supported.|  
|[XQuery Operators Against the xml Data Type](../xquery/xquery-operators-against-the-xml-data-type.md)|Describes XQuery operators that are supported.|  
|[Additional Sample XQueries Against the xml Data Type](../xquery/additional-sample-xqueries-against-the-xml-data-type.md)|Provides additional XQuery samples.|  
  
## See Also  
 [XML Data &#40;SQL Server&#41;](../relational-databases/xml/xml-data-sql-server.md)   
 [XML Schema Collections &#40;SQL Server&#41;](../relational-databases/xml/xml-schema-collections-sql-server.md)   
 [Examples of Bulk Import and Export of XML Documents &#40;SQL Server&#41;](../relational-databases/import-export/examples-of-bulk-import-and-export-of-xml-documents-sql-server.md)  
  
  
