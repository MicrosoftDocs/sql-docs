---
title: "Data Quality Client Home Screen | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dqs.clienthome.f1"
ms.assetid: 7c6ec469-bc7d-4d19-8e21-11dcf8ade108
author: leolimsft
ms.author: lle
manager: craigg
---
# Data Quality Client Home Screen
  Use this screen to gain access to the user interfaces for each the three major [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS) groups of tasks: knowledge base management, data quality projects, and administration.  
  
## Options  
  
### Knowledge Base Management  
 A DQS knowledge base is a repository of metadata that is used by DQS to improve the quality of data. This metadata is created both by the DQS platform in a computer-assisted knowledge discovery process and by the data steward in an interactive domain management process.  
  
 **New Knowledge Base**  
 Start the process of creating a knowledge base either from scratch or based upon the metadata of an existing knowledge base. This command opens a page in which you can identify the knowledge base, base it on an existing knowledge base, select the desired knowledge base activity, and then create the knowledge base.  
  
 **Open Knowledge Base**  
 Open a knowledge base so you can manage its domains, perform knowledge discovery, or build a matching policy. Clicking the **Open Knowledge Base** button displays the **Open Knowledge Base** page that shows a list of existing knowledge bases with their properties, current state, knowledge base, and details of their domains. Select a knowledge base and open it from the **Open Knowledge Base**.  
  
 **Recent Knowledge Base**  
 From the list on the screen, open a knowledge base that has already been created. If not locked, click the right arrow and then select the activity that you want to start the knowledge base in (Domain Management, Knowledge Discovery, or Matching Policy).  
  
 You can open a locked knowledge base and edit it only if you locked it. If so, the knowledge base will be opened in the state that it was in when it was closed, which is indicated in parentheses. If a knowledge base is locked, and you did not lock it, you can only open it as read-only.  
  
### Data Quality Projects  
 A data quality project is the process in which DQS performs data cleansing or data matching, both through computer-assisted data correction and interactive data cleansing.  
  
 **New Data Quality Project**  
 Start the project of creating a new project. This command opens a page in which you can identify the project, associate it with a knowledge base, display details of the knowledge base, select the desired project activity, and then create the project.  
  
 **Open Data Quality Project**  
 Open a project so you can perform data cleansing or data matching. Clicking the **Open data quality project** button displays the **Open data quality project** page that shows a list of existing projects with their properties, current state, knowledge base, and details of their domains and matching policy rules. Select a project and open it from the **Open data quality project**.  
  
 **Recent data quality project**  
 From the list on the screen, select a project that has already been created. You can open a locked project only if you locked it. If so, the project will be opened in the state that it was in when it was closed, which is indicated in parentheses. If the project was completed, it will be opened in the Export step of the activity.  
  
### Administration  
 DQS administration enables you to monitor, configure, and maintain DQS.  
  
 **Activity Monitoring**  
 Display a view of the status of all activities (both current and historical) that are related to the connected [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)]. The types of activities monitored include Knowledge Management, Data Quality Project, and SSIS-based data correction.  
  
 **Configuration**  
 Display the configuration properties for reference data service accounts (both through Windows Azure Marketplace and directly to reference data services), general settings (interactive cleansing, matching, and profiling) and log severity settings.  
  
## See Also  
 [DQS Knowledge Bases and Domains](../../2014/data-quality-services/dqs-knowledge-bases-and-domains.md)   
 [Data Quality Projects &#40;DQS&#41;](../../2014/data-quality-services/data-quality-projects-dqs.md)   
 [DQS Administration](../../2014/data-quality-services/dqs-administration.md)  
  
  
