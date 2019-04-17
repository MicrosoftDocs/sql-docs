---
title: "Lesson 13: Deploy | Microsoft Docs"
ms.date: 08/22/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: tutorial
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Lesson 13: Deploy
[!INCLUDE[ssas-appliesto-sql2016-later-aas](../includes/ssas-appliesto-sql2016-later-aas.md)]

In this lesson, you will configure deployment properties; specifying an on-premises or Azure server instance, and a name for the model. You'll then deploy the model to that instance. After your model is deployed, users can connect to it by using a reporting client application. To learn more about deploying, see [Tabular model solution deployment](../analysis-services/tabular-models/tabular-model-solution-deployment-ssas-tabular.md) and [Deploy to Azure Analysis Services](https://docs.microsoft.com/azure/analysis-services/analysis-services-deploy).  
  
Estimated time to complete this lesson: **5 minutes**  
  
## Prerequisites  
This topic is part of a tabular modeling tutorial, which should be completed in order. Before performing the tasks in this lesson, you should have completed the previous lesson: [Lesson 12: Analyze in Excel](../analysis-services/lesson-12-analyze-in-excel.md).  
  
## Deploy the model  
  
#### To configure deployment properties  
  
1.  In **Solution Explorer**, right-click the **AW Internet Sales** project, and then click **Properties**.  
  
2.  In the **AW Internet Sales Property Pages** dialog box, under **Deployment Server**, in the **Server** property, type the name of an Azure Analysis Services server or an on-premises server instance running in Tabular mode. This will be the server instance your model will be deployed to.  

    ![aas-deploy-deployment-server-property](../analysis-services/media/aas-deploy-deployment-server-property.png)
 
    > [!IMPORTANT]  
    > You must have Administrator permissions on the remote Analysis Services instance in-order to deploy to it.  
  
3.  In the **Database** property, type **Adventure Works Internet Sales**.  
  
4.  In the **Model Name** property, type **Adventure Works Internet Sales Model**.  
  
5.  Verify your selections and then click **OK**.  
  
#### To deploy the Adventure Works Internet Sales tabular model  
  
1.  In **Solution Explorer**, right-click the **AW Internet Sales** project > **Build**.  

2.  Right-click the **AW Internet Sales** project > **Deploy**.

    When deploying to Azure Analysis Services, you'll likely be prompted to enter your account. Enter your organizational account and passsword, for example nancy@adventureworks.com. This account must be in Admins on the server instance.
  
    The Deploy dialog box appears and displays the deployment status of the metadata as well as each table included in the model.  
    
    ![aas-deploy-status](../analysis-services/media/aas-deploy-status.png)
  
3. When deployment successfully completes, go ahead and click **Close**.  
  
## Conclusion  
Congratulations! You're finished authoring and deploying your first Analysis Services Tabular model. This tutorial has helped guide you through completing the most common tasks in creating a tabular model. Now that your Adventure Works Internet Sales model is deployed, you can use SQL Server Management Studio to manage the model; create process scripts and a backup plan. Users can also now connect to the model using a reporting client application such as Microsoft Excel or Power BI.  

![as-tabular-lesson13-ssms](../analysis-services/media/as-tabular-lesson13-ssms.png)
  
  
## See also  
[DirectQuery Mode](../analysis-services/tabular-models/directquery-mode-ssas-tabular.md)  
[Configure default data modeling and deployment properties](../analysis-services/tabular-models/configure-default-data-modeling-and-deployment-properties-ssas-tabular.md)  
[Tabular Model Databases](../analysis-services/tabular-models/tabular-model-databases-ssas-tabular.md)  
  
  
  ## What's next?
*  [Supplemental Lesson - Implement Dynamic Security by Using Row Filters](../analysis-services/supplemental-lesson-implement-dynamic-security-by-using-row-filters.md).

*  [Supplemental Lesson - Configure Reporting Properties for Power View Reports](../analysis-services/supplemental-lesson-configure-reporting-properties-for-power-view-reports.md).
