---
title: "Configure Dialog Box | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.configure.f1"
  - "sql13.SSIS.SSMS.ISPROJECTPROP.REFERENCES.F1"
  - "sql13.SSIS.SSMS.ISPROJECTPROP.PARAMETERS.F1"
ms.assetid: 10183c8d-b1be-420f-972a-96ea97d4f4d8
author: janinezhang
ms.author: janinez
manager: craigg
---
# Configure Dialog Box
  Use the **Configure** dialog box to configure parameters, connection managers, and references to environments, for packages and projects.  
  
 **What do you want to do?**  
  
-   [Open the Configure Dialog Box](#open_dialog)  
  
-   [Set the options on the Parameters page](#parameter)  
  
-   [Set the options on the References page](#references)  
  
##  <a name="open_dialog"></a> Open the Configure Dialog Box  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.  
  
     You're connecting to the instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] that hosts the SSISDB database.  
  
2.  In Object Explorer, expand the tree to display the **Integration Services Catalogs** node.  
  
3.  Expand the **SSISDB** node.  
  
4.  Expand the folder that contains the package or project that you want to configure.  
  
5.  Right-click the package or project, and then click **Configure**.  
  
##  <a name="parameter"></a> Set the options on the Parameters page  
 Use the **Parameters** page to view parameter names and values, and to modify the values.  
  
 Select the scope of the parameters displayed in the **Parameters** and **Connection Managers** tabs, in the **Scope** drop-down list.  
  
 The following is a list of the options in the **Parameters** tab.  
  
 **Container**  
 Lists the object that contains the parameter.  
  
 **Name**  
 Lists the parameter name.  
  
 **Value**  
 Lists the parameter value. Click the ellipsis button to change the value in the **Set Parameter Value** dialog box.  
  
 The following is a list of the options in the **Connection Managers** tab. You use this tab to change values for connection manager properties. Parameters are automatically generated on the SSIS server for the properties.  
  
 **Container**  
 Lists the object that contains the connection manager.  
  
 **Name**  
 Lists the connection manager name.  
  
 **Property name**  
 Lists the name of the connection manager property.  
  
 **Value**  
 Lists the value assigned to the connection manager property. Click the ellipsis button to change the value in the **Set Parameter Value** dialog box. You can enter a literal value, map an environment variable that contains the value you want to use, or use the default value from the package.  
  
##  <a name="references"></a> Set the options on the References page  
 Use the **References** page to add and remove references to environments, and to access environment properties.  
  
 An environment specifies runtime values for packages contained in the projects you've deployed to [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.  
  
 **Environment**  
 Lists the environment.  
  
 **Environment Folder**  
 Lists the folder that contains the environment.  
  
 **Open**  
 Click to open the **Environment Properties** dialog box.  
  
 **Add**  
 Click to add a reference to an environment. In the **Browse Environments** dialog box click an environment and then click **OK**.  
  
 You can select an environment contained in any project folder under the **SSISDB** node.  
  
 **Remove**  
 Click an environment listed in the **References** area, and then click **Remove**.  
  
  
