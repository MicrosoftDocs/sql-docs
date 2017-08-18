---
title: "sp_execute_external_script (Transact-SQL) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "08/18/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sp_execute_external_script_TSQL"
  - "sys.sp_execute_external_script"
  - "sys.sp_execute_external_script_TSQL"
  - "sp_execute_external_script"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_execute_external_script"
ms.assetid: de4e1fcd-0e1a-4af3-97ee-d1becc7f04df
caps.latest.revision: 34
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sp_execute_external_script (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Executes the script provided as argument at an external location. The script must be written in a supported and registered language. The query tree is controlled by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and users cannot perform arbitrary operations on the query. To execute **sp_execute_external_script**, you must first enable external scripts by using the `sp_configure 'external scripts enabled', 1;` statement.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
sp_execute_external_script   
    @language = N'language' ,   
    @script = N'script',  
  
    @input_data_1 = ] 'input_data_1'   
    [ , @input_data_1_name = ] N'input_data_1_name' ]   
    [ , @output_data_1_name = 'output_data_1_name' ]  
    [ , @parallel = 0 | 1 ]  
    [ , @params = ] N'@parameter_name data_type [ OUT | OUTPUT ] [ ,...n ]'  
    [ , @parameter1 = ] 'value1' [ OUT | OUTPUT ] [ ,...n ]  
    [ WITH <execute_option> ]  
[;]  
  
<execute_option>::=  
{  
      { RESULT SETS UNDEFINED }   
    | { RESULT SETS NONE }   
    | { RESULT SETS ( <result_sets_definition> ) }  
}  
  
<result_sets_definition> ::=   
{  
    (  
         { column_name   
           data_type   
         [ COLLATE collation_name ]   
         [ NULL | NOT NULL ] }  
         [,...n ]  
    )  
    | AS OBJECT   
        [ db_name . [ schema_name ] . | schema_name . ]   
        {table_name | view_name | table_valued_function_name }  
    | AS TYPE [ schema_name.]table_type_name  
}  
```  
  
## Arguments  
 [ @language =    ] '*language*'  
Indicates the script language. *language* is **sysname**.  

 Valid values are `Python` or `R`. 
  
 [ @script = ]    '*script*'  
 External language  script specified as a literal or variable input. *script* is **nvarchar(max)**.  
  
 [ @input_data_1_name = ] '*input_data_1_name*'  
 Specifies the name of the variable used to represent the query defined by @input_data_1. The data type of the variable in the external script depends on the language. In case of R, the input variable is a data frame. In the case of Python, input must be tabular. *input_data_1_name* is **sysname**.  
  
 Default value is "InputDataSet".  
  
 [ @input_data_1 = ] '*input_data_1*'  
 Specifies the input data used by the external script in the form of a [!INCLUDE[tsql](../../includes/tsql-md.md)] query. *input_data_1* is **nvarchar(max)**.  
  
 [ @output_data_1_name = ] '*output_data_1_name*'  
 Specifies the name of the variable in the external script that contains the data to be returned to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] upon completion of the stored procedure call. The data type of the variable in the external script depends on the language. In the case of R, the output must be a data frame. In the case of Python, the output must be a pandas data frame. *output_data_1_name* is **sysname**.  
  
 Default value is "OutputDataSet".  
  
 [ @parallel = ] 0 | 1  
 Enable parallel execution of R scripts by setting the `@parallel` parameter to 1. The default for this parameter is 0 (no parallelism).  
  
 For R scripts that do not use RevoScaleR functions, using the  `@parallel` parameter can be beneficial for processing large datasets, assuming the script can be trivially parallelized. For example, when using the R `predict` function with a model to generate new predictions, set `@parallel = 1` as a hint to the query engine. If the query can be parallelized, rows are distributed according to the **MAXDOP** setting.  
  
 If `@parallel = 1` and the output is being streamed directly to the client machine, then the `WITH RESULTS SETS` clause is required and an output schema must be specified.  
  
 For R scripts that use RevoScaleR functions, parallel processing is handled automatically and you should not specify `@parallel = 1` to the **sp_execute_external_script** call.  
  
 [ @params = ] N'*@parameter_name data_type* [ OUT | OUTPUT ] [ ,...n ]'  
 A list of input parameter declarations that are used in the external script.  
  
 [ @parameter1 = ] '*value1*'  [ OUT | OUTPUT ] [ ,...n ]  
 A list of values for the input parameters used by the external script.  
  
## Remarks  
 Use **sp_execute_external_script** to execute scripts written in a supported language such as R. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] is comprised of a server component installed with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and a set of workstation tools and connectivity libraries that connect the data scientist to the high-performance environment of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Install R Services (In-Database) during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup to enable the execution of R scripts. For more information, see [Set up SQL Server R Services &#40;In-Database&#41;](../../advanced-analytics/r-services/set-up-sql-server-r-services-in-database.md).  
  
 You can control the resources used by an external script by configuring an external resource pool. For more information, see [CREATE EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-resource-pool-transact-sql.md). Information about the workload can be obtained from the resource governor catalog views, DMV's, and counters. For more information, see [Resource Governor Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/resource-governor-catalog-views-transact-sql.md), [Resource Governor Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/resource-governor-related-dynamic-management-views-transact-sql.md), and [SQL Server, External Scripts Object](../../relational-databases/performance-monitor/sql-server-external-scripts-object.md).  

Monitor script execution using [sys.dm_external_script_requests](../../relational-databases/system-dynamic-management-views/sys-dm-external-script-requests.md) and [sys.dm_external_script_execution_stats](../../relational-databases/system-dynamic-management-views/sys-dm-external-script-execution-stats.md). 

  
 In addition to returning a result set, you can return scalar values from R script to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using OUTPUT parameters. The following example shows the use of OUTPUT parameter:  
  
```  
DECLARE @model varbinary(max);  
EXEC sp_execute_external_script  
        @language = N'R'  
        , @script = N'  
    # build classification model to predict tipped or not  
    logitObj <- glm(tipped ~ passenger_count + trip_distance + trip_time_in_secs + direct_distance, data = featureDataSource, family = binomial(link=logit));  
  
    # First, serialize a model and put it into a database table  
    modelbin <- serialize(logitObj, NULL);  
    '  
        , @input_data_1 = N'  
SELECT tipped, passenger_count, trip_time_in_secs, trip_distance, d.direct_distance  
  FROM dbo.nyctaxi_sample TABLESAMPLE (1 PERCENT) REPEATABLE (98074)  
  CROSS APPLY [CalculateDistance](pickup_latitude, pickup_longitude,  dropoff_latitude, dropoff_longitude) as d  
'  
        , @input_data_1_name = N'featureDataSource'  
        , @params = N'@modelbin varbinary(max) OUTPUT'  
        , @modelbin = @model OUTPUT;  
```  


## Streaming execution for R script  
 Streaming execution of R script is supported by specifying `@r_rowsPerRead int parameter` in `@params` collection.  Streaming allows R scripts to work with data that doesn’t fit in memory. For example, if there are billion rows to score using predict function the new `@r_rowsPerRead` parameter can be used to split the execution into one stream at a time. Because this parameter controls the number of rows that are sent to the R processes, it can also be used to throttle the number of rows being read at one time. This might be useful to mitigate server performance issues if, for example, a large model is being trained. Note that this parameter can only be used in cases where the output of the R script doesn’t depend on reading or looking at the entire set of rows.  
  
 Both the `@r_rowsPerRead` parameter for streaming and the `@parallel` argument should be considered hints. For the hint to be applied, it must be possible to generate a SQL query plan that includes parallel processing. If this is not possible, parallel processing cannot be enabled.  
  
> [!NOTE]  
>  Streaming and parallel processing are supported only in Enterprise Edition. You can include the parameters in your queries in Standard Edition without raising an error, but the parameters have no effect and R scripts run in a single process.  
  
## Restrictions  
 **Data types:** The following data types are not supported when used in the input query or parameters of `sp_execute_external_script` procedure, and return an unsupported type error.  
  
 As a workaround, **CAST** the column or value to a supported type in [!INCLUDE[tsql](../../includes/tsql-md.md)] and send it to R.  
  
-   **cursor**  
  
-   **timestamp**  
  
-   **datetime2**, **datetimeoffset**, **time**  
  
-   **sql_variant**  
  
-   **text**, **image**  
  
-   **xml**  
  
-   **hierarchyid**, **geometry**, **geography**  
  
-   CLR user-defined types  
  
 `WITH RESULTS SETS`  clause is mandatory if you are returning a result set from R . The specified column data types need to match the types supported in R (**bit**, **int**, **float**, **datetime**, **varchar**)  
  
 **datetime** values in the input are converted to NA on the R side for values that do not fit the permissible range of values in R. this is required because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] permits a larger range of values than is supported in the R language.  
  
 Float values (for example, +Inf, -Inf, NaN) are not supported in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] even though both languages use IEEE 754. Current behavior just sends the values to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directly and as a result sqlclient in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] throws error. These values convert to **NULL**.  
  
 When using the `WITH RESULTS SET` clause, an error is raised in the following conditions:  
  
-   The number of columns doesn’t match the number of columns in the R data frame.  
  
-   Any [!INCLUDE[tsql](../../includes/tsql-md.md)] data type that cannot be mapped to an R data type is transfered as NULL. Any R result set that cannot be mapped to a [!INCLUDE[tsql](../../includes/tsql-md.md)] data type, transfers as NULL.  
  
## Permissions  
 Requires **EXECUTE ANY EXTERNAL SCRIPT** database permission.  
  
## Examples  
 This section contains examples of how this stored procedure can be used to execute R scripts using [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
### A. Return a data set from R to SQL Server  
 The following example creates a stored procedure that uses **sp_execute_external_script** to return an iris dataset from R to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
```  
DROP PROC IF EXISTS get_iris_dataset;  
go  
CREATE PROC get_iris_dataset  
AS  
BEGIN  
 EXEC   sp_execute_external_script  
       @language = N'R'  
     , @script = N'iris_data <- iris;'  
     , @input_data_1 = N''  
     , @output_data_1_name = N'iris_data'  
     WITH RESULT SETS (("Sepal.Length" float not null,   
           "Sepal.Width" float not null,  
        "Petal.Length" float not null,   
        "Petal.Width" float not null, "Species" varchar(100)));  
END;  
go  
```  
  
### B. Generate a model based on data from SQL Server  
 The following example creates a stored procedure that uses **sp_execute_external_script** to generate an iris model and return the model to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  This example requires installing the e1071 package. For more information, see [Install Additional R Packages on SQL Server](../../advanced-analytics/r-services/install-additional-r-packages-on-sql-server.md).  
  
```  
DROP PROC IF EXISTS generate_iris_model;  
go  
  
CREATE PROC generate_iris_model  
AS  
BEGIN  
 EXEC sp_execute_external_script  
      @language = N'R'  
     , @script = N'  
          library(e1071);  
          irismodel <-naiveBayes(iris_data[,1:4], iris_data[,5]);  
          trained_model <- data.frame(payload = as.raw(serialize(irismodel, connection=NULL)));  
'  
     , @input_data_1 = N'select "Sepal.Length", "Sepal.Width", "Petal.Length", "Petal.Width", "Species" from iris_data'  
     , @input_data_1_name = N'iris_data'  
     , @output_data_1_name = N'trained_model'  
    WITH RESULT SETS ((model varbinary(max)));  
END;  
go  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Python Libraries and Data Types](../../advanced-analytics/python/python-libraries-and-data-types.md)  
 [R Libraries and R Data Types](../../advanced-analytics/r/r-libraries-and-data-types.md)  
 [SQL Server R Services](../../advanced-analytics/r-services/sql-server-r-services.md)   
 [Known Issues for SQL Server R Services](../../advanced-analytics/r-services/known-issues-for-sql-server-r-services.md)   
 [CREATE EXTERNAL LIBRARY &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-library-transact-sql.md)  
 [sp_prepare &#40;Transact SQL&#41;](../../relational-databases/system-stored-procedures/sp-prepare-transact-sql.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [external scripts enabled Server Configuration Option](../../database-engine/configure-windows/external-scripts-enabled-server-configuration-option.md)   
 [SERVERPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/serverproperty-transact-sql.md)   
 [SQL Server, External Scripts Object](../../relational-databases/performance-monitor/sql-server-external-scripts-object.md)  
[sys.dm_external_script_requests](../../relational-databases/system-dynamic-management-views/sys-dm-external-script-requests.md)  
[sys.dm_external_script_execution_stats](../../relational-databases/system-dynamic-management-views/sys-dm-external-script-execution-stats.md) 
  
