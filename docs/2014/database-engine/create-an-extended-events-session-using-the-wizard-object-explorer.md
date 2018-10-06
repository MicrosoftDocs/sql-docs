---
title: "Create an Extended Events Session Using the Wizard (Object Explorer) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "Sql12.ssms.XeWizard.Summary.f1"
  - "Sql12.ssms.XeWizard.SetSessionProperties.f1"
  - "Sql12.ssms.XeWizard.CaptureAddFields.f1"
  - "Sql12.ssms.XeWizard.SelectEvents.f1"
  - "Sql12.ssms.XeWizard.SpecifySessionTargets.f1"
  - "Sql12.ssms.XeWizard.Welcome.f1"
  - "sql12.ssms.XeWizard.Welcome.f1"
  - "Sql12.ssms.XeWizard.ChooseTemplate.f1"
  - "Sql12.ssms.XeWizard.SetSessionEventFilters.f1"
  - "Sql12.ssms.XeWizard.Results.f1"
helpviewer_keywords: 
  - "Sql11.ssms.XeWizard.CaptureAddFields.f1"
  - "Sql11.ssms.XeWizard.SetSessionEventFilters.f1"
  - "Sql11.ssms.XeWizard.SpecifySessionTargets.f1"
  - "Sql11.ssms.XeWizard.Results.f1"
  - "Sql11.ssms.XeWizard.ChooseTemplate.f1"
  - "Sql11.ssms.XeWizard.SetSessionProperties.f1"
  - "Sql11.ssms.XeWizard.Welcome.f1"
  - "Sql11.ssms.XeWizard.Summary.f1"
  - "Sql11.ssms.XeWizard.SelectEvents.f1"
ms.assetid: 80c0456f-17c0-41d8-b2aa-502a2f3bb6de
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Create an Extended Events Session Using the Wizard (Object Explorer)
  To help you select and capture events on your server, Extended Events includes a New Session Wizard that guides you through the steps to create an Extended Events session. The New Session Wizard exposes most of the Extended Events functionality. The [New Session dialog](../../2014/database-engine/create-an-extended-events-session-using-the-new-session-dialog.md) also lets you define an Extended Events session that captures, displays, and analyzes your data. The New Session dialog exposes all Extended Events functionality.  
  
### To open the New Session Wizard  
  
1.  In Object Explorer, expand the **Management** node, and then expand **Extended Events**.  
  
2.  Right-click **Sessions**, and then click **New Session Wizard**.  
  
### Use the following New Session Wizard pages to create an event session  
  
-   [Introduction](#BKMK_Welcome)  
  
-   [Set Session Properties](#BKMK_SetSessionProperties)  
  
-   [Choose Template](#BKMK_ChooseTemplate)  
  
-   [Select Events to Capture](#BKMK_SelectEventsToCapture)  
  
-   [Capture Global Fields](#BKMK_CaptureGlobalFields)  
  
-   [Set Session Event Filters](#BKMK_SetSessionEventFilters)  
  
-   [Specify Session Data Storage](#BKMK_SpecifySessionDataOutput)  
  
-   [Summary](#BKMK_Summary)  
  
-   [Create Event Session](#BKMK_CreateEventSession)  
  
##  <a name="BKMK_Welcome"></a> Introduction  
 On the **Introduction** page, do the following:  
  
-   On the **Introduction** page of the New Session Wizard, click **Next**.  
  
     Select the **Do not show this page again** check box if you will be using the wizard more than once and do not want to read the introduction each time the wizard is launched.  
  
##  <a name="BKMK_SetSessionProperties"></a> Set Session Properties  
 On the **Set Session Properties** page, do the following:  
  
-   In the **Session name** box, type a meaningful name for the event session.  
  
     If you want to start the session when you start the server, select the **Start the event session at server startup** check box, and then click **Next**.  
  
##  <a name="BKMK_ChooseTemplate"></a> Choose Template  
 On the **Choose Template** page, do the following:  
  
-   Select the **Use this event session template** option to select from a set of pre-configured templates designed for common problems. Select the template you want to use from the drop-down list, and then click **Next**.  
  
     -OR-  
  
-   Select the **Do not use a template** option if you do not want to use any pre-configured template, and then click **Next**.  
  
##  <a name="BKMK_SelectEventsToCapture"></a> Select Events to Capture  
 On the **Select Events to Capture** page, do the following:  
  
1.  Select the events you want to capture from the **Event library**, and then click the right arrow. You can select multiple events in the Event Library by using either Shift+Click or Ctrl+Click.  
  
     You can select how you want to search for the events you want to capture from the drop-down list box. For example, you can search for event names or event names and their descriptions. You can search for any word in the table by entering the text you want to search for in the **Event library** box. For example, if you want to find the **lock_acquired** event, you can enter **lock**, or **lock acquire**.  
  
     The events you select appear in the **Selected events** box. To remove events from the **Selected events** box, click the left arrow.  
  
2.  After you select the events you want to capture, click **Next**.  
  
    > [!NOTE]  
    >  Events from the **Debug** channel are hidden by default. To display debug events, select **Debug** from the **Channel** drop-down list.  
  
##  <a name="BKMK_CaptureGlobalFields"></a> Capture Global Fields  
 You use global fields (also referred to as actions) to associate single or multiple actions for your selected events. If you selected a template on the **Choose Template** page, all of the global fields that are defined in the template are displayed on this page.  
  
 On the **Capture Global Fields** page, do the following:  
  
-   Select the global fields that you want to capture for the event session, and then click **Next**.  
  
     You can remove or add global fields by selecting or clearing the check box next to the global field.  
  
    > [!NOTE]  
    >  The selected actions are sorted by **Name** enabling you to view the associated actions in alphabetical order. You can sort by description or by the enable/disable status by clicking the column heading next to the field name.  
  
##  <a name="BKMK_SetSessionEventFilters"></a> Set Session Event Filters  
 You can apply filters (also called predicates) to limit the events that you want to capture. On the **Set Session Event Filters** page, do the following:  
  
1.  If you are not using a pre-configured template, create your filter criteria, and then click **Next**.  
  
     If you are using a pre-configured template, in the **Filters from template (read-only)** section, the New Session Wizard lists filters already created from the template.  
  
2.  In the **Additional filters** section, you can configure additional filters for the event session.  
  
     The filters you configure apply to all selected events. The New Session Wizard does not support configuring filters for each event.  
  
    > [!NOTE]  
    >  When you configure a group clause for your filter, redundant parentheses are removed from the filter after the result is saved. For example, if you create a filter grouping **Clause 1** and **Clause 2**, parentheses will appear around the clauses. After you save the filter, the redundant parentheses are removed. The removal of the parentheses does not affect the filter logic.  
  
##  <a name="BKMK_SpecifySessionDataOutput"></a> Specify Session Data Storage  
 You use the **Specify Session Data Storage** page to specify how you want to collect your data for analysis. SQL Server Extended Events uses targets for data output. Targets store event data and can perform actions such as writing to a file and aggregating event data. Decide how you want to collect your data for analysis, and on the **Specify Session Data Storage** page, do the following:  
  
1.  For large data sets and creating historical records, select the **Save data to a file for later analysis** check box, and then do the following:  
  
    1.  In the **File name on server** box, enter the path and file name, or click **Browse** to specify the file directory on the server where you want to save the data.  
  
    2.  In the **Maximum file size** box, specify the maximum file size to be used for the file target. If you do not specify the maximum file size, the file will grow until the disk is full. The default file size is 1 gigabyte (GB).  
  
    3.  Select the **Enable file rollover** check box to enable file rollover for the file target.  
  
    4.  In the **Maximum number of files** box, specify the maximum number of files you want to retain in the file system.  
  
2.  For collecting recent data, select the **Work with only the most recent data (ring buffer target)** check box, and then do the following:  
  
    1.  In the **Number of events to keep** box, enter or select the number of events you want to keep by using the up and down arrows.  
  
    2.  In the **Maximum buffer memory size** box, specify the maximum amount of buffer memory to use. The existing events are dropped when this value is reached.  
  
    3.  If you want to keep a specific number of events of each type in the buffer, select the **Keep a specified number of events (per type) when the buffer is full** check box.  
  
    4.  In the **Number of events to keep (per type)** box, enter or select the number of events (per type) that you want to keep by using the up and down arrows.  
  
##  <a name="BKMK_Summary"></a> Summary  
 On the **Summary** page, do the following:  
  
1.  Make sure that your selections are correct. Expand the event session nodes to verify that all of your selections will be included in the event session.  
  
2.  Click **Script** to export the session creation script to a new Query Editor window.  
  
3.  Click **Finish** to create the event session.  
  
##  <a name="BKMK_CreateEventSession"></a> Create Event Session  
 On the **Create Event Session** page, after your event session has been successfully created, do the following:  
  
1.  Click **Start the event session immediately after session creation** to start the session after you close the wizard. You must start the event session immediately after you create the session to be able to watch live data.  
  
2.  Click **Watch live data on screen as it is captured** to view live data for your event session. The live data will start displaying tracing immediately after the session is created.  
  
## See Also  
 [Create an Extended Events Session Using the New Session Dialog](../../2014/database-engine/create-an-extended-events-session-using-the-new-session-dialog.md)  
  
  
