---
title: Data Profiling and Notifications in DQS (673f)
ms.date: 08/12/2020
ms.prod: sql
ms.technology: data-quality-services
ms.topic: conceptual
ms.author: genemi
author: MightyPen
ms.custom: at-gm-code=f-g-Q2-673f
ROBOTS: NOINDEX, NOFOLLOW
ms.localizationpriority: "null"
localization_priority: "None"
---
# Data Profiling and Notifications in Data Quality Services (DQS) (673f)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

[!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS) is a data profiling service. DQS analyzes data from a source, and displays statistics about the data in DQS activities.

## Attributes, Goals, Benefits

### Attributes of DQS

DQS profiling has the following attributes:

- Provides automated measurements of data quality.
- Is integrated into DQS knowledge management and data-quality projects.
- Is dynamic and adjustable.

### Goals of DQS

Profiling with DQS has the following two major goals:

- To guide you through data quality processes, and to support your decisions.
- To assess the effectiveness of the processes.

### Benefits of DQS

DQS profiling has the following benefits:

- Provides insight into the quality of your source data, and helps you identify data quality issues.

- Assesses the data quality processes, and guides your:
  - Knowledge discovery
  - Data cleansing
  - Matching policy
  - Matching work

- Presents you with the most relevant information at the most relevant time.

- Generates notifications that emphasize important statistics or events that might warrant action. DQS notifications often indicate a condition, and recommend corrective action.

DQS profiling provides knowledge discovery, data cleansing, and data matching. DQS is also data analysis tool. You can create a knowledge base for analysis. Then you can run knowledge discovery using the knowledge base. The knowledge discovery uses profiling statistics to determine whether the knowledge base satisfies your needs for  discovery, cleansing, and matching.

## <a name="How"></a> How DQS Profiling Works

DQS profiling does not measure the quality of the knowledge base. Profiling measures the quality of the source data. Profiling provides statistics that indicate the effect of the following efforts:

- Your specific operation in knowledge management.
- A data quality project on your source data.

### Activity Context

Profiling is always in the context of the specific activity that you are performing. You can click the profiling tab in a screen to display profiling data. This click does not take you away from the stage of the activity that you are performing. The profiling table is populated in real time as the process is performed. Therefore you can assess data quality tasks as you are performing them.

You can determine whether source data is better after cleansing or de-duplication, and by how much.

### Profiling is Based on Counts

All profiling numbers refer to the count of appearances of specific values. In many cases, the numbers refer to the percent of the total. The uniqueness metrics are an exception. Uniqueness metrics refer to the absolute number of values, regardless of the count of appearances of those values.

### Knowledge-driven Solution

Profiling is part of the DQS knowledge-driven solution. Profiling provides information on a knowledge base, matching, or data cleansing process. The information is based on the mapping between data source fields and knowledge base domains. Profiling is performed only after mapping is complete. No profiling is performed during the mapping stage of any activity.

Profiling is always attached to an activity. The profiling process is performed on the data that is _mapped_ to domains, not on the data _in_ the domains.

Profiling is integrated into the following steps of activities:

- The **Discover** and **Manage domain values** steps of the Knowledge discovery activity.

- The **Cleanse** and **Manage and view results** steps of the Cleansing activity.

- The **Matching policy** and **Matching results** steps of the Matching policy activity.

- The **Matching** and **Export** steps of the Matching activity.

DQS does not provide profiling statistics for the Domain Management activity.

## <a name="Activity"></a> Profiling Data by Activity

DQS profiling uses the following standard data quality dimensions to represent the quality of the data:

- Completeness - the extent to which data is present.
- Accuracy - the extent to which data can be used for its intended use.
- Uniqueness - the extent to which different values represent different entities.

By default, NULL and empty values are considered to be missing, or they lower the completeness percentage. However, you can define other values to be NULL-equivalent. These values are also be considered to be missing.

Profiling provides you with the statistics you need to assess your processes, but you must interpret the statistics. Make sense of what profiling is telling you by looking at the statistics column by column.

### Differing Sets of Profiling Statistics

The DQS activities have different sets of profiling statistics, as follows:

- Only the Cleansing activity has profiling statistics for accuracy (in percent by domain). Accuracy is affecting by validity, consistency, syntax errors, and domain rules.

- Only the Cleansing activity has profiling statistics for correct, corrected, and suggested in the source, and corrected and suggested values by domain (both number of percent).

- The Cleansing and Knowledge Discovery activities have profiling statistics for validity (Cleansing by record, Knowledge Discovery by record and domain). The Matching Policy and Matching activities do not have statistics for validity.

- Profiling statistics are provided for uniqueness, for the source and by domain. This statement applies to the following activities:
  - Knowledge Discovery.
  - Matching Policy.
  - Matching.
  - But _not_ for the Cleansing activity.

DQS offers specific profiling statistics that are related to an activity. For details, see the **Profiling** sections in the following topics:

- [Perform Knowledge Discovery](../data-quality-services/perform-knowledge-discovery.md)

- [Cleanse Data Using DQS &#40;Internal&#41; Knowledge](../data-quality-services/cleanse-data-using-dqs-internal-knowledge.md)

- [Create a Matching Policy](../data-quality-services/create-a-matching-policy.md)

- [Run a Matching Project](../data-quality-services/run-a-matching-project.md)

## <a name="Monitoring"></a> Profiling Data in Activity Monitoring

Profiling information is available not only in the activity pages of the Data Quality client, but also in activity monitoring. The information concerns Knowledge Discovery, Matching Policy, Matching, and Cleansing activities. The information is available in the activity pages in the Data Quality client, and in activity monitoring.

You can view all the following items in one location:

- Properties
- Related computational processes of activities
- Profiling information generated for each activity

You select an activity in the activity table to display profiling results in second table. You can also export the profiling results.

For more information, see [DQS Administration](../data-quality-services/dqs-administration.md).

## <a name="Notifications"></a> Notifications

DQS generates notifications to indicate when you might want to take an action based on profiled statistics. DQS uses notifications for important facts about the data source. These facts show the effectiveness of the current activity relative to the purpose for which it was executed. Notifications provide tips and recommendations that indicate a condition. Notifications can also recommend how you can improve a knowledge discovery, data cleansing, or data matching activity.

DQS sends a notification when it detects an issue that might matter to you. As an example situation, when completeness and accuracy are both 100%, the data cleansing activity might produce no corrected or suggested values. DQS might post a notification for that situation. This notification would indicate that the activity might not be needed. Whether to run the activity remains your decision.

A notification is indicated by a tool tip having an exclamation point, in the **Profiling** tab. Statistics associated with the notification are colored red to indicate the statistical justification for the notification.

### Disable notifications

Notifications are enabled by default. But you can disable notifications by using the Data Quality Client home page. On the **Administration** section, use the **General Settings** tab.

When notifications are disabled, tool tips are not displayed and statistics are not colored red. Profiling remains operational while notifications are disabled. There is no improvement in performance by disabling notifications.

For specific conditions associated with notifications for an activity, see the following:

- [Perform Knowledge Discovery](../data-quality-services/perform-knowledge-discovery.md)

- [Cleanse Data Using DQS &#40;Internal&#41; Knowledge](../data-quality-services/cleanse-data-using-dqs-internal-knowledge.md)

- [Create a Matching Policy](../data-quality-services/create-a-matching-policy.md)

- [Run a Matching Project](../data-quality-services/run-a-matching-project.md)

## Related Tasks

| Task description | Topic |
| :--------------- | :---- |
| Describes how to enable or disable notifications in DQS. | [Enable or Disable Profiling Notifications in DQS](../data-quality-services/enable-or-disable-profiling-notifications-in-dqs.md) |
|||
