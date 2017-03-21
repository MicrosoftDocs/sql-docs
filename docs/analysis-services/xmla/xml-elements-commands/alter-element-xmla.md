---
title: "Alter Element (XMLA) | Microsoft Docs"
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
  - "Alter Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Alter"
  - "microsoft.xml.analysis.alter"
  - "urn:schemas-microsoft-com:xml-analysis#Alter"
helpviewer_keywords: 
  - "Alter command"
ms.assetid: 84e58385-c9ba-48fa-a867-94d35b777a56
caps.latest.revision: 15
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Alter Element (XMLA)
  Contains Analysis Services Scripting Language (ASSL) elements used by the [Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md) method to alter objects on an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
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
|Parent elements|[Command](../../../analysis-services/xmla/xml-elements-properties/command-element-xmla.md)|  
|Child elements|[Object](../../../analysis-services/xmla/xml-elements-properties/object-element-xmla.md), [ObjectDefinition](../../../analysis-services/xmla/xml-elements-properties/objectdefinition-element-xmla.md)|  
  
## Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|AllowCreate|(Optional **Boolean** attribute) Indicates whether objects defined in the **Alter** command should be created if they do not already exist.<br /><br /> If set to true, the objects defined in the **ObjectDefinition** element are created on the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance if they do not already exist. In other words, the **Alter** command is treated as a **Create** command if the objects do not already exist on the instance.<br /><br /> If this attribute is omitted or set to **false**, an error occurs if the objects do not already exist.|  
|ObjectExpansion|(Optional **Enum** attribute) Defines the extent of alteration to be performed by the **Execute** method.<br /><br /> If set to *ObjectProperties*, the **ObjectDefinition** element should contain only the complete definition of the major object to be altered, including subordinate minor objects. Subordinate major objects of the object to be altered remain unchanged.<br /><br /> Note: When using the *ObjectProperties* setting with the [ClrAssembly](../../../analysis-services/scripting/data-type/clrassembly-data-type-assl.md) data type, the [Data](../../../analysis-services/scripting/objects/data-element-assl.md) element of the associated [ClrAssemblyFile](../../../analysis-services/scripting/data-type/clrassemblyfile-data-type-assl.md) data types does not need to be specified. If not specified, the **ClrAssembly** uses existing files.<br /><br /> If set to *ExpandFull*, the **ObjectDefinition** element should contain not just the definition of the object to be altered, but also the definitions of all major objects which are descendants of the object to be altered.<br /><br /> Note: The *ExpandFull* setting cannot be used with the [Server](../../../analysis-services/scripting/objects/server-element-assl.md) element.|  
|Scope|(Optional **Enum** attribute) Defines the duration of objects defined in the **ObjectDefinition** element.<br /><br /> If set to *Session*, the objects defined in the **ObjectDefinition** element exist only for the duration of the XMLA session.<br /><br /> Note: When using the *Session* setting, the **ObjectDefinition** element can only contain [Dimension](../../../analysis-services/scripting/objects/dimension-element-assl.md), [Cube](../../../analysis-services/scripting/objects/cube-element-assl.md), or [MiningModel](../../../analysis-services/scripting/objects/miningmodel-element-assl.md) ASSL elements.<br /><br /> If this attribute is omitted, the objects defined in the **ObjectDefinition** element are persisted on the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance.|  
  
## Remarks  
 Each **Alter** command changes the definition of one major object under the parent object specified by the [ParentObject](../../../analysis-services/xmla/xml-elements-properties/parentobject-element-xmla.md) element.  
  
## See Also  
 [Commands &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/xml-elements-commands.md)  
  
  