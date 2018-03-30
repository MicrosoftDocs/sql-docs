---
title: "Process Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Process Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Process"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Process"
  - "microsoft.xml.analysis.process"
helpviewer_keywords: 
  - "Process command"
ms.assetid: 886fd480-c0e6-4c9b-b65e-da47f874d938
caps.latest.revision: 13
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# Process Element (XMLA)
  Processes objects on a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance.  
  
## Syntax  
  
```xml  
  
<Command>  
   <Process>  
      <Type>...</Type>  
      <Object>...</Object>  
      <Bindings>...</Bindings>  
      <DataSource>...</DataSource>  
      <DataSourceView>...</DataSourceView>  
      <ErrorConfiguration>...</ErrorConfiguration>  
      <WriteBackTableCreation>...</WriteBackTableCreation>  
   </Process>  
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
|Parent elements|[Command](../../../2014/analysis-services/dev-guide/command-element-xmla.md)|  
|Child elements|[Bindings](../../../2014/analysis-services/dev-guide/bindings-element-xmla.md), [DataSource](../../../2014/analysis-services/dev-guide/datasource-element-xmla.md), [DataSourceView](../../../2014/analysis-services/dev-guide/datasourceview-element-xmla.md), [ErrorConfiguration](../../../2014/analysis-services/dev-guide/errorconfiguration-element-xmla.md), [Object](../../../2014/analysis-services/dev-guide/object-element-xmla.md), [Type Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/type-element-xmla.md), [WriteBackTableCreation](../../../2014/analysis-services/dev-guide/writebacktablecreation-element-xmla.md)|  
  
## Remarks  
 For more information about processing objects, see [Processing Objects &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/processing-objects-xmla.md).  
  
## See Also  
 [Commands &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/commands-xmla.md)  
  
  