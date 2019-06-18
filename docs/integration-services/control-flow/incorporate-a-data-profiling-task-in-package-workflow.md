---
title: "Incorporate a Data Profiling Task in Package Workflow | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Data Profiling task [Integration Services], using output in workflow"
ms.assetid: 39a51586-6977-4c45-b80b-0157a54ad510
author: janinezhang
ms.author: janinez
manager: craigg
---
# Incorporate a Data Profiling Task in Package Workflow

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Data profiling and cleanup are not candidates for an automated process in their early stages. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], the output of the Data Profiling task usually requires visual analysis and human judgment to determine whether reported violations are meaningful or excessive. Even after recognizing data quality problems, there still has to be a carefully thought-out plan that addresses the best approach for cleanup.  
  
 However, after criteria for data quality have been established, you might want to automate a periodic analysis and cleanup of the data source. Consider these scenarios:  
  
-   **Checking data quality before an incremental load**. Use the Data Profiling task to compute the Column Null Ratio Profile of new data intended for the CustomerName column in a Customers table. If the percentage of null values is greater than 20%, send an e-mail message that contains the profile output to the operator and end the package. Otherwise, continue the incremental load.  
  
-   **Automating cleanup when the specified conditions are met**. Use the Data Profiling task to compute the Value Inclusion Profile of the State column against a lookup table of states, and of the ZIP Code/Postal Code column against a lookup table of zip codes. If the inclusion strength of the state values is less than 80%, but the inclusion strength of the ZIP Code/Postal Code values is greater than 99%, this indicates two things. First, the state data is bad. Second, the ZIP Code/Postal Code data is good. Launch a Data Flow task that cleans up the state data by performing a lookup of the correct state value from the current Zip Code/Postal Code value.  
  
 After you have a workflow into which you can incorporate the Data Flow task, you have to understand the steps that are required to add this task. The next section describes the general process of incorporating the Data Flow task. The final two sections describe how to connect the Data Flow task either directly to a data source or to transformed data from the Data Flow.  
  
## Defining a General Workflow for the Data Flow Task  
 The following procedure outlines the general approach for using the output of the Data Profiling task in the workflow of a package.  
  
#### To use the output of the Data Profiling task programmatically in a package  
  
1.  Add and configure the Data Profiling task in a package.  
  
2.  Configure package variables to hold the values that you want to retrieve from the profile results.  
  
3.  Add and configure a Script task. Connect the Script task to the Data Profiling task. In the Script task, write code that reads the desired values from the output file of the Data Profiling task and populates the package variables.  
  
4.  In the precedence constraints that connect the Script task to downstream branches in the workflow, write expressions that use the values of the variables to direct the workflow.  
  
 When incorporating the Data Profiling task into the workflow of a package, keep these two features of the task in mind:  
  
-   **Task output**. The Data Profiling task writes its output to a file or a package variable in XML format according to the DataProfile.xsd schema. Therefore, you have to query the XML output if you want to use the profile results in the conditional workflow of a package. You can easily use the Xpath query language to query this XML output. To study the structure of this XML output, you can open a sample output file or the schema itself. To open the output file or schema, you can use [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)], another XML editor, or a text editor, such as Notepad.  
  
    > [!NOTE]  
    >  Some of the profile results that are displayed in the Data Profile Viewer are calculated values that are not directly found in the output. For example, the output of the Column Null Ratio Profile contains the total number of rows and the number of rows that contain null values. You have to query these two values, and then calculate the percentage of rows that contain null values, to obtain the column null ratio.  
  
-   **Task input**. The Data Profiling task reads its input from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables. Therefore, you have to save data that is in memory to staging tables if you want to profile data that has already been loaded and transformed in the data flow.  
  
 The following sections apply this general workflow to profiling data that comes directly from an external data source or that comes transformed from the Data Flow task. These sections also show how to handle the input and output requirements of the Data Flow task.  
  
## Connecting the Data Profiling Task Directly to an External Data Source  
 The Data Profiling task can profile data that comes directly from a data source.  To illustrate this capability, the following example uses the Data Profiling task to compute a Column Null Ratio Profile on the columns of the Person.Address table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database. Then, this example uses a Script task to retrieve the results from the output file and populate package variables that can be used to direct workflow.  
  
> [!NOTE]  
>  The AddressLine2 column was selected for this simple example because this column contains a high percentage of null values.  
  
 This example consists of the following steps:  
  
-   Configuring the connection managers that connect to the external data source and to the output file that will contain the profile results.  
  
-   Configuring the package variables that will hold the values needed by the Data Profiling task.  
  
-   Configuring the Data Profiling task to compute the Column Null Ratio Profile.  
  
-   Configuring the Script task to work the XML output from the Data Profiling task.  
  
-   Configuring the precedence constraints that will control which downstream branches in the workflow are run based on the results of the Data Profiling task.  
  
### Configure the Connection Managers  
 For this example, there are two connection managers:  
  
-   An [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager that connects to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
-   A File connection manager that creates the output file that will hold the results of the Data Profiling task.  
  
##### To configure the connection managers  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], create a new [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package.  
  
2.  Add an [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager to the package. Configure this connection manager to use the NET Data Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (SqlClient) and to connect to an available instance of the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
     By default, the connection manager has the following name: \<server name>.AdventureWorks1.  
  
3.  Add a File connection manager to the package. Configure this connection manager to create the output file for the Data Profiling task.  
  
     This example uses the file name, DataProfile1.xml. By default, the connection manager has the same name as the file.  
  
### Configure the Package Variables  
 This example uses two package variables:  
  
-   The ProfileConnectionName variable passes the name of the File connection manager to the Script task.  
  
-   The AddressLine2NullRatio variable passes the calculated null ratio for this column out of the Script task to the package.  
  
##### To configure the package variables that will hold profile results  
  
-   In the **Variables** window, add and configure the following two package variables:  
  
    -   Enter the name, **ProfileConnectionName**, for one of the variables and set the type of this variable to **String**.  
  
    -   Enter the name, **AddressLine2NullRatio**, for the other variable and set the type of this variable to **Double**.  
  
### Configure the Data Profiling Task  
 The Data Profiling task has to be configured in the following way:  
  
-   To use the data that the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager supplies as input.  
  
-   To perform a Column Null Ratio profile on the input data.  
  
-   To save the profile results to the file that is associated with the File connection manager.  
  
##### To configure the Data Profiling task  
  
1.  To the Control Flow, add a Data Profiling task.  
  
2.  Open the **Data Profiling Task Editor** to configure the task.  
  
3.  On the **General** page of the editor, for **Destination**, select the name of the File connection manager that you have previously configured.  
  
4.  On the **Profile Requests** page of the editor, create a new Column Null Ratio Profile.  
  
5.  In the **Request properties** pane, for **ConnectionManager**, select the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager that you have previously configured. Then, for **TableOrView**, select Person.Address.  
  
6.  Close the Data Profiling Task Editor.  
  
### Configure the Script Task  
 The Script task has to be configured to retrieve the results from the output file and populate the package variables that were previously configured.  
  
##### To configure the Script task  
  
1.  To the Control Flow, add a Script task.  
  
2.  Connect the Script task to the Data Profiling task.  
  
3.  Open the **Script Task Editor** to configure the task.  
  
4.  On the **Script** page, select your preferred programming language. Then, make the two package variables available to the script:  
  
    1.  For **ReadOnlyVariables**, select **ProfileConnectionName**.  
  
    2.  For **ReadWriteVariables**, select **AddressLine2NullRatio**.  
  
5.  Select **Edit Script** to open the script development environment.  
  
6.  Add a reference to the System.Xml namespace.  
  
7.  Enter the sample code that corresponds to your programming language:  
  
    ```vb  
    Imports System  
    Imports Microsoft.SqlServer.Dts.Runtime  
    Imports System.Xml  
  
    Public Class ScriptMain  
  
      Private FILENAME As String = "C:\ TEMP\DataProfile1.xml"  
      Private PROFILE_NAMESPACE_URI As String = "https://schemas.microsoft.com/DataDebugger/"  
      Private NULLCOUNT_XPATH As String = _  
        "/default:DataProfile/default:DataProfileOutput/default:Profiles" & _  
        "/default:ColumnNullRatioProfile[default:Column[@Name='AddressLine2']]/default:NullCount/text()"  
      Private TABLE_XPATH As String = _  
        "/default:DataProfile/default:DataProfileOutput/default:Profiles" & _  
        "/default:ColumnNullRatioProfile[default:Column[@Name='AddressLine2']]/default:Table"  
  
      Public Sub Main()  
  
        Dim profileConnectionName As String  
        Dim profilePath As String  
        Dim profileOutput As New XmlDocument  
        Dim profileNSM As XmlNamespaceManager  
        Dim nullCountNode As XmlNode  
        Dim nullCount As Integer  
        Dim tableNode As XmlNode  
        Dim rowCount As Integer  
        Dim nullRatio As Double  
  
        ' Open output file.  
        profileConnectionName = Dts.Variables("ProfileConnectionName").Value.ToString()  
        profilePath = Dts.Connections(profileConnectionName).ConnectionString  
        profileOutput.Load(profilePath)  
        profileNSM = New XmlNamespaceManager(profileOutput.NameTable)  
        profileNSM.AddNamespace("default", PROFILE_NAMESPACE_URI)  
  
        ' Get null count for column.  
        nullCountNode = profileOutput.SelectSingleNode(NULLCOUNT_XPATH, profileNSM)  
        nullCount = CType(nullCountNode.Value, Integer)  
  
        ' Get row count for table.  
        tableNode = profileOutput.SelectSingleNode(TABLE_XPATH, profileNSM)  
        rowCount = CType(tableNode.Attributes("RowCount").Value, Integer)  
  
        ' Compute and return null ratio.  
        nullRatio = nullCount / rowCount  
        Dts.Variables("AddressLine2NullRatio").Value = nullRatio  
  
        Dts.TaskResult = Dts.Results.Success  
  
      End Sub  
  
    End Class  
    ```  
  
    ```csharp  
    using System;  
    using Microsoft.SqlServer.Dts.Runtime;  
    using System.Xml;  
  
    public class ScriptMain  
    {  
  
      private string FILENAME = "C:\\ TEMP\\DataProfile1.xml";  
      private string PROFILE_NAMESPACE_URI = "https://schemas.microsoft.com/DataDebugger/";  
      private string NULLCOUNT_XPATH = "/default:DataProfile/default:DataProfileOutput/default:Profiles" + "/default:ColumnNullRatioProfile[default:Column[@Name='AddressLine2']]/default:NullCount/text()";  
      private string TABLE_XPATH = "/default:DataProfile/default:DataProfileOutput/default:Profiles" + "/default:ColumnNullRatioProfile[default:Column[@Name='AddressLine2']]/default:Table";  
  
      public void Main()  
      {  
  
        string profileConnectionName;  
        string profilePath;  
        XmlDocument profileOutput = new XmlDocument();  
        XmlNamespaceManager profileNSM;  
        XmlNode nullCountNode;  
        int nullCount;  
        XmlNode tableNode;  
        int rowCount;  
        double nullRatio;  
  
        // Open output file.  
        profileConnectionName = Dts.Variables["ProfileConnectionName"].Value.ToString();  
        profilePath = Dts.Connections[profileConnectionName].ConnectionString;  
        profileOutput.Load(profilePath);  
        profileNSM = new XmlNamespaceManager(profileOutput.NameTable);  
        profileNSM.AddNamespace("default", PROFILE_NAMESPACE_URI);  
  
        // Get null count for column.  
        nullCountNode = profileOutput.SelectSingleNode(NULLCOUNT_XPATH, profileNSM);  
        nullCount = (int)nullCountNode.Value;  
  
        // Get row count for table.  
        tableNode = profileOutput.SelectSingleNode(TABLE_XPATH, profileNSM);  
        rowCount = (int)tableNode.Attributes["RowCount"].Value;  
  
        // Compute and return null ratio.  
        nullRatio = nullCount / rowCount;  
        Dts.Variables["AddressLine2NullRatio"].Value = nullRatio;  
  
        Dts.TaskResult = Dts.Results.Success;  
  
      }  
  
    }  
    ```  
  
    > [!NOTE]  
    >  The sample code shown in this procedure demonstrates how to load the output of the Data Profiling task from a file. To load the output of the Data Profiling task from a package variable instead, see the alternative sample code that follows this procedure.  
  
8.  Close the script development environment, and then close the Script Task Editor.  
  
#### Alternative Code-Reading the Profile Output from a Variable  
 The previous procedure shows how to load the output of the Data Profiling task from a file. However, an alternative method would be to load this output from a package variable. To load the output from a variable, you have to make the following changes to the sample code:  
  
-   Call the **LoadXml** method of the **XmlDocument** class instead of the **Load** method.  
  
-   In the Script Task Editor, add the name of the package variable that contains the profile output to the task's **ReadOnlyVariables** list.  
  
-   Pass the string value of the variable to the **LoadXML** method, as shown in the following code example. (This example uses "ProfileOutput" as the name of the package variable that contains the profile output.)  
  
    ```vb  
    Dim outputString As String  
    outputString = Dts.Variables("ProfileOutput").Value.ToString()  
    ...  
    profileOutput.LoadXml(outputString)  
    ```  
  
    ```csharp  
    string outputString;  
    outputString = Dts.Variables["ProfileOutput"].Value.ToString();  
    ...  
    profileOutput.LoadXml(outputString);  
    ```  
  
### Configure the Precedence Constraints  
 The precedence constraints have to be configured to control which downstream branches in the workflow are run based on the results of the Data Profiling task.  
  
##### To configure the precedence constraints  
  
-   In the precedence constraints that connect the Script task to downstream branches in the workflow, write expressions that use the values of the variables to direct the workflow.  
  
     For example, you might set the **Evaluation operation** of the precedence constraint to **Expression and Constraint**. Then, you might use `@AddressLine2NullRatio < .90` as the value of the expression. This causes the workflow to follow the selected path when the previous tasks succeed, and when the percentage of null values in the selected column is less than 90%.  
  
## Connecting the Data Profiling Task to Transformed Data from the Data Flow  
 Instead of profiling data directly from a data source, you can profile data that has already been loaded and transformed in the data flow. However, the Data Profiling task works only against persisted data, not against in-memory data. Therefore, you must first use a destination component to save the transformed data to a staging table.  
  
> [!NOTE]  
>  When you configure the Data Profiling task, you have to select existing tables and columns. Therefore, you must create the staging table at design time before you can configure the task. In other words, this scenario does not allow you to use a temporary table that is created at run time.  
  
 After saving the data to a staging table, you can do the following actions:  
  
-   Use the Data Profiling task to profile the data.  
  
-   Use a Script task to read the results as described earlier in this topic.  
  
-   Use those results to direct the subsequent workflow of the package.  
  
 The following procedure provides the general approach for using the Data Profiling task to profile data that has been transformed by the data flow. Many of these steps are similar to the ones described earlier for profiling data that comes directly from an external data source. You might want to review those earlier steps for more information about how to configure the various components.  
  
#### To use the Data Profiling task in the data flow  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], create a package.  
  
2.  In the Data Flow, add, configure, and connect the appropriate sources and transformations.  
  
3.  In the Data Flow, add, configure, and connect a destination component that saves the transformed data to a staging table.  
  
4.  In the Control Flow, add and configure a Data Profiling task that computes the desired profiles against the transformed data in the staging table. Connect the Data Profiling task to the Data Flow task.  
  
5.  Configure package variables to hold the values that you want to retrieve from the profile results.  
  
6.  Add and configure a Script task. Connect the Script task to the Data Profiling task. In the Script task, write code that reads the desired values from the output of the Data Profiling task and populates the package variables.  
  
7.  In the precedence constraints that connect the Script task to downstream branches in the workflow, write expressions that use the values of the variables to direct the workflow.  
  
## See Also  
 [Setup of the Data Profiling Task](../../integration-services/control-flow/setup-of-the-data-profiling-task.md)   
 [Data Profile Viewer](../../integration-services/control-flow/data-profile-viewer.md)  
  
  
