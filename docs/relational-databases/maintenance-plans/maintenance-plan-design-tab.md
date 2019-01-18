---
title: "Maintenance Plan (Design Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.maint.maintplanproperties.optimizations.f1"
  - "sql13.swb.maint.planeditor.f1"
  - "sql13.swb.maint.subplaneditor.f1"
ms.assetid: 6d20d4d4-5b3f-454a-8a05-f0aac803c5ad
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Maintenance Plan (Design Tab)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Use the **Maintenance Plan (Design Tab)** to specify the properties of a maintenance plan and its subplans. Drag tasks from the Toolbox to the plan designer. Right-click groups of tasks to create branching execution paths. Maintenance plans are saved as [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages that are run by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs.  
  
## Options  
 **Add Subplan**  
 Add a subplan that you can configure.  
  
 **Subplan Properties**  
 Display the **Subplan Properties** dialog box. Select a subplan in the grid and click this icon to enter a name, description, and schedule for the subplan. You can also double-click the subplan in the grid to display the **Subplan Properties** dialog box. Subplan names are limited to 128 characters and subplan descriptions are limited to 512 characters.  
  
 **Delete Selected Subplan**  
 Delete the selected subplan.  
  
 **Subplan Schedule**  
 Display the **Job Schedule Properties** dialog box. Select a subplan in the grid and click this icon to configure a schedule for the subplan.  
  
 **Remove Schedule**  
 Remove a schedule from the selected subplan.  
  
 **Manage Connections**  
 Display the **Manage Connections** dialog box. Used to add additional [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance connections to the maintenance plan. Each maintenance task in the subplan editor can use any of these connections. When executing, the maintenance plan makes a connection from the maintenance plan server to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] servers specified, using the connection credentials.  
  
 **Reporting and Logging**  
 Display the **Reporting and Logging** dialog box, used to manage reports concerning maintenance plan activity, and to configure logging to the local or a remote server.  
  
 **Servers**  
 Display the **Servers** dialog box, which is used to select the servers where the subplan tasks will be run. This option is enabled only on master servers in multiserver environments. For more information, see [Create a Multiserver Environment](../../ssms/agent/create-a-multiserver-environment.md).  
  
 **Name**  
 Display the maintenance plan name. For new maintenance plans, the name is specified in a dialog box before the maintenance plan designer opens. To rename a maintenance plan, right-click the plan in Object Explorer, and then click **Rename**.  
  
 **Description**  
 View or specify a description for the maintenance plan. The maximum length for a description is 512 characters.  
  
 **Designer Surface**  
 Design and maintain maintenance plans. Use the designer surface to add maintenance tasks to a plan, remove tasks from a plan, specify precedence links between the tasks, and indicate task branching and parallelism.  
  
 A precedence link between two tasks establishes a relationship between the tasks. The second task (the *dependent task*) executes only if the execution result of the first task (the *precedent task*) matches specified criteria. Typically the execution result specified is **Success**, **Failure**, or **Completion**. The maintenance plan designer surface is based on the [!INCLUDE[ssIS](../../includes/ssis-md.md)] designer surface. For more information, see [Precedence Constraints](../../integration-services/control-flow/precedence-constraints.md).  
  
 As an example, a Defragment Index Task could be specified to execute only if a previous Check Database Integrity task completed successfully. The task precedence linkage feature also allows for error or failure conditions to be handled in a plan. For example, if the Check Database Integrity task failed, a Notify Operator task could notify a user or operator about the failure.  
  
 Specifying tasks to execute after the failure of a predecessor task is an example of *task branching*.  
  
 Indicating that two or more tasks begin simultaneously, for example upon the successful completion of a predecessor task, is an example of specifying *task parallelism*. All tasks with no constraints will start and run in parallel. Use constraints to delay  tasks so earlier tasks complete first.  
  
 After a maintenance task is placed on the design surface, its properties can be edited as needed. For example, the specific database to back up in a Back Up Database Task is specified after the task is added the plan. Tasks on the design surface that are not properly configured contain a red icon with a white x.  
  
 To add a maintenance task to a plan, drag the task's icon from the **Maintenance Plan Tasks** toolbox to the plan design surface, or double-click the task in the toolbox, which adds that task to the currently active designer surface. If the **Maintenance Plan Tasks** toolbox is not visible, choose **Toolbox** from the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] **View** menu. Expand the **Maintenance Plan Tasks** node in the **Toolbox** pane.  
  
 To remove a task from a plan, select the task in the designer surface and press the **DELETE** key, or right-click the task and then click **Delete**.  
  
 To specify precedence links between two tasks, first drag the tasks to the design surface, and then click the task that occurs first (the precedent task), and drag the arrow to the dependent task. When a precedence link has been established, the designer displays an arrow linking the two tasks, with the precedent task pointing to the dependent task. By default, when a link is first established, the link's constraint is set such that the dependent task only executes if the execution result of the precedent task is **Success**.  
  
 To change the properties of a precedence link, double-click the link to launch the **Precedence Constraint Editor**. This provides many options for specifying the logical conditions which determine whether the dependent task executes. For example, the **Execution result** can be set to **Failure**, in which case the dependent task only executes if the precedent task fails. Modifying the execution result property of a link to **Success**, **Failure**, or **Completion**, can also be accomplished by right-clicking the link and then selecting from the context menu.  
  
 To specify task branching, first create precedence links between two tasks. Then, put another dependent task on the design surface that executes on a different outcome than the first dependent task. Click the predecessor task, and drag the second arrow from the precedent task to the dependent task. To change the execution result (**Success**, **Failure**, **Completion**) that causes a dependent task to execute, double-click the link arrow and modify the **Execution result** field. Alternatively, right-click the link and select the desired execution result value from the shortcut menu.  
  
 To specify task parallelism, link two or more dependent tasks to a single precedent task. Modify the properties of the precedence links so that the links pointing to the dependent tasks that run in parallel have the same value for their execution result fields.  
  
## Additional Features Available from the Shortcut Menu  
 To see additional options, select one or more tasks on the design surface, and then right-click, to open the shortcut menu. In addition to typical **Cut**, **Copy**, **Paste**, **Delete**, and **Select All**, the following special options are available for some tasks.  
  
 **Add Annotation**  
 Adds a descriptive note to the design surface.  
  
 **Edit**  
 Opens the property dialog box for the task.  
  
 **Disable**  
 Makes the task temporarily unavailable.  
  
 **Enable**  
 Restores a disabled task.  
  
 **Group**  
 Creates a group that contains one or more tasks.  
  
 **Ungroup**  
 Removes tasks from a group.  
  
 **Autosize**  
 Sets the size of each task to the optimal size for that task.  
  
 **Collapse**  
 Hides tasks within a group.  
  
 **Expand**  
 Shows the tasks in a group that were previously hidden using **Collapse**.  
  
 **Zoom**  
 Changes the size of the tasks on the design surface  
  
## See Also  
 [Maintenance Plans](../../relational-databases/maintenance-plans/maintenance-plans.md)   
 [Create a Maintenance Plan](../../relational-databases/maintenance-plans/create-a-maintenance-plan.md)  
  
  
