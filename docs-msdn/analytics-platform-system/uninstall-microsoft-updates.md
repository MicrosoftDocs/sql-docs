---
title: "Uninstall Microsoft Updates (Analytics Platform System)"
ms.custom: na
ms.date: 01/05/2017
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: df61570a-210d-4154-822f-98acd721f075
caps.latest.revision: 19
author: BarbKess
---
# Uninstall Microsoft Updates
This topic describes how to uninstall a previously installed Microsoft update on the Analytics Platform System appliance.  
  
## Before You Begin  
  
### Prerequisites  
To perform these steps, you will need:  
  
-   A Analytics Platform System login with permissions to access the Admin Console to monitor the appliance.  
  
-   Knowledge of the Fabric Domain Administrator account to login to the *<Fabric Domain>***-HST01** node.  
  
## <a name="HowToUninstallMSFT"></a>To uninstall Microsoft updates  
  
1.  Login to the *<Fabric Domain>***-HST01** node as the Fabric Domain Administrator.  
  
2.  To uninstall all updates that are approved for WSUS to uninstall, open a Command Prompt window and enter the following command. Replace the placeholder items *<  >* with the appropriate information.  
  
    ```  
    C:\pdwinst\media\setup.exe /action="RemoveMicrosoftUpdate" /DomainAdminPasswords="<password>"  
    ```  
  
## See Also  
[Download and Apply Microsoft Updates &#40;Analytics Platform System&#41;](download-and-apply-microsoft-updates.md)  
[Apply Analytics Platform System Hotfixes &#40;Analytics Platform System&#41;](apply-analytics-platform-system-hotfixes.md)  
[Uninstall Analytics Platform System Hotfixes &#40;Analytics Platform System&#41;](uninstall-analytics-platform-system-hotfixes.md)  
[Software Servicing &#40;Analytics Platform System&#41;](software-servicing.md)  
  
