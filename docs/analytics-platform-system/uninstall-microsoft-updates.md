---
title: Uninstall Microsoft updates
description: Uninstall Microsoft updates in Analytics Platform System (APS).
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 12/04/2023
ms.service: sql
ms.subservice: data-warehouse
ms.topic: how-to
---

# Uninstall Microsoft updates in Analytics Platform System
This article describes how to uninstall a previously installed Microsoft update on the Analytics Platform System appliance.  
  
## Before You Begin
  
### Prerequisites
To perform these steps, you will need:  
  
-   An Analytics Platform System login with permissions to access the Admin Console to monitor the appliance.  
  
-   Knowledge of the Fabric Domain Administrator account to log in to the *\<Fabric Domain\>***-HST01** node.  
  
## <a id="HowToUninstallMSFT"></a> Uninstall Microsoft updates
  
1. Log in to the *\<Fabric Domain\>***-HST01** node as the Fabric Domain Administrator.  
  
1. To uninstall all updates that are approved for WSUS to uninstall, open a Command Prompt window and enter the following command. Replace the placeholder items `<  >` with the appropriate information.  
  
    ```console
    C:\pdwinst\media\setup.exe /action="RemoveMicrosoftUpdate" /DomainAdminPasswords="<password>"  
    ```  
  
## Related content

- [Download and apply Microsoft updates for Analytics Platform System](download-and-apply-microsoft-updates.md)
- [Apply Analytics Platform System hotfixes](apply-analytics-platform-system-hotfixes.md)
- [Uninstall Analytics Platform System hotfixes](uninstall-analytics-platform-system-hotfixes.md)
- [Software servicing in Analytics Platform System](software-servicing.md)
