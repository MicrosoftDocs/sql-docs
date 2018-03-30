---
title: "DISCOVER_ENUMERATORS Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "DISCOVER_ENUMERATORS"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "DISCOVER_ENUMERATORS rowset"
ms.assetid: ddc7b13c-3135-4419-8166-eddd459167da
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# DISCOVER_ENUMERATORS Rowset
  Returns a list of names, data types, and enumeration values of enumerators supported by the [!INCLUDE[msCoName](../../includes/msconame-md.md)] XML for Analysis (XMLA) provider for a specific data source. The XMLA Provider publishes all the enumeration constants that it recognizes.  
  
 If you call the [Discover](../../../2014/analysis-services/dev-guide/discover-method-xmla.md) method with the `DISCOVER_ENUMERATORS` enumeration value in the [RequestType](../../../2014/analysis-services/dev-guide/requesttype-element-xmla.md) element, the `Discover` method returns the `DISCOVER_ENUMERATORS` schema rowset.  
  
## Rowset Columns  
 For each enumerator, there are multiple elements, one for each value in the enumeration. The rowset that represents each enumerator is flat, and the name of the enumerator may be repeated for elements belonging to the same enumeration.  
  
 The `DISCOVER_ENUMERATORS` rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|`EnumName`|`DBTYPE_WSTR`||The name of the enumerator that contains a set of values.|  
|`EnumDescription`|`DBTYPE_WSTR`||A localizable description of the enumerator. Can be `NULL`.|  
|`EnumType`|`DBTYPE_WSTR`||The data type of the enumeration values.|  
|`ElementName`|`DBTYPE_WSTR`||The name of one of the value elements in the enumerator set.<br /><br /> Example: `TDP`|  
|`ElementDescription`|`DBTYPE_WSTR`||(Optional) A localizable description of the element. Can be `NULL`.|  
|`ElementValue`|`DBTYPE_WSTR`||The value of the element. Can be `NULL`.<br /><br /> Example: `01`|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The `DISCOVER_ENUMERATORS` rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|`EnumName`|`DBTYPE_WSTR`||  
  
## See Also  
 [XML for Analysis Schema Rowsets](../../../2014/analysis-services/dev-guide/xml-for-analysis-schema-rowsets.md)  
  
  