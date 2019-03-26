---
title: "Execute Package Task | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.executepackagetask.f1"
helpviewer_keywords: 
  - "Execution Package task [Integration Services]"
  - "child packages"
  - "parent packages [Integration Services]"
ms.assetid: 042d4ec0-0668-401c-bb3a-a25fe2602eac
author: janinezhang
ms.author: janinez
manager: craigg
---
# Execute Package Task
  The Execute Package task extends the enterprise capabilities of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] by letting packages run other packages as part of a workflow.  
  
 You can use the Execute Package task for the following purposes:  
  
-   Breaking down complex package workflow. This task lets you break down workflow into multiple packages, which are easier to read, test, and maintain. For example, if you are loading data into a star schema, you can build a separate package to populate each dimension and the fact table.  
  
-   Reusing parts of packages. Other packages can reuse parts of a package workflow. For example, you can build a data extraction module that can be called from different packages. Each package that calls the extraction module can perform different data scrubbing, filtering, or aggregation operations.  
  
-   Grouping work units. Units of work can be encapsulated into separate packages and joined as transactional components to the workflow of a parent package. For example, the parent package runs the accessory packages, and based on the success or failure of the accessory packages, the parent package either commits or rolls back the transaction.  
  
-   Controlling package security. Package authors require access to only a part of a multipackage solution. By separating a package into multiple packages, you can provide a greater level of security, because you can grant an author access to only the relevant packages.  
  
 A package that runs other packages is generally referred to as the parent package, and the packages that a parent workflow runs are called child packages.  
  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes tasks that perform workflow operations, such as executing executables and batch files. For more information, see [Execute Process Task](execute-process-task.md).  
  
## Running Packages  
 The Execute Package task can run child packages that are contained in the same project that contains the parent package. You select a child package from the project by setting the **ReferenceType** property to **Project Reference**, and then setting the **PackageNameFromProjectReference** property.  
  
> [!NOTE]  
>  The **ReferenceType** option is ready-only and set to **External Reference** if the project that contains the package has not been converted to the project deployment model. For more information about conversion, see [Deploy Projects to Integration Services Server](../deploy-projects-to-integration-services-server.md).  
  
 The Execute Package task can also run packages stored in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] msdb database and packages stored in the file system. The task uses an OLE DB connection manager to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or a File connection manager to access the file system. For more information, see [OLE DB Connection Manager](../connection-manager/ole-db-connection-manager.md) and [Flat File Connection Manager](../connection-manager/flat-file-connection-manager.md).  
  
 The Execute Package task can also run a database maintenance plan, which lets you manage both [!INCLUDE[ssIS](../../includes/ssis-md.md)] packages and database maintenance plans in the same [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] solution. A database maintenance plan is similar to an [!INCLUDE[ssIS](../../includes/ssis-md.md)] package, but a plan can include only database maintenance tasks, and it is always stored in the msdb database.  
  
 If you choose a package stored in the file system, you must provide the name and location of the package. The package can reside anywhere in the file system; it does not have to be in the same folder as the parent package.  
  
 The child package can be run in the process of the parent package, or it can be run in its own process. Running the child package in its own process requires more memory, but it provides more flexibility. For example, if the child process fails, the parent process can continue to run.  
  
 Alternatively, sometimes you want the parent and child packages to fail together as one unit, or you might not want to incur the additional overhead of another process. For example, if a child process fails and subsequent processing in the parent process of the package depends on success of the child process, the child package should run in the process of the parent package.  
  
 By default, the ExecuteOutOfProcess property of the Execute Package task is set to `False`, and the child package runs in the same process as the parent package. If you set this property to `True`, the child package runs in a separate process. This may slow down the launching of the child package. In addition, if you set the property to `True`, you cannot debug the package in a tools-only install. You must install [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. For more information, see [Install Integration Services](../install-windows/install-integration-services.md)  
  
## Extending Transactions  
 The transaction that the parent package uses can extend to the child package; therefore, the work both packages perform can be committed or rolled back. For example, the database inserts that the parent package performs can be committed or rolled back, depending on the database inserts that the child package performs, and vice versa. For more information, see [Inherited Transactions](../inherited-transactions.md).  
  
## Propagating Logging Details  
 The child package that the Execute Package task runs may or may not be configured to use logging, but the child package will always forward the log details to the parent package. If the Execute Package task is configured to use logging, the task logs the log details from the child package. For more information, see [Integration Services &#40;SSIS&#41; Logging](../performance/integration-services-ssis-logging.md).  
  
## Passing Values to Child Packages  
 Frequently a child package uses values passed to it by another package that calls it, ordinarily its parent package. Using values from a parent package is useful in scenarios such as the following:  
  
-   Parts of a larger workflow are assigned to different packages. For example, one package downloads data on a nightly basis, summarizes the data, assigns summary data values to variables, and then passes the values to another package for additional processing of the data.  
  
-   The parent package dynamically coordinates tasks in a child package. For example, the parent package determines the number of days in a current month and assigns the number to a variable, and the child package performs a task that number of times.  
  
-   A child package requires access to data that is dynamically derived by the parent package. For example, the parent package extracts data from a table and loads the rowset into a variable, and the child package performs additional operations on the data.  
  
 You can use the following methods to pass values to a child package:  
  
-   **Package Configurations**  
  
     [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides a configuration type, the Parent Package Variable configuration, for passing values from parent to child packages. The configuration is built on the child package and uses a variable in the parent package. The configuration is mapped to a variable in the child package, or to the property of an object in the child package. The variable can also be used in the scripts used by the Script task or Script component.  
  
-   **Parameters**  
  
     You can configure the Execute Package Task to map parent package variables or parameters, or project parameters, to child package parameters. The project must use the project deployment model and the child package must be contained in the same project that contains the parent package. For more information, see [Execute Package Task Editor](../execute-package-task-editor.md).  
  
    > [!NOTE]  
    >  If the child package parameter is not sensitive and is mapped to a parent parameter that is sensitive, the child package will fail to run.  
    >   
    >  The following mapping conditions are supported:  
    >   
    >  -   Sensitive, child package parameter is mapped to a sensitive, parent parameter  
    > -   Sensitive, child package parameter is mapped to a non-sensitive, parent parameter  
    > -   Non-sensitive, child package parameter is mapped to a non-sensitive, parent parameter  
  
 The parent package variable can be defined in the scope of the Execute Package task or in a parent container such as the package. If multiple variables with the same name are available, the variable defined in the scope of the Execute Package task is used, or the variable that is closest in scope to the task.  
  
 For more information, see [Use the Values of Variables and Parameters in a Child Package](../use-the-values-of-variables-and-parameters-in-a-child-package.md).  
  
### Accessing Parent Package Variables  
 Child packages can access parent package variables by using the Script task. When you enter the name of the parent package variable on the **Script** page in the **Script Task Editor**, don't include **User:** in the variable name. Otherwise, the child package doesn't locate the variable when you run the parent package. For more information about using the Script task to access parent package variables, see this blog entry, [SSIS: Accessing variables in a parent package](https://go.microsoft.com/fwlink/?LinkId=257729), on consultingblogs.emc.com.  
  
## Configuring the Execute Package Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click one of the following topics:  
  
-   [Execute Package Task Editor](../execute-package-task-editor.md)  
  
-   [Expressions Page](../expressions/expressions-page.md)  
  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](../set-the-properties-of-a-task-or-container.md)  
  
## Related Tasks  
  
## Related Content  
  
-   Blog entry, [SSIS: Should you execute child packages in-process or out-of-process?](https://go.microsoft.com/fwlink/?LinkId=220819), on consultingblogs.emc.com.  
  
-   Blog entry, [SSIS: Accessing variables in a parent package](https://go.microsoft.com/fwlink/?LinkId=257729), on consultingblogs.emc.com.  
  
  
