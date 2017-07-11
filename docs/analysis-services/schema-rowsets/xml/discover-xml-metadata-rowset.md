---
title: "DISCOVER_XML_METADATA Rowset | Microsoft Docs"
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
  - "DISCOVER_XML_METADATA"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DISCOVER_XML_METADATA rowset"
ms.assetid: 0befd026-db1b-43ac-b0e6-734abb56a4b1
caps.latest.revision: 40
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_XML_METADATA Rowset
  Returns an XML document describing a requested object. The rowset that is returned always consists of one row and one column.  
  
 If you call the [Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md) method with the **DISCOVER_XML_METATDATA** enumeration value in the [RequestType](../../../analysis-services/xmla/xml-elements-properties/requesttype-element-xmla.md) element, the **Discover** method returns the **DISCOVER_XML_METATDATA** rowset.  
  
## Rowset Columns  
 The **DISCOVER_XML_METADATA** rowset contains the following column.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**METADATA**|**DBTYPE_WSTR**||An XML document that describes the object requested by the restriction.|  
  
 This schema rowset is not sorted.  
  
> [!IMPORTANT]  
>  The **DISCOVER_XML_METADATA** rowset cannot be queried using the SELECT command syntax. However, the **DISCOVER_XML_METADATA** rowset can be queried using <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.GetSchemaDataSet%2A>.  
  
## Restriction Columns  
 The **DISCOVER_XML_METADATA** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**DatabaseID**|**DBTYPE_WSTR**|Optional.|  
|**DimensionID**|**DBTYPE_WSTR**|Optional.|  
|**CubeID**|**DBTYPE_WSTR**|Optional.|  
|**MeasureGroupID**|**DBTYPE_WSTR**|Optional.|  
|**PartitionID**|**DBTYPE_WSTR**|Optional.|  
|**PerspectiveID**|**DBTYPE_WSTR**|Optional.|  
|**DimensionPermissionID**|**DBTYPE_WSTR**|Optional.|  
|**RoleID**|**DBTYPE_WSTR**|Optional.|  
|**DatabasePermissionID**|**DBTYPE_WSTR**|Optional.|  
|**MiningModelID**|**DBTYPE_WSTR**|Optional.|  
|**MiningModelPermissionID**|**DBTYPE_WSTR**|Optional.|  
|**DataSourceID**|**DBTYPE_WSTR**|Optional.|  
|**MiningStructureID**|**DBTYPE_WSTR**|Optional.|  
|**AggregationDesignID**|**DBTYPE_WSTR**|Optional.|  
|**TraceID**|**DBTYPE_WSTR**|Optional.|  
|**MiningStructurePermissionID**|**DBTYPE_WSTR**|Optional.|  
|**CubePermissionID**|**DBTYPE_WSTR**|Optional.|  
|**AssemblyID**|**DBTYPE_WSTR**|Optional.|  
|**MdxScriptID**|**DBTYPE_WSTR**|Optional.|  
|**DataSourceViewID**|**DBTYPE_WSTR**|Optional.|  
|**DataSourcePermissionID**|**DBTYPE_WSTR**|Optional.|  
|**ObjectExpansion**|**DBTYPE_WSTR**|Optional.|  
  
 The restriction, **ObjectExpansion**, is available for every major object of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]. The client typically uses restrictions to describe the OLAP objects for which the DDL is to be returned, and uses the **ObjectExpansion** restriction to define the degree of expansion in the returned DDL. The following table indicates whether the enumeration value is allowed for [Alter Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/alter-element-xmla.md) commands.  
  
|Enumeration Value|Description|  
|-----------------------|-----------------|  
|**ReferenceOnly**|Returns only the name/ID/timestamp/state requested for the requested objects and all descendant major objects recursively.|  
|**ObjectProperties**|Expands the requested object with no references to contained objects (includes expanded minor contained objects).|  
|**ExpandObject**|Same as *ObjectProperties*, but also returns the name, ID, and timestamp for contained major objects.|  
|**ExpandFull**|Fully expands the requested object recursively to the bottom of every contained object.|  
  
## See Also  
 [XML for Analysis Schema Rowsets](../../../analysis-services/schema-rowsets/xml/xml-for-analysis-schema-rowsets.md)  
  
  