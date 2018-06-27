---
title: "Define Time Intelligence Calculations using the Business Intelligence Wizard | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Define Time Intelligence Calculations using the Business Intelligence Wizard
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The time intelligence enhancement is a cube enhancement that adds time calculations (or time views) to a selected hierarchy. This enhancement supports the following categories of calculations:  
  
-   Period to date.  
  
-   Period over period growth.  
  
-   Moving averages.  
  
-   Parallel period comparisons.  
  
 You apply time intelligence to cubes that have a time dimension. (A time dimension is a dimension whose **Type** property is set to **Time**). Additionally, the time attributes of that dimension must also have the appropriate setting (such as, Years or Months) for their **Type** property. The **Type** property of both the dimension and its attributes will be set correctly if you use the Dimension Wizard to create the time dimension.  
  
 To add time intelligence to a cube, you use the Business Intelligence Wizard, and select the **Define time intelligence** option on the **Choose Enhancement** page. This wizard then guides you through the steps of selecting a hierarchy to which you are adding time intelligence and specifying which members in the hierarchy will have time intelligence applied to them. On the last page of the wizard, you can see the changes that will be made to the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database to add the selected time intelligence.  
  
## Selecting a Time Hierarchy  
 On the **Choose Target Hierarchy and Calculations** page, you select the time hierarchy to which the time enhancement applies. You can apply the time enhancement to only one time hierarchy every time that you run the Business Intelligence Wizard. If you want to apply the enhancement to more than one time hierarchy, you run the wizard again.  
  
 After you select a time hierarchy, in the **Available time calculations** list, you select the calculations that apply to the hierarchy. The calculations that are listed depend on the levels in the hierarchy, and on the **Type** property setting for the attribute for each level. For example, a Years hierarchy supports Year to Date and Year Over Year Growth, but a Quarters hierarchy does not.  
  
> [!NOTE]  
>  The Timeintelligence.xml template file defines the time calculations that are listed in **Available time calculations**. If the listed calculations do not meet your needs, you can either change the existing calculations or add new calculations to the Timeintelligence.xml file.  
  
> [!TIP]  
>  To view a description for a calculation, use the up and down arrows to highlight that calculation.  
  
## Apply Time Views to Members  
 On the **Define Scope of Calculations** page, you specify the members to which the new time views apply. You can apply the new time views to one of the following objects:  
  
-   **Members of an account dimension** On the **Define Scope of Calculations** page, the **Available measures** list includes account dimensions. Account dimensions have their **Type** properties set to **Accounts**. If you have an accounts dimension but that dimension does not appear in the **Available measures** list, you can use the Business Intelligence Wizard to apply the account intelligence to that dimension. For more information, see [Add Account Intelligence to a Dimension](../../analysis-services/multidimensional-models/bi-wizard-add-account-intelligence-to-a-dimension.md).  
  
-   **Measures** Instead of specifying an account dimension, you can specify the measures to which the time views apply. In this case, select the views to which selected time calculations apply. For example, assets and liabilities are year-to-date data; therefore, you do not apply a Year-to-Date calculation to assets or liabilities measures.  
  
## Viewing the Time Intelligence Enhancement  
 On the final page of the Business Intelligence Wizard, you can view the changes that will be made to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. For a time intelligence enhancement, the wizard will change the selected time dimension, associated data source view, and associated cube as described in the following table.  
  
|Object|Change|  
|------------|------------|  
|Time dimension|Adds an attribute for each calculation (or view).|  
|Data source view|Adds a calculated column in the time table for each new attribute in the time dimension.|  
|Cube|Adds a calculated member that defines the Multidimensional Expressions (MDX) code to perform the calculation.|  
  
## See Also  
 [Create Calculated Members](../../analysis-services/multidimensional-models/create-calculated-members.md)  
  
  
