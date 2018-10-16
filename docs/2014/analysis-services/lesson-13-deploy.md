---
title: "Lesson 14: Deploy | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 24863a8a-9017-415a-a97b-fbac76ed0675
author: minewiskan
ms.author: owend
manager: craigg
---
# Lesson 14: Deploy
  In this lesson, you will configure deployment properties; specifying a deployment server instance of Analysis Services running in Tabular mode, and a name for the model you deploy. You will then deploy the model to that instance. After it is deployed, users can connect to the model by using a reporting client application. To learn more, see [Tabular Model Solution Deployment &#40;SSAS Tabular&#41;](tabular-models/tabular-model-solution-deployment-ssas-tabular.md).  
  
 Estimated time to complete this lesson: **5 minutes**  
  
## Prerequisites  
 This topic is part of a tabular modeling tutorial, which should be completed in order. Before performing the tasks in this lesson, you should have completed the previous lesson: [Lesson 13: Analyze in Excel](lesson-12-analyze-in-excel.md).  
  
## Deploy the Model  
  
#### To configure deployment properties  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], in **Solution Explorer**, right-click on the **Adventure Works Internet Sales Tabular Model** project, and then in the context menu, click **Properties**.  
  
2.  In the **AW Internet Sales Tabular Model Property Pages** dialog box, under **Deployment Server**, in the **Server** property, type the name of an Analysis Services instance running in Tabular mode. This will be the instance your model will be deployed to.  
  
    > [!IMPORTANT]  
    >  You must have Administrator permissions on a remote Analysis Services instance in-order to deploy to it.  
  
3.  Verify the **Query Mode** property is set to **In-Memory**.  
  
    > [!NOTE]  
    >  The model created by using this tutorial is not supported in DirectQuery mode.  
  
4.  In the **Database** property, type `Adventure Works Internet Sales Model`.  
  
5.  In the **Cube** Name property, type `Adventure Works Internet Sales Model`.  
  
6.  Verify your selections and then click **OK**.  
  
#### To deploy the Adventure Works Internet Sales tabular model  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click the **Build** menu, and then click **Deploy AW Internet Sales Tabular Model**.  
  
     The Deploy dialog box appears and displays the deployment status of the metadata as well as each table included in the model.  
  
## Conclusion  
 Congratulations! You are finished authoring and deploying your first Analysis Services tabular model. This tutorial has helped guide you through completing the most common tasks in creating a tabular model. Now that your Adventure Works Internet Sales Model is deployed, you can use SQL Server Management Studio to manage the model; create process scripts and a backup plan. Users can connect to the model using a reporting client application such as Microsoft Excel or [!INCLUDE[ssCrescent](../includes/sscrescent-md.md)].  
  
## Additional Resources  
 To learn more about tabular model properties that support [!INCLUDE[ssCrescent](../includes/sscrescent-md.md)] reports, see [Power View Reporting Properties &#40;SSAS Tabular&#41;](tabular-models/properties-ssas-tabular.md).  
  
## See Also  
 [DirectQuery Mode &#40;SSAS Tabular&#41;](tabular-models/directquery-mode-ssas-tabular.md)   
 [Configure Default Data Modeling and Deployment Properties &#40;SSAS Tabular&#41;](tabular-models/configure-default-data-modeling-and-deployment-properties-ssas-tabular.md)   
 [Tabular Model Databases &#40;SSAS Tabular&#41;](tabular-models/tabular-model-databases-ssas-tabular.md)  
  
  
