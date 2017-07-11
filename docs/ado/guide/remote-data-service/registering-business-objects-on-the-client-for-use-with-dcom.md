---
title: "Registering Business Objects on the Client for Use with DCOM | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "business objects in RDS [ADO]"
ms.assetid: 75a21910-607f-463a-ae18-a17130dafb7e
caps.latest.revision: 16
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Registering Business Objects on the Client for Use with DCOM
Custom business objects need to ensure that the client side can map their program name (ProgId) to an identifier (CLSID) that can be used over DCOM. For this reason, the ProgID of the DCOM object must be in the client-side registry and map to the class ID of the server-side business object. For the other supported protocols (HTTP, HTTPS, and in-process), this is not necessary.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/en-us/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](http://go.microsoft.com/fwlink/?LinkId=199565).  
  
 For example, if you expose a server-side business object called MyBObj with a specific class ID, for instance, "{00112233-4455-6677-8899-00AABBCCDDEE}", make sure that the following entries are added to the client-side registry:  
  
```  
[HKEY_CLASSES_ROOT]  
\MyBObj\Clsid\(Default) "{00112233-4455-6677-8899-00AABBCCDDEE}"  
```


