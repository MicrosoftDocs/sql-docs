---
title: "Dimension Processing Destination Editor (Connection Manager Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.dimprocessingtransformation.connection.f1"
helpviewer_keywords: 
  - "Dimension Processing Destination Editor"
ms.assetid: 44aab631-d62d-4895-8fc7-7f1f3b1b68ce
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Dimension Processing Destination Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **Dimension Processing Destination Editor** dialog box to specify a connection to a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] project or to an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
 To learn more about the Dimension Processing destination, see [Dimension Processing Destination](data-flow/dimension-processing-destination.md).  
  
## Options  
 **Connection manager**  
 Select an existing connection manager from the list, or click **New** to create a new connection manager.  
  
 **New**  
 Create a new connection by using the **Add Analysis Services Connection Manager** dialog box.  
  
 **List of available dimensions**  
 Select the dimension to process.  
  
 **Processing method**  
 Select the processing method to apply to the dimension selected in the list. The default value of this option is **Full**.  
  
|Value|Description|  
|-----------|-----------------|  
|**Add (incremental)**|Perform an incremental processing of the dimension.|  
|**Full**|Perform full processing of the dimension.|  
|**Update**|Perform an update processing of the dimension.|  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Dimension Processing Destination Editor &#40;Mappings Page&#41;](../../2014/integration-services/dimension-processing-destination-editor-mappings-page.md)   
 [Dimension Processing Destination Editor &#40;Advanced Page&#41;](../../2014/integration-services/dimension-processing-destination-editor-advanced-page.md)  
  
  
