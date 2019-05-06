---
title: "Set Parameter Value Dialog Box | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: ce9c2201-4e9a-4495-948f-b68deeaa7955
author: janinezhang
ms.author: janinez
manager: craigg
---
# Set Parameter Value Dialog Box
  Use the **Set Parameter Value** dialog box to set values for parameters and connection manager properties, for projects and packages.  
  
 **What do you want to do?**  
  
-   [Open the Set Parameter Value dialog box](#open_dialog)  
  
-   [Configure the options](#option)  
  
##  <a name="open_dialog"></a> Open the Set Parameter Value dialog box  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.  
  
     You're connecting to the instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] that hosts the SSISDB database.  
  
2.  In Object Explorer, expand the tree to display the **Integration Services Catalogs** node.  
  
3.  Expand the **SSISDB** node.  
  
4.  Right-click a package or project, click **Configure**, and then click the ellipsis button in the **Parameters** tab or in the **Connection Managers** tab.  
  
##  <a name="option"></a> Configure the options  
 **Parameter**  
 Lists the parameter name.  
  
 **Type**  
 Lists the data type of the parameter value.  
  
 **Description**  
 Shows an optional description for the parameter.  
  
 **Edit value**  
 Select this option to edit the parameter value.  
  
 **Use default value from package**  
 Select this option to use the default parameter value stored in the package.  
  
 **Use environment variable**  
 Select this option to use a variable value stored in the environment, which is referenced by the project or package. To add an environment reference to a project or package, use the **Configure** dialog box. For more information, see [Configure Dialog Box](../../integration-services/catalog/configure-dialog-box.md).  
  
  
