---
title: "Enable the Lock Pages in Memory Option (Windows) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "Lock Pages in Memory option"
ms.assetid: cd581fbc-4747-439e-87f9-2f18e39c5bb9
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Enable the Lock Pages in Memory Option (Windows)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This Windows policy determines which accounts can use a process to keep data in physical memory, preventing the system from paging the data to virtual memory on disk.  
  
> [!NOTE]  
>  Locking pages in memory may boost performance when paging memory to disk is expected.  
  
 Use the Windows Group Policy tool (gpedit.msc) to enable this policy for the account used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You must be a system administrator to change this policy.  
  
### To enable the lock pages in memory option  
  
1.  On the **Start** menu, click **Run**. In the **Open** box, type **gpedit.msc**.  
  
2.  On the **Local Group Policy Editor** console, expand **Computer Configuration**, and then expand **Windows Settings**.  
  
3.  Expand **Security Settings**, and then expand **Local Policies**.  
  
4.  Select the **User Rights Assignment** folder.  
  
     The policies will be displayed in the details pane.  
  
5.  In the pane, double-click **Lock pages in memory**.  
  
6.  In the **Local Security Setting - Lock pages in memory** dialog box, click **Add User or Group**.  
  
7.  In the **Select Users, Service Accounts, or Groups** dialog box, select the SQL Server Service account.  
  
8.  Restart the SQL Server Service for this setting to take effect.
  
## See Also  
 [Server Memory Server Configuration Options](../../database-engine/configure-windows/server-memory-server-configuration-options.md)  
  
  
