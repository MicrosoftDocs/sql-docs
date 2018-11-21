---
title: "XML Output File Format (ssbdiagnose) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
helpviewer_keywords: 
  - "XML output file format [ssbdiagnose]"
  - "ssbdiagnose"
ms.assetid: 3ceb991b-6f15-4504-8828-de5adf448bc5
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# XML Output File Format (ssbdiagnose)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The **ssbdiagnose** utility delivers its output as an XML file when you run it with the **-XML** switch. The XML output file lists header information and the errors that it found in the [!INCLUDE[ssSB](../../includes/sssb-md.md)] configuration or conversation that was analyzed. You can write an application to analyze or report on the errors listed in the file. Or, you can view the XML file in a general XML editor, such as XML Notepad.  
  
 An **ssbdiangose** output file contains a DiagnosticInformation root element with two child types:  
  
-   A Banner element with header information.  
  
-   An Issue element for each issue that is reported by **ssbdiagnose**.  
  
## DiagnosticInformation Root Element  
  
-   [DiagnosticInformation Element &#40;ssbdiagnose&#41;](../../tools/ssbdiagnose/diagnosticinformation-element-ssbdiagnose.md)  
  
## Banner Element  
  
-   [Banner Element &#40;ssbdiagnose&#41;](../../tools/ssbdiagnose/banner-element-ssbdiagnose.md)  
  
## Issue Element  
  
-   [Issue Element &#40;ssbdiagnose&#41;](../../tools/ssbdiagnose/issue-element-ssbdiagnose.md)  
  
## See Also  
 [ssbdiagnose Utility &#40;Service Broker&#41;](../../tools/ssbdiagnose/ssbdiagnose-utility-service-broker.md)  
  
  
