---
title: "Alter Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Alter Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Alter"
  - "microsoft.xml.analysis.alter"
  - "urn:schemas-microsoft-com:xml-analysis#Alter"
helpviewer_keywords: 
  - "Alter command"
ms.assetid: 84e58385-c9ba-48fa-a867-94d35b777a56
author: minewiskan
ms.author: owend
manager: craigg
---
# Alter Element (XMLA)
  Contains Analysis Services Scripting Language (ASSL) elements used by the [Execute](../xml-elements-methods-execute.md) method to alter objects on an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
## Syntax  
  
```xml  
  
<Command>  
   <Alter Scope="enum" AllowCreate="boolean" ObjectExpansion="enum">  
      <Object>...</Object>  
      <ObjectDefinition>...</ObjectDefinition>  
   </Alter>  
</Command>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Command](../xml-elements-properties/command-element-xmla.md)|  
|Child elements|[Object](../xml-elements-properties/object-element-xmla.md), [ObjectDefinition](../xml-elements-properties/objectdefinition-element-xmla.md)|  
  
## Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|AllowCreate|(Optional `Boolean` attribute) Indicates whether objects defined in the `Alter` command should be created if they do not already exist.<br /><br /> If set to true, the objects defined in the `ObjectDefinition` element are created on the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance if they do not already exist. In other words, the `Alter` command is treated as a `Create` command if the objects do not already exist on the instance.<br /><br /> If this attribute is omitted or set to `false`, an error occurs if the objects do not already exist.|  
|ObjectExpansion|(Optional `Enum` attribute) Defines the extent of alteration to be performed by the `Execute` method.<br /><br /> If set to *ObjectProperties*, the `ObjectDefinition` element should contain only the complete definition of the major object to be altered, including subordinate minor objects. Subordinate major objects of the object to be altered remain unchanged. **Note:**  When using the *ObjectProperties* setting with the [ClrAssembly](../../scripting/data-type/assembly-data-type-assl.md) data type, the [Data](../../scripting/objects/data-element-assl.md) element of the associated [ClrAssemblyFile](../../scripting/data-type/clrassemblyfile-data-type-assl.md) data types does not need to be specified. If not specified, the `ClrAssembly` uses existing files. <br /><br /> If set to *ExpandFull*, the `ObjectDefinition` element should contain not just the definition of the object to be altered, but also the definitions of all major objects which are descendants of the object to be altered. **Note:**  The *ExpandFull* setting cannot be used with the [Server](../../scripting/objects/server-element-assl.md) element.|  
|Scope|(Optional `Enum` attribute) Defines the duration of objects defined in the `ObjectDefinition` element.<br /><br /> If set to *Session*, the objects defined in the `ObjectDefinition` element exist only for the duration of the XMLA session. **Note:**  When using the *Session* setting, the `ObjectDefinition` element can only contain [Dimension](../../scripting/objects/dimension-element-assl.md), [Cube](../../scripting/objects/cube-element-assl.md), or [MiningModel](../../scripting/objects/miningmodel-element-assl.md) ASSL elements. <br /><br /> If this attribute is omitted, the objects defined in the `ObjectDefinition` element are persisted on the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance.|  
  
## Remarks  
 Each `Alter` command changes the definition of one major object under the parent object specified by the [ParentObject](../xml-elements-properties/parentobject-element-xmla.md) element.  
  
## See Also  
 [Commands &#40;XMLA&#41;](xml-elements-commands.md)  
  
  
