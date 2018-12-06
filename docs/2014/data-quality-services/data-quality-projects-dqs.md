---
title: "Data Quality Projects (DQS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
ms.assetid: a43fc9c0-19b6-414a-8661-4c7c55e0c03e
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Data Quality Projects (DQS)
  A data quality project in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS) is a means of using a knowledge base to improve the quality of your source data by performing *data cleansing* and *data matching* activities, and then exporting the resultant data to a SQL Server database or a .csv file. You can create a data quality project as a cleansing project or a matching project to perform respective activities. Cleansing and matching projects can be run using the same knowledge base, because knowledge for data cleansing and matching can be built into the same knowledge base.  
  
 A data quality project has the following benefits:  
  
-   Enables you to perform data cleansing on your source data by using the knowledge in a DQS knowledge base.  
  
-   Enables you to perform data matching on your source data by using the matching policy in a knowledge base.  
  
-   Provides a wizard to guide you through the cleansing and matching activities, and export the data as per your selection to a SQL Server database or to a .csv file. The data steward can use the data quality project to run and control the computer-assisted/interactive cleansing and data matching steps.  
  
##  <a name="Cleansing"></a> Data Quality Project: Cleansing Activity  
 A cleansing data quality project enables you to cleanse your source data based on a knowledge base. The data cleansing activity in DQS is a two-step process:  
  
1.  A *computer-assisted* data cleansing process that analyzes source data against the knowledge in the knowledge base, and proposes changes. The processed data is categorized (suggested, new, invalid, corrected, and correct) by DQS, and displayed to the user for further processing.  
  
2.  An *interactive* cleansing process that enables the data steward to approve, reject, or modify the data proposed by the computer-assisted data cleansing process.  
  
 For detailed information about the cleansing activity in a data quality project, see [Data Cleansing](../../2014/data-quality-services/data-cleansing.md).  
  
##  <a name="Matching"></a> Data Quality Project: Matching Activity  
 A matching data quality project enables you to perform matching activity based on matching policy in a knowledge base to prevent data duplication by identifying exact and approximate matches, and thereby enabling you to remove duplicate data. It is recommended that you cleanse your data before running matching on it. To do so:  
  
1.  Create a data quality project, select the **Cleansing** activity, complete the data cleansing activity on your source data, and then export it to a table in a SQL Server database.  
  
2.  Create another data quality project by using a knowledge base that contains a matching policy, select the **Matching** activity, and then in the **Map** page, select the database and the table where you exported the cleansed data in step 1.  
  
3.  Complete the matching activity on the cleansed data.  
  
 For detailed information about the matching activity in a data quality project, see [Data Matching](../../2014/data-quality-services/data-matching.md).  
  
##  <a name="ProfilingNotification"></a> Data Profiling and Notifications  
 While running the cleansing and matching activities in a data quality project, you can see real-time statistics and information about the data that is being processed by DQS. Data profiling helps you assess the effectiveness of the cleansing and matching processes, and you can potentially determine the extent to which data cleansing or matching helped improve the data quality. DQS profiling provides two data-quality dimensions: *completeness* (the extent to which data is present) and *accuracy* (the extent to which data can be used for its intended use). Further, based on the data profiling information, notifications are displayed to the user on the actions that can be taken to enhance the data cleansing and data matching operations. For detailed information about data profiling and notifications, see [Data Profiling and Notifications in DQS](../../2014/data-quality-services/data-profiling-and-notifications-in-dqs.md).  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Describes how to create a data quality project.|[Create a Data Quality Project](../../2014/data-quality-services/create-a-data-quality-project.md)|  
|Describes how to manage (open, unlock, rename, and delete) a data quality project.|[Manage &#40;Open, Unlock, Rename, and Delete&#41; a Data Quality Project](../../2014/data-quality-services/manage-open-unlock-rename-and-delete-a-data-quality-project.md)|  
|Describes how to open an Integration Services project in [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)].|[Open Integration Services Projects in Data Quality Client](../../2014/data-quality-services/open-integration-services-projects-in-data-quality-client.md)|  
  
## See Also  
 [DQS Knowledge Bases and Domains](../../2014/data-quality-services/dqs-knowledge-bases-and-domains.md)  
  
  
