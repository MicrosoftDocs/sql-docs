---
title: "EnumString Data Type (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "EnumString Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "EnumString"
  - "urn:schemas-microsoft-com:xml-analysis#EnumString"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#EnumString"
helpviewer_keywords: 
  - "EnumString data type"
ms.assetid: 9214195e-4539-419b-95ec-b7aa75e033ab
caps.latest.revision: 29
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# EnumString Data Type (XMLA)
  Defines a derived data type that represents a set of named constants for a given enumerator.  
  
## Syntax  
  
```xml  
  
<EnumString>...</EnumString>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|**string**|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|None|  
|Derived elements|None|  
  
## Remarks  
 XML for Analysis (XMLA) uses enumerations to limit string values to a set of verifiable settings. **EnumString** uses the standard XML **string** data type. The specific values for each of the named constants are specified with the enumerator definition. Enumerators are defined by adding them to the [DISCOVER_ENUMERATORS](../../../analysis-services/schema-rowsets/xml/discover-enumerators-rowset.md) schema rowset, and can be retrieved by using the [Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md) method with the DISCOVER_ENUMERATORS request type.  
  
 The following table describes the enumerators supported by an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
|Enumerator|Description|  
|----------------|-----------------|  
|ProviderType|Supports the ProviderType column in the [DISCOVER_DATASOURCES](../../../analysis-services/schema-rowsets/xml/discover-datasources-rowset.md) schema rowset, which determines the type of data that can be returned by an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance.<br /><br /> This enumeration also supports the XMLA property, **ProviderType**, which determines the provider types supported by an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance. This enumeration is also used in the DISCOVER_DATASOURCES schema rowset.<br /><br /> For more information about **ProviderType**, see [Supported XMLA Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/propertylist-element-supported-xmla-properties.md).|  
|AuthenticationMode|Supports the AuthenticationMode column in the DISCOVER_DATASOURCES schema rowset, which determines the security credentials that must be passed to access an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance.|  
|PropertyAccessType|Supports the PropertyAccessType column in the [DISCOVER_PROPERTIES](../../../analysis-services/schema-rowsets/xml/discover-properties-rowset.md) schema rowset, which determines the type of access available to an XMLA property.|  
|StateSupport|Supports the XMLA property, **StateSupport**, which determines the level of statefulness supported by an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance.<br /><br /> For more information about **StateSupport**, see [Supported XMLA Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/propertylist-element-supported-xmla-properties.md).|  
|StateActionVerb|Contains a list of verbs supported by XMLA in SOAP headers, and used to begin, identify, and end a session.<br /><br /> For more information about sessions, see [Managing Connections and Sessions &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/managing-connections-and-sessions-xmla.md).|  
|ResultsetFormat|Supports the XMLA property, **Format**, which determines the type of data returned in a [root](../../../analysis-services/xmla/xml-elements-properties/root-element-xmla.md) element.<br /><br /> For more information about **Format**, see [Supported XMLA Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/propertylist-element-supported-xmla-properties.md).|  
|ResultsetAxisFormat|Supports the XMLA property, **AxisFormat**, which determines the format of axis information returned in a **root** element that contains multidimensional data.<br /><br /> For more information about **AxisFormat**, see [Supported XMLA Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/propertylist-element-supported-xmla-properties.md).|  
|ResultsetContents|Supports the XMLA property, **Content**, which determines whether metadata or data is returned in a **root** element.<br /><br /> For more information about **Content**, see [Supported XMLA Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/propertylist-element-supported-xmla-properties.md).|  
|MDXSupportLevel|Supports the XMLA property, **MDXSupport**, which indicates the level of Multidimensional Expressions (MDX) support available on an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance.<br /><br /> For more information about **MDXSupport**, see [Supported XMLA Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/propertylist-element-supported-xmla-properties.md).|  
  
## See Also  
 [XML Data Types &#40;XMLA&#41;](../../../analysis-services/xmla/xml-data-types/xml-data-types-xmla.md)  
  
  