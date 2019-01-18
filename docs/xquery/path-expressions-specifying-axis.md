---
title: "Specifying Axis in a Path Expression Step | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "attribute axis [SQL Server]"
  - "axis step [XQuery]"
  - "descendant axis"
  - "self axis"
  - "path expressions [XQuery]"
  - "child axis"
  - "descendant-or-self axis"
  - "parent axis"
ms.assetid: c44fb843-0626-4496-bde0-52ca0bac0a9e
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Path Expressions - Specifying Axis
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  An axis step in a path expression includes the following components:  
  
-   An axis  
  
-   A [node test](../xquery/path-expressions-specifying-node-test.md)  
  
-   [Zero or more step qualifiers (optional)](../xquery/path-expressions-specifying-predicates.md)  
  
 For more information, see [Path Expressions &#40;XQuery&#41;](../xquery/path-expressions-xquery.md).  
  
 The XQuery implementation in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] supports the following axis steps,  
  
|Axis|Description|  
|----------|-----------------|  
|**child**|Returns children of the context node.|  
|**descendant**|Returns all descendants of the context node.|  
|**parent**|Returns the parent of the context node.|  
|**attribute**|Returns attributes of the context node.|  
|**self**|Returns the context node itself.|  
|**descendant-or-self**|Returns the context node and all descendants of the context node.|  
  
 All these axes, except the **parent** axis, are forward axes. The **parent** axis is a reverse axis, because it searches backward in the document hierarchy. For example, the relative path expression `child::ProductDescription/child::Summary` has two steps, and each step specifies a `child` axis. The first step retrieves the \<ProductDescription> element children of the context node. For each \<ProductDescription> element node, the second step retrieves the \<Summary> element node children.  
  
 The relative path expression, `child::root/child::Location/attribute::LocationID`, has three steps. The first two steps each specify a `child` axis, and the third step specifies the `attribute` axis. When executed against the manufacturing instructions XML documents in the **Production.ProductModel** table, the expression returns the `LocationID` attribute of the \<Location> element node child of the \<root> element.  
  
## Examples  
 The query examples in this topic are specified against **xml** type columns in the **AdventureWorks** database.  
  
### A. Specifying a child axis  
 For a specific product model, the following query retrieves the \<Features> element node children of the \<ProductDescription> element node from the product catalog description stored in the `Production.ProductModel` table.  
  
```  
SELECT CatalogDescription.query('  
declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
  /child::PD:ProductDescription/child::PD:Features')  
FROM Production.ProductModel  
WHERE ProductModelID=19  
```  
  
 Note the following from the previous query:  
  
-   The `query()` method of the **xml** data type specifies the path expression.  
  
-   Both steps in the path expression specify a `child` axis and the node names, `ProductDescription` and `Features`, as node tests. For information about node tests, see [Specifying Node Test in a Path Expression Step](../xquery/path-expressions-specifying-node-test.md).  
  
### B. Specifying descendant and descendant-or-self axes  
 The following example uses descendant and descendant-or-self axes. The query in this example is specified against an **xml** type variable. The XML instance is simplified in order to easily illustrate the difference in the generated results.  
  
```  
declare @x xml  
set @x='  
<a>  
 <b>text1  
   <c>text2  
     <d>text3</d>  
   </c>  
 </b>  
</a>'  
declare @y xml  
set @y = @x.query('  
  /child::a/child::b  
')  
select @y  
```  
  
 In the following result, the expression returns the `<b>` element node child of the `<a>` element node:  
  
```  
<b>text1  
   <c>text2  
     <d>text3</d>  
   </c>  
</b>  
```  
  
 In this expression, if you specify a descendant axis for the path expression,  
  
 `/child::a/child::b/descendant::*`, you are asking for all descendants of the <`b`> element node.  
  
 The asterisk (*) in the node test represents the node name as a node test. Therefore, the primary node type of the descendant axis, the element node, determines the types of nodes returned. That is, the expression returns all the element nodes.. Text nodes are not returned. For more information about the primary node type and its relationship with the node test, see [Specifying Node Test in a Path Expression Step](../xquery/path-expressions-specifying-node-test.md) topic.  
  
 The element nodes <`c`> and <`d`> are returned, as shown in the following result:  
  
```  
<c>text2  
     <d>text3</d>  
</c>  
<d>text3</d>  
```  
  
 If you specify a descendant-or-self axis instead of the descendant axis, `/child::a/child::b/descendant-or-self::*` returns the context node, element <`b`>, and its descendant.  
  
 This is the result:  
  
```  
<b>text1  
   <c>text2  
     <d>text3</d>  
   </c>  
</b>  
  
<c>text2  
     <d>text3</d>  
</c>  
  
<d>text3</d>   
```  
  
 The following sample query against the **AdventureWorks** database retrieves all the descendant element nodes of the <`Features`> element child of the <`ProductDescription`> element:  
  
```  
SELECT CatalogDescription.query('  
declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
  /child::PD:ProductDescription/child::PD:Features/descendant::*  
')  
FROM  Production.ProductModel  
WHERE ProductModelID=19  
```  
  
### C. Specifying a parent axis  
 The following query returns the <`Summary`> element child of the <`ProductDescription`> element in the product catalog XML document stored in the `Production.ProductModel` table.  
  
 This example uses the parent axis to return to the parent of the <`Feature`> element and retrieve the <`Summary`> element child of the <`ProductDescription`> element.  
  
```  
SELECT CatalogDescription.query('  
declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
  
/child::PD:ProductDescription/child::PD:Features/parent::PD:ProductDescription/child::PD:Summary  
')  
FROM   Production.ProductModel  
WHERE  ProductModelID=19  
  
```  
  
 In this query example, the path expression uses the `parent` axis. You can rewrite the expression without the parent axis, as shown in the following:  
  
```  
/child::PD:ProductDescription[child::PD:Features]/child::PD:Summary  
```  
  
 A more useful example of the parent axis is provided in the following example.  
  
 Each product model catalog description stored in the **CatalogDescription** column of the **ProductModel** table has a `<ProductDescription>` element that has the `ProductModelID` attribute and `<Features>` child element, as shown in the following fragment:  
  
```  
<ProductDescription ProductModelID="..." >  
  ...  
  <Features>  
    <Feature1>...</Feature1>  
    <Feature2>...</Feature2>  
   ...  
</ProductDescription>  
```  
  
 The query sets an iterator variable, `$f`, in the FLWOR statement to return the element children of the `<Features>` element. For more information, see [FLWOR Statement and Iteration &#40;XQuery&#41;](../xquery/flwor-statement-and-iteration-xquery.md). For each feature, the `return` clause constructs an XML in the following form:  
  
```  
<Feature ProductModelID="...">...</Feature>  
<Feature ProductModelID="...">...</Feature>  
```  
  
 To add the `ProductModelID` for each `<Feature`> element, the `parent` axis is specified:  
  
```  
SELECT CatalogDescription.query('  
declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
declare namespace wm="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain";  
  for $f in /child::PD:ProductDescription/child::PD:Features/child::*  
  return  
   <Feature  
     ProductModelID="{ ($f/parent::PD:Features/parent::PD:ProductDescription/attribute::ProductModelID)[1]}" >  
          { $f }  
   </Feature>  
')  
FROM  Production.ProductModel  
WHERE ProductModelID=19  
```  
  
 This is the partial result:  
  
```  
<Feature ProductModelID="19">  
  <wm:Warranty   
   xmlns:wm="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain">  
    <wm:WarrantyPeriod>3 years</wm:WarrantyPeriod>  
    <wm:Description>parts and labor</wm:Description>  
  </wm:Warranty>  
</Feature>  
<Feature ProductModelID="19">  
  <wm:Maintenance   
   xmlns:wm="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain">  
    <wm:NoOfYears>10 years</wm:NoOfYears>  
    <wm:Description>maintenance contract available through your dealer   
                  or any AdventureWorks retail store.</wm:Description>  
  </wm:Maintenance>  
</Feature>  
<Feature ProductModelID="19">  
  <p1:wheel   
   xmlns:p1="https://www.adventure-works.com/schemas/OtherFeatures">  
      High performance wheels.  
  </p1:wheel>  
</Feature>  
```  
  
 Note that the predicate `[1]` in the path expression is added to ensure that a singleton value is returned.  
  
  
