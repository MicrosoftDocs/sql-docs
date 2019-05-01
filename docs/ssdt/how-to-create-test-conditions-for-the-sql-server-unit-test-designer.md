---
title: "How to: Create Test Conditions for the SQL Server Unit Test Designer | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 48076062-1ef5-419a-8a55-3c7b4234cc35
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# How to: Create Test Conditions for the SQL Server Unit Test Designer
You can use the extensible [TestCondition](https://msdn.microsoft.com/library/microsoft.data.tools.schema.sql.unittesting.conditions.testcondition(v=vs.103).aspx) class to create new test conditions. For example, you might create a new test condition that verifies the number of columns or the values in a result set.  
  
## To create a test condition  
This procedure explains how to create a test condition to appear in the SQL Server Unit Test Designer.  
  
1.  In Visual Studio, create a class library project.  
  
2.  On the **Project** menu, click **Add Reference**.  
  
3.  Click the **.NET** tab.  
  
4.  In the **Component Name** list, select **System.ComponentModel.Composition** and then click **OK**.  
  
5.  Add the required assembly references. Right-click the project node and then click **Add Reference**. Click **Browse** and navigate to the C:\Program Files (x86)\\MicrosoftSQL Server\110\DAC\Bin folder. Choose Microsoft.Data.Tools.Schema.Sql.dll and click Add, then click OK.  
  
6.  On the **Project** menu, click **Unload Project**.  
  
7.  Right-click on the project in **Solution Explorer** and choose **Edit <project name>.csproj**.  
  
8.  Add the following Import statements after the import of Microsoft.CSharp.targets:  
  
    ```  
    <Import Project="$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v10.0\SSDT\Microsoft.Data.Tools.Schema.Sql.UnitTesting.targets" Condition="'$(VisualStudioVersion)' == ''" />  
    <Import Project="$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v$(VisualStudioVersion)\SSDT\Microsoft.Data.Tools.Schema.Sql.UnitTesting.targets" Condition="'$(VisualStudioVersion)' != ''" />  
    ```  
  
9. Save the file and close it. Right-click on the project in **Solution Explorer** and choose **Reload Project**.  
  
10. Derive your class from the [TestCondition](https://msdn.microsoft.com/library/microsoft.data.tools.schema.sql.unittesting.conditions.testcondition(v=vs.103).aspx) class.  
  
11. Sign the assembly with a strong name. For more information, see [How to: Sign an Assembly with a Strong Name](https://msdn.microsoft.com/library/xc31ft41.aspx).  
  
12. Build the class library.  
  
13. Before you can use the new test condition, you must copy your signed assembly to the %Program Files%\Microsoft Visual Studio <Version>\Common7\IDE\Extensions\Microsoft\SQLDB\TestConditions folder. If this folder does not exist, create it. You need administrative privileges on your machine to copy to this directory.  
  
14. Install the test condition. For more information, see [Custom Test Conditions  for SQL Server Unit Tests](../ssdt/custom-test-conditions-for-sql-server-unit-tests.md).  
  
15. Add a new SQL Server unit test to the project to create a reference to the test condition to be added to the project. You can manually add a reference to the test condition assembly in the project. Reload the designer after this step.  
  
    > [!NOTE]  
    > A test class must be added to create the reference. You can delete the test class after the reference is added.  
  
In the following example, you create a simple test condition that verifies that the number of columns returned in the ResultSet. You can use this simple test condition to make sure that the contract for a stored procedure is correct.  
  
```  
using System;  
using System.ComponentModel;  
using System.Data;  
using System.Data.Common;  
using Microsoft.Data.Tools.Schema.Sql.UnitTesting;  
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;  
  
namespace Ssdt.Samples.SqlUnitTesting  
{  
  
    [ExportTestCondition("ResultSet Column Count", typeof(ResultSetColumnCountCondition))]  
    public class ResultSetColumnCountCondition : TestCondition  
    {  
        private int _resultSet;  
        private int _count;  
        private int _batch;  
  
        public ResultSetColumnCountCondition()  
        {  
            _resultSet = 1;  
            _count = 0;  
            _batch = 1;  
        }  
  
        // method you need to override  
        // to perform the condition verification  
        public override void Assert(DbConnection validationConnection, SqlExecutionResult[] results)  
        {  
            // call base for parameter validation  
            base.Assert(validationConnection, results);  
  
            // verify batch exists  
            if (results.Length < _batch)  
                throw new DataException(String.Format("Batch {0} does not exist", _batch));  
  
            SqlExecutionResult result = results[_batch - 1];  
  
            // verify resultset exists  
            if (result.DataSet.Tables.Count < ResultSet)  
                throw new DataException(String.Format("ResultSet {0} does not exist", ResultSet));  
  
            DataTable table = result.DataSet.Tables[ResultSet - 1];  
  
            // actual condition verification  
            // verify resultset column count matches expected  
            if (table.Columns.Count != Count)  
                throw new DataException(String.Format(  
                    "ResultSet {0}: {1} columns did not match the {2} columns expected",  
                    ResultSet, table.Columns.Count, Count));  
        }  
  
        // this method is called to provide the string shown in the  
        // test conditions panel grid describing what the condition tests  
        public override string ToString()  
        {  
            return String.Format(  
                "Condition fails if ResultSet {0} does not contain {1} columns",  
                ResultSet, Count);  
        }  
  
        // below are the test condition properties  
        // that are exposed to the user in the property browser  
        #region Properties  
  
        // property specifying the resultset for which  
        // you want to check the column count  
        [Category("Test Condition")]  
        [DisplayName("ResultSet")]  
        [Description("ResultSet Number")]  
        public int ResultSet  
        {  
            get { return _resultSet; }  
  
            set  
            {  
                //basic validation  
                if (value < 1)  
                    throw new ArgumentException("ResultSet cannot be less than 1");  
  
                _resultSet = value;  
            }  
        }  
  
        // property specifying  
        // expected column count  
        [Category("Test Condition")]  
        [DisplayName("Count")]  
        [Description("Column Count")]  
        public int Count  
        {  
            get { return _count; }  
  
            set  
            {  
                //basic validation  
                if (value < 0)  
                    throw new ArgumentException("Count cannot be less than 0");  
  
                _count = value;  
            }  
        }  
  
        #endregion  
    }  
}  
```  
  
The class for the custom test condition inherits from the base [TestCondition](https://msdn.microsoft.com/library/microsoft.data.tools.schema.sql.unittesting.conditions.testcondition(v=vs.103).aspx) class. Because of the additional properties on the custom test condition, users can configure the condition from the Properties window after they have installed the condition.  
  
[ExportTestConditionAttribute](https://msdn.microsoft.com/library/microsoft.data.tools.schema.sql.unittesting.conditions.exporttestconditionattribute(v=vs.103).aspx) must be added to classes extending [TestCondition](https://msdn.microsoft.com/library/microsoft.data.tools.schema.sql.unittesting.conditions.testcondition(v=vs.103).aspx). This attribute enables the class to be discovered by SQL Server Data Tools and used during unit test design and execution. The attribute takes two parameters:  
  
|Attribute Parameter|Position|Description|  
|-----------------------|------------|---------------|  
|DisplayName|1|Identifies the string in the "Test Conditions" combo box. This name must be unique. If two conditions have the same display name, the first condition found will be shown to the user, and a warning will be shown in the Visual Studio Error Manager.|  
|ImplementingType|2|This is used to uniquely identify the extension. You need to change this to match the type you are placing the attribute on. This example uses the type **ResultSetColumnCountCondition** so use **typeof(ResultSetColumnCountCondition)**. If your type is **NewTestCondition**, use **typeof(NewTestCondition)**.|  
  
In this example, you add two properties. Users of the custom test condition can use the ResultSet property to specify for which result set the column count should be verified. Then, users can use the Count property to specify the expected column count.  
  
Three attributes are added for each property:  
  
-   The category name, which helps organize the properties.  
  
-   The display name of the property.  
  
-   A description of the property.  
  
Validation is performed on the properties, to verify that the value of the ResultSet property is not less than one and that the value of the Count property is greater than zero.  
  
The Assert method performs the primary task of the test condition. You override the Assert method to validate that the expected condition is met. This method provides two parameters:  
  
-   The first parameter is the database connection that is used to validate the test condition.  
  
-   The second and more important parameter is the results array, which returns a single array element for each batch that was executed.  
  
Only a single batch is supported for each test script. Therefore, test conditions will always examine the first array element. The array element contains a DataSet that, in turn, contains the returned result sets for the test script. In this example, the code verifies that the data table in the DataSet contains the appropriate number of columns. For more information, see DataSet.  
  
You must set the class library that contains your test condition to be signed, which you can do in the project's properties on the Signing tab.  
  
## See Also  
[Custom Test Conditions  for SQL Server Unit Tests](../ssdt/custom-test-conditions-for-sql-server-unit-tests.md)  
  
