---
title: "Using Test Conditions in SQL Server Unit Tests | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
f1_keywords: 
  - "sql.data.tools.unittesting.testconditions"
ms.assetid: e3d1c86c-1e58-4d2c-b625-d1b591b221aa
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# Using Test Conditions in SQL Server Unit Tests
In a SQL Server unit test, one or more Transact\-SQL test scripts are executed. The results can be evaluated within the Transact\-SQL script and THROW or RAISERROR used to return an error and fail the test, or test conditions can be defined in the test to evaluate the results. The test returns an instance of the [SqlExecutionResult](https://msdn.microsoft.com/library/microsoft.data.tools.schema.sql.unittesting.sqlexecutionresult.aspx) class. The instance of this class contains one or more DataSets, the execution time, and the rows affected by the script. All of this information is collected during execution of the script. These results can be evaluated by using test conditions. SQL Server Data Tools provides a set of predefined test conditions. You can also create and use custom conditions; see [Custom Test Conditions  for SQL Server Unit Tests](../ssdt/custom-test-conditions-for-sql-server-unit-tests.md).  
  
## Predefined Test Conditions  
The following table lists the predefined test conditions that you can add by using the Test Conditions pane in the SQL Server Unit Test Designer.  
  
|**Test Condition**|**Test Condition Description**|  
|----------------------|----------------------------------|  
|Data Checksum|Fails if the checksum of the result set returned from the Transact\-SQL script does not match the expected checksum. For more information, see [Specifying a Data Checksum](#SpecifyDataChecksum).<br /><br />**NOTE:** This test condition is not recommended if you are returning data that will vary between test runs. For example, if your result set contains generated dates or times, or contains identity columns, your tests will fail because the checksum will be different on each run.|  
|Empty ResultSet|Fails if the result set returned from the Transact\-SQL script is not empty.|  
|Execution Time|Fails if the Transact\-SQL test script takes longer than expected to execute. The default execution time is 30 seconds.<br /><br />The execution time applies to the test script test only, not to the pre-test script or the post-test script.|  
|Expected Schema|Fails if the columns and data types of the result set do not match those specified for the test condition. You must specify a schema through the properties of the test condition. For more information, see [Specifying an Expected Schema](#SpecifyExpectedSchema).|  
|Inconclusive|Always produces a test with a result of Inconclusive. This is the default condition added to every test. This test condition is included to indicate that test verification has not been implemented. Delete this test condition from your test after you have added other test conditions.|  
|Not Empty ResultSet|Fails if the result set is empty. You can use this test condition or the EmptyResultSet with the Transact\-SQL @@RAISERROR function in your test script to test whether an update worked correctly. For example, you can save pre-update values, run the update, compare post-update values, and raise an error if you do not get the expected results.|  
|Row Count|Fails if the result set does not contain the expected number of rows.|  
|Scalar Value|Fails if a particular value in the result set does not equal the specified value. The default **Expected value** is null.|  
  
> [!NOTE]  
> The Execution Time test condition specifies a time limit under which the Transact\-SQL test script must run. If this time limit is exceeded, the test fails. Test results also include a Duration statistic, which differs from the Execution Time test condition. The Duration statistic includes not only the execution time but also the time to connect to the database two times; the time to run any other test scripts, such as the pre-test script and the post-test script; and the time to run the test conditions. Therefore, a test can pass even if its duration is longer than its execution time.  
>   
> The reported Duration does not include time used for data generation and schema deployment because they occur before the tests are run. To view the test duration, select a test run in the **Test Results** window, right-click, and choose **View Test Results Details**.  
  
You can add test conditions to SQL Server unit tests by using the Test Conditions pane of the SQL Server Unit Test Designer. For more information, see [How to: Add Test Conditions to SQL Server Unit Tests](../ssdt/how-to-add-test-conditions-to-sql-server-unit-tests.md).  
  
You can also edit your test-method code directly to add more functionality. For more information, see [How to: Open a SQL Server Unit Test to Edit](../ssdt/how-to-open-a-sql-server-unit-test-to-edit.md) and [How to: Write a SQL Server Unit Test that Runs within the Scope of a Single Transaction](../ssdt/how-to-write-sql-server-unit-test-that-runs-in-single-transaction-scope.md). For example, you can add functionality to a test method by adding Assert statements. For more information, see [Using Transact-SQL Assertions in SQL Server Unit Tests](../ssdt/using-transact-sql-assertions-in-sql-server-unit-tests.md).  
  
## Expected Failures  
You might create SQL Server unit tests to test behavior that should not succeed. These expected failures are sometimes referred to as negative testing. Some examples would include the following:  
  
-   Verify that a stored procedure that deletes a customer's data fails if you specify an invalid customer ID.  
  
-   Verify that a stored procedure that fills an order fails if the order was never placed or if the order was already filled.  
  
-   Verify that a stored procedure that cancels an order cannot cancel completed orders or orders that were already canceled.  
  
You can define SQL Server unit tests for stored procedures that throw expected exceptions. You can add an attribute to the unit test method to indicate which exception or exceptions are expected. By doing this, you prevent the test from failing when the exception occurs.  
  
To mark a SQL Server unit test method with expected exceptions, add the following attribute:  
  
```  
[ExpectedSqlException(MessageNumber = nnnnn, Severity = x, MatchFirstError = false, State = y)]  
```  
  
Where:  
  
-   *nnnnn* is the number of the expected message, for example 14025  
  
-   *x* is the severity of the expected exception  
  
-   *y* is the state of the expected exception  
  
Any unspecified parameters are ignored. You pass these parameters to the **THROW** statement in your database code. If you specify MatchFirstError = false, then the attribute will match any of the SqlErrors in the exception. The default behavior (MatchFirstError = true) is to only match the first error that occurs.  
  
For an example of how to use expected exceptions and a negative SQL Server unit test, see [Walkthrough: Creating and Running a SQL Server Unit Test](../ssdt/walkthrough-creating-and-running-a-sql-server-unit-test.md).  
  
## <a name="SpecifyDataChecksum"></a>Specifying a Data Checksum  
To display the SQL Server Unit Test Designer, double click the unit test source code file in **Solution Explorer**.  
  
After you add a Data Checksum test condition to your database unit test, you must configure the expected checksum by using the following procedure:  
  
#### To specify an expected checksum  
  
1.  In the list of test conditions, click the Data Checksum test condition for which you want to specify a checksum.  
  
2.  Open the **Properties** window by pressing F4. You can also open the **View** menu and click **Properties** Window.  
  
3.  (Optional) You might want to change the **(Name)** property of the test condition to be more descriptive.  
  
4.  In the **Configuration** property, click the browse (**...**) button.  
  
    The **Configuration for TestConditionName** dialog box appears.  
  
5.  Specify a connection to the database that you want to test. For more information, see [How to: Create a Database Connection](https://msdn.microsoft.com/library/aa833420(VS.100).aspx).  
  
6.  By default, the Transact\-SQL body of your test appears in the edit pane. You can modify the code, if necessary, to produce the expected results. For example, if your test has code in the pre-test, you might have to add that code.  
  
    > [!IMPORTANT]  
    > If you modify a checksum condition for which you had previously specified a checksum, any changes that you made in the edit pane are not saved. You must make those changes again before you click **Retrieve**.  
  
7.  Click **Retrieve**.  
  
    The Transact\-SQL is executed against the specified database connection and the results appear in the dialog box.  
  
8.  If the results match the expected results of your test, click **OK**. Otherwise modify the Transact\-SQL body and repeat steps 6, 7, and 8 until the results are as expected.  
  
    The **Value** column of the test condition displays the value of the expected checksum.  
  
## <a name="SpecifyExpectedSchema"></a>Specifying an Expected Schema  
After you add an Expected Schema test condition to your SQL Server unit test, you must configure the expected schema by using the following procedure:  
  
#### To specify an expected schema  
  
1.  In the list of test conditions, click the Expected Schema test condition for which you want to specify a schema.  
  
2.  Open the **Properties** window by pressing F4. You can also open the **View** menu and click the **Properties** window.  
  
3.  (Optional) You might want to change the **(Name)** property of the test condition to be more descriptive.  
  
4.  In the **Configuration** property, click the browse (**...**) button.  
  
    The **Configuration for TestConditionName** dialog box appears.  
  
5.  Specify a connection to the database that you want to test. For more information, see [How to: Create a Database Connection](https://msdn.microsoft.com/library/aa833420(VS.100).aspx).  
  
6.  By default, the Transact\-SQL body of your test appears in the edit pane. You can modify the code, if necessary, to produce the expected results. For example, if your test has code in the pre-test, you might have to add that code.  
  
    > [!IMPORTANT]  
    > If you modify an expected schema condition for which you had previously specified a schema, any changes that you made in the edit pane are not saved. You must make those changes again before you click **Retrieve**.  
  
7.  Click **Retrieve**.  
  
    The Transact\-SQL is executed against the specified database connection and the results appear in the dialog box. Because you are verifying the schema, or shape, of the result set and not the values of the results, you do not have to see any data in the returned results, as long as the columns appear the way that you expect them to appear.  
  
8.  If the results match the expected results of your test, click **OK**. Otherwise modify the Transact\-SQL body and repeat steps 6, 7, and 8 until the results are as expected.  
  
    The **Value** column of the test condition displays information about the expected schema. For example, it might say "Expected: 2 tables".  
  
## Extensible Test Conditions  
In addition to the six predefined test conditions, you can write new test conditions of your own. These test conditions will be displayed in the Test Conditions pane of the SQL Server Unit Test Designer. For more information, see [Custom Test Conditions  for SQL Server Unit Tests](../ssdt/custom-test-conditions-for-sql-server-unit-tests.md).  
  
## See Also  
[Creating and Defining SQL Server Unit Tests](../ssdt/creating-and-defining-sql-server-unit-tests.md)  
[Using Transact-SQL Assertions in SQL Server Unit Tests](../ssdt/using-transact-sql-assertions-in-sql-server-unit-tests.md)  
[Scripts in SQL Server Unit Tests](../ssdt/scripts-in-sql-server-unit-tests.md)  
  
