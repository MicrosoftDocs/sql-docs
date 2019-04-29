---
title: "Use Full-Text Search with XML Columns | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "xml columns [full-text search]"
  - "indexes [full-text search]"
ms.assetid: 8096cfc6-1836-4ed5-a769-a5d63b137171
author: MightyPen
ms.author: genemi
manager: craigg
---
# Use Full-Text Search with XML Columns
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  You can create a full-text index on XML columns that indexes the content of the XML values, but ignores the XML markup. Element tags are used as token boundaries. The following items are indexed:  
  
-   The content of XML elements.  
  
-   The content of XML attributes of the top-level element only, unless those values are numeric values.  
  
 When possible, you can combine full-text search with XML index in the following way:  
  
1.  First, filter the XML values of interest by using SQL full-text search.  
  
2.  Next, query those XML values that use XML index on the XML column.  
  
## Example: Combining Full-text Search with XML Querying  
 After the full-text index has been created on the XML column, the following query checks that an XML value contains the word "custom" in the title of a book:  
  
```  
SELECT *   
FROM   T   
WHERE  CONTAINS(xCol,'custom')   
AND    xCol.exist('/book/title/text()[contains(.,"custom")]') =1  
```  
  
 The **contains()** method uses the full-text index to subset the XML values that contain the word "custom" anywhere in the document. The **exist()** clause ensures that the word "custom" occurs in the title of a book.  
  
 A full-text search that uses **contains()** and XQuery **contains()** has different semantics. The latter is a substring match and the former is a token match that uses stemming. Therefore, if the search is for the string that has "run" in the title, the matches will include "run", "runs", and "running", because both the full-text **contains()** and the Xquery **contains()** are satisfied. However, the query does not match the word "customizable" in the title in that the full-text **contains()** fails, but the Xquery **contains()** is satisfied. Generally, for pure substring match, the full-text **contains()** clause should be removed.  
  
 Additionally, full-text search uses word stemming, but XQuery **contains()** is a literal match. This difference is illustrated in the next example.  
  
## Example: Full-text Search on XML Values Using Stemming  
 The XQuery **contains()** check that was performed in the previous example generally cannot be eliminated. Consider this query:  
  
```  
SELECT *   
FROM   T   
WHERE  CONTAINS(xCol,'run')   
```  
  
 The word "ran" in the document matches the search condition because of stemming. Additionally, the search context is not checked by using XQuery.  
  
 When XML is decomposed into relational columns by using AXSD that are full-text indexed, XPath queries that occur over the XML view do not perform full-text search on the underlying tables.  
  
## See Also  
 [XML Indexes &#40;SQL Server&#41;](../../relational-databases/xml/xml-indexes-sql-server.md)  
  
  
