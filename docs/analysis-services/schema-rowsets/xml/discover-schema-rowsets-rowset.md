---
title: "DISCOVER_SCHEMA_ROWSETS Rowset | Microsoft Docs"
ms.date: 05/03/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: schema-rowsets
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# DISCOVER_SCHEMA_ROWSETS Rowset
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Returns the names, restrictions, description, and other information for all enumeration values and any additional provider-specific enumeration values supported by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] XML for Analysis (XMLA) provider.  
  
 If you call the [Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md) method with the **DISCOVER_SCHEMA_ROWSETS** enumeration value in the [RequestType](../../../analysis-services/xmla/xml-elements-properties/requesttype-element-xmla.md) element, the **Discover** method returns the **DISCOVER_SCHEMA_ROWSETS** rowset.  
  
## Rowset Columns  
 The DISCOVER_SCHEMA_ROWSETS rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**SchemaName**|**DBTYPE_WSTR**||The name of the schema or request. This request returns the values in the *RequestTypes* enumeration.|  
|**SchemaGuid**|**DBTYPE_GUID**||The GUID of the schema.|  
|**Restrictions**|**DBTYPE_HCHAPTER**||An array of the restrictions supported by the provider.|  
|**Description**|**DBTYPE_WSTR**||A localizable description of the schema.|  
|**RestrictionsMask**|**DBTYPE_UI8**|||  
  
 This schema rowset is not sorted.  
  
 For a provider that supports three restrictions for the DBSCHEMA_MEMBERS schema rowset, the **Restrictions** array might return the following result. The elements in the result refer to column names in the schema.  
  
```  
<Restrictions>  
      <CATALOG_NAME type="string" />   
      <SCHEMA_NAME type="string" />   
      <CUBE_NAME type="string" />   
</Restrictions>  
```  
  
## Restriction Columns  
 The **DISCOVER_SCHEMA_ROWSETS** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**SchemaName**|**DBTYPE_WSTR**||  
  
## See Also  
 [XML for Analysis Schema Rowsets](../../../analysis-services/schema-rowsets/xml/xml-for-analysis-schema-rowsets.md)  
  
  
