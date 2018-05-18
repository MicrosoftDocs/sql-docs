---
title: "Deploy Model Solutions Using the Deployment Wizard | Microsoft Docs"
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
# Deploy Model Solutions Using the Deployment Wizard
[!INCLUDE[ssas-appliesto-sqlas-all-aas](../../includes/ssas-appliesto-sqlas-all-aas.md)]

  The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard uses JSON output files generated from a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project as input files. These input files are easily modifiable to customize the deployment of an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project. The generated deployment script can then either be immediately run or saved for later deployment.  

> [!NOTE]
> The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard/Utility is installed with [SQL Server Managment Studio](../../ssms/download-sql-server-management-studio-ssms.md) (SSMS). Be sure you're using the latest version. If running from the command prompt, by default, the latest version of  the deployment wizard is installed to C:\Program Files (x86)\Microsoft SQL Server\140\Tools\Binn\ManagementStudio. 
  
 You can deploy by using the wizard as discussed here. You can also automate deployment or use the Synchronize capability. If the deployed database is large, consider using partitions on target systems. You can automate partition creation and population by using Tabular Object Model (TOM), Tabular Model Scriting Language (TMSL), and Analysis Management Objects (AMO).  
  
> [!IMPORTANT]  
>  Neither the output files nor the deployment script will contain the user id or password if these are specified in either the connection string for a data source or for impersonation purposes. Since these are required for processing purposes in this scenario, you will add this information manually. If the deployment will not include processing, you can add this connection and impersonation information as needed after deployment. If the deployment will include processing, you can either add this information within the wizard or in the deployment script after it is saved.  
  
## In This Section  
 The following topics describe how to work with the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard, the input files, and the deployment script:  
  
|Topic|Description|  
|-----------|-----------------|  
|[Running the Analysis Services Deployment Wizard](../../analysis-services/multidimensional-models/running-the-analysis-services-deployment-wizard.md)|Describes the various ways in which you can run the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard.|  
|[Understanding the Input Files Used to Create the Deployment Script](../../analysis-services/multidimensional-models/deployment-script-files-input-used-to-create-deployment-script.md)|Describes which files the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard uses as input values, what each of those files contains, and provides links to topics that describe how to modify the values in each of the input files.|  
|[Understanding the Analysis Services Deployment Script](../../analysis-services/multidimensional-models/understanding-the-analysis-services-deployment-script.md)|Describes what the deployment script contains and how the script runs.|  
  
## See Also  
 [Deploy Model Solutions Using XMLA](../../analysis-services/multidimensional-models/deploy-model-solutions-using-xmla.md)   
 [Synchronize Analysis Services Databases](../../analysis-services/multidimensional-models/synchronize-analysis-services-databases.md)   
 [Understanding the Input Files Used to Create the Deployment Script](../../analysis-services/multidimensional-models/deployment-script-files-input-used-to-create-deployment-script.md)   
 [Deploy Model Solutions with the Deployment Utility](../../analysis-services/multidimensional-models/deploy-model-solutions-with-the-deployment-utility.md)  
  
  
