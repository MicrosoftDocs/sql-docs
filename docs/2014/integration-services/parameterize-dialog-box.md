---
title: "Parameterize Dialog Box | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.ssis.designer.parameter.f1"
ms.assetid: fac02b6d-d247-447a-8940-e8700c7ac350
author: janinezhang
ms.author: janinez
manager: craigg
---
# Parameterize Dialog Box
  The **Parameterize** dialog box enables you to associate a new or an existing parameter with a property of a task. You open the dialog box by right-clicking a task or the Control Flow tab in [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer and then by clicking **Parameterize**. The following list describes UI elements in the dialog box. For more information about parameters, see [Integration Services &#40;SSIS&#41; Parameters](integration-services-ssis-package-and-project-parameters.md).  
  
## UIElement List  
 **Property**  
 Select the property of the task that you want to associate with a parameter. This list is populated with all the properties that can be parameterized.  
  
 **Use existing parameter**  
 Select this option to associate the property of task with an existing parameter and then select the parameter from drop-down list.  
  
 **Do not use parameter**  
 Select this option to remove a reference to a parameter. The parameter is not deleted.  
  
 **Create new parameter**  
 Select this option to create a new parameter that you want to associate with the property of the task.  
  
 **Name**  
 Specify the name of the parameter you want to create.  
  
 **Description**  
 Specify the description for parameter.  
  
 **Value**  
 Specify the default value for the parameter. This is also known as the design default, which can be overridden later at the deployment time.  
  
 **Scope**  
 Specify the scope of the parameter by selecting either **Project** or **Package** option. Project parameters are used to supply any external input the project receives to one or more packages in the project. Package parameters allow you to modify package execution without having to edit and redeploy the package.  
  
 **Sensitive**  
 Specify whether the parameter is a sensitive by checking or clearing the check box. Sensitive parameter values are encrypted in the catalog and appear as a NULL value when viewed with Transact-SQL or SQL Server Management Studio.  
  
 **Required**  
 Specify whether the parameter requires that a value, other than the design default, is specified before the package can execute.  
  
## UIElement List  
  
