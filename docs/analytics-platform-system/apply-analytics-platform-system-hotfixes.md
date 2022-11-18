---
title: Apply Analytics Platform System hotfixes
description: This article discusses how to apply hotfixes to the Analytics Platform System software.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 04/17/2018
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
ms.custom: seo-dt-2019
---

# Apply Analytics Platform System hotfixes
This article discusses how to apply hotfixes to the Analytics Platform System software.  
  
## Before You Begin  
  
> [!WARNING]  
> Do not attempt to apply a Analytics Platform System hotfix if your appliance or any appliance component is down or in a failed over state. In that case, contact support for assistance.  
  
> [!WARNING]  
> Do not apply a Analytics Platform System hotfix while the appliance is in use. Applying a hotfix can cause appliance nodes to reboot. The hotfix should be applied during a maintenance window when the appliance is not being used.  
  
### Prerequisites  
To perform these steps, you will need:  
  
-   An Analytics Platform System login with permissions to access the Admin Console to monitor the appliance state. <!-- MISSING LINKS See [Grant Permissions to Use the Admin Console &#40;SQL Server PDW&#41;](../sqlpdw/grant-permissions-to-use-the-admin-console-sql-server-pdw.md).  -->  
  
-   Knowledge of the Fabric Domain Administrator account to connect to the _<domain_name>_**-HST01** node.  
  
## <a name="HowToInstallPDW"></a>To apply a Analytics Platform System hotfix  
Unlike the Microsoft updates, the hotfixes for the Analytics Platform System software are not handled through WSUS. They have a different workflow and are installed by running a hotfix package.  
  
1.  **Verify appliance state indicators.**  
  
    1.  Open the Admin Console and navigate to the Appliance State page. For more information, see [Monitor the Appliance by Using the Admin Console &#40;Analytics Platform System&#41;](monitor-the-appliance-by-using-the-admin-console.md)  
  
    2.  All red or yellow indicators must be resolved before you proceed to the next step. A couple exceptions to this are:  
  
        -   If there are disk failures, use the Admin Console Alerts page to verify there is no more than one disk failure within each server or SAN array. If there is no more than one disk failure within each server or SAN array, you can proceed to the next step before fixing the disk failure(s). Be sure to contact Microsoft support to fix the disk failure(s) as soon as possible.  
  
        -   If there is a non-critical (yellow) disk volume error that is not on the C:\ drive, you can proceed to the next step before resolving the disk volume error.  
  
2.  **Install the Analytics Platform System hotfix**  
  
    1.  Login to the <*appliance_domain*>-HST01 node as the domain administrator.  
  
    2.  Use the **Run as administrator** option to open a Command Prompt.  
  
    3.  Run the following command, replacing *\<HotfixPackageName\>* with the name of the hotfix executable package, and replacing the other placeholder items *<  >* with the appropriate information.  
  
        ```  
        <HotfixPackageName> /DomainAdminPassword="<password>"  
        ```  
  
    4.  Follow the steps as presented by the hotfix package.  
  
## See Also  
[Download and Apply Microsoft Updates &#40;Analytics Platform System&#41;](download-and-apply-microsoft-updates.md)  
[Uninstall Microsoft Updates &#40;Analytics Platform System&#41;](uninstall-microsoft-updates.md)  
[Uninstall Analytics Platform System Hotfixes &#40;Analytics Platform System&#41;](uninstall-analytics-platform-system-hotfixes.md)  
[Software Servicing &#40;Analytics Platform System&#41;](software-servicing.md)  
  
