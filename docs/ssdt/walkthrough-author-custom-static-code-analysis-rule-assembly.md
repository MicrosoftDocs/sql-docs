---
title: Author a custom static Code Analysis rule assembly for SQL Server
description: Find out how to create SQL Server Code Analysis rules. Set up a rule to avoid WAITFOR DELAY statements in stored procedures, triggers, and functions.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/03/2024
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
---

# Author a custom static Code Analysis rule assembly for SQL Server

This walkthrough demonstrates the steps used to create a SQL Server Code Analysis rule. The rule created in this walkthrough is used to avoid `WAITFOR DELAY` statements in stored procedures, triggers, and functions.

In this walkthrough, you create a custom rule for Transact-SQL static code analysis by using the following processes:

1. Create a class library, enable signing for that project, and add the necessary references.

1. Create a [!INCLUDE [c-sharp-md](../includes/c-sharp-md.md)] custom rule class.

1. Create two helper [!INCLUDE [c-sharp-md](../includes/c-sharp-md.md)] classes.

1. Copy the resulting DLL that you create into the Extensions directory in order to install it.

1. Verify that the new Code Analysis rule is in place.

## Prerequisites

You need the following components to complete this walkthrough:

- A version of Visual Studio installed, which includes SQL Server Data Tools, and supports [!INCLUDE [c-sharp-md](../includes/c-sharp-md.md)] or [!INCLUDE [visual-basic-md](../includes/visual-basic-md.md)] .NET Framework development.

- A SQL Server project that contains SQL Server objects.

- An instance of SQL Server to which you can deploy a database project.

This walkthrough is intended for users who are already familiar with the SQL Server features of SQL Server Data Tools. You should be familiar with Visual Studio concepts, such as how to create a class library, add NuGet packages, and how to use the code editor to add code to a class.

## Create a custom Code Analysis rule for SQL Server

First create a class library. To create a class library project:

1. Create a [!INCLUDE [c-sharp-md](../includes/c-sharp-md.md)] (.NET Framework) or [!INCLUDE [visual-basic-md](../includes/visual-basic-md.md)] (.NET Framework) class library project named `SampleRules`.

1. Rename the file `Class1.cs` to `AvoidWaitForDelayRule.cs`.

1. In Solution Explorer, right-click the project node and then select **Add** then **Reference**.

1. Select `System.ComponentModel.Composition` on the **Assemblies\Frameworks** tab.

1. In Solution Explorer, right-click the project node and then select **Manage NuGet Packages**. Locate and install the `Microsoft.SqlServer.DacFx` NuGet package. The selected version must be `162.x.x` (for example `162.2.111`) with Visual Studio 2022.

Next, add supporting classes that will be used by the rule.

## Create the custom Code Analysis rule supporting classes

Before you create the class for the rule itself, add a visitor class and an attribute class to the project. These classes might be useful for creating more custom rules.

### Define the WaitForDelayVisitor class

The first class that you must define is the `WaitForDelayVisitor` class, derived from [TSqlConcreteFragmentVisitor](/dotnet/api/microsoft.sqlserver.transactsql.scriptdom.tsqlconcretefragmentvisitor). This class provides access to the `WAITFOR DELAY` statements in the model. Visitor classes make use of the [ScriptDom](/dotnet/api/microsoft.sqlserver.transactsql.scriptdom) APIs provided by SQL Server. In this API, Transact-SQL code is represented as an abstract syntax tree (AST) and visitor classes can be useful when you wish to find specific syntax objects such as `WAITFOR DELAY` statements. These statements might be difficult to find using the object model since they're not associated to a specific object property or relationship, but you can find them using the visitor pattern and the [ScriptDom](/dotnet/api/microsoft.sqlserver.transactsql.scriptdom) API.

1. In **Solution Explorer**, select the `SampleRules` project.

1. On the **Project** menu, select **Add Class**. The **Add New Item** dialog box appears.

1. In the **Name** text box, type `WaitForDelayVisitor.cs` and then select the **Add** button. The `WaitForDelayVisitor.cs` file is added to the project in **Solution Explorer**.

1. Open the `WaitForDelayVisitor.cs` file and update the contents to match the following code:

    ```csharp
    using System.Collections.Generic;
    using Microsoft.SqlServer.TransactSql.ScriptDom;
    namespace SampleRules {
        class WaitForDelayVistor {}
    }
    ```

1. In the class declaration, change the access modifier to internal and derive the class from `TSqlConcreteFragmentVisitor`:

    ```csharp
    internal class WaitForDelayVisitor : TSqlConcreteFragmentVisitor {}
    ```

1. Add the following code to define the List member variable:

    ```csharp
    public IList<WaitForStatement> WaitForDelayStatements { get; private set; }
    ```

1. Define the class constructor by adding the following code:

    ```csharp
    public WaitForDelayVisitor() {
       WaitForDelayStatements = new List<WaitForStatement>();
    }
    ```

1. Override the `ExplicitVisit` method by adding the following code:

    ```csharp
    public override void ExplicitVisit(WaitForStatement node) {
       // We are only interested in WAITFOR DELAY occurrences
       if (node.WaitForOption == WaitForOption.Delay)
          WaitForDelayStatements.Add(node);
    }
    ```

    This method visits the `WAITFOR` statements in the model, and adds statements that have the `DELAY` option specified to the list of `WAITFOR DELAY` statements. The key class referenced is [WaitForStatement](/dotnet/api/microsoft.sqlserver.transactsql.scriptdom.waitforstatement).

1. On the **File** menu, select **Save**.

### Define the LocalizedExportCodeAnalysisRuleAttribute class

The second class is `LocalizedExportCodeAnalysisRuleAttribute.cs`. This is an extension of the built-in `Microsoft.SqlServer.Dac.CodeAnalysis.ExportCodeAnalysisRuleAttribute` provided by the framework, and supports reading the `DisplayName` and `Description` used by your rule from a resources file. This is a useful class if you ever intend to have your rules used in multiple languages.

1. In **Solution Explorer**, select the `SampleRules` project.

1. On the **Project** menu, select **Add Class**. The **Add New Item** dialog box appears.

1. In the **Name** text box, type `LocalizedExportCodeAnalysisRuleAttribute.cs` and then select the **Add** button. The file is added to the project in **Solution Explorer**.

1. Open the file and update the contents to match the following code:

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

1. In **Solution Explorer**, select the `SampleRules` project.

1. On the **Project** menu, select **Add** then **New Item**. The **Add New Item** dialog box appears.

1. In the list of **Installed Templates**, select **General**.

1. In the details pane, select **Resources File**.

1. In **Name**, type `RuleResources.resx`. The resource editor appears, with no resources defined.

1. Define four resource strings as follows:

    | Name | Value |
    | --- | --- |
    | `AvoidWaitForDelay_ProblemDescription` | `WAITFOR DELAY statement was found in {0}.` |
    | `AvoidWaitForDelay_RuleName` | `Avoid using WaitFor Delay statements in stored procedures, functions and triggers.` |
    | `CategorySamples` | `SamplesCategory` |
    | `CannotCreateResourceManager` | `Can't create ResourceManager for {0} from {1}.` |

1. On the **File** menu, select **Save RuleResources.resx**.

### Define the SampleConstants class

Next, define a class that references the resources in the resource file that are used by Visual Studio to display information about your rule in the user interface.

1. In **Solution Explorer**, select the `SampleRules` project.

1. On the **Project** menu, select **Add** then **Class**. The **Add New Item** dialog box appears.

1. In the **Name** text box, type `SampleRuleConstants.cs` and select the **Add** button. The `SampleRuleConstants.cs` file is added to the project in **Solution Explorer**.

1. Open the `SampleRuleConstants.cs` file and add the following using statements to the file:

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

1. Select **File** > **Save**.

## Create the custom Code Analysis rule class

After you add the helper classes that the custom Code Analysis rule will use, create a custom rule class and name it `AvoidWaitForDelayRule`. The `AvoidWaitForDelayRule` custom rule will be used to help database developers avoid `WAITFOR DELAY` statements in stored procedures, triggers, and functions.

### Create the AvoidWaitForDelayRule class

1. In **Solution Explorer**, select the `SampleRules` project.

1. On the **Project** menu, select **Add** then **Class**. The **Add New Item** dialog box appears.

1. In the **Name** text box, type `AvoidWaitForDelayRule.cs` and then select **Add**. The `AvoidWaitForDelayRule.cs` file is added to the project in **Solution Explorer**.

1. Open the `AvoidWaitForDelayRule.cs` file and add the following using statements to the file:

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

1. In the `AvoidWaitForDelayRule` class declaration, change the access modifier to public:

    ```csharp
    /// <summary>
    /// This is a rule that returns a warning message
    /// whenever there is a WAITFOR DELAY statement appears inside a subroutine body.
    /// This rule only applies to stored procedures, functions and triggers.
    /// </summary>
    public sealed class AvoidWaitForDelayRule
    ```

1. Derive the `AvoidWaitForDelayRule` class from the `Microsoft.SqlServer.Dac.CodeAnalysis.SqlCodeAnalysisRule` base class:

    ```csharp
    public sealed class AvoidWaitForDelayRule : SqlCodeAnalysisRule
    ```

1. Add the `LocalizedExportCodeAnalysisRuleAttribute` to your class.

    `LocalizedExportCodeAnalysisRuleAttribute` allows the code analysis service to discover custom Code Analysis rules. Only classes marked with an `ExportCodeAnalysisRuleAttribute` (or an attribute that inherits from this) can be used in code analysis.

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

1. Add a constructor that sets up the `Microsoft.SqlServer.Dac.CodeAnalysis.SqlAnalysisRule.SupportedElementTypes`. This is required for element-scoped rules. It defines the types of elements to which this rule applies. In this case, the rule is applied to stored procedures, triggers, and functions. The `Microsoft.SqlServer.Dac.Model.ModelSchema` class lists all available element types that can be analyzed.

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

1. Add an override for the `Microsoft.SqlServer.Dac.CodeAnalysis.SqlAnalysisRule.Analyze` `(Microsoft.SqlServer.Dac.CodeAnalysis.SqlRuleExecutionContext)` method, which uses `Microsoft.SqlServer.Dac.CodeAnalysis.SqlRuleExecutionContext` as input parameters. This method returns a list of potential problems.

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

1. Select **File** > **Save**.

### Build the class library

1. On the **Project** menu, select **SampleRules Properties**.

1. Select the **Signing** tab.

1. Select **Sign the assembly**.

1. In **Choose a strong name key file**, select **\<New\>**.

1. In the **Create Strong Name Key** dialog box, in **Key file name**, type `MyRefKey`.

1. (optional) You can specify a password for your strong name key file.

1. Select **OK**.

1. On the **File** menu, select **Save All**.

1. On the **Build** menu, select **Build Solution**.

## Install a static Code Analysis rule

Next, you must install the assembly so that it loads when you build and deploy SQL Server projects.

To install a rule, you must copy the assembly and associated `.pdb` file to the Extensions folder.

### Install the SampleRules assembly

Next, copy the assembly information to the Extensions directory. When Visual Studio starts, it identifies any extensions in `<Visual Studio Install Dir>\Common7\IDE\Extensions\Microsoft\SQLDB\DAC\Extensions` directory and subdirectories, and makes them available for use.

For Visual Studio 2022, the `<Visual Studio Install Dir>` is usually `C:\Program Files\Microsoft Visual Studio\2022\Enterprise`. Replace `Enterprise` with `Professional` or `Community` depending in your installed Visual Studio edition.

Copy the SampleRules.dll assembly file from the output directory to the `<Visual Studio Install Dir>\Common7\IDE\Extensions\Microsoft\SQLDB\DAC\Extensions` directory. By default, the path of your compiled `.dll` file is `YourSolutionPath\YourProjectPath\bin\Debug` or `YourSolutionPath\YourProjectPath\bin\Release`.

> [!NOTE]  
> You might have to create the `Extensions` directory.

Your rule should now be installed and appears once you restart Visual Studio. Next, start a new session of Visual Studio and create a database project.

### Start a new Visual Studio session and create a database project

1. Start a second session of Visual Studio.

1. Select **File** > **New** > **Project**.

1. In the **New Project** dialog box, locate and the select **SQL Server Database Project**.

1. In the **Name** text box, type `SampleRulesDB` and select **OK**.

Finally, you see the new rule displaying in the SQL Server project. To view the new `AvoidWaitForRule` Code Analysis rule:

1. In **Solution Explorer**, select the `SampleRulesDB` project.

1. On the **Project** menu, select **Properties**. The `SampleRulesDB` properties page is displayed.

1. Select **Code Analysis**. You should see a new category named `RuleSamples.CategorySamples`.

1. Expand `RuleSamples.CategorySamples`. You should see `SR1004: Avoid WAITFOR DELAY statement in stored procedures, triggers, and functions`.

## Related content

- [Overview of Extensibility for Database Code Analysis rules](overview-of-extensibility-for-database-code-analysis-rules.md)
