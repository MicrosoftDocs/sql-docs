---
title: Author custom code analysis rules
description: "How to create custom code analysis rules with DacFx and ScriptDOM."
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan, randolphwest
ms.date: 08/30/2024
ms.service: sql
ms.topic: how-to
zone_pivot_groups: sq1-sql-projects-tools
---

# Author custom code analysis rules

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This walkthrough demonstrates the steps used to create a SQL Server code analysis rule. The rule created in this walkthrough is used to avoid `WAITFOR DELAY` statements in stored procedures, triggers, and functions.

In this walkthrough, you create a custom rule for Transact-SQL static code analysis by using the following steps:

1. Create a class library project, enable signing for that project, and add the necessary references.
2. Create two helper [!INCLUDE [c-sharp-md](../../../includes/c-sharp-md.md)] classes.
3. Create a [!INCLUDE [c-sharp-md](../../../includes/c-sharp-md.md)] custom rule class.
4. Build the class library project.
5. Install and test the new code analysis rule.

Except for the Visual Studio (SQL Server Data Tools) instructions, the guide focuses on SDK-style SQL projects.

## Prerequisites

You need the following components to complete this walkthrough:

::: zone pivot="sq1-visual-studio"

- A version of Visual Studio installed, which includes SQL Server Data Tools, and supports [!INCLUDE [c-sharp-md](../../../includes/c-sharp-md.md)] .NET Framework development.
- A SQL Server project that contains SQL Server objects.
- An instance of SQL Server to which you can deploy a database project.

This walkthrough is intended for users who are already familiar with the SQL Server features of SQL Server Data Tools. You should be familiar with Visual Studio concepts, such as how to create a class library, add NuGet packages, and how to use the code editor to add code to a class.

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

- A version of Visual Studio Code installed, which includes the SQL Database Projects extension.
- A SQL database project that contains SQL objects.
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Recommended: [C# Dev Kit extension](https://code.visualstudio.com/docs/csharp/get-started) for VS Code

This walkthrough is intended for users who are already familiar with the SQL Database Projects extension in Visual Studio Code. You should be familiar with development concepts, such as how to create a class library, add packages, and how to use the code editor to edit code.

::: zone-end

::: zone pivot="sq1-command-line"

- A text editor, such as the file editor in Visual Studio Code.
- A SQL database project that contains SQL objects.
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

This walkthrough is intended for users who are already familiar with SQL projects. You should be familiar with development concepts, such as how to create a class library, add packages, and how to use the code editor to edit code.

::: zone-end

## Step 1: Create a class library project

::: zone pivot="sq1-visual-studio"

First create a class library. To create a class library project:

1. Create a [!INCLUDE [c-sharp-md](../../../includes/c-sharp-md.md)] (.NET Framework) class library project named `SampleRules`.

2. Rename the file `Class1.cs` to `AvoidWaitForDelayRule.cs`.

3. In Solution Explorer, right-click the project node and then select **Add** then **Reference**.

4. Select `System.ComponentModel.Composition` on the **Assemblies\Frameworks** tab.

5. In Solution Explorer, right-click the project node and then select **Manage NuGet Packages**. Locate and install the `Microsoft.SqlServer.DacFx` NuGet package. The selected version must be `162.x.x` (for example `162.2.111`) with Visual Studio 2022.

Next, add supporting classes that will be used by the rule.

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

1. Start Visual Studio Code and open the folder where you want to create the project.
2. Open a **Terminal** window in Visual Studio Code by selecting the **View** menu, then **Terminal**.
3. In the **Terminal**, enter the following commands to create a new solution and project:

    ```bash
    dotnet new sln
    dotnet new classlib -n SampleRules -o SampleRules
    dotnet sln add SampleRules/SampleRules.csproj
    ```

4. Change to the `SampleRules` directory:

    ```bash
    cd SampleRules
    ```

5. Add the required NuGet package:

    ```bash
    dotnet add package Microsoft.SqlServer.DacFx
    ```

Next, add supporting classes that will be used by the rule.

::: zone-end

::: zone pivot="sq1-command-line"

1. Open a command prompt or terminal window and navigate to the folder where you want to create the project.
2. In the **Terminal**, enter the following commands to create a new solution and project:

    ```bash
    dotnet new sln
    dotnet new classlib -n SampleRules -o SampleRules
    dotnet sln add SampleRules/SampleRules.csproj
    ```

3. Change to the `SampleRules` directory:

    ```bash
    cd SampleRules
    ```

4. Add the required NuGet package:

    ```bash
    dotnet add package Microsoft.SqlServer.DacFx
    ```

::: zone-end

## Step 2. Create custom rule helper classes

Before you create the class for the rule itself, add a visitor class and an attribute class to the project. These classes might be useful for creating more custom rules.

### Define the WaitForDelayVisitor class

The first class that you must define is the `WaitForDelayVisitor` class, derived from [TSqlConcreteFragmentVisitor](/dotnet/api/microsoft.sqlserver.transactsql.scriptdom.tsqlconcretefragmentvisitor). This class provides access to the `WAITFOR DELAY` statements in the model. Visitor classes make use of the [ScriptDom](/dotnet/api/microsoft.sqlserver.transactsql.scriptdom) APIs provided by SQL Server. In this API, Transact-SQL code is represented as an abstract syntax tree (AST) and visitor classes can be useful when you wish to find specific syntax objects such as `WAITFOR DELAY` statements. These statements might be difficult to find using the object model since they're not associated to a specific object property or relationship, but you can find them using the visitor pattern and the [ScriptDom](/dotnet/api/microsoft.sqlserver.transactsql.scriptdom) API.

::: zone pivot="sq1-visual-studio"

1. In **Solution Explorer**, select the `SampleRules` project.

2. On the **Project** menu, select **Add Class**. The **Add New Item** dialog box appears. In the **Name** text box, type `WaitForDelayVisitor.cs` and then select the **Add** button. The `WaitForDelayVisitor.cs` file is added to the project in **Solution Explorer**.

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

1. Open the **Explorer** view in Visual Studio Code.

2. Create a new file named `WaitForDelayVisitor.cs` in the `SampleRules` folder.

::: zone-end

::: zone pivot="sq1-command-line"

1. Navigate to the `SampleRules` directory.
2. Create a new file named `WaitForDelayVisitor.cs`.

::: zone-end

3. Open the `WaitForDelayVisitor.cs` file and update the contents to match the following code:

    ```csharp
    using System.Collections.Generic;
    using Microsoft.SqlServer.TransactSql.ScriptDom;
    namespace SampleRules {
        class WaitForDelayVistor {}
    }
    ```

4. In the class declaration, change the access modifier to internal and derive the class from `TSqlConcreteFragmentVisitor`:

    ```csharp
    internal class WaitForDelayVisitor : TSqlConcreteFragmentVisitor {}
    ```

5. Add the following code to define the List member variable:

    ```csharp
    public IList<WaitForStatement> WaitForDelayStatements { get; private set; }
    ```

6. Define the class constructor by adding the following code:

    ```csharp
    public WaitForDelayVisitor() {
       WaitForDelayStatements = new List<WaitForStatement>();
    }
    ```

7. Override the `ExplicitVisit` method by adding the following code:

    ```csharp
    public override void ExplicitVisit(WaitForStatement node) {
       // We are only interested in WAITFOR DELAY occurrences
       if (node.WaitForOption == WaitForOption.Delay)
          WaitForDelayStatements.Add(node);
    }
    ```

    This method visits the `WAITFOR` statements in the model, and adds statements that have the `DELAY` option specified to the list of `WAITFOR DELAY` statements. The key class referenced is [WaitForStatement](/dotnet/api/microsoft.sqlserver.transactsql.scriptdom.waitforstatement).

8. On the **File** menu, select **Save**.

### Define the LocalizedExportCodeAnalysisRuleAttribute class

The second class is `LocalizedExportCodeAnalysisRuleAttribute.cs`. This is an extension of the built-in `Microsoft.SqlServer.Dac.CodeAnalysis.ExportCodeAnalysisRuleAttribute` provided by the framework, and supports reading the `DisplayName` and `Description` used by your rule from a resources file. This is a useful class if you ever intend to have your rules used in multiple languages.

::: zone pivot="sq1-visual-studio"

9. In **Solution Explorer**, select the `SampleRules` project.

10. On the **Project** menu, select **Add Class**. The **Add New Item** dialog box appears. In the **Name** text box, type `LocalizedExportCodeAnalysisRuleAttribute.cs` and then select the **Add** button. The file is added to the project in **Solution Explorer**.

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

9. Navigate to the `SampleRules` directory in the **Explorer** view in Visual Studio Code.
10. Create a new file named `LocalizedExportCodeAnalysisRuleAttribute.cs`.

::: zone-end

::: zone pivot="sq1-command-line"

9. Navigate to the `SampleRules` directory.
10. Create a new file named `LocalizedExportCodeAnalysisRuleAttribute.cs`.

::: zone-end

11. Open the file and update the contents to match the following code:

    ```csharp
    using Microsoft.SqlServer.Dac.CodeAnalysis;
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Resources;

    namespace SampleRules
    {

        internal class LocalizedExportCodeAnalysisRuleAttribute : ExportCodeAnalysisRuleAttribute
        {
            private readonly string _resourceBaseName;
            private readonly string _displayNameResourceId;
            private readonly string _descriptionResourceId;

            private ResourceManager _resourceManager;
            private string _displayName;
            private string _descriptionValue;

            /// <summary>
            /// Creates the attribute, with the specified rule ID, the fully qualified
            /// name of the resource file that will be used for looking up display name
            /// and description, and the Ids of those resources inside the resource file.
            /// </summary>
            public LocalizedExportCodeAnalysisRuleAttribute(
                string id,
                string resourceBaseName,
                string displayNameResourceId,
                string descriptionResourceId)
                : base(id, null)
            {
                _resourceBaseName = resourceBaseName;
                _displayNameResourceId = displayNameResourceId;
                _descriptionResourceId = descriptionResourceId;
            }

            /// <summary>
            /// Rules in a different assembly would need to overwrite this
            /// </summary>
            /// <returns></returns>
            protected virtual Assembly GetAssembly()
            {
                return GetType().Assembly;
            }

            private void EnsureResourceManagerInitialized()
            {
                var resourceAssembly = GetAssembly();

                try
                {
                    _resourceManager = new ResourceManager(_resourceBaseName, resourceAssembly);
                }
                catch (Exception ex)
                {
                    var msg = String.Format(CultureInfo.CurrentCulture, RuleResources.CannotCreateResourceManager, _resourceBaseName, resourceAssembly);
                    throw new RuleException(msg, ex);
                }
            }

            private string GetResourceString(string resourceId)
            {
                EnsureResourceManagerInitialized();
                return _resourceManager.GetString(resourceId, CultureInfo.CurrentUICulture);
            }

            /// <summary>
            /// Overrides the standard DisplayName and looks up its value inside a resources file
            /// </summary>
            public override string DisplayName
            {
                get
                {
                    if (_displayName == null)
                    {
                        _displayName = GetResourceString(_displayNameResourceId);
                    }
                    return _displayName;
                }
            }

            /// <summary>
            /// Overrides the standard Description and looks up its value inside a resources file
            /// </summary>
            public override string Description
            {
                get
                {
                    if (_descriptionValue == null)
                    {
                        _descriptionValue = GetResourceString(_descriptionResourceId);
                    }
                    return _descriptionValue;
                }
            }
        }
    }
    ```

### Add a resource file and three resource strings

Next, add a resource file that defines the rule name, rule description, and the category in which the rule will appear in the rule configuration interface.

::: zone pivot="sq1-visual-studio"

12. In **Solution Explorer**, select the `SampleRules` project. On the **Project** menu, select **Add** then **New Item**. The **Add New Item** dialog box appears.

13. In the list of **Installed Templates**, select **General**. In the details pane, select **Resources File**.

14. In **Name**, type `RuleResources.resx`. The resource editor appears, with no resources defined.

15. Define four resource strings as follows:

    | Name | Value |
    | --- | --- |
    | `AvoidWaitForDelay_ProblemDescription` | `WAITFOR DELAY statement was found in {0}.` |
    | `AvoidWaitForDelay_RuleName` | `Avoid using WaitFor Delay statements in stored procedures, functions and triggers.` |
    | `CategorySamples` | `SamplesCategory` |
    | `CannotCreateResourceManager` | `Can't create ResourceManager for {0} from {1}.` |

16. On the **File** menu, select **Save RuleResources.resx**.

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

12. In the `SampleRules` directory, create a new file named `RuleResources.resx`.
13. Open the `RuleResources.resx` file and add the following code:

    ```xml
    <?xml version="1.0" encoding="utf-8"?>
    <root>
      <xsd:schema id="root" xmlns="" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
        <xsd:import namespace="http://www.w3.org/XML/1998/namespace" />
        <xsd:element name="root" msdata:IsDataSet="true">
          <xsd:complexType>
            <xsd:choice maxOccurs="unbounded">
              <xsd:element name="metadata">
                <xsd:complexType>
                  <xsd:sequence>
                    <xsd:element name="value" type="xsd:string" minOccurs="0" />
                  </xsd:sequence>
                  <xsd:attribute name="name" use="required" type="xsd:string" />
                  <xsd:attribute name="type" type="xsd:string" />
                  <xsd:attribute name="mimetype" type="xsd:string" />
                  <xsd:attribute ref="xml:space" />
                </xsd:complexType>
              </xsd:element>
              <xsd:element name="assembly">
                <xsd:complexType>
                  <xsd:attribute name="alias" type="xsd:string" />
                  <xsd:attribute name="name" type="xsd:string" />
                </xsd:complexType>
              </xsd:element>
              <xsd:element name="data">
                <xsd:complexType>
                  <xsd:sequence>
                    <xsd:element name="value" type="xsd:string" minOccurs="0" msdata:Ordinal="1" />
                    <xsd:element name="comment" type="xsd:string" minOccurs="0" msdata:Ordinal="2" />
                  </xsd:sequence>
                  <xsd:attribute name="name" type="xsd:string" use="required" msdata:Ordinal="1" />
                  <xsd:attribute name="type" type="xsd:string" msdata:Ordinal="3" />
                  <xsd:attribute name="mimetype" type="xsd:string" msdata:Ordinal="4" />
                  <xsd:attribute ref="xml:space" />
                </xsd:complexType>
              </xsd:element>
              <xsd:element name="resheader">
                <xsd:complexType>
                  <xsd:sequence>
                    <xsd:element name="value" type="xsd:string" minOccurs="0" msdata:Ordinal="1" />
                  </xsd:sequence>
                  <xsd:attribute name="name" type="xsd:string" use="required" />
                </xsd:complexType>
              </xsd:element>
            </xsd:choice>
          </xsd:complexType>
        </xsd:element>
      </xsd:schema>
      <resheader name="resmimetype">
        <value>text/microsoft-resx</value>
      </resheader>
      <resheader name="version">
        <value>2.0</value>
      </resheader>
      <resheader name="reader">
        <value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
      </resheader>
      <resheader name="writer">
        <value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
      </resheader>
      <data name="AvoidWaitForDelay_ProblemDescription" xml:space="preserve">
        <value>WAITFOR DELAY statement was found in {0}</value>
      </data>
      <data name="AvoidWaitFormDelay_RuleName" xml:space="preserve">
        <value>Avoid using WaitFor Delay statements in stored procedures, functions and triggers.</value>
      </data>
      <data name="CategorySamples" xml:space="preserve">
        <value>SamplesCategory</value>
      </data>
      <data name="CannotCreateResourceManager" xml:space="preserve">
        <value>Can't create ResourceManager for {0} from {1}</value>
      </data>
    </root>
    ```

14. Save the `RuleResources.resx` file.

15. Open the `SampleRules.csproj` file and add the following code to update and include the resource contents in the project:

    ```xml
    <ItemGroup>
      <Compile Update="RuleResources.Designer.cs">
        <DesignTime>True</DesignTime>
        <AutoGen>True</AutoGen>
        <DependentUpon>RuleResources.resx</DependentUpon>
      </Compile>
    </ItemGroup>
    <ItemGroup>
      <EmbeddedResource Include="RuleResources.resx">
        <Generator>PublicResXFileCodeGenerator</Generator>
        <LastGenOutput>RuleResources.Designer.cs</LastGenOutput>
      </EmbeddedResource>
    </ItemGroup>
    ```

16. Save the `SampleRules.csproj` file.

::: zone-end

::: zone pivot="sq1-command-line"

12. In the `SampleRules` directory, create a new file named `RuleResources.resx`.
13. Open the `RuleResources.resx` file and add the following code:

    ```xml
    <?xml version="1.0" encoding="utf-8"?>
    <root>
      <xsd:schema id="root" xmlns="" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
        <xsd:import namespace="http://www.w3.org/XML/1998/namespace" />
        <xsd:element name="root" msdata:IsDataSet="true">
          <xsd:complexType>
            <xsd:choice maxOccurs="unbounded">
              <xsd:element name="metadata">
                <xsd:complexType>
                  <xsd:sequence>
                    <xsd:element name="value" type="xsd:string" minOccurs="0" />
                  </xsd:sequence>
                  <xsd:attribute name="name" use="required" type="xsd:string" />
                  <xsd:attribute name="type" type="xsd:string" />
                  <xsd:attribute name="mimetype" type="xsd:string" />
                  <xsd:attribute ref="xml:space" />
                </xsd:complexType>
              </xsd:element>
              <xsd:element name="assembly">
                <xsd:complexType>
                  <xsd:attribute name="alias" type="xsd:string" />
                  <xsd:attribute name="name" type="xsd:string" />
                </xsd:complexType>
              </xsd:element>
              <xsd:element name="data">
                <xsd:complexType>
                  <xsd:sequence>
                    <xsd:element name="value" type="xsd:string" minOccurs="0" msdata:Ordinal="1" />
                    <xsd:element name="comment" type="xsd:string" minOccurs="0" msdata:Ordinal="2" />
                  </xsd:sequence>
                  <xsd:attribute name="name" type="xsd:string" use="required" msdata:Ordinal="1" />
                  <xsd:attribute name="type" type="xsd:string" msdata:Ordinal="3" />
                  <xsd:attribute name="mimetype" type="xsd:string" msdata:Ordinal="4" />
                  <xsd:attribute ref="xml:space" />
                </xsd:complexType>
              </xsd:element>
              <xsd:element name="resheader">
                <xsd:complexType>
                  <xsd:sequence>
                    <xsd:element name="value" type="xsd:string" minOccurs="0" msdata:Ordinal="1" />
                  </xsd:sequence>
                  <xsd:attribute name="name" type="xsd:string" use="required" />
                </xsd:complexType>
              </xsd:element>
            </xsd:choice>
          </xsd:complexType>
        </xsd:element>
      </xsd:schema>
      <resheader name="resmimetype">
        <value>text/microsoft-resx</value>
      </resheader>
      <resheader name="version">
        <value>2.0</value>
      </resheader>
      <resheader name="reader">
        <value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
      </resheader>
      <resheader name="writer">
        <value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
      </resheader>
      <data name="AvoidWaitForDelay_ProblemDescription" xml:space="preserve">
        <value>WAITFOR DELAY statement was found in {0}</value>
      </data>
      <data name="AvoidWaitFormDelay_RuleName" xml:space="preserve">
        <value>Avoid using WaitFor Delay statements in stored procedures, functions and triggers.</value>
      </data>
      <data name="CategorySamples" xml:space="preserve">
        <value>SamplesCategory</value>
      </data>
      <data name="CannotCreateResourceManager" xml:space="preserve">
        <value>Can't create ResourceManager for {0} from {1}</value>
      </data>
    </root>
    ```

14. Save the `RuleResources.resx` file.

15. Open the `SampleRules.csproj` file and add the following code to update and include the resource contents in the project:

    ```xml
    <ItemGroup>
      <Compile Update="RuleResources.Designer.cs">
        <DesignTime>True</DesignTime>
        <AutoGen>True</AutoGen>
        <DependentUpon>RuleResources.resx</DependentUpon>
      </Compile>
    </ItemGroup>
    <ItemGroup>
      <EmbeddedResource Include="RuleResources.resx">
        <Generator>PublicResXFileCodeGenerator</Generator>
        <LastGenOutput>RuleResources.Designer.cs</LastGenOutput>
      </EmbeddedResource>
    </ItemGroup>
    ```

16. Save the `SampleRules.csproj` file.

::: zone-end

### Define the SampleConstants class

Next, define a class that references the resources in the resource file that are used by Visual Studio to display information about your rule in the user interface.

::: zone pivot="sq1-visual-studio"

17. In **Solution Explorer**, select the `SampleRules` project.

18. On the **Project** menu, select **Add** then **Class**. The **Add New Item** dialog box appears. In the **Name** text box, type `SampleRuleConstants.cs` and select the **Add** button. The `SampleRuleConstants.cs` file is added to the project in **Solution Explorer**.

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

17. Navigate to the `SampleRules` directory in the **Explorer** view in Visual Studio Code.
18. Create a new file named `SampleRuleConstants.cs`.

::: zone-end

::: zone pivot="sq1-command-line"

17. Navigate to the `SampleRules` directory.
18. Create a new file named `SampleRuleConstants.cs`.

::: zone-end

19. Open the `SampleRuleConstants.cs` file and add the following using statements to the file:

    ```csharp
    namespace SampleRules
    {
        internal static class RuleConstants
        {
            /// <summary>
            /// The name of the resources file to use when looking up rule resources
            /// </summary>
            public const string ResourceBaseName = "Public.Dac.Samples.Rules.RuleResources";

            /// <summary>
            /// Lookup name inside the resources file for the select asterisk rule name
            /// </summary>
            public const string AvoidWaitForDelay_RuleName = "AvoidWaitForDelay_RuleName";
            /// <summary>
            /// Lookup ID inside the resources file for the select asterisk description
            /// </summary>
            public const string AvoidWaitForDelay_ProblemDescription = "AvoidWaitForDelay_ProblemDescription";

            /// <summary>
            /// The design category (should not be localized)
            /// </summary>
            public const string CategoryDesign = "Design";

            /// <summary>
            /// The performance category (should not be localized)
            /// </summary>
            public const string CategoryPerformance = "Design";
        }
    }
    ```

20. On the **File** menu, select **Save**.

## Step 3: Create a custom rule class

After you add the helper classes that the custom code analysis rule will use, create a custom rule class and name it `AvoidWaitForDelayRule`. The `AvoidWaitForDelayRule` custom rule will be used to help database developers avoid `WAITFOR DELAY` statements in stored procedures, triggers, and functions.

### Create the AvoidWaitForDelayRule class

::: zone pivot="sq1-visual-studio"

1. In **Solution Explorer**, select the `SampleRules` project.

2. On the **Project** menu, select **Add** then **Class**. The **Add New Item** dialog box appears. In the **Name** text box, type `AvoidWaitForDelayRule.cs` and then select **Add**. The `AvoidWaitForDelayRule.cs` file is added to the project in **Solution Explorer**.

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

1. Navigate to the `SampleRules` directory in the **Explorer** view in Visual Studio Code.
2. Create a new file named `AvoidWaitForDelayRule.cs`.

::: zone-end

::: zone pivot="sq1-command-line"

1. Navigate to the `SampleRules` directory.
2. Create a new file named `AvoidWaitForDelayRule.cs`.

::: zone-end

3. Open the `AvoidWaitForDelayRule.cs` file and add the following using statements to the file:

    ```csharp
    using Microsoft.SqlServer.Dac.CodeAnalysis;
    using Microsoft.SqlServer.Dac.Model;
    using Microsoft.SqlServer.TransactSql.ScriptDom;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    namespace SampleRules {
        class AvoidWaitForDelayRule {}
    }
    ```

4. In the `AvoidWaitForDelayRule` class declaration, change the access modifier to public:

    ```csharp
    /// <summary>
    /// This is a rule that returns a warning message
    /// whenever there is a WAITFOR DELAY statement appears inside a subroutine body.
    /// This rule only applies to stored procedures, functions and triggers.
    /// </summary>
    public sealed class AvoidWaitForDelayRule
    ```

5. Derive the `AvoidWaitForDelayRule` class from the `Microsoft.SqlServer.Dac.CodeAnalysis.SqlCodeAnalysisRule` base class:

    ```csharp
    public sealed class AvoidWaitForDelayRule : SqlCodeAnalysisRule
    ```

6. Add the `LocalizedExportCodeAnalysisRuleAttribute` to your class.

    `LocalizedExportCodeAnalysisRuleAttribute` allows the code analysis service to discover custom code analysis rules. Only classes marked with an `ExportCodeAnalysisRuleAttribute` (or an attribute that inherits from this) can be used in code analysis.

    `LocalizedExportCodeAnalysisRuleAttribute` provides some required metadata used by the service. This includes a unique ID for this rule, a display name that is shown in the Visual Studio user interface, and a `Description` that can be used by your rule when identifying problems.

    ```csharp
    [LocalizedExportCodeAnalysisRule(AvoidWaitForDelayRule.RuleId,
        RuleConstants.ResourceBaseName,
        RuleConstants.AvoidWaitForDelay_RuleName,
        RuleConstants.AvoidWaitForDelay_ProblemDescription
        Category = RuleConstants.CategoryPerformance,
        RuleScope = SqlRuleScope.Element)]
    public sealed class AvoidWaitForDelayRule : SqlCodeAnalysisRule
    {
       /// <summary>
       /// The Rule ID should resemble a fully-qualified class name. In the Visual Studio UI
       /// rules are grouped by "Namespace + Category", and each rule is shown using "Short ID: DisplayName".
       /// For this rule, that means the grouping will be "Public.Dac.Samples.Performance", with the rule
       /// shown as "SR1004: Avoid using WaitFor Delay statements in stored procedures, functions and triggers."
       /// </summary>
       public const string RuleId = "RuleSamples.SR1004";
    }
    ```

    The RuleScope property should be `Microsoft.SqlServer.Dac.CodeAnalysis.SqlRuleScope.Element` as this rule analyzes specific elements. The rule is called once for each matching element in the model. If you wish to analyze an entire model then `Microsoft.SqlServer.Dac.CodeAnalysis.SqlRuleScope.Model` can be used instead.

7. Add a constructor that sets up the `Microsoft.SqlServer.Dac.CodeAnalysis.SqlAnalysisRule.SupportedElementTypes`. This is required for element-scoped rules. It defines the types of elements to which this rule applies. In this case, the rule is applied to stored procedures, triggers, and functions. The `Microsoft.SqlServer.Dac.Model.ModelSchema` class lists all available element types that can be analyzed.

    ```csharp
    public AvoidWaitForDelayRule()
    {
       // This rule supports Procedures, Functions and Triggers. Only those objects will be passed to the Analyze method
       SupportedElementTypes = new[]
       {
          // Note: can use the ModelSchema definitions, or access the TypeClass for any of these types
          ModelSchema.ExtendedProcedure,
          ModelSchema.Procedure,
          ModelSchema.TableValuedFunction,
          ModelSchema.ScalarFunction,

          ModelSchema.DatabaseDdlTrigger,
          ModelSchema.DmlTrigger,
          ModelSchema.ServerDdlTrigger
       };
    }
    ```

8. Add an override for the `Microsoft.SqlServer.Dac.CodeAnalysis.SqlAnalysisRule.Analyze` `(Microsoft.SqlServer.Dac.CodeAnalysis.SqlRuleExecutionContext)` method, which uses `Microsoft.SqlServer.Dac.CodeAnalysis.SqlRuleExecutionContext` as input parameters. This method returns a list of potential problems.

    The method obtains the `Microsoft.SqlServer.Dac.Model.TSqlModel`, `Microsoft.SqlServer.Dac.Model.TSqlObject`, and [TSqlFragment](/dotnet/api/microsoft.sqlserver.transactsql.scriptdom.tsqlfragment) from the context parameter. The `WaitForDelayVisitor` class is then used to obtain a list of all `WAITFOR DELAY` statements in the model.

    For each [WaitForStatement](/dotnet/api/microsoft.sqlserver.transactsql.scriptdom.waitforstatement) in that list, a `Microsoft.SqlServer.Dac.CodeAnalysis.SqlRuleProblem` is created.

    ```csharp
    /// <summary>
    /// For element-scoped rules the Analyze method is executed once for every matching
    /// object in the model.
    /// </summary>
    /// <param name="ruleExecutionContext">The context object contains the TSqlObject being
    /// analyzed, a TSqlFragment
    /// that's the AST representation of the object, the current rule's descriptor, and a
    /// reference to the model being
    /// analyzed.
    /// </param>
    /// <returns>A list of problems should be returned. These will be displayed in the Visual
    /// Studio error list</returns>
    public override IList<SqlRuleProblem> Analyze(
        SqlRuleExecutionContext ruleExecutionContext)
    {
         IList<SqlRuleProblem> problems = new List<SqlRuleProblem>();

         TSqlObject modelElement = ruleExecutionContext.ModelElement;

         // this rule does not apply to inline table-valued function
         // we simply do not return any problem in that case.
         if (IsInlineTableValuedFunction(modelElement))
         {
             return problems;
         }

         string elementName = GetElementName(ruleExecutionContext, modelElement);

         // The rule execution context has all the objects we'll need, including the
         // fragment representing the object,
         // and a descriptor that lets us access rule metadata
         TSqlFragment fragment = ruleExecutionContext.ScriptFragment;
         RuleDescriptor ruleDescriptor = ruleExecutionContext.RuleDescriptor;

         // To process the fragment and identify WAITFOR DELAY statements we will use a
         // visitor
         WaitForDelayVisitor visitor = new WaitForDelayVisitor();
         fragment.Accept(visitor);
         IList<WaitForStatement> waitforDelayStatements = visitor.WaitForDelayStatements;

         // Create problems for each WAITFOR DELAY statement found
         // When creating a rule problem, always include the TSqlObject being analyzed. This
         // is used to determine
         // the name of the source this problem was found in and a best guess as to the
         // line/column the problem was found at.
         //
         // In addition if you have a specific TSqlFragment that is related to the problem
         //also include this
         // since the most accurate source position information (start line and column) will
         // be read from the fragment
         foreach (WaitForStatement waitForStatement in waitforDelayStatements)
         {
            SqlRuleProblem problem = new SqlRuleProblem(
                String.Format(CultureInfo.CurrentCulture,
                    ruleDescriptor.DisplayDescription, elementName),
                modelElement,
                waitForStatement);
            problems.Add(problem);
        }
        return problems;
    }

    private static string GetElementName(
        SqlRuleExecutionContext ruleExecutionContext,
        TSqlObject modelElement)
    {
        // Get the element name using the built in DisplayServices. This provides a number of
        // useful formatting options to
        // make a name user-readable
        var displayServices = ruleExecutionContext.SchemaModel.DisplayServices;
        string elementName = displayServices.GetElementName(
            modelElement, ElementNameStyle.EscapedFullyQualifiedName);
        return elementName;
    }

    private static bool IsInlineTableValuedFunction(TSqlObject modelElement)
    {
        return TableValuedFunction.TypeClass.Equals(modelElement.ObjectType)
                       && FunctionType.InlineTableValuedFunction ==
            modelElement.GetMetadata<FunctionType>(TableValuedFunction.FunctionType);
    }
    ```

9. From the **File** menu, select **Save**.

## Step 4: Build the class library

::: zone pivot="sq1-visual-studio"

1. On the **Project** menu, select **SampleRules Properties**.
2. Select the **Signing** tab.
3. Select **Sign the assembly**.
4. In **Choose a strong name key file**, select **\<New\>**.
5. In the **Create Strong Name Key** dialog box, in **Key file name**, type `MyRefKey`.
6. (optional) You can specify a password for your strong name key file.
7. Select **OK**.
8. On the **File** menu, select **Save All**.
9. On the **Build** menu, select **Build Solution**.

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

1. Open the **Terminal** window in Visual Studio Code by selecting the **View** menu, then **Terminal**.
2. In the **Terminal**, enter the following command to build the project:

    ```bash
    dotnet build /p:Configuration=Release
    ```

::: zone-end

::: zone pivot="sq1-command-line"

1. Navigate to the `SampleRules` directory.
2. Run the following command to build the project:

    ```bash
    dotnet build /p:Configuration=Release
    ```

::: zone-end

## Step 5: Install and test the new code analysis rule

Next, you must install the assembly so that it loads when you build a SQL database project.

::: zone pivot="sq1-visual-studio"

To install a rule that will run when you build an original SQL project with Visual Studio, you must copy the assembly and associated `.pdb` file to the Extensions folder.

### Install the SampleRules assembly

Next, copy the assembly information to the Extensions directory. When Visual Studio starts, it identifies any extensions in `<Visual Studio Install Dir>\Common7\IDE\Extensions\Microsoft\SQLDB\DAC\Extensions` directory and subdirectories, and makes them available for use.

For Visual Studio 2022, the `<Visual Studio Install Dir>` is usually `C:\Program Files\Microsoft Visual Studio\2022\Enterprise`. Replace `Enterprise` with `Professional` or `Community` depending in your installed Visual Studio edition.

Copy the SampleRules.dll assembly file from the output directory to the `<Visual Studio Install Dir>\Common7\IDE\Extensions\Microsoft\SQLDB\DAC\Extensions` directory. By default, the path of your compiled `.dll` file is `YourSolutionPath\YourProjectPath\bin\Debug` or `YourSolutionPath\YourProjectPath\bin\Release`.

> [!NOTE]  
> You might have to create the `Extensions` directory.

Your rule should now be installed and appears once you restart Visual Studio. Next, start a new session of Visual Studio and create a database project.

### Start a new Visual Studio session and create a database project

1. Start a second session of Visual Studio.
2. Select **File** > **New** > **Project**.
3. In the **New Project** dialog box, locate and the select **SQL Server Database Project**.
4. In the **Name** text box, type `SampleRulesDB` and select **OK**.

Finally, you can see the new rule in the SQL database project interface in Visual Studio. To view the new `AvoidWaitForRule` Code Analysis rule:

5. In **Solution Explorer**, select the `SampleRulesDB` project.
6. On the **Project** menu, select **Properties**. The `SampleRulesDB` properties page is displayed.
7. Select **Code Analysis**. You should see a new category named `RuleSamples.CategorySamples`.
8. Expand `RuleSamples.CategorySamples`. You should see `SR1004: Avoid WAITFOR DELAY statement in stored procedures, triggers, and functions`.
9. Enable this rule by selecting the checkbox next to the rule name and the checkbox for **Enable code analysis on build**. For more information on enabling code analysis, check the [Code analysis overview](../concepts/sql-code-analysis/sql-code-analysis.md).
10. When the project **build** action is used, the rule will be executed and any `WAITFOR DELAY` statements found will be reported as warnings.

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

A workaround is available for SDK-style projects to install custom rules until package references are supported.

1. Run `dotnet restore` to restore the project dependencies on the SQL project, ensuring that the local NuGet packages cache contains Microsoft.Build.Sql.
2. Note the version of Microsoft.Build.Sql used in the SQL project file, such as `0.1.19-preview`.
3. Copy the `SampleRules.dll` assembly file from the output directory to the `~/.nuget/packages/microsoft.build.sql/0.1.19-preview/tools/netstandard2.1` directory. The exact directory path might vary depending on the version of Microsoft.Build.Sql used in the SQL project file.
4. Enable [code analysis on build](../concepts/sql-code-analysis/sql-code-analysis.md) in the SQL project file by setting the `RunSqlCodeAnalysis` property to `true`.
5. Run `dotnet build` to build the SQL project and execute the custom rule.

::: zone-end

::: zone pivot="sq1-command-line"

A workaround is available for SDK-style projects to install custom rules until package references are supported.

1. Run `dotnet restore` to restore the project dependencies on the SQL project, ensuring that the local NuGet packages cache contains Microsoft.Build.Sql.
2. Note the version of Microsoft.Build.Sql used in the SQL project file, such as `0.1.19-preview`.
3. Copy the `SampleRules.dll` assembly file from the output directory to the `~/.nuget/packages/microsoft.build.sql/0.1.19-preview/tools/netstandard2.1` directory. The exact directory path might vary depending on the version of Microsoft.Build.Sql used in the SQL project file.
4. Enable [code analysis on build](../concepts/sql-code-analysis/sql-code-analysis.md) in the SQL project file by setting the `RunSqlCodeAnalysis` property to `true`.
5. Run `dotnet build` to build the SQL project and execute the custom rule.

::: zone-end

## Related content

- [SQL code analysis to improve code quality](../concepts/sql-code-analysis/sql-code-analysis.md)
- [Code analysis rules extensibility overview](../concepts/code-analysis-extensibility.md)
- [Analyze T-SQL code to find defects](analyze-t-sql-code-to-find-defects.md)
