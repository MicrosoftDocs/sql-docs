---
title: "Azure Data Lake Analytics task | Microsoft Docs"
description: You can submit U-SQL jobs to Azure Data Lake Analytics service with the Data Lake Analytics task.
ms.custom: ""
ms.date: "05/18/2018"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "SQL13.DTS.DESIGNER.AFPADLSTASK.F1"
  - "SQL14.DTS.DESIGNER.AFPADLSTASK.F1"
author: "yanancai"
ms.author: "yanacai"
ms.reviewer: "douglasl"
manager: craigg
---

# Azure Data Lake Analytics task

You can submit U-SQL jobs to Azure Data Lake Analytics service with the Data Lake Analytics task. This task is a component of the [SQL Server Integration Services (SSIS) feature pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).

For general background, see [Azure Data Lake Analytics](https://azure.microsoft.com/services/data-lake-analytics/).

## Configure the task

To add a Data Lake Analytics task to a package, drag the task from SSIS Toolbox to the designer canvas. Then double-click the task, or right-click the task and select **Edit**. The **Azure Data Lake Analytics Task Editor** dialog box opens. You can set properties through SSIS Designer, or programmatically.

## General page configuration

Use the **General** page to configure the task and provide the U-SQL script that the task submits. To learn more about the U-SQL language, see [U-SQL language reference](https://msdn.microsoft.com/azure/data-lake-analytics/u-sql/u-sql-language-reference).

### Basic configuration

You can specify the name and description of the task.

### U-SQL configuration

U-SQL configuration has two settings: **SourceType**, and dynamic options based on the **SourceType** value. 

**SourceType** specifies the source of the U-SQL script. The script is submitted to a Data Lake Analytics account during SSIS package execution. The options for this property are:

|Value|Description|  
|-----------|-----------------|  
|**DirectInput**|Specifies the U-SQL script through the inline editor. Selecting this value displays the dynamic option, **USQLStatement**.|  
|**FileConnection**|Specifies a local .usql file that contains the U-SQL script. Selecting this option displays the dynamic option, **FileConnection**.|  
|**Variable**|Specifies an SSIS variable that contains the U-SQL script. Selecting this value displays the dynamic option, **SourceVariable**.|

**SourceType Dynamic Options** specifies the script content for the U-SQL query. 

|SourceType|Dynamic Options|  
|-----------|-----------------|  
|**SourceType = DirectInput**|Type the U-SQL query to be submitted in the option box directly, or select the browse button (...) to type the U-SQL query in the **Enter U-SQL Query** dialog box.|  
|**SourceType = FileConnection**|Select an existing file connection manager, or select <**New connection...**> to create a new file connection. For related information, see [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md) and [File Connection Manager Editor](../../integration-services/connection-manager/file-connection-manager-editor.md).|  
|**SourceType = Variable**|Select an existing variable, or select \<**New variable...**> to create a new variable. For related information, see [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md) and [Add Variable](https://msdn.microsoft.com/library/d09b5d31-433f-4f7c-8c68-9df3a97785d5).|


### Job configuration
Job configuration specifies U-SQL job submission properties.

- **AzureDataLakeAnalyticsConnection:** Specifies the Data Lake Analytics account where the U-SQL script is submitted. Choose the connection from a list of defined connection managers. To create a new connection, select <**New connection**>. For related information, see [Azure Data Lake Analytics Connection Manager](../../integration-services/connection-manager/azure-data-lake-analytics-connection-manager.md).

- **JobName:** Specifies the name of the U-SQL job. 
- **AnalyticsUnits:** Specifies the analytics unit count of the U-SQL job.
- **Priority:** Specifies the priority of the U-SQL job. You can set this from 0 to 1000. The lower the number is, the higher the priority.
- **RuntimeVersion:** Specifies the Data Lake Analytics runtime version of the U-SQL job. It is set to "default" by default. Usually you don't need to change this property.
- **Synchronous:** A Boolean value specifies if the task waits for the job execution to complete or not. If the value is set to true, the task is marked as **succeed** after the job completes. If the value is set to false, the task is marked as **succeed** after the job passes the preparation phase.

  |Value|Description|
  |-----------|-----------------|
  |True|The task result is based on the U-SQL job execution result. Job succeeds > task succeeds. Job fails > task fails. Task succeeds or fails > task completes.|
  |False|The task result is based on the U-SQL job submission and preparation result. Job submission succeeds and passes the preparation phase > task succeeds. Job submission fails or job fails at the preparation phase > task fails. Task succeeds or fails > task completes.|

- **TimeOut:** Specifies a time-out time, in seconds, for job execution. If the job times out, it is cancelled and marked as failed. This property is not available if **Synchronous** is set to false.

## Parameter Mapping page configuration

Use the **Parameter Mapping** page of the **Azure Data Lake Analytics Task Editor** dialog box to map variables to parameters (U-SQL variables) in U-SQL script.

- **Variable Name:** After you have added a parameter mapping by selecting **Add**, select a system or user-defined variable from the list. Alternatively, you can select <**New variable...**> to add a new variable by using the **Add Variable** dialog box. For related information, see [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md).  

- **Parameter Name:** Provide a parameter/variable name in U-SQL script. Make sure the parameter name starts with the \@ sign, like \@Param1. 

Here is an example of how to pass parameters to U-SQL script.

**Sample U-SQL script**
```
@searchlog =
    EXTRACT UserId          int,
            Start           DateTime,
            Region          string,
            Query           string,
            Duration        int,
            Urls            string,
            ClickedUrls     string
    FROM @in
    USING Extractors.Tsv(nullEscape:"#NULL#");

@rs1 =
    SELECT Start, Region, Duration
    FROM @searchlog
WHERE Region == "en-gb";

@rs1 =
    SELECT Start, Region, Duration
    FROM @rs1
    WHERE Start <= DateTime.Parse("2012/02/19");

OUTPUT @rs1   
    TO @out
      USING Outputters.Tsv(quoting:false, dateTimeFormat:null);
```

Note that the input and output paths are defined in **\@in** and **\@out** parameters. The values for **\@in** and **\@out** parameters in the U-SQL script are passed dynamically by the parameter mapping configuration.

|Variable name|Parameter name|
|-------------|--------------|
|User: Variable1|\@in|
|User: Variable2|\@out| 

## Expression page configuration

You can assign all properties in the General page configuration as a property expression, to enable dynamic update of the property at runtime. For related information, see [Use Property Expressions in Packages](../../integration-services/expressions/use-property-expressions-in-packages.md).

## See also
- [Azure Data Lake Analytics Connection Manager](../../integration-services/connection-manager/azure-data-lake-analytics-connection-manager.md)
- [Azure Data Lake Store File System Task](../../integration-services/control-flow/azure-data-lake-store-file-system-task.md)
- [Azure Data Lake Store Connection Manager](../../integration-services/connection-manager/azure-data-lake-store-connection-manager.md)

