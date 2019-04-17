---
title: "XQuery Functions against the xml Data Type | Microsoft Docs"
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
  - "XQuery, functions"
  - "xml data type [SQL Server], XQuery"
  - "functions [SQL Server], XQuery"
ms.assetid: 8df0877d-a03f-4ca9-b84e-908c4bb42b5e
author: rothja
ms.author: jroth
manager: craigg
---
# XQuery Functions against the xml Data Type
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  This topic and its subtopics describe the functions you can use when specifying XQuery against the **xml** data type. For the W3C specifications, see [http://www.w3.org/TR/2004/WD-xpath-functions-20040723](https://go.microsoft.com/fwlink/?LinkId=4873).  
  
 The XQuery functions belong to the http://www.w3.org/2004/07/xpath-functions namespace. The W3C specifications use the "fn:" namespace prefix to describe these functions. You do not have to specify the "fn:" namespace prefix explicitly when you are using the functions. Because of this and to improve readability, the namespace prefixes are generally not used in this documentation.  
  
 The following table lists the XQuery functions that are supported against the **xml**data type.  
  
|Category|Function Name|  
|--------------|-------------------|  
|[Functions on Numeric Values](https://msdn.microsoft.com/library/d5740a32-b174-43b9-b64d-1cc6edc50cff)|[ceiling](../xquery/numeric-values-functions-ceiling.md)|  
||[floor](../xquery/numeric-values-functions-floor.md)|  
||[round](../xquery/numeric-values-functions-round.md)|  
|[XQuery Functions on String Values](https://msdn.microsoft.com/library/2dccefef-5d90-4f56-bda7-4c1954d8a730)|[concat](../xquery/functions-on-string-values-concat.md)|  
||[contains](../xquery/functions-on-string-values-contains.md)|  
||[substring](../xquery/functions-on-string-values-substring.md)|  
||[lower-case  Function &#40;XQuery&#41;](../xquery/functions-on-string-values-lower-case.md)|  
||[string-length](../xquery/functions-on-string-values-string-length.md)|  
||[upper-case  Function &#40;XQuery&#41;](../xquery/functions-on-string-values-upper-case.md)|  
|Functions on Boolean Values|[not](../xquery/functions-on-boolean-values-not-function.md)|  
|[Functions on Nodes](https://msdn.microsoft.com/library/09a8affa-3341-4f50-aebc-fdf529e00c08)|[number](../xquery/functions-on-nodes-number.md)|  
||[local-name Function (XQuery)](../xquery/functions-on-nodes-local-name.md)|  
||[namespace-uri Function (XQuery)](../xquery/functions-on-nodes-namespace-uri.md)|  
|[Context Functions](https://msdn.microsoft.com/library/f7d8af33-9de9-450c-a667-23dee3129b5f)|[last](../xquery/context-functions-last-xquery.md)|  
||[position](../xquery/context-functions-position-xquery.md)|  
|[Functions on Sequences](https://msdn.microsoft.com/library/672d2795-53ab-49c2-bf24-bc81a47ecd3f)|[empty](../xquery/functions-on-sequences-empty.md)|  
||[distinct-values](../xquery/functions-on-sequences-distinct-values.md)|  
||[id Function (XQuery)](../xquery/functions-on-sequences-id.md)|  
|[Aggregate Functions &#40;XQuery&#41;](https://msdn.microsoft.com/library/be647ef1-291e-4a5d-ab18-07c759efe176)|[count](../xquery/aggregate-functions-count.md)|  
||[avg](../xquery/aggregate-functions-avg.md)|  
||[min](../xquery/aggregate-functions-min.md)|  
||[max](../xquery/aggregate-functions-max.md)|  
||[sum](../xquery/aggregate-functions-sum.md)|  
|[Constructor Functions &#40;XQuery&#41;](../xquery/constructor-functions-xquery.md)|[Constructor Functions](../xquery/constructor-functions-xquery.md)|  
|[Data Accessor Functions](../xquery/data-accessor-functions.md)|[string](../xquery/data-accessor-functions-string-xquery.md)|  
||[data](../xquery/data-accessor-functions-data-xquery.md)|  
|[Boolean Constructor Functions &#40;XQuery&#41;](https://msdn.microsoft.com/library/fa907f39-d4b7-4495-b829-c788928e0f64)|[true Function (XQuery)](../xquery/boolean-constructor-functions-true-xquery.md)|  
||[false Function (XQuery)](../xquery/boolean-constructor-functions-false-xquery.md)|  
|[Functions Related to QNames &#40;XQuery&#41;](https://msdn.microsoft.com/library/7e07eb26-f551-4b63-ab77-861684faff71)|[expanded-QName (XQuery)](../xquery/functions-related-to-qnames-expanded-qname.md)|  
||[local-name-from-QName (XQuery)](../xquery/functions-related-to-qnames-local-name-from-qname.md)|  
||[namespace-uri-from-QName (XQuery)](../xquery/functions-related-to-qnames-namespace-uri-from-qname.md)|  
|[SQL Server XQuery Extension Functions](https://msdn.microsoft.com/library/4bc5d499-5fec-4c3f-b11e-5ab5ef9d8f97)|[sql:column() function (XQuery)](../xquery/xquery-extension-functions-sql-column.md)|  
||[sql:variable() function (XQuery)](../xquery/xquery-extension-functions-sql-variable.md)|  
  
## See Also  
 [xml Data Type Methods](../t-sql/xml/xml-data-type-methods.md)   
 [XQuery Language Reference &#40;SQL Server&#41;](../xquery/xquery-language-reference-sql-server.md)   
 [XML Data &#40;SQL Server&#41;](../relational-databases/xml/xml-data-sql-server.md)  
  
  
