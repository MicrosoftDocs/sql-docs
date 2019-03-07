---
title: "Performing Batch Operations (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
helpviewer_keywords: 
  - "multiple projects"
  - "XML for Analysis, batches"
  - "parallel batch execution [XMLA]"
  - "transactional batches"
  - "serial batch execution [XMLA]"
  - "XMLA, batches"
  - "batches [XML for Analysis]"
  - "nontransactional batches"
ms.assetid: 731c70e5-ed51-46de-bb69-cbf5aea18dda
author: minewiskan
ms.author: owend
manager: craigg
---
# Performing Batch Operations (XMLA)
  You can use the [Batch](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/batch-element-xmla) command in XML for Analysis (XMLA) to run multiple XMLA commands using a single XMLA [Execute](https://docs.microsoft.com/bi-reference/xmla/xml-elements-methods-execute) method. You can run multiple commands contained in the `Batch` command either as a single transaction or in individual transactions for each command, in serial or in parallel. You can also specify out-of-line bindings and other properties in the `Batch` command for processing multiple [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects.  
  
## Running Transactional and Nontransactional Batch Commands  
 The `Batch` command executes commands in one of two ways:  
  
 **Transactional**  
 If the `Transaction` attribute of the `Batch` command is set to true, the `Batch` command run commands all of the commands contained by the `Batch` command in a single transaction-a *transactional* batch.  
  
 If any command fails in a transactional batch, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] rolls back any command in the `Batch` command that ran before the command that failed and the `Batch` command immediately ends. Any commands in the `Batch` command that have not yet run are not executed. After the `Batch` command ends, the `Batch` command reports any errors that occurred for the failed command.  
  
 **Nontransactional**  
 If the `Transaction` attribute is set to false, the `Batch` command runs each command contained by the `Batch` command in a separate transaction-a *nontransactional* batch. If any command fails in a nontransactional batch, the `Batch` command continues to run commands after the command which failed. After the `Batch` command tries to run all the commands that the `Batch` command contains, the `Batch` command reports any errors that occurred.  
  
 All results returned by commands contained in a `Batch` command are returned in the same order in which the commands are contained in the `Batch` command. The results returned by a `Batch` command vary based on whether the `Batch` command is transactional or nontransactional.  
  
> [!NOTE]  
>  If a `Batch` command contains a command that does not return output, such as the [Lock](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/lock-element-xmla) command, and that command successfully runs, the `Batch` command returns an empty [root](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/root-element-xmla) element within the results element. The empty `root` element ensures that each command contained in a `Batch` command can be matched with the appropriate `root` element for that command's results.  
  
### Returning Results from Transactional Batch Results  
 Results from commands run within a transactional batch are not returned until the entire `Batch` command is completed. The results are not returned after each command runs because any command that fails within a transactional batch would cause the entire `Batch` command and all containing commands to be rolled back. If all commands start and run successfully, the [return](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/return-element-xmla) element of the [ExecuteResponse](https://docs.microsoft.com/bi-reference/xmla/xml-elements-objects-executeresponse) element returned by the `Execute` method for the `Batch` command contains one [results](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/results-element-xmla) element, which in turn contains one `root` element for each successfully run command contained in the `Batch` command. If any command in the `Batch` command cannot be started or fails to complete, the `Execute` method returns a SOAP fault for the `Batch` command that contains the error of the command that failed.  
  
### Returning Results from Nontransactional Batch Results  
 Results from commands run within a nontransactional batch are returned in the order in which the commands are contained within the `Batch` command and as they are returned by each command. If no command contained in the `Batch` command can be successfully started, the `Execute` method returns a SOAP fault that contains an error for the `Batch` command. If at least one command is successfully started, the `return` element of the `ExecuteResponse` element returned by the `Execute` method for the `Batch` command contains one `results` element, which in turn contains one `root` element for each command contained in the `Batch` command. If one or more commands in a nontransactional batch cannot be started or fails to complete, the `root` element for that failed command contains an [error](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/error-element-xmla) element describing the error.  
  
> [!NOTE]  
>  As long as at least one command in a nontransactional batch can be started, the nontransactional batch is considered to have successfully run, even if every command contained in the nontransactional batch returns an error in the results of the `Batch` command.  
  
## Using Serial and Parallel Execution  
 You can use the `Batch` command to run included commands in serial or in parallel. When the commands are run in serial, the next command included in the `Batch` command cannot start until the currently running command in the `Batch` command is completed. When the commands are run in parallel, multiple commands can be executed simultaneously by the `Batch` command.  
  
 To run commands in parallel, you add the commands to be run in parallel to the [Parallel](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/parallel-element-xmla) property of the `Batch` command. Currently, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] can run only contiguous, sequential [Process](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/process-element-xmla) commands in parallel. Any other XMLA command, such as [Create](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/create-element-xmla) or [Alter](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/alter-element-xmla), included in the `Parallel` property is run serially.  
  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] tries to run all `Process` commands included in the `Parallel` property in parallel, but cannot guarantee that all included `Process` commands can be run in parallel. The instance analyzes each `Process` command and, if the instance determines that the command cannot be run in parallel, the `Process` command is run in serial.  
  
> [!NOTE]  
>  To run commands in parallel, the `Transaction` attribute of the `Batch` command must be set to true because [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] supports only one active transaction per connection and nontransactional batches run each command in a separate transaction. If you include the `Parallel` property in a nontransactional batch, an error occurs.  
  
### Limiting Parallel Execution  
 An [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance tries to run as many `Process` commands in parallel as possible, up to the limits of the computer on which the instance runs. You can limit the number of concurrently executing `Process` commands by setting the `maxParallel` attribute of the `Parallel` property to a value indicating the maximum number of `Process` commands that can be run in parallel.  
  
 For example, a `Parallel` property contains the following commands in the sequence listed:  
  
1.  `Create`  
  
2.  `Process`  
  
3.  `Alter`  
  
4.  `Process`  
  
5.  `Process`  
  
6.  `Process`  
  
7.  `Delete`  
  
8.  `Process`  
  
9. `Process`  
  
 The `maxParalle`l attribute of this `Parallel` property is set to 2. Therefore, the instance runs the previous lists of commands as described in the following list:  
  
-   Command 1 runs serially because command 1 is a `Create` command and only `Process` commands can be run in parallel.  
  
-   Command 2 runs serially after command 1 is completed.  
  
-   Command 3 runs serially after command 2 is completed.  
  
-   Commands 4 and 5 run in parallel after command 3 is completed. Although command 6 is also a `Process` command, command 6 cannot run in parallel with commands 4 and 5 because the `maxParallel` property is set to 2.  
  
-   Command 6 runs serially after both commands 4 and 5 are completed.  
  
-   Command 7 runs serially after command 6 is completed.  
  
-   Commands 8 and 9 run in parallel after command 7 is completed.  
  
## Using the Batch Command to Process Objects  
 The `Batch` command contains several optional properties and attributes specifically included to support processing multiple [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] projects:  
  
-   The `ProcessAffectedObjects` attribute of the `Batch` command indicates whether the instance should also process any object that requires reprocessing as a result of a `Process` command included in the `Batch` command processing a specified object.  
  
-   The [Bindings](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/bindings-element-xmla) property contains a collection of out-of-line bindings used by all of the `Process` commands in the `Batch` command.  
  
-   The [DataSource](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/source-element-xmla) property contains an out-of-line binding for a data source used by all of the `Process` commands in the `Batch` command.  
  
-   The [DataSourceView](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/datasourceview-element-xmla) property contains an out-of-line binding for a data source view used by all of the `Process` commands in the `Batch` command.  
  
-   The [ErrorConfiguration](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/errorconfiguration-element-xmla) property specifies the way in which the `Batch` command handles errors encountered by all `Process` commands contained in the `Batch` command.  
  
    > [!IMPORTANT]  
    >  A `Process` command cannot include the `Bindings`, `DataSource`, `DataSourceView`, or `ErrorConfiguration` properties, if the `Process` command is contained in a `Batch` command. If you must specify these properties for a `Process` command, provide the necessary information in the corresponding properties of the `Batch` command that contains the `Process` command.  
  
## See Also  
 [Batch Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/batch-element-xmla)   
 [Process Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/process-element-xmla)   
 [Multidimensional Model Object Processing](../multidimensional-models/processing-a-multidimensional-model-analysis-services.md)   
 [Developing with XMLA in Analysis Services](developing-with-xmla-in-analysis-services.md)  
  
  
