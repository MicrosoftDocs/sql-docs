---
title: "Set Breakpoints | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.setbreakpoints.f1"
helpviewer_keywords: 
  - "Set Breakpoints dialog box"
ms.assetid: 876e61b7-875c-43f4-bbce-d7eeb90f6730
caps.latest.revision: 24
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Set Breakpoints
  Use the **Set Breakpoints** dialog box to specify the events on which to enable breakpoints and to control the behavior of the breakpoint.  
  
## Options  
 **Enabled**  
 Select to enable a breakpoint on an event.  
  
 **Break Condition**  
 View a list of available events on which to set breakpoints.  
  
 **Hit Count Type**  
 Specify when the breakpoint takes effect.  
  
|Value|Description|  
|-----------|-----------------|  
|**Always**|Execution is always suspended when the breakpoint is hit.|  
|**Hit count equals**|Execution is suspended when the number of times the breakpoint has occurred is equal to the hit count.|  
|**Hit greater or equal**|Execution is suspended when the number of times the breakpoint has occurred is equal to or greater than the hit count.|  
|**Hit count multiple**|Execution is suspended when a multiple of the hit count occurs. For example, if you set this option to 5, execution is suspended every fifth time.|  
  
 **Hit Count**  
 Specify the number of hits at which to trigger a break. This option is not available if the breakpoint is always in effect.  
  
## See Also  
 [Debugging Control Flow](../../integration-services/troubleshooting/debugging-control-flow.md)  
  
  