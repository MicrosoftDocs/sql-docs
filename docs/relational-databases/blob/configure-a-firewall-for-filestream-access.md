---
title: "Configure a Firewall for FILESTREAM Access | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: filestream
ms.topic: conceptual
helpviewer_keywords: 
  - "Windows Firewall [Database Engine], FILESTREAM"
  - "FILESTREAM [SQL Server], Windows Firewall"
ms.assetid: fc52007f-c26f-4f8e-b9d8-55a7978f4d56
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Configure a Firewall for FILESTREAM Access
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  To use FILESTREAM in a firewall-protected environment, both the client and server must be able to resolve DNS names to the server that contains the FILESTREAM files. FILESTREAM requires the Windows file-sharing ports 139 and 445 to be open.  
  
### To open the Windows file-sharing ports on a computer that is running Windows 7  
  
1.  In Control Panel, open **Windows Firewall**.  
  
2.  In the left pane, click **Advanced settings**. If you're prompted for an administrator password or confirmation, type the password or provide confirmation.  
  
3.  In the **Windows Firewall with Advanced Security** dialog box, in the left pane, click **Inbound Rules**, and then, in the right pane, click **New Rule**.  
  
4.  Follow the instructions in the New Inbound Rule wizard to add TCP port 139.  
  
5.  Repeat the previous step to add TCP port 445.  
  
6.  Close the **Windows Firewall with Advanced Security** dialog box.  
  
  
