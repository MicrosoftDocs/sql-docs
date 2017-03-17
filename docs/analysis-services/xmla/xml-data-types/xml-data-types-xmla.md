---
title: "XML Data Types (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "XML data types [XMLA]"
  - "data types [XML for Analysis]"
  - "XMLA, data types"
  - "XML for Analysis, data types"
ms.assetid: 979b5384-90d9-4e09-9286-1d1eafdc4864
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# XML Data Types (XMLA)
  In addition to the standard primitive and derived types defined by the XML 1.0 recommendation, the XML for Analysis (XMLA) 1.1 specification defines additional data types to support the representation of multidimensional and tabular data.  
  
 XMLA uses the data types listed in the following table.  
  
|Data types|Description|  
|----------------|-----------------|  
|Boolean|The standard XML **boolean** data type.|  
|Decimal|The standard XML **decimal** data type.|  
|[EmptyResult](../../../analysis-services/xmla/xml-data-types/emptyresult-data-type-xmla.md)|A namespace on the **root** element. This namespace is returned when an XMLA command does not return a result because either the XMLA command does not normally return a result or because an error occurred on the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance while executing the XMLA command.|  
|[EnumString](../../../analysis-services/xmla/xml-data-types/enumstring-data-type-xmla.md)|A set of named string constants for a given enumerator.|  
|Integer|The standard XML **int** data type.|  
|[MDDataSet](../../../analysis-services/xmla/xml-data-types/mddataset-data-type-xmla.md)|Multidimensional data returned by the *Result* parameter of the [Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md) method.|  
|[Resultset](../../../analysis-services/xmla/xml-data-types/resultset-data-type-xmla.md)|A self-describing XML result set returned by the **Execute** method.|  
|[Rowset](../../../analysis-services/xmla/xml-data-types/rowset-data-type-xmla.md)|Rows from a data source, structured by an embedded XML schema, returned by the [Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md) method.|  
|String|The XML **string** data type.|  
|UnsignedInt|The XML **unsignedInt** schema type.|  
  
 For complete descriptions of the standard XML data types, see the World Wide Web Consortium (WC3) candidate recommendation.  
  
## See Also  
 [XML Elements &#40;XMLA&#41;](http://msdn.microsoft.com/library/40ab2360-efb6-4ba6-bf23-e84964e51008)   
 [XML for Analysis  &#40;XMLA&#41; Reference](../../../analysis-services/xmla/xml-for-analysis-xmla-reference.md)  
  
  