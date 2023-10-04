---
title: "Configuring Virtual Servers on IIS"
description: "Configuring Virtual Servers on IIS"
author: rothja
ms.author: jroth
ms.date: 11/09/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "virtual servers in RDS [ADO]"
---
# Configuring Virtual Servers on IIS
When creating virtual servers in Internet Information Services 4.0, the following two extra steps are needed in order to configure the virtual server to work with RDS:  
  
1.  When setting up the server, check "Allow Execute Access."  
  
2.  Move msadcs.dll to *vroot*\msadc, where *vroot* is the home directory of your virtual server.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
## See Also  
 [RDS Fundamentals](./rds-fundamentals.md)