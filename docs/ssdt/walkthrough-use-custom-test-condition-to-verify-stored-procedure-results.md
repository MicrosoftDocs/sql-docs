---
title: Custom Test Condition to Verify the Results of a Stored Procedure
description: Walk through the steps of setting up a custom test condition that checks whether a stored procedure returns the correct number of columns.
author: dzsquared
ms.author: drskwier
ms.reviewer: randolphwest
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
---

# Walkthrough: Using a custom test condition to verify the results of a stored procedure

In this feature extension walkthrough, you create a test condition, and you verify its functionality by creating a SQL Server unit test. The process includes creating a class library project for the test condition, and signing and installing it. If you already have a test condition that you want to update, see [How to: Upgrade a Visual Studio 2010 Custom Test Condition from a Previous Release to SQL Server Data Tools](how-to-upgrade-visual-studio-2010-custom-test-condition-to-ssdt.md).

This walkthrough illustrates the following tasks:

- How to create a test condition.
- How to sign the assembly with a strong name.
- How to add the necessary references to the project.
- How to build a test condition.
- How to install the new test condition.
- How to test the new test condition.

You must have Visual Studio 2010 or Visual Studio 2012 with the latest version of SQL Server Data Tools to complete this walkthrough. For more information, see [Install SQL Server Data Tools (SSDT) for Visual Studio](download-sql-server-data-tools-ssdt.md).

## Create a custom test condition

First, you create a class library.

1. On the **File** menu, select **New** and then select **Project**.
1. In the **New Project** dialog box, under **Project Types**, select C#.
1. Under **Templates**, select **Class Library**.
1. In the **Name** text box, type **ColumnCountCondition** and then select **OK**.

Next, sign the project.

1. On the **Project** menu, select **ColumnCountCondition Properties**.

1. On the **Signing** tab, select the **Sign the assembly** check box.

1. In the **Choose a strong name key file** box, select **\<New...>**.

   The **Create Strong Name Key** dialog box appears.

1. In the **Key file name** box, type **SampleKey**.

1. Type and confirm a password, and then select **OK**. When you build your solution, the key file is used to sign the assembly.

1. On the **File** menu, select **Save All**.

1. On the **Build** menu, select **Build Solution**.

Next, you add the necessary references to the project.

1. In **Solution Explorer**, select the **ColumnCountCondition** project.

1. On the **Project** menu, select **Add Reference** to display the **Add Reference** dialog box.

1. Select the **.NET** tab.

1. In the **Component Name** column, locate and select the **System.ComponentModel.Composition** component. Select **OK** after you select the component.

1. Add the required assembly references. Right-click the project node and then select **Add Reference**. Select **Browse** and navigate to the `C:\Program Files (x86)\Microsoft SQL Server\110\DAC\Bin` folder. Choose `Microsoft.Data.Tools.Schema.Sql.dll` and select **Add**, then select **OK**.

1. On the **Project** menu, select **Unload Project**.

1. Right-click on the project in **Solution Explorer** and choose **Edit \<project name\>.csproj**.

1. Add the following Import statement after the import of **Microsoft.CSharp.targets**:

   ```xml
   <Import Project="$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v10.0\SSDT\Microsoft.Data.Tools.Schema.Sql.UnitTesting.targets" Condition="'$(VisualStudioVersion)' == ''" />

   <Import Project="$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v$(VisualStudioVersion)\SSDT\Microsoft.Data.Tools.Schema.Sql.UnitTesting.targets" Condition="'$(VisualStudioVersion)' != ''" />
   ```

1. Save the file and close it. Right-click on the project in **Solution Explorer** and choose **Reload Project**.

   The required references are shown under the **References** node of the project in **Solution Explorer**.

## Create the ResultSetColumnCountCondition class

Now, you rename **Class1** to **ResultSetColumnCountCondition** and derive it from [testcondition](/previous-versions/sql/sql-server-data-tools/jj856583(v=vs.103)). The **ResultSetColumnCountCondition** class is a simple test condition that verifies that the number of columns returned in the ResultSet. You can use this condition to make sure that the contract for a stored procedure is correct.

1. In **Solution Explorer**, right-click Class1.cs, select **Rename**, and type **ResultSetColumnCountCondition.cs**.

1. Select **Yes** to confirm renaming all the references to Class1.

1. Open the **ResultSetColumnCountCondition.cs** file and add the following using statements to the file:

   ```csharp
   using System;
   using System.ComponentModel;
   using System.Data;
   using System.Data.Common;
   using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
   using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;

   namespace ColumnCountCondition {
       public class ResultSetColumnCountCondition
   ```

1. Derive the class from [testcondition](/previous-versions/sql/sql-server-data-tools/jj856583(v=vs.103)):

   ```csharp
   public class ResultSetColumnCountCondition : TestCondition
   ```

1. Add [ExportTestConditionAttribute](/previous-versions/sql/sql-server-data-tools/jj856578(v=vs.103)). See [How to: Create Test Conditions for the SQL Server Unit Test Designer](how-to-create-test-conditions-for-the-sql-server-unit-test-designer.md) for more information about UnitTesting.Conditions.ExportTestConditionAttribute.

   ```csharp
   [ExportTestCondition("ResultSet Column Count", typeof(ResultSetColumnCountCondition))]
       public class ResultSetColumnCountCondition : TestCondition
   ```

1. Create the member variables and the constructor:

   ```csharp
           private int _resultSet;
           private int _count;
           private int _batch;

           public ResultSetColumnCountCondition() {
               _resultSet = 1;
               _count = 0;
               _batch = 1;
           }
   ```

1. Override the **Assert** method. The method includes arguments for **IDbConnection**, which represents the connection to the database, and **SqlExecutionResult**. The method uses **DataSchemaException** for error handling:

   ```csharp
          //method you need to override
           //to perform the condition verification
           public override void Assert(DbConnection validationConnection, SqlExecutionResult[] results)
           {
               //call base for parameter validation
               base.Assert(validationConnection, results);

               //verify batch exists
               if (results.Length < _batch)
                   throw new DataException(String.Format("Batch {0} does not exist", _batch));

               SqlExecutionResult result = results[_batch - 1];

               //verify resultset exists
               if (result.DataSet.Tables.Count < ResultSet)
                   throw new DataException(String.Format("ResultSet {0} does not exist", ResultSet));

               DataTable table = result.DataSet.Tables[ResultSet - 1];

               //actual condition verification
               //verify resultset column count matches expected
               if (table.Columns.Count != Count)
                   throw new DataException(String.Format(
                       "ResultSet {0}: {1} columns did not match the {2} columns expected",
                       ResultSet, table.Columns.Count, Count));
           }
   ```

   Add the following method, which overrides the ToString method:

   ```csharp
           //this method is called to provide the string shown in the
           //test conditions panel grid describing what the condition tests
           public override string ToString()
           {
               return String.Format(
                   "Condition fails if ResultSet {0} does not contain {1} columns",
                   ResultSet, Count);
           }
   ```

1. Add the following test condition properties by using the **CategoryAttribute**, **DisplayNameAttribute**, and **DescriptionAttribute** attributes:

   ```csharp
           //below are the test condition properties
           //that are exposed to the user in the property browser
           //property specifying the resultset for which
           //you want to check the column count
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

           //property specifying
           //expected column count
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
   ```

The final code listing is:

```csharp
using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;

namespace ColumnCountCondition
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

        //method you need to override
        //to perform the condition verification
        public override void Assert(DbConnection validationConnection, SqlExecutionResult[] results)
        {
            //call base for parameter validation
            base.Assert(validationConnection, results);

            //verify batch exists
            if (results.Length < _batch)
                throw new DataException(String.Format("Batch {0} does not exist", _batch));

            SqlExecutionResult result = results[_batch - 1];

            //verify resultset exists
            if (result.DataSet.Tables.Count < ResultSet)
                throw new DataException(String.Format("ResultSet {0} does not exist", ResultSet));

            DataTable table = result.DataSet.Tables[ResultSet - 1];

            //actual condition verification
            //verify resultset column count matches expected
            if (table.Columns.Count != Count)
                throw new DataException(String.Format(
                    "ResultSet {0}: {1} columns did not match the {2} columns expected",
                    ResultSet, table.Columns.Count, Count));
        }

        //this method is called to provide the string shown in the
        //test conditions panel grid describing what the condition tests
        public override string ToString()
        {
            return String.Format(
                "Condition fails if ResultSet {0} does not contain {1} columns",
                ResultSet, Count);
        }

        //below are the test condition properties
        //that are exposed to the user in the property browser
        //property specifying the resultset for which
        //you want to check the column count
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

        //property specifying
        //expected column count
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
    }
}
```

Next, we build the project.

## Compile the project and install your test condition

On the **Build** menu, select **Build Solution**.

Next, you copy the assembly information to the Extensions directory. When Visual Studio starts, it identifies any extensions in the `%ProgramFiles%\Microsoft Visual Studio <Version>\Common7\IDE\Extensions\Microsoft\SQLDB\TestConditions` directory and subdirectories, and makes them available for use:

Copy the `ColumnCountCondition.dll` assembly file from the output directory to the `%ProgramFiles%\Microsoft Visual Studio <Version>\Common7\IDE\Extensions\Microsoft\SQLDB\TestConditions` directory.

By default, the path of your compiled `.dll` file is `<YourSolutionPath>\<YourProjectPath>\bin\Debug` or `<YourSolutionPath>\<YourProjectPath>\bin\Release`.

Next, you start a new session of Visual Studio and create a database project. To start a new Visual Studio session and create a database project:

1. Start a second session of Visual Studio.

1. On the **File** menu, select **New** and then select **Project**.

1. In the **New Project** dialog box, in the list of installed templates, select the **SQL Server** node.

1. In the details pane, select **SQL Server Database Project**.

1. In the **Name** text box, type **SampleConditionDB** and then select **OK**.

Next, we need to create a unit test. To create a SQL Server unit test inside a new test class:

1. On the **Test** menu, select **New Test** to display the **Add New Test** dialog box.

   You can also open **Solution Explorer**, right-click a test project, point to **Add**, and then select **New Test**.

1. In the templates list, select **SQL Server Unit Test**.

1. In **Test Name**, type **SampleUnitTest**.

1. In **Add to Test Project**, select **Create a new C# test project**. Then, select **OK** to display the **New Test Project** dialog box.

1. Type **SampleUnitTest** for the project name.

1. Select **Cancel** to create the unit test without configuring the test project to use a database connection. Your blank test appears in the SQL Server Unit Test Designer. A C# source code file is added to the test project.

   For more information about creating and configuring database unit tests with database connections, see [How to: Create an Empty SQL Server Unit Test](how-to-create-an-empty-sql-server-unit-test.md).

1. Select **Click here to create** to finish creating the unit test. You see the new test condition displaying in the SQL Server project.

> [!NOTE]  
> To use your custom test condition with existing unit test projects, at least one new SQL Server unit test class must be created. The required reference to your test condition assembly is added to your test project during test class creation.

To view the new test condition:

1. In the **SQL Server Unit Test Designer**, under **Test Conditions**, under the **Name** column, select the `inconclusiveCondition1` test.

1. Select the **Delete Test Condition** toolbar button to remove the `inconclusiveCondition1` test.

1. Select the **Test Conditions** dropdown list and select **ResultSet Column Count**.

1. Select the **Add Test Condition** toolbar button to add your custom test condition.

1. In the **Properties** window, configure the Count, Enabled, and ResultSet properties.

   For more information, see [How to: Add Test Conditions to SQL Server Unit Tests](how-to-add-test-conditions-to-sql-server-unit-tests.md).

## Related content

- [Custom test conditions for SQL Server unit tests](custom-test-conditions-for-sql-server-unit-tests.md)
