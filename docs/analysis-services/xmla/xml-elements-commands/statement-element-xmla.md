---
title: "Statement Element (XMLA) | Microsoft Docs"
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
# Statement Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Contains a query or statement to be sent using the **Execute** method to an instance of Analysis Services.  
  
## Syntax  
  
```xml  
  
<Command>  
   <Statement>...</Statement>  
</Command>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Command](../../../analysis-services/xmla/xml-elements-properties/command-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The **Statement** command executes a query or statement on the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance. [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] supports the following languages:  
  
-   Multidimensional Expressions (MDX)  
  
-   Data Mining Extensions (MDX)  
  
-   A subset of Structured Query Language (SQL)  
  
## See also
 [Commands &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/xml-elements-commands.md)  
  
  
