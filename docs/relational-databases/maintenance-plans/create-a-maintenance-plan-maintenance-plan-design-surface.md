---
title: "Create a Maintenance Plan with the Design Surface"
description: Learn how to create a single server or multiserver maintenance plan by using the Maintenance Plan Design Surface in SQL Server.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "Maintenance Plan Design Surface"
ms.assetid: 2ef803ee-a9f8-454a-ad63-fedcbe6838d1
---
# Create a Maintenance Plan (Maintenance Plan Design Surface)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to create a single server or multiserver maintenance plan using the Maintenance Plan Design Surface in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]. While the **Maintenance Plan Wizard** is best for creating basic maintenance plans, creating a plan using the design surface allows you to utilize enhanced workflow.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   [Creating a maintenance plan using the Maintenance Plan Design Surface](#SSMSProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   To create a multiserver maintenance plan, a multiserver environment containing one master server and one or more target servers must be configured. Multiserver maintenance plans must be created and maintained on the master server. These plans can be viewed, but not maintained, on target servers.  
  
-   Members of the **db_ssisadmin** and **dc_admin** roles may be able to elevate their privileges to **sysadmin**. This elevation of privilege can occur because these roles can modify [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages; these packages can be executed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using the **sysadmin** security context of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. To guard against this elevation of privilege when running maintenance plans, data collection sets, and other [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages, configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs that run packages to use a proxy account with limited privileges or only add **sysadmin** members to the **db_ssisadmin** and **dc_admin** roles.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 To create or manage maintenance plans, you must be a member of the **sysadmin** fixed server role. Object Explorer only displays the **Maintenance Plans** node for users who are members of the **sysadmin** fixed server role.  
  
##  <a name="SSMSProcedure"></a> Using Maintenance Plan Design Surface  
  
#### To create a maintenance plan  
  
1.  In Object Explorer, click the plus sign to expand the server where you want to create a maintenance plan.  
  
2.  Click the plus sign to expand the **Management** folder.  
  
3.  Right-click the **Maintenance Plans** folder and select **New Maintenance Plan**.  
  
4.  In the **New Maintenance Plan** dialog box, in the **Name** box, type a name for the plan and click **OK**. This opens the  Toolbox and the *maintenance_plan_name* **[Design]** surface with the **Subplan_1** subplan created in the main grid.  
  
     The following options are available in the design space's header.  
  
     **Add Subplan**  
     Adds a subplan that you can configure.  
  
     **Subplan Properties**  
     Displays the **Subplan Properties** dialog box for the selected subplan in the main grid. Alternately, double-click a subplan in the grid to display the **Subplan Properties** dialog box. See below for more information on this dialog box.  
  
     **Delete Selected Subplan**  
     Deletes the selected subplan.  
  
     **Subplan Schedule**  
     Displays the **New Job Schedule** dialog box for the selected subplan.  
  
     **Remove Schedule**  
     Removes a schedule from the selected subplan.  
  
     **Manage Connections**  
     Displays the **Manage Connections** dialog box. Used to add additional [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance connections to the maintenance plan. See below for more information on this dialog box.  
  
     **Reporting and Logging**  
     Displays the **Reporting and Logging** dialog box. See below for more information on this dialog box.  
  
     **Servers**  
     Display the **Servers** dialog box, which is used to select the servers where the subplan tasks will be run. This option is enabled only on master servers in multiserver environments. For more information, see [Create a Multiserver Environment](../../ssms/agent/create-a-multiserver-environment.md) and [Maintenance Plan &#40;Servers&#41;](../../relational-databases/maintenance-plans/maintenance-plan-servers.md).  
  
     **Name**  
     Display the maintenance plan name. For new maintenance plans, the name is specified in a dialog box before the maintenance plan designer opens. To rename a maintenance plan, right-click the plan in Object Explorer, and then click **Rename**.  
  
     **Description**  
     View or specify a description for the maintenance plan. The maximum length for a description is 512 characters.  
  
     **Designer Surface**  
     Design and maintain maintenance plans. Use the designer surface to add maintenance tasks to a plan, remove tasks from a plan, specify precedence links between the tasks, and indicate task branching and parallelism.  
  
     A precedence link between two tasks establishes a relationship between the tasks. The second task (the *dependent task*) executes only if the execution result of the first task (the *precedent task*) matches specified criteria. Typically the execution result specified is **Success**, **Failure**, or **Completion**. For more information, see step **8** below.  
  
5.  In the design space's header, double-click **Subplan_1** and enter a name and description for the subplan in the **Subplan Properties** dialog box.  
  
     The following options are available in the **Subplan Properties** dialog box.  
  
     **Name**  
     The name of the subplan.  
  
     **Description**  
     A brief description of the subplan.  
  
     **Schedule**  
     Indicates on what schedule the subplan will be run. Click **Subplan Schedule** to open the **New Job Schedule** dialog box. Click **Remove Schedule** to delete the schedule from the subplan.  
  
     **Run as** list  
     Select the account to use to run this subtask.  
  
6.  Click **Subplan Schedule** to enter schedule details in the **New Job Schedule** dialog box.  
  
7.  To build the subplan, drag and drop task flow elements from the **Toolbox** to the plan design surface. Double-click tasks to open dialog boxes to configure the task options.  
  
     The following maintenance plan tasks are available in the **Toolbox**:  
  
    -   **Back up Database Task**  
  
    -   **Check Database Integrity Task**  
  
    -   **Execute SQL Server Agent Job Task**  
  
    -   **Execute T-SQL Statement Task**  
  
    -   **History Cleanup Task**  
  
    -   **Maintenance Cleanup Task**  
  
    -   **Notify Operator Task**  
  
    -   **Rebuild Index Task**  
  
    -   **Reorganize Index Task**  
  
    -   **Shrink Database Task**  
  
    -   **Update Statistics Task**  
  
     To add tasks to the **Toolbox**:  
  
    1.  On the **Tools** menu, click **Choose Toolbox Items**.  
  
    2.  Select the tools you want to appear in the **Toolbox**, and then click **OK**.  
  
     Adding maintenance plan tasks to the **Toolbox** also makes them available in the **Maintenance Plan Wizard**. For more information on the individual tasks above, see [Using Maintenance Plan Wizard](../../relational-databases/maintenance-plans/use-the-maintenance-plan-wizard.md#SSMSProcedure) under **Start the Maintenance Plan Wizard**.  
  
8.  To define a workflow between tasks:  
  
    1.  Right-click the precedent task and select **Add Precedence Constraint**.  
  
    2.  In the **Control Flow** dialog box, in the **To** list, select the dependent task and click **OK**.  
  
    3.  Double click the connector between the two tasks to open the **Precedence Constraint Editor** dialog box.  
  
         The following options are available in the **Precedence Constraint Editor** dialog box.  
  
         **Constraint option**  
         Defines how a constraint works between two tasks.  
  
         **Evaluation operation**  list  
         Specify the evaluation operation that the precedence constraint uses. The operations are: **Constraint**, **Expression**, **Expression and Constraint**, and **Expression or Constraint**.  
  
         **Value** list  
         Specify the constraint value: **Success**, **Failure**, or **Completion**. **Success** is the default.  
  
        > [!NOTE]  
        >  The precedence constraint line is green for **Success**, red for **Failure**, and blue for **Completion**.  
  
         **Expression**  
         If using the operations **Expression**, **Expression and Constraint**, or **Expression or Constraint**, type an expression. The expression must evaluate to a Boolean.  
  
         **Test**  
         Validate the expression.  
  
         **Multiple constraints**  
         Define how multiple constraints interoperate to control the execution of the constrained task.  
  
         **Logical AND**  
         Select to specify that multiple precedence constraints on the same executable must be evaluated together. All constraints must evaluate to True. This option is the default.  
  
        > [!NOTE]  
        >  This type of precedence constraint appears as a solid green, red, or blue line.  
  
         **Logical OR**  
         Select to specify that multiple precedence constraints on the same executable must be evaluated together. At least one constraint must evaluate to True.  
  
        > [!NOTE]  
        >  This type of precedence constraint appears as a dotted green, red, or blue line.  
  
9. To add another subplan that contains tasks run on a different schedule, click **Add Subplan** on the toolbar to open the **Subplan Properties** dialog box.  
  
10. To add connections to different servers:  
  
    1.  In the design space's toolbar, click **Manage Connections**.  
  
    2.  In the **Manage Connections** dialog box, click **Add**.  
  
    3.  In the **Connection Properties** dialog box, in the **Connection name** box, enter the name of the connection you are creating.  
  
    4.  Under **Specify the following to connect to SQL Server data**, in the **Select or enter a server name** box, either enter the name of the SQL server you want to use or click the ellipsis **(...)** and select a server in the **SQL Server** dialog box. If you select a server from the **SQL Server** dialog box, click **OK**.  
  
    5.  Under **Enter information to log on to the server**, select either **Use Windows NT Integrated security** or **Use a specific user name and password**. If you elect to use a specific user name and password, enter that information in the **User name** and **Password** boxes, respectively.  
  
    6.  In the **Connection Properties** dialog box, click **OK**.  
  
    7.  In the **Manage Connections** dialog box, click **Close**.  
  
11. To specify reporting options:  
  
    1.  In the design space's toolbar, click **Reporting and Logging**.  
  
    2.  In the **Reporting and Logging** dialog box, under **Reporting**, select **Generate a text file report** or **Send report to an email recipient** or both.  
  
        1.  If you select **Generate a text file report**, select either **Create a new file** or **Append to file**.  
  
        2.  Depending on the selection above, enter the name and full path of the new file or file to be appended by entering the information in the **Folder** or **File name** boxes. Alternately, click on the ellipsis **(...)** and select the path to the folder or file name from the **Locate Folder -**_server\_name_ or **Locate Database Files -**_server\_name_ dialog boxes.  
  
        3.  If you select **Send report to an email recipient**, on the **Agent operator** list, select the recipient of the emailed report.  
  
            > [!NOTE]  
            >  SQL Server Agent must be configured to use Database Mail in order to send mail. For more information, see [Configure SQL Server Agent Mail to Use Database Mail](../../relational-databases/database-mail/configure-sql-server-agent-mail-to-use-database-mail.md)  
  
    3.  To save more detailed information, under **Logging**, select **Log extended information**.  
  
    4.  To write maintenance plan results information to another server, select **Log to remote server** and either select a server connection from the **Connection** list or click **New** and enter the connection information in the **Connection Properties** dialog box.  
  
    5.  In the **Reporting and Logging** dialog box, click **OK**.  
  
12. To view the results in the log file viewer, in **Object Explorer**, right-click either the **Maintenance Plans** folder or the specific maintenance plan and select **View History**.  

     The following options are available on the **Log File Viewer -**_server\_name_ dialog box.  
  
     **Load Log**  
     Open a dialog box where you can specify a log file to load.  
  
     **Export**  
     Open a dialog box that lets you export the information that is shown in the **Log file summary** grid to a text file.  
  
     **Refresh**  
     Refresh the view of the selected logs. The **Refresh** button rereads the selected logs from the target server while applying any filter settings.  
  
     **Filter**  
     Open a dialog box that lets you specify settings that are used to filter the log file, such as **Connection**, **Date**, or other **General** filter criteria.  
  
     **Search**  
     Search the log file for specific text. Searching with wildcard characters is not supported.  
  
     **Stop**  
     Stops loading the log file entries. For example, you can use this option if a remote or offline log file takes a long time to load, and you only want to view the most recent entries.  
  
     **Log file summary**  
     This information panel displays a summary of the log file filtering. If the file is not filtered, you will see the following text, **No filter applied**. If a filter is applied to the log, you will see the following text, **Filter log entries where:** \<filter criteria>.  
  
     **Date**  
     Displays the date of the event.  
  
     **Source**  
     Displays the source feature from which the event is created, such as the name of the service (MSSQLSERVER, for example). This does not appear for all log types.  
  
     **Message**  
     Displays any messages associated with the event.  
  
     **Log Type**  
     Displays the type of log to which the event belongs. All selected logs appear in the log file summary window.  
  
     **Log Source**  
     Displays a description of the source log in which the event is captured.  
  
     **Selected row details**  
     Select a row to display additional details about the selected event row at the bottom of the page. The columns can be reordered by dragging them to new locations in the grid. The columns can be resized by dragging the column separator bars in the grid header to the left or right. Double-click the column separator bars in the grid header to automatically size the column to the content width.  
  
     **Instance**  
     The name of the instance on which the event occurred. This is displayed as *computer name*\\*instance name*.  
  
  
