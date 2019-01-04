---
title: "DiagnosticInformation Element (ssbdiagnose) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
helpviewer_keywords: 
  - "XML output file format [ssbdiagnose], diagnosticinformation element"
  - "diagnosticinformation element"
  - "ssbdiagnose"
ms.assetid: 0cfda544-542c-4cf4-86d2-8031c91b10f6
author: stevestein
ms.author: sstein
manager: craigg
---
# DiagnosticInformation Element (ssbdiagnose)
  The **DiagnosticInformation** element contains all elements that report the diagnostic information found by the utility. **DiagnosticInformation** is the root element of a **ssbdiagnostic** XML output file.  
  
## Syntax  
  
```  
  
<DiagnosticInformation>   
    ...   
</DiagnosticInformation>  
```  
  
## Element Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|`None`|N/A|  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|None.|  
|**Default value**|None.|  
|**Occurrence**|Once per **ssbdiagnose** XML output file.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|None.|  
|**Child elements**|[Banner Element &#40;ssbdiagnose&#41;](banner-element-ssbdiagnose.md)<br /><br /> [Issue Element &#40;ssbdiagnose&#41;](issue-element-ssbdiagnose.md)|  
  
## Remarks  
 For more information about XML namespaces, see [Namespaces in an XML Document](https://go.microsoft.com/fwlink/?LinkId=7341) in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] MSDN Library.  
  
## See Also  
 [ssbdiagnose Utility &#40;Service Broker&#41;](ssbdiagnose-utility-service-broker.md)  
  
  
