---
description: "LOCALDB_ERROR_UNKNOWN_LANGUAGE_ID"
title: "LOCALDB_ERROR_UNKNOWN_LANGUAGE_ID | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: "reference"
ms.assetid: fa082dca-bf88-46e7-b61e-7ac8835a3493
author: WilliamDAssafMSFT
ms.author: wiassaf
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
  
  
