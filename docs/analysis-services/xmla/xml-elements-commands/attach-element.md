---
title: "Attach Element | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.component: xmla
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Attach Element
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Attaches a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database that was previously detached from the current server instance or from another instance, to the current server instance.  
  
## Syntax  
  
```xml  
  
<Command>  
   <Attach>  
      <Folder>...</Folder>  
            <ReadWriteMode>...</ReadWriteMode>  
            <Password>...</Password>  
   </Attach>  
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
|Child elements|[Folder](../../../analysis-services/xmla/xml-elements-properties/folder-element-xmla.md)<br /><br /> [ReadWriteMode](../../../analysis-services/xmla/xml-elements-properties/readwritemode-element.md)<br /><br /> [Password](../../../analysis-services/xmla/xml-elements-properties/password-element-xmla.md)|  
  
## See Also  
 <xref:Microsoft.AnalysisServices.Database.Detach%2A>   
 [Detach Element](../../../analysis-services/xmla/xml-elements-commands/detach-element.md)   
 [Attach and Detach Analysis Services Databases](../../../analysis-services/multidimensional-models/attach-and-detach-analysis-services-databases.md)   
 [Move an Analysis Services Database](../../../analysis-services/multidimensional-models/move-an-analysis-services-database.md)   
 [Database ReadWriteModes](../../../analysis-services/multidimensional-models/database-readwritemodes.md)   
 [Switch an Analysis Services database between ReadOnly and ReadWrite modes](../../../analysis-services/multidimensional-models/switch-an-analysis-services-database-between-readonly-and-readwrite-modes.md)  
  
  
