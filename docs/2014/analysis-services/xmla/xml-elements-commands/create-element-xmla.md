---
title: "Create Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Create Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Create"
  - "urn:schemas-microsoft-com:xml-analysis#Create"
  - "microsoft.xml.analysis.create"
helpviewer_keywords: 
  - "Create command (XMLA)"
ms.assetid: a623d362-a1ac-40e4-8816-65fac89cb391
author: minewiskan
ms.author: owend
manager: craigg
---
# Create Element (XMLA)
  Contains Analysis Services Scripting Language (ASSL) elements used by the `Execute` method to create objects on a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance.  
  
## Syntax  
  
```xml  
  
<Command>  
   <Create Scope="enum" AllowOverwrite="boolean">  
      <ParentObject>...</ParentObject>  
      <ObjectDefinition>...</ObjectDefinition>  
   </Create>  
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
|Child elements|[ObjectDefinition](../xml-elements-properties/objectdefinition-element-xmla.md), [ParentObject](../xml-elements-properties/object-element-xmla.md)|  
  
## Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|AllowOverwrite|Optional `Boolean` attribute. If set to True, the objects defined in the `ObjectDefinition` element can overwrite existing objects on the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance. If this attribute is omitted or set to False, the presence of an existing object generates an error.|  
|Scope|Optional `Enum` attribute. Defines the duration of objects defined in the `ObjectDefinition` element. If this attribute is omitted, the objects defined in the `ObjectDefinition` element are persisted on the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance. The following values are available:<br /><br /> -   *Session*<br />     The objects defined in the `ObjectDefinition` element exist only for the duration of the XML for Analysis (XMLA) session. **Note:**      When using the *Session* setting, the `ObjectDefinition` element can only contain [Dimension](../../scripting/objects/dimension-element-assl.md), [Cube](../../scripting/objects/cube-element-assl.md), or [MiningModel](../../scripting/objects/miningmodel-element-assl.md) ASSL elements.|  
  
## Remarks  
 Each `Create` operation creates one major object under a parent given by the `ParentObject` element. If the parent object is omitted, it is assumed to be the destination [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance. This generates an error if the parent of a major object is not the destination instance.  
  
## Example  
 The following example creates an empty database named `Test Database` on an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance.  
  
```  
  
      <Create xmlns="http://schemas.microsoft.com/analysisservices/2003/engine">  
   <ObjectDefinition>  
      <Database xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">  
         <Name>Test Database</Name>  
         <Description>A test database.</Description>  
      </Database>  
   </ObjectDefinition>  
</Create>  
```  
  
## See Also  
 [Commands &#40;XMLA&#41;](xml-elements-commands.md)  
  
  
