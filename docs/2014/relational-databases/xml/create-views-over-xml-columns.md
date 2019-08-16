---
title: "Create Views over XML Columns | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "views [XML in SQL Server]"
ms.assetid: eb5f0439-1f69-49c2-8759-e59bda1633b7
author: MightyPen
ms.author: genemi
manager: craigg
---
# Create Views over XML Columns
  You can use an `xml` type column to create views. The following example creates a view in which the value from an `xml` type column is retrieved using the `value()` method of the `xml` data type.  
  
```  
-- Create the table.  
CREATE TABLE T (  
    ProductID          int primary key,   
    CatalogDescription xml)  
GO  
-- Insert sample data.  
INSERT INTO T values(1,'<ProductDescription ProductID="1" ProductName="SomeName" />')  
GO  
-- Create view (note the value() method used to retrieve ProductName   
-- attribute value from the XML).  
CREATE VIEW MyView AS   
  SELECT ProductID,  
         CatalogDescription.value('(/ProductDescription/@ProductName)[1]', 'varchar(40)') AS PName  
  FROM T  
GO   
```  
  
 Execute the following query against the view:  
  
```  
SELECT *   
FROM   MyView  
```  
  
 This is the result:  
  
```  
ProductID   PName        
----------- ------------  
1           SomeName   
```  
  
 Note the following points about using the `xml` data type to create views:  
  
-   The xml data type can be created in a materialized view. The materialized view cannot be based on an xml data type method. However, it can be cast to an XML schema collection that is different from the xml type column in the base table.  
  
-   The `xml` data type cannot be used in Distributed Partitioned Views.  
  
-   SQL predicates running against the view will not be pushed into the XQuery of the view definition.  
  
-   XML data type methods in a view are not updatable.  
  
  
