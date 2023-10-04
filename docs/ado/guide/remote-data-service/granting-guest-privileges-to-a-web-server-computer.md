---
title: "Granting Guest Privileges to a Web Server Computer"
description: "Granting Guest Privileges to a Web Server Computer"
author: rothja
ms.author: jroth
ms.date: 11/09/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "guest privileges in RDS [ADO]"
---
# Granting Guest Privileges to a Web Server Computer
The anonymous Web server account (IUSR_*ComputerName*) must be added to the Guests local group on the Web server computer to use RDS.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
### To grant guest privileges to a Web server computer  
  
1.  On your Microsoft Windows 2000 Server computer, click **Start**, point to **Programs**, point to **Administrative Tools**, and then click **Computer Management**.  
  
2.  In the console tree, in **Local Users and Groups**, click **Groups**.  
  
3.  Select the **Guests** local group. From the **Action** menu, choose **Properties**.  
  
4.  In the **Guests Properties** dialog box, click **Add**.  
  
5.  If the anonymous Web server account does not appear in the list in the **Select Users or Groups** dialog box, type its name (IUSR_*ComputerName*) into the bottom blank box, and then click **Add**.  
  
6.  Click **OK**.