---
title: "Automate Analysis Services Administrative Tasks with SSIS | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom:
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Automate Analysis Services Administrative Tasks with SSIS
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] enables you to automate execution of DDL scripts, cube and mining model processing tasks, and data mining query tasks. [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] can be thought of as a collection of control flow and maintenance tasks, which can be linked to form sequential and parallel data processing jobs.  
  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] is designed to perform data cleaning operations during data processing tasks, and to bring together data from different data sources. When working with cubes and mining models, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] can transform non-numeric data to numeric data, and can ensure that data values fall within expected bounds, thus creating clean data from which to populate fact tables and dimensions.  
  
## Integration Services Tasks  
 There are two main elements in any [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] task or job: control flow elements and data flow elements. The control flow elements define the logical ordering of job progression by applying precedence constraints. The data flow elements concern connectivity between the output of a component to the input of the following component, plus any data transform that might operate on that data in between. As for the decision about where the data goes, precedence constraints contain logic to specify which component receives the output. The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] tasks that are most relevant to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] include the Execute DDL Task, the Analysis Services Processing Task, and the Data Mining Query Task. For each of these tasks, the Send Mail Task can be used to send the administrator an e-mail message containing the task results.  
  
## The Execute DDL Task  
 The Execute DDL Task in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] enables you to send DDL scripts directly to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] server and to run them automatically. This allows the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] administrator to perform backup, restore, or sync operations from within an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package. A package is made up of the control and data flow elements described earlier, which all must be **run regularly**, as do other DDL statements that can be added to tasks. Because the tasks discussed here are frequently run at night, it is particularly useful to have packages that can easily be run from any scheduling application. You can schedule a package to be run at any time using [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Agent. For more information about how to implement this task, see [Analysis Services Execute DDL Task](../../integration-services/control-flow/analysis-services-execute-ddl-task.md).  
  
## Analysis Services Processing Task  
 The Analysis Services Processing Task in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] enables you to automatically populate cubes with new information when you make regular updates to your source relational database. You can process at the dimension, cube, or partition level using the Analysis Services Processing Task. The processing itself can be of type **incremental** or **full**, which you select based on your job requirements. Incremental processing adds new data and performs enough recalculation to keep the target up-to-date, whereas full processing drops existing data for a complete reload and recalculation. Full processing takes more time, but is more complete. For more information about how to implement this task, see [Analysis Services Processing Task](../../integration-services/control-flow/analysis-services-processing-task.md).  
  
## Data Mining Query Task  
 The Data Mining Query Task in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] enables you to extract and store information from mining models. The information is often stored in a relational database and, for example, can be used to isolate a list of potential customers for a targeted marking campaign. Data mining can identify the value of a customer and the probability that the customer will respond to a particular marketing pitch. You can use the Data Mining Query Task to extract and modify data to a preferred format. For more information about how to implement this task, see [Data Mining Query Task](../../integration-services/control-flow/data-mining-query-task.md).  
  
## See Also  
 [Partition Processing Destination](../../integration-services/data-flow/partition-processing-destination.md)   
 [Dimension Processing Destination](../../integration-services/data-flow/dimension-processing-destination.md)   
 [Data Mining Query Transformation](../../integration-services/data-flow/transformations/data-mining-query-transformation.md)   
 [Processing a multidimensional model &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/processing-a-multidimensional-model-analysis-services.md)   
  
  
