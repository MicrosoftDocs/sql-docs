---
title: "ObjectDefinition Element (XMLA) | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# ObjectDefinition Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Contains one or more Analysis Services Scripting Language (ASSL) elements, used to create or alter objects on an instance of Analysis Services.  
  
## Syntax  
  
```xml  
  
<Create> <!-- or Alter -->  
   ...  
   <ObjectDefinition>  
      <!-- One or more ASSL elements -->  
   </ObjectDefinition>  
   ...  
</Create>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Alter](../../../analysis-services/xmla/xml-elements-commands/alter-element-xmla.md), [Create](../../../analysis-services/xmla/xml-elements-commands/create-element-xmla.md)|  
|Child elements|Required ASSL elements. One or more ASSL elements, used to define [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] objects. For more information about ASSL, see [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md).|  
  
## Remarks  
  
## Example  
 The following example creates an empty database named **Test Database** on an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance.  
  
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
  
## See also
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
