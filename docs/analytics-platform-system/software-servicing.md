---
title: Software servicing
description: Software servicing in Analytics Platform System (APS).
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 12/04/2023
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
---

# Software servicing in Analytics Platform System
This section summarizes the software servicing requirements for Analytics Platform System appliances, including WSUS and Analytics Platform System hotfixes.  
  
## <a id="Basics"></a> Software servicing basics
**WSUS:** Your Analytics Platform System appliance needs to be configured to receive updates from Windows Server Update Services (WSUS). These updates include important changes to appliance software. After they are configured, many updates will install automatically and do not require hands-on management. Typically, WSUS updates are configured during the [Configure Windows Server Update Services (WSUS) (Analytics Platform System)](configure-windows-server-update-services-wsus.md) step performed during new appliance setup. If not, this configuration step can be performed later. For information on WSUS, see the [WSUS website Guide](/windows/deployment/deploy-whats-new).  
  
**Hotfixes:** Additionally, you might need to apply Analytics Platform System hotfixes. A *hotfix* is a software update that is created for a specific customer to resolve an issue with the Analytics Platform System software. Each hotfix is an executable file that installs the fix for the customer-specific issue. Each hotfix also contains an accumulation of all the previously released software updates for Windows, SQL Server, and Analytics Platform System. If you need to install a hotfix, Microsoft support will provide you with the hotfix and instructions.  
  
**Scope of Updates:** Applying a hotfix or service pack to the Analytics Platform System must take the entire appliance offline.  
  
**SSIS Destination Adapter and Client Tools:** When applying a hotfix that includes changes to the SSIS Destination Adapter MSI's or Client Tools MSI, the MSI files will be updated in the `C:\PDWINST\ClientTools` directory on the Control node. The hotfix does not automatically install the components from the updated MSI files. To update those components, the customer must uninstall the old versions of the components, and install the new versions from the updated MSI files. When uninstalling a hotfix that includes changes to the SSIS Destination Adapter MSI's or Client Tools MSI, the MSI files for those components will be reverted to the previous versions. To revert those components to a previous version, the customer must uninstall the existing (newer) versions of the components, and reinstall the older versions from the reverted MSI files.  
  
## Software servicing topics
The following topics describe how to manage software servicing on the appliance:  
  
-   [Download and Apply Microsoft Updates (Analytics Platform System)](download-and-apply-microsoft-updates.md)  
  
-   [Uninstall Microsoft Updates (Analytics Platform System)](uninstall-microsoft-updates.md)  
  
-   [Apply Analytics Platform System Hotfixes (Analytics Platform System)](apply-analytics-platform-system-hotfixes.md)  
  
-   [Uninstall Analytics Platform System Hotfixes (Analytics Platform System)](uninstall-analytics-platform-system-hotfixes.md)  
  
## Related content

- [Monitor the appliance with system views - Analytics Platform System](monitor-the-appliance-by-using-system-views.md)
- [Microsoft Analytics Platform System](home-analytics-platform-system-aps-pdw.md)