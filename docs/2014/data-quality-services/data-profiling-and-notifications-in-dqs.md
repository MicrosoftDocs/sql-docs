---
title: "Data Profiling and Notifications in DQS | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
ms.assetid: a778bb5b-8e35-4a7b-b04a-ae2b46dec21b
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Data Profiling and Notifications in DQS
  Data profiling in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS) is the process of analyzing the data in an existing data source, and displaying statistics about the data in DQS activities. It provides you with automated measurements of data quality. DQS profiling is integrated into DQS knowledge management and data-quality projects. It is dynamic and adjustable. Profiling has two major goals: first, to guide you through data quality processes and support your decisions, and second, to assess the effectiveness of the processes. The DQS profiling process has the following benefits:  
  
-   Profiling provides insight into the quality of your source data, and helps you identify data quality issues.  
  
-   Profiling assesses the effectiveness of data quality processes, guiding you in your knowledge discovery, data cleansing, matching policy, and matching work.  
  
-   Profiling presents you with the most relevant information at the most relevant time.  
  
-   The profiling process generates notifications that emphasize important statistics or events that may warrant action. In many cases, DQS notifications will indicate a condition and recommend the action that you can take to remedy that condition.  
  
 Profiling enables you to use Data Quality Services not only for knowledge discovery, cleansing, and matching, but also as an analysis tool. You may want to create one knowledge base for analysis, and run knowledge discovery using that knowledge base to determine from the profiling statistics whether the knowledge base satisfies your discovery, cleansing, and matching needs.  
  
##  <a name="How"></a> How Profiling Works  
 Profiling does not measure the quality of the knowledge base. It measures the quality of the source data. Profiling provides you with statistics that indicate the effect of the specific operation that you are doing in knowledge management or a data quality project on your source data. Profiling is always in the context of the specific activity that you are performing. You can click the profiling tab in a screen to display profiling data without leaving the stage of the activity that you are performing. The profiling table is populated in real time as the process is performed, enabling you to assess data quality tasks as you are performing them. You can determine whether source data is better after cleansing or de-duplication, and by how much.  
  
 All profiling numbers refer to the number of appearances of a value, and in many cases the percent of the total, with the exception of uniqueness metrics. Uniqueness metrics refer to the absolute number of values, regardless of the number of appearances of those values.  
  
 Profiling is part of the DQS knowledge-driven solution. It provides information on a knowledge base, matching, or data cleansing process based upon the mapping between data source fields and knowledge base domains. Profiling is performed only after mapping is complete; no profiling is performed during the mapping stage of any activity. Profiling is always attached to an activity. The profiling process is performed on the data that is mapped to domains, not on the data in the domains. Profiling is integrated into the following steps of activities:  
  
-   The **Discover** and **Manage domain values** steps of the Knowledge discovery activity  
  
-   The **Cleanse** and **Manage and view results** steps of the Cleansing activity  
  
-   The **Matching policy** and **Matching results** steps of the Matching policy activity  
  
-   The **Matching** and **Export** steps of the Matching activity  
  
 DQS does not provide profiling statistics for the Domain Management activity.  
  
##  <a name="Activity"></a> Profiling Data by Activity  
 DQS profiling uses standard data quality dimensions to represent the quality of the data: completeness (the extent to which data is present), accuracy (the extent to which data can be used for its intended use), and uniqueness (the extent to which different values represent different entities). By default, NULL and empty values are considered to be missing, or lower the completeness percentage; however, you can also define other values to be NULL-equivalent, in which case they will also be considered to be missing.  
  
 Profiling provides you with the statistics you need to assess your processes, but you must interpret the statistics. Make sense of what profiling is telling you by looking at the statistics column by column.  
  
 The DQS activities have different sets of profiling statistics, as follows:  
  
-   Only the Cleansing activity has profiling statistics for accuracy (in percent by domain). Accuracy is affecting by validity, consistency, syntax errors, and domain rules.  
  
-   Only the Cleansing activity has profiling statistics for correct, corrected, and suggested in the source, and corrected and suggested values by domain (both number of percent).  
  
-   The Cleansing and Knowledge Discovery activities have profiling statistics for validity (Cleansing by record, Knowledge Discovery by record and domain). The Matching Policy and Matching activities do not have statistics for validity.  
  
-   The Cleansing activity does not have profiling statistics for uniqueness. The Knowledge Discovery, Matching Policy, and Matching activities have profiling statistics for uniqueness in number and percent for the source and by domain.  
  
 For more information about the specific profiling statistics related to an activity, see the Profiling sections in the following topics:  
  
-   [Perform Knowledge Discovery](../../2014/data-quality-services/perform-knowledge-discovery.md)  
  
-   [Cleanse Data Using DQS &#40;Internal&#41; Knowledge](../../2014/data-quality-services/cleanse-data-using-dqs-internal-knowledge.md)  
  
-   [Create a Matching Policy](../../2014/data-quality-services/create-a-matching-policy.md)  
  
-   [Run a Matching Project](../../2014/data-quality-services/run-a-matching-project.md)  
  
##  <a name="Monitoring"></a> Profiling Data in Activity Monitoring  
 Profiling information for the Knowledge Discovery, Matching Policy, Matching, and Cleansing activities is available not only in the activity pages in the Data Quality client, but also in activity monitoring. Activity monitoring provides you with an overview of current and past activities. In addition to the properties and related computational processes of activities, you can view the profiling information generated for each activity in one location. You select an activity in the activity table to display profiling results in a table below. You can also export the profiling results. For more information, see [DQS Administration](../../2014/data-quality-services/dqs-administration.md).  
  
##  <a name="Notifications"></a> Notifications  
 In addition to collecting and displaying important statistics and metrics through profiling, DQS will generate notifications (if enabled) to indicate when you may want to take an action based on the displayed profiling statistics. DQS uses notifications to emphasize important facts about the data source, and to show the effectiveness of the current activity relative to the purpose for which it was executed. Notifications provide tips and recommendations that indicate a condition and recommend how you could improve a knowledge discovery, data cleansing, or data matching activity.  
  
 A DQS notification is used to raise an issue that may interest you, or to address a potential problem. Whether you act upon the notification depends upon whether it is relevant to your purposes. For example, suppose DQS posts a notification when data cleansing produces no corrected values or suggested values while completeness and accuracy are both 100%. This notification would indicate that the activity may not need to be run. Whether you choose to run the activity, however, is your decision.  
  
 A notification is indicated by a tool tip with an exclamation point in the **Profiling** tab. Statistics associated with the notification are colored red to indicate the statistical justification for the notification.  
  
 You can enable (the default) or disable notifications in the **General Settings** tab of the **Administration** section of the Data Quality Client home page. When notification is disabled, tool tips are not displayed and statistics are not colored red. There is no significant improvement in performance by disabling notifications. Profiling will still be operational if you disable notifications.  
  
 For specific conditions associated with notifications for an activity, see the following:  
  
-   [Perform Knowledge Discovery](../../2014/data-quality-services/perform-knowledge-discovery.md)  
  
-   [Cleanse Data Using DQS &#40;Internal&#41; Knowledge](../../2014/data-quality-services/cleanse-data-using-dqs-internal-knowledge.md)  
  
-   [Create a Matching Policy](../../2014/data-quality-services/create-a-matching-policy.md)  
  
-   [Run a Matching Project](../../2014/data-quality-services/run-a-matching-project.md)  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Describes how to enable or disable notifications in DQS.|[Enable or Disable Profiling Notifications in DQS](../../2014/data-quality-services/enable-or-disable-profiling-notifications-in-dqs.md)|  
  
  
