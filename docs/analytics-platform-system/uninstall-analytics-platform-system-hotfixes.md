---
title: Uninstall hotfixes
description: Uninstall Analytics Platform System hotfixes.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 12/04/2023
ms.service: sql
ms.subservice: data-warehouse
ms.topic: how-to
---

# Uninstall Analytics Platform System hotfixes

The following steps describe how to uninstall a previously installed Analytics Platform System hotfix.  
  
## Before You Begin
  
### Prerequisites
To perform these steps, you will need:  
  
-   An Analytics Platform System login with permissions to access the Admin Console to monitor the appliance.  
  
-   The Domain Administrator account to login to the *<appliance_domain>***-HST01** node.  
  
-   The Knowledge Base article number for the hotfix to uninstall.  
  
## <a id="HowToUninstallPDW"></a> Uninstall a SQL Server PDW hotfix
  
1. Log on to the *<appliance_domain>***-HST01** node as the Fabric Domain Administrator.  
  
1. Use the Run as Administrator option to open a Command Prompt.  
  
1. Change directories to `C:\PDWINST\Patches\<kbarticle>\media` where *\<kbarticle\>* is the Knowledge Base article number for the hotfix to uninstall.  
  
    ```console
    cd /d c:\PDWINST\Patches\<kbarticle>\media  
    ```  
  
1. To remove the hotfix, run the following command.  
  
    ```console
    setup.exe /action="removepatch" /DomainAdminPassword="<password>"  
    ```  
  
## Related content

- [Download and apply Microsoft updates for Analytics Platform System](download-and-apply-microsoft-updates.md)
- [Uninstall Microsoft updates in Analytics Platform System](uninstall-microsoft-updates.md)
- [Apply Analytics Platform System hotfixes](apply-analytics-platform-system-hotfixes.md)
- [Software servicing in Analytics Platform System](software-servicing.md)
