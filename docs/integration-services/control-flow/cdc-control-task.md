---
title: "CDC Control Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.ssis.designer.cdccontroltask.f1"
  - "sql13.ssis.designer.cdccontroltask.config.f1"
ms.assetid: 6404dc7f-550c-47cc-b901-c072742f430a
author: janinezhang
ms.author: janinez
manager: craigg
---
# CDC Control Task
  The CDC Control task is used to control the life cycle of change data capture (CDC) packages. It handles CDC package synchronization with the initial load package, the management of Log Sequence Number (LSN) ranges that are processed in a run of a CDC package. In addition, the CDC Control task deals with error scenarios and recovery.  
  
 The CDC Control task maintains the state of the CDC package in an SSIS package variable and it can also persist it in a database table so that the state is maintained across package activations and between multiple packages that together perform a common CDC process (for example, one task may be responsible for the initial loading and the other for the trickle-feed updates).  
  
 The CDC Control task supports two groups of operations. One group handles the synchronization of initial load and change processing, and the other manages the change-processing range of LSNs for a run of a CDC package and keeps track of what was processed successfully.  
  
 The following operations handle the synchronization of initial load and change processing:  
  
|Operation|Description|  
|---------------|-----------------|  
|ResetCdcState|This operation is used to reset the persistent CDC state associated with the current CDC context. After this operation is run, the current maximum LSN from the LSN-timestamp `sys.fn_cdc_get_max_lsn` table becomes the start of the range for the next processing range. This operation requires a connection to the source database.|  
|MarkInitialLoadStart|This operation is used at the beginning of an initial-load package to record the current LSN in the source database before the initial-load package starts reading the source tables. This requires a connection to the source database to call `sys.fn_cdc_get_max_lsn`.<br /><br /> If you select MarkInitialLoadStart when working on [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] CDC (that is, not Oracle) the user specified in the connection manager must be either  db_owner or sysadmin.|  
|MarkInitialLoadEnd|This operation is used at the end of an initial-load package to record the current LSN in the source database after the initial-load package finished reading the source tables. This LSN is determined by recording the current time when this operation occurred and then querying the `cdc.lsn_time_`mapping table in the CDC database looking for a change that occurred after that time.<br /><br /> If you select MarkInitialLoadEnd when working on [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] CDC (that is , not Oracle) the user specified in the connection manager must be either  db_owner or sysadmin.|  
|MarkCdcStart|This operation is used when then the initial load is made from a snapshot database. In this case, the change processing should start immediately after the snapshot LSN. You can specify the name of the snapshot database to use and the CDC Control task queries [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] for the snapshot LSN. You also have the option to directly specify the snapshot LSN.<br /><br /> If you select MarkCdcStart when working on [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] CDC (that is , not Oracle) the user specified in the connection manager must be either  db_owner or sysadmin.|  
  
 The following operations are used to manage the processing range:  
  
|Operation|Description|  
|---------------|-----------------|  
|GetProcessingRange|This operation is used before invoking the data flow that uses the CDC Source data flow. It establishes a range of LSNs that the CDC Source data flow reads when invoked. The range is stored in an SSIS package variable that is used by the CDC Source during data-flow processing.<br /><br /> For more information about the states that are stored, see [Define a State Variable](../../integration-services/data-flow/define-a-state-variable.md).|  
|MarkProcessedRange|: This operation is executed after each CDC run (after the CDC data flow is completed successfully) to record the last LSN that was fully processed in the CDC run. The next time GetProcessingRange is executed, this position is the start of the processing range.|  
  
## Handling CDC State Persistency  
 The CDC Control task maintains a persistent state between activations. The information stored in the CDC state is used to determine and maintain the processing range for the CDC package and for detecting error conditions. The persistent state is stored as a string. For more information, see [Define a State Variable](../../integration-services/data-flow/define-a-state-variable.md).  
  
 The CDC Control task supports two types of state persistency  
  
-   Manual State Persistency: In this case, the CDC Control task manages the state stored in a package variable but the package developer must read the variable from a persistent store before calling the CDC Control and then write it back to that persistent store after the CDC Control is last called and the CDC run completes.  
  
-   Automatic State Persistency: The CDC state is stored in a table in a database. The state is stored under a name provided in the **StateName** property in a table named in the **Table to Use for Storing State** property, which is located in a selected connection manager for storing the state. The default is the source connection manager but the common practice is for it to be the target connection manager. The CDC Control task updates the state value in the state table and this is committed as part of the ambient transaction.  
  
## Error Handling  
 The CDC Control task may report an error when:  
  
-   It fails to read the persistent CDC state or when updating the persistent state fails.  
  
-   It fails to read the current LSN information from the source database.  
  
-   The CDC state read is not consistent.  
  
 In all of these cases, the CDC Control task reports an error that can be handled in the standard way SSIS handles control-flow errors.  
  
 The CDC Control task may also report a warning when the Get Processing Range operation is invoked directly after another Get Processing Range operation without Mark Processed Range being called. This is an indication that the previous run failed or that another CDC package may be running using the same CDC state name.  
  
## Configuring the CDC Control Task  
 You can set properties through SSIS Designer or programmatically.  
  
## In This Section  
  
-   [CDC Control Task Custom Properties](../../integration-services/control-flow/cdc-control-task-custom-properties.md)  
  
## Related Tasks  
 [Define a State Variable](../../integration-services/data-flow/define-a-state-variable.md)  
  
## Related Content  
  
-   Technical article, [Installing Microsoft SQL Server 2012 Change Data Capture for Oracle by Attunity](https://go.microsoft.com/fwlink/?LinkId=252958), on social.technet.microsoft.com.  
  
-   Technical article, [Troubleshoot Configuration Issues in Microsoft Change Data Capture for Oracle by Attunity](https://go.microsoft.com/fwlink/?LinkId=252960), on social.technet.microsoft.com.  
  
-   Technical article, [Troubleshoot CDC Instance Errors in Microsoft Change Data Capture for Oracle by Attunity](https://go.microsoft.com/fwlink/?LinkId=252961), on social.technet.microsoft.com.  
  
## CDC Control Task Editor
  Use the **CDC Control Task Editor** dialog box to configure the CDC Control task. The CDC Control task configuration includes defining a connection to the CDC database, the CDC task operation and the state management information.  
  
 To learn more about the CDC Control task, see [CDC Control Task](../../integration-services/control-flow/cdc-control-task.md).  
  
 **To open the CDC Control Task Editor**  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] package that has the CDC Control task.  
  
2.  On the **Control Flow** tab, double-click the CDC Control task.  
  
### Options  
 **SQL Server CDC database ADO.NET connection manager**  
 Select an existing connection manager from the list, or click **New** to create a new connection. The connection must be to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database that is enabled for CDC and where the selected change table is located.  
  
 **CDC Control Operation**  
 Select the operation to run for this task. All operations use the state variable that is stored in an SSIS package variable that stores the state and passes it between the different components in the package.  
  
-   **Mark initial load start**: This operation is used when executing an initial load from an active database without a snapshot. It is invoked at the beginning of an initial-load package to record the current LSN in the source database before the initial-load package starts reading the source tables. This requires a connection to the source database.  
  
     If you select **Mark Initial Load Start** when working on [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] CDC (that is, not Oracle) the user specified in the connection manager must be either  **db_owner** or **sysadmin**.  
  
-   **Mark initial load end**: This operation is used when executing an initial load from an active database without a snapshot. It is invoked at the end of an initial-load package to record the current LSN in the source database after the initial-load package finished reading the source tables. This LSN is determined by recording nthe current time when this operation occurred and then querying the `cdc.lsn_time_`mapping table in the CDC database looking for a change that occurred after that time  
  
     If you select **Mark Initial Load End** when working on [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] CDC (that is, not Oracle) the user specified in the connection manager must be either  **db_owner** or **sysadmin**.  
  
-   **Mark CDC start**: This operation is used when then the initial load is made from a snapshot database or from a quiescence database. It is invoked at any point within the initial load package. The operation accepts a parameter that can be a snapshot LSN, a name of a snapshot database (from which the snapshot LSN will be derived automatically) or it can be left empty, in which case the current database LSN is used as the start LSN for the change processing package.  
  
     This operation is used instead of the Mark Initial Load Start/End operations.  
  
     If you select **Mark CDC Start** when working on [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] CDC (that is, not Oracle) the user specified in the connection manager must be either  **db_owner** or **sysadmin**.  
  
-   **Get processing range**: This operation is used in a change processing package before invoking the data flow that uses the CDC Source data flow. It establishes a range of LSNs that the CDC Source data flow reads when invoked. The range is stored in an SSIS package variable that is used by the CDC Source during data-flow processing.  
  
     For more information about the possible CDC states that are stored, see [Define a State Variable](../../integration-services/data-flow/define-a-state-variable.md).  
  
-   **Mark processed range**: This operation is used in a change processing package at the end of a  CDC run (after the CDC data flow is completed successfully) to record the last LSN that was fully processed in the CDC run. The next time `GetProcessingRange` is executed, this position determines the start of the next processing range.  
  
-   **Reset CDC state**: This operation is used to reset the persistent CDC state associated with the current CDC context. After this operation is run, the current maximum LSN from the LSN-timestamp `sys.fn_cdc_get_max_lsn` table becomes the start of the range for the next processing range. This operation requires a connection to the source database.  
  
     An example of when this operation is used is when you want to process only the newly created change records and ignore all old change records.  
  
 **Variable containing the CDC state**  
 Select the SSIS package variable that stores the state information for the task operation. You should define a variable before you begin. If you select **Automatic state persistence**, the state variable is loaded and saved automatically.  
  
 For more information about defining the state variable, see [Define a State Variable](../../integration-services/data-flow/define-a-state-variable.md).  
  
 **SQL Server LSN to start the CDC/Snapshot name:**  
 Type the current source database LSN or the name of the snapshot database from which the initial load is performed to determine where the CDC starts. This is available only if the **CDC Control Operation** is set to **Mark CDC Start**.  
  
 For more information about these operations, see [CDC Control Task](../../integration-services/control-flow/cdc-control-task.md)  
  
 **Automatically store state in a database table**  
 Select this check box for the CDC Control task to automatically handle loading and storing the CDC state in a state table contained in the specified database. When not selected, the developer must load the CDC State when the package starts and save it whenever the CDC State changes.  
  
 **Connection manager for the database where the state is stored**  
 Select an existing ADO.NET connection manager from the list, or click New to create a new connection. This connection is to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database that contains the State table. The State table contains the State information.  
  
 This is available only if **Automatic state persistence** is selected and it is a required parameter.  
  
 **Table to use for storing state**  
 Type the name of the state table to be used for storing the CDC state. The table specified must have two columns called **name** and **state** and both columns must be of the data type **varchar (256)**.  
  
 You can optionally select **New** to get an SQL script that builds a new State table with the required columns. When **Automatic state persistence** is selected, the developer must create a state table according to the requirements listed above.  
  
 This is available only if **Automatic state persistence** is selected and it is a required parameter.  
  
 **State name**  
 Type a name to associate with the persistent CDC state. The full load and CDC packages that work with the same CDC context will specify a common state name. This name is used for looking up the state row in the state table  
  
