---
title: "How to: Upgrade a Visual Studio 2010 Custom Test Condition from a Previous Release to SQL Server Data Tools | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 44c895a3-dee0-4032-a60f-812f5fe3c713
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# How to: Upgrade a Visual Studio 2010 Custom Test Condition from a Previous Release to SQL Server Data Tools
To use a test unit condition that you created in a version prior to SQL Server Data Tools, you must upgrade it:  
  
-   [Update References](#UpdateReferences)  
  
-   [Update Class Attributes and Type References](#UpdateClassAttributesandTypeReference)  
  
-   [Install the Upgraded Test Condition](#ApplytheNewRegistrationProcess)  
  
## <a name="UpdateReferences"></a>Update References  
To update the project references:  
  
1.  Visual Basic only, in **Solution Explorer**, click **Show All Files**.  
  
2.  In **Solution Explorer**, expand the **References** node.  
  
3.  Right-click the following assembly references and click **Remove**:  
  
    1.  Microsoft.Data.Schema.UnitTesting  
  
    2.  Microsoft.Data.Schema  
  
4.  On the **Project** menu, or by right-clicking the project folder in **Solution Explorer**, click **Add Reference**.  
  
5.  Click the **.NET** tab.  
  
6.  In the **Component Name** list, select **System.ComponentModel.Composition** and click **OK**.  
  
7.  Add the required assembly references. Right-click the project node and then click **Add Reference**. Click **Browse** and navigate to the C:\Program Files (x86)\\MicrosoftSQL Server\110\DAC\Bin folder. Choose Microsoft.Data.Tools.Schema.Sql.dll and click Add, then click OK.  
  
8.  On the **Project** menu, click **Unload Project**.  
  
9. Right-click on the **Project** in **Solution Explorer** and choose **Edit**`project_name`**.csproj**.  
  
10. Add the following Import statement after the import of `Microsoft.CSharp.targets`:  
  
    ```  
    <Import Project="$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v10.0\SSDT\Microsoft.Data.Tools.Schema.Sql.UnitTesting.targets" Condition="'$(VisualStudioVersion)' == ''" />  
  
    <Import Project="$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v$(VisualStudioVersion)\SSDT\Microsoft.Data.Tools.Schema.Sql.UnitTesting.targets" Condition="'$(VisualStudioVersion)' != ''" />  
    ```  
  
11. Save the file and close it. Right-click on the project in **Solution Explorer** and choose **Reload Project**.  
  
12. Open the test condition class and remove all using statements that begin with **Microsoft.Data.Schema**. The easiest way to do this is to right-click on the file and choose **Organize Usings** and then **Remove and Sort**. The following using statements must be removed:  
  
    ```  
    using Microsoft.Data.Schema.UnitTesting;  
    using Microsoft.Data.Schema.UnitTesting.Conditions;  
    using Microsoft.Data.Schema.Extensibility;  
    using Microsoft.Data.Schema;  
    ```  
  
13. Add the following using statements to the file if they are not present:  
  
    ```  
    using System.ComponentModel;  
    using Microsoft.Data.Tools.Schema.Sql.UnitTesting;  
    using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;  
    ```  
  
Your test condition now uses SQL Server unit testing assembly references.  
  
## <a name="UpdateClassAttributesandTypeReference"></a>Update Class Attributes and Type References  
Replace the older unit testing class attributes with a new attribute. SQL Server unit testing extensibility is now based on the Managed Extensibility Framework (MEF). You must also update some type references.  
  
### Update Class Attributes  
Update your code as follows:  
  
1.  Remove the `DatabaseSchemaProviderCompatibility` attribute(s). This (these) were required by the previous version's extensibility feature and are not supported in SQL Server unit testing.  
  
2.  Remove the `DisplayName` attribute. The display name is included in the new attribute.  
  
3.  Add the new `ExportTestCondition` attribute. This attribute must be present for your test condition to be discovered and used in SQL Server unit testing. `ExportTestCondition` and replaces the `DatabaseSchemaProviderCompatibility` attribute(s). `ExportTestCondition` takes two parameters:  
  
    -   `DisplayName` is the first parameter. This replaces the `DisplayName` attribute and is used to describe all test conditions of this type.  
  
    -   The second parameter is used to uniquely identify your extension. You can simply pass in your type using `typeof(NewTestCondition)`, since this should be unique. However a string ID can also be passed, if preferred.  
  
4.  Your class definition should change as follows:  
  
    Before:  
  
    ```  
    [DatabaseSchemaProviderCompatibility(typeof(DatabaseSchemaProvider))]  
    [DatabaseSchemaProviderCompatibility(null)]  
    [DisplayName("NewTestCondition")]  
    public class NewTestCondition:TestCondition  
    {  
       // Additional implementation to be added here  
    }  
    ```  
  
    After:  
  
    ```  
    [ExportTestCondition("NewTestCondition ", typeof(NewTestCondition))]  
    public class NewTestCondition:TestCondition  
    {  
       // Additional implementation to be added here  
    }  
    ```  
  
### Update Type References  
Some type names have changed in the SQL Server unit testing framework. To update your code to use the new type names, use **Find and Replace** from the **Edit** menu. Type names now begin with **Sql**. Class names should be updated as follows:  
  
|Old Type Name|New Type Name|  
|-----------------|-----------------|  
|`ExecutionResult`|`SqlExecutionResult`|  
  
## <a name="ApplytheNewRegistrationProcess"></a>Install the Upgraded Test Condition  
In previous versions of database unit testing, you may have been required to install your test condition into the global assembly cache, or to create an XML file containing your assembly information. With SQL Server unit testing, this additional process is no longer required. (For more information, see [Compiling the project and installing your test condition](../ssdt/walkthrough-use-custom-test-condition-to-verify-stored-procedure-results.md#xxx).  
  
After you update your references, verify that your assembly is signed and compiled.  
  
Next, copy the assembly file from the output directory, by default, My Documents\Visual Studio 2010\Projects\\<yoursolutionname>\\<yourprojectname>\bin\Debug\\) to %Program Files%\Microsoft Visual Studio <Version>\Common7\IDE\Extensions\Microsoft\SQLDB\TestConditions directory. When Visual Studio starts, it will identify any extensions in the TestConditions directory and make them available for use in the session:  
  
## Upgrade Existing Tests that Need to Use the New Test Condition  
Locate all test projects that use the old test condition and that need to use the new condition. Make sure these test projects are upgraded. For more information, see [Upgrade an Older Test Project Containing Database Unit Tests](../ssdt/upgrade-an-older-test-project-containing-database-unit-tests.md).  
  
Remove the assembly reference to the old test condition.  
  
Add a new SQL Server unit test to the project to create an assembly reference to the upgraded test condition in the project. A test class must be added to create the reference. You can delete the test class after the reference is added.  
  
## See Also  
[Custom Test Conditions  for SQL Server Unit Tests](../ssdt/custom-test-conditions-for-sql-server-unit-tests.md)  
  
