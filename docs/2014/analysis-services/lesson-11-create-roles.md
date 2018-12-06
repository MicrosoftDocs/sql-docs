---
title: "Lesson 12: Create Roles | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 984face4-00fc-46d3-8ae1-9755bf737bdf
author: minewiskan
ms.author: owend
manager: craigg
---
# Lesson 12: Create Roles
  In this lesson, you will create roles. Roles provide model database object and data security by limiting access to only those Windows users which are role members. Each role is defined with a single permission: None, Read, Read and Process, Process, or Administrator. Roles can be defined during model authoring by using the Role Manager dialog box in [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)]. After a model has been deployed, you can manage roles by using [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]. To learn more, see [Roles &#40;SSAS Tabular&#41;](tabular-models/roles-ssas-tabular.md).  
  
> [!NOTE]  
>  Creating roles is not necessary to complete this tutorial. By default, the account you are currently logged in with will have Administrator privileges on the model. However, to allow other users in your organization to browse the model by using a reporting client application, you must create at least one role with Read permissions and add those users as members.  
  
 You will create three roles:  
  
-   Sales Manager - This role can include users in your organization for which you want to have Read permission to all model objects and data.  
  
-   Sales Analyst US - This role can include users in your organization for which you want only to be able to browse data related to sales in the US (United States). For this role, you will use a DAX formula to define a *Row Filter*, which restricts members to browse data only for the United States.  
  
-   Administrator - This role can include users for which you want to have Administrator permission, which allows unlimited access and permissions to perform administrative tasks on the model database.  
  
 Because Windows user and group accounts in your organization are unique, you can add accounts from your particular organization to members. However, for this tutorial, you can also leave the members blank. You will still be able to test the effect of each role later in Lesson 12: Analyze in Excel.  
  
 Estimated time to complete this lesson: **15 minutes**  
  
## Prerequisites  
 This topic is part of a tabular modeling tutorial, which should be completed in order. Before performing the tasks in this lesson, you should have completed the previous lesson: [Lesson 11: Create Partitions](lesson-10-create-partitions.md).  
  
## Create Roles  
  
#### To create a Sales Manager user role  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click on the **Model** menu, and then click **Roles**.  
  
2.  In the **Role Manager** dialog box, click **New**.  
  
     A new role with the None permission is added to the list.  
  
3.  Click on the new role, and then in the **Name** column, rename the role to `Internet Sales Manager`.  
  
4.  In the **Permissions** column, click the dropdown list, and then select the **Read** permission.  
  
5.  Optional: Click on the **Members** tab, and then click **Add**.  
  
6.  In the **Select Users or Groups** dialog box, enter the Windows users or groups from your organization you want to include in the role.  
  
7.  Verify your selections, and then click **OK**  
  
#### To create a Sales Analyst US user role  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click on the **Model** menu, and then click **Roles**.  
  
2.  In the **Role Manager** dialog box, click **New**.  
  
     A new role with the None permission is added to the list.  
  
3.  Click on the new role, and then in the **Name** column, rename the role to `Internet Sales US`.  
  
4.  In the **Permissions** column, click the dropdown list, and then select the **Read** permission.  
  
5.  Click on the Row Filters tab, and then for the **Geography** table only, in the DAX Filter column, type the following formula:  
  
     `=Geography[Country Region Code] = "US"`  
  
     A Row Filter formula must resolve to a Boolean (TRUE/FALSE) value. With this formula, you are specifying that only rows with the Country Region Code value of "US" be visible to the user.  
  
     When you have finished building the formula, press ENTER.  
  
6.  Optional: Click on the **Members** tab, and then click **Add**.  
  
7.  In the **Select Users or Groups** dialog box, enter the Windows users or groups from your organization you want to include in the role.  
  
8.  Verify your selections, and then click **OK**  
  
#### To create an Administrator role  
  
1.  In the **Role Manager** dialog box, click **New**.  
  
2.  Click on the new role, and then in the **Name** column, rename the role to `Internet Sales Administrator`.  
  
3.  In the **Permissions** column, click the dropdown list, and then select the **Administrator** permission.  
  
4.  Click on the **Members** tab, and then click **Add**.  
  
5.  Optional: In the **Select Users or Groups** dialog box, enter the Windows users or groups from your organization you want to include in the role.  
  
6.  Verify your selections, and then click **OK**  
  
## Next Steps  
 To continue this tutorial, go to the next lesson: Lesson: [Lesson 13: Analyze in Excel](lesson-12-analyze-in-excel.md).  
  
  
