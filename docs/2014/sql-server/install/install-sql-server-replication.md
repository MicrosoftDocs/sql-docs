---
title: "Install SQL Server Replication | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "components [SQL Server replication]"
  - "command line installations [SQL Server replication]"
  - "installing replication"
  - "replication [SQL Server], installing"
  - "command prompt [SQL Server replication]"
ms.assetid: c50ad078-060b-4a8d-ad45-9e31a8d85729
caps.latest.revision: 40
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Install SQL Server Replication
  Replication components can be installed by using the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Installation Wizard or at a command prompt. Install replication when you install [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], or when you modify an existing instance.  
  
 After replication components are installed, you must configure the server before you can use replication. For more information, see [Configure Distribution](../../../2014/relational-databases/replication/configure-distribution.md) in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Books Online.  
  
> [!IMPORTANT]  
>  If you install replication components when you modify an existing instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], you must stop and restart [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent after the installation is completed. This action helps ensure that [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent recognizes the replication agent subsystems and can call replication agents from job steps.  
  
## Installing Replication by Using Setup  
 **To install replication when installing a new instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]**  
  
-   To install replication components, including Replication Management Objects (RMO), select **SQL Server Replication** on the **Feature Selection** page of the Installation Wizard.  
  
## Installing Replication from the Command Prompt  
 **To install replication when installing a new instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]**  
  
-   See [Install SQL Server 2014 from the Command Prompt](../../../2014/sql-server/install/install-sql-server-2014-from-the-command-prompt.md).  
  
## See Also  
 [Install SQL Server 2014](../../../2014/sql-server/install/install-sql-server-2014.md)   
 [Install SQL Server 2014 from the Command Prompt](../../../2014/sql-server/install/install-sql-server-2014-from-the-command-prompt.md)   
 [Features Supported by the Editions of SQL Server 2014](../../../2014/getting-started/features-supported-by-the-editions-of-sql-server-2014.md)  
  
  