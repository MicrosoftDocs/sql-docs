---
title: "Project Properties Dialog Box | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.ssis.ssms.isprojectprop.general.f1"
  - "sql13.ssis.ssms.isprojectprop.permissions.f1"
ms.assetid: d5cf52f5-1fe2-438a-98a3-fe117360acf8
author: janinezhang
ms.author: janinez
manager: craigg
---
# Project Properties Dialog Box

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  An [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project is a unit of deployment. Each project can contain packages, parameters, and environment references. A project is a securable object and can define permissions for database principals. When a project is re-deployed, the previous version of the project can be stored in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 Project parameters and package parameters can be used to assign values to properties within packages at the time of execution. Some parameters require values before the package can be executed. Parameter values that reference environment variables require that the project has the corresponding environment reference prior to execution.  
  
 **What do you want to do?**  
  
-   [Open the Project Properties dialog box](#open_dialog)  
  
-   [Set the options on the General page](#general)  
  
-   [Set the options on the Permissions page](#permissions)  
  
##  <a name="open_dialog"></a> Open the Project Properties dialog box  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.  
  
     You're connecting to the instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] that hosts the SSISDB database.  
  
2.  In Object Explorer, expand the tree to display the **Integration Services Catalogs** node.  
  
3.  Expand the **SSISDB** node.  
  
4.  Expand the folder that contains the project that you want to set properties for.  
  
5.  Right-click the project, and then click **Properties**.  
  
##  <a name="general"></a> Set the options on the General page  
 Use the General page to view project properties.  
  
 **Name**  
 Lists the project name.  
  
 **Identifier**  
 Lists the project ID.  
  
 **Description**  
 Displays the optional description of the project.  
  
 **Project Version**  
 Lists the project version.  
  
 **Deployment Date**  
 Lists the date and time the project was deployed or redeployed.  
  
##  <a name="permissions"></a> Set the options on the Permissions page  
 Use the **Permissions** page to view and set explicit permissions for the project.  
  
 Browse  
 Click **Browse** to select users and roles that you want to set permissions for, by using the **Browse All Principals** dialog box.  
  
 **Name**  
 Lists the name of the user or role.  
  
 **Type**  
 Lists the type of user or role.  
  
 **Permission**  
 Lists the permissions.  
  
 **Grantor**  
 Lists the user or role that grants the permission.  
  
 **Grant**  
 When **Grant** is selected, the permission is granted for the selected user or role.  
  
 **Deny**  
 When **Deny** is selected, the permission is denied for the selected user or role.  
  
  
