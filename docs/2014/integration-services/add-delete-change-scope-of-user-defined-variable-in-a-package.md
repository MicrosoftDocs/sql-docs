---
title: "Add, Delete, Change Scope of User-Defined Variable in a Package | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "variables [Integration Services], adding"
ms.assetid: cbf40c7f-3c8a-48cd-aefa-8b37faf8b40e
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Add, Delete, Change Scope of User-Defined Variable in a Package
  These procedures describe how to add, delete, and change the scope of a user-defined variable in a package by using the **Variables** window.  
  
 For more information about variable scope, see [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md).  
  
 [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] also provides system variables that make system information available at run time and can be used in containers such as packages and event handlers. You cannot delete system variables.  
  
### To add a variable  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package you want to work with.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, to define the scope of the variable, do one of the following:  
  
    -   To set the scope to the package, click anywhere on the design surface of the **Control Flow** tab.  
  
    -   To set the scope to an event handler, select an executable and an event handler on the design surface of the **Event Handler** tab.  
  
    -   To set the scope to a task or container, on the design surface of the **Control Flow** tab or the **Event Handler** tab, click a task or container.  
  
4.  On the **SSIS** menu, click **Variables**. You can optionally display the **Variables** window by mapping the View.Variables command to a key combination of your choosing on the **Keyboard** page of the **Options** dialog box.  
  
5.  In the **Variables** window, click the **Add Variable** icon. The new variable is added to the list.  
  
6.  Optionally, click the **Grid Options** icon, select additional columns to show in the **Variables Grid Options** dialog box, and then click **OK**.  
  
7.  Optionally, set the variable properties. For more information, see [Set the Properties of a User-Defined Variable](../../2014/integration-services/set-the-properties-of-a-user-defined-variable.md).  
  
8.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
### To delete a variable  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, right-click the package to open it.  
  
3.  On the **SSIS** menu, click **Variables**. You can optionally display the **Variables** window by mapping the View.Variables command to a key combination of your choosing on the **Keyboard** page of the **Options** dialog box.  
  
4.  Select the variable to delete, and then click **Delete Variable**.  
  
     If you don't see the variable in the Variables window, click **Grid Options** and then select **Show variables of all scopes**.  
  
5.  If the **Confirm Deletion of Variables** dialog box opens, click **Yes** to confirm.  
  
6.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
### To change the scope of a variable  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, right-click the package to open it.  
  
3.  On the **SSIS** menu, click **Variables**. You can optionally display the **Variables** window by mapping the View.Variables command to a key combination of your choosing on the **Keyboard** page of the **Options** dialog box.  
  
4.  Select the variable and then click **Move Variable**.  
  
     If you don't see the variable in the Variables window, click **Grid Options** and then select **Show variables of all scopes**.  
  
5.  In the **Select New Scope** dialog box, select the package or a container, task, or event handler in the package, to change the variable scope.  
  
6.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md)   
 [Use Variables in Packages](../../2014/integration-services/use-variables-in-packages.md)   
 [Set the Properties of a User-Defined Variable](../../2014/integration-services/set-the-properties-of-a-user-defined-variable.md)   
 [Use the Values of Variables and Parameters in a Child Package](../../2014/integration-services/use-the-values-of-variables-and-parameters-in-a-child-package.md)  
  
  
