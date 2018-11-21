---
title: "Using Abbreviated Syntax in a Path Expression | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "axis step [XQuery]"
  - "abbreviated syntax [XQuery]"
ms.assetid: f83c2e41-5722-47c3-b5b8-bf0f8cbe05d3
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Path Expressions - Using Abbreviated Syntax
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  All the examples in [Understanding the Path Expressions in XQuery](../xquery/path-expressions-xquery.md) use unabbreviated syntax for path expressions. The unabbreviated syntax for an axis step in a path expression includes the axis name and node test, separated by double colon, and followed by zero or more step qualifiers.  
  
 For example:  
  
```  
child::ProductDescription[attribute::ProductModelID=19]  
```  
  
 XQuery supports the following abbreviations for use in path expressions:  
  
-   The **child** axis is the default axis. Therefore, the **child::** axis can be omitted from a step in an expression. For example, `/child::ProductDescription/child::Summary` can be written as `/ProductDescription/Summary`.  
  
-   An **attribute** axis can be abbreviated as @. For example, `/child::ProductDescription[attribute::ProductModelID=10]` can be written as `/ProudctDescription[@ProductModelID=10]`.  
  
-   A **/descendant-or-self::node()/** can be abbreviated as //. For example, `/descendant-or-self::node()/child::act:telephoneNumber` can be written as `//act:telephoneNumber`.  
  
     The previous query retrieves all telephone numbers stored in the AdditionalContactInfo column in the Contact table. The schema for AdditionalContactInfo is defined in a way that a \<telephoneNumber> element can appear anywhere in the document. Therefore, to retrieve all the telephone numbers, you must search every node in the document. The search starts at the root of the document and continues through all the descendant nodes.  
  
     The following query retrieves all the telephone numbers for a specific customer contact:  
  
    ```  
    SELECT AdditionalContactInfo.query('             
                declare namespace act="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes";             
                declare namespace crm="https://schemas.adventure-works.com/Contact/Record";             
                declare namespace ci="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactInfo";             
                /descendant-or-self::node()/child::act:telephoneNumber             
                ') as result             
    FROM Person.Contact             
    WHERE ContactID=1             
    ```  
  
     If you replace the path expression with the abbreviated syntax, `//act:telephoneNumber`, you receive the same results.  
  
-   The **self::node()** in a step can be abbreviated to a single dot (.). However, the dot is not equivalent or interchangeable with the **self::node()**.  
  
     For example, in the following query, the use of a dot represents a value and not a node:  
  
    ```  
    ("abc", "cde")[. > "b"]  
    ```  
  
-   The **parent::node()** in a step can be abbreviated to a double dot (..).  
  
  
