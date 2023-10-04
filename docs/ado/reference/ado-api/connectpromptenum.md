---
title: "ConnectPromptEnum"
description: "ConnectPromptEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "ConnectPromptEnum"
helpviewer_keywords:
  - "ConnectPromptEnum enumeration [ADO]"
apitype: "COM"
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
 [Prompt Property-Dynamic (ADO)](./prompt-property-dynamic-ado.md)