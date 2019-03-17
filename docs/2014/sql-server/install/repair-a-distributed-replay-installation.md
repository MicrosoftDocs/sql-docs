---
title: "Repair a Distributed Replay Installation | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 6fcd8ca8-1ceb-457d-9545-096c74e274d7
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Repair a Distributed Replay Installation
  Repairing a Distributed Replay component (controller or client) will do the following:  
  
1.  Install all associated files on disk again, and replace any existing files.  
  
2.  Recreate the Windows service account if the corresponding service account was removed.  
  
 You cannot use the Repair operation to add or remove components. To add or remove components, check or uncheck the corresponding component in the Feature tree on the **Feature Selection** page in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup.  
  
### To repair a failed installation of Distributed Replay  
  
1.  From the **Start** menu, click **Control Panel**, and then double-click **Add or Remove Programs**.  
  
2.  Select [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] in the **Uninstall or change a program** window, and then in the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] dialog box, click **Repair**.  
  
3.  Follow the steps in the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] wizard, and on the **Select Features** page, select the Distributed Replay components you want to repair, and then click **Next.**.  
  
4.  Complete the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Installation Wizard to repair the selected Distributed Replay features.  
  
  
