---
title: "ConnectPromptEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "ConnectPromptEnum"
helpviewer_keywords: 
  - "ConnectPromptEnum enumeration [ADO]"
ms.assetid: 21026e24-62b7-4cc9-8aef-62c1fc6cba75
author: MightyPen
ms.author: genemi
manager: craigg
---
# ConnectPromptEnum
Specifies whether a dialog box should be displayed to prompt for missing parameters when opening a connection to a data source.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adPromptAlways**|1|Prompts always.|  
|**adPromptComplete**|2|Prompts if more information is required.|  
|**adPromptCompleteRequired**|3|Prompts if more information is required but optional parameters are not allowed.|  
|**adPromptNever**|4|Never prompts.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.ConnectPrompt.ALWAYS|  
|AdoEnums.ConnectPrompt.COMPLETE|  
|AdoEnums.ConnectPrompt.COMPLETEREQUIRED|  
|AdoEnums.ConnectPrompt.NEVER|  
  
## Applies To  
 [Prompt Property-Dynamic (ADO)](../../../ado/reference/ado-api/prompt-property-dynamic-ado.md)
