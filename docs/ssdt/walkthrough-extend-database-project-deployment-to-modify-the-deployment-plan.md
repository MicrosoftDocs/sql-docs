---
title: "Walkthrough: Extend Database Project Deployment to Modify the Deployment Plan | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 22b077b1-fa25-49ff-94f6-6d0d196d870a
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# Walkthrough: Extend Database Project Deployment to Modify the Deployment Plan
You can create deployment contributors to perform custom actions when you deploy a SQL project. You can create either a [DeploymentPlanModifier](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.deploymentplanmodifier.aspx) or a [DeploymentPlanExecutor](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.deploymentplanexecutor.aspx). Use a [DeploymentPlanModifier](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.deploymentplanmodifier.aspx) to change the plan before it is executed and a [DeploymentPlanExecutor](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.deploymentplanexecutor.aspx) to perform operations while the plan is being executed. In this walkthrough, you create a [DeploymentPlanModifier](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.deploymentplanmodifier.aspx) named SqlRestartableScriptContributor that adds IF statements to the batches in the deployment script to enable the script to be re-run until they are completed if an error occurs during execution.  
  
In this walkthrough, you will accomplish the following major tasks:  
  
-   [Create the DeploymentPlanModifier type of deployment contributor](#CreateDeploymentContributor)  
  
-   [Install the deployment contributor](#InstallDeploymentContributor)  
  
-   [Run or Test your deployment contributor](#TestDeploymentContributor)  
  
## Prerequisites  
You need the following components to complete this walkthrough:  
  
-   You must have installed a version of Visual Studio that includes SQL Server Data Tools and supports C# or VB development.  
  
-   You must have a SQL project that contains SQL objects.  
  
-   An instance of SQL Server to which you can deploy a database project.  
  
> [!NOTE]  
> This walkthrough is intended for users who are already familiar with the SQL features of SQL Server Data Tools. You are also expected to be familiar with basic Visual Studio concepts, such as how to create a class library and how to use the code editor to add code to a class.  
  
## <a name="CreateDeploymentContributor"></a>Create a Deployment Contributor  
To create a deployment contributor, you must perform the following tasks:  
  
-   Create a class library project and add required references.  
  
-   Define a class named SqlRestartableScriptContributor that inherits from [DeploymentPlanModifier](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.deploymentplanmodifier.aspx).  
  
-   Override the [OnExecute](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.deploymentplancontributor.onexecute.aspx) method.  
  
-   Add private helper methods.  
  
-   Build the resulting assembly.  
  
#### To create a class library project  
  
1.  Create a Visual C# or Visual Basic class library project named MyOtherDeploymentContributor.  
  
2.  Rename the file "Class1.cs" to "SqlRestartableScriptContributor.cs."  
  
3.  In Solution Explorer, right-click the project node and then click **Add Reference**.  
  
4.  Select **System.ComponentModel.Composition** on the Frameworks tab.  
  
5.  Click **Browse** and navigate to the **C:\Program Files (x86)\Microsoft SQL Server\110\SDK\Assemblies** directory, select **Microsoft.SqlServer.TransactSql.ScriptDom.dll**, and the click **OK**.  
  
6.  Add required SQL references: right-click the project node and then click **Add Reference**. Click **Browse** and navigate to the **C:\Program Files (x86)\Microsoft SQL Server\110\DAC\Bin** folder. Choose the **Microsoft.SqlServer.Dac.dll**, **Microsoft.SqlServer.Dac.Extensions.dll**, and **Microsoft.Data.Tools.Schema.Sql.dll** entries and click **Add**, then click **OK**.  
  
Next, start to add code to the class.  
  
#### To define the SqlRestartableScriptContributor class  
  
1.  In the code editor, update the class1.cs file to match the following **using** statements:  
  
    ```csharp  
    using System;  
    using System.Collections.Generic;  
    using System.Globalization;  
    using System.Text;  
    using Microsoft.SqlServer.Dac.Deployment;  
    using Microsoft.SqlServer.Dac.Model;  
    using Microsoft.SqlServer.TransactSql.ScriptDom;  
    ```  
  
2.  Update the class definition to match the following example:  
  
    ```csharp  
        /// <summary>  
    /// This deployment contributor modifies a deployment plan by adding if statements  
    /// to the existing batches in order to make a deployment script able to be rerun to completion  
    /// if an error is encountered during execution  
    /// </summary>  
    [ExportDeploymentPlanModifier("MyOtherDeploymentContributor.RestartableScriptContributor", "1.0.0.0")]  
    public class SqlRestartableScriptContributor : DeploymentPlanModifier  
    {  
    }  
  
    ```  
  
    Now you have defined your deployment contributor that inherits from [DeploymentPlanModifier](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.deploymentplanmodifier.aspx). During the build and deployment processes, custom contributors are loaded from a standard extension directory. Deployment plan modifying contributors are identified by an [ExportDeploymentPlanModifier](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.exportdeploymentplanmodifierattribute.aspx) attribute. This attribute is required so that contributors can be discovered. This attribute should look similar to the following:  
  
    ```csharp  
    [ExportDeploymentPlanModifier("MyOtherDeploymentContributor.RestartableScriptContributor", "1.0.0.0")]  
  
    ```  
  
3.  Add the following member declarations:  
  
    ```vb  
         private const string BatchIdColumnName = "BatchId";  
            private const string DescriptionColumnName = "Description";  
  
            private const string CompletedBatchesVariableName = "CompletedBatches";  
            private const string CompletedBatchesVariable = "$(CompletedBatches)";  
            private const string CompletedBatchesSqlCmd = @":setvar " + CompletedBatchesVariableName + " __completedBatches_{0}_{1}";  
            private const string TotalBatchCountSqlCmd = @":setvar TotalBatchCount {0}";  
            private const string CreateCompletedBatchesTable = @"  
    if OBJECT_ID(N'tempdb.dbo." + CompletedBatchesVariable + @"', N'U') is null  
    begin  
    use tempdb  
    create table [dbo].[$(CompletedBatches)]  
    (  
    BatchId int primary key,  
    Description nvarchar(300)  
    )  
    use [$(DatabaseName)]  
    end  
    ";  
  
    ```  
  
    Next, you override the OnExecute method to add the code that you want to run when a database project is deployed.  
  
#### To override OnExecute  
  
1.  Add the following method to your SqlRestartableScriptContributor class:  
  
    ```csharp  
    /// <summary>  
    /// You override the OnExecute method to do the real work of the contributor.  
    /// </summary>  
    /// <param name="context"></param>  
    protected override void OnExecute(DeploymentPlanContributorContext context)  
    {  
         // Replace this with the method body  
    }  
  
    ```  
  
    You override the [OnExecute](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.deploymentplancontributor.onexecute.aspx) method from the base class, [DeploymentPlanContributor](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.deploymentplancontributor.aspx), which is the base class for both [DeploymentPlanModifier](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.deploymentplanmodifier.aspx) and [DeploymentPlanExecutor](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.deploymentplanexecutor.aspx). The OnExecute method is passed a [DeploymentPlanContributorContext](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.deploymentplancontributorcontext.aspx) object that provides access to any specified arguments, the source and target database model, the deployment plan, and deployment options. In this example, we get the deployment plan and the target database name.  
  
2.  Now add the beginnings of a body to the OnExecute method:  
  
    ```vb  
    // Obtain the first step in the Plan from the provided context  
    DeploymentStep nextStep = context.PlanHandle.Head;  
    int batchId = 0;  
    BeginPreDeploymentScriptStep beforePreDeploy = null;  
  
    // Loop through all steps in the deployment plan  
    while (nextStep != null)  
    {  
        // Increment the step pointer, saving both the current and next steps  
        DeploymentStep currentStep = nextStep;  
        nextStep = currentStep.Next;  
  
        // Add additional step processing here  
    }  
  
    // if we found steps that required processing, set up a temporary table to track the work that you are doing  
    if (beforePreDeploy != null)  
    {  
        // Add additional post-processing here  
    }  
  
    // Cleanup and drop the table   
    DeploymentScriptStep dropStep = new DeploymentScriptStep(DropCompletedBatchesTable);  
    base.AddAfter(context.PlanHandle, context.PlanHandle.Tail, dropStep);  
  
    ```  
  
    In this code, we define a few local variables, and set up the loop that will handle processing of all the steps in the deployment plan. After the loop completes, we will have to do some post-processing, and then will drop the temporary table that we created during deployment to track progress as the plan executed. Key types here are: [DeploymentStep](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.deploymentstep.aspx) and [DeploymentScriptStep](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.deploymentscriptstep.aspx). A key method is AddAfter.  
  
3.  Now add the additional step processing, to replace the comment that reads "Add additional step processing here":  
  
    ```csharp  
    // Look for steps that mark the pre/post deployment scripts  
    // These steps will always be in the deployment plan even if the  
    // user's project does not have a pre/post deployment script  
    if (currentStep is BeginPreDeploymentScriptStep)  
    {  
        // This step marks the beginning of the predeployment script.  
        // Save the step and move on.  
        beforePreDeploy = (BeginPreDeploymentScriptStep)currentStep;  
        continue;  
    }  
    if (currentStep is BeginPostDeploymentScriptStep)  
    {  
        // This is the step that marks the beginning of the post deployment script.    
        // We do not continue processing after this point.  
        break;  
    }  
    if (currentStep is SqlPrintStep)  
    {  
        // We do not need to put if statements around these  
        continue;  
    }  
  
    // if we have not yet found the beginning of the pre-deployment script steps,   
    // skip to the next step.  
    if (beforePreDeploy == null)  
    {  
        // We only surround the "main" statement block with conditional  
        // statements  
        continue;  
    }  
  
    // Determine if this is a step that we need to surround with a conditional statement  
    DeploymentScriptDomStep domStep = currentStep as DeploymentScriptDomStep;  
    if (domStep == null)  
    {  
        // This step is not a step that we know how to modify,  
        // so skip to the next step.  
        continue;  
    }  
  
    TSqlScript script = domStep.Script as TSqlScript;  
    if (script == null)  
    {  
        // The script dom step does not have a script with batches - skip  
        continue;  
    }  
  
        // Loop through all the batches in the script for this step.  All the statements  
        // in the batch will be enclosed in an if statement that will check the  
        // table to ensure that the batch has not already been executed  
        TSqlObject sqlObject;  
        string stepDescription;  
        GetStepInfo(domStep, out stepDescription, out sqlObject);  
        int batchCount = script.Batches.Count;  
  
    for (int batchIndex = 0; batchIndex < batchCount; batchIndex++)  
    {  
        // Add batch processing here  
    }  
  
    ```  
  
    The code comments explain the processing. At a high-level, this code looks for the steps that you care about, skipping others and stopping when you reach the beginning of the post-deployment steps. If the step contains statements that we must surround with conditionals, we will perform additional processing. Key types, methods, and properties include the following: [BeginPreDeploymentScriptStep](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.beginpredeploymentscriptstep.aspx), [BeginPostDeploymentScriptStep](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.beginpostdeploymentscriptstep.aspx), [TSqlObject](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.model.tsqlobject.aspx), [TSqlScript](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.tsqlscript.aspx), Script, [DeploymentScriptDomStep](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.deploymentscriptdomstep.aspx), and [SqlPrintStep](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.sqlprintstep.aspx).  
  
4.  Now, add the batch processing code by replacing the comment that reads "Add batch processing here":  
  
    ```csharp  
        // Create the if statement that will contain the batch's contents  
        IfStatement ifBatchNotExecutedStatement = CreateIfNotExecutedStatement(batchId);  
        BeginEndBlockStatement statementBlock = new BeginEndBlockStatement();  
        ifBatchNotExecutedStatement.ThenStatement = statementBlock;  
        statementBlock.StatementList = new StatementList();  
  
        TSqlBatch batch = script.Batches[batchIndex];  
        int statementCount = batch.Statements.Count;  
  
        // Loop through all statements in the batch, embedding those in an sp_execsql  
        // statement that must be handled this way (schemas, stored procedures,   
        // views, functions, and triggers).  
        for (int statementIndex = 0; statementIndex < statementCount; statementIndex++)  
        {  
            // Add additional statement processing here  
        }  
  
        // Add an insert statement to track that all the statements in this  
        // batch were executed.  Turn on nocount to improve performance by  
        // avoiding row inserted messages from the server  
        string batchDescription = string.Format(CultureInfo.InvariantCulture,  
            "{0} batch {1}", stepDescription, batchIndex);  
  
        PredicateSetStatement noCountOff = new PredicateSetStatement();  
        noCountOff.IsOn = false;  
        noCountOff.Options = SetOptions.NoCount;  
  
        PredicateSetStatement noCountOn = new PredicateSetStatement();  
        noCountOn.IsOn = true;  
        noCountOn.Options = SetOptions.NoCount;   
        InsertStatement batchCompleteInsert = CreateBatchCompleteInsert(batchId, batchDescription);  
        statementBlock.StatementList.Statements.Add(noCountOn);  
    statementBlock.StatementList.Statements.Add(batchCompleteInsert);  
        statementBlock.StatementList.Statements.Add(noCountOff);  
  
        // Remove all the statements from the batch (they are now in the if block) and add the if statement  
        // as the sole statement in the batch  
        batch.Statements.Clear();  
        batch.Statements.Add(ifBatchNotExecutedStatement);  
  
        // Next batch  
        batchId++;  
  
    ```  
  
    This code creates an IF statement along with a BEGIN/END block. We then perform additional processing on the statements in the batch. Once that is complete, we add an INSERT statement to add information to the temporary table that tracks the progress of the script execution. Finally, update the batch, replacing the statements that used to be there with the new IF that contains those statements within it.Key types, methods, and properties include: [IfStatement](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.ifstatement.aspx), [BeginEndBlockStatement](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.beginendblockstatement.aspx), [StatementList](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.statementlist.aspx), [TSqlBatch](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.tsqlbatch.aspx), [PredicateSetStatement](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.predicatesetstatement.aspx), [SetOptions](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.setoptions.aspx), and [InsertStatement](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.insertstatement.aspx).  
  
5.  Now, add the body of the statement processing loop. Replace the comment that reads "Add additional statement processing here":  
  
    ```csharp  
    TSqlStatement smnt = batch.Statements[statementIndex];  
  
    if (IsStatementEscaped(sqlObject))  
    {  
        // "escape" this statement by embedding it in a sp_executesql statement  
        string statementScript;  
        domStep.ScriptGenerator.GenerateScript(smnt, out statementScript);  
        ExecuteStatement spExecuteSql = CreateExecuteSql(statementScript);  
        smnt = spExecuteSql;  
    }  
  
    statementBlock.StatementList.Statements.Add(smnt);  
  
    ```  
  
    For each statement in the batch, if the statement is of a type that must be wrapped with an sp_executesql statement, modify the statement accordingly. The code then adds the statement to the statement list for the BEGIN/END block that you created. Key types, methods, and properties include [TSqlStatement](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.tsqlstatement.aspx) and [ExecuteStatement](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.executestatement.aspx).  
  
6.  Finally, add the post-processing section in place of the comment that reads "Add additional post-processing here":  
  
    ```csharp  
    // Declare a SqlCmd variables.  
    //  
    // CompletedBatches variable - defines the name of the table in tempdb that will track  
    // all the completed batches.  The temporary table's name has the target database name and  
    // a guid embedded in it so that:  
    // * Multiple deployment scripts targeting different DBs on the same server  
    // * Failed deployments with old tables do not conflict with more recent deployments  
    //  
    // TotalBatchCount variable - the total number of batches surrounded by if statements.  Using this  
    // variable pre/post deployment scripts can also use the CompletedBatches table to make their  
    // script rerunnable if there is an error during execution  
    StringBuilder sqlcmdVars = new StringBuilder();  
    sqlcmdVars.AppendFormat(CultureInfo.InvariantCulture, CompletedBatchesSqlCmd,  
        context.Options.TargetDatabaseName, Guid.NewGuid().ToString("D"));  
    sqlcmdVars.AppendLine();  
    sqlcmdVars.AppendFormat(CultureInfo.InvariantCulture, TotalBatchCountSqlCmd, batchId);  
  
    DeploymentScriptStep completedBatchesSetVarStep = new DeploymentScriptStep(sqlcmdVars.ToString());  
    base.AddBefore(context.PlanHandle, beforePreDeploy, completedBatchesSetVarStep);  
  
    // Create the temporary table we will use to track the work that we are doing  
    DeploymentScriptStep createStatusTableStep = new DeploymentScriptStep(CreateCompletedBatchesTable);  
    base.AddBefore(context.PlanHandle, beforePreDeploy, createStatusTableStep);  
  
    ```  
  
    If our processing found one or more steps that we surrounded with a conditional statement, we must add statements to the deployment script to define SQLCMD variables. The first variable, CompletedBatches, contains a unique name for the temporary table that the deployment script uses to keep track of which batches were successfully completed when the script was executed. The second variable, TotalBatchCount, contains the total number of batches in the deployment script.  
  
    Additional types, properties, and methods of interest include:  
  
    StringBuilder, [DeploymentScriptStep](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.deploymentscriptstep.aspx), and AddBefore.  
  
    Next, you define the helper methods called by this method.  
  
#### To add the helper methods  
  
-   A number of helper methods must be defined. Important methods include:  
  
    |**Method**|**Description**|  
    |--------------|-------------------|  
    |CreateExecuteSQL|Define the CreateExecuteSQL method to surround a provided statement with an EXEC sp_executesql statement. Key types, methods, and properties include the following: [ExecuteStatement](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.executestatement.aspx), [ExecutableProcedureReference](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.executableprocedurereference.aspx), [SchemaObjectName](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.schemaobjectname.aspx), [ProcedureReference](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.procedurereference.aspx), and [ExecuteParameter](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.executeparameter.aspx).|  
    |CreateCompletedBatchesName|Define the CreateCompletedBatchesName method. This method creates the name that will be inserted into the temporary table for a batch.Key types, methods, and properties include the following: [SchemaObjectName](https://msdn.microsoft.com/library/microsoft.sqlserver.transactsql.scriptdom.schemaobjectname.aspx).|  
    |IsStatementEscaped|Define the IsStatementEscaped method. This method determines whether the type of model element requires the statement to be wrapped in an EXEC sp_executesql statement before it can be enclosed within an IF statement. Key types, methods, and properties include the following: TSqlObject.ObjectType, ModelTypeClass, and the TypeClass property for the following model types: Schema, Procedure, View,  TableValuedFunction, ScalarFunction, DatabaseDdlTrigger, DmlTrigger, ServerDdlTrigger.|  
    |CreateBatchCompleteInsert|Define the CreateBatchCompleteInsert method. This method creates the INSERT statement that will be added to the deployment script to track progress of script execution. Key types, methods, and properties include the following: InsertStatement, NamedTableReference, ColumnReferenceExpression, ValuesInsertSource, and RowValue.|  
    |CreateIfNotExecutedStatement|Define the CreateIfNotExecutedStatement method. This method generates an IF statement that checks to see if the temporary batches executes table indicates that this batch has already been executed. Key types, methods, and properties include the following: IfStatement, ExistsPredicate, ScalarSubquery, NamedTableReference, WhereClause, ColumnReferenceExpression, IntegerLiteral, BooleanComparisonExpression, and BooleanNotExpression.|  
    |GetStepInfo|Define the GetStepInfo method. This method extracts information about the model element used to create the step's script, in addition to the step name. Types and methods of interest include the following: [DeploymentPlanContributorContext](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.deploymentplancontributorcontext.aspx), [DeploymentScriptDomStep](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.deploymentscriptdomstep.aspx), [TSqlObject](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.model.tsqlobject.aspx), [CreateElementStep](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.createelementstep.aspx), [AlterElementStep](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.alterelementstep.aspx), and [DropElementStep](https://msdn.microsoft.com/library/microsoft.sqlserver.dac.deployment.dropelementstep.aspx).|  
    |GetElementName|Creates a formatted name for a TSqlObject.|  
  
1.  Add the following code to define the helper methods:  
  
    ```csharp  
  
    /// <summary>  
    /// The CreateExecuteSql method "wraps" the provided statement script in an "sp_executesql" statement  
    /// Examples of statements that must be so wrapped include: stored procedures, views, and functions  
    /// </summary>  
    private static ExecuteStatement CreateExecuteSql(string statementScript)  
    {  
        // define a new Exec statement  
        ExecuteStatement executeSp = new ExecuteStatement();  
        ExecutableProcedureReference spExecute = new ExecutableProcedureReference();  
        executeSp.ExecuteSpecification = new ExecuteSpecification { ExecutableEntity = spExecute };  
  
        // define the name of the procedure that you want to execute, in this case sp_executesql  
        SchemaObjectName procName = new SchemaObjectName();  
        procName.Identifiers.Add(CreateIdentifier("sp_executesql", QuoteType.NotQuoted));  
        ProcedureReference procRef = new ProcedureReference { Name = procName };  
  
        spExecute.ProcedureReference = new ProcedureReferenceName { ProcedureReference = procRef };  
  
        // add the script parameter, constructed from the provided statement script  
        ExecuteParameter scriptParam = new ExecuteParameter();  
        spExecute.Parameters.Add(scriptParam);  
        scriptParam.ParameterValue = new StringLiteral { Value = statementScript };  
        scriptParam.Variable = new VariableReference { Name = "@stmt" };  
        return executeSp;  
    }  
  
    /// <summary>  
    /// The CreateIdentifier method returns a Identifier with the specified value and quoting type  
    /// </summary>  
    private static Identifier CreateIdentifier(string value, QuoteType quoteType)  
    {  
        return new Identifier { Value = value, QuoteType = quoteType };  
    }  
  
    /// <summary>  
    /// The CreateCompletedBatchesName method creates the name that will be inserted  
    /// into the temporary table for a batch.  
    /// </summary>  
    private static SchemaObjectName CreateCompletedBatchesName()  
    {  
        SchemaObjectName name = new SchemaObjectName();  
        name.Identifiers.Add(CreateIdentifier("tempdb", QuoteType.SquareBracket));  
        name.Identifiers.Add(CreateIdentifier("dbo", QuoteType.SquareBracket));  
        name.Identifiers.Add(CreateIdentifier(CompletedBatchesVariable, QuoteType.SquareBracket));  
        return name;  
    }  
  
    /// <summary>  
    /// Helper method that determins whether the specified statement needs to  
    /// be escaped  
    /// </summary>  
    /// <param name="sqlObject"></param>  
    /// <returns></returns>  
    private static bool IsStatementEscaped(TSqlObject sqlObject)  
    {  
        HashSet<ModelTypeClass> escapedTypes = new HashSet<ModelTypeClass>  
        {  
            Schema.TypeClass,  
            Procedure.TypeClass,  
            View.TypeClass,  
            TableValuedFunction.TypeClass,  
            ScalarFunction.TypeClass,  
            DatabaseDdlTrigger.TypeClass,  
            DmlTrigger.TypeClass,  
            ServerDdlTrigger.TypeClass  
        };  
        return escapedTypes.Contains(sqlObject.ObjectType);  
    }  
  
    /// <summary>  
    /// Helper method that creates an INSERT statement to track a batch being completed  
    /// </summary>  
    /// <param name="batchId"></param>  
    /// <param name="batchDescription"></param>  
    /// <returns></returns>  
    private static InsertStatement CreateBatchCompleteInsert(int batchId, string batchDescription)  
    {  
        InsertStatement insert = new InsertStatement();  
        NamedTableReference batchesCompleted = new NamedTableReference();  
        insert.InsertSpecification = new InsertSpecification();  
        insert.InsertSpecification.Target = batchesCompleted;  
        batchesCompleted.SchemaObject = CreateCompletedBatchesName();  
  
        // Build the columns inserted into  
        ColumnReferenceExpression batchIdColumn = new ColumnReferenceExpression();  
        batchIdColumn.MultiPartIdentifier = new MultiPartIdentifier();  
        batchIdColumn.MultiPartIdentifier.Identifiers.Add(CreateIdentifier(BatchIdColumnName, QuoteType.NotQuoted));  
  
        ColumnReferenceExpression descriptionColumn = new ColumnReferenceExpression();  
        descriptionColumn.MultiPartIdentifier = new MultiPartIdentifier();  
        descriptionColumn.MultiPartIdentifier.Identifiers.Add(CreateIdentifier(DescriptionColumnName, QuoteType.NotQuoted));  
  
        insert.InsertSpecification.Columns.Add(batchIdColumn);  
        insert.InsertSpecification.Columns.Add(descriptionColumn);  
  
        // Build the values inserted  
        ValuesInsertSource valueSource = new ValuesInsertSource();  
        insert.InsertSpecification.InsertSource = valueSource;  
  
        RowValue values = new RowValue();  
        values.ColumnValues.Add(new IntegerLiteral { Value = batchId.ToString() });  
        values.ColumnValues.Add(new StringLiteral { Value = batchDescription });  
        valueSource.RowValues.Add(values);  
  
        return insert;  
    }  
  
    /// <summary>  
    /// This is a helper method that generates an if statement that checks the batches executed  
    /// table to see if the current batch has been executed.  The if statement will look like this  
    ///   
    /// if not exists(select 1 from [tempdb].[dbo].[$(CompletedBatches)]   
    ///                where BatchId = batchId)  
    /// begin  
    /// end  
    /// </summary>  
    /// <param name="batchId"></param>  
    /// <returns></returns>  
    private static IfStatement CreateIfNotExecutedStatement(int batchId)  
    {  
        // Create the exists/select statement  
        ExistsPredicate existsExp = new ExistsPredicate();  
        ScalarSubquery subQuery = new ScalarSubquery();  
        existsExp.Subquery = subQuery;  
  
        subQuery.QueryExpression = new QuerySpecification  
        {  
            SelectElements =  
            {  
                new SelectScalarExpression  { Expression = new IntegerLiteral { Value ="1" } }  
            },  
            FromClause = new FromClause  
            {  
                TableReferences =  
                    {  
                        new NamedTableReference() { SchemaObject = CreateCompletedBatchesName() }  
                    }  
            },  
            WhereClause = new WhereClause  
            {  
                SearchCondition = new BooleanComparisonExpression  
                {  
                    ComparisonType = BooleanComparisonType.Equals,  
                    FirstExpression = new ColumnReferenceExpression  
                    {  
                        MultiPartIdentifier = new MultiPartIdentifier  
                        {  
                            Identifiers = { CreateIdentifier(BatchIdColumnName, QuoteType.SquareBracket) }  
                        }  
                    },  
                    SecondExpression = new IntegerLiteral { Value = batchId.ToString() }  
                }  
            }  
        };  
  
        // Put together the rest of the statement  
        IfStatement ifNotExists = new IfStatement  
        {  
            Predicate = new BooleanNotExpression  
            {  
                Expression = existsExp  
            }  
        };  
  
        return ifNotExists;  
    }  
  
    /// <summary>  
    /// Helper method that generates a useful description of the step.  
    /// </summary>  
    private static void GetStepInfo(  
        DeploymentScriptDomStep domStep,  
        out string stepDescription,  
        out TSqlObject element)  
    {  
        element = null;  
  
        // figure out what type of step we've got, and retrieve  
        // either the source or target element.  
        if (domStep is CreateElementStep)  
        {  
            element = ((CreateElementStep)domStep).SourceElement;  
        }  
        else if (domStep is AlterElementStep)  
        {  
            element = ((AlterElementStep)domStep).SourceElement;  
        }  
        else if (domStep is DropElementStep)  
        {  
            element = ((DropElementStep)domStep).TargetElement;  
        }  
  
        // construct the step description by concatenating the type and the fully qualified  
        // name of the associated element.  
        string stepTypeName = domStep.GetType().Name;  
        if (element != null)  
        {  
            string elementName = GetElementName(element);  
  
            stepDescription = string.Format(CultureInfo.InvariantCulture, "{0} {1}",  
                stepTypeName, elementName);  
        }  
        else  
        {  
            // if the step has no associated element, just use the step type as the description  
            stepDescription = stepTypeName;  
        }  
    }  
  
    private static string GetElementName(TSqlObject element)  
    {  
        StringBuilder name = new StringBuilder();  
        if (element.Name.HasExternalParts)  
        {  
            foreach (string part in element.Name.ExternalParts)  
            {  
                if (name.Length > 0)  
                {  
                    name.Append('.');  
                }  
                name.AppendFormat("[{0}]", part);  
            }  
        }  
  
        foreach (string part in element.Name.Parts)  
        {  
            if (name.Length > 0)  
            {  
                name.Append('.');  
            }  
            name.AppendFormat("[{0}]", part);  
        }  
  
        return name.ToString();  
    }  
  
    ```  
  
2.  Save the changes to SqlRestartableScriptContributor.cs.  
  
Next, you build the class library.  
  
#### To sign and build the assembly  
  
1.  On the **Project** menu, click **MyOtherDeploymentContributor Properties**.  
  
2.  Click the **Signing** tab.  
  
3.  Click **Sign the assembly**.  
  
4.  In **Choose a strong name key file**, click **<New>**.  
  
5.  In the **Create Strong Name Key** dialog box, in **Key file name**, type **MyRefKey**.  
  
6.  (optional) You can specify a password for your strong name key file.  
  
7.  Click **OK**.  
  
8.  On the **File** menu, click **Save All**.  
  
9. On the **Build** menu, click **Build Solution**.  
  
    Next, you must install the assembly so that it will be loaded when you deploy SQL projects.  
  
## <a name="InstallDeploymentContributor"></a>Install a Deployment Contributor  
To install a deployment contributor, you must copy the assembly and associated .pdb file to the Extensions folder.  
  
#### To install the MyOtherDeploymentContributor assembly  
  
1.  Next, you will copy the assembly information to the Extensions directory. When Visual Studio starts, it will identify any extensions in the %Program Files%\Microsoft SQL Server\110\DAC\Bin\Extensions directory and subdirectories, and make them available for use.  
  
2.  Copy the **MyOtherDeploymentContributor.dll** assembly file from the output directory to the %Program Files%\Microsoft SQL Server\110\DAC\Bin\Extensions directory. By default, the path of your compiled .dll file is YourSolutionPath\YourProjectPath\bin\Debug or YourSolutionPath\YourProjectPath\bin\Release.  
  
## <a name="TestDeploymentContributor"></a>Run or Test your Deployment Contributor  
To run or test your deployment contributor, you must perform the following tasks:  
  
-   Add properties to the .sqlproj file that you plan to build.  
  
-   Deploy the database project by using MSBuild and providing the appropriate parameters.  
  
### Add Properties to the SQL Project (.sqlproj) File  
You must always update the SQL project file to specify the ID of the contributors you wish to run. You can do this in one of two ways:  
  
1.  You can manually modify the .sqlproj file to add the required arguments. You might choose to do this if your contributor does not have any contributor arguments required for configuration, or if you do not intend to reuse the build contributor across a large number of projects. If you choose this option, add the following statements to the .sqlproj file after the first Import node in the file:  
  
    ```  
    <PropertyGroup>  
      <DeploymentContributors>  
        $(DeploymentContributors); MyOtherDeploymentContributor.RestartableScriptContributor  
      </DeploymentContributors>  
    </PropertyGroup>  
    ```  
  
2.  The second method is to create a targets file containing the required contributor arguments. This is useful if you are using the same contributor for multiple projects and have contributor arguments required, since it will include the default values. In this case, create a targets file in the MSBuild extensions path:  
  
    1.  Navigate to %Program Files%\MSBuild.  
  
    2.  Create a new folder "MyContributors" where your targets files will be stored.  
  
    3.  Create a new file "MyContributors.targets" inside this directory, add the following text to it and save the file:  
  
        ```  
        <?xml version="1.0" encoding="utf-8"?>  
  
        <Project xmlns="https://schemas.microsoft.com/developer/msbuild/2003">  
          <PropertyGroup>  
            <DeploymentContributors>$(DeploymentContributors);MyOtherDeploymentContributor.RestartableScriptContributor</DeploymentContributors>  
          </PropertyGroup>  
        </Project>  
  
        ```  
  
    4.  Inside the .sqlproj file for any project you want to run contributors, import the targets file by adding the following statement to the .sqlproj file after the \<Import Project="$(MSBuildExtensionsPath)\Microsoft\VisualStudio\v$(VisualStudioVersion)\SSDT\Microsoft.Data.Tools.Schema.SqlTasks.targets" \/> node in the file :  
  
        ```  
        <Import Project="$(MSBuildExtensionsPath)\MyContributors\MyContributors.targets " />  
  
        ```  
  
After you have followed one of these approaches, you can use MSBuild to pass in the parameters for command-line builds.  
  
> [!NOTE]  
> You must always update the "DeploymentContributors" property to specify your contributor ID. This is the same ID used in the "ExportDeploymentPlanModifier" attribute in your contributor source file. Without this your contributor will not be run when building the project. The "ContributorArguments" property needs to be updated only if you have arguments required for your contributor to run.  
  
## Deploy the Database Project  
  
#### To deploy your SQL project and generate a deployment report  
  
-   Your project can be published or deployed as normal inside Visual Studio. Simply open a solution containing your SQL project and choose the Publish... option from the right-click context menu for the project, or use F5 for a debug deployment to LocalDB. In this example we will use the "Publish..." dialog to generate a deployment script.  
  
    1.  Open Visual Studio and open the solution containing your SQL Project.  
  
    2.  Right-click on the project in Solution Explorer and choose the **Publish...** option.  
  
    3.  Set the server name and database name to publish to.  
  
    4.  Choose **Generate Script** from the options at the bottom of the dialog. This will create a script that can be used for deployment. We will examine this to verify that our IF statements have been added in order to make the script restartable.  
  
    5.  Examine the resulting deployment script. Just before the section labeled "Pre-Deployment Script Template", you should see something that resembles the following Transact-SQL syntax:  
  
        ```  
        :setvar CompletedBatches __completedBatches_CompareProjectDB_cd1e348a-8f92-44e0-9a96-d25d65900fca  
        :setvar TotalBatchCount 17  
        GO  
  
        if OBJECT_ID(N'tempdb.dbo.$(CompletedBatches)', N'U') is null  
        begin  
        use tempdb  
        create table [dbo].[$(CompletedBatches)]  
        (  
        BatchId int primary key,  
        Description nvarchar(300)  
        )  
        use [$(DatabaseName)]  
        end  
  
        ```  
  
        Later in the deployment script, around each batch, you see an IF statement that surrounds the original statement. For example, the following might appear for a CREATE SCHEMA statement:  
  
        ```  
        IF NOT EXISTS (SELECT 1  
                       FROM   [tempdb].[dbo].[$(CompletedBatches)]  
                       WHERE  [BatchId] = 0)  
            BEGIN  
                EXECUTE sp_executesql @stmt = N'CREATE SCHEMA [Sales]  
            AUTHORIZATION [dbo]';  
                SET NOCOUNT ON;  
                INSERT  [tempdb].[dbo].[$(CompletedBatches)] (BatchId, Description)  
                VALUES                                      (0, N'CreateElementStep Sales batch 0');  
                SET NOCOUNT OFF;  
            END  
  
        ```  
  
        Notice that CREATE SCHEMA is one of the statements that must be enclosed within an EXECUTE sp_executesql statement within the IF statement. Statements such as CREATE TABLE do not require the EXECUTE sp_executesql statement and will resemble the following example:  
  
        ```  
        IF NOT EXISTS (SELECT 1  
                       FROM   [tempdb].[dbo].[$(CompletedBatches)]  
                       WHERE  [BatchId] = 1)  
            BEGIN  
                CREATE TABLE [Sales].[Customer] (  
                    [CustomerID]   INT           IDENTITY (1, 1) NOT NULL,  
                    [CustomerName] NVARCHAR (40) NOT NULL,  
                    [YTDOrders]    INT           NOT NULL,  
                    [YTDSales]     INT           NOT NULL  
                );  
                SET NOCOUNT ON;  
                INSERT  [tempdb].[dbo].[$(CompletedBatches)] (BatchId, Description)  
                VALUES                                      (1, N'CreateElementStep Sales.Customer batch 0');  
                SET NOCOUNT OFF;  
            END  
  
        ```  
  
        > [!NOTE]  
        > If you deploy a database project that is identical to the target database, the resulting report will not be very meaningful. For more meaningful results, either deploy changes to a database or deploy a new database.  
  
## Command-line deployment using generated dacpac file  
Once a SQL project has been built, a dacpac file is created that can be used to deploy the schema from the command line, and which can enable deployment from a different machine such as a build machine. SqlPackage is a command-line utility that enables deployment of dacpacs with a full range of options that enable users to deploy a dacpac or generate a deployment script, among other actions. For more information, see [SqlPackage.exe](https://msdn.microsoft.com/library/hh550080(v=VS.103).aspx).  
  
> [!NOTE]  
> To successfully deploy dacpacs built from projects with the DeploymentContributors property defined, the DLL(s) containing your deployment contributor(s) must be installed on the machine being used. This is because they have been marked as required for the deployment to complete successfully.  
>   
> To avoid this requirement, exclude the deployment contributor from the .sqlproj file. Instead, specify contributors to run during deployment using SqlPackage with the **AdditionalDeploymentContributors** parameter. This is useful in cases where you only wish to use a contributor for special circumstances, like deploying to a specific server.  
  
## Next Steps  
You can experiment with other types of modifications to deployment plans before they are executed. Some other types of modifications that you might want to make include:  
  
-   Adding an extended property to all database objects that associate a version number with them.  
  
-   Adding or removing additional diagnostic print statements or comments from deployment scripts.  
  
## See Also  
[Customize Database Build and Deployment by Using Build and Deployment Contributors](../ssdt/use-deployment-contributors-to-customize-database-build-and-deployment.md)  
[Walkthrough: Extend Database Project Build to Generate Model Statistics](../ssdt/walkthrough-extend-database-project-build-to-generate-model-statistics.md)  
[Walkthrough: Extend Database Project Deployment to Analyze the Deployment Plan](../ssdt/walkthrough-extend-database-project-deployment-to-analyze-the-deployment-plan.md)  
  
