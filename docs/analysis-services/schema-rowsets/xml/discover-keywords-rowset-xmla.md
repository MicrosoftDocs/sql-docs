---
title: "DISCOVER_KEYWORDS Rowset (XMLA) | Microsoft Docs"
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
  - "DISCOVER_KEYWORDS"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DISCOVER_KEYWORDS rowset"
ms.assetid: 99945e53-3a1b-4d7b-9aff-712977db8b2d
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_KEYWORDS Rowset (XMLA)
  Returns information about keywords reserved by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] XML for Analysis (XMLA) provider.  
  
 If you call the [Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md) method with the **DISCOVER_KEYWORDS** enumeration value in the [RequestType](../../../analysis-services/xmla/xml-elements-properties/requesttype-element-xmla.md) element, the **Discover** method returns the **DISCOVER_KEYWORDS** rowset.  
  
## Rowset Columns  
 The **DISCOVER_KEYWORDS** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**Keyword**|**DBTYPE_WSTR**||A list of all the keywords reserved by a provider.<br /><br /> Example: **AND**|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The **DISCOVER_KEYWORDS** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**Keyword**|**DBTYPE_WSTR**|Optional.|  
  
## See Also  
 [XML for Analysis Schema Rowsets](../../../analysis-services/schema-rowsets/xml/xml-for-analysis-schema-rowsets.md)   
 [DISCOVER_LITERALS Rowset](../../../analysis-services/schema-rowsets/xml/discover-literals-rowset.md)  
  
  