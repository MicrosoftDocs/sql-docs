---
title: "Binding Relational Data Inside XML Data | Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "relational data binding [SQL Server]"
  - "XML [SQL Server], binding relational data"
  - "xml data type [SQL Server], relational data binding"
  - "binding relational data [XML in SQL Server]"
  - "variables [XML in SQL Server], relational data binding"
  - "columns [XML in SQL Server], relational data binding"
ms.assetid: 03d013a9-b53f-46c3-9628-da77f099c74a
author: MightyPen
ms.author: genemi
manager: "craigg"
---
# Binding Relational Data Inside XML Data
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  You can specify [xml Data Type Methods](../../t-sql/xml/xml-data-type-methods.md) against an **xml** data type variable or column. For example, the [query&#40;&#41; Method &#40;xml Data Type&#41;](../../t-sql/xml/query-method-xml-data-type.md) executes the specified XQuery against an XML instance. When you construct XML in this manner, you may want to bring in a value from a non-XML type column or a Transact-SQL variable. This process is referred to as binding relational data inside XML.  
  
 To bind the non-XML relational data inside XML, the SQL Server Database Engine provides the following pseudo-functions:  
  
-   [sql:column&#40;&#41; Function &#40;XQuery&#41;](../../xquery/xquery-extension-functions-sql-column.md) Lets you use the values from a relational column in your XQuery or XML DML expression.  
  
-   [sql:variable&#40;&#41; Function &#40;XQuery&#41;](../../xquery/xquery-extension-functions-sql-variable.md) . Lets you use the value of a SQL variable in your XQuery or XML DML expression.  
  
 You can use these functions with **xml** data type methods whenever you want to expose a relational value inside XML.  
  
 You cannot use these functions to reference data in columns or variables of the **xml**, CLR user-defined types, datetime, smalldatetime, **text**, **ntext**, **sql_variant**, and **image** types.  
  
 Also, this binding is for read-only purposes. That is, you cannot write data in columns that use these functions. For example, sql:variable("\@x")="*some expression"* is not allowed.  
  
## Example: Cross-domain Query Using sql:variable()  
 This example shows how **sql:variable()** can enable an application to parameterize a query. The ISBN is passed in by using a SQL variable @isbn. By replacing the constant with **sql:variable()**, the query can be used to search for any ISBN and not just the one whose ISBN is 0-7356-1588-2.  
  
```  
DECLARE @isbn varchar(20)  
SET     @isbn = '0-7356-1588-2'  
SELECT  xCol  
FROM    T  
WHERE   xCol.exist ('/book/@ISBN[. = sql:variable("@isbn")]') = 1  
```  
  
 **sql:column()** can be used in a similar manner and provides additional benefits. Indexes over the column may be used for efficiency, as decided by the cost-based query optimizer. Also, the computed column may store a promoted property.  
  
## See Also  
 [xml Data Type Methods](../../t-sql/xml/xml-data-type-methods.md)  
  
  
