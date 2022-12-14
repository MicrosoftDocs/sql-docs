---
title: "Sequence and QNames (XQuery) | Microsoft Docs"
description: Learn about the fundamental concepts of sequences and QNames in XQuery.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "sequence [XQuery]"
  - "XQuery, sequence"
  - "QName [XQuery]"
  - "predefined namespaces [XML in SQL Server]"
ms.assetid: 3593ac26-dd78-4bf0-bb87-64fbcac5f026
author: "rothja"
ms.author: "jroth"
---
# Sequence and QNames (XQuery)
[!INCLUDE [SQL Server Azure SQL Database ](../includes/applies-to-version/sqlserver.md)]

  This topic describes the following fundamental concepts of XQuery:  
  
-   Sequence  
  
-   QNames and predefined namespaces  
  
## Sequence  
 In XQuery, the result of an expression is a sequence that is made up of a list of XML nodes and instances of XSD atomic types. An individual entry in a sequence is referred to as an item. An item in a sequence can be either of the following:  
  
-   A node such as an element, attribute, text, processing instruction, comment, or document  
  
-   An atomic value such as an instance of an XSD simple type  
  
 For example, the following query constructs a sequence of two element-node items:  
  
```  
SELECT Instructions.query('  
     <step1> Step 1 description goes here</step1>,  
     <step2> Step 2 description goes here </step2>  
') AS Result  
FROM Production.ProductModel  
WHERE ProductModelID=7;  
  
```  
  
 This is the result:  
  
```  
<step1> Step 1 description goes here </step1>  
<step2> Step 2 description goes here </step2>   
```  
  
 In the previous query, the comma (`,`) at the end of the `<step1>` construction is the sequence constructor and is required. The white spaces in the results are added for illustration only and are included in all the example results in this documentation.  
  
 Following is additional information that you should know about sequences:  
  
-   If a query results in a sequence that contains another sequence, the contained sequence is flattened into the container sequence. For example, the sequence ((1,2, (3,4,5)),6) is flattened in the data model as (1, 2, 3, 4, 5, 6).  
  
    ```  
    DECLARE @x xml;  
    SET @x = '';  
    SELECT @x.query('(1,2, (3,4,5)),6');  
    ```  
  
-   An empty sequence is a sequence that does not contain any item. It is represented as "()".  
  
-   A sequence with only one item can be treated as an atomic value, and vice versa. That is, (1) = 1.  
  
 In this implementation, the sequence must be homogeneous. That is, either you have a sequence of atomic values or a sequence of nodes. For example, the following are valid sequences:  
  
```  
DECLARE @x xml;  
SET @x = '';  
-- Expression returns a sequence of 1 text node (singleton).  
SELECT @x.query('1');  
-- Expression returns a sequence of 2 text nodes  
SELECT @x.query('"abc", "xyz"');  
-- Expression returns a sequence of one atomic value. data() returns  
-- typed value of the node.  
SELECT @x.query('data(1)');  
-- Expression returns a sequence of one element node.   
-- In the expression XML construction is used to construct an element.  
SELECT @x.query('<x> {1+2} </x>');  
```  
  
 The following query returns an error, because heterogeneous sequences are not supported.  
  
```  
SELECT @x.query('<x>11</x>, 22');  
```  
  
## QName  
 Every identifier in an XQuery is a QName. A QName is made up of a namespace prefix and a local name. In this implementation, the variable names in XQuery are QNames and they cannot have prefixes.  
  
 Consider the following example in which a query is specified against an untyped **xml** variable:  
  
```  
DECLARE @x xml;  
SET @x = '<Root><a>111</a></Root>';  
SELECT @x.query('/Root/a');  
```  
  
 In the expression (`/Root/a`), `Root` and `a` are QNames.  
  
 In the following example, a query is specified against a typed **xml** column. The query iterates over all \<step> elements at the first workcenter location.  
  
```  
SELECT Instructions.query('  
   declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
for $Step in /AWMI:root/AWMI:Location[1]/AWMI:step  
      return  
           string($Step)   
') AS Result  
FROM Production.ProductModel  
WHERE ProductModelID=7;  
```  
  
 In the query expression, note the following:  
  
-   `AWMI root`, `AWMI:Location`, `AWMI:step`, and `$Step` are all QNames. `AWMI` is a prefix, and `root`, `Location`, and `Step` are all local names.  
  
-   The `$step` variable is a QName and does not have a prefix.  
  
 The following namespaces are predefined for use with XQuery support in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
|Prefix|URI|  
|------------|---------|  
|xs|http://www.w3.org/2001/XMLSchema|  
|xsi|http://www.w3.org/2001/XMLSchema-instance|  
|xdt|http://www.w3.org/2004/07/xpath-datatypes|  
|fn|http://www.w3.org/2004/07/xpath-functions|  
|(no prefix)|`urn:schemas-microsoft-com:xml-sql`|  
|sqltypes|`https://schemas.microsoft.com/sqlserver/2004/sqltypes`|  
|xml|`http://www.w3.org/XML/1998/namespace`|  
|(no prefix)|`https://schemas.microsoft.com/sqlserver/2004/SOAP`|  
  
 Every database you create has the **sys** XML schema collection. It reserves these schemas so they can be accessed from any user-created XML schema collection.  
  
> [!NOTE]  
>  This implementation does not support the `local` prefix as described in the XQuery specification in http://www.w3.org/2004/07/xquery-local-functions.  
  
## See Also  
 [XQuery Basics](../xquery/xquery-basics.md)  
  
  
