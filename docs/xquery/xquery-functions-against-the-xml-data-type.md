---
title: "XQuery Functions against the xml Data Type"
description: Learn about the XQuery functions that are supported for use against the xml data type.
author: rothja
ms.author: jroth
ms.date: "03/09/2017"
ms.service: sql
ms.subservice: xml
ms.topic: reference
helpviewer_keywords:
  - "XQuery, functions"
  - "xml data type [SQL Server], XQuery"
  - "functions [SQL Server], XQuery"
dev_langs:
  - "XML"
---
# XQuery Functions against the xml Data Type
[!INCLUDE [SQL Server Azure SQL Database](../includes/applies-to-version/sqlserver.md)]

  This topic and its subtopics describe the functions you can use when specifying XQuery against the **xml** data type. For the W3C specifications, see [http://www.w3.org/TR/2004/WD-xpath-functions-20040723](https://go.microsoft.com/fwlink/?LinkId=4873).  
  
 The XQuery functions belong to the http://www.w3.org/2004/07/xpath-functions namespace. The W3C specifications use the "fn:" namespace prefix to describe these functions. You do not have to specify the "fn:" namespace prefix explicitly when you are using the functions. Because of this and to improve readability, the namespace prefixes are generally not used in this documentation.  
  
 The following lists the XQuery functions that are supported against the **xml** data type.  
  
| Function Category | Functions |
| --- | --- |
| Functions on Numeric Values | [ceiling](../xquery/numeric-values-functions-ceiling.md)<br>[floor](../xquery/numeric-values-functions-floor.md)<br>[round](../xquery/numeric-values-functions-round.md) |
| XQuery Functions on String Values | [concat](../xquery/functions-on-string-values-concat.md)<br>[contains](../xquery/functions-on-string-values-contains.md)<br>[substring](../xquery/functions-on-string-values-substring.md)<br>[lower-case Function (XQuery)](../xquery/functions-on-string-values-lower-case.md)<br>[string-length](../xquery/functions-on-string-values-string-length.md)<br>[upper-case Function (XQuery)](../xquery/functions-on-string-values-upper-case.md) |
| Functions on Boolean Values | [not](../xquery/functions-on-boolean-values-not-function.md) |
| Functions on Nodes | [number](../xquery/functions-on-nodes-number.md)<br>[local-name Function (XQuery)](../xquery/functions-on-nodes-local-name.md)<br>[namespace-uri Function (XQuery)](../xquery/functions-on-nodes-namespace-uri.md) |
| Context Functions | [last](../xquery/context-functions-last-xquery.md)<br>[position](../xquery/context-functions-position-xquery.md) |
| Functions on Sequences | [empty](../xquery/functions-on-sequences-empty.md)<br>[distinct-values](../xquery/functions-on-sequences-distinct-values.md)<br>[id Function (XQuery)](../xquery/functions-on-sequences-id.md) |
| Aggregate Functions (XQuery) | [count](../xquery/aggregate-functions-count.md)<br>[avg](../xquery/aggregate-functions-avg.md)<br>[min](../xquery/aggregate-functions-min.md)<br>[max](../xquery/aggregate-functions-max.md)<br>[sum](../xquery/aggregate-functions-sum.md) |
| Constructor Functions (XQuery) | [Constructor Functions](../xquery/constructor-functions-xquery.md) |
| Data Accessor Functions | [string](../xquery/data-accessor-functions-string-xquery.md)<br>[data](../xquery/data-accessor-functions-data-xquery.md) |
| Boolean Constructor Functions (XQuery) | [true Function (XQuery)](../xquery/boolean-constructor-functions-true-xquery.md)<br>[false Function (XQuery)](../xquery/boolean-constructor-functions-false-xquery.md) |
| Functions Related to QNames (XQuery) | [expanded-QName (XQuery)](../xquery/functions-related-to-qnames-expanded-qname.md)<br>[local-name-from-QName (XQuery)](../xquery/functions-related-to-qnames-local-name-from-qname.md)<br>[namespace-uri-from-QName (XQuery)](../xquery/functions-related-to-qnames-namespace-uri-from-qname.md) |
| SQL Server XQuery Extension Functions | [sql:column() function (XQuery)](../xquery/xquery-extension-functions-sql-column.md)<br>[sql:variable() function (XQuery)](../xquery/xquery-extension-functions-sql-variable.md) |

## Related content

- [xml Data Type Methods](../t-sql/xml/xml-data-type-methods.md)
- [XQuery Language Reference (SQL Server)](xquery-language-reference-sql-server.md)
- [XML data (SQL Server)](../relational-databases/xml/xml-data-sql-server.md)
