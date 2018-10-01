---
title: "Troubleshooting Tools for Package Development | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Integration Services packages, troubleshooting"
  - "SSIS packages, troubleshooting"
  - "Integration Services, troubleshooting"
  - "errors [Integration Services], troubleshooting"
  - "packages [Integration Services], troubleshooting"
ms.assetid: 41dd248c-dab3-4318-b8ba-789a42d5c00c
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Troubleshooting Tools for Package Development
  [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes features and tools that you can use to troubleshoot packages while you are developing them in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
## Troubleshooting Design-time Validation Issues  
 In the current release of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], when a package is opened, the system validates all connections before validating all of the data flow components and sets any connections that are slow or unavailable to work offline. This helps reduce the delay in validating the package data flow.  
  
 After a package is opened, you can also turn off a connection by right-clicking the connection manager in the **Connection Managers** area and then clicking **Work Offline**. This can speed up operations in the SSIS Designer.  
  
 Connections that have been set to work offline, will remain offline until you do one of the following:  
  
-   Test the connection by right-clicking the connection manager in the **Connection Managers** area of SSIS Designer and then clicking **Test Connectivity**.  
  
     For example, a connection is initially set to work offline when the package is opened. You modify the connection string to resolve the issue and click **Test Connectivity** to test the connection.  
  
-   Re-open the package or re-open the project that contains the package. Validation is run again on all of the connections in the package.  
  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes the following, additional features to help you avoid validation errors :  
  
-   **Set all of the package and all of the connections to work offline when data sources are not available**. You can enable **Work Offline** from the **SSIS** menu. Unlike the **DelayValidation** property, the **Work Offline** option is available even before you open a package. You can also enable **Work Offline** to speed up operations in the designer, and disable it only when you want your package to be validated.  
  
-   **Configure the DelayValidation property on package elements that are not valid until run time**. You can set **DelayValidation** to **True** on package elements whose configuration is not valid at design time to prevent validation errors. For example, you may have a Data Flow task that uses a destination table that does not exist until an Execute SQL task creates the table at run time. The **DelayValidation** property can be enabled at the package level, or at the level of the individual tasks and containers that the package includes. Normally you must leave this property set to **True** on the same package elements when you deploy the package, to prevent the same validation errors at run time.  
  
     The **DelayValidation** property can be set on a Data Flow task, but not on individual data flow components. You can achieve a similar effect by setting the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100.ValidateExternalMetadata%2A> property of individual data flow components to **false**. However, when the value of this property is **false**, the component is not aware of changes to the metadata of external data sources.  
  
 If database objects that are used by the package are locked when validation occurs, the validation process might stop responding. In these circumstances, the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer also stops responding. You can resume validation by using [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to close the associated session in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can also avoid this issue by using the settings described in this section.  
  
## Troubleshooting Control Flow  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes the following features and tools that you can use to troubleshoot the control flow in packages during package development:  
  
-   **Set breakpoints on tasks, containers, and the package**. You can set breakpoints by using the graphical tools that [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer provides. Breakpoints can be enabled at the package level, or at the level of the individual tasks and containers that the package includes. Some tasks and containers provide additional break conditions for setting breakpoints. For example, you can enable a break condition on the For Loop container that suspends execution at the start of each iteration of the loop.  
  
-   **Use the debugging windows**. When you run a package that has breakpoints, the debug windows in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] provide access to variable values and status messages.  
  
-   **Review the information on the Progress tab**. [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer provides additional information about control flow when you run a package in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. The Progress tab lists tasks and containers in order of execution and includes start and finish times, warnings, and error messages for each task and container, including the package itself.  
  
 For more information on these features, see [Debugging Control Flow](../../integration-services/troubleshooting/debugging-control-flow.md).  
  
## Troubleshooting Data Flow  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes the following features and tools that you can use to troubleshoot the data flows in packages during package development:  
  
-   **Test with only a subset of your data**. If you want to troubleshoot the data flow in a package by using only a sample of the dataset, you can include a Percentage Sampling or Row Sampling transformation to create an in-line data sample at run time. For more information, see [Percentage Sampling Transformation](../../integration-services/data-flow/transformations/percentage-sampling-transformation.md) and [Row Sampling Transformation](../../integration-services/data-flow/transformations/row-sampling-transformation.md).  
  
-   **Use data viewers to monitor data as it moves through the data flow**. Data viewers display data values as the data moves between sources, transformations, and destinations. A data viewer can display data in a grid. You can copy the data from a data viewer to the Clipboard, and then paste the data into a file or Excel spreadsheet. For more information, see [Debugging Data Flow](../../integration-services/troubleshooting/debugging-data-flow.md) .  
  
-   **Configure error outputs on data flow components that support them**. Many data flow sources, transformations, and destinations also support error outputs. By configuring the error output of a data flow component, you can direct data that contains errors to a different destination. For example, you can capture the data that failed or was truncated in a separate text file. You can also attach data viewers to error outputs and examine only the erroneous data. At design time, error outputs capture troublesome data values to help you develop packages that deal effectively with real-world data. However, while other troubleshooting tools and features are useful only at design time, error outputs retain their usefulness in the production environment. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).  
  
-   **Capture the count of rows processed**. When you run a package in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, the number of rows that have passed through a path is displayed in the data flow designer. This number is updated periodically while the data moves through the path. You can also add a Row Count transformation to the data flow to capture the final row count in a variable. For more information, see [Row Count Transformation](../../integration-services/data-flow/transformations/row-count-transformation.md).  
  
-   **Review the information on the Progress tab**. [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer provides additional information about data flows when you run a package in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. The Progress tab lists data flow components in order of execution and includes information about progress for each phase of the package, displayed as percentage complete, and the number of rows written to the destination.  
  
 For more information on these features, see [Debugging Data Flow](../../integration-services/troubleshooting/debugging-data-flow.md).  
  
## Troubleshooting Scripts  
 [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] Tools for Applications (VSTA) is the development environment in which you write the scripts that are used by the Script task and Script component. VSTA provides the following features and tools that you can use to troubleshoot scripts during package development:  
  
-   **Set breakpoints in script in Script tasks.** VSTA provides debugging support for scripts in the Script task only. The breakpoints that you set in Script tasks are integrated with the breakpoints that you set on packages and the tasks and containers in the package, enabling seamless debugging of all package elements.  
  
    > [!NOTE]  
    >  When you debug a package that contains multiple Script tasks, the debugger hits breakpoints in only one Script task and will ignore breakpoints in the other Script tasks. If a Script task is part of a Foreach Loop or For Loop container, the debugger ignores breakpoints in the Script task after the first iteration of the loop.  
  
 For more information, see [Debugging Script](../../integration-services/troubleshooting/debugging-script.md). For suggestions about how to debug the Script component, see [Coding and Debugging the Script Component](../../integration-services/extending-packages-scripting/data-flow-script-component/coding-and-debugging-the-script-component.md).  
  
## Troubleshooting Errors without a Description  
 If you encounter an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] error number without an accompanying description during package development, you can locate the description in [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md). The list does not include troubleshooting information at this time.  
  
## See Also  
 [Troubleshooting Tools for Package Execution](../../integration-services/troubleshooting/troubleshooting-tools-for-package-execution.md)   
 [Data Flow Performance Features](../../integration-services/data-flow/data-flow-performance-features.md)  
  
  
