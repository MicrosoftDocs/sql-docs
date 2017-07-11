---
title: "Example: Specifying the ELEMENT Directive and Entity Encoding | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-xml"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "ELEMENT directive"
  - "entity encoding [XML]"
ms.assetid: 50cda5c1-7293-4080-93b3-872e3b8d484e
caps.latest.revision: 12
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Example: Specifying the ELEMENT Directive and Entity Encoding
  This example illustrates the difference between the **ELEMENT** and **XML** directives. The **ELEMENT** directive entitizes the data, but the **XML** directive does not. The \<Summary> element is assigned XML, `<Summary>This is summary description</Summary>`, in the query.  
  
 Consider this query:  
  
```  
USE AdventureWorks2012;  
GO  
SELECT  1 as Tag,  
        0 as Parent,  
        ProductModelID  as [ProductModel!1!ProdModelID],  
        Name            as [ProductModel!1!Name],  
        NULL            as [Summary!2!SummaryDescription!ELEMENT]  
FROM    Production.ProductModel  
WHERE   ProductModelID=19  
UNION ALL  
SELECT  2 as Tag,  
        1 as Parent,  
        ProductModelID,  
        NULL,  
       '<Summary>This is summary description</Summary>'  
FROM   Production.ProductModel  
WHERE  ProductModelID=19  
FOR XML EXPLICIT  
```  
  
 This is the result. The summary description is entitized in the result.  
  
```  
<ProductModel ProdModelID="19" Name="Mountain-100">  
  <Summary>  
    <SummaryDescription><Summary>This is summary description</Summary></SummaryDescription>  
  </Summary>  
</ProductModel>  
```  
  
 Now, if you specify the **XML** directive in the column name, `Summary!2!SummaryDescription!XML`, instead of the **ELEMENT** directive, you will receive the summary description without entitization.  
  
```  
<ProductModel ProdModelID="19" Name="Mountain-100">  
  <Summary>  
    <SummaryDescription>  
      <Summary>This is summary description</Summary>  
    </SummaryDescription>  
  </Summary>  
</ProductModel>  
```  
  
 Instead of assigning a static XML value, the following query uses the **query()** method of the **xml** type to retrieve the product model summary description from the CatalogDescription column of **xml** type. Because the result is known to be of **xml** type, no entitization is applied.  
  
```  
SELECT  1 as Tag,  
        0 as Parent,  
        ProductModelID  as [ProductModel!1!ProdModelID],  
        Name            as [ProductModel!1!Name],  
        NULL            as [Summary!2!SummaryDescription]  
FROM    Production.ProductModel  
WHERE   CatalogDescription is not null  
UNION ALL  
SELECT  2 as Tag,  
        1 as Parent,  
        ProductModelID,  
        Name,  
       (SELECT CatalogDescription.query('  
            declare namespace pd="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
          /pd:ProductDescription/pd:Summary'))  
FROM     Production.ProductModel  
WHERE    CatalogDescription is not null  
ORDER BY [ProductModel!1!ProdModelID],Tag  
FOR XML EXPLICIT  
```  
  
## See Also  
 [Use EXPLICIT Mode with FOR XML](../../relational-databases/xml/use-explicit-mode-with-for-xml.md)  
  
  