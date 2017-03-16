---
title: "DISCOVER_LITERALS Rowset | Microsoft Docs"
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
apiname: 
  - "DISCOVER_LITERALS"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DISCOVER_LITERALS rowset"
ms.assetid: 1bf0a2e2-a419-4c25-b271-37dfa44de2ea
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_LITERALS Rowset
  Returns information about literals, including data types and values, supported by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] XML for Analysis (XMLA) provider.  
  
 If you call the [Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md) method with the **DISCOVER_LITERALS** enumeration value in the [RequestType](../../../analysis-services/xmla/xml-elements-properties/requesttype-element-xmla.md) element, the **Discover** method returns the **DISCOVER_LITERALS** rowset.  
  
## Rowset Columns  
 The **DISCOVER_LITERALS** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**LiteralName**|**DBTYPE_WSTR**||The name of the literal described in the row.<br /><br /> For example: **DBLITERAL_LIKE_PERCENT**|  
|**LiteralValue**|**DBTYPE_WSTR**||An actual literal value.<br /><br /> For example, if **LiteralName** is **DBLITERAL_LIKE_PERCENT** and the percent character (**%**) matches zero or more characters in a LIKE clause, the value of the **LiteralValue** column is "**%**".|  
|**LiteralInvalidChars**|**DBTYPE_WSTR**||The characters that are not valid in the literal.<br /><br /> For example, if table names can contain anything other than a numeric character, this string is "**0123456789**".|  
|**LiteralInvalidStartingChars**|**DBTYPE_WSTR**||The characters that are not valid as the first character of the literal. If the literal can start with any valid character, this is **null**.|  
|**LiteralMaxLength**|**DBTYPE_I4**||The maximum number of characters in the literal. If there is no maximum or the maximum is unknown, the value is –1.|  
|**LiteralNameEnumValue**|**DBTYPE_I4**|||  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The **DISCOVER_LITERALS** rowset can be restricted on the columns in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**LiteralName**|**DBTYPE_WSTR**|Optional.|  
  
## See Also  
 [XML for Analysis Schema Rowsets](../../../analysis-services/schema-rowsets/xml/xml-for-analysis-schema-rowsets.md)   
 [DISCOVER_KEYWORDS Rowset &#40;XMLA&#41;](../../../analysis-services/schema-rowsets/xml/discover-keywords-rowset-xmla.md)  
  
  