---
title: "Step 2: Add and configure logging | Microsoft Docs"
ms.custom: ""
ms.date: "01/04/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: 56105f3f-e500-4669-8c8e-acf434527727
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Lesson 3-2: Add and configure logging

In this task, you enable logging for the data flow in the Lesson 3.dtsx package. Then, you configure a Text File log provider to log the PipelineExecutionPlan and PipelineExecuteTrees events. The Text Files log provider creates logs that are easy to view and transport. The simplicity of these log files makes these files useful during the basic testing phase of a package. You can also view the log entries in the **Log Events** window of [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer.  
  
## Add logging to the package  
  
1.  On the **SSIS** menu, select **Logging**.  
  
2.  In the **Configure SSIS Logs** dialog, in the **Containers** pane, make sure the topmost object is selected. This object represents the Lesson 3 package.
  
3.  On the **Providers and Logs** tab, in the **Provider type** box, select **SSIS log provider for Text files**, and then select **Add**.  
  
    Integration Services adds a new Text File log provider to the package, with the default name **SSIS log provider for text files**. You can now configure the new log provider.  
  
4.  In the **Name** column, enter **Lesson 3 Log File**.  
  
5.  Optionally, modify the **Description**.  
  
6.  In the **Configuration** column, select **\<New Connection>** to specify where [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] writes log information.  
  
    In the **File Connection Manager Editor** dialog box, for **Usage type**, select **Create file**, and then select **Browse**. By default, the **Select File** dialog opens the project folder, but you can save log information to any location.  
  
7.  In the **Select File** dialog, in the **File name** box enter **TutorialLog.log** and select **Open**.
  
8.  Select **OK** to close the **File Connection Manager Editor** dialog.  
  
9. In the **Containers** pane, expand all nodes of the package container hierarchy, and then clear all check boxes, including the **Extract Sample Currency Data** check box. Now select the check box for **Extract Sample Currency Data** to get only the events for this node.  
  
    > [!NOTE]  
    > If the state of the **Extract Sample Currency Data** check box appears dimmed instead of selected, the task uses the log settings of the parent container and you cannot enable the log events that are specific to the task. To resolve this instance, clear the parent check box.
  
10. On the **Details** tab, in the **Events** column, select the **PipelineExecutionPlan** and **PipelineExecutionTrees** events.  
  
11. Select **Advanced** to review the details that the log provider writes to the log for each event. By default, all information categories are automatically selected for the events you specify.  
  
12. Select **Basic** to hide the information categories.  
  
13. On the **Provider and Logs** tab, in the **Name** column, select **Lesson 3 Log File**. After you have created a log provider for your package, you can optionally deselect it to turn off logging, without having to delete and re-create a log provider.  
  
14. Select **OK**.  
  
## Go to next task  
[Step 3: Test the Lesson 3 package](../integration-services/lesson-3-3-testing-the-lesson-3-tutorial-package.md)  
  
