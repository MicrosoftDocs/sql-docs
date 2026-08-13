---
title: Apply Analytics Platform System Hotfixes
description: Analytics Platform System hotfixes install differently than Microsoft updates. Learn the prerequisites, warnings, and commands to run the hotfix package correctly.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 12/04/2023
ms.service: sql
ms.subservice: data-warehouse
ms.topic: how-to
ms.custom: sfi-ropc-nochange
---

# Apply Analytics Platform System hotfixes
This article explains how to apply hotfixes to the Analytics Platform System software.  
  
## Before you begin
  
> [!WARNING]  
> Don't attempt to apply an Analytics Platform System hotfix if your appliance or any appliance component is down or in a failed state. In that case, contact support for assistance.  
  
> [!WARNING]  
> Don't apply an Analytics Platform System hotfix while the appliance is in use. Applying a hotfix can cause appliance nodes to restart. Apply the hotfix during a maintenance window when the appliance isn't being used.  
  
### Prerequisites
To perform these steps, you need:  
  
-   An Analytics Platform System login with permissions to access the Admin Console to monitor the appliance state. <!-- MISSING LINKS See [Grant Permissions to Use the Admin Console (SQL Server PDW)](../sqlpdw/grant-permissions-to-use-the-admin-console-sql-server-pdw.md).  -->  
  
-   Knowledge of the Microsoft Fabric Domain Administrator account to connect to the _<domain_name>_**-HST01** node.  
  
## <a id="HowToInstallPDW"></a> Apply an analytics Platform System hotfix
Unlike Microsoft updates, WSUS doesn't handle hotfixes for the Analytics Platform System software. These hotfixes use a different workflow. To install them, run a hotfix package.  
  
1. **Verify appliance state indicators.**  
  
    1. Open the Admin Console and go to the Appliance State page. For more information, see [Monitor the Appliance by Using the Admin Console (Analytics Platform System)](monitor-the-appliance-by-using-the-admin-console.md).  
  
    1. Resolve all red or yellow indicators before you proceed to the next step. A couple exceptions to this rule are:  
  
        -   If there are disk failures, use the Admin Console **Alerts** page to verify there's no more than one disk failure within each server or SAN array. If there's no more than one disk failure within each server or SAN array, you can proceed to the next step before fixing the disk failures. Be sure to contact Microsoft support to fix the disk failures as soon as possible.  
  
        -   If there's a noncritical (yellow) disk volume error that isn't on the C:\ drive, you can proceed to the next step before resolving the disk volume error.  
  
1. **Install the Analytics Platform System hotfix**  
  
    1. Sign in to the <*appliance_domain*>-HST01 node as the domain administrator.  
  
    1. Use the **Run as administrator** option to open a Command Prompt.  
  
    1. Run the following command. Replace *\<HotfixPackageName\>* with the name of the hotfix executable package. Replace the other placeholder items *<  >* with the appropriate information.  
  
        ```xml
        <HotfixPackageName> /DomainAdminPassword="<password>"  
        ```  
  
    1. Follow the steps as presented by the hotfix package.  
  
## Related content

- [Download and apply Microsoft updates for Analytics Platform System](download-and-apply-microsoft-updates.md)
- [Uninstall Microsoft updates in Analytics Platform System](uninstall-microsoft-updates.md)
- [Uninstall Analytics Platform System hotfixes](uninstall-analytics-platform-system-hotfixes.md)
- [Software servicing in Analytics Platform System](software-servicing.md)
