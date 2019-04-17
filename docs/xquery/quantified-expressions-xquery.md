---
title: "Quantified Expressions (XQuery) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "universal quantifiers"
  - "effective Boolean value [XQuery]"
  - "quantified expressions [XQuery]"
  - "XQuery, quantified expressions"
  - "every expression [XQuery]"
  - "existential quantifiers [XQuery]"
  - "some expression [XQuery]"
  - "EBV"
  - "expressions [XQuery], quantifiers"
ms.assetid: a3a75a6c-8f67-4923-8406-1ada546c817f
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Quantified Expressions (XQuery)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Existential and universal quantifiers specify different semantics for Boolean operators that are applied to two sequences. This is shown in the following table.  
  
 *Existential quantifier*  
 Given two sequences, if any item in the first sequence has a match in the second sequence, based on the comparison operator that is used, the returned value is True.  
  
 *Universal quantifier*  
 Given two sequences, if every item in the first sequence has a match in the second sequence, the returned value is True.  
  
 XQuery supports quantified expressions in the following form:  
  
```  
( some | every ) <variable> in <Expression> (,...) satisfies <Expression>  
```  
  
 You can use these expressions in a query to explicitly apply either existential or universal quantification to an expression over one or several sequences. In [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], the expression in the `satisfies` clause has to result in one of the following: a node sequence, an empty sequence, or a Boolean value. The effective Boolean value of the result of that expression will be used in the quantification. The existential quantification that uses **some** will return True if at least one of the values bound by the quantifier has a True result in the satisfy expression. The universal quantification that uses **every** must have True for all values bound by the quantifier.  
  
 For example, the following query checks every \<Location> element to see whether it has a LocationID attribute.  
  
```  
SELECT Instructions.query('  
     declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
        if (every $WC in //AWMI:root/AWMI:Location   
            satisfies $WC/@LocationID)  
        then  
             <Result>All work centers have workcenterLocation ID</Result>  
         else  
             <Result>Not all work centers have workcenterLocation ID</Result>  
') as Result  
FROM Production.ProductModel  
where ProductModelID=7  
```  
  
 Because LocationID is a required attribute of the \<Location> element, you receive the expected result:  
  
```  
<Result>All work centers have Location ID</Result>   
```  
  
 Instead of using the [query() method](../t-sql/xml/query-method-xml-data-type.md), you can use the [value() method](../t-sql/xml/value-method-xml-data-type.md) to return the result to the relational world, as shown in the following query. The query returns True if all work center locations have LocationID attributes. Otherwise, the query returns False.  
  
```  
SELECT Instructions.value('  
     declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
        every $WC in  //AWMI:root/AWMI:Location   
            satisfies $WC/@LocationID',   
  'nvarchar(10)') as Result  
FROM Production.ProductModel  
where ProductModelID=7  
```  
  
 The following query checks to see if one of the product pictures is small. In the product catalog XML, various angles are stored for each product picture of a different size. You might want to ensure that each product catalog XML includes at least one small-sized picture. The following query accomplishes this:  
  
```  
SELECT ProductModelID, CatalogDescription.value('  
     declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
     some $F in /PD:ProductDescription/PD:Picture  
        satisfies $F/PD:Size="small"', 'nvarchar(20)') as SmallPicturesStored  
FROM Production.ProductModel  
WHERE ProductModelID = 19  
```  
  
 This is a partial result:  
  
```  
ProductModelID SmallPicturesStored   
-------------- --------------------  
19             true        
```  
  
## Implementation Limitations  
 These are the limitations:  
  
-   Type assertion is not supported as part of binding the variable in the quantified expressions.  
  
## See Also  
 [XQuery Expressions](../xquery/xquery-expressions.md)  
  
  
