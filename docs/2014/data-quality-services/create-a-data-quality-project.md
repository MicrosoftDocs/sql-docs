---
title: "Create a Data Quality Project | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dqs.dqproject.newdqproject.f1"
helpviewer_keywords: 
  - "create,data quality project"
  - "data quality project,create"
ms.assetid: 19c52d2b-d28e-4449-ab59-5fe0dc326cd9
author: leolimsft
ms.author: lle
manager: craigg
---
# Create a Data Quality Project
  This topic describes how to create a data quality project by using [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)]. A data quality project is used to run the cleansing or matching activity in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS).  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 You must have a relevant knowledge base to use in the data quality project for the cleansing or matching activity.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor or dqs_kb_operator role on the DQS_MAIN database to create a data quality project.  
  
##  <a name="Create"></a> Create a Data Quality Project  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../../2014/data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, click **New data quality project**.  
  
3.  In the **New Data Quality Project** screen:  
  
    1.  In the **Name** box, type a name for the new data quality project.  
  
    2.  (Optional) In the **Description** box, type a description for the new data quality project.  
  
    3.  In the **Use Knowledge base** list, click to select a knowledge base to be used for the data quality project. The **Knowledge base details: <Knowledge_Base_Name>** area on the right side displays the domain names available in the selected knowledge base.  
  
    4.  In the **Select Activity** area, click on an activity that you want to perform using this data quality project:  
  
        -   **Cleansing**: Select this activity to cleanse the source data.  
  
        -   **Matching**: Select this activity to perform matching. This activity is available only if the knowledge base selected for the data quality project contains a matching policy.  
  
4.  Click **Create** to create a data quality project.  
  
##  <a name="FollowUp"></a> Follow Up: After Creating a Data Quality Project  
 After you create a data quality project, you are presented with a wizard that you use to perform the selected activity: cleansing or matching. For more information about the cleansing and matching activities, see [Data Cleansing](../../2014/data-quality-services/data-cleansing.md) and [Data Matching](../../2014/data-quality-services/data-matching.md).  
  
  
