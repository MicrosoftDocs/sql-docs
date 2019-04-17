---
title: "Constructor Functions (XQuery) | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "constructor functions [XQuery]"
ms.assetid: 98562d0e-d0e0-4f62-b001-90acbac67277
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Constructor Functions (XQuery)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  From a specified input, the constructor functions create instances of any of the XSD built-in or user-defined atomic types.  
  
## Syntax  
  
```  
  
TYP($atomicvalue as xdt:anyAtomicType?  
  
) as TYP?  
  
```  
  
## Arguments  
 *$strval*  
 String that will be converted.  
  
 *TYP*  
 Any built-in XSD type.  
  
## Remarks  
 Constructors are supported for base and derived atomic XSD types. However, the subtypes of **xs:duration**, which includes **xdt:yearMonthDuration and xdt:dayTimeDuration**, and **xs:QName**, **xs:NMTOKEN**, and **xs:NOTATION** are not supported. User-defined atomic types that are available in the associated schema collections are also available, provided they are directly or indirectly derived from the following types.  
  
#### Supported Base Types  
 These are the supported base types:  
  
-   xs:string  
  
-   xs:boolean  
  
-   xs:decimal  
  
-   xs:float  
  
-   xs:double  
  
-   xs:duration  
  
-   xs:dateTime  
  
-   xs:time  
  
-   xs:date  
  
-   xs:gYearMonth  
  
-   xs:gYear  
  
-   xs:gMonthDay  
  
-   xs:gDay  
  
-   xs:gMonth  
  
-   xs:hexBinary  
  
-   xs:base64Binary  
  
-   xs:anyURI  
  
#### Supported Derived Types  
 These are the supported derived types:  
  
-   xs:normalizedString  
  
-   xs:token  
  
-   xs:language  
  
-   xs:Name  
  
-   xs:NCName  
  
-   xs:ID  
  
-   xs:IDREF  
  
-   xs:ENTITY  
  
-   xs:integer  
  
-   xs:nonPositiveInteger  
  
-   xs:negativeInteger  
  
-   xs:long  
  
-   xs:int  
  
-   xs:short  
  
-   xs:byte  
  
-   xs:nonNegativeInteger  
  
-   xs:unsignedLong  
  
-   xs:unsignedInt  
  
-   xs:unsignedShort  
  
-   xs:unsignedByte  
  
-   xs:positiveInteger  
  
 SQL Server also supports constant folding for construction function invocations in the following ways:  
  
-   If the argument is a string literal, the expression will be evaluated during compilation. When the value does not satisfy the type constraints, a static error is raised.  
  
-   If the argument is a literal of another type, the expression will be evaluated during compilation. When the value does not satisfy the type constraints, the empty sequence is returned.  
  
## Examples  
 This topic provides XQuery examples against XML instances that are stored in various **xml** type columns in the AdventureWorks database.  
  
### A. Using the dateTime() XQuery function to retrieve older product descriptions  
 In this example, a sample XML document is first assigned to an **xml** type variable. This document contains three sample <`ProductDescription`> elements, with each one that contain a <`DateCreated`> child element.  
  
 The variable is then queried to retrieve only those product descriptions that were created before a specific date. For purposes of comparison, the query uses the **xs:dateTime()** constructor function to type the dates.  
  
```  
declare @x xml  
set @x = '<root>  
<ProductDescription ProductID="1" >  
  <DateCreated DateValue="2000-01-01T00:00:00Z" />  
  <Summary>Some Summary description</Summary>  
</ProductDescription>  
<ProductDescription  ProductID="2" >  
  <DateCreated DateValue="2001-01-01T00:00:00Z" />  
  <Summary>Some Summary description</Summary>  
</ProductDescription>  
<ProductDescription ProductID="3" >  
  <DateCreated DateValue="2002-01-01T00:00:00Z" />  
  <Summary>Some Summary description</Summary>  
</ProductDescription>  
</root>'  
  
select @x.query('  
     for $PD in  /root/ProductDescription  
     where xs:dateTime(data( ($PD/DateCreated/@DateValue)[1] )) < xs:dateTime("2001-01-01T00:00:00Z")  
     return  
        element Product  
       {   
        ( attribute ProductID { data($PD/@ProductID ) },  
        attribute DateCreated { data( ($PD/DateCreated/@DateValue)[1] ) } )  
        }  
 ')  
```  
  
 Note the following from the previous query:  
  
-   The FOR ... WHERE loop structure is used to retrieve the \<ProductDescription> element satisfying the condition specified in the WHERE clause.  
  
-   The **dateTime()** constructor function is used to construct **dateTime** type values so they can be compared appropriately.  
  
-   The query then constructs the resulting XML. Because you are constructing a sequence of attributes, commas and parentheses are used in the XML construction.  
  
 This is the result:  
  
```  
<Product   
   ProductID="1"   
   DateCreated="2000-01-01T00:00:00Z"/>  
```  
  
## See Also  
 [XML Construction &#40;XQuery&#41;](../xquery/xml-construction-xquery.md)   
 [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)  
  
  
