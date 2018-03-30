---
title: "ConnectionPoolSize Property (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "article"
api_name: 
  - "ConnectionPoolSize"
api_location: 
  - "reportingservices.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "ConnectionPoolSize property"
ms.assetid: b80c8e5d-b725-4fe4-aec6-02fb18ec4434
caps.latest.revision: 17
author: "douglaslM"
ms.author: "carlasab"
manager: "mblythe"
---
# ConnectionPoolSize Property (WMI MSReportServer_ConfigurationSetting)
  The connection pool size used by the report server to communicate with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that hosts the report server database. Read-only.  
  
## Syntax  
  
```vb  
Public Dim ConnectionPoolSize As UInt32  
```  
  
```csharp  
public UInt32 ConnectionPoolSize;  
```  
  
## Property Values  
 A read-only **integer** object that returns a value of `768`.  
  
## Example Code  
 [MSReportServer_ConfigurationSetting Class](../../2014/reporting-services/msreportserver-configurationsetting-class.md)  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](../../2014/reporting-services/msreportserver-configurationsetting-members.md)  
  
  