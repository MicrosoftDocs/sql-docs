---
title: "Logging in the Script Component | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "Script component [Integration Services], logging"
ms.assetid: 17c19787-379e-43fe-9107-e36e17ecda53
author: janinezhang
ms.author: janinez
manager: craigg
---
# Logging in the Script Component

[!INCLUDE[ssis-appliesto](../../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Logging in [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] packages lets you save detailed information about execution progress, results, and problems by recording predefined events or user-defined messages for later analysis. The Script component can use the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.Log%2A> method of the **ScriptMain** class to log user-defined data. If logging is enabled, and the **ScriptComponentLogEntry** event is selected for logging on the **Details** tab of the **Configure SSIS Logs** dialog box, a single call to the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.Log%2A> method stores the event information in all the log providers that have been configured for the data flow task.  
  
 Here is a simple example of logging:  
  
 `Dim bt(0) As Byte`  
  
 `Me.Log("Test Log Event", _`  
  
 `0, _`  
  
 `bt)`  
  
> [!NOTE]  
>  Although you can perform logging directly from your Script component, you may want to consider implementing events rather than logging. When using events, not only can you enable the logging of event messages, but you can respond to the event with default or user-defined event handlers.  
  
 For more information about logging, see [Integration Services &#40;SSIS&#41; Logging](../../../integration-services/performance/integration-services-ssis-logging.md).  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Logging](../../../integration-services/performance/integration-services-ssis-logging.md)  
  
  
