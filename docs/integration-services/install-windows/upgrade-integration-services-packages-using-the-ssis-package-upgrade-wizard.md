---
description: "Upgrade Integration Services Packages Using the SSIS Package Upgrade Wizard"
title: "Upgrade Integration Services Packages Using the SSIS Package Upgrade Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Integration Services packages, upgrading"
  - "upgrading Integration Services packages"
ms.assetid: 9359275a-48f5-4d1e-8ae7-e797759e3ccf
author: chugugrace
ms.author: chugu
---
# Upgrade Integration Services Packages Using the SSIS Package Upgrade Wizard

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  You can upgrade packages that were created in earlier versions of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] format that [!INCLUDE[ssSQL19](../../includes/sssql19-md.md)] uses. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Package Upgrade Wizard to help in this process. Because you can configure the wizard to backup up your original packages, you can continue to use the original packages if you experience upgrade difficulties.  
  
 The [!INCLUDE[ssIS](../../includes/ssis-md.md)] Package Upgrade Wizard is installed when [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] is installed.  
  
> [!NOTE]  
>  The [!INCLUDE[ssIS](../../includes/ssis-md.md)] Package Upgrade Wizard is available in the Standard, Enterprise, and Developer Editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 For more information about how to upgrade [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages, see [Upgrade Integration Services Packages](../../integration-services/install-windows/upgrade-integration-services-packages.md).  
  
 The remainder of this topic describes how to run the wizard and back up the original packages.  
  
## Running the SSIS Package Upgrade Wizard  
 You can run the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Package Upgrade Wizard from [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], or at the command prompt.  
  
#### To run the wizard from SQL Server Data Tools  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], create or open an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project.  
  
2.  In Solution Explorer, right-click the **SSIS Packages** node, and then click **Upgrade All Packages** to upgrade all the packages under this node.  
  
    > [!NOTE]  
    >  When you open an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains [!INCLUDE[ssISversion10](../../includes/ssisversion10-md.md)] or later packages, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] automatically opens the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Package Upgrade Wizard.  
  
#### To run the wizard from SQL Server Management Studio  
  
-   In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], expand the **Stored Packages** node, and right-click the **File System** or **MSDB** node, and then click **Upgrade Packages**.  
  
#### To run the wizard at the command prompt  
  
-   At the command prompt, run the SSISUpgrade.exe file from the **C:\Program Files\Microsoft SQL Server\130\DTS\Binn** folder.  
  
## Backing Up the Original Packages  
 To back up the original packages, both the original packages and the upgraded packages must be stored in the same folder in the file system. Depending on how you run the wizard, this storage location might be automatically selected.  
  
-   When you run the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Package Upgrade Wizard from [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], the wizard automatically stores both the original packages and upgraded packages in the same folder in the file system.  
  
-   When you run the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Package Upgrade Wizard from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or at the command prompt, you can specify different storage locations for the original and upgraded packages. To back up the original packages, make sure to specify that both the original and upgraded packages are stored in the same folder in the file system. If you specify any other storage options, the wizard will not be able to back up the original packages.  
  
 When the wizard backs up the original packages, the wizard stores a copy of the original packages in an **SSISBackupFolder** folder. The wizard creates this **SSISBackupFolder** folder as a subfolder to the folder that contains the original packages and the upgraded packages.  
  
#### To back up the original packages in SQL Server Management Studio or at the command prompt  
  
1.  Save the original packages to a location on the file system.  
  
    > [!NOTE]  
    >  The backup option in the wizard only works with packages that have been stored to the file system.  
  
2.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or at the command prompt, run the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Package Upgrade Wizard.  
  
3.  On the **Select Source Location** page of the wizard, set the **Package source** property to **File System**.  
  
4.  On the **Select Destination Location** page of the wizard, select **Save to source location** tosave the upgraded packages to the same location as the original packages.  
  
    > [!NOTE]  
    >  The backup option in the wizard is available only when the upgraded packages are stored in the same folder as the original packages.  
  
5.  On the **Select Package Management Options** page of the wizard, select the **Backup original packages** option.  
  
#### To back up the original packages in SQL Server Data Tools  
  
1.  Save the original packages to a location on the file system.  
  
2.  On the **Select Package Management Options** page of the wizard, select the **Backup original packages** option.  
  
    > [!WARNING]  
    >  The **Backup original packages** option is not displayed when you open a [!INCLUDE[ssISversion10](../../includes/ssisversion10-md.md)] or later project in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], which automatically launches the wizard.  
  
3.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], run the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Package Upgrade Wizard.  
  
  
