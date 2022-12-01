---
title: "Sequence Expressions (XQuery) | Microsoft Docs"
description: Learn about XQuery sequence expressions that construct, filter, and combine a sequence of items.
ms.custom: ""
ms.date: "08/09/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "sequence [XQuery]"
  - "expressions [XQuery], sequence"
  - "filtering sequences [XQuery]"
ms.assetid: 41e18b20-526b-45d2-9bd9-e3b7d7fbce4e
author: "rothja"
ms.author: "jroth"
---
# Sequence Expressions (XQuery)
[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]

  [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] supports the XQuery operators that are used to construct, filter, and combine a sequence of items. An item can be an atomic value or a node.  
  
## Constructing Sequences  
 You can use the comma operator to construct a sequence that concatenates items into a single sequence.  
  
 A sequence can contain duplicate values. Nested sequences, a sequence within a sequence, are collapsed. For example, the sequence (1, 2, (3, 4, (5))) becomes (1, 2, 3, 4, 5). These are examples of constructing sequences.  
  
### Example A  
 The following query returns a sequence of five atomic values:  
  
```  
declare @x xml  
set @x=''  
select @x.query('(1,2,3,4,5)')  
go  
-- result 1 2 3 4 5  
```  
  
 The following query returns a sequence of two nodes:  
  
```  
-- sequence of 2 nodes  
declare @x xml  
set @x=''  
select @x.query('(<a/>, <b/>)')  
go  
-- result  
<a />  
<b />  
```  
  
 The following query returns an error, because you are constructing a sequence of atomic values and nodes. This is a heterogeneous sequence and is not supported.  
  
```  
declare @x xml  
set @x=''  
select @x.query('(1, 2, <a/>, <b/>)')  
go  
```  
  
### Example B  
 The following query constructs a sequence of atomic values by combining four sequences of different length into a single sequence.  
  
```  
declare @x xml  
set @x=''  
select @x.query('(1,2),10,(),(4, 5, 6)')  
go  
-- result = 1 2 10 4 5 6  
```  
  
 You can sort the sequence by using FLOWR and ORDER BY:  
  
```  
declare @x xml  
set @x=''  
select @x.query('for $i in ((1,2),10,(),(4, 5, 6))  
                  order by $i  
                  return $i')  
go  
```  
  
 You can count the items in the sequence by using the **fn:count()** function.  
  
```  
declare @x xml  
set @x=''  
select @x.query('count( (1,2,3,(),4) )')  
go  
-- result = 4  
```  
  
### Example C  
 The following query is specified against the AdditionalContactInfo column of the **xml** type in the Contact table. This column stores additional contact information, such as one or more additional telephone numbers, pager numbers, and addresses. The \<telephoneNumber>, \<pager>, and other nodes can appear anywhere in the document. The query constructs a sequence that contains all the \<telephoneNumber> children of the context node, followed by the \<pager> children. Note the use of the comma sequence operator in the return expression, `($a//act:telephoneNumber, $a//act:pager)`.  
  
```  
WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes' AS act,  
 'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactInfo' AS aci)  
  
SELECT AdditionalContactInfo.query('  
   for $a in /aci:AdditionalContactInfo   
   return ($a//act:telephoneNumber, $a//act:pager)  
') As Result  
FROM Person.Contact  
WHERE ContactID=3  
```  
  
 This is the result:  
  
```  
<act:telephoneNumber xmlns:act="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes">  
  <act:number>333-333-3333</act:number>  
</act:telephoneNumber>  
<act:telephoneNumber xmlns:act="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes">  
  <act:number>333-333-3334</act:number>  
</act:telephoneNumber>  
<act:pager xmlns:act="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes">  
  <act:number>999-555-1244</act:number>  
  <act:SpecialInstructions>  
Page only in case of emergencies.  
</act:SpecialInstructions>  
</act:pager>  
```  
  
## Filtering Sequences  
 You can filter the sequence returned by an expression by adding a predicate to the expression. For more information, see [Path Expressions &#40;XQuery&#41;](../xquery/path-expressions-xquery.md). For example, the following query returns a sequence of three <`a`> element nodes:  
  
```  
declare @x xml  
set @x = '<root>  
<a attrA="1">111</a>  
<a></a>  
<a></a>  
</root>'  
SELECT @x.query('/root/a')  
```  
  
 This is the result:  
  
```  
<a attrA="1">111</a>  
<a />  
<a />  
```  
  
 To retrieve only <`a`> elements that have the attribute attrA, you can specify a filter in the predicate. The resulting sequence will have only one <`a`> element.  
  
```  
declare @x xml  
set @x = '<root>  
<a attrA="1">111</a>  
<a></a>  
<a></a>  
</root>'  
SELECT @x.query('/root/a[@attrA]')  
```  
  
 This is the result:  
  
```  
<a attrA="1">111</a>  
```  
  
 For more information about how to specify predicates in a path expression, see [Specifying Predicates in a Path Expression Step](../xquery/path-expressions-specifying-predicates.md).  
  
 The following example builds a sequence expression of subtrees and then applies a filter to the sequence.  
  
```  
declare @x xml  
set @x = '  
<a>  
  <c>C under a</c>  
</a>  
<b>    
   <c>C under b</c>  
</b>  
<c>top level c</c>  
<d></d>  
'  
```  
  
 The expression in `(/a, /b)` constructs a sequence with subtrees `/a` and `/b` and from the resulting sequence the expression filters element `<c>`.  
  
```  
SELECT @x.query('  
  (/a, /b)/c  
')  
```  
  
 This is the result:  
  
```  
<c>C under a</c>  
<c>C under b</c>  
```  
  
 The following example applies a predicate filter. The expression finds elements <`a`> and <`b`> that contain element <`c`>.  
  
```  
declare @x xml  
set @x = '  
<a>  
  <c>C under a</c>  
</a>  
<b>    
   <c>C under b</c>  
</b>  
  
<c>top level c</c>  
<d></d>  
'  
SELECT @x.query('  
  (/a, /b)[c]  
')  
```  
  
 This is the result:  
  
```  
<a>  
  <c>C under a</c>  
</a>  
<b>  
  <c>C under b</c>  
</b>  
```  
  
### Implementation Limitations  
 These are the limitations:  
  
-   XQuery range expression is not supported.  
  
-   Sequences must be homogeneous. Specifically, all items in a sequence must be either nodes or atomic values. This is statically checked.  
  
-   Combining node sequences by using the union, intersect, or except operator is not supported.  
  
## See Also  
 [XQuery Expressions](../xquery/xquery-expressions.md)  
  
  
