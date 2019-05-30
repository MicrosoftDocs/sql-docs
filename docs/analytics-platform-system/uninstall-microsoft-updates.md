---
title: Uninstall Microsoft updates - Analytics Platform System | Microsoft Docs"
description: Uninstall Microsoft updates in Analytics Platform System (APS).
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
---

# Uninstall Microsoft updates in Analytics Platform System
This article describes how to uninstall a previously installed Microsoft update on the Analytics Platform System appliance.  
  
## Before You Begin  
  
### Prerequisites  
To perform these steps, you will need:  
  
-   An Analytics Platform System login with permissions to access the Admin Console to monitor the appliance.  
  
-   Knowledge of the Fabric Domain Administrator account to log in to the <em><Fabric Domain></em>**-HST01** node.  
  
## <a name="HowToUninstallMSFT"></a>To uninstall Microsoft updates  
  
1.  Log in to the <em><Fabric Domain></em>**-HST01** node as the Fabric Domain Administrator.  
  
2.  To uninstall all updates that are approved for WSUS to uninstall, open a Command Prompt window and enter the following command. Replace the placeholder items *<  >* with the appropriate information.  
  
    ```  
    C:\pdwinst\media\setup.exe /action="RemoveMicrosoftUpdate" /DomainAdminPasswords="<password>"  
    ```  
  
## Next steps
For more information, see:
- [Download and Apply Microsoft Updates &#40;Analytics Platform System&#41;](download-and-apply-microsoft-updates.md) 
- [Apply Analytics Platform System Hotfixes &#40;Analytics Platform System&#41;](apply-analytics-platform-system-hotfixes.md)  
- [Uninstall Analytics Platform System Hotfixes &#40;Analytics Platform System&#41;](uninstall-analytics-platform-system-hotfixes.md)  
- [Software Servicing &#40;Analytics Platform System&#41;](software-servicing.md)  
  
