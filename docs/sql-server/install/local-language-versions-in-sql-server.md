---
title: "Local Language Versions in SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "08/23/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
ms.assetid: 20b99363-0490-4aa3-9a3d-262f827d81e8
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Local Language Versions in SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports all languages that are supported by Windows operating systems.  
  
## Cross-Language Support  
  
-   The English-language version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is supported on all localized versions of operating systems.  
  
-   Localized versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are supported on localized operating systems with the corresponding language or on English-language versions of supported operating systems by using the Windows Multilingual User Interface Pack (MUI) settings. For more information, see [Configure Operating System to Support Localized Versions](../../sql-server/install/local-language-versions-in-sql-server.md#BK_ConfigureOS).  
  
-   Localized versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can only be upgraded to localized versions of the same language, and cannot be upgraded to the English-language version.  
  
-   Localized versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can also be installed side by side with English-language instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
##  <a name="BK_ConfigureOS"></a> Configure Operating System to Support Localized Versions  
 Localized versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are supported on English-language versions of supported operating systems through the use of Windows Multilingual User Interface Pack (MUI) settings.  
  
 However, you must verify certain operating system settings before installing a localized version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a server that is running an English-language operating system with a non-English MUI setting. You need to verify that the following operating system settings match the language of the localized [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to be installed:  
  
-   The operating system user interface setting  
  
-   The operating system user locale setting  
  
-   The system locale setting  
  
 If the settings do not match the language of the localized [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to be installed, then use the following procedures to correctly set these operating system settings.  
  
> [!CAUTION]  
>  Installations of different language versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances on the same computer are not supported.  
  
#### To change the operating system user interface setting  
  
1.  If not already installed, install the operating system MUI that matches your localized version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
2.  In Control Panel, open **Regional and Language Options**.  
  
3.  On the **Languages** tab, for **Language used in menus and dialogs**, select a value from the list.  
  
     This setting will affect the user interface language of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], so it must match your localized version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
4.  Click **Apply** to confirm the change, and **OK** to close the window.  
  
#### To change the operating system user locale setting  
  
1.  If not already installed, install the operating system MUI that matches your localized version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
2.  In Control Panel, open **Regional and Language Options**.  
  
3.  On the **Regional Options** tab, for **Select an item to match its preferences**, select a value from the list.  
  
     This setting will affect culture-specific data formatting.  
  
4.  Click **Apply** to confirm the change, and **OK** to close the window.  
  
#### To change the system locale setting  
  
1.  If not already installed, install the operating system MUI that matches your localized version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
2.  In Control Panel, open **Regional and Language Options**.  
  
3.  On the **Advanced** tab, for **Select a language to match the language version of the non-Unicode programs you want to use**, select a value from the list.  
  
     This setting will allow [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup to choose the best default collation for your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation.  
  
4.  Click **Apply** to confirm the change, and **OK** to close the window.  
  
## See Also  
 [Hardware and Software Requirements for Installing SQL Server](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)   
 [Install SQL Server](../../database-engine/install-windows/install-sql-server.md)  
  
  
