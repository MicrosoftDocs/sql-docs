---
title: "XPath Data Types (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "mapping XDR types to XPath types [SQLXML]"
  - "data types [XPath]"
  - "arithmetic operators"
  - "mapping data types [SQLXML]"
  - "relational operators [SQLXML]"
  - "node-set [SQLXML]"
  - "data types [SQLXML], XPath"
  - "XPath operators [SQLXML]"
  - "XDR data type [SQLXML]"
  - "equality operators [SQLXML]"
  - "XPath conversions [SQLXML]"
  - "converting data types [SQLXML]"
  - "Boolean operators"
  - "XPath queries [SQLXML], data types"
  - "XPath data types [SQLXML]"
  - "operators [SQLXML]"
ms.assetid: a90374bf-406f-4384-ba81-59478017db68
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# XPath Data Types (SQLXML 4.0)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], XPath, and XML Schema (XSD) have very different data types. For example, XPath does not have integer or date data types, but [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and XSD have many. XSD uses nanosecond precision for time values, and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses at most 1/300-second precision. Consequently, mapping one data type to another is not always possible. For more information about mapping [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types to XSD data types, see [Data Type Coercions and the sql:datatype Annotation &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-using/data-type-coercions-and-the-sql-datatype-annotation-sqlxml-4-0.md).  
  
 XPath has three data types: **string**, **number**, and **boolean**. The **number** data type is always an IEEE 754 double-precision floating-point. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]**float(53)** data type is the closest to XPath **number**. However, **float(53)** is not exactly IEEE 754. For example, neither NaN (Not-a-Number) nor infinity is used. Attempting to convert a nonnumeric string to **number** and trying to divide by zero results in an error.  
  
## XPath Conversions  
 When you use an XPath query such as `OrderDetail[@UnitPrice > "10.0"]`, implicit and explicit data type conversions can change the meaning of the query in subtle ways. Therefore, it is important to understand how XPath data types are implemented. The XPath language specification, XML Path Language (XPath) version 1.0 W3C Proposed Recommendation 8 October 1999, can be found at the W3C Web site at http://www.w3.org/TR/1999/PR-xpath-19991008.html.  
  
 XPath operators are divided into four categories:  
  
-   Boolean operators (and, or)  
  
-   Relational operators (\<, >, \<=, >=)  
  
-   Equality operators (=, !=)  
  
-   Arithmetic operators (+, -, *, div, mod)  
  
 Each category of operator converts its operands differently. XPath operators implicitly convert their operands if necessary. Arithmetic operators convert their operands to **number**, and result in a number value. Boolean operators convert their operands to **boolean**, and result in a Boolean value. Relational operators and equality operators result in a Boolean value. However, they have different conversion rules depending on the original data types of their operands, as shown in this table.  
  
|Operand|Relational operator|Equality operator|  
|-------------|-------------------------|-----------------------|  
|Both operands are node-sets.|TRUE if and only if there is a node in one set and a node in the second set such that the comparison of their **string** values is TRUE.|Same.|  
|One is a node-set, the other a **string**.|TRUE if and only if there is a node in the node-set such that when converted to **number**, the comparison of it with the **string** converted to **number** is TRUE.|TRUE if and only if there is a node in the node-set such that when converted to **string**, the comparison of it with the **string** is TRUE.|  
|One is a node-set, the other a **number**.|TRUE if and only if there is a node in the node-set such that when converted to **number**, the comparison of it with the **number** is TRUE.|Same.|  
|One is a node-set, the other a **boolean**.|TRUE if and only if there is a node in the node-set such that when converted to **boolean** and then to **number**, the comparison of it with the **boolean** converted to **number** is TRUE.|TRUE if and only if there is a node in the node-set such that when converted to **boolean**, the comparison of it with the **boolean** is TRUE.|  
|Neither is a node-set.|Convert both operands to **number** and then compare.|Convert both operands to a common type and then compare. Convert to **boolean** if either is **boolean**, **number** if either is **number**; otherwise, convert to **string**.|  
  
> [!NOTE]  
>  Because XPath relational operators always convert their operands to **number**, **string** comparisons are not possible. To include date comparisons, SQL Server 2000 offers this variation to the XPath specification: When a relational operator compares a **string** to a **string**, a node-set to a **string**, or a string-valued node-set to a string-valued node-set, a **string** comparison (not a **number** comparison) is performed.  
  
## Node-Set Conversions  
 Node-set conversions are not always intuitive. A node-set is converted to a **string** by taking the string value of only the first node in the set. A node-set is converted to **number** by converting it to **string**, and then converting **string** to **number**. A node-set is converted to **boolean** by testing for its existence.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not perform positional selection on node-sets: for example, the XPath query `Customer[3]` means the third customer; this type of positional selection is not supported in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Therefore, the node-set-to-**string** or node-set-to-**number** conversions as described by the XPath specification are not implemented. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses "any" semantics wherever the XPath specification specifies "first" semantics. For example, based on the W3C XPath specification, the XPath query `Order[OrderDetail/@UnitPrice > 10.0]` selects those orders with the first **OrderDetail** that has a **UnitPrice** greater than 10.0. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this XPath query selects those orders with any **OrderDetail** that has a **UnitPrice** greater than 10.0.  
  
 Conversion to **boolean** generates an existence test; therefore, the XPath query `Products[@Discontinued=true()]` is equivalent to the SQL expression "Products.Discontinued is not null", not the SQL expression "Products.Discontinued = 1". To make the query equivalent to the latter SQL expression, first convert the node-set to a non-**boolean** type, such as **number**. For example, `Products[number(@Discontinued) = true()]`.  
  
 Because most operators are defined to be TRUE if they are TRUE for any or one of the nodes in the node-set, these operations always evaluate to FALSE if the node-set is empty. Thus, if A is empty, both `A = B` and `A != B` are FALSE, and `not(A=B)` and `not(A!=B)` are TRUE.  
  
 Usually, an attribute or element that maps to a column exists if the value of that column in the database is not **null**. Elements that map to rows exist if any of their children exist.  
  
> [!NOTE]  
>  Elements annotated with **is-constant** always exist. Consequently, XPath predicates cannot be used on **is-constant** elements.  
  
 When a node-set is converted to **string** or **number**, its XDR type (if any) is inspected in the annotated schema and that type is used to determine the conversion that is required.  
  
## Mapping XDR Data Types to XPath Data Types  
 The XPath data type of a node is derived from the XDR data type in the schema, as shown in the following table (the node **EmployeeID** is used for illustrative purpose).  
  
|XDR data type|Equivalent<br /><br /> XPath data type|SQL Server conversion used|  
|-------------------|------------------------------------|--------------------------------|  
|Nonebin.base64bin.hex|N/A|NoneEmployeeID|  
|boolean|boolean|CONVERT(bit, EmployeeID)|  
|number, int, float,i1, i2, i4, i8,r4, r8ui1, ui2, ui4, ui8|number|CONVERT(float(53), EmployeeID)|  
|id, idref, idrefsentity, entities, enumerationnotation, nmtoken, nmtokens, chardate, Timedate, Time.tz, string, uri, uuid|string|CONVERT(nvarchar(4000), EmployeeID, 126)|  
|fixed14.4|N/A(There is no data type in XPath that is equivalent to the fixed14.4 XDR data type)|CONVERT(money, EmployeeID)|  
|date|string|LEFT(CONVERT(nvarchar(4000), EmployeeID, 126), 10)|  
|time<br /><br /> time.tz|string|SUBSTRING(CONVERT(nvarchar(4000), EmployeeID, 126), 1 + CHARINDEX(N'T', CONVERT(nvarchar(4000), EmployeeID, 126)), 24)|  
  
 The date and time conversions are designed to work whether the value is stored in the database using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]**datetime** data type or a **string**. Note that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]**datetime** data type does not use **timezone** and has a smaller precision than the XML **time** data type. To include the **timezone** data type or additional precision, store the data in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using a **string** type.  
  
 When a node is converted from its XDR data type to the XPath data type, additional conversion is sometimes necessary (from one XPath data type to another XPath data type). For example, consider this XPath query:  
  
```  
(@m + 3) = 4  
```  
  
 If @m is of the **fixed14.4** XDR data type, the conversion from XDR data type to XPath data type is accomplished using:  
  
```  
CONVERT(money, m)  
```  
  
 In this conversion, the node `m` is converted from **fixed14.4** to **money**. However, adding the value of 3, requires additional conversion:  
  
```  
CONVERT(float(CONVERT(money, m))  
```  
  
 The XPath expression is evaluated as:  
  
```  
CONVERT(float(CONVERT(money, m)) + CONVERT(float(53), 3) = CONVERT(float(53), 3)  
```  
  
 As shown in the following table, this is the same conversion that is applied for other XPath expressions (such as literals or compound expressions).  
  
||||||  
|-|-|-|-|-|  
||X is unknown|X is **string**|X is **number**|X is **boolean**|  
|string(X)|CONVERT (nvarchar(4000), X, 126)|-|CONVERT (nvarchar(4000), X, 126)|CASE WHEN X THEN N'true' ELSE N'false' END|  
|number(X)|CONVERT (float(53), X)|CONVERT (float(53), X)|-|CASE WHEN X THEN 1 ELSE 0 END|  
|boolean(X)|-|LEN(X) > 0|X != 0|-|  
  
## Examples  
  
### A. Convert a data type in an XPath query  
 In the following XPath query specified against an annotated XSD schema, the query selects all **Employee** nodes with the **EmployeeID** attribute value of E-1, where "E-" is the prefix specified using the **sql:id-prefix** annotation.  
  
 `Employee[@EmployeeID="E-1"]`  
  
 The predicate in the query is equivalent to the SQL expression:  
  
 `N'E-' + CONVERT(nvarchar(4000), Employees.EmployeeID, 126) = N'E-1'`  
  
 Because **EmployeeID** is one of the **id** (**idref**, **idrefs**, **nmtoken**, **nmtokens**, and so on) data type values in the XSD schema, **EmployeeID** is converted to the **string** XPath data type using the conversion rules described previously.  
  
 `CONVERT(nvarchar(4000), Employees.EmployeeID, 126)`  
  
 The "E-" prefix is added to the string, and the result is then compared with `N'E-1'`.  
  
### B. Perform several data type conversions in an XPath query  
 Consider this XPath query specified against an annotated XSD schema: `OrderDetail[@UnitPrice * @OrderQty > 98]`  
  
 This XPath query returns all the **\<OrderDetail>** elements satisfying the predicate `@UnitPrice * @OrderQty > 98`. If the **UnitPrice** is annotated with a **fixed14.4** data type in the annotated schema, this predicate is equivalent to the SQL expression:  
  
 `CONVERT(float(53), CONVERT(money, OrderDetail.UnitPrice)) * CONVERT(float(53), OrderDetail.OrderQty) > CONVERT(float(53), 98)`  
  
 In converting the values in the XPath query, the first conversion converts the XDR data type to the XPath data type. Because the XSD data type of **UnitPrice** is **fixed14.4**, as described in the previous table, this is the first conversion that is used:  
  
```  
CONVERT(money, OrderDetail.UnitPrice))   
```  
  
 Because the arithmetic operators convert their operands to the **number** XPath data type, the second conversion (from one XPath data type to another XPath data type) is applied in which the value is converted to **float(53)** (**float(53)** is close to the XPath **number** data type):  
  
```  
CONVERT(float(53), CONVERT(money, OrderDetail.UnitPrice))   
```  
  
 Assuming the **OrderQty** attribute has no XSD data type, **OrderQty** is converted to a **number** XPath data type in a single conversion:  
  
```  
CONVERT(float(53), OrderDetail.OrderQty)  
```  
  
 Similarly, the value 98 is converted to the **number** XPath data type:  
  
```  
CONVERT(float(53), 98)  
```  
  
> [!NOTE]  
>  If the XSD data type used in the schema is incompatible with the underlying [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type in the database, or if an impossible XPath data type conversion is performed, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] may return an error. For example, if the **EmployeeID** attribute is annotated with **id-prefix** annotation, the XPath `Employee[@EmployeeID=1]` generates an error, because **EmployeeID** has the **id-prefix** annotation and cannot be converted to **number**.  
  
  
