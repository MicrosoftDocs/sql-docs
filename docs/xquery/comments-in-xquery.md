---
title: "Comments in XQuery"
description: Learn the syntax and delimiters for adding comments to an XQuery.
author: "rothja"
ms.author: "jroth"
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "language-reference"
helpviewer_keywords:
  - "comments [XQuery]"
  - "XQuery, comments"
dev_langs:
  - "XML"
---
# Comments in XQuery
[!INCLUDE [SQL Server Azure SQL Database](../includes/applies-to-version/sqlserver.md)]

  You can add comments to XQuery. The comment strings are added by using the "`(:`" and "`:)`" delimiters. For example:  
  
```  
declare @x xml  
set @x=''  
SELECT @x.query('  
(: simple query to construct an element :)  
<ProductModel ProductModelID="10" />  
')  
```  
  
 Following is another example in which a query is specified against an Instruction column of the **xml** type:  
  
```  
SELECT Instructions.query('  
(: declare prefix and namespace binding in the prolog. :)  
     declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
  (: Following expression retrieves the <Location> element children of the <root> element. :)  
  /AWMI:root/AWMI:Location  
') as Result  
FROM Production.ProductModel  
where ProductModelID=7  
```  
  
  
