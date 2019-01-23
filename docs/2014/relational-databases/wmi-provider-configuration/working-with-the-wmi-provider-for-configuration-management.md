---
title: "Working with the WMI Provider for Configuration Management | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
helpviewer_keywords: 
  - "permissions [WMI]"
  - "connection strings [WMI]"
  - "security [WMI]"
  - "WMI Provider for Configuration Management, connection strings"
  - "WMI Provider for Configuration Management, security"
  - "late binding [WMI]"
  - "WMI Provider for Configuration Management, late binding"
  - "binding [WMI]"
ms.assetid: 34daa922-7074-41d0-9077-042bb18c222a
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# Working with the WMI Provider for Configuration Management
  Consider the following before programming with the WMI Provider for Computer Management:  
  
## Binding  
 The WMI Provider for Configuration Management is a COM object model and it supports early and late binding. With late binding you can use script languages, such as VBScript, to manipulate the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services, network settings, and aliases programmatically.  
  
 For further information about programming WMI Provider implementations using scripting languages, see the [!INCLUDE[msCoName](../../includes/msconame-md.md)] MSDN [Web site](https://go.microsoft.com/fwlink/?linkid=15426).  
  
## Specifying a Connection String  
 Applications direct the WMI Provider for Configuration Management to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by connecting to a WMI namespace defined by the provider. The Windows WMI service maps this namespace to the provider DLL and loads it into memory. All instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are represented with a single WMI namespace. The namespace defaults to  
  
```  
\\.\root\Microsoft\SqlServer\ComputerManagement12\instance_name  
```  
  
 where `instance_name` defaults to `MSSQLSERVER` in a default installation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 **Note:** If you are connecting through Windows Firewall you will need to make sure your computers are configured appropriately. See the "Connecting Through Windows Firewall" article in the Windows Management Instrumentation documentation on [!INCLUDE[msCoName](../../includes/msconame-md.md)] MSDN [Web site](https://go.microsoft.com/fwlink/?linkid=15426).  
  
## Permissions and Server Authentication  
 To access the WMI Provider for Configuration Management, the client WMI management script must be running in the context of an administrator on the target computer. You need to be a member of the local Windows administrators group on the computer you want to manage.  
  
 The administrator can set group policies to control user access to WMI providers. For more information about setting group policies see "Group Policy and MMC" in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager Help.  
  
 The WMI management script can be used to update the account under which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services run.  
  
 Security certificates are supported by the WMI Provider for Configuration Management. For more information about certificates, see [Encryption Hierarchy](../security/encryption/encryption-hierarchy.md).  
  
## See Also  
 [SQL Server Configuration Manager](../sql-server-configuration-manager.md)  
  
  
