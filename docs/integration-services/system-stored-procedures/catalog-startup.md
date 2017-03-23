---
title: "catalog.startup | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "03/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
ms.assetid: 271fd405-246a-4852-bfbe-f557241ce6ea
caps.latest.revision: 9
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# catalog.startup
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Performs maintenance of the state of operations for the SSISDB catalog.  
  
 The stored procedure fixes the status of any packages there were running if and when the [!INCLUDE[ssIS](../../includes/ssis-md.md)] server instance goes down.  
  
 You have the option of enabling the stored procedure to run automatically each time the [!INCLUDE[ssIS](../../includes/ssis-md.md)] server instance is restarted, by selecting the **Enable automatic execution of Integration Services stored procedure at SQL Server startup** option in the **Create Catalog** dialog box.  
  
## Syntax  
  
```tsql  
Catalog.startup  
```  
  
## Return Code Value  
 0 (success)  
  
## Result Sets  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   READ and MODIFY permissions on the instance of execution, READ and EXECUTE permissions on the project, and if applicable, READ permissions on the referenced environment  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
  