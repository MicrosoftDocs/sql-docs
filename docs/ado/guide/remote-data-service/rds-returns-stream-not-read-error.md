---
title: "RDS Returns &quot;Stream Not Read&quot; Error"
description: "RDS Returns &quot;Stream Not Read&quot; Error"
author: rothja
ms.author: jroth
ms.date: 11/09/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "stream not read error in RDS [ADO]"
---
# RDS Returns &quot;Stream Not Read&quot; Error
"The Stream object could not be read because it is empty, or the current position is at the end of the Stream. For non-empty Streams, set the current position with the Position property. To determine if a Stream is empty, check the Size property."  
  
 If you see this error message, you may have tried to use a parameterized hierarchical query over http. RDS does not permit you to use remote parameterized hierarchies.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
## See Also  
 [RDS Fundamentals](./rds-fundamentals.md)