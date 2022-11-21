---
title: DiagnosticInformation Element
description: In SQL Server, the DiagnosticInformation element is the root element of a ssbdiagnostic XML output file.
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
helpviewer_keywords: 
  - "XML output file format [ssbdiagnose], diagnosticinformation element"
  - "diagnosticinformation element"
  - "ssbdiagnose"
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 03/14/2017
---

# DiagnosticInformation Element (ssbdiagnose)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

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
|**None**|N/A|  
  
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
|**Child elements**|[Banner Element &#40;ssbdiagnose&#41;](../../tools/ssbdiagnose/banner-element-ssbdiagnose.md)<br /><br /> [Issue Element &#40;ssbdiagnose&#41;](../../tools/ssbdiagnose/issue-element-ssbdiagnose.md)|  
  
## Remarks  
 For more information about XML namespaces, see [Namespaces in an XML Document](/dotnet/standard/data/xml/managing-namespaces-in-an-xml-document).  
  
## See Also  
 [ssbdiagnose Utility &#40;Service Broker&#41;](../../tools/ssbdiagnose/ssbdiagnose-utility-service-broker.md)  
  
