---
title: "XQuery Operators Against the xml Data Type | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "XQuery, operators"
  - "operators [XQuery]"
  - "xml data type [SQL Server], XQuery"
ms.assetid: 39ca3d2e-e928-4333-872b-75c4ccde8e79
author: rothja
ms.author: jroth
manager: craigg
---
# XQuery Operators Against the xml Data Type
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  XQuery supports the following operators:  
  
-   Numeric operators (+, -, *, div, mod)  
  
-   Operators for value comparison (eq, ne, lt, gt, le, ge)  
  
-   Operators for general comparison ( =, !=, \<, >, \<=, >= )  
  
 For more information about these operators, see [Comparison Expressions &#40;XQuery&#41;](../xquery/comparison-expressions-xquery.md)  
  
## Examples  
  
### A. Using general operators  
 The query illustrates the use of general operators that apply to sequences, and also to compare sequences. The query retrieves a sequence of telephone numbers for each customer from the **AdditionalContactInfo** column of the **Contact** table. This sequence is then compared with the sequence of two telephone numbers ("111-111-1111", "222-2222").  
  
 The query uses the **=** comparison operator. Each node in the sequence on the right side of the **=** operator is compared with each node in the sequence on the left side. If the nodes match, the node comparison is **TRUE**. It is then converted to an int and compared with 1, and the query returns the customer ID.  
  
```sql
WITH XMLNAMESPACES (  
'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactInfo' AS ACI,  
'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes' AS ACT)  
SELECT ContactID   
FROM   Person.Contact  
WHERE  AdditionalContactInfo.value('  
      //ACI:AdditionalContactInfo//ACT:telephoneNumber/ACT:number =   
          ("111-111-1111", "222-2222")',  
      'bit')= cast(1 as bit)  
```  
  
 There is another way to observe how the previous query works: Each phone telephone number value retrieved from the **AdditionalContactInfo** column is compared with the set of two telephone numbers. If the value is in the set, that customer is returned in the result.  
  
### B. Using a numeric operator  
 The + operator in this query is a value operator, because it applies to a single item. For example, value 1 is added to a lot size that is returned by the query:  
  
```sql
SELECT ProductModelID, Instructions.query('  
     declare namespace   
 AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
     for $i in (/AWMI:root/AWMI:Location)[1]  
     return   
       <Location LocationID="{ ($i/@LocationID) }"  
                   LotSize  = "{  number($i/@LotSize) }"  
                   LotSize2 = "{ number($i/@LotSize) + 1 }"  
                   LotSize3 = "{ number($i/@LotSize) + 2 }" >  
  
       </Location>  
') as Result  
FROM Production.ProductModel  
where ProductModelID=7  
```  
  
### C. Using a value operator  
 The following query retrieves the <`Picture`> elements for a product model where the picture size is "small":  
  
```sql
SELECT CatalogDescription.query('  
     declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
     for $P in /PD:ProductDescription/PD:Picture[PD:Size eq "small"]  
     return  
           $P  
    ') as Result  
FROM Production.ProductModel  
where ProductModelID=19  
```  
  
 Because both the operands to the **eq** operator are atomic values, the value operator is used in the query. You can write the same query by using the general comparison operator ( **=** ).  
  
## See Also  
 [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)   
 [XML Data &#40;SQL Server&#41;](../relational-databases/xml/xml-data-sql-server.md)   
 [XQuery Language Reference &#40;SQL Server&#41;](../xquery/xquery-language-reference-sql-server.md)  
  
  
