---
title: "Install SQL Server Using a Configuration File | Microsoft Docs"
ms.custom: ""
ms.date: "09/07/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
ms.assetid: a832153a-6775-4bed-83f0-55790766d885
author: MashaMSFT
ms.author: mathoma
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
manager: craigg
---
# Install SQL Server using a configuration file

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]
 
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup provides the ability to generate a configuration file based upon the system default and run-time inputs. You can use the configuration file to deploy [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] throughout the enterprise with the same configuration. You can also standardize manual installations throughout the enterprise, by creating a batch file that launches Setup.exe. 
 
This article is specifically updated for SQL Server 2016 and SQL Server 2017. For older versions of SQL Server, see [Install SQL Server 2014 Using a Configuration File](../../2014/database-engine/install-windows/install-sql-server-using-a-configuration-file).
 
Setup supports the use of the configuration file only through the command prompt. The processing order of the parameters while using the configuration file is outlined below:  
  
-  The configuration file overwrites the defaults in a package  
  
-   Command-line values overwrite the values in the configuration file  
  
 The configuration file can be used to track the parameters and values for each installation. This makes the configuration file useful for verifying and auditing the installations. 
  
## Configuration file structure  
 The ConfigurationFile.ini file is a text file with parameters (name/value pair) and descriptive comments. 
  
 The following is an example of a ConfigurationFile.ini file:  
  
```  
; Microsoft SQL Server Configuration file  
[OPTIONS]  
; Specifies a Setup work flow, like INSTALL, UNINSTALL, or UPGRADE.  
; This is a required parameter.  
ACTION="Install"  
; Specifies features to install, uninstall, or upgrade.  
; The list of top-level features include SQL, AS, RS, IS, and Tools.  
; The SQL feature will install the database engine, replication, and full-text.  
; The Tools feature will install Management Tools, Books online,   
; SQL Server Data Tools, and other shared components.  
FEATURES=SQL,Tools  
```  
  
#### How to generate a configuration file  
  
1. Insert the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation media. From the root folder, double-click Setup.exe. To install from a network share, locate the root folder on the share, and then double-click Setup.exe. 
  
    > [!NOTE]  
    >  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Express Edition setup does not create a configuration file automatically. The following command will start  setup and create a configuration file. 
    >   
    >  SETUP.exe /UIMODE=Normal /ACTION=INSTALL  
  
2. Follow the wizard through to the **Ready to Install** page. The path to the configuration file is specified in the **Ready to Install** page in the configuration file path section. For more information about how to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Install SQL Server from the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md). 
  
3. Cancel the setup without actually completing the installation, to generate the INI file. 
  
    > [!NOTE]  
    >  The setup infrastructure writes out all the appropriate parameters for the actions that were run, with the exception of sensitive information such as passwords. The /IAcceptSQLServerLicenseTerms parameter is also not written out to the configuration file and requires either a modification of the configuration file or a value to be supplied at the command prompt. For more information, see [Install SQL Server from the Command Prompt](../../database-engine/install-windows/install-sql-server-2016-from-the-command-prompt.md). In addition, a value is included for Boolean parameters where a value is usually not supplied through the command prompt. 
  
## Using the configuration file to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  

You can only use the configuration file on command-line installations. 
  
> [!NOTE]  
> If you need to make changes to the configuration file, we recommend that you make a copy and work with the copy. 
  
### How to use a configuration file to install a stand-alone [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance  
  
-   Run the installation through the command prompt and supply the ConfigurationFile.ini using the *ConfigurationFile* parameter. 
  
### How to use a configuration file to prepare and complete an image of a stand-alone [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance (SysPrep)  
  
1. To prepare one or more instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and configure them on the same machine. 
  
    - Run **Image preparation of a stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]** from the **Advanced** page of the Installation Center and capture the prepare image configuration file. 
  
    - Use the same prepare image configuration file as a template to prepare more instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. 
  
    - Run **Image completion of a prepared stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]** from the **Advanced** page of the Installation Center to configure a prepared instances on the machine. 
  
2. To prepare an image of the operating system including an unconfigured prepared instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], using Windows SysPrep tool. 
  
    -   Run the **Image preparation of a stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]** from the Advanced page of the Installation Center and capture the prepare image configuration file. 
  
    -   Run the **Image completion of a prepared stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]** from the **Advanced** page of the Installation Center, but cancel it on the **Ready to Complete** page after capturing the complete configuration file. 
  
    -   The complete image configuration file can be stored with the Windows image for automating the configuration of the prepared instances. 
  
### How to install a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster using the configuration file  
  
1. Integrated Install option (create a single node failover cluster on a node and for additional nodes, run AddNode on them):  
  
    -   Run the "Install a Failover Cluster" option and capture the configuration file that lists all the installation settings. 
  
    -   Run the command-line failover cluster install by supplying the *ConfigurationFile* parameter. 
  
    -   On an additional node to be added, run AddNode to capture the ConfigurationFile.ini file applicable to the existing failover cluster. 
  
    -   Run the command-line AddNode on all the additional nodes that will join the failover cluster, by supplying the same configuration file using the ConfigurationFile parameter. 
  
2. Advanced install option (prepare failover cluster on all failover cluster nodes, then, after preparing all the nodes, run complete on the node that owns the shared disk):  
  
    -   Run **Prepare** on one of the nodes, and capture the ConfigurationFile.ini file. 
  
    -   Supply the same ConfigurationFile.ini file to Setup on all the nodes that will be prepared for the failover cluster. 
  
    -   After all the nodes have been prepared, run a complete failover cluster operation on the node that owns the shared disk and capture the ConfigurationFile.ini file. 
  
    -   You can then supply this ConfigurationFile.ini file to complete the failover cluster. 
  
### How to add or remove a node to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster using the configuration file  
  
-   If you have a configuration file that was previously used to add a node to or remove a node from a failover cluster, you can reuse that same file to add or remove additional nodes. 
  
### How to upgrade a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster using the configuration file  
  
1. Run upgrade on the passive node and capture the ConfigurationFile.ini file. You can do this either by performing the actual upgrade, or exiting at the end without doing the actual upgrade. 
  
2. On all the additional nodes to be upgraded, supply the ConfigurationFile.ini file to complete the process. 
  
## Sample syntax  
 Following are some examples on how to use the configuration file:  
  
-   To specify the configuration file at the command prompt:  
  
```  
Setup.exe /ConfigurationFile=MyConfigurationFile.INI  
```  
  
-   To specify passwords at the command prompt instead of in the configuration file:  
  
```  
Setup.exe /SQLSVCPASSWORD="************" /AGTSVCPASSWORD="************" /ASSVCPASSWORD="************" /ISSVCPASSWORD="************" /RSSVCPASSWORD="************" /ConfigurationFile=MyConfigurationFile.INI  
```  
  
## See also  
 [Install SQL Server from the Command Prompt](../../database-engine/install-windows/install-sql-server-2016-from-the-command-prompt.md)   
 [SQL Server Failover Cluster Installation](../../sql-server/failover-clusters/install/sql-server-failover-cluster-installation.md)   
 [Upgrade a SQL Server Failover Cluster Instance](../../sql-server/failover-clusters/windows/upgrade-a-sql-server-failover-cluster-instance.md)  
  
  
