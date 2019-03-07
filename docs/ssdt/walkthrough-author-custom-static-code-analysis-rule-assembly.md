---
title: "Walkthrough Authoring a Custom Static Code Analysis Rule Assembly for SQL Server | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: f7b6ed8c-a4e0-4e33-9858-a8aa40aef309
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# Walkthrough Authoring a Custom Static Code Analysis Rule Assembly for SQL Server
This walkthrough demonstrates the steps used to create a SQL Server Code Analysis rule. The rule created in this walkthrough is used to avoid WAITFOR DELAY statements in stored procedures, triggers, and functions.  
  
In this walkthrough, you will create a custom rule for Transact\-SQL static code analysis by using the following processes:  
  
1.  Create a class library, enable signing for that project, and add the necessary references.  
  
2.  Create a Visual C\# custom rule class.  
  
3.  Create two helper Visual C\# classes.  
  
4.  Copy the resulting DLL that you create into the Extensions directory in order to install it.  
  
5.  Verify that the new code analysis rule is in place.  
  
**Prerequisites**  
  
You need the following components to complete this walkthrough:  
  
-   You must have installed a version of Visual Studio that includes SQL Server Data Tools and supports Visual C\# or Visual Basic development.  
  
-   You must have a SQL Server project that contains SQL Server objects.  
  
-   An instance of SQL Server to which you can deploy a database project.  
  
> [!NOTE]  
> This walkthrough is intended for users who are already familiar with the SQL Server features of SQL Server Data Tools. You are also expected to be familiar with Visual Studio concepts, such as how to create a class library and how to use the code editor to add code to a class.  
  
## Creating a Custom Code Analysis Rule for SQL Server  
First create a class library. To create a class library project:  
  
1.  Create a Visual C\# or Visual Basic class library project named SampleRules.  
  
2.  Rename the file Class1.cs to AvoidWaitForDelayRule.cs.  
  
3.  In Solution Explorer, right-click the project node and then click **Add Reference**.  
  
4.  Select System.ComponentModel.Composition on the Frameworks tab.  
  
5.  Click **Browse** and navigate to the C:\Program Files (x86)\\MicrosoftSQL Server\120\SDK\Assemblies directory, select Microsoft.SqlServer.TransactSql.ScriptDom.dll, and the click OK.  
  
6.  Next install the required DACFx references. Click **Browse** and navigate to the <Visual Studio Install Dir>\Common7\IDE\Extensions\\Microsoft\SQLDB\DAC\120 directory. Choose the Microsoft.SqlServer.Dac.dll, Microsoft.SqlServer.Dac.Extensions.dll, and Microsoft.Data.Tools.Schema.Sql.dll entries and click **Add**, then click **OK**.  
  
    DACFx binaries are now installed inside your Visual Studio install directory. For Visual Studio 2012 the <Visual Studio Install Dir> will usually be C:\Program Files (x86)\\MicrosoftVisual Studio 11.0. For Visual Studio 2013 this will usually be C:\Program Files (x86)\\MicrosoftVisual Studio 12.0.  
  
Next you will add supporting classes that will be used by the rule.  
  
## Creating the Custom Code Analysis Rule Supporting Classes  
Before you create the class for the rule itself, you will add a visitor class and an attribute class to the project. These classes might be useful for creating additional custom rules.  
  
The first class that you must define is the WaitForDelayVisitor class, derived from [TSqlConcreteFragmentVisitor](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.aspx). This class provides access to the WAITFOR DELAY statements in the model. Visitor classes make use of the [ScriptDom](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.aspx) APIs provided by SQL Server. In this API, Transact\-SQL code is represented as an abstract syntax tree (AST) and visitor classes can be useful when you wish to find specific syntax objects such as WAITFORDELAY statements. These might be difficult to find using the object model since they're not associated to a specific object property or relationship, but it's easy to find them using the visitor pattern and the [ScriptDom](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.aspx) API.  
  
### Defining the WaitForDelayVisitor Class  
  
1.  In **Solution Explorer**, select the SampleRules project.  
  
2.  On the **Project** menu, select **Add Class**. The **Add New Item** dialog box appears.  
  
3.  In the **Name** text box, type WaitForDelayVisitor.cs and then click the **Add** button. The WaitForDelayVisitor.cs file is added to the project in **Solution Explorer**.  
  
4.  Open the WaitForDelayVisitor.cs file and update the contents to match the following code:  
  
    ```  
    using System.Collections.Generic;  
    using Microsoft.SqlServer.TransactSql.ScriptDom;  
    namespace SampleRules {  
        class WaitForDelayVistor {}  
    }  
    ```  
  
5.  In the class declaration, change the access modifier to internal and derive the class from TSqlConcreteFragmentVisitor:  
  
    ```  
    internal class WaitForDelayVisitor : TSqlConcreteFragmentVisitor {}  
    ```  
  
6.  Add the following code to define the List member variable:  
  
    ```  
    public IList<WaitForStatement> WaitForDelayStatements { get; private set; }  
    ```  
  
7.  Define the class constructor by adding the following code:  
  
    ```  
    public WaitForDelayVisitor() {  
       WaitForDelayStatments = new List<WaitForStatement>();  
    }  
    ```  
  
8.  Override the ExplicitVisit method by adding the following code:  
  
    ```  
    public override void ExplicitVisit(WaitForStatement node) {  
       // We are only interested in WAITFOR DELAY occurrences  
       if (node.WaitForOption == WaitForOption.Delay)  
          WaitForDelayStatments.Add(node);  
    }  
    ```  
  
    This method visits the WAITFOR statements in the model, and adds those that have the DELAY option specified to the list of WAITFOR DELAY statements. The key class referenced here is [WaitForStatement](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.waitforstatement.aspx).  
  
9. On the **File** menu, click **Save**.  
  
The second class is LocalizedExportCodeAnalysisRuleAttribute.cs. This is an extension of the built-in Microsoft.SqlServer.Dac.CodeAnalysis.ExportCodeAnalysisRuleAttribute provided by the framework, and supports reading the DisplayName and Description used by your rule from a resources file. This is a useful class if you ever intend to have your rules used in multiple languages.  
  
### Defining the LocalizedExportCodeAnalysisRuleAttribute Class  
  
1.  In **Solution Explorer**, select the SampleRules project.  
  
2.  On the **Project** menu, select **Add Class**. The **Add New Item** dialog box appears.  
  
3.  In the **Name** text box, type LocalizedExportCodeAnalysisRuleAttribute.cs and then click the **Add** button. The file is added to the project in **Solution Explorer**.  
  
4.  Open the file and update the contents to match the following code:  
  
    ```  
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
  
Next, you add a resource file that will define the rule name, rule description, and the category in which the rule will appear in the rule configuration interface.  
  
### To Add a Resource File and Three Resource Strings  
  
1.  In **Solution Explorer**, select the SampleRules project.  
  
2.  On the **Project** menu, select **Add New Item**. The **Add New Item** dialog box appears.  
  
3.  In the list of **Installed Templates**, click **General**.  
  
4.  In the details pane, click **Resources File**.  
  
5.  In **Name**, type RuleResources.resx. The resource editor appears, with no resources defined.  
  
6.  Define four resource strings as follows:  
  
    |Name|Value|  
    |--------|---------|  
    |AvoidWaitForDelay_ProblemDescription|WAITFOR DELAY statement was found in {0}.|  
    |AvoidWaitForDelay_RuleName|Avoid using WaitFor Delay statements in stored procedures, functions and triggers.|  
    |CategorySamples|SamplesCategory|  
    |CannotCreateResourceManager|Can't create ResourceManager for {0} from {1}.|  
  
7.  On the **File** menu, click **Save RuleResources.resx**.  
  
Next, define a class that references the resources in the resource file that are used by Visual Studio to display information about your rule in the user interface.  
  
### Defining the SampleConstants Class  
  
1.  In **Solution Explorer**, select the SampleRules project.  
  
2.  On the **Project** menu, select **Add Class**. The **Add New Item** dialog box appears.  
  
3.  In the **Name** text box, type SampleRuleConstants.cs and click the **Add** button. The SampleRuleConstants.cs file is added to the project in **Solution Explorer**.  
  
4.  Open the SampleRuleConstants.cs file and add the following using statements to the file:  
  
    ```  
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
  
5.  Click **File** > **Save**.  
  
## Creating the Custom Code Analysis Rule Class  
Now that you have added the helper classes that the custom Code Analysis rule will use, you will create a custom rule class and name it AvoidWaitForDelayRule. The AvoidWaitForDelayRule custom rule will be used to help database developers avoid WAITFOR DELAY statements in stored procedures, triggers, and functions.  
  
### Creating the AvoidWaitForDelayRule Class  
  
1.  In **Solution Explorer**, select the SampleRules project.  
  
2.  On the **Project** menu, select **Add Class**. The **Add New Item** dialog box appears.  
  
3.  In the **Name** text box, type AvoidWaitForDelayRule.cs and then click **Add**. The AvoidWaitForDelayRule.cs file is added to the project in **Solution Explorer**.  
  
4.  Open the AvoidWaitForDelayRule.cs file and add the following using statements to the file:  
  
    ```  
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
  
5.  In the AvoidWaitForDelayRule class declaration, change the access modifier to public:  
  
    ```  
    /// <summary>  
    /// This is a rule that returns a warning message   
    /// whenever there is a WAITFOR DELAY statement appears inside a subroutine body.  
    /// This rule only applies to stored procedures, functions and triggers.  
    /// </summary>  
    public sealed class AvoidWaitForDelayRule  
    ```  
  
6.  Derive the AvoidWaitForDelayRule class from the Microsoft.SqlServer.Dac.CodeAnalysis.SqlCodeAnalysisRule base class:  
  
    ```  
    public sealed class AvoidWaitForDelayRule : SqlCodeAnalysisRule  
    ```  
  
7.  Add the LocalizedExportCodeAnalysisRuleAttribute to your class.  
  
    LocalizedExportCodeAnalysisRuleAttribute allows the code analysis service to discover custom code analysis rules. Only classes marked with an ExportCodeAnalysisRuleAttribute (or an attribute that inherits from this) can be used in code analysis.  
  
    LocalizedExportCodeAnalysisRuleAttribute provides some required metadata used by the service. This includes a unique ID for this rule, a display name that will be shown in the Visual Studio user interface, and a Description that can be used by your rule when identifying problems.  
  
    ```  
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
  
    The RuleScope property should be Microsoft.SqlServer.Dac.CodeAnalysis.SqlRuleScope.Element as this rule will analyze specific elements. The rule will be called once for each matching element in the model. If you wish to analyze an entire model then Microsoft.SqlServer.Dac.CodeAnalysis.SqlRuleScope.Model can be used instead.  
  
8.  Add a constructor that sets up the Microsoft.SqlServer.Dac.CodeAnalysis.SqlAnalysisRule.SupportedElementTypes. This is required for element-scoped rules. It defines the types of elements to which this rule will be applied. In this case, the rule will be applied to stored procedures, triggers, and functions. Note that the Microsoft.SqlServer.Dac.Model.ModelSchema class lists all available element types that can be analyzed.  
  
    ```  
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
  
9. Add an override for the Microsoft.SqlServer.Dac.CodeAnalysis.SqlAnalysisRule.Analyze(Microsoft.SqlServer.Dac.CodeAnalysis.SqlRuleExecutionContext) method, which uses Microsoft.SqlServer.Dac.CodeAnalysis.SqlRuleExecutionContext as input parameters. This method returns a list of potential problems.  
  
    The method obtains the Microsoft.SqlServer.Dac.Model.TSqlModel, Microsoft.SqlServer.Dac.Model.TSqlObject, and [TSqlFragment](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.tsqlfragment.aspx) from the context parameter. The WaitForDelayVisitor class is then used to obtain a list of all WAITFOR DELAY statements in the model.  
  
    For each [WaitForStatement](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.waitforstatement.aspx) in that list, a Microsoft.SqlServer.Dac.CodeAnalysis.SqlRuleProblem is created.  
  
    ```  
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
  
10. Click **File** > **Save**.  
  
### Building the Class Library  
  
1.  On the **Project** menu, click **SampleRules Properties**.  
  
2.  Click the **Signing** tab.  
  
3.  Click **Sign the assembly**.  
  
4.  In **Choose a strong name key file**, click **<New>**.  
  
5.  In the **Create Strong Name Key** dialog box, in **Key file name**, type MyRefKey.  
  
6.  (optional) You can specify a password for your strong name key file.  
  
7.  Click **OK**.  
  
8.  On the **File** menu, click **Save All**.  
  
9. On the **Build** menu, click **Build Solution**.  
  
Next, you must install the assembly so that it will be loaded when you build and deploy SQL Server projects.  
  
## Install a Static Code Analysis Rule  
To install a rule, you must copy the assembly and associated .pdb file to the Extensions folder.  
  
### To Install the SampleRules Assembly  
Next, you will copy the assembly information to the Extensions directory. When Visual Studio starts, it will identify any extensions in <Visual Studio Install Dir>\Common7\IDE\Extensions\\Microsoft\SQLDB\DAC\120\Extensions directory and subdirectories, and make them available for use.  
  
For Visual Studio 2012 the <Visual Studio Install Dir> will usually be C:\Program Files (x86)\\MicrosoftVisual Studio 11.0. For Visual Studio 2013 this will usually be C:\Program Files (x86)\\MicrosoftVisual Studio 12.0.  
  
Copy the SampleRules.dll assembly file from the output directory to the <Visual Studio Install Dir>\Common7\IDE\Extensions\\Microsoft\SQLDB\DAC\120\Extensions directory. By default, the path of your compiled .dll file is YourSolutionPath\YourProjectPath\bin\Debug or YourSolutionPath\YourProjectPath\bin\Release.  
  
Your rule should now be installed and will appear once you restart Visual Studio. Next, you will start a new session of Visual Studio and create a database project.  
  
### Starting a New Visual Studio Session and Creating a Database Project  
  
1.  Start a second session of Visual Studio.  
  
2.  Click **File** > **New** > **Project**.  
  
3.  In the **New Project** dialog box, in the list of **Installed Templates**, expand the **SQL Server** node, and then click **SQL Server Database Project**.  
  
4.  In the **Name** text box, type SampleRulesDB and click **OK**.  
  
Finally, you will see the new rule displaying in the SQL Server project. To view the new AvoidWaitForRule Code Analysis rule:  
  
1.  In **Solution Explorer**, select the SampleRulesDB project.  
  
2.  On the **Project** menu, click **Properties**. The SampleRulesDB properties page is displayed.  
  
3.  Click **Code Analysis**. You should see a new category named RuleSamples.CategorySamples.  
  
4.  Expand RuleSamples .CategorySamples. You should see SR1004: Avoid WAITFOR DELAY statement in stored procedures, triggers, and functions.  
  
## See Also  
[Overview of Extensibility for Database Code Analysis Rules](../ssdt/overview-of-extensibility-for-database-code-analysis-rules.md)  
  
