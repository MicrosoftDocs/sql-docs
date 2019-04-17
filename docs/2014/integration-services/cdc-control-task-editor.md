---
title: "CDC Control Task Editor | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.ssis.designer.cdccontroltask.config.f1"
ms.assetid: 4f09d040-9ec8-4aaa-b684-f632d571f0a8
author: janinezhang
ms.author: janinez
manager: craigg
---
# CDC Control Task Editor
  Use the **CDC Control Task Editor** dialog box to configure the CDC Control task. The CDC Control task configuration includes defining a connection to the CDC database, the CDC task operation and the state management information.  
  
 To learn more about the CDC Control task, see [CDC Control Task](control-flow/cdc-control-task.md).  
  
 **To open the CDC Control Task Editor**  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../includes/ssiscurrent-md.md)] package that has the CDC Control task.  
  
2.  On the **Control Flow** tab, double-click the CDC Control task.  
  
## Options  
 **SQL Server CDC database ADO.NET connection manager**  
 Select an existing connection manager from the list, or click **New** to create a new connection. The connection must be to a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database that is enabled for CDC and where the selected change table is located.  
  
 **CDC Control Operation**  
 Select the operation to run for this task. All operations use the state variable that is stored in an SSIS package variable that stores the state and passes it between the different components in the package.  
  
-   **Mark initial load start**: This operation is used when executing an initial load from an active database without a snapshot. It is invoked at the beginning of an initial-load package to record the current LSN in the source database before the initial-load package starts reading the source tables. This requires a connection to the source database.  
  
     If you select **Mark Initial Load Start** when working on [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] CDC (that is, not Oracle) the user specified in the connection manager must be either  **db_owner** or **sysadmin**.  
  
-   **Mark initial load end**: This operation is used when executing an initial load from an active database without a snapshot. It is invoked at the end of an initial-load package to record the current LSN in the source database after the initial-load package finished reading the source tables. This LSN is determined by recording nthe current time when this operation occurred and then querying the `cdc.lsn_time_`mapping table in the CDC database looking for a change that occurred after that time  
  
     If you select **Mark Initial Load End** when working on [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] CDC (that is, not Oracle) the user specified in the connection manager must be either  **db_owner** or **sysadmin**.  
  
-   **Mark CDC start**: This operation is used when then the initial load is made from a snapshot database or from a quiescence database. It is invoked at any point within the initial load package. The operation accepts a parameter that can be a snapshot LSN, a name of a snapshot database (from which the snapshot LSN will be derived automatically) or it can be left empty, in which case the current database LSN is used as the start LSN for the change processing package.  
  
     This operation is used instead of the Mark Initial Load Start/End operations.  
  
     If you select **Mark CDC Start** when working on [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] CDC (that is, not Oracle) the user specified in the connection manager must be either  **db_owner** or **sysadmin**.  
  
-   **Get processing range**: This operation is used in a change processing package before invoking the data flow that uses the CDC Source data flow. It establishes a range of LSNs that the CDC Source data flow reads when invoked. The range is stored in an SSIS package variable that is used by the CDC Source during data-flow processing.  
  
     For more information about the possible CDC states that are stored, see [Define a State Variable](data-flow/define-a-state-variable.md).  
  
-   **Mark processed range**: This operation is used in a change processing package at the end of a  CDC run (after the CDC data flow is completed successfully) to record the last LSN that was fully processed in the CDC run. The next time `GetProcessingRange` is executed, this position determines the start of the next processing range.  
  
-   **Reset CDC state**: This operation is used to reset the persistent CDC state associated with the current CDC context. After this operation is run, the current maximum LSN from the LSN-timestamp `sys.fn_cdc_get_max_lsn` table becomes the start of the range for the next processing range. This operation requires a connection to the source database.  
  
     An example of when this operation is used is when you want to process only the newly created change records and ignore all old change records.  
  
 **Variable containing the CDC state**  
 Select the SSIS package variable that stores the state information for the task operation. You should define a variable before you begin. If you select **Automatic state persistence**, the state variable is loaded and saved automatically.  
  
 For more information about defining the state variable, see [Define a State Variable](data-flow/define-a-state-variable.md).  
  
 **SQL Server LSN to start the CDC/Snapshot name:**  
 Type the current source database LSN or the name of the snapshot database from which the initial load is performed to determine where the CDC starts. This is available only if the **CDC Control Operation** is set to **Mark CDC Start**.  
  
 For more information about these operations, see [CDC Control Task](control-flow/cdc-control-task.md)  
  
 **Automatically store state in a database table**  
 Select this check box for the CDC Control task to automatically handle loading and storing the CDC state in a state table contained in the specified database. When not selected, the developer must load the CDC State when the package starts and save it whenever the CDC State changes.  
  
 **Connection manager for the database where the state is stored**  
 Select an existing ADO.NET connection manager from the list, or click New to create a new connection. This connection is to a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database that contains the State table. The State table contains the State information.  
  
 This is available only if **Automatic state persistence** is selected and it is a required parameter.  
  
 **Table to use for storing state**  
 Type the name of the state table to be used for storing the CDC state. The table specified must have two columns called **name** and **state** and both columns must be of the data type **varchar (256)**.  
  
 You can optionally select **New** to get an SQL script that builds a new State table with the required columns. When **Automatic state persistence** is selected, the developer must create a state table according to the requirements listed above.  
  
 This is available only if **Automatic state persistence** is selected and it is a required parameter.  
  
 **State name**  
 Type a name to associate with the persistent CDC state. The full load and CDC packages that work with the same CDC context will specify a common state name. This name is used for looking up the state row in the state table  
  
## See Also  
 [CDC Control Task Custom Properties](control-flow/cdc-control-task-custom-properties.md)  
  
  
