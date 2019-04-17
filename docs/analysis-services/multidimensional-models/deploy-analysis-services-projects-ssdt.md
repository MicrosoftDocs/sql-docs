---
title: "Deploy Analysis Services Projects (SSDT) | Microsoft Docs"
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
# Deploy Analysis Services Projects (SSDT)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  During development of an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], you frequently deploy the project to a development server in order to create the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database defined by the project. This is required to test the project; for example, to browse cells in the cube, browse dimension members, or verify key performance indicators (KPIs) formulas.  
  
## Deploying a Project  
 You can deploy a project independently, or you can deploy all of the projects within the solution. When you deploy a project, several things happen in sequence. First, the project is built. This step creates the output files that define the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database and its constituent objects. Next, the destination server is validated. Finally, the destination database and its objects are created on the destination server. During deployment, the deployment engine totally replaces any pre-existing database with the contents of the project unless those objects were created by the project during a previous deployment.  
  
 After an initial deployment, an IncrementalSnapshot.xml file is generated in the \<Project Name>\obj folder. This file is used to determine if the database or its objects on the destination server have changed outside of the project. If so, you will be prompted to overwrite all objects in the destination database. If all changes were made within the project and the project is configured for incremental deployment, only the changes will be deployed to the destination server.  
  
 The project configuration and its associated settings determine the deployment properties that will be used to deploy the project. For a shared project, each developer can use their own configuration with their own project configuration options. For example, each developer can specify a different test server to work in isolation from other developers.  
  
## See Also  
 [Create an Analysis Services Project &#40;SSDT&#41;](../../analysis-services/multidimensional-models/create-an-analysis-services-project-ssdt.md)  
  
  
