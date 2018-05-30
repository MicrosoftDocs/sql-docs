---
title: "results Element (XMLA) | Microsoft Docs"
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
# results Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Contains a collection of [root](../../../analysis-services/xmla/xml-elements-properties/root-element-xmla.md) elements returned by the [Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md) method using the [Batch](../../../analysis-services/xmla/xml-elements-commands/batch-element-xmla.md) command.  
  
 **Namespace** `http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults`  
  
## Syntax  
  
```xml  
  
<return>  
   <results>  
      <root>...</root>  
   </results>  
</return>  
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
|Parent elements|[return](../../../analysis-services/xmla/xml-elements-properties/return-element-xmla.md)|  
|Child elements|[root](../../../analysis-services/xmla/xml-elements-properties/root-element-xmla.md)|  
  
## Remarks  
 If a **Batch** command is executed by the **Execute** method, the **return** element contains a single **results** element instead of a single **root** element. The content of the **results** element depends on the settings used to run the **Batch** command.  
  
 For non-transactional **Batch** commands, the **results** element contains one **root** element for each command executed by the **Batch** command, whether the command completes successfully or unsuccessfully. For transactional **Batch** commands, the **results** element contains only one **root** element, which contains the error information for the command that failed within the **Batch** command.  
  
## See also
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
