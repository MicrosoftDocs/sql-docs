---
title: "Determine Whether the Change Data Is Ready | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "incremental load [Integration Services],determining readiness"
ms.assetid: 04935f35-96cc-4d70-a250-0fd326f8daff
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Determine Whether the Change Data Is Ready
  In the control flow of an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that performs an incremental load of change data, the second task is to ensure that the change data for the selected interval is ready. This step is necessary because the asynchronous capture process might not yet have processed all the changes up to the selected endpoint.  
  
> [!NOTE]  
>  The first task for the control flow is to calculate the endpoints of the change interval. For more information about this task, see [Specify an Interval of Change Data](../../integration-services/change-data-capture/specify-an-interval-of-change-data.md). For a description of the overall process of designing the control flow, see [Change Data Capture &#40;SSIS&#41;](../../integration-services/change-data-capture/change-data-capture-ssis.md).  
  
## Understanding the Components of the Solution  
 The solution described in this topic uses 4 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] components:  
  
-   A For Loop container that repeatedly evaluates the output of an Execute SQL Task.  
  
-   An Execute SQL task that queries special tables that the change data capture process maintains and then uses this information to determine whether data is ready.  
  
-   A component that implements a delay in processing when the data is not ready. This can be either a Script task or an Execute SQL task.  
  
-   Optionally, a component that reports an error or a timeout when the Execute SQL task returns a value that indicates an error or a timeout condition.  
  
 These components set or read the values of several package variables to control the flow of execution inside the loop and later in the package.  
  
#### To set up package variables  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], in the **Variables** window, create the following variables:  
  
    1.  Create a variable with an integer data type to hold the status value returned by the Execute SQL task.  
  
         This example uses the variable name, DataReady, with an initial value of 0.  
  
    2.  Create a variable to hold the period of time to delay when data is not ready. If you plan to use a Script task to implement the delay, the variable should have an integer data type integer. If you plan to use an Execute SQL task with a WAITFOR statement, the variable should have a string data type to accept values such as "00:00:10".  
  
         This example uses the variable name, DelaySeconds, with an initial value of 10.  
  
    3.  Create a variable with an integer data type to hold the current iteration of the loop.  
  
         This example uses the variable name, TimeoutCount, with an initial value of 0.  
  
    4.  Create a variable with an integer data type to specify the number of times that the loop should test for data before reporting a timeout condition.  
  
         This example uses the variable name, TimeoutCeiling, with an initial value of 20.  
  
    5.  (Optional) Create a variable with an integer data type that you can use to indicate the first load of change data.  
  
         This example uses the variable name, IntervalID, and checks only for a value of 0 to indicate the initial load.  
  
## Configuring a For Loop Container  
 With the variables set, the For Loop container is the first component to be added.  
  
#### To configure a For Loop container to wait until change data is ready  
  
1.  On the **Control Flow** tab of the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, add a For Loop container to the control flow.  
  
2.  Connect the Execute SQL Task that calculates the endpoints of the interval to the For Loop container.  
  
3.  In the **For Loop Editor**, select the following options:  
  
    1.  For **InitExpression**, enter `@DataReady = 0`.  
  
         This expression sets the initial value of the loop variable.  
  
    2.  For **EvalExpression**m, enter `@DataReady == 0`.  
  
         When this expression evaluates to **False**, execution passes out of the loop and the incremental load starts.  
  
## Configuring the Execute SQL Task That Queries for Change Data  
 Inside the For Loop container, you add an Execute SQL task. This task queries the tables that the change data capture process maintains in the database. The result of this query is a status value that indicates whether the change data is ready.  
  
 In the following table, the first column shows the values returned from the Execute SQL task by the sample Transact-SQL query. The second column shows how the other components respond to these values.  
  
|Return Value|Meaning|Response|  
|------------------|-------------|--------------|  
|0|Indicates that the change data is not ready.<br /><br /> There are no change data capture records later than the ending point of the selected interval.|Execution continues with the component that implements a delay. Then control returns to the For Loop container, which continues to check the Execute SQL task as long as the value returned is 0.|  
|1|Might indicate that the change data has not been captured for the complete interval, or that it has been deleted. This is treated as an error condition.<br /><br /> There are no change data capture records earlier than the starting point of the selected interval|Execution continues with the optional component that logs the error.|  
|2|Indicates that data is ready.<br /><br /> There are change data capture records that are both earlier than the starting point and later than the ending point of the selected interval.|Execution passes out of the For Loop container and the incremental load starts.|  
|3|Indicates the initial load of all available change data.<br /><br /> The conditional logic obtains this value from a special package variable that is used only for this purpose.|Execution passes out of the For Loop container and the incremental load starts.|  
|5|Indicates that the TimeoutCeiling has been reached.<br /><br /> The loop has tested for data the specified number of times, and data is still not available. Without this test or a similar test, the package might run indefinitely.|Execution continues with the optional component that logs the timeout.|  
  
#### To configure an Execute SQL task to query whether change data is ready  
  
1.  Inside the For Loop container, add an Execute SQL task.  
  
2.  In the **Execute SQL Task Editor**, on the **General** page, select the following options:  
  
    1.  For **ResultSet**, select **Single row**.  
  
    2.  Configure a valid connection to the source database.  
  
    3.  For **SQLSourceType**, select **Direct input**.  
  
    4.  For **SQLStatement**, enter the following SQL statement:  
  
        ```  
        declare @DataReady int, @TimeoutCount int  
  
        if not exists (select tran_end_time from cdc.lsn_time_mapping  
                where tran_end_time > ?  )  
            select @DataReady = 0  
        else  
            if ? = 0  
                select @DataReady = 3   
        else  
            if not exists (select tran_end_time from cdc.lsn_time_mapping  
                    where tran_end_time <= ? )  
                select @DataReady = 1   
        else  
            select @DataReady = 2  
  
        select @TimeoutCount = ?  
        if (@DataReady = 0)  
            select @TimeoutCount = @TimeoutCount + 1  
        else  
            select @TimeoutCount = 0  
  
        if (@TimeoutCount > ?)  
            select @DataReady = 5  
  
        select @DataReady as DataReady, @TimeoutCount as TimeoutCount  
  
        ```  
  
3.  On the **Parameter Mapping** page of the **Execute SQL Task Editor**, make the following mappings:  
  
    1.  Map the ExtractEndTime variable to parameter 0.  
  
    2.  Map the IntervalID variable to parameter 1.  
  
    3.  Map the ExtractStartTime variable to parameter 2.  
  
    4.  Map the TimeoutCount variable to parameter 3.  
  
    5.  Map the TimeoutCeiling variable to parameter 4.  
  
4.  On the **Result Set** page of the **Execute SQL Task Editor**, map the DataReady result to the DataReady variable, and the TimeoutCount result to the TimeoutCount variable.  
  
## Waiting Until the Change Data Is Ready  
 You can use one of several methods to implement a delay when the change data is not ready. The following two procedures illustrate how to use a Script task or an Execute SQL task to implement the delay.  
  
> [!NOTE]  
>  A precompiled script incurs less overhead than an Execute SQL task.  
  
#### To implement a delay by using a Script task  
  
1.  Inside the For Loop container, add a Script task.  
  
2.  Connect the Execute SQL task that queries to determine whether the change data is ready to the new Script task.  
  
3.  For the precedence constraint that connects the Execute SQL task to the Script task, open the **Precedence Constraint Editor** and select the following options:  
  
    1.  For **Evaluation operation**, select **Expression and Constraint**.  
  
    2.  For **Value**, select **Success**.  
  
         The constraint value of **Success** refers to the success of the previous task. In this case, the success of the Execute SQL task.  
  
    3.  For **Expression**, enter `@DataReady == 0 && @TimeoutCount <= @TimeoutCeiling`.  
  
    4.  Select **Logical AND. All constraints must evaluate to True**, if not already selected.  
  
4.  In the **Script Task Editor**, on the **Script** page, for **ReadOnlyVariables**, select the **User::DelaySeconds** integer variable from the list.  
  
5.  In the **Script Task Editor**, on the **Script** page, click **Edit Script** to open the script development environment.  
  
6.  In the Main procedure, enter one of the following lines of code:  
  
    -   If you are programming in C#, enter the following line of code:  
  
        ```  
        System.Threading.Thread.Sleep((int)Dts.Variables["DelaySeconds"].Value * 1000);  
        ```  
  
         \- or -  
  
    -   If you are programming in [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)], enter the following line of code:  
  
        ```  
        System.Threading.Thread.Sleep(Ctype(Dts.Variables("DelaySeconds").Value, Integer) * 1000)  
  
        ```  
  
        > [!NOTE]  
        >  The **Thread.Sleep** method expects an argument that is specified in milliseconds.  
  
7.  Leave the default line of code which returns **DtsExecResult.Success** from the execution of the script.  
  
8.  Close the script development environment and the **Script Task Editor**.  
  
#### To implement a delay by using an Execute SQL task  
  
1.  Inside the For Loop container, add an Execute SQL task.  
  
2.  Connect the Execute SQL task that queries to determine whether the change data is ready to the new Execute SQL task.  
  
3.  For the precedence constraint that connects the two Execute SQL tasks, open the **Precedence Constraint Editor** and select the following options:  
  
    1.  For **Evaluation operation**, select **Expression and Constraint**.  
  
    2.  For **Value**, select **Success**.  
  
         The constraint value of **Success** refers to the success of the previous Execute SQL task.  
  
    3.  For **Expression**, enter `@DataReady == 0`.  
  
    4.  Select **Logical AND. All constraints must evaluate to True**, if not already selected.  
  
         This selection requires that both conditions, the constraint and the expression, must be true.  
  
4.  In the **Execute SQL Task Editor**, on the **General** page, select the following options:  
  
    1.  For **ResultSet**, select **Single row**.  
  
    2.  Configure a valid connection to the source database.  
  
    3.  For **SQLSourceType**, select **Direct input**.  
  
    4.  For **SQLStatement**, enter the following SQL statement:  
  
        ```  
        WAITFOR DELAY ?  
  
        ```  
  
5.  On the **Parameter Mapping** page of the editor, map the DelaySeconds string variable to parameter 0.  
  
## Handling an Error Condition  
 You can optionally configure an additional component inside the loop to log an error or a timeout condition:  
  
-   This component can log an error condition when the value of the DataReady variable = 1. This value indicates that there is no available change data before the start of the selected interval.  
  
-   This component can also log a timeout condition when the value of the TimeoutCeiling variable is reached. This value indicates the loop has tested for data the specified number of times, and data is still not available. Without this test or a similar test, the package might run indefinitely.  
  
#### To configure an optional Script task to log an error condition  
  
1.  If you want to report the error or timeout by writing a message to the log, configure logging for the package. For more information, see [Enable Package Logging in SQL Server Data Tools](../../integration-services/performance/integration-services-ssis-logging.md#ssdt).  
  
2.  Inside the For Loop container, add a Script task.  
  
3.  Connect the Execute SQL task that queries to determine whether the change data is ready to the new Script task.  
  
4.  For the precedence constraint that connects the Execute SQL task to the Script task, open the **Precedence Constraint Editor** and select the following options:  
  
    1.  For **Evaluation operation**, select **Expression and Constraint**.  
  
    2.  For **Value**, select **Success**.  
  
         The constraint value of **Success** refers to the success of the previous task. In this case, the success of the Execute SQL task.  
  
    3.  For **Expression**, enter `@DataReady == 1 || @DataReady == 5`.  
  
    4.  Select **Logical AND. All constraints must evaluate to True**, if not already selected.  
  
         This selection requires that both conditions, the constraint and the expression, must be true.  
  
5.  In the **Script Task Editor**, on the **Script** page of the editor, for **ReadOnlyVariables**, select **User::DataReady** and **User::ExtractStartTime** from the list to make their values available to the script.  
  
     If you want to include information from certain system variables (for example, System::PackageName) in the information that you write to the log, select those variables also.  
  
6.  In the **Script Task Editor**, on the **Script** page, click **Edit Script** to open the script development environment.  
  
7.  In the Main procedure, enter code to log an error by calling the **Dts.Log** method, or to raise an event by calling one of the methods of the **Dts.Events** interface. Inform the package of the error by returning `Dts.TaskResult = Dts.Results.Failure`.  
  
     The following sample shows how to write a message to the log. For more information, see [Logging in the Script Task](../../integration-services/extending-packages-scripting/task/logging-in-the-script-task.md), [Raising Events in the Script Task](../../integration-services/extending-packages-scripting/task/raising-events-in-the-script-task.md), and [Returning Results from the Script Task](../../integration-services/extending-packages-scripting/task/returning-results-from-the-script-task.md).  
  
    ```  
    ' User variables.  
    Dim dataReady As Integer = _  
      CType(Dts.Variables("DataReady").Value, Integer)  
    Dim extractStartTime As Date = _  
      CType(Dts.Variables("ExtractStartTime").Value, DateTime)  
  
    ' System variables.  
    Dim packageName As String = _  
      Dts.Variables("PackageName").Value.ToString()  
    Dim executionStartTime As Date = _  
      CType(Dts.Variables("StartTime").Value, DateTime)  
  
    Dim eventMessage As New System.Text.StringBuilder()  
  
    If dataReady = 1 OrElse dataReady = 5 Then  
  
      If dataReady = 1 Then  
        eventMessage.AppendLine("Start Time Error")  
      Else  
        eventMessage.AppendLine("Timeout Error")  
      End If  
  
      With eventMessage  
        .Append("The package ")  
        .Append(packageName)  
        .Append(" started at ")  
        .Append(executionStartTime.ToString())  
        .Append(" and ended at ")  
        .AppendLine(DateTime.Now().ToString())  
        If dataReady = 1 Then  
          .Append("The specified ExtractStartTime was ")  
          .AppendLine(extractStartTime.ToString())  
        End If  
      End With  
  
      System.Windows.Forms.MessageBox.Show(eventMessage.ToString())  
  
      Dts.Log(eventMessage.ToString(), 0, Nothing)  
  
      Dts.TaskResult = Dts.Results.Failure  
  
    Else  
  
      Dts.TaskResult = Dts.Results.Success  
  
    End If  
  
    ```  
  
8.  Close the script development environment and the **Script Task Editor**.  
  
## Next Step  
 After you determine that change data is ready, the next step is to prepare to query for the change data.  
  
 **Next topic:** [Prepare to Query for the Change Data](../../integration-services/change-data-capture/prepare-to-query-for-the-change-data.md)  
  
  
