---
description: "SqlToolsVSNativeHelpers - FrameWindowVisible"
title: "FrameWindowVisible | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: "reference"
ms.assetid: 9091d714-98bc-43ec-b8d1-9c892cb57f19
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SqlToolsVSNativeHelpers - FrameWindowVisible
[!INCLUDE [SQL Server Azure SQL Database](../includes/applies-to-version/sql-asdb.md)]
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
  
  
