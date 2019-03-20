---
title: "Foreach Loop Container | Microsoft Docs"
ms.custom: ""
ms.date: "08/22/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.foreachloopcontainer.f1"
  - "sql13.dts.designer.foreachloopcontainer.general.f1"
  - "sql13.dts.designer.foreachloopcontainer.collection.f1"
  - "sql13.dts.designer.foreachloopcontainer.mapping.f1"
  - "sql13.dts.designer.schemarestrictions.f1"
  - "sql13.dts.designer.foreachitemcolumns.f1"
  - "sql13.dts.designer.selectsmoenumeration.f1"
  - "sql14.dts.designer.foreachloopcontainer.f1"
  - "sql14.dts.designer.foreachloopcontainer.general.f1"
  - "sql14.dts.designer.foreachloopcontainer.collection.f1"
  - "sql14.dts.designer.foreachloopcontainer.mapping.f1"
  - "sql14.dts.designer.schemarestrictions.f1"
  - "sql14.dts.designer.foreachitemcolumns.f1"
  - "sql14.dts.designer.selectsmoenumeration.f1"
helpviewer_keywords: 
  - "repeating control flow"
  - "Foreach Loop containers"
  - "foreach enumerators [Integration Services]"
  - "containers [Integration Services], Foreach Loop"
ms.assetid: dd6cc2ba-631f-4adf-89dc-29ef449c6933
author: janinezhang
ms.author: janinez
manager: craigg
---
# Foreach Loop Container
  The Foreach Loop container defines a repeating control flow in a package. The loop implementation is similar to **Foreach** looping structure in programming languages. In a package, looping is enabled by using a Foreach enumerator.  The Foreach Loop container repeats the control flow for each member of a specified enumerator.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides the following enumerator types:  
  
-   Foreach ADO enumerator to enumerate rows in tables. For example, you can get the rows in an ADO recordset.  
  
     The Recordset destination saves data in memory in a recordset that is stored in a package variable of **Object** data type. You typically use a Foreach Loop container with the Foreach ADO enumerator to process one row of the recordset at a time. The variable specified for the Foreach ADO enumerator must be of Object data type. For more information about the Recordset destination, see [Use a Recordset Destination](../../integration-services/data-flow/use-a-recordset-destination.md).  
  
-   Foreach ADO.NET Schema Rowset enumerator to enumerate the schema information about a data source. For example, you can enumerate and get a list of the tables in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
  
-   Foreach File enumerator to enumerate files in a folder. The enumerator can traverse subfolders. For example, you can read all the files that have the *.log file name extension in the Windows folder and its subfolders.  
  
-   Foreach From Variable enumerator to enumerate the enumerable object that a specified variable contains. The enumerable object can be an array, an ADO.NET **DataTable**, an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] enumerator, and so on. For example, you can enumerate the values of an array that contains the name of servers.  
  
-   Foreach Item enumerator to enumerate items that are collections. For example, you can enumerate the names of executables and working directories that an Execute Process task uses.  
  
-   Foreach Nodelist enumerator to enumerate the result set of an XML Path Language (XPath) expression. For example, this expression enumerates and gets a list of all the authors in the classical period: `/authors/author[@period='classical']`.  
  
-   Foreach SMO enumerator to enumerate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Objects (SMO) objects. For example, you can enumerate and get a list of the views in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
  
-   Foreach HDFS File Enumerator to enumerate HDFS files in the specified HDFS location.  
  
-   Foreach Azure Blob enumerator to enumerate blobs in a blob container in Azure Storage.  

-   Foreach ADLS File enumerator to enumerate files in a directory in Azure Data Lake Store.
  
 The following diagram shows a Foreach Loop container that has a File System task. The Foreach loop uses the Foreach File enumerator, and the File System task is configured to copy a file. If the folder that the enumerator specifies contains four files, the loop repeats four times and copies four files.  
  
 ![A Foreach Loop container that enumerates a folder](../../integration-services/control-flow/media/ssis-foreachloop.gif "A Foreach Loop container that enumerates a folder")  
  
 You can use a combination of variables and property expressions to update the property of the package object with the enumerator collection value. First you map the collection value to a user-defined variable, and then you implement a property expression on the property that uses the variable. For example, the collection value of the Foreach File enumerator is mapped to a variable called **MyFile** and the variable is then used in the property expression for the Subject property of a Send Mail task. When the package runs, the Subject property is updated with the name of a file each time that the loop repeats. For more information, see [Use Property Expressions in Packages](../../integration-services/expressions/use-property-expressions-in-packages.md).  
  
 Variables that are mapped to the enumerator collection value can also be used in expressions and scripts.  
  
 A Foreach Loop container can include multiple tasks and containers, but it can use only one type of enumerator. If the Foreach Loop container includes multiple tasks, you can map the enumerator collection value to multiple properties of each task.  
  
 You can set a transaction attribute on the Foreach Loop container to define a transaction for a subset of the package control flow. In this way, you can manage transactions at the level of the Foreach Loop instead of the package level. For example, if a Foreach Loop container repeats a control flow that updates dimensions and fact tables in a star schema, you can configure a transaction to ensure that all fact tables are updated successfully, or none are updated. For more information, see [Integration Services Transactions](../../integration-services/integration-services-transactions.md).  
  
## Enumerator Types  
 Enumerators are configurable, and you must provide different information, depending on the enumerator.  
  
 The following table summarizes the information each enumerator type requires.  
  
|Enumerator|Configuration requirements|  
|----------------|--------------------------------|  
|Foreach ADO|Specify the ADO object source variable and the enumerator mode. The variable must be of Object data type.|  
|Foreach ADO.NET Schema Rowset|Specify the connection to a database and the schema to enumerate.|  
|Foreach File|Specify a folder and the files to enumerate, the format of the file name of the retrieved files, and whether to traverse subfolders.|  
|Foreach From Variable|Specify the variable that contains the objects to enumerate.|  
|Foreach Item|Define the items in the Foreach Item collection, including columns and column data types.|  
|Foreach Nodelist|Specify the source of the XML document and configure the XPath operation.|  
|Foreach SMO|Specify the connection to a database and the SMO objects to enumerate.|  
|Foreach HDFS File Enumerator|Specify a folder and the files to enumerate, the format of the file name of the retrieved files, and whether to traverse subfolders.|  
|Foreach Azure Blob|Specify the Azure blob container that containers blobs to be enumerated.|  
|Foreach ADLS File|Specify the Azure Data Lake Store directory that contains the files to be enumerated.|

## Add enumeration to a control flow with a Foreach Loop container
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes the Foreach Loop container, a control flow element that makes it simple to include a looping construct that enumerates files and objects in the control flow of a package. For more information, see [Foreach Loop Container](../../integration-services/control-flow/foreach-loop-container.md).  
  
 The Foreach Loop container provides no functionality; it provides only the structure in which you build the repeatable control flow, specify an enumerator type, and configure the enumerator. To provide container functionality, you must include at least one task in the Foreach Loop container. For more information, see [Integration Services Tasks](../../integration-services/control-flow/integration-services-tasks.md).  
  
 The Foreach Loop container can include a control flow with multiple tasks and other containers. Adding tasks and containers to a Foreach Loop container is similar to adding them to a package, except you drag the tasks and containers to the Foreach Loop container instead of to the package. If the Foreach Loop container includes more than one task or container, you can connect them using precedence constraints just as you do in a package. For more information, see [Precedence Constraints](../../integration-services/control-flow/precedence-constraints.md).  
  
### Add and configure a Foreach Loop container
  
1.  Add the Foreach Loop container to the package. For more information, see [Add or Delete a Task or a Container in a Control Flow](../../integration-services/control-flow/add-or-delete-a-task-or-a-container-in-a-control-flow.md).  
  
2.  Add tasks and containers to the Foreach Loop container. For more information, see [Add or Delete a Task or a Container in a Control Flow](../../integration-services/control-flow/add-or-delete-a-task-or-a-container-in-a-control-flow.md).  
  
3.  Connect tasks and containers in the Foreach Loop container using precedence constraints. For more information, see [Connect Tasks and Containers by Using a Default Precedence Constraint](https://msdn.microsoft.com/library/8f31f15f-98ff-4c35-b41f-8b8cfd148d75).  
  
4.  Configure the Foreach Loop container. For more information, see [Configure a Foreach Loop Container](https://msdn.microsoft.com/library/519c6f96-5e1f-47d2-b96a-d49946948c25).  

## Configure a Foreach Loop Container
This procedure describes how to configure a Foreach Loop container, including property expressions at the enumerator and container levels.  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  Click the **Control Flow** tab and double-click the Foreach Loop.  
  
3.  In the **Foreach Loop Editor** dialog box, click **General** and, optionally, modify the name and description of the Foreach Loop.  
  
4.  Click **Collection** and select an enumerator type from the **Enumerator** list.  
  
5.  Specify an enumerator and set enumerator options as follows:  
  
    -   To use the Foreach File enumerator, provide the folder that contains the files to enumerate, specify a filter for the file name and type, and specify whether the fully qualified file name should be returned. Also, indicate whether to recurse through subfolders for more files.  
  
    -   To use the Foreach Item enumerator, click **Columns**, and, in the **For Each Item Columns** dialog box, click **Add** to add columns. Select a data type in the **Data Type** list for each column, and click **OK**.  
  
         Type values in the columns or select values from lists.  
  
        > [!NOTE]  
        >  To add a new row, click anywhere outside the cell in which you typed.  
  
        > [!NOTE]  
        >  If a value is not compatible with the column data type, the text is highlighted.  
  
    -   To use the Foreach ADO enumerator, select an existing variable or click **New variable** in the **ADO object source variable** list to specify the variable that contains the name of the ADO object to enumerate, and select an enumeration mode option.  
  
         If creating a new variable, set the variable properties in the **Add Variable** dialog box.  
  
    -   To use the Foreach ADO.NET Schema Rowset enumerator, select an existing ADO.NET connection or click **New connection** in the **Connection** list, and then select a schema.  
  
         Optionally, click **Set Restrictions** and select schema restrictions, select the variable that contains the restriction value or type the restriction value, and click **OK**.  
  
    -   To use the Foreach From Variable enumerator, select a variable in the **Variable** list.  
  
    -   To use the Foreach NodeList enumerator, click DocumentSourceType and select the source type from the list, and then click DocumentSource. Depending on the value selected for DocumentSourceType, select a variable or a file connection from the list, create a new variable or file connection, or type the XML source in the **Document Source Editor**.  
  
         Next, click EnumerationType and select an enumeration type from the list. If EnumerationType is **Navigator, Node, or NodeText**, click OuterXPathStringSourceType and select the source type, and then click OuterXPathString. Depending on the value set for OuterXPathStringSourceType, select a variable or a file connection from the list, create a new variable or file connection, or type the string for the outer XML Path Language (XPath) expression.  
  
         If EnumerationType is **ElementCollection**, set OuterXPathStringSourceType and OuterXPathString as described above. Then, click InnerElementType and select an enumeration type for the inner elements, and then click InnerXPathStringSourceType. Depending on the value set for InnerXPathStringSourceType, select a variable or a file connection, create a new variable or file connection, or type the string for the inner XPath expression.  
  
    -   To use the Foreach SMO enumerator, select an existing ADO.NET connection or click **New connection** in the **Connection** list, and then either type the string to use or click **Browse**. If you click **Browse**, in the **Select SMO Enumeration** dialog box, select the object type to enumerate and the enumeration type, and click **OK**.  
  
6.  Optionally, click the browse button **(...)** in the **Expressions** text box on the **Collection** page to create expressions that update property values. For more information, see [Add or Change a Property Expression](../../integration-services/expressions/add-or-change-a-property-expression.md).  
  
    > [!NOTE]  
    >  The properties listed in the **Property** list vary by enumerator.  
  
7.  Optionally, click **Variable Mappings** to map object properties to the collection value, and then do the following things:  
  
    1.  In the **Variables** list, select a variable or click **\<New Variable>** to create a new variable.  
  
    2.  If you add a new variable, set the variable properties in the **Add Variable** dialog box and click **OK**.  
  
    3.  If you use the For Each Item enumerator, you can update the index value in the **Index** list.  
  
        > [!NOTE]  
        >  The index value indicates which column in the item to map to the variable. Only the For Each Item enumerator can use an index value other than 0.  
  
8.  Optionally, click **Expressions** and, on the **Expressions** page, create property expressions for the properties of the Foreach Loop container. For more information, see [Add or Change a Property Expression](../../integration-services/expressions/add-or-change-a-property-expression.md).  
  
9. Click **OK**.  

## General Page - Foreach Loop Editor
Use the **General** page of the **Foreach Loop Editor** dialog box to name and describe a Foreach Loop container that uses a specified enumerator to repeat a workflow for each member in a collection.  
  
 To learn about the Foreach Loop container and how to configure it, see [Foreach Loop Container](../../integration-services/control-flow/foreach-loop-container.md) and [Configure a Foreach Loop Container](https://msdn.microsoft.com/library/519c6f96-5e1f-47d2-b96a-d49946948c25).  
  
### Options  
 **Name**  
 Provide a unique name for the Foreach Loop container. This name is used as the label in the task icon and in the logs.  
  
> [!NOTE]  
>  Object names must be unique within a package.  
  
 **Description**  
 Type a description of the Foreach Loop container.  

## Collection Page - Foreach Loop Editor
 Use the **Collection** page of the **Foreach Loop Editor** dialog box to specify the enumerator type and configure the enumerator.  
  
 To learn about the Foreach Loop container and how to configure it, see [Foreach Loop Container](../../integration-services/control-flow/foreach-loop-container.md) and [Configure a Foreach Loop Container](https://msdn.microsoft.com/library/519c6f96-5e1f-47d2-b96a-d49946948c25).  
  
### Static Options  
 **Enumerator**  
 Select the enumerator type from the list. This property has the options listed in the following table:  
  
|Value|Description|  
|-----------|-----------------|  
|**Foreach File Enumerator**|Enumerate files. Selecting this value displays the dynamic options in the section, **Foreach File Enumerator**.|  
|**Foreach Item Enumerator**|Enumerate values in an item. Selecting this value displays the dynamic options in the section, **Foreach Item Enumerator**.|  
|**Foreach ADO Enumerator**|Enumerate tables or rows in tables. Selecting this value displays the dynamic options in the section, **Foreach ADO Enumerator**.|  
|**Foreach ADO.NET Schema Rowset Enumerator**|Enumerate a schema. Selecting this value displays the dynamic options in the section, **Foreach ADO.NET Enumerator**.|  
|**Foreach From Variable Enumerator**|Enumerate the value in a variable. Selecting this value displays the dynamic options in the section, **Foreach From Variable Enumerator**.|  
|**Foreach Nodelist Enumerator**|Enumerate nodes in an XML document. Selecting this value displays the dynamic options in the section, **Foreach Nodelist Enumerator**.|  
|**Foreach SMO Enumerator**|Enumerate a SMO object. Selecting this value displays the dynamic options in the section, **Foreach SMO Enumerator**.|  
|**Foreach HDFS File Enumerator**|Enumerate HDFS files in the specified HDFS location. Selecting this value displays the dynamic options in the section, **Foreach HDFS File Enumerator**.|  
|**Foreach Azure Blob Enumerator**|Enumerate blob files in the specified blob location. Selecting this value displays the dynamic options in the section, **Foreach Azure Blob Enumerator**.|  
|**Foreach ADLS File Enumerator**|Enumerate files in the specified Data Lake Store directory. Selecting this value displays the dynamic options in the section, **Foreach ADLS File Enumerator**.|
  
 **Expressions**  
 Click or expand **Expressions** to view the list of existing property expressions. Click the ellipsis button **(...)** to add a property expression for an enumerator property, or edit and evaluate an existing property expression.  
  
 **Related Topics:**  [Integration Services &#40;SSIS&#41; Expressions](../../integration-services/expressions/integration-services-ssis-expressions.md), [Property Expressions Editor](../../integration-services/expressions/property-expressions-editor.md), [Expression Builder](../../integration-services/expressions/expression-builder.md)  
  
### Enumerator Dynamic Options  
  
#### Enumerator = Foreach File Enumerator  
 You use the Foreach File enumerator to enumerate files in a folder. For example, if the Foreach Loop includes an Execute SQL task, you can use the Foreach File enumerator to enumerate files that contain SQL statements that the Execute SQL task runs. The enumerator can be configured to include subfolders.  
  
 The content of the folders and subfolders that the Foreach File enumerator enumerates might change while the loop is executing because external processes or tasks in the loop add, rename, or delete files while the loop is executing. These changes may cause a number of unexpected situations:  
  
-   If files are deleted, the actions of one task in the Foreach Loop may affect a different set of files than the files used by subsequent tasks.  
  
-   If files are renamed and an external process automatically adds files to replace the renamed files, the actions of tasks in the Foreach Loop may affect the same files twice.  
  
-   If files are added, it may be difficult to determine for which files the Foreach Loop affected.  
  
 **Folder**  
 Provide the path of the root folder to enumerate.  
  
 **Browse**  
 Browse to locate the root folder.  
  
 **Files**  
 Specify the files to enumerate.  
  
> [!NOTE]  
>  Use wildcard characters (*) to specify the files to include in the collection. For example, to include files with names that contain "abc", use the following filter: \*abc\*.  
>   
>  When you specify a file name extension, the enumerator also returns files that have the same extension with additional characters appended. (This is the same behavior as that of the **dir** command in the operating system, which also compares 8.3 file names for backward compatibility.) This behavior of the enumerator could cause unexpected results. For example, you want to enumerate only Excel 2003 files, and you specify "*.xls". However, the enumerator also returns Excel 2007 files because those files have the extension, ".xlsx".  
>   
>  You can use an expression to specify the files to include in a collection, by expanding **Expressions** on the **Collection** page, selecting the **FileSpec** property, and then clicking the ellipsis button (...) to add the property expression.  
  
 **Fully qualified**  
 Select to retrieve the fully qualified path of file names. If wildcard characters are specified in the Files option, then the fully qualified paths that are returned match the filter.  
  
 **Name only**  
 Select to retrieve only the file names. If wildcard characters are specified in the Files option, then the file names returned match the filter.  
  
 **Name and extension**  
 Select to retrieve the file names and their file name extensions. If wildcard characters are specified in the Files option, then the name and extension of files returned match the filter.  
  
 **Traverse Subfolders**  
 Select to include the subfolders in the enumeration.  
  
#### Enumerator = Foreach Item Enumerator  
 You use the Foreach Item enumerator to enumerate items in a collection. You define the items in the collection by specifying columns and column values. The columns in a row define an item. For example, an item that specifies the executables that an Execute Process task runs and the working directory that the task uses has two columns, one that lists the names of executables and one that lists the working directory. The number of rows determines the number of times that the loop is repeated. If the table has 10 rows, the loop repeats 10 times.  
  
 To update the properties of the Execute Process task, you map variables to item columns by using the index of the column. The first column defined in the enumerator item has the index value 0, the second column 1, and so on. The variable values are updated with each repeat of the loop. The **Executable** and **WorkingDirectory** properties of the Execute Process task can then be updated by property expressions that use these variables.  
  
 **Define the items in the For Each Item collection**  
 Provide a value for each column in the table.  
  
> [!NOTE]  
>  A new row is automatically added to the table after you enter values in row columns.  
  
> [!NOTE]  
>  If the values provided are not compatible with the column data type, the text is colored red.  
  
 **Column data type**  
 Lists the data type of the active column.  
  
 **Remove**  
 Select an item, and then click **Remove** to remove it from the list.  
  
 **Columns**  
 Click to configure the data type of the columns in the item.  
  
 **Related Topics:** [For Each Item Columns Dialog Box UI Reference](https://msdn.microsoft.com/library/ea76aae0-8798-4677-8ab8-4a579de4957c)  
  
#### Enumerator = Foreach ADO Enumerator  
 You use the Foreach ADO enumerator to enumerate rows or tables in an ADO or ADO.NET object that is stored in a variable. For example, if the Foreach Loop includes a Script task that writes a dataset to a variable, you can use the Foreach ADO enumerator to enumerate the rows in the dataset. If the variable contains an ADO.NET dataset, the enumerator can be configured to enumerate rows in multiple tables or to enumerate tables.  
  
 **ADO object source variable**  
 Select a user-defined variable in the list, or click \<**New variable...**> to create a new variable.  
  
> [!NOTE]  
>  The variable must have the Object data type, otherwise an error occurs.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md), [Add Variable](https://msdn.microsoft.com/library/d09b5d31-433f-4f7c-8c68-9df3a97785d5)  
  
 **Rows in first table**  
 Select to enumerate only rows in the first table.  
  
 **Rows in all tables (ADO.NET dataset only)**  
 Select to enumerate rows in all tables. This option is available only if the objects to enumerate are all members of the same ADO.NET dataset.  
  
 **All tables (ADO.NET dataset only)**  
 Select to enumerate tables only.  
  
#### Enumerator = Foreach ADO.NET Schema Rowset Enumerator  
 You use the Foreach ADO.NET Schema Rowset enumerator to enumerate a schema for a specified data source. For example, if the Foreach Loop includes an Execute SQL task, you can use the Foreach ADO.NET Schema Rowset enumerator to enumerate schemas such as the columns in the **AdventureWorks** database, and the Execute SQL task to get the schema permissions.  
  
 **Connection**  
 Select an ADO.NET connection manager in the list, or click \<**New connection...**> to create a new ADO.NET connection manager.  
  
> [!IMPORTANT]  
>  The ADO.NET connection manager must use a .NET provider for OLE DB. If connecting to SQL Server, the recommended provider to use is the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client, listed in the **.Net Providers for OleDb** section of the **Connection Manager** dialog box.  
  
 **Related Topics:** [ADO Connection Manager](../../integration-services/connection-manager/ado-connection-manager.md), [Configure ADO.NET Connection Manager](../../integration-services/connection-manager/configure-ado-net-connection-manager.md)  
  
 **Schema**  
 Select the schema to enumerate.  
  
 **Set Restrictions**  
 Set the restrictions to apply to the specified schema.  
  
 **Related Topics:** [Schema Restrictions Dialog Box](https://msdn.microsoft.com/library/92e5fd32-4944-4f7c-a448-b458df93d0d5)  
  
#### Enumerator = Foreach From Variable Enumerator  
 You use the Foreach From Variable enumerator to enumerate the enumerable objects in the specified variable. For example, if the Foreach Loop includes an Execute SQL task that runs a query and stores the result in a variable, you can use the Foreach From Variable enumerator to enumerate the query results.  
  
 **Variable**  
 Select a variable in the list, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md), [Add Variable](https://msdn.microsoft.com/library/d09b5d31-433f-4f7c-8c68-9df3a97785d5)  
  
#### Enumerator = Foreach NodeList Enumerator  
 You use the Foreach Nodelist enumerator to enumerate the set of XML nodes that results from applying an XPath expression to an XML file. For example, if the Foreach Loop includes a Script task, you can use the Foreach NodeList enumerator to pass a value that meets the XPath expression criteria from the XML file to the Script task.  
  
 The XPath expression that applies to the XML file is the outer XPath operation, stored in the OuterXPathString property. If the XPath enumeration type is set to **ElementCollection**, the Foreach NodeList enumerator can apply an inner XPath expression, stored in the InnerXPathString property, to a collection of element.  
  
 To learn more about working with XML documents and data, see "[Employing XML in the .NET Framework](https://go.microsoft.com/fwlink/?LinkId=56214)" in the MSDN Library.  
  
 **DocumentSourceType**  
 Select the source type of the XML document. This property has the options listed in the following table:  
  
|Value|Description|  
|-----------|-----------------|  
|**Direct input**|Set the source to an XML document.|  
|**File connection**|Select a file that contains the XML document.|  
|**Variable**|Set the source to a variable that contains the XML document.|  
  
 **DocumentSource**  
 If **DocumentSourceType** is set to **Direct input**, provide the XML code, or click the ellipsis (...) button to provide XML by using the **Document Source Edito**r dialog box.  
  
 If **DocumentSourceType** is set to **File connection**, select a File connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../integration-services/connection-manager/file-connection-manager-editor.md)  
  
 If **DocumentSourceType** is set to **Variable**, select an existing variable, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md), [Add Variable](https://msdn.microsoft.com/library/d09b5d31-433f-4f7c-8c68-9df3a97785d5).  
  
 **EnumerationType**  
 Select an enumeration type from the list. This property has the options listed in the following table:  
  
|Value|Description|  
|-----------|-----------------|  
|**Navigator**|Enumerate using an XPathNavigator.|  
|**Node**|Enumerate nodes returned by an XPath operation.|  
|**NodeText**|Enumerate text nodes returned by an XPath operation.|  
|**ElementCollection**|Enumerates element nodes returned by an XPath operation.|  
  
 **OuterXPathStringSourceType**  
 Select the source type of the XPath string. This property has the options listed in the following table: 
  
|Value|Description|  
|-----------|-----------------|  
|**Direct input**|Set the source to an XML document.|  
|**File connection**|Select a file that contains the XML document.|  
|**Variable**|Set the source to a variable that contains the XML document.|  
  
 **OuterXPathString**  
 If **OuterXPathStringSourceType** is set to **Direct input**, provide the XPath string.  
  
 If **OuterXPathStringSourceType** is set to **File connection**, select a File connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../integration-services/connection-manager/file-connection-manager-editor.md)  
  
 If **OuterXPathStringSourceType** is set to **Variable**, select an existing variable, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md), [Add Variable](https://msdn.microsoft.com/library/d09b5d31-433f-4f7c-8c68-9df3a97785d5).  
  
 **InnerElementType**  
 If **EnumerationType** is set to **ElementCollection**, select the type of inner element in the list.  
  
 **InnerXPathStringSourceType**  
 Select the source type of the inner XPath string. This property has the options listed in the following table:  
  
|Value|Description|  
|-----------|-----------------|  
|**Direct input**|Set the source to an XML document.|  
|**File connection**|Select a file that contains the XML document.|  
|**Variable**|Set the source to a variable that contains the XML document.|  
  
 **InnerXPathString**  
 If **InnerXPathStringSourceType** is set to **Direct input**, provide the XPath string.  
  
 If **InnerXPathStringSourceType** is set to **File connection**, select a File connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../integration-services/connection-manager/file-connection-manager-editor.md)  
  
 If **InnerXPathStringSourceType** is set to **Variable**, select an existing variable, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md), [Add Variable](https://msdn.microsoft.com/library/d09b5d31-433f-4f7c-8c68-9df3a97785d5).  
  
#### Enumerator = Foreach SMO Enumerator  
 You use the Foreach SMO enumerator to enumerate SQL Server Management Object (SMO) objects. For example, if the Foreach Loop includes an Execute SQL task, you can use the Foreach SMO enumerator to enumerate the tables in the **AdventureWorks** database and run queries that count the number of rows in each table.  
  
 **Connection**  
 Select an existing ADO.NET connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 Related Topics: [ADO.NET Connection Manager](../../integration-services/connection-manager/ado-net-connection-manager.md), [Configure ADO.NET Connection Manager](../../integration-services/connection-manager/configure-ado-net-connection-manager.md)  
  
 **Enumerate**  
 Specify the SMO object to enumerate.  
  
 **Browse**  
 Select the SMO enumeration.  
  
 **Related Topics:** [Select SMO Enumeration Dialog Box](https://msdn.microsoft.com/library/64ada1fe-21a2-4675-98fc-d5c803aa32f0)  
  
####  <a name="ForeachHDFSFile"></a> Enumerator = Foreach HDFS File Enumerator  
 The **Foreach HDFS File Enumerator** enables an SSIS package to enumerate HDFS files in the specified HDFS location. The name of each  HDFS file can be stored in a variable and used in tasks inside the Foreach Loop Container.  
  
 **Hadoop Connection Manager**  
 Specify an existing Hadoop Connection Manager or create a new one, which points to where the HDFS files are hosted. For more info, see [Hadoop Connection Manager](../../integration-services/connection-manager/hadoop-connection-manager.md).  
  
 **Directory Path**  
 Specify the name of the HDFS directory that contains the HDFS files to be enumerated.  
  
 **File name filter**  
 Specify a name filter to select files with a certain name pattern. For example, MySheet*.xls\* includes files such as MySheet001.xls and MySheetABC.xlsx.  
  
 **Retrieve file name**  
 Specify the file name type retrieved by SSIS.  
  
-   **Fully qualified name** means the full name, which contains the directory path and file name.  
  
-   **Name only** means the file name is retrieved without the path.  
  
 **Traverse subfolders**  
 Specify whether to loop through subfolders recursively.  
  
 On the **Variable Mappings** page of the editor, select or create a variable to store the name of the enumerated HDFS file.  
  
####  <a name="ForeachAzureBlob"></a> Enumerator = Foreach Azure Blob Enumerator  
 The  **Azure Blob Enumerator** enables an SSIS package to enumerate blob files in the specified blob location. You can store the name of the enumerated blob file in a variable and use it in tasks inside the Foreach Loop Container.  
  
 The **Azure Blob Enumerator** is a component of the SQL Server Integration Services (SSIS) Feature Pack for Azure for [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. Download the Feature Pack [here](https://go.microsoft.com/fwlink/?LinkID=626967).  
  
 **Azure storage connection manager**  
 Select an existing Azure Storage Connection Manager or create a new one that refers to an Azure Storage Account.  
  
 Related Topics: [Azure Storage Connection Manager](../../integration-services/connection-manager/azure-storage-connection-manager.md).  
  
 **Blob container name**  
 Specify the name of the blob container that contains the blob files to be enumerated.
  
 **Blob directory**  
 Specify the blob directory that contains the blob files to be enumerated. The blob directory is a virtual hierarchical structure.  
  
 **Blob name filter**  
 Specify a name filter to enumerate files with a certain name pattern. For example, `MySheet*.xls\*` includes files such as MySheet001.xls and MySheetABC.xlsx.  
  
 **Blob time range from/to filter**  
 Specify a time range filter. Files modified after **TimeRangeFrom** and before **TimeRangeTo** are enumerated. 

####  <a name="ForeachAdlsFile"></a> Enumerator = Foreach ADLS File Enumerator 
The **ADLS File Enumerator** enables an SSIS package to enumerate files in Azure Data Lake Store. You can store the full path of the enumerated file (prefixed with a slash - `/`) in a variable and use the file path in tasks inside the Foreach Loop Container.
  
**AzureDataLakeConnection**  
Specifies an Azure Data Lake connection manager, or creates a new one that refers to an ADLS account.   
  
**AzureDataLakeDirectory**  
Specifies the ADLS directory that contains the files to be enumerated.
  
**FileNamePattern**  
Specifies a file name filter. Only files whose names match the specified pattern are enumerated. The wildcards `*` and `?` are supported. 
  
**SearchRecursively**  
Specifies whether to search recursively within the specified directory.  

## Variable Mappings Page - Foreach Loop Editor
 Use the **Variables Mappings** page of the **Foreach Loop Editor** dialog box to map variables to the collection value. The value of the variable is updated with the collection values on each iteration of the loop.  
  
 To learn about how to use the Foreach Loop container in an Integration Services package,  see [Foreach Loop Container](../../integration-services/control-flow/foreach-loop-container.md). To learn about how to configure it, see [Configure a Foreach Loop Container](https://msdn.microsoft.com/library/519c6f96-5e1f-47d2-b96a-d49946948c25).  
  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] tutorial, Creating a Simple ETL Package Tutorial, includes a lesson that teaches you to add and configure a Foreach Loop.  
  
### Options  
 **Variable**  
 Select an existing variable, or click **New variable...** to create a new variable.  
  
> [!NOTE]  
>  After you map a variable, a new row is automatically added to the **Variable** list.  
  
 **Related Topics**: [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md), [Add Variable](https://msdn.microsoft.com/library/d09b5d31-433f-4f7c-8c68-9df3a97785d5)  
  
 **Index**  
 If using the Foreach Item enumerator, specify the index of the column in the collection value to map to the variable. For other enumerator types, the index is read-only.  
  
> [!NOTE]  
>  The index is 0-based.  
  
**Delete**  
 Select a variable, and then click **Delete**.  

## Schema Restrictions dialog box (ADO.NET)
Use the **Schema Restrictions** dialog box to set the schema restrictions to apply to the Foreach ADO.NET Schema Rowset enumerator.  
  
### Options  
 **Restrictions**  
 Select the constraints to apply to the schema.  
  
 **Variable**  
 Use a variable to define restrictions. Select a variable in the list, or click **New variable...** to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md) , [Add Variable](https://msdn.microsoft.com/library/d09b5d31-433f-4f7c-8c68-9df3a97785d5)  
  
 **Text**  
 Provide the text to define restrictions.  
 
## For Each Item Columns dialog box
Use the **For Each Item Columns** dialog box to define the columns in the items that the Foreach Item enumerator enumerates.  
  
### Options  
 **Column**  
 Lists the columns.  
  
 **Data Type**  
 Select the data type.  
  
 **Add**  
 Add a new column.  
  
 **Remove**  
 Select a column, and then click **Remove**.  
 
 ## Select SMO Enumeration dialog box
Use the **Select SMO Enumeration** dialog box to specify the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Objects (SMO) object on the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to enumerate, and to select the enumeration type.  
  
### Options  
 **Enumerate**  
 Expand the server and select the SMO object.  
  
 **Objects**  
 Use the Objects enumeration type.  
  
 **Prepopulate**  
 Use the **Prepopulate** option with the Objects enumeration type.  
  
 **Names**  
 Use the Names enumeration type.  
  
 **URNs**  
 Use the URNs enumeration type.  
  
 **Locations**  
 Use the Locations enumeration type. This option is available only for files.  

## Use property expressions with Foreach Loop containers  
 Packages can be configured to concurrently run multiple executables. This configuration should be used with caution when the package includes a Foreach Loop container that implements property expressions.  
  
 It is often useful to implement a property expression to set the value of the ConnectionString property of the connection managers that the Foreach Loop enumerators use. The property expression of ConnectionString is set by a variable that maps to the collection value of the enumerator and is updated at each iteration of the loop.  
  
 To avoid negative consequences of nondeterminative timing of parallel execution of tasks in the loop, the package should be configured to run only one executable at a time. For example, if a package can run multiple tasks concurrently, a Foreach Loop container that enumerates files in the folder, retrieves the file names, and then uses an Execute SQL task to insert the file names into a table may incur write conflicts when two instances of the Execute SQL task attempt to write at the same time. For more information, see [Use Property Expressions in Packages](../../integration-services/expressions/use-property-expressions-in-packages.md).  

## See Also  
 [Control Flow](../../integration-services/control-flow/control-flow.md)   
 [Integration Services Containers](../../integration-services/control-flow/integration-services-containers.md)  
  
  
