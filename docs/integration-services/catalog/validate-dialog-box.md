---
description: "Validate Dialog Box"
title: "Validate Dialog Box | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.ssis.ssms.isprojectvalidate.f1"
  - "sql13.ssis.ssms.ispackagevalidate.f1"
ms.assetid: 134e14ce-4f8d-4a20-889a-918014c841d8
author: chugugrace
ms.author: chugu
---
# Validate Dialog Box

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  Use the **Validate** dialog box to check for common problems in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] a project or package.  
  
 If there is a problem, a message is displayed at the top of the dialog box. Otherwise, the term Ready displays at the top.  
  
 **What do you want to do?**  
  
-   [Open the Validate dialog box](#open_dialog)  
  
-   [Set the options on the General page](#general)  
  
##  <a name="open_dialog"></a> Open the Validate dialog box  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.  
  
     You're connecting to the instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] that hosts the SSISDB database.  
  
2.  In Object Explorer, expand the tree to display the **Integration Services Catalogs** node.  
  
3.  Expand the **SSISDB** node.  
  
4.  Expand the folder that contains the project or package you want to validate.  
  
5.  Right-click the package or package, and then click **Validate**.  
  
##  <a name="general"></a> Set the options on the General page  
 **Environment**  
 Select the environment that you want to use to validate the project or package.  
  
 **32-bit runtime**  
 Select to use a 32-bit runtime to execute a package.  
  
 The **Parameters** tab lists the parameter values that you use to validate the project or package. The following are the options on the Parameters tab.  
  
 **Container**  
 Lists the object that contains the parameter.  
  
 **Parameter**  
 Lists the name of the parameters  
  
 **Value**  
 Lists the parameter value.  
  
 The **Connection Managers** tab lists the connection manager property values that you use to validate the project or package.  
  
 The following are the options on the **Connection Managers** tab.  
  
 **Container**  
 Lists the object that contains the connection manager.  
  
 **Name**  
 Lists the connection manager name.  
  
 **Property name**  
 Lists the name of the connection manager property.  
  
 **Value**  
 Lists the value assigned to the connection manager property.  
  
  
