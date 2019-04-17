---
title: "Prepare to Query for the Change Data | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "incremental load [Integration Services],preparing query"
ms.assetid: 9ea2db7a-3dca-4bbf-9903-cccd2d494b5f
author: janinezhang
ms.author: janinez
manager: craigg
---
# Prepare to Query for the Change Data
  In the control flow of an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that performs an incremental load of change data, the third and final task is to prepare to query for the change data and add a Data Flow task.  
  
> [!NOTE]  
>  The second task for the control flow is to ensure that the change data for the selected interval is ready. For more information about this task, see [Determine Whether the Change Data Is Ready](../../integration-services/change-data-capture/determine-whether-the-change-data-is-ready.md). For a description of the overall process of designing the control flow, see [Change Data Capture &#40;SSIS&#41;](../../integration-services/change-data-capture/change-data-capture-ssis.md).  
  
## Design Considerations  
 To retrieve the change data, you will call a Transact-SQL table-valued function that accepts the endpoints of the interval as input parameters and returns change data for the specified interval. A source component in the data flow calls this function. For information about this source component, see [Retrieve and Understand the Change Data](../../integration-services/change-data-capture/retrieve-and-understand-the-change-data.md).  
  
 The most frequently used [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] source components, including the OLE DB source, the ADO source, and the ADO NET source, cannot derive parameter information for a table-valued function. Therefore, most sources cannot call a parameterized function directly.  
  
 You have two design options for passing the input parameters to the function:  
  
-   **Assemble the parameterized query as a string**. You can use a Script task or an Execute SQL task to assemble a dynamic SQL string with parameter values hard-coded into the string. Then, you can store this string in a package variable and use it to set the SqlCommand property of a source component. This approach succeeds because the source component no longer requires parameter information.  
  
    > [!NOTE]  
    >  A precompiled script incurs less overhead than an Execute SQL task.  
  
-   **Use a parameterized wrapper**. Alternatively, you can create a parameterized stored procedure as a wrapper that calls the parameterized table-valued function. This approach succeeds because a source component can successfully derive parameter information for a stored procedure.  
  
 This topic uses the first design option and assembles a parameterized query as a string.  
  
## Preparing the Query  
 Before you can concatenate the values of the input parameters into a single query string, you have to set up the package variables that the query needs.  
  
#### To set up package variables  
  
-   In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], in the **Variables** window, create a variable with a string data type to hold the query string returned by the Execute SQL Task.  
  
     This example uses the variable name, SqlDataQuery.  
  
 With the package variable created, you can use either a Script task or Execute SQL task to concatenate the values of the input parameters. The following two procedures describe how to configure these components.  
  
#### To use a Script task to concatenate the query string  
  
1.  On the **Control Flow** tab, add a Script task to the package after the For Loop container and connect the For Loop container to the task.  
  
    > [!NOTE]  
    >  This procedure assumes that the package performs an incremental load from a single table. If the package loads from multiple tables and has a parent package with multiple child packages, this task would be added as the first component to each child package. For more information, see [Perform an Incremental Load of Multiple Tables](../../integration-services/change-data-capture/perform-an-incremental-load-of-multiple-tables.md).  
  
2.  In the **Script Task Editor**, on the **Script** page, select the following options:  
  
    1.  For **ReadOnlyVariables**, select **User::DataReady**, **User::ExtractStartTime**, and **User::ExtractEndTime** from the.  
  
    2.  For **ReadWriteVariables**, select User::SqlDataQuery from the list.  
  
3.  In the **Script Task Editor**, on the **Script** page, click **Edit Script** to open the script development environment.  
  
4.  In the Main procedure, enter one of the following code segments:  
  
    -   If you are programming in C#, enter the following lines of code:  
  
        ```csharp 
        int dataReady;  
        System.DateTime extractStartTime;  
        System.DateTime extractEndTime;  
        string sqlDataQuery;  
  
        dataReady = (int)Dts.Variables["DataReady"].Value;  
        extractStartTime = (System.DateTime)Dts.Variables["ExtractStartTime"].Value;  
        extractEndTime = (System.DateTime)Dts.Variables["ExtractEndTime"].Value;  
  
        if (dataReady == 2)  
          {  
          sqlDataQuery = "SELECT * FROM CDCSample.uf_Customer('" + string.Format("{0:yyyy-MM-dd hh:mm:ss}", extractStartTime) + "', '" + string.Format("{0:yyyy-MM-dd hh:mm:ss}", extractEndTime) + "')";  
          }  
        else  
          {  
          sqlDataQuery = "SELECT * FROM CDCSample.uf_Customer(null" + ", '" + string.Format("{0:yyyy-MM-dd hh:mm:ss}", extractEndTime) + "')";  
          }  
  
        Dts.Variables["SqlDataQuery"].Value = sqlDataQuery;  
        ```  
  
         \- or -  
  
    -   If you are programming in [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)], enter the following lines of code:  
  
        ```vb  
        Dim dataReady As Integer  
        Dim extractStartTime As Date  
        Dim extractEndTime As Date  
        Dim sqlDataQuery As String  
  
        dataReady = CType(Dts.Variables("DataReady").Value, Integer)  
        extractStartTime = CType(Dts.Variables("ExtractStartTime").Value, Date)  
        extractEndTime = CType(Dts.Variables("ExtractEndTime").Value, Date)  
  
        If dataReady = 2 Then  
          sqlDataQuery = "SELECT * FROM CDCSample.uf_Customer('" & _  
              String.Format("{0:yyyy-MM-dd hh:mm:ss}", extractStartTime) & _  
              "', '" & _  
              String.Format("{0:yyyy-MM-dd hh:mm:ss}", extractEndTime) & _  
              "')"  
        Else  
          sqlDataQuery = "SELECT * FROM CDCSample.uf_Customer(null" & _  
              ", '" & _  
              String.Format("{0:yyyy-MM-dd hh:mm:ss}", extractEndTime) & _  
              "')"  
        End If  
  
        Dts.Variables("SqlDataQuery").Value = sqlDataQuery  
  
        ```  
  
5.  Leave the default line of code which returns **DtsExecResult.Success** from the execution of the script.  
  
6.  Close the script development environment and the **Script Task Editor**.  
  
#### To use an Execute SQL task to concatenate the query string  
  
1.  On the **Control Flow** tab, add an Execute SQL task to the package after the For Loop container and connect the For Loop container to this task.  
  
    > [!NOTE]  
    >  This procedure assumes that the package performs an incremental load from a single table. If the package loads from multiple tables and has a parent package with multiple child packages, this task would be added as the first component to each child package. For more information, see [Perform an Incremental Load of Multiple Tables](../../integration-services/change-data-capture/perform-an-incremental-load-of-multiple-tables.md).  
  
2.  In the **Execute SQL Task Editor**, on the **General** page, select the following options:  
  
    1.  For **ResultSet**, select **Single row**.  
  
    2.  Configure a valid connection to the source database.  
  
    3.  For **SQLSourceType**, select **Direct input**.  
  
    4.  For **SQLStatement**, enter the following SQL statement:  
  
        ```sql
        declare @ExtractStartTime datetime,  
        @ExtractEndTime datetime,   
        @DataReady int  
  
        select @DataReady = ?,   
        @ExtractStartTime = ?,   
        @ExtractEndTime = ?  
  
        if @DataReady = 2  
        select N'select * from CDCSample.uf_Customer'  
        + N'('''+ convert(nvarchar(30),@ExtractStartTime,120)  
        + ''', '''  
        + convert(nvarchar(30),@ExtractEndTime,120) + ''') '   
        as SqlDataQuery  
        else  
        select N'select * from CDCSample.uf_Customer'  
        + N'(null, '''  
        + convert(nvarchar(30),@ExtractEndTime,120)  
        + ''') '  
        as SqlDataQuery  
  
        ```  
  
        > [!NOTE]  
        >  The **else** clause in this sample generates a query for the initial load of change data by passing a null value for the starting date and time. This sample does not address the scenario in which changes that were made before change data capture was enabled also have to be uploaded to the data warehouse.  
  
3.  On the **Parameter Mapping** page of the **Execute SQL Task Editor**, do the following mapping:  
  
    1.  Map the DataReady variable to parameter 0.  
  
    2.  Map the ExtractStartTime variable to parameter 1.  
  
    3.  Map the ExtractEndTime variable to parameter 2.  
  
4.  On the **Result Set** page of the **Execute SQL Task Editor**, map the Result Name to the SqlDataQuery variable.  
  
     The Result Name is the name of the single column that is returned, SqlDataQuery.  
  
 The previous procedures configure a task that prepares a query string with hard-coded string values for the input parameters. The following code is an example of such a query string:  
  
 `select * from CDCSample. uf_Customer('2007-06-11 14:21:58', '2007-06-12 14:21:58')`  
  
## Adding a Data Flow Task  
 The last step in designing the control flow for the package is to add a Data Flow task.  
  
#### To add a Data Flow task and complete the control flow  
  
-   On the **Control Flow** tab, add a Data Flow task and connect the task that concatenated the query string.  
  
## Next Step  
 After you prepare the query string and configure the Data Flow task, the next step is create the table-valued function that will retrieve the change data from the database.  
  
 **Next topic:** [Create the Function to Retrieve the Change Data](../../integration-services/change-data-capture/create-the-function-to-retrieve-the-change-data.md)  
  
  
