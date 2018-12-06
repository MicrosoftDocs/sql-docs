---
title: "Configuring Virtual Servers on IIS | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: 11/09/2018
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "virtual servers in RDS [ADO]"
ms.assetid: 2b4786c6-40c4-4ce1-9ad4-03df436e0aff
author: MightyPen
ms.author: genemi
manager: craigg
---
# Configuring Virtual Servers on IIS
When creating virtual servers in Internet Information Services 4.0, the following two extra steps are needed in order to configure the virtual server to work with RDS:  
  
1.  When setting up the server, check "Allow Execute Access."  
  
2.  Move msadcs.dll to *vroot*\msadc, where *vroot* is the home directory of your virtual server.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## See Also  
 [RDS Fundamentals](../../../ado/guide/remote-data-service/rds-fundamentals.md)


