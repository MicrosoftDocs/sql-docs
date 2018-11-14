---
title: "XQuery Prolog | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "XQuery, prolog"
  - "prolog"
  - "namespaces [XQuery]"
  - "default namespace declarations"
ms.assetid: 03924684-c5fd-44dc-8d73-c6ab90f5e069
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Modules and Prologs - XQuery Prolog
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  An XQuery query is made up of a prolog and a body. The XQuery prolog is a series of declarations and definitions that together create the required environment for query processing. In SQL Server, the XQuery prolog can include namespace declarations. The XQuery body is made up of a sequence of expressions that specify the intended query result.  
  
 For example, the following XQuery is specified against the Instructions column of **xml** type that stores manufacturing instructions as XML. The query retrieves the manufacturing instructions for the work center location `10`. The `query()` method of the **xml** data type is used to specify the XQuery.  
  
```  
SELECT Instructions.query('declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";           
    /AWMI:root/AWMI:Location[@LocationID=10]  
') AS Result   
FROM  Production.ProductModel  
WHERE ProductModelID=7  
```  
  
 Note the following from the previous query:  
  
-   The XQuery prolog includes a namespace prefix (AWMI) declaration, `(namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";`.  
  
-   The `declare namespace` keyword defines a namespace prefix that is used later in the query body.  
  
-   `/AWMI:root/AWMI:Location[@LocationID="10"]` is the query body.  
  
## Namespace Declarations  
 A namespace declaration defines a prefix and associates it with a namespace URI, as shown in the following query. In the query, `CatalogDescription` is an **xml** type column.  
  
 In specifying XQuery against this column, the query prolog specifies the `declare namespace` declaration to associate the prefix `PD`, product description, with the namespace URI. This prefix is then used in the query body instead of the namespace URI. The nodes in the resulting XML are in the namespace associated with the namespace URI.  
  
```  
SELECT CatalogDescription.query('  
declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
         /PD:ProductDescription/PD:Summary   
    ') as Result  
FROM Production.ProductModel  
where ProductModelID=19  
```  
  
 To improve query readability, you can declare namespaces by using WITH XMLNAMESPACES instead of declaring prefix and namespace binding in the query prolog by using `declare namespace`.  
  
```  
WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS PD)  
  
SELECT CatalogDescription.query('  
         /PD:ProductDescription/PD:Summary   
    ') as Result  
FROM Production.ProductModel  
where ProductModelID=19  
```  
  
 For more information, see, [Add Namespaces to Queries with WITH XMLNAMESPACES](../relational-databases/xml/add-namespaces-to-queries-with-with-xmlnamespaces.md).  
  
### Default Namespace Declaration  
 Instead of declaring a namespace prefix by using the `declare namespace` declaration, you can use the `declare default element namespace` declaration to bind a default namespace for element names. In this case, you do not specify any prefix.  
  
 In the following example, the path expression in the query body does not specify a namespace prefix. By default, all element names belong to the default namespace specified in the prolog.  
  
```  
SELECT CatalogDescription.query('  
     declare default element namespace  "https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
        /ProductDescription/Summary   
    ') as Result  
FROM  Production.ProductModel  
WHERE ProductModelID=19   
```  
  
 You can declare a default namespace by using WITH XMLNAMESPACES:  
  
```  
WITH XMLNAMESPACES (DEFAULT 'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription')  
SELECT CatalogDescription.query('  
        /ProductDescription/Summary   
    ') as Result  
FROM  Production.ProductModel  
WHERE ProductModelID=19   
```  
  
## See Also  
 [Add Namespaces to Queries with WITH XMLNAMESPACES](../relational-databases/xml/add-namespaces-to-queries-with-with-xmlnamespaces.md)  
  
  
