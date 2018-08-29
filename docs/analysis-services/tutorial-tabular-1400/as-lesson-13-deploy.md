---
title: "Analysis Services tutorial lesson 13: Deploy | Microsoft Docs"
ms.date: 08/27/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: tutorial
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Deploy

[!INCLUDE[ssas-appliesto-sql2017-later-aas](../../includes/ssas-appliesto-sql2017-later-aas.md)]

In this lesson, you configure deployment properties; specifying a server to deploy to and a name for the model. You then deploy the model to the server. After your model is deployed, users can connect to it by using a reporting client application. To learn more, see [Deploy to Azure Analysis Services](https://docs.microsoft.com/azure/analysis-services/analysis-services-deploy) and [Tabular model solution deployment](../tabular-models/tabular-model-solution-deployment-ssas-tabular.md).  
  
Estimated time to complete this lesson: **5 minutes**  
  
## Prerequisites  

This article is part of a tabular modeling tutorial, which should be completed in order. Before performing the tasks in this lesson, you should have completed the previous lesson: [Lesson 12: Analyze in Excel](../tutorial-tabular-1400/as-lesson-12-analyze-in-excel.md).  

> [!IMPORTANT]  
> If deploying to Azure Analysis Services, you must have [Administrator permissions](https://docs.microsoft.com/azure/analysis-services/analysis-services-server-admins) on the serever.  

> [!IMPORTANT]  
> If you installed the AdventureWorksDW sample database on an on-premises SQL Server, and you're deploying your model to an Azure Analysis Services server, an [On-premises data gateway](https://docs.microsoft.com/azure/analysis-services/analysis-services-gateway) is required.
  
## Deploy the model  
  
#### To configure deployment properties  

  
1.  In **Solution Explorer**, right-click the **AW Internet Sales** project, and then click **Properties**.  
  
2.  In the **AW Internet Sales Property Pages** dialog box, under **Deployment Server**, in the **Server** property, enter the full server name. If connecting to Azure Analysis Services, server name must inlcude the full URL.

    ![as-lesson13-deploy-property](../tutorial-tabular-1400/media/as-lesson13-deploy-property.png)
  
3.  In the **Database** property, type **Adventure Works Internet Sales**.  
  
4.  In the **Model Name** property, type **Adventure Works Internet Sales Model**.  
  
5.  Verify your selections and then click **OK**.  
  
#### To deploy the Adventure Works Internet Sales
  
1.  In **Solution Explorer**, right-click the **AW Internet Sales** project > **Build**.  

2.  Right-click the **AW Internet Sales** project > **Deploy**.

    When deploying to Azure Analysis Services, you may be prompted to enter your account. Enter your organizational account and password, for example nancy@adventureworks.com. This account must be in Admins on the server.
  
    The Deploy dialog box appears and displays the deployment status of the metadata and each table included in the model.  
    
    ![as-lesson13-deploy-status](../tutorial-tabular-1400/media/as-lesson13-deploy-status.png)
  
3. When deployment successfully completes, go ahead and click **Close**.  
  

This lesson describes the most common and easiest method to deploy a tabular model from SSDT. Advanced deployment options such as the Deployment Wizard or automating with XMLA and AMO provide greater flexibility, consistency, and scheduled deployments. To learn more, see [Tabular model solution deployment](../tabular-models/tabular-model-solution-deployment-ssas-tabular.md).

## Conclusion  
Congratulations! You're finished authoring and deploying your first Analysis Services Tabular model. This tutorial has helped guide you through completing the most common tasks in creating a tabular model. Now that your Adventure Works Internet Sales model is deployed, you can use SQL Server Management Studio to manage the model; create process scripts and a backup plan. Users can also now connect to the model using a reporting client application such as Microsoft Excel or Power BI.  

![as-lesson13-ssms](../tutorial-tabular-1400/media/as-lesson13-ssms.png)
  
  
  
## What's next?
[Connect with Power BI Desktop](https://docs.microsoft.com/azure/analysis-services/analysis-services-connect-pbi)   
[Supplemental Lesson - Dynamic security](../tutorial-tabular-1400/as-supplemental-lesson-dynamic-security.md)   
[Supplemental Lesson - Detail rows](../tutorial-tabular-1400/as-supplemental-lesson-detail-rows.md)   
[Supplemental Lesson - Ragged hierarchies](../tutorial-tabular-1400/as-supplemental-lesson-ragged-hierarchies.md)   
