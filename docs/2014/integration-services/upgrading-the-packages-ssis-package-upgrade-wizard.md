---
title: "Upgrading the Packages (SSIS Package Upgrade Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.is.upgradewizard.upgradingpackage.f1"
ms.assetid: cdb842e3-2e59-4ede-b127-be4fde46875c
author: janinezhang
ms.author: janinez
manager: craigg
---
# Upgrading the Packages (SSIS Package Upgrade Wizard)
  Use the **Upgrading the Packages** page to view the progress of package upgrade and to interrupt the upgrade process. The [!INCLUDE[ssIS](../includes/ssis-md.md)] Package Upgrade Wizard upgrades the selected packages one by one.  
  
 **To view upgraded packages that were saved to a SQL Server database or to the package store**  
  
-   In [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)], in Object Explorer, connect to the local instance of [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)], and then expand the **Stored Packages** node to see the packages that were upgraded.  
  
 **To view upgraded packages that were upgraded from SQL Server Data Tools**  
  
-   In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], in Solution Explorer, open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project, and then expand the **SSIS Packages** node to see the upgraded packages.  
  
## Options  
 **Message pane**  
 Displays progress messages and summary information during the upgrade process.  
  
 **Action**  
 View the actions in the upgrade.  
  
 **Status**  
 View the result of each action.  
  
 **Message**  
 View the error messages that each action generates.  
  
 **Stop**  
 Stop the package upgrade.  
  
 **Report**  
 Select what you want to do with the report that contains the results of the package upgrade:  
  
-   View the report online.  
  
-   Save the report to a file.  
  
-   Copy the report to the Clipboard  
  
-   Send the report as an e-mail message.  
  
## See Also  
 [Upgrade Integration Services Packages](install-windows/upgrade-integration-services-packages.md)  
  
  
