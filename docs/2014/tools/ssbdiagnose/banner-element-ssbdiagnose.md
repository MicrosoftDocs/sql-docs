---
title: "Banner Element (ssbdiagnose) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
helpviewer_keywords: 
  - "banner element"
  - "XML output file format [ssbdiagnose], banner element"
  - "ssbdiagnose"
ms.assetid: cc6cd49a-acf0-4cfb-8c6a-554692b89de2
author: stevestein
ms.author: sstein
manager: craigg
---
# Banner Element (ssbdiagnose)
  Identifies which utility generated the **ssbdiagnose** output XML file.  
  
## Syntax  
  
```  
  
<Banner  
    title="..."   
    product="..."   
    version="..." />  
```  
  
## Element Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|`title`|Identifies the utility that generated the **ssbdiagnose** XML output file.|  
|`product`|Identifies the product that generated the **ssbdiagnose** XML output file.|  
|`version`|Identifies which version of the utility generated the XML output file.|  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|None.|  
|**Default value**|None.|  
|**Occurrence**|Occurs once per **ssbdiagnose** output XML file.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[DiagnosticInformation Element &#40;ssbdiagnose&#41;](diagnosticinformation-element-ssbdiagnose.md)|  
|**Child elements**|None.|  
  
## Example  
 This is an example of a banner element.  
  
```  
<Banner title="Service Broker Diagnostics Utility" product="Microsoft SQL Server" version="10.0.1073.0" />  
```  
  
## See Also  
 [ssbdiagnose Utility &#40;Service Broker&#41;](ssbdiagnose-utility-service-broker.md)  
  
  
