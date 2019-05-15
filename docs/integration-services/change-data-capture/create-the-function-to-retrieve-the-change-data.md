---
title: "Create the Function to Retrieve the Change Data | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "incremental load [Integration Services],creating function"
ms.assetid: 55dd0946-bd67-4490-9971-12dfb5b9de94
author: janinezhang
ms.author: janinez
manager: craigg
---
# Create the Function to Retrieve the Change Data

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  After completing the control flow for an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that performs an incremental load of change data, the next task is to create a table-valued function that retrieves the change data. You only have to create this function one time before the first incremental load.  
  
> [!NOTE]  
>  The creation of a function to retrieve the change data is the second step in the process of creating a package that performs an incremental load of change data. For a description of the overall process for creating this package, see [Change Data Capture &#40;SSIS&#41;](../../integration-services/change-data-capture/change-data-capture-ssis.md).  
  
## Design Considerations for Change Data Capture Functions  
 To retrieve change data, a source component in the data flow of the package calls one of the following change data capture query functions:  
  
-   **cdc.fn_cdc_get_net_changes_<capture_instance>** For this query, the single row returned for each update contains the final state of each changed row. In most cases, you only need the data returned by a query for net changes. For more information, see [cdc.fn_cdc_get_net_changes_&#60;capture_instance&#62; &#40;Transact-SQL&#41;](../../relational-databases/system-functions/cdc-fn-cdc-get-net-changes-capture-instance-transact-sql.md).  
  
-   **cdc.fn_cdc_get_all_changes_<capture_instance>** This query returns all the changes that have occurred in each row during the capture interval. For more information, see [cdc.fn_cdc_get_all_changes_&#60;capture_instance&#62;  &#40;Transact-SQL&#41;](../../relational-databases/system-functions/cdc-fn-cdc-get-all-changes-capture-instance-transact-sql.md).  
  
 The source component then takes the results returned by the function and passes them to downstream transformations and destinations, which apply the change data to the final destination.  
  
 However, an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] source component cannot call these change data capture functions directly. An [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] source component requires metadata about the columns that the query returns. The change data capture functions do not define the columns of their output table. Thus, these functions do not return sufficient metadata for an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] source component.  
  
 Instead, you use a table-valued wrapper function because this kind of function explicitly defines the columns of its output table in its RETURNS clause. This explicit definition of columns provides the metadata that an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] source component needs. You have to create this function for each table for which you want to retrieve change data.  
  
 You have two options for creating the table-valued wrapper function that calls the change data capture query function:  
  
-   You can call the **sys.sp_cdc_generate_wrapper_function** system stored procedure to create the table-valued functions for you.  
  
-   You can write your own table-valued function by using the guidelines and the example in this topic.  
  
## Calling a Stored Procedure to Create the Table-valued Function  
 The quickest and easiest way to create the table-valued functions that you need is to call the **sys.sp_cdc_generate_wrapper_function** system stored procedure. This stored procedure generates scripts to create wrapper functions that are designed specifically to meet the needs of an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] source component.  
  
> [!IMPORTANT]  
>  The **sys.sp_cdc_generate_wrapper_function** system stored procedure does not directly create the wrapper functions. Instead, the stored procedure generates the CREATE scripts for the wrapper functions. The developer must run the CREATE scripts that the stored procedure generates before an incremental load package can call the wrapper functions.  
  
 To understand how to use this system stored procedure, you should understand what the procedure does, what scripts the procedure generates, and what wrapper functions the scripts create.  
  
### Understanding and Using the Stored Procedure  
 The **sys.sp_cdc_generate_wrapper_function** system stored procedure generates scripts to create wrapper functions for use by [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages.  
  
 Here are the first few lines of the definition of the stored procedure:  
  
 `CREATE PROCEDURE sys.sp_cdc_generate_wrapper_function`  
  
 `(`  
  
 `@capture_instance sysname = null`  
  
 `@closed_high_end_point bit = 1,`  
  
 `@column_list = null,`  
  
 `@update_flag_list = null`  
  
 `)`  
  
 All the parameters for the stored procedure are optional. If you call the stored procedure without supplying values for any of the parameters, the stored procedure creates wrapper functions for all the capture instances to which you have access.  
  
> [!NOTE]  
>  For more information about the syntax of this stored procedure and its parameters, see [sys.sp_cdc_generate_wrapper_function &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-generate-wrapper-function-transact-sql.md).  
  
 The stored procedure always generates a wrapper function to return all changes from each capture instance. If the *@supports_net_changes* parameter was set when the capture instance was created, the stored procedure also generates a wrapper function to return net changes from each applicable capture instance.  
  
 The stored procedure returns a result set with two columns:  
  
-   The name of the wrapper function that the stored procedure has generated. This stored procedure derives the function name from the name of the capture instance name. (The function name is 'fn_all_changes_' followed by the capture instance name. The prefix used for the net changes function, if it is created, is 'fn_net_changes_'.)  
  
-   The CREATE statement for the wrapper function.  
  
### Understanding and Using the Scripts Created by the Stored Procedure  
 Typically, a developer would use an INSERT...EXEC statement to call the **sys.sp_cdc_generate_wrapper_function** stored procedure and save the scripts that the stored procedure creates to a temporary table. Each script could then be individually selected and run to create the corresponding wrapper function. However, a developer could also use one set of SQL commands to run all the CREATE scripts, as shown in the following sample code:  
  
```  
create table #wrapper_functions  
      (function_name sysname, create_stmt nvarchar(max))  
insert into #wrapper_functions  
exec sys.sp_cdc_generate_wrapper_function  
  
declare @stmt nvarchar(max)  
declare #hfunctions cursor local fast_forward for   
      select create_stmt from #wrapper_functions  
open #hfunctions  
fetch #hfunctions into @stmt  
while (@@fetch_status <> -1)  
begin  
      exec sp_executesql @stmt  
      fetch #hfunctions into @stmt  
end  
close #hfunctions  
deallocate #hfunctions  
```  
  
### Understanding and Using the Functions Created by the Stored Procedure  
 To systematically walk the timeline of captured change data, the generated wrapper functions expect that the *@end_time* parameter for one interval will be the *@start_time* parameter for the subsequent interval. When this convention is followed, the generated wrapper functions can do the following tasks:  
  
-   Map the date/time values to the LSN values that are used internally.  
  
-   Ensure that no data is lost or repeated.  
  
 To make querying for all rows of a change table simpler, the generated wrapper functions also support the following conventions:  
  
-   If the @start_time parameter is null, the wrapper functions use the lowest LSN value in the capture instance as the lower bound of the query.  
  
-   If the @end_time parameter is null, the wrapper functions use the highest LSN value in the capture instance as the upper bound of the query.  
  
 Most users should be able to use the wrapper functions that the **sys.sp_cdc_generate_wrapper_function** system stored procedure creates without modification. However, to customize the wrapper functions, you have to customize the CREATE scripts before you run the scripts.  
  
 When your package calls the wrapper functions, the package must supply values for three parameters. These three parameters are like the three parameters that the change data capture functions use. These three parameters are as follows:  
  
-   The starting date/time value and the ending date/time value for the interval. While the wrapper functions use date/time values as the end points for the query interval, the change data capture functions use two LSN values as the end points.  
  
-   The row filter. For both the wrapper functions and the change data capture functions, the *@row_filter_option* parameter is the same. For more information, see [cdc.fn_cdc_get_all_changes_&#60;capture_instance&#62;  &#40;Transact-SQL&#41;](../../relational-databases/system-functions/cdc-fn-cdc-get-all-changes-capture-instance-transact-sql.md) and [cdc.fn_cdc_get_net_changes_&#60;capture_instance&#62; &#40;Transact-SQL&#41;](../../relational-databases/system-functions/cdc-fn-cdc-get-net-changes-capture-instance-transact-sql.md).  
  
 The result set returned by the wrapper functions includesthe following data:  
  
-   All of the requested columns of change data.  
  
-   A column named __CDC_OPERATION that uses a one- or two-character field to identify the operation that is associated with the row. The valid values for this field are as follows: 'I' for insert, 'D' for delete, 'UO' for update old values, and 'UN' for update new values.  
  
-   Update flags, when you request them, that appear as bit columns after the operation code and in the order that is specified in the *@update_flag_list* parameter. These columns are named by appending '_uflag' to the associated column name.  
  
 If your package calls a wrapper function that queries for all changes, the wrapper function also returns the columns, __CDC_STARTLSN and \__CDC_SEQVAL. These two columns become the first and second columns, respectively, of the result set. The wrapper function also sorts the result set based on these two columns.  
  
## Writing Your Own Table-Value Function  
 You can also use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to write your own table-valued wrapper function that calls the change data capture query function, and store the table-valued wrapper function in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information about how to create a Transact-SQL function, see [CREATE FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-function-transact-sql.md).  
  
 The following example defines a table-valued function that retrieves changes from a Customer table for the specified change interval. This function uses change data capture functions to map the **datetime** values to the binary log sequence number (LSN) values that the change tables use internally. This function also handles several special conditions:  
  
-   When a null value is passed for the starting time, this function uses the earliest available value.  
  
-   When a null value is passed for the ending time, this function uses the latest available value.  
  
-   When the starting LSN is equal to the ending LSN, which usually indicates that there are no records for the selected interval, this function exits.  
  
### Example of a Table-Value Function that Queries for Change Data  
  
```  
CREATE function CDCSample.uf_Customer (  
     @start_time datetime  
    ,@end_time datetime  
)  
returns @Customer table (  
     CustomerID int  
    ,TerritoryID int  
    ,CustomerType nchar(1)  
    ,rowguid uniqueidentifier  
    ,ModifiedDate datetime  
    ,CDC_OPERATION varchar(1)  
) as  
begin  
    declare @from_lsn binary(10), @to_lsn binary(10)  
  
    if (@start_time is null)  
        select @from_lsn = sys.fn_cdc_get_min_lsn('Customer')  
    else  
        select @from_lsn = sys.fn_cdc_increment_lsn(sys.fn_cdc_map_time_to_lsn('largest less than or equal',@start_time))  
  
    if (@end_time is null)  
        select @to_lsn = sys.fn_cdc_get_max_lsn()  
    else  
        select @to_lsn = sys.fn_cdc_map_time_to_lsn('largest less than or equal',@end_time)  
  
    if (@from_lsn = sys.fn_cdc_increment_lsn(@to_lsn))  
        return  
  
    -- Query for change data  
    insert into @Customer  
    select   
        CustomerID,      
        TerritoryID,   
        CustomerType,   
        rowguid,   
        ModifiedDate,   
        case __$operation  
                when 1 then 'D'  
                when 2 then 'I'  
                when 4 then 'U'  
                else null  
         end as CDC_OPERATION  
    from   
        cdc.fn_cdc_get_net_changes_Customer(@from_lsn, @to_lsn, 'all')  
  
    return  
end   
go  
  
```  
  
### Retrieving Additional Metadata with the Change Data  
 Although the user-created table-valued function shown earlier uses only the **__$operation** column, the **cdc.fn_cdc_get_net_changes_<capture_instance>** function returns four columns of metadata for each change row. If you want to use these values in your data flow, you can return them as additional columns from the table-valued wrapper function.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**__$start_lsn**|**binary(10)**|LSN associated with the commit transaction for the change.<br /><br /> All changes committed in the same transaction share the same commit LSN. For example, if an update operation on the source table modifies two different rows, the change table will contain four rows (two with the old values and two with the new values), each with the same **__$start_lsn** value.|  
|**__$seqval**|**binary(10)**|Sequence value that is used to order the row changes in a transaction.|  
|**__$operation**|**int**|The data manipulation language (DML) operation associated with the change. Can be one of the following:<br /><br /> 1 = delete<br /><br /> 2 = insert<br /><br /> 3 = update (Values before the update operation.)<br /><br /> 4 = update (Values after the update operation.)|  
|**__$update_mask**|**varbinary(128)**|A bitmask that is based on the column ordinals of the change table identifying those columns that changed. You could examine this value if you had to determine which columns have changed.|  
|**\<captured source table columns>**|varies|The remaining columns returned by the function are the columns from the source table that were identified as captured columns when the capture instance was created. If no columns were originally specified in the captured column list, all columns in the source table are returned.|  
  
 For more information, see [cdc.fn_cdc_get_net_changes_&#60;capture_instance&#62; &#40;Transact-SQL&#41;](../../relational-databases/system-functions/cdc-fn-cdc-get-net-changes-capture-instance-transact-sql.md).  
  
## Next Step  
 After you have created the table-valued function that queries for change data, the next step is to start designing the data flow in the package.  
  
 **Next topic:** [Retrieve and Understand the Change Data](../../integration-services/change-data-capture/retrieve-and-understand-the-change-data.md)  
  
  
