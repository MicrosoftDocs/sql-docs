---
title: "Specify an Interval of Change Data | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "incremental load [Integration Services],specifying interval"
ms.assetid: 17899078-8ba3-4f40-8769-e9837dc3ec60
author: janinezhang
ms.author: janinez
manager: craigg
---
# Specify an Interval of Change Data

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  In the control flow of an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that performs an incremental load of change data, the first task is to calculate the endpoints of the change interval. These endpoints are **datetime** values and will be stored in package variables for use later in the package.  
  
> [!NOTE]  
>  For a description of the overall process of designing the control flow, see [Change Data Capture &#40;SSIS&#41;](../../integration-services/change-data-capture/change-data-capture-ssis.md).  
  
## Set Up Package Variables for the Endpoints  
 Before configuring the Execute SQL task to calculate the endpoints, the package variables that will store the endpoints have to be defined.  
  
#### To set up package variables  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open a new [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project.  
  
2.  In the **Variables** window, create the following variables:  
  
    1.  Create a variable with the **datetime** data type to hold the starting point for the interval.  
  
         This example uses the variable name, ExtractStartTime.  
  
    2.  Create another variable with the **datetime** data type to hold the ending point for the interval.  
  
         This example uses the variable name, ExtractEndTime.  
  
 If you calculate the endpoints in a master package that executes multiple child packages, you can use Parent Package Variable configurations to pass the values of these variables to each child package. For more information, see [Execute Package Task](../../integration-services/control-flow/execute-package-task.md) and [Use the Values of Variables and Parameters in a Child Package](../../integration-services/packages/legacy-package-deployment-ssis.md#child).  
  
## Calculate a Starting Point and an Ending Point for Change Data  
 After you set up the package variables for the interval endpoints, you can calculate the actual values for those endpoints and map those values to the corresponding package variables. Because those endpoints are **datetime** values, you will have to use functions that can calculate or work with **datetime** values. Both the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] expression language and Transact-SQL have functions that work with **datetime** values:  
  
 Functions in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] expression language that work with **datetime** values  
 -   [DATEADD &#40;SSIS Expression&#41;](../../integration-services/expressions/dateadd-ssis-expression.md)  
  
-   [DATEDIFF &#40;SSIS Expression&#41;](../../integration-services/expressions/datediff-ssis-expression.md)  
  
-   [DATEPART &#40;SSIS Expression&#41;](../../integration-services/expressions/datepart-ssis-expression.md)  
  
-   [DAY &#40;SSIS Expression&#41;](../../integration-services/expressions/day-ssis-expression.md)  
  
-   [GETDATE &#40;SSIS Expression&#41;](../../integration-services/expressions/getdate-ssis-expression.md)  
  
-   [GETUTCDATE &#40;SSIS Expression&#41;](../../integration-services/expressions/getutcdate-ssis-expression.md)  
  
-   [MONTH &#40;SSIS Expression&#41;](../../integration-services/expressions/month-ssis-expression.md)  
  
-   [YEAR &#40;SSIS Expression&#41;](../../integration-services/expressions/year-ssis-expression.md)  
  
 Functions in Transact-SQL that work with **datetime** values  
 [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md).  
  
 Before you use any one of these **datetime** functions to calculate the endpoints, you have to determine whether the interval is fixed and occurs on a regular schedule. Typically, you want to apply changes that have occurred in source tables to destination tables on a regular schedule. For example, you might want to apply those changes on an hourly, daily, or weekly basis.  
  
 After you understand whether your change interval is fixed or is more random, you can calculate the endpoints:  
  
-   **Calculating the starting date and time**. You use the ending date and time from the previous load as the current starting date and time. If you use a fixed interval for incremental loads, you can calculate this value by using the **datetime** functions of Transact-SQL or of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] expression language. Otherwise, you might have to persist the endpoints between executions, and use an Execute SQL task or a Script task to load the previous endpoint.  
  
-   **Calculating the ending date and time**. If you use a fixed interval for incremental loads, calculate the current ending date and time as an offset from the starting date and time. Again, you can calculate this value by using the **datetime** functions of Transact-SQL or of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] expression language.  
  
 In the following procedure, the change interval uses a fixed interval and assumes that the incremental load package is run daily without exception. Otherwise, change data for missed intervals would be lost. The starting point for the interval is midnight the day before yesterday, that is, between 24 and 48 hours ago. The ending point for the interval is midnight yesterday, that is, the previous night, between 0 and 24 hours ago.  
  
#### To calculate the starting point and ending point for the capture interval  
  
1.  On the **Control Flow** tab of [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, add an Execute SQL Task to the package.  
  
2.  Open the **Execute SQL Task Editor**, and on the **General** page of the editor, select the following options:  
  
    1.  For **ResultSet**, select **Single row**.  
  
    2.  Configure a valid connection to the source database.  
  
    3.  For **SQLSourceType**, select **Direct input**.  
  
    4.  For **SQLStatement**, enter the following SQL statement:  
  
        ```sql
        SELECT DATEADD(dd,0, DATEDIFF(dd,0,GETDATE()-1)) AS ExtractStartTime,  
          DATEADD(dd,0, DATEDIFF(dd,0,GETDATE())) AS ExtractEndTime  
  
        ```  
  
3.  On the **Result Set** page of the **Execute SQL Task Editor**, map the ExtractStartTime result to the ExtractStartTime package variable, and map the ExtractEndTime result to the ExtractEndTime package variable.  
  
    > [!NOTE]  
    >  When you use an expression to set the value of an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] variable, the expression is evaluated every time that the value of the variable is accessed.  
  
## Next Step  
 After you calculate the starting point and ending point for a range of changes, the next step is to determine whether the change data is ready.  
  
 **Next topic:** [Determine Whether the Change Data Is Ready](../../integration-services/change-data-capture/determine-whether-the-change-data-is-ready.md)  
  
## See Also  
 [Use Variables in Packages](https://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787)   
 [Integration Services &#40;SSIS&#41; Expressions](../../integration-services/expressions/integration-services-ssis-expressions.md)   
 [Execute SQL Task](../../integration-services/control-flow/execute-sql-task.md)   
 [Script Task](../../integration-services/control-flow/script-task.md)  
  
  
