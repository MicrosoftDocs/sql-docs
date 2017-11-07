---
title: "FrameWindowVisible | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 9091d714-98bc-43ec-b8d1-9c892cb57f19
caps.latest.revision: 6
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# SqlToolsVSNativeHelpers - FrameWindowVisible
  Property that specifies whether a given window frame is visible. The helper method is used from managed code.  
  
## Syntax  
  
```  
  
BOOL WINAPI IsFrameWindowVisible(IVsWindowFrame* frame)  
{  
    if (NULL == frame)  
    {  
        return FALSE;  
    }  
  
    return S_OK == frame->IsVisible();  
}  
```  
  
#### Parameters  
 *frame*  
  
 IVsWindowFrame* pointer to a Visual Studio WindowFrame.  
  
## Property Value/Return Value  
 A Boolean value that specifies whether the window frame specified by *frame* is visible.  
  
## See Also  
 [SqlToolsVSNativeHelpers](../relational-databases/sqltoolsvsnativehelpers.md)  
  
  