---
title: "Step 2: Adding and Configuring Logging | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 56105f3f-e500-4669-8c8e-acf434527727
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Step 2: Adding and Configuring Logging
  In this task, you will enable logging for the data flow in the Lesson 3.dtsx package. Then, you will configure a Text File log provider to log the PipelineExecutionPlan and PipelineExecuteTrees events. The Text Files log provider creates logs that are easy to view and easily transportable. The simplicity of these log files makes these files especially useful during the basic testing phase of a package. You can also view the log entries in the Log Events window of [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer.  
  
### To add logging to the package  
  
1.  On the **SSIS** menu, click **Logging**.  
  
2.  In the **Configure SSIS Logs** dialog box, in the **Containers** pane, make sure that the topmost object, which represents the Lesson 3 package, is selected.  
  
3.  On the **Providers and Logs** tab, in the **Provider type** box, select **SSIS log provider for Text files**, and then click **Add**.  
  
     Integration Services adds a new Text File log provider to the package with the default name **SSIS log provider for text files**. You can now configure the new log provider.  
  
4.  In the **Name** column, type `Lesson 3 Log File`.  
  
5.  Optionally, modify the **Description**.  
  
6.  In the **Configuration** column, click **\<New Connection>** to specify the destination to which the log information is written.  
  
     In the **File Connection Manager Editor** dialog box, for **Usage type**, select **Create file**, and then click **Browse**. By default, the **Select File** dialog box opens the project folder, but you can save log information to any location.  
  
7.  In the **Select File** dialog box, in the **File name** box type `TutorialLog.log`, and click **Open**.  
  
8.  Click **OK** to close the **File Connection Manager Editor** dialog box.  
  
9. In the **Containers** pane, expand all nodes of the package container hierarchy, and then clear all check boxes, including the **Extract Sample Currency Data** check box. Now select the check box for **Extract Sample Currency Data** to get only the events for this node.  
  
    > [!IMPORTANT]  
    >  If the state of the **Extract Sample Currency Data** check box is dimmed instead of selected, the task uses the log settings of the parent container and you cannot enable the log events that are specific to the task.  
  
10. On the **Details** tab, in the **Events** column, select the **PipelineExecutionPlan** and **PipelineExecutionTrees** events.  
  
11. Click **Advanced** to review the details that the log provider will write to the log for each event. By default, all information categories are automatically selected for the events you specify.  
  
12. Click **Basic** to hide the information categories.  
  
13. On the **Provider and Logs** tab, in the **Name** column, select `Lesson 3 Log File`. Once you have created a log provider for your package, you can optionally deselect it to temporarily turn off logging, without having to delete and re-create a log provider.  
  
14. Click **OK**.  
  
## Next Steps  
 [Step 3: Testing the Lesson 3 Tutorial Package](../integration-services/lesson-3-3-testing-the-lesson-3-tutorial-package.md)  
  
  
