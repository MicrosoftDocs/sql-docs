---
title: "Configure Severity Levels for DQS Log Files | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "data-quality-services"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dqs.admin.config.log.f1"
helpviewer_keywords: 
  - "severity levels"
  - "log files,severity levels"
  - "dqs log files,severity levels"
  - "logging,severity levels"
  - "configure severity levels"
ms.assetid: 66ffcdec-4bf7-4dd5-a221-fd9baefeeef4
author: leolimsft
ms.author: lle
manager: craigg
---
# Configure Severity Levels for DQS Log Files

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  This topic describes how to configure severity levels for various activities and modules in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS) by using [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)]. Severity levels define the intensity of events that occur in DQS. DQS events have the following severity levels, in the decreasing order of severity:  
  
-   **Fatal**: Critical runtime errors that might cause severe/unexpected results.  
  
-   **Error**: Other runtime errors.  
  
-   **Warn**: Warning about events that might result in an error.  
  
-   **Info**: Information about general events that is not an error or a warning. For example, a DQS process has started.  
  
-   **Debug**: Detailed (verbose) information about the event.  
  
 By configuring severity levels for various DQS activities and modules, you are filtering the information that you want to be logged, and written to the DQS log file for the respective DQS activity or module. For example, if you set the severity level of a DQS activity to **Warn**, only warning and higher severity messages (Error and Fatal) associated with the DQS activity will be logged.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_administrator role on the DQS_MAIN database to configure log severity settings.  
  
##  <a name="ConfigureActivity"></a> Configure Severity Levels at Activity Level  
 You can configure log severity settings for the following activities in DQS: domain management, knowledge discovery, matching policy, data cleansing, data matching, and reference data services. To do so:  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, click **Configuration**.  
  
3.  Next, click the **Log Settings** tab. The following DQS activities are listed for which you can select a severity level: **Domain Management**, **Knowledge Discovery**, **Cleansing Project (Ex. RDS)**, **Matching Policy and Matching Project**, and **RDS**.  
  
4.  For a DQS activity, select the severity level that you want to be logged. You can select one among the following: **Fatal**, **Error**, **Warn**, **Info**, and **Debug**. For example, if you want only fatal messages to be written to the DQS log files for the knowledge discovery activity, select **Fatal** in the drop-down list against the **Knowledge Discovery** activity.  
  
    > [!NOTE]  
    >  By default, **Error** is selected for each of the activities. This implies that error and fatal messages will be written to the DQS log files for each activity, by default.  
  
5.  Click **Close**.  
  
##  <a name="ConfigureModule"></a> Configure Severity Levels at Module Level (Advanced)  
 The **Advanced** section in the **Log Settings** tab enables you to configure log severity settings at a module level. Modules are DQS system assemblies that implement various functionalities within a feature in DQS. For example, the domain management activity contains various functionalities such as defining domain rules, defining rule conditions, defining cross-domain rules for composite domains, and so on.  
  
 At times, the granularity level at the activity level is not sufficient. You might want to investigate an issue that is occurring in a particular module within an activity. It helps to have an option to configure log severities at the module level to isolate and track the issue more precisely.  
  
 The log severity setting specified at the activity level determines the log severity setting of all the modules that constitute the activity. However, if there is any conflict between the log severity settings at the activity and module levels, the severity settings at the module level are considered.  
  
> [!NOTE]
>  -   By default, the **Microsoft.Ssdqs.Core.Startup** module is preconfigured in the **Advanced** section with the severity level set as **Info**. This is done to enable logging of events of severity Info and higher (Warn, Error, and Fatal) that are related to starting and stopping of DQS services.  
> -   You should configure log severity levels at the module level only if you are an advanced DQS user who is familiar with the DQS system assemblies.  
  
 To configure log severity levels at the module level:  
  
1.  In the **Log Settings** tab, click the down arrow against **Advanced** to display the area.  
  
2.  In the grid that appears, select a module name from the drop-down list in the **Module** column.  
  
3.  Next, select a severity level for the module from the drop-down list in the **Severity** column. You can select one among the following: **Fatal**, **Error**, **Warn**, **Info**, and **Debug**.  
  
     For example, within the domain management activity, you can set a different granularity level for the domain rule definition functionality than the domain management activity by selecting the **Microsoft.Ssdqs.DomainRules.Define** module, and selecting a different log severity level. Similarly, you can set a different granularity level for the cross-domain rule functionality by selecting the **Microsoft.Ssdqs.DomainRules.Condition.CrossDomain** module, and selecting a different log severity level.  
  
4.  Repeat steps 2 and 3 for other modules, if required. You can also add or delete rows to the grid by clicking the **Add Module** and **Remove Module** icons.  
  
5.  Click **Close**.  
  
## See Also  
 [Configure Advanced Settings for DQS Log Files](../data-quality-services/configure-advanced-settings-for-dqs-log-files.md)  
  
  
