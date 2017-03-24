---
title: "Package Roles Dialog Box UI Reference | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.dtsserver.packageroles.f1"
helpviewer_keywords: 
  - "Package Roles dialog box"
ms.assetid: 63f13416-c0aa-4480-a336-ef1e6e31a860
caps.latest.revision: 26
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Package Roles Dialog Box UI Reference
  Use the **Package Roles** dialog box, available in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], to specify the database-level roles that have read access to the package and the database-level roles that have write access to the package. Database-level roles apply only to packages that are stored in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **msdb** database.  
  
 To learn more about the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] database-level roles and their permissions, see [Integration Services Roles &#40;SSIS Service&#41;](../../integration-services/service/integration-services-roles-ssis-service.md).  
  
 The roles listed in the dialog box are the current database roles of the **msdb** system database. If no roles are selected, the default [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] roles apply. By default, the reader role includes **db_ssisadmin**, **db_ssisoperator**, and the user who created the package. A user who is a member of one of these roles or created the packages can enumerate, view, export, and run packages. By default, the writer role includes **db_ssisadmin** and the user who created the package. A user who is a member of this role and the user who created the packages can import, delete, and change packages.  
  
 The **ownersid** column in the **sysssispackages** table lists the unique security identifier of the user who created the package.  
  
## Options  
 **Package Name**  
 Specify the name of the package.  
  
 **Reader Role**  
 Select a role in the list.  
  
 **Writer Role**  
 Select a role in the list  
  
## See Also  
 [Database-Level Roles](../../relational-databases/security/authentication-access/database-level-roles.md)   
 [Security Overview &#40;Integration Services&#41;](../../integration-services/security/security-overview-integration-services.md)  
  
  