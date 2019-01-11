---
title: "XQuery and Static Typing | Microsoft Docs"
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
  - "XQuery, static typing"
  - "static typing"
  - "checking static types"
  - "inference [XQuery]"
ms.assetid: d599c791-200d-46f8-b758-97e761a1a5c0
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# XQuery and Static Typing
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  XQuery in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is a statically typed language. That is, it raises type errors during query compilation when an expression returns a value that has a type or cardinality that is not accepted by a particular function or operator. Additionally, static type checking can also detect if a path expression on a typed XML document has been mistyped. The XQuery compiler first applies the normalization phase that adds the implicit operations, such as atomization, and then performs static type inference and static type checking.  
  
## Static Type Inference  
 Static type inference determines the return type of an expression. It determines this by taking the static types of the input parameters and the static semantics of the operation and inferring the static type of the result. For example, the static type of the expression 1 + 2.3 is determined in the following way:  
  
-   The static type of 1 is **xs:integer** and the static type of 2.3 is **xs:decimal**. Based on the dynamic semantics, the static semantics of the **+** operation converts the integer to a decimal and then returns a decimal. The inferred static type would then be **xs:decimal**.  
  
 For untyped XML instances, there are special types to indicate that the data was not typed. This information is used during static type checking and to perform certain implicit casts).  
  
 For typed data, the input type is inferred from the XML schema collection that constrains the XML data type instance. For example, if the schema allows only elements of type **xs:integer**, the results of a path expression using that element will be zero or more elements of type **xs:integer**. This is currently expressed by using an expression such as `element(age,xs:integer)*` where the asterisk (\*) indicates the cardinality of the resulting type. In this example, the expression may result in zero or more elements of name "age" and type **xs:integer**. Other cardinalities are exactly one and are expressed by using the type name alone, zero or one and expressed by using a question mark (**?**), and 1 or more and expressed by using a plus sign (**+**).  
  
 Sometimes, the static type inference can infer that an expression will always return the empty sequence. For example, if a path expression on a typed XML data type looks for a \<name> element inside a \<customer> element (/customer/name), but the schema does not allow a \<name> inside a \<customer>, the static type inference will infer that the result will be empty. This will be used to detect incorrect queries and will be reported as a static error, unless the expression was () or **data( () )**.  
  
 The detailed inference rules are provided in the formal semantics of the XQuery specification. Microsoft has modified these only slightly to work with typed XML data type instances. The most important change from the standard is that the implicit document node knows about the type of the XML data type instance. As a result, a path expression of the form /age will be precisely typed based on that information.  
  
 By using [SQL Server Profiler Templates and Permissions](../tools/sql-server-profiler/sql-server-profiler-templates-and-permissions.md), you can see the static types returned as part of query compilations. To see these, your trace must include the XQuery Static Type event in the TSQL event category.  
  
## Static Type Checking  
 Static type checking ensures that the run-time execution will only receive values that are the appropriate type for the operation. Because the types do not have to be checked at run time, potential errors can be detected early in the compilation. This helps improve performance. However, static typing requires that the query writer be more careful in formulating a query.  
  
 Following are the appropriate types that can be used:  
  
-   Types explicitly allowed by a function or operation.  
  
-   A subtype of an explicitly allowed type.  
  
 Subtypes are defined, based on the subtyping rules for using derivation by restriction or extension of the XML schema. For example, a type S is a subtype of type T, if all the values that have the type S are also instances of the type T.  
  
 Additionally, all integer values are also decimal values, based on the XML schema type hierarchy. However, not all decimal values are integers. Therefore, an integer is a subtype of decimal, but not vice versa. For example, the **+** operation only allows values of certain types, such as the numeric types **xs:integer**, **xs:decimal**, **xs:float**, and **xs:double**. If values of other types, such as **xs:string**, are passed, the operation raises a type error. This is referred to as strong typing. Values of other types, such as the atomic type used to indicate untyped XML, can be implicitly converted to a value of a type that the operation accepts. This is referred to as weak typing.  
  
 If it is required after an implicit conversion, static type checking guarantees that only values of the allowed types with the correct cardinality are passed to an operation. For "string" + 1, it recognizes that the static type of "string" is **xs:string**. Because this is not an allowed type for the **+** operation, a type error is raised.  
  
 In the case of adding the result of an arbitrary expression E1 to an arbitrary expression E2 (E1 + E2), static type inference first determines the static types of E1 and E2 and then checks their static types with the allowed types for the operation. For example, if the static type of E1 can be either an **xs:string** or an **xs:integer**, the static type check raises a type error, even though some values at run time might be integers. The same would be the case if the static type of E1 were **xs:integer&#42;**. Because the **+** operation only accepts exactly one integer value and E1 could return zero or more than 1, the static type check raises an error.  
  
 As mentioned earlier, type inference frequently infers a type that is broader than what the user knows about the type of the data that is being passed. In these cases, the user has to rewrite the query. Some typical cases include the following:  
  
-   The type infers a more general type such as a supertype or a union of types. If the type is an atomic type, you should use the cast expression or constructor function to indicate the actual static type. For example, if the inferred type of the expression E1 is a choice between **xs:string** or **xs:integer** and the addition requires **xs:integer**, you should write `xs:integer(E1) + E2` instead of `E1+E2`. This expression may fail at run time if a string value is encountered that cannot be cast to **xs:integer**. However, the expression will now pass the static type check. This expression is mapped to the empty sequence.  
  
-   The type infers a higher cardinality than what the data actually contains. This occurs frequently, because the **xml** data type can contain more than one top-level element, and an XML schema collection cannot constrain this. In order to reduce the static type and guarantee that there is indeed at most one value being passed, you should use the positional predicate `[1]`. For example, to add 1 to the value of the attribute `c` of the element `b` under the top-level a element, you must `write (/a/b/@c)[1]+1`. Additionally, the DOCUMENT keyword can be used together with an XML schema collection.  
  
-   Some operations lose type information during inference. For example, if the type of a node cannot be determined, it becomes **anyType**. This is not implicitly cast to any other type. These conversions occur most notably during navigation by using the parent axis. You should avoid using such operations and rewrite the query, if the expression will create a static type error.  
  
## Type Checking of Union Types  
 Union types require careful handling because of type checking. Two of the problems are illustrated in the following examples.  
  
### Example: Function over Union Type  
 Consider an element definition for <`r`> of a union type:  
  
```  
<xs:element name="r">  
<xs:simpleType>  
   <xs:union memberTypes="xs:int xs:float xs:double"/>  
</xs:simpleType>  
</xs:element>  
```  
  
 Within XQuery context, the "average" function `fn:avg (//r)` returns a static error, because the XQuery compiler cannot add values of different types (**xs:int**, **xs:float** or **xs:double**) for the <`r`> elements in the argument of **fn:avg()**. To  solve this, rewrite the function invocation as `fn:avg(for $r in //r return $r cast as xs:double ?)`.  
  
### Example: Operator over Union Type  
 The addition operation ('+') requires precise types of the operands. As a result, the expression `(//r)[1] + 1` returns a static error that has the previously described type definition for element <`r`>. One solution is to rewrite it as `(//r)[1] cast as xs:int? +1`, where the "?" indicates 0 or 1 occurrences. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] requires "cast as" with "?", because any cast can cause the empty sequence as a result of run-time errors.  
  
## See Also  
 [XQuery Language Reference &#40;SQL Server&#41;](../xquery/xquery-language-reference-sql-server.md)  
  
  
