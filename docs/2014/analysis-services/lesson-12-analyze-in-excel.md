---
title: "Lesson 13: Analyze in Excel | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: ce717071-193b-4c99-9654-c7a613e16327
author: minewiskan
ms.author: owend
manager: craigg
---
# Lesson 13: Analyze in Excel
  In this lesson, you will use the Analyze in Excel feature in [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)] to open Microsoft Excel, automatically create a data source connection to the model workspace, and automatically add a PivotTable to the worksheet. The Analyze in Excel feature is meant to provide a quick and easy way to test the efficacy of your model design prior to deploying your model. You will not perform any data analysis in this lesson. The purpose of this lesson is to familiarize you, the model author, with the tools you can use to test your model design. Unlike using the Analyze in Excel feature, which is meant for model authors, end-users will use client reporting applications such as Excel or [!INCLUDE[ssCrescent](../includes/sscrescent-md.md)] to connect to and browse deployed model data.  
  
 In order to complete this lesson, Excel must be installed on the same computer as [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)]. To learn more, see [Analyze in Excel &#40;SSAS Tabular&#41;](tabular-models/analyze-in-excel-ssas-tabular.md).  
  
 Estimated time to complete this lesson: **20 minutes**  
  
## Prerequisites  
 This topic is part of a tabular modeling tutorial, which should be completed in order. Before performing the tasks in this lesson, you should have completed the previous lesson: [Lesson 11: Create Partitions](lesson-10-create-partitions.md).  
  
## Browse using the Default and Internet Sales perspectives  
 In these first tasks, you will browse your model by using both the default perspective, which includes all model objects, and also by using the Internet Sales perspective you created in Lesson 8: Create Perspectives. The Internet Sales perspective excludes the Customer table object.  
  
#### To browse by using the Default perspective  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click the **Model** menu, and then click **Analyze in Excel**.  
  
2.  In the **Analyze in Excel** dialog box, click **OK**.  
  
     Excel will open with a new workbook. A data source connection is created using the current user account and the Default perspective is used to define viewable fields. A Pivot table is automatically added to the worksheet.  
  
3.  In Excel, in the **PivotTable Field List**, notice the **Date** and **Internet Sales** measures appear, as well as the **Customer**, **Date**, **Geography**, **Product**, **Product Category**, **Product Subcategory**, and **Internet Sales** tables with all of their respective columns appear.  
  
4.  Close Excel without saving the workbook.  
  
#### To browse by using the Internet Sales perspective  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click the **Model** menu, and then click **Analyze in Excel**.  
  
2.  In the **Analyze in Excel** dialog box, leave **Current Windows User** selected, then in the **Perspective** drop-down listbox, select **Internet Sales**, and then click **OK**. Excel opens.  
  
3.  In Excel, in the **PivotTable Field List**, notice the Customer table is excluded from the field list.  
  
## Browse Using Roles  
 Roles are an integral part of any tabular model. Without at least one role, to which users are added as members, users will not be able to access and analyze data using your model. The Analyze in Excel feature provides a way for you to test the roles you have defined.  
  
#### To browse by using the Internet Sales Manager user role  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click the **Model** menu, and then click **Analyze in Excel**.  
  
2.  In the **Analyze in Excel** dialog box, in **Specify the user name or role to use to connect to the model**, select **Role**, and then in the drop-down listbox, select **Internet Sales Manager**, and then click **OK**.  
  
     Excel will open with a new workbook. A Pivot table is automatically created. The Pivot Table Field List includes all of the data fields available in your new model.  
  
## Next Steps  
 To continue this tutorial, go to the next lesson: [Lesson 14: Deploy](lesson-13-deploy.md).  
  
  
