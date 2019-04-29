---
title: "Access to Files Used by Packages | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "SSIS packages, security"
  - "packages [Integration Services], security"
  - "configuration files [Integration Services]"
  - "checkpoint files"
  - "Integration Services packages, security"
  - "logs [Integration Services], security"
  - "files [Integration Services], security"
  - "SQL Server Integration Services packages, security"
ms.assetid: 2e3ddea9-5289-4289-a70e-11c018f34977
author: janinezhang
ms.author: janinez
manager: craigg
---
# Access to Files Used by Packages
  The package protection level does not protect files that are stored outside the package. These files include the following:  
  
-   Configuration files  
  
-   Checkpoint files  
  
-   Log files  
  
 These files must be protected separately, especially if they include sensitive information.  
  
## Configuration Files  
 If you have sensitive information in a configuration, such as login and password information, you should consider saving the configuration to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], or use an access control list (ACL) to restrict access to the location or folder where you store the files and allow access only to certain accounts. Typically, you would grant access to the accounts that you permit to run packages, and to the accounts that manage and troubleshoot packages, which may include reviewing the contents of configuration, checkpoint, and log files. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provides the more secure storage because it offers protection at the server and database levels. To save configurations to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], you use the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] configuration type. To save to the file system, you use the XML configuration type.  
  
 For more information, see [Package Configurations](../../2014/integration-services/package-configurations.md), [Create Package Configurations](../../2014/integration-services/create-package-configurations.md), and [Security Considerations for a SQL Server Installation](../../2014/sql-server/install/security-considerations-for-a-sql-server-installation.md).  
  
## Checkpoint Files  
 Similarly, if the checkpoint file that the package uses includes sensitive information, you should use an access control list (ACL) to secure the location or folder where you store the file. Checkpoint files save current state information on the progress of the package as well as the current values of variables. For example, the package may include a custom variable that contains a telephone number. For more information, see [Restart Packages by Using Checkpoints](packages/restart-packages-by-using-checkpoints.md).  
  
## Log Files  
 Log entries that are written to the file system should also be secured using an access control list (ACL). Log entries can also be stored in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] tables and protected by [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] security. Log entries may include sensitive information, For example, if the package contains an Execute SQL task that constructs an SQL statement that refers to a telephone number, the log entry for the SQL statement includes the telephone number. The SQL statement may also reveal private information about table and column names in databases. For more information, see [Integration Services &#40;SSIS&#41; Logging](performance/integration-services-ssis-logging.md).  
  
  
