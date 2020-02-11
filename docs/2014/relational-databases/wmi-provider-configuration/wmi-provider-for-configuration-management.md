---
title: "WMI Provider for Configuration Management Concepts | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
helpviewer_keywords: 
  - "WMI Provider for Configuration Management"
  - "SQL Server WMI Provider"
  - "configuration management [WMI]"
  - "WMI Provider for Configuration Management, about WMI Provider for Configuration Management"
ms.assetid: 7e41db24-b915-4eb8-a1d6-e6948ee915b7
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# WMI Provider for Configuration Management Concepts
  The WMI provider is a published layer that is used with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager snap-in for [!INCLUDE[msCoName](../../includes/msconame-md.md)] Management Console (MMC) and the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager. It provides a unified way for interfacing with the API calls that manage the registry operations requested by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager and provides enhanced control and manipulation over the selected [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] WMI Provider is a DLL and a MOF file, which are compiled automatically by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] WMI Provider contains a set of object classes used to control the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services using the following methods:  
  
-   A script language such as VBScript, [!INCLUDE[jsprjscript](../../includes/jsprjscript-md.md)], or Perl, in which Windows Query Language (WQL) can be embedded.  
  
-   The <xref:Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer> object in an SMO managed code program.  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager or MMC with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] WMI provider snap-in.  
  
## Using a Script Language  
 The advantages of using a script language include the following:  
  
-   A development environment is not required.  
  
-   The files that support the script language are widely available.  
  
 The script can also work with other WMI Providers in addition to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] WMI Provider. A domain administrator can use a script to set up the services, network settings, and alias settings on multiple computers on a network.  
  
 This section deals with accessing the WMI Provider for Configuration Management from scripts in further detail.  
  
## Using the SMO ManagedComputer Object  
 The <xref:Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer> object is a managed SMO object that provides access to the WMI Provider for Configuration Management. By using an SMO program, the <xref:Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer> object can be used to view and modify [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services, network settings, and alias settings. For more information, see [Managing Services and Network Settings by Using WMI Provider](../server-management-objects-smo/tasks/managing-services-and-network-settings-by-using-wmi-provider.md).  
  
## Using the Microsoft Management Console or SQL Server Configuration Manager  
 Microsoft Management Console (MMC) provides an interface to manage [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services, as opposed to a scripting language or managed code program. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management MMC snap-in can be used to stop and start services, and to change service accounts.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager can also be used to manage [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services, client and server protocols, and server aliases  
  
## See Also  
 [Understanding the WMI Provider for Configuration Management](understanding-the-wmi-provider-for-configuration-management.md)   
 [Working with the WMI Provider for Configuration Management](working-with-the-wmi-provider-for-configuration-management.md)   
 [Using WQL and Scripting Languages with the WMI Provider for Configuration Management](using-wql-and-scripting-languages-with-the-wmi-provider.md)  
  
  
