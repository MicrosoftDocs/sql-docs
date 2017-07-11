---
title: "DISCOVER_PROPERTIES Rowset | Microsoft Docs"
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
  - "DISCOVER_PROPERTIES"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DISCOVER_PROPERTIES rowset"
ms.assetid: 3e2b50e2-3855-4091-8b02-4968e8e57d4c
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_PROPERTIES Rowset
  Returns a list of information and values about the standard and provider-specific properties that are supported by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] XML for Analysis (XMLA) provider for the specified data source. Unsupported properties are not listed in the returned result set.  
  
 If you call the [Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md) method with the **DISCOVER_PROPERTIES** enumeration value in the [RequestType](../../../analysis-services/xmla/xml-elements-properties/requesttype-element-xmla.md) element, the **Discover** method returns the **DISCOVER_PROPERTIES** rowset..  
  
## Rowset Columns  
 The **DISCOVER_PROPERTIES** rowset contains the following columns.  
  
|Column name|Type|Length|Description|  
|-----------------|----------|------------|-----------------|  
|**PropertyName**|**DBTYPE_WSTR**||The name of the property.|  
|**PropertyDescription**|**DBTYPE_WSTR**||A localizable text description of the property. May return **NULL**.|  
|**PropertyType**|**DBTYPE_WSTR**||The XML data type of the property.<br /><br /> May return **NULL**.|  
|**PropertyAccessType**|**DBTYPE_WSTR**||The access for the property. The value can be **Read**, **Write**, or **ReadWrite**.|  
|**IsRequired**|**DBTYPE_BOOL**||A Boolean that indicates whether a property is required.<br /><br /> True if a property is required; false if it is not required.<br /><br /> May return **NULL**.|  
|**Value**|**DBTYPE_WSTR**||The current value of the property.<br /><br /> May return **NULL**.|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The **DISCOVER_PROPERTIES** rowset can be restricted on the column listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**PropertyName**|**DBTYPE_WSTR**||  
  
## See Also  
 [XML for Analysis Schema Rowsets](../../../analysis-services/schema-rowsets/xml/xml-for-analysis-schema-rowsets.md)  
  
  