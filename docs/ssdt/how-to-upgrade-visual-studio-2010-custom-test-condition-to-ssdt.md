---
title: Upgrade a Visual Studio 2010 Custom Test Condition from a Previous Release
description: Find out how to upgrade a Visual Studio 2010 custom test condition for use in SQL Server Data Tools. See which changes to make and how to install the condition.
author: dzsquared
ms.author: drskwier
ms.reviewer: randolphwest
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: upgrade-and-migration-article
---

# How to: Upgrade a Visual Studio 2010 custom test condition from a previous release to SQL Server Data Tools

To use a test unit condition that you created in a version before SQL Server Data Tools, you must upgrade it.

<a id="UpdateReferences"></a>

## Update references

To update the project references:

1. For Visual Basic only, in **Solution Explorer**, select **Show All Files**.

1. In **Solution Explorer**, expand the **References** node.

1. Right-click the following assembly references and select **Remove**:

   1. Microsoft.Data.Schema.UnitTesting

   1. Microsoft.Data.Schema

1. On the **Project** menu, or by right-clicking the project folder in **Solution Explorer**, select **Add Reference**.

1. Select the **.NET** tab.

1. In the **Component Name** list, select **System.ComponentModel.Composition** and select **OK**.

1. Add the required assembly references. Right-click the project node and then select **Add Reference**. Select **Browse** and navigate to the `C:\Program Files (x86)\Microsoft SQL Server\110\DAC\Bin` folder. Choose Microsoft.Data.Tools.Schema.Sql.dll and select **Add**, then select **OK**.

1. On the **Project** menu, select **Unload Project**.

1. Right-click on the **Project** in **Solution Explorer** and choose **Edit`project_name`.csproj**.

1. Add the following Import statement after the import of `Microsoft.CSharp.targets`:

   ```xml
   <Import Project="$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v10.0\SSDT\Microsoft.Data.Tools.Schema.Sql.UnitTesting.targets" Condition="'$(VisualStudioVersion)' == ''" />

   <Import Project="$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v$(VisualStudioVersion)\SSDT\Microsoft.Data.Tools.Schema.Sql.UnitTesting.targets" Condition="'$(VisualStudioVersion)' != ''" />
   ```

1. Save the file and close it. Right-click on the project in **Solution Explorer** and choose **Reload Project**.

1. Open the test condition class and remove all using statements that begin with **Microsoft.Data.Schema**. The easiest way to do this is to right-click on the file and choose **Organize Usings** and then **Remove and Sort**. The following using statements must be removed:

   ```csharp
   using Microsoft.Data.Schema.UnitTesting;
   using Microsoft.Data.Schema.UnitTesting.Conditions;
   using Microsoft.Data.Schema.Extensibility;
   using Microsoft.Data.Schema;
   ```

1. Add the following using statements to the file if they aren't present:

   ```csharp
   using System.ComponentModel;
   using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
   using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
   ```

Your test condition now uses SQL Server unit testing assembly references.

<a id="UpdateClassAttributesandTypeReference"></a>

## Update class attributes and type references

Replace the older unit testing class attributes with a new attribute. SQL Server unit testing extensibility is now based on the Managed Extensibility Framework (MEF). You must also update some type references.

### Update class attributes

Update your code as follows:

1. Remove the `DatabaseSchemaProviderCompatibility` attributes. They were required by the previous version's extensibility feature and aren't supported in SQL Server unit testing.

1. Remove the `DisplayName` attribute. The display name is included in the new attribute.

1. Add the new `ExportTestCondition` attribute. This attribute must be present for your test condition to be discovered and used in SQL Server unit testing. `ExportTestCondition` and replaces the `DatabaseSchemaProviderCompatibility` attributes. `ExportTestCondition` takes two parameters:

   - `DisplayName` is the first parameter. This replaces the `DisplayName` attribute and is used to describe all test conditions of this type.

   - The second parameter is used to uniquely identify your extension. You can pass in your type using `typeof(NewTestCondition)`, since this should be unique. However a string ID can also be passed, if preferred.

1. Your class definition should change as follows:

   Before:

   ```csharp
   [DatabaseSchemaProviderCompatibility(typeof(DatabaseSchemaProvider))]
   [DatabaseSchemaProviderCompatibility(null)]
   [DisplayName("NewTestCondition")]
   public class NewTestCondition:TestCondition
   {
      // Additional implementation to be added here
   }
   ```

   After:

   ```csharp
   [ExportTestCondition("NewTestCondition ", typeof(NewTestCondition))]
   public class NewTestCondition:TestCondition
   {
      // Additional implementation to be added here
   }
   ```

### Update type references

Some type names have changed in the SQL Server unit testing framework. To update your code to use the new type names, use **Find and Replace** from the **Edit** menu. Type names now begin with **Sql**. Class names should be updated as follows:

| Old type name | New type name |
| --- | --- |
| `ExecutionResult` | `SqlExecutionResult` |

<a id="ApplytheNewRegistrationProcess"></a>

## Install the upgraded test condition

In previous versions of database unit testing, you might have needed to install your test condition into the global assembly cache, or to create an XML file containing your assembly information. With SQL Server unit testing, this additional process is no longer required. (For more information, see [Compiling the project and installing your test condition](../ssdt/walkthrough-use-custom-test-condition-to-verify-stored-procedure-results.md#compile-the-project-and-install-your-test-condition).

After you update your references, verify that your assembly is signed and compiled.

Next, copy the assembly file from the output directory, by default, `My Documents\Visual Studio 2010\Projects\<yoursolutionname>\<yourprojectname>\bin\Debug\`) to `%ProgramFiles%\Microsoft Visual Studio <Version>\Common7\IDE\Extensions\Microsoft\SQLDB\TestConditions` directory. When Visual Studio starts, it identifies any extensions in the TestConditions directory, and makes them available for use in the session:

## Upgrade existing tests that need to use the new test condition

Locate all test projects that use the old test condition and that need to use the new condition. Make sure these test projects are upgraded. For more information, see [Upgrade an older test project containing database unit tests](upgrade-an-older-test-project-containing-database-unit-tests.md).

Remove the assembly reference to the old test condition.

Add a new SQL Server unit test to the project to create an assembly reference to the upgraded test condition in the project. A test class must be added to create the reference. You can delete the test class after the reference is added.

## Related content

- [Custom test conditions for SQL Server unit tests](custom-test-conditions-for-sql-server-unit-tests.md)
