---
title: "Create and Map a Server Environment | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.ssis.ssms.isenvprop.variables.f1"
  - "sql12.ssis.ssms.iscreateenv.f1"
  - "sql12.ssis.ssms.isenvprop.permissions.f1"
  - "sql12.ssis.ssms.isenvprop.general.f1"
ms.assetid: b1cbb697-713f-48e4-b234-b23724d87451
author: janinezhang
ms.author: janinez
manager: craigg
---
# Create and Map a Server Environment
  You create a server environment to specify runtime values for packages contained in a project you've deployed to the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] server. You can then map the environment variables to parameters, for a specific package, for entry-point packages, or for all the packages in a given project. An entry-point package is typically a parent package that executes a child package.  
  
> [!IMPORTANT]  
>  For a given execution, a package can execute only with the values contained in a single server environment.  
  
 You can query views for a list of server environments, environment references, and environment variables. You can also call stored to add, delete, and modify environments, environment references, and environment variables. For more information, see the **Server Environments, Server Variables and Server Environment References** section in [SSIS Catalog](catalog/ssis-catalog.md).  
  
### To create and use a server environment  
  
1.  In [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)], expand the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] Catalogs> **SSISDB** node in Object Explorer, and locate the **Environments** folder of the project for which you want to create an environment.  
  
2.  Right-click the **Environments** folder, and then click **Create Environment**.  
  
3.  Type a name for the environment and optionally a description, and then click **OK**.  
  
4.  Right-click the new environment and then click **Properties**.  
  
5.  On the **Variables** page, do the following to add a variable.  
  
    1.  Select the **Type** for the variable. The name of the variable **does not** need to match the name of the project parameter that you map to the variable.  
  
    2.  Enter an optional **Description** for the variable.  
  
    3.  Enter the **Value** for the environment variable.  
  
         For information about the rules for environment variable names, see the **Environment Variable** section in [SSIS Catalog](catalog/ssis-catalog.md).  
  
    4.  Indicate whether the variable contains sensitive value, by selecting or clearing the **Sensitive** checkbox.  
  
         If you select **Sensitive**, the variable value does not display in the **Value** field.  
  
         Sensitive values are encrypted in the SSISDB catalog. For more information about the encryption, see [SSIS Catalog](catalog/ssis-catalog.md).  
  
6.  On the **Permissions** page, grant or deny permissions for selected users and roles by doing the following.  
  
    1.  Click **Browse**, and then select one or more users and roles in the **Browse All Principals** dialog box.  
  
    2.  In the **Logins or roles** area, select the user or role that you want to grant or deny permissions for.  
  
    3.  In the **Explicit** area, click **Grant** or **Deny** next to each permission.  
  
7.  To script the environment, click **Script**. By default, the script displays in a new Query Editor window.  
  
    > [!TIP]  
    >  You need to click **Script** after you've made one or changes to the environment properties, such as adding a variable, and before you click **OK** in the **Environment Properties** dialog box. Otherwise, a script is not generated.  
  
8.  Click **OK** to save your changes to the environment properties.  
  
9. Under the **SSISDB** node in Object Explorer, expand the **Projects** folder, right-click the project, and then click **Configure**.  
  
10. On the **References** page, click **Add** to add an environment, and then click **OK** to save the reference to the environment.  
  
11. Right-click the project again, and then click **Configure**.  
  
12. To map the environment variable to a parameter that you added to the package at design-time or to a parameter that was generated when you converted the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project to the project deployment model, do the following.,  
  
    1.  In the **Parameters** tab on the **Parameters** page, click the browse button next to the **Value** field.  
  
    2.  Click **Use environment variable**, and then select the environment variable you created.  
  
13. To map the environment variable to a connection manager property, do the following. Parameters are automatically generated on the SSIS server for the connection manager properties.  
  
    1.  In the **Connection Managers** tab on the **Parameters** page, click the browse button next to the **Value** field.  
  
    2.  Click **Use environment variable**, and then select the environment variable you created.  
  
14. Click **OK** twice to save your changes.  
  
  
