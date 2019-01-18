---
title: "Considerations for Installing SQL Server Using SysPrep | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
ms.assetid: e1792eeb-2874-4653-b20e-3063f4eb4e5d
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Considerations for Installing SQL Server Using SysPrep
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SysPrep allows you to prepare a stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a computer and to complete the configuration at a later time. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SysPrep involves a two-step process to get to a configured stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The steps include the following:  
  
-   [Prepare Image](#BKMK_PrepareImage)  
  
     This step stops the installation process after the product binaries are installed, without configuring the computer, network, or account-specific information for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is being prepared.  
  
-   [Complete Image](#BKMK_CompleteImage)  
  
     This step enables you to complete the configuration of a prepared instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. During this step, you can provide the computer, network, and account-specific information.  
  
 For more information about how to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using SysPrep, see [Install SQL Server 2014 Using SysPrep](install-sql-server-using-sysprep.md).  
  
## Common Uses for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SysPrep  
 You can use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SysPrep capability in any of the following ways:  
  
-   By using the Prepare Image step, you can prepare one or more unconfigured instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the same computer. You can configure these prepared instances by using the Complete Image step on the same computer.  
  
-   You can capture the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup configuration file of the prepared instance and use it to prepare additional unconfigured [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances on multiple computers for later configuration.  
  
-   In combination with the Windows System Preparation tool (also known as Windows SysPrep); you can create an image of the operating system including the unconfigured prepared instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the source computer. You can then deploy the operating system image to multiple computers. After you complete the configuration of the operating system, you can configure the prepared instances by using the Complete Image step of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup.  
  
     The Windows SysPrep tool is used to prepare Windows operating system images. It is used to capture a customized image of the operating system for deployment throughout an organization. For more information about SysPrep and its uses, see [What is SysPrep](https://go.microsoft.com/fwlink/?LinkId=143546).  
  
## Installation Media Considerations  
 If you are using a full version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], consider the following:  
  
-   Non-Express editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
    -   The Prepare Image step uses [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Evaluation edition to install the product binaries. When the instance is completed, the edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] depends on the product ID provided during the complete image step.  
  
    -   If you provide an Evaluation edition product ID, the evaluation period is set to expire 180 days after the prepared instance is completed.  
  
-   Express editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
    -   To prepare an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Express editions, use the Express installation media.  
  
    -   You cannot specify Product IDs for a prepared instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Express editions.  
  
## Supported [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installations  
 SysPrep in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] supports all features, including tools, of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 You can prepare multiple instances for side-by-side installations of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] or earlier versions. The features of these instances must support SysPrep.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client is automatically installed and completed at the end of the prepare image step.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Writer are automatically prepared when you prepare an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. They are completed when you complete the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance by using the Complete Image step.  
  
 For information about supported editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
 You can perform an edition upgrade while configuring a prepared instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This option is not supported for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Express editions.  
  
 Beginning in [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SysPrep supports [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster installations from the command line.  
  
## [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SysPrep Limitations  
 Repairing a prepared instance is not supported. If Setup fails during the Prepare Image or Complete Image step, you must run uninstall.  
  
##  <a name="BKMK_PrepareImage"></a> Prepare Image  
 The Prepare Image step installs the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] product and features but does not configure the installation.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features to be installed and the installation location for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] product installation files can be specified during this step. You can prepare an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] either through the **Image Preparation of a stand-alone instance for SysPrep deployment** on the **Advanced** page of the **Installation Center** or from the command prompt.  
  
-   You can prepare multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the same computer that can be completed later.  
  
-   You can add or remove features that are supported for SysPrep installations from the existing prepared instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 After the instance is prepared, a shortcut on the **Start** menu becomes available to complete the configuration of the prepared instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
##  <a name="BKMK_CompleteImage"></a> Complete Image  
 You can complete the prepared instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using either of the following methods:  
  
-   Use the shortcut on the Start menu.  
  
-   Access the **Image completion of a prepared stand-alone instance** step on the **Advanced** page of the **Installation Center**.  
  
## See Also  
 [Planning a SQL Server Installation](../../sql-server/install/planning-a-sql-server-installation.md)  
  
  
