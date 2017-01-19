---
title: "Browse for Servers (Network Servers) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssms"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.swb.browseservers.network.f1"
ms.assetid: a59ffcd6-4b69-4c5c-9740-699ccb2183fb
caps.latest.revision: 4
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Browse for Servers (Network Servers)
If you are connecting to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] component and you do not know the exact name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] instance, click **Browse for more** in the **Server name** box to open the **Browse for Servers** dialog box.  
  
This dialog is populated by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Browser service on the server computers. There are several reasons why the name of an instance might not appear in the list:  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Browser service might not be running on the computer running [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)].  
  
-   UDP port 1434 might be blocked by a firewall.  
  
-   The **HideInstance** flag might be set.  
  
To connect to an instance that does not appear in the list, type a full connection string for the instance, including the TCP/IP port number or the named pipes pipe name.  
  
## Options  
**Select a SQL Server instance in the network for your connection**  
Designate the server you want to connect to by clicking on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] instance displayed in the tree. You can show or hide portions of the tree view by clicking on the nodes marked with a **+** or **–** symbol.  
  
