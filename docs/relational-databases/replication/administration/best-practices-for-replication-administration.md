---
title: "Best Practices for Replication Administration | Microsoft Docs"
description: After you configure replication, use these best practices to administer your replication topology in SQL Server.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "administering replication, best practices"
  - "replication [SQL Server], administering"
ms.assetid: 850e8a87-b34c-4934-afb5-a1104f118ba8
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Best Practices for Replication Administration
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  After you have configured replication, it is important to understand how to administer a replication topology. This topic provides basic best practice guidance in a number of areas with links to more information for each area. In addition to following the best practice guidance presented in this topic, consider reading through the frequently asked questions topic to acquaint yourself with common questions and issues: [Frequently Asked Questions for Replication Administrators](../../../relational-databases/replication/administration/frequently-asked-questions-for-replication-administrators.yml).  
  
 It is useful to divide the best practice guidance into two areas:  
  
-   The following information covers best practices that should be implemented for all replication topologies:  
  
    -   Develop and test a backup and restore strategy.  
  
    -   Script the replication topology.  
  
    -   Create thresholds and alerts.  
  
    -   Monitor the replication topology.  
  
    -   Establish performance baselines and tune replication if necessary.  
  
-   The following information covers best practices that should be considered, but might not be required for your topology:  
  
    -   Validate data periodically.  
  
    -   Adjust agent parameters through profiles.  
  
    -   Adjust publication and distribution retention periods.  
  
    -   Understand how to change article and publication properties if application requirements change.  
  
    -   Understand how to make schema changes if application requirements change.  
  
## Develop and test a backup and restore strategy  
 All databases should be backed up on a regular basis, and the ability to restore those backups should be tested periodically; replicated databases are no different. The following databases should be backed up regularly:  
  
-   Publication database  
  
-   Distribution database  
  
-   Subscription databases  
  
-   **msdb** database and **master** database at the Publisher, Distributor, and all Subscribers  
  
 Replicated databases require special attention with regard to backing up and restoring data. For more information, see [Back Up and Restore Replicated Databases](../../../relational-databases/replication/administration/back-up-and-restore-replicated-databases.md).  
  
## Script the replication topology  
 All replication components in a topology should be scripted as part of a disaster recovery plan, and scripts can also be used to automate repetitive tasks. A script contains the [!INCLUDE[tsql](../../../includes/tsql-md.md)] system stored procedures necessary to implement the replication component(s) scripted, such as a publication or subscription. Scripts can be created in a wizard (such as the New Publication Wizard) or in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] after you create a component. You can view, modify, and run the script using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or **sqlcmd**. Scripts can be stored with backup files to be used in case a replication topology must be reconfigured. For more information, see [Scripting Replication](../../../relational-databases/replication/scripting-replication.md).  
  
 A component should be rescripted if any property changes are made. If you use custom stored procedures with transactional replication, a copy of each procedure should be stored with the scripts; the copy should be updated if the procedure changes (procedures are typically updated due to schema changes or changing application requirements). For more information about custom procedures, see [Specify How Changes Are Propagated for Transactional Articles](../../../relational-databases/replication/transactional/transactional-articles-specify-how-changes-are-propagated.md).  
  
## Establish performance baselines and tune replication if necessary  
 Before replication is configured, it is recommended to familiarize yourself with the factors that affect replication performance:  
  
-   Server and network hardware  
  
-   Database design  
  
-   Distributor configuration  
  
-   Publication design and options  
  
-   Filter design and use  
  
-   Subscription options  
  
-   Snapshot options  
  
-   Agent parameters  
  
-   Maintenance  
  
 After replication is configured, it is recommended to develop a performance baseline, which will allow you to determine how replication behaves with a workload that is typical for your applications and topology. Use Replication Monitor and System Monitor to determine typical numbers for the following five dimensions of replication performance:  
  
-   Latency: the amount of time it takes for a data change to be propagated between nodes in a replication topology.  
  
-   Throughput: the amount of replication activity (measured in commands delivered over a period of time) a system can sustain over time.  
  
-   Concurrency: the number of replication processes that can operate on a system simultaneously.  
  
-   Duration of synchronization: how long it takes a given synchronization to complete.  
  
-   Resource consumption: hardware and network resources used as a result of replication processing.  
  
 Latency and throughput are most relevant to transactional replication, because systems built on transactional replication generally require low latency and high throughput. Concurrency and duration of synchronization are most relevant to merge replication, because systems built on merge replication often have a large number of Subscribers, and a Publisher can have a significant number of concurrent synchronizations with these Subscribers.  
  
 After you have established baseline numbers, set thresholds in Replication Monitor. For more information, see [Set Thresholds and Warnings in Replication Monitor](../../../relational-databases/replication/monitor/set-thresholds-and-warnings-in-replication-monitor.md) and [Use Alerts for Replication Agent Events](../../../relational-databases/replication/agents/use-alerts-for-replication-agent-events.md). If you encounter a performance problem, it is recommended to read through the suggestions in the enhancing performance topics listed above and to apply changes in areas that affect the issues you are encountering.  
  
## Create thresholds and alerts  
 Replication Monitor allows you to set a number of thresholds related to status and performance. It is recommended to set the appropriate thresholds for your topology; if a threshold is reached, a warning is displayed, and, optionally, an alert can be sent to an e-mail account, a pager, or other device. For more information, see [Set Thresholds and Warnings in Replication Monitor](../../../relational-databases/replication/monitor/set-thresholds-and-warnings-in-replication-monitor.md).  
  
 In addition to the alerts that can be associated with monitoring thresholds, replication provides a number of predefined alerts that respond to replication agent actions. These alerts can be used by an administrator to stay informed about the state of the replication topology. It is recommended to read through the topic describing the alerts and to use any that fit your administration needs (it is also possible to create additional alerts if necessary). For more information, see [Use Alerts for Replication Agent Events](../../../relational-databases/replication/agents/use-alerts-for-replication-agent-events.md).  
  
## Monitor the replication topology  
 After the replication topology is in place and thresholds and alerts have been configured, it is recommended to regularly monitor replication. Monitoring a replication topology is an important aspect of deploying replication. Because replication activity is distributed, it is essential to track activity and status across all computers involved in replication. The following tools can be used to monitor replication:  
  
-   Replication Monitor is the most important tool for monitoring replication, allowing you to monitor the overall health of a replication topology. For more information, see [Monitoring Replication](../../../relational-databases/replication/monitor/monitoring-replication.md).  
  
-   [!INCLUDE[tsql](../../../includes/tsql-md.md)] and Replication Management Objects (RMO) provide interfaces for monitoring replication. For more information, see [Monitoring Replication](../../../relational-databases/replication/monitor/monitoring-replication.md).  
  
-   System Monitor can also be useful for monitoring replication performance. For more information, see [Monitoring Replication with System Monitor](../../../relational-databases/replication/monitor/monitoring-replication-with-system-monitor.md).  
  
## Validate data periodically  
 Validation is not required by replication, but it is recommended to run validation periodically for transactional replication and merge replication. Validation allows you to verify that data at the Subscriber matches data at the Publisher. Successful validation indicates that at that point in time all changes from the Publisher have been replicated to the Subscriber (and from the Subscriber to the Publisher if updates are supported at the Subscriber) and that the two databases are in sync.  
  
 It is recommended that validation be performed according to the backup schedule of the publication database. For example, if the publication database has a full backup once per week, validation could be run once per week after the backup completes. For more information, see [Validate Replicated Data](../../../relational-databases/replication/validate-data-at-the-subscriber.md).  
  
## Use agent profiles to change agent parameters if necessary  
 Agent profiles provide a convenient method of setting replication agent parameters. Parameters can also be specified on the agent command line, but it is typically more appropriate to use a predefined agent profile or to create a new profile if you need to change the value of a parameter. For example, if you are using merge replication and a Subscriber moves from a broadband connection to a dialup connection, consider using the **slow link** profile for the Merge Agent; this profile uses a set of parameters that are better suited to the slower communications link. For more information, see [Replication Agent Profiles](../../../relational-databases/replication/agents/replication-agent-profiles.md).  
  
## Adjust publication and distribution retention periods if necessary  
 Transactional replication and merge replication use retention periods to determine, respectively, how long transactions are stored in the distribution database, and how frequently a subscription must synchronize. It is recommended to use the default settings initially, but to monitor your topology to determine if the settings require adjustment. For example, in the case of merge replication, the publication retention period (which defaults to 14 days) determines how long metadata is stored in system tables. If subscriptions always synchronize within five days, consider adjusting the setting to a lower number, which will reduce metadata and possibly provide better performance. For more information, see [Subscription Expiration and Deactivation](../../../relational-databases/replication/subscription-expiration-and-deactivation.md).  
  
## Understand how to modify publications if application requirements change  
 After you have created a publication, it might be necessary to add or drop articles, or change publication and article properties. Most changes are allowed after a publication is created, but in some cases, it is necessary to generate a new snapshot for a publication and/or reinitialize subscriptions to the publication. For more information, see [Change Publication and Article Properties](../../../relational-databases/replication/publish/change-publication-and-article-properties.md) and [Add Articles to and Drop Articles from Existing Publications](../../../relational-databases/replication/publish/add-articles-to-and-drop-articles-from-existing-publications.md).  
  
## Understand how to make schema changes if application requirements change  
 In many cases, schema changes are required after an application is in production. In a replication topology, these changes must often be propagated to all Subscribers. Replication supports a wide range of schema changes to published objects. When you make any of the following schema changes on the appropriate published object at a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Publisher, that change is propagated by default to all [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers:  
  
-   ALTER TABLE  
  
-   ALTER VIEW  
  
-   ALTER PROCEDURE  
  
-   ALTER FUNCTION  
  
-   ALTER TRIGGER  
  
 For more information, see [Make Schema Changes on Publication Databases](../../../relational-databases/replication/publish/make-schema-changes-on-publication-databases.md).  
  
## See Also  
 [Replication Administration FAQ](../../../relational-databases/replication/administration/frequently-asked-questions-for-replication-administrators.yml)  
  
  
