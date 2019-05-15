---
title: "Data Flow Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.dataflowtask.f1"
helpviewer_keywords: 
  - "data flow task [Integration Services]"
  - "performance [Integration Services]"
  - "data flow [Integration Services], performance"
  - "data flow [Integration Services], Data Flow task"
  - "Integration Services, performance"
ms.assetid: c27555c4-208c-43c8-b511-a4de2a8a3344
author: janinezhang
ms.author: janinez
manager: craigg
---
# Data Flow Task

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The Data Flow task encapsulates the data flow engine that moves data between sources and destinations, and lets the user transform, clean, and modify data as it is moved. Addition of a Data Flow task to a package control flow makes it possible for the package to extract, transform, and load data.  
  
 A data flow consists of at least one data flow component, but it is typically a set of connected data flow components: sources that extract data; transformations that modify, route, or summarize data; and destinations that load data.  
  
 At run time, the Data Flow task builds an execution plan from the data flow, and the data flow engine executes the plan. You can create a Data Flow task that has no data flow, but the task executes only if it includes at least one data flow.  
  
 To bulk insert data from text files into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, you can use the Bulk Insert task instead of a Data Flow task and a data flow. However, the Bulk Insert task cannot transform data. For more information, see [Bulk Insert Task](../../integration-services/control-flow/bulk-insert-task.md).  
  
## Multiple Flows  
 A Data Flow task can include multiple data flows. If a task copies several sets of data, and if the order in which the data is copied is not significant, it can be more convenient to include multiple data flows in the Data Flow task. For example, you might create five data flows, each copying data from a flat file into a different dimension table in a data warehouse star schema.  
  
 However, the data flow engine determines order of execution when there are multiple data flows within one data flow task. Therefore, when order is important, the package should use multiple Data Flow tasks, each task containing one data flow. You can then apply precedence constraints to control the execution order of the tasks.  
  
 The following diagram shows a Data Flow task that has multiple data flows.  
  
 ![Data flows](../../integration-services/control-flow/media/mw-dts-09.gif "Data flows")  
  
## Log Entries  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides a set of log events that are available to all tasks. [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] also provides custom log entries to many tasks. For more information, see [Integration Services &#40;SSIS&#41; Logging](../../integration-services/performance/integration-services-ssis-logging.md). The Data Flow task includes the following custom log entries:  
  
|Log entry|Description|  
|---------------|-----------------|  
|**BufferSizeTuning**|Indicates that the Data Flow task changed the size of the buffer. The log entry describes the reasons for the size change and lists the temporary new buffer size.|  
|**OnPipelinePostEndOfRowset**|Denotes that a component has been given its end-of-rowset signal, which is set by the last call of the **ProcessInput** method. An entry is written for each component in the data flow that processes input. The entry includes the name of the component.|  
|**OnPipelinePostPrimeOutput**|Indicates that the component has completed its last call to the **PrimeOutput** method. Depending on the data flow, multiple log entries may be written. If the component is a source, this log entry means that the component has finished processing rows.|  
|**OnPipelinePreEndOfRowset**|Indicates that a component is about to receive its end-of-rowset signal, which is set by the last call of the **ProcessInput** method. An entry is written for each component in the data flow that processes input. The entry includes the name of the component.|  
|**OnPipelinePrePrimeOutput**|Indicates that the component is about to receive its call from the **PrimeOutput** method. Depending on the data flow, multiple log entries may be written.|  
|**OnPipelineRowsSent**|Reports the number of rows provided to a component input by a call to the **ProcessInput** method. The log entry includes the component name.|  
|**PipelineBufferLeak**|Provides information about any component that kept buffers alive after the buffer manager goes away. If a buffer is still alive, buffers resources were not released and may cause memory leaks. The log entry provides the name of the component and the ID of the buffer.|  
|**PipelineComponentTime**|Reports the amount of time (in milliseconds) that the component spent in each of its five major processing steps-Validate, PreExecute, PostExecute, ProcessInput, and ProcessOutput.|  
|**PipelineExecutionPlan**|Reports the execution plan of the data flow. The execution plan provides information about how buffers will be sent to components. This information, in combination with the PipelineExecutionTrees log entry, describes what is happening within the Data Flow task.|  
|**PipelineExecutionTrees**|Reports the execution trees of the layout in the data flow. The scheduler of the data flow engine uses the trees to build the execution plan of the data flow.|  
|**PipelineInitialization**|Provides initialization information about the task. This information includes the directories to use for temporary storage of BLOB data, the default buffer size, and the number of rows in a buffer. Depending on the configuration of the Data Flow task, multiple log entries may be written.|  
  
 These log entries provide a wealth of information about the execution of the Data Flow task each time you run a package. As you run the packages repeatedly, you can capture information that over time provides important historical information about the processing that the task performs, issues that might affect performance, and the data volume that task handles.  
  
 For more information about how to use these log entries to monitor and improve the performance of the data flow, see one of the following topics:  
  
-   [Performance Counters](../../integration-services/performance/performance-counters.md)  
  
-   [Data Flow Performance Features](../../integration-services/data-flow/data-flow-performance-features.md)  
  
### Sample Messages From a Data Flow Task  
 The following table lists sample messages for log entries for a very simple package. The package uses an OLE DB source to extract data from a table, a Sort transformation to sort the data, and an OLE DB destination to writes the data to a different table.  
  
|Log entry|Messages|  
|---------------|--------------|  
|**BufferSizeTuning**|`Rows in buffer type 0 would cause a buffer size greater than the configured maximum. There will be only 9637 rows in buffers of this type.`<br /><br /> `Rows in buffer type 2 would cause a buffer size greater than the configured maximum. There will be only 9497 rows in buffers of this type.`<br /><br /> `Rows in buffer type 3 would cause a buffer size greater than the configured maximum. There will be only 9497 rows in buffers of this type.`|  
|**OnPipelinePostEndOfRowset**|`A component will be given the end of rowset signal. : 1180 : Sort : 1181 : Sort Input`<br /><br /> `A component will be given the end of rowset signal. : 1291 : OLE DB Destination : 1304 : OLE DB Destination Input`|  
|**OnPipelinePostPrimeOutput**|`A component has returned from its PrimeOutput call. : 1180 : Sort`<br /><br /> `A component has returned from its PrimeOutput call. : 1 : OLE DB Source`|  
|**OnPipelinePreEndOfRowset**|`A component has finished processing all of its rows. : 1180 : Sort : 1181 : Sort Input`<br /><br /> `A component has finished processing all of its rows. : 1291 : OLE DB Destination : 1304 : OLE DB Destination Input`|  
|**OnPipelinePrePrimeOutput**|`PrimeOutput will be called on a component. : 1180 : Sort`<br /><br /> `PrimeOutput will be called on a component. : 1 : OLE DB Source`|  
|**OnPipelineRowsSent**|`Rows were provided to a data flow component as input. :  : 1185 : OLE DB Source Output : 1180 : Sort : 1181 : Sort Input : 76`<br /><br /> `Rows were provided to a data flow component as input. :  : 1308 : Sort Output : 1291 : OLE DB Destination : 1304 : OLE DB Destination Input : 76`|  
|**PipelineComponentTime**|`The component "Calculate LineItemTotalCost" (3522) spent 356 milliseconds in ProcessInput.`<br /><br /> `The component "Sum Quantity and LineItemTotalCost" (3619) spent 79 milliseconds in ProcessInput.`<br /><br /> `The component "Calculate Average Cost" (3662) spent 16 milliseconds in ProcessInput.`<br /><br /> `The component "Sort by ProductID" (3717) spent 125 milliseconds in ProcessInput.`<br /><br /> `The component "Load Data" (3773) spent 0 milliseconds in ProcessInput.`<br /><br /> `The component "Extract Data" (3869) spent 688 milliseconds in PrimeOutput filling buffers on output "OLE DB Source Output" (3879).`<br /><br /> `The component "Sum Quantity and LineItemTotalCost" (3619) spent 141 milliseconds in PrimeOutput filling buffers on output "Aggregate Output 1" (3621).`<br /><br /> `The component "Sort by ProductID" (3717) spent 16 milliseconds in PrimeOutput filling buffers on output "Sort Output" (3719).`|  
|**PipelineExecutionPlan**|`SourceThread0`<br /><br /> `Drives: 1`<br /><br /> `Influences: 1180 1291`<br /><br /> `Output Work List`<br /><br /> `CreatePrimeBuffer of type 1 for output ID 11.`<br /><br /> `SetBufferListener: "WorkThread0" for input ID 1181`<br /><br /> `CreatePrimeBuffer of type 3 for output ID 12.`<br /><br /> `CallPrimeOutput on component "OLE DB Source" (1)`<br /><br /> `End Output Work List`<br /><br /> `End SourceThread0`<br /><br /> `WorkThread0`<br /><br /> `Drives: 1180`<br /><br /> `Influences: 1180 1291`<br /><br /> `Input Work list, input ID 1181 (1 EORs Expected)`<br /><br /> `CallProcessInput on input ID 1181 on component "Sort" (1180) for view type 2`<br /><br /> `End Input Work list for input 1181`<br /><br /> `Output Work List`<br /><br /> `CreatePrimeBuffer of type 4 for output ID 1182.`<br /><br /> `SetBufferListener: "WorkThread1" for input ID 1304`<br /><br /> `CallPrimeOutput on component "Sort" (1180)`<br /><br /> `End Output Work List`<br /><br /> `End WorkThread0`<br /><br /> `WorkThread1`<br /><br /> `Drives: 1291`<br /><br /> `Influences: 1291`<br /><br /> `Input Work list, input ID 1304 (1 EORs Expected)`<br /><br /> `CallProcessInput on input ID 1304 on component "OLE DB Destination" (1291) for view type 5`<br /><br /> `End Input Work list for input 1304`<br /><br /> `Output Work List`<br /><br /> `End Output Work List`<br /><br /> `End WorkThread1`|  
|**PipelineExecutionTrees**|`begin execution tree 0`<br /><br /> `output "OLE DB Source Output" (11)`<br /><br /> `input "Sort Input" (1181)`<br /><br /> `end execution tree 0`<br /><br /> `begin execution tree 1`<br /><br /> `output "OLE DB Source Error Output" (12)`<br /><br /> `end execution tree 1`<br /><br /> `begin execution tree 2`<br /><br /> `output "Sort Output" (1182)`<br /><br /> `input "OLE DB Destination Input" (1304)`<br /><br /> `output "OLE DB Destination Error Output" (1305)`<br /><br /> `end execution tree 2`|  
|**PipelineInitialization**|`No temporary BLOB data storage locations were provided. The buffer manager will consider the directories in the TEMP and TMP environment variables.`<br /><br /> `The default buffer size is 10485760 bytes.`<br /><br /> `Buffers will have 10000 rows by default`<br /><br /> `The data flow will not remove unused components because its RunInOptimizedMode property is set to false.`|  
  
 Many log events write multiple entries, and the messages for a number of log entries contain complex data. To make it easier to understand and to communicate the content of complex messages you can parse the message text. Depending on the location of the logs, you can use Transact-SQL statements or a Script component to separate the complex text into columns or other formats that you find more useful.  
  
 For example, the following table contains the message "Rows were provided to a data flow component as input. :  : 1185 : OLE DB Source Output : 1180 : Sort : 1181 : Sort Input : 76", parsed into columns. The message was written by the **OnPipelineRowsSent** event when rows were sent from the OLE DB source to the Sort transformation.  
  
|Column|Description|Value|  
|------------|-----------------|-----------|  
|**PathID**|The value from the **ID** property of the path between the OLE DB source and the Sort transformation.|1185|  
|**PathName**|The value from the **Name** property of the path.|OLE DB Source Output|  
|**ComponentID**|The value of the **ID** property of the Sort transformation.|1180|  
|**ComponentName**|The value from the **Name** property of the Sort transformation.|Sort|  
|**InputID**|The value from the **ID** property of the input to the Sort transformation.|1181|  
|**InputName**|The value from the **Name** property of the input to the Sort transformation.|Sort Input|  
|**RowsSent**|The number of rows sent to the input of the Sort transformation.|76|  
  
## Configuration of the Data Flow Task  
 You can set properties in the **Properties** window or programmatically.  
  
 For more information about how to set these properties in the **Properties** window, click the following topic:  
  
-   [Set the Properties of a Task or Container](https://msdn.microsoft.com/library/52d47ca4-fb8c-493d-8b2b-48bb269f859b)  
  
## Programmatic Configuration of the Data Flow Task  
 For more information about programmatically adding a data flow task to a package and setting data flow properties, click the following topic:  
  
-   [Adding the Data Flow Task Programmatically](../../integration-services/building-packages-programmatically/adding-the-data-flow-task-programmatically.md)  
  
## Related Tasks  
 [Set the Properties of a Task or Container](https://msdn.microsoft.com/library/52d47ca4-fb8c-493d-8b2b-48bb269f859b)  
  
## Related Content  
 Video, [Balanced Data Distributer](https://go.microsoft.com/fwlink/?LinkID=226278&clcid=0x409), on technet.microsoft.com.  
  
  
