---
title: "LOCALDB_ERROR_UNKNOWN_LANGUAGE_ID"
description: "LOCALDB_ERROR_UNKNOWN_LANGUAGE_ID"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: performance
ms.topic: "reference"
---
# LOCALDB_ERROR_UNKNOWN_LANGUAGE_ID
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
## Details  
  
| Attribute | Value |
| --------- | ----- |
|Product Name|SQL Server|  
|Event ID|270|  
|Event Source|SQL Server Local Database Runtime 12.0|  
|Component|Local Database Runtime API|  
|Message Text|Error getting the localized error message. The language specified by 'Language ID' parameter is unknown.|  
  
## Explanation  
 The requested language for the Local Database Runtime error message is unknown or not supported.  
  
## User Action  
 Try requesting one of the supported languages for Local Database Runtime error messages, or a default language.  
  
  
