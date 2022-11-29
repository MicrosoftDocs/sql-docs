---
title: "Comparison Expressions (XQuery) | Microsoft Docs"
description: Learn how to use XQuery comparison expressions that contain general, value, node, and node order comparison operators.
ms.custom: ""
ms.date: "08/09/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "node comparison operators [XQuery]"
  - "comparison expressions [XQuery]"
  - "node order comparison operators [XQuery]"
  - "expressions [XQuery], comparison"
  - "comparison operators [XQuery]"
  - "value comparison operators"
ms.assetid: dc671348-306f-48ef-9e6e-81fc3c7260a6
author: "rothja"
ms.author: "jroth"
---
# Comparison Expressions (XQuery)
[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]

  XQuery provides the following types of comparison operators:  
  
-   General comparison operators  
  
-   Value comparison operators  
  
-   Node comparison operators  
  
-   Node order comparison operators  
  
## General Comparison Operators  
 General comparison operators can be used to compare atomic values, sequences, or any combination of the two.  
  
 The general operators are defined in the following table.  
  
|Operator|Description|  
|--------------|-----------------|  
|=|Equal|  
|!=|Not equal|  
|\<|Less than|  
|>|Greater than|  
|\<=|Less than or equal to|  
|>=|Greater than or equal to|  
  
 When you are comparing two sequences by using general comparison operators and a value exists in the second sequence that compares True to a value in the first sequence, the overall result is True. Otherwise, it is False. For example, (1, 2, 3) = (3, 4) is True, because the value 3 appears in both sequences.  
  
```  
declare @x xml  
set @x=''  
select @x.query('(1,2,3) = (3,4)')    
```  
  
 The comparison expects that the values are of comparable types. Specifically, they are statically checked. For numeric comparisons, numeric type promotion can occur. For example, if a decimal value of 10 is compared to a double value 1e1, the decimal value is changed to double. Note that this can create inexact results, because double comparisons cannot be exact.  
  
 If one of the values is untyped, it is cast to the other value's type. In the following example, value 7 is treated as an integer. Before being compared, the untyped value of /a[1] is converted to an integer. The integer comparison returns True.  
  
```  
declare @x xml  
set @x='<a>6</a>'  
select @x.query('/a[1] < 7')  
```  
  
 Conversely, if the untyped value is compared to a string or another untyped value, it will be cast to xs:string. In the following query, string 6 is compared to string "17". The following query returns False, because of the string comparison.  
  
```  
declare @x xml  
set @x='<a>6</a>'  
select @x.query('/a[1] < "17"')  
```  
  
 The following query returns small-size pictures of a product model from the product catalog provided in the AdventureWorks sample database. The query compares a sequence of atomic values returned by `PD:ProductDescription/PD:Picture/PD:Size` with a singleton sequence, "small". If the comparison is True, it returns the <Picture\> element.  
  
```  
WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS PD)  
SELECT CatalogDescription.query('         
    for $P in /PD:ProductDescription/PD:Picture[PD:Size = "small"]         
    return $P') as Result         
FROM   Production.ProductModel         
WHERE  ProductModelID=19         
```  
  
 The following query compares a sequence of telephone numbers in <number\> elements to the string literal "112-111-1111". The query compares the sequence of telephone number elements in the AdditionalContactInfo column to determine if a specific telephone number for a specific customer exists in the document.  
  
```  
WITH XMLNAMESPACES (  
  'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes' AS act,  
  'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactInfo' AS aci)  
  
SELECT AdditionalContactInfo.value('         
   /aci:AdditionalContactInfo//act:telephoneNumber/act:number = "112-111-1111"', 'nvarchar(10)') as Result         
FROM Person.Contact         
WHERE ContactID=1         
```  
  
 The query returns True. This indicates that the number exists in the document. The following query is a slightly modified version of the previous query. In this query, the telephone number values retrieved from the document are compared to a sequence of two telephone number values. If the comparison is True, the <number\> element is returned.  
  
```  
WITH XMLNAMESPACES (  
  'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes' AS act,  
  'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactInfo' AS aci)  
  
SELECT AdditionalContactInfo.query('         
  if (/aci:AdditionalContactInfo//act:telephoneNumber/act:number = ("222-222-2222","112-111-1111"))         
  then          
     /aci:AdditionalContactInfo//act:telephoneNumber/act:number         
  else         
    ()') as Result         
FROM Person.Contact         
WHERE ContactID=1  
  
```  
  
 This is the result:  
  
```  
\<act:number   
  xmlns:act="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes">  
    111-111-1111  
\</act:number>  
\<act:number   
  xmlns:act="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes">  
    112-111-1111  
\</act:number>   
```  
  
## Value Comparison Operators  
 Value comparison operators are used to compare atomic values. Note that you can use general comparison operators instead of value comparison operators in your queries.  
  
 The value comparison operators are defined in the following table.  
  
|Operator|Description|  
|--------------|-----------------|  
|eq|Equal|  
|ne|Not equal|  
|lt|Less than|  
|gt|Greater than|  
|le|Less than or equal to|  
|ge|Greater than or equal to|  
  
 If the two values compare the same according to the chosen operator, the expression will return True. Otherwise, it will return False. If either value is an empty sequence, the result of the expression is False.  
  
 These operators work on singleton atomic values only. That is, you cannot specify a sequence as one of the operands.  
  
 For example, the following query retrieves \<Picture> elements for a product model where the picture size is "small:  
  
```  
SELECT CatalogDescription.query('         
              declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";         
              for $P in /PD:ProductDescription/PD:Picture[PD:Size eq "small"]         
              return         
                    $P         
             ') as Result         
FROM Production.ProductModel         
WHERE ProductModelID=19         
```  
  
 Note the following from the previous query:  
  
-   `declare namespace` defines the namespace prefix that is subsequently used in the query.  
  
-   The \<Size> element value is compared with the specified atomic value, "small".  
  
-   Note that because the value operators work only on atomic values, the **data()** function is implicitly used to retrieve the node value. That is, `data($P/PD:Size) eq "small"` produces the same result.  
  
 This is the result:  
  
```  
\<PD:Picture   
  xmlns:PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription">  
  \<PD:Angle>front\</PD:Angle>  
  \<PD:Size>small\</PD:Size>  
  \<PD:ProductPhotoID>31\</PD:ProductPhotoID>  
\</PD:Picture>  
```  
  
 Note that the type promotion rules for value comparisons are the same as for general comparisons. Also, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] uses the same casting rules for untyped values during value comparisons as it uses during general comparisons. In contrast, the rules in the XQuery specification always cast the untyped value to xs:string during value comparisons.  
  
## Node Comparison Operator  
 The node comparison operator, **is**, applies only to node types. The result it returns indicates whether two nodes passed in as operands represent the same node in the source document. This operator returns True if the two operands are the same node. Otherwise, it returns False.  
  
 The following query checks whether the work center location 10 is the first in the manufacturing process of a specific product model.  
  
```  
WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions' AS AWMI)  
  
SELECT ProductModelID, Instructions.query('         
    if (  (//AWMI:root/AWMI:Location[@LocationID=10])[1]         
          is          
          (//AWMI:root/AWMI:Location[1])[1] )          
    then         
          <Result>equal</Result>         
    else         
          <Result>Not-equal</Result>         
         ') as Result         
FROM Production.ProductModel         
WHERE ProductModelID=7           
```  
  
 This is the result:  
  
```  
ProductModelID       Result          
-------------- --------------------------  
7              <Result>equal</Result>      
```  
  
## Node Order Comparison Operators  
 Node order comparison operators compare pairs of nodes, based on their positions in a document.  
  
 These are the comparisons that are made, based on document order:  
  
-   `<<` : Does **operand 1** precede **operand 2** in the document order.  
  
-   `>>` : Does **operand 1** follow **operand 2** in the document order.  
  
 The following query returns True if the product catalog description has the \<Warranty> element appearing before the \<Maintenance> element in the document order for a particular product.  
  
```  
WITH XMLNAMESPACES (  
  'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS PD,  
  'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain' AS WM)  
  
SELECT CatalogDescription.value('  
     (/PD:ProductDescription/PD:Features/WM:Warranty)[1] <<   
           (/PD:ProductDescription/PD:Features/WM:Maintenance)[1]', 'nvarchar(10)') as Result  
FROM  Production.ProductModel  
where ProductModelID=19  
```  
  
 Note the following from the previous query:  
  
-   The **value()** method of the **xml**data type is used in the query.  
  
-   The Boolean result of the query is converted to **nvarchar(10)** and returned.  
  
-   The query returns True.  
  
## See Also  
 [Type System &#40;XQuery&#41;](../xquery/type-system-xquery.md)   
 [XQuery Expressions](../xquery/xquery-expressions.md)  
  
  
