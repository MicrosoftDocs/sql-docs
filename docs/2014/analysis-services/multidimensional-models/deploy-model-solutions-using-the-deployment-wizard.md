---
title: "Deploy Model Solutions Using the Deployment Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "Analysis Services Deployment Wizard"
  - "deploying [Analysis Services], Analysis Services Deployment Wizard"
  - "Analysis Services deployments, Analysis Services Deployment Wizard"
  - "Analysis Services Deployment Wizard, about Analysis Services Deployment Wizard"
ms.assetid: ff711e8e-971c-43ba-b479-effc034af4a4
author: minewiskan
ms.author: owend
manager: craigg
---
# Deploy Model Solutions Using the Deployment Wizard
  The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard uses the XML output files generated from a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project as input files. These input files are easily modifiable to customize the deployment of an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project. The generated deployment script can then either be immediately run or saved for later deployment.  
  
 You can deploy by using the wizard as discussed here. You can also automate deployment or use the Synchronize capability. If the deployed database is large, consider using partitions on target systems. You can also automate partition creation and population by using Analysis Management Objects (AMO).  
  
> [!IMPORTANT]  
>  Neither the XML output files nor the deployment script will contain the user id or password if these are specified in either the connection string for a data source or for impersonation purposes. Since these are required for processing purposes in this scenario, you will add this information manually. If the deployment will not include processing, you can add this connection and impersonation information as needed after deployment. If the deployment will include processing, you can either add this information within the wizard or in the deployment script after it is saved.  
  
## In This Section  
 The following topics describe how to work with the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard, the input files, and the deployment script:  
  
|Topic|Description|  
|-----------|-----------------|  
|[Running the Analysis Services Deployment Wizard](running-the-analysis-services-deployment-wizard.md)|Describes the various ways in which you can run the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard.|  
|[Understanding the Input Files Used to Create the Deployment Script](deployment-script-files-input-used-to-create-deployment-script.md)|Describes which files the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard uses as input values, what each of those files contains, and provides links to topics that describe how to modify the values in each of the input files.|  
|[Understanding the Analysis Services Deployment Script](understanding-the-analysis-services-deployment-script.md)|Describes what the deployment script contains and how the script runs.|  
  
## See Also  
 [Deploy Model Solutions Using XMLA](deploy-model-solutions-using-xmla.md)   
 [Synchronize Analysis Services Databases](synchronize-analysis-services-databases.md)   
 [Understanding the Input Files Used to Create the Deployment Script](deployment-script-files-input-used-to-create-deployment-script.md)   
 [Deploy Model Solutions with the Deployment Utility](deploy-model-solutions-with-the-deployment-utility.md)  
  
  
