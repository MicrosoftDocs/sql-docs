---
title: "Foreach Loop Editor (Collection Page) | Microsoft Docs"
ms.custom: ""
ms.date: "08/24/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.foreachloopcontainer.collection.f1"
ms.assetid: 95a19dde-61ca-4d9b-aa3d-131fa4264296
author: janinezhang
ms.author: janinez
manager: craigg
---
# Foreach Loop Editor (Collection Page)
  Use the **Collection** pageof the **Foreach Loop Editor** dialog box to specify the enumerator type and configure the enumerator.  
  
 To learn about the Foreach Loop container and how to configure it, see [Foreach Loop Container](control-flow/foreach-loop-container.md) and [Configure a Foreach Loop Container](../../2014/integration-services/configure-a-foreach-loop-container.md).  
  
## Static Options  
 **Enumerator**  
 Select the enumerator type from the list. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Foreach File Enumerator**|Enumerate files. Selecting this value displays the dynamic options in the section, **Foreach File Enumerator**.|  
|**Foreach Item Enumerator**|Enumerate values in an item. Selecting this value displays the dynamic options in the section, **Foreach Item Enumerator**.|  
|**Foreach ADO Enumerator**|Enumerate tables or rows in tables. Selecting this value displays the dynamic options in the section, **Foreach ADO Enumerator**.|  
|**Foreach ADO.NET Schema Rowset Enumerator**|Enumerate a schema. Selecting this value displays the dynamic options in the section, **Foreach ADO.NET Enumerator**.|  
|**Foreach From Variable Enumerator**|Enumerate the value in a variable. Selecting this value displays the dynamic options in the section, **Foreach From Variable Enumerator**.|  
|**Foreach Nodelist Enumerator**|Enumerate nodes in an XML document. Selecting this value displays the dynamic options in the section, **Foreach Nodelist Enumerator**.|  
|**Foreach SMO Enumerator**|Enumerate a SMO object. Selecting this value displays the dynamic options in the section, **Foreach SMO Enumerator**.|  
|**Foreach Azure Blob Enumerator**|Enumerate blob files in the specified blob location. Selecting this value displays the dynamic options in the section, **Foreach Azure Blob Enumerator**.|  
|**Foreach ADLS File Enumerator**|Enumerate files on ADLS with filters. Selecting this value displays the dynamic options in the section, **Foreach ADLS File Enumerator**.|
  
 **Expressions**  
 Click or expand **Expressions** to view the list of existing property expressions. Click the ellipsis button **(...)** to add a property expression for an enumerator property, or edit and evaluate an existing property expression.  
  
 **Related Topics:**  [Integration Services &#40;SSIS&#41; Expressions](expressions/integration-services-ssis-expressions.md), [Property Expressions Editor](expressions/property-expressions-editor.md), [Expression Builder](expressions/expression-builder.md)  
  
## Enumerator Dynamic Options  
  
### Enumerator = Foreach File Enumerator  
 You use the Foreach File enumerator to enumerate files in a folder. For example, if the Foreach Loop includes an Execute SQL task, you can use the Foreach File enumerator to enumerate files that contain SQL statements that the Execute SQL task runs. The enumerator can be configured to include subfolders.  
  
 The content of the folders and subfolders that the Foreach File enumerator enumerates might change while the loop is executing because external processes or tasks in the loop add, rename, or delete files while the loop is executing. This means that a number of unexpected situations may occur:  
  
-   If files are deleted, one task in the Foreach Loop may perform work on a different set of files than the files used by subsequent tasks.  
  
-   If files are renamed and an external process automatically adds files to replace the renamed files, the Foreach Loop might perform work twice on the same file content.  
  
-   If files are added, it may be difficult to determine for which files the Foreach Loop performed work.  
  
 **Folder**  
 Provide the path of the root folder to enumerate.  
  
 **Browse**  
 Browse to locate the root folder.  
  
 **Files**  
 Specify the files to enumerate.  
  
> [!NOTE]  
>  Use wildcard characters (*) to specify the files to include in the collection. For example, to include files with names that contain "abc", use the following filter: \*abc\*.  
>   
>  When you specify a file name extension, the enumerator also returns files that have the same extension with additional characters appended. (This is the same behavior as that of the **dir** command in the operating system, which also compares 8.3 file names for backward compatibility.) This behavior of the enumerator could cause unexpected results. For example, you want to enumerate only Excel 2003 files, and you specify "*.xls". However, the enumerator will also return Excel 2007 files because those files have the extension, ".xlsx".  
>   
>  You can use an expression to specify the files to include in a collection, by expanding **Expressions** on the **Collection** page, selecting the **FileSpec** property, and then clicking the ellipsis button (...) to add the property expression. For more information about dynamically selecting specified files, see [SSIS-Dynamically set File Mask : FileSpec](https://go.microsoft.com/fwlink/?LinkId=238154)  
  
 **Fully qualified**  
 Select to retrieve the fully qualified path of file names. If wildcard characters are specified in the Files option, then the fully-qualified paths that are returned match the filter.  
  
 **Name only**  
 Select to retrieve only the file names. If wildcard characters are specified in the Files option, then the file names returned match the filter.  
  
 **Name and extension**  
 Select to retrieve the file names and their file name extensions. If wildcard characters are specified in the Files option, then the name and extension of files returned match the filter.  
  
 **Traverse Subfolders**  
 Select to include the subfolders in the enumeration.  
  
### Enumerator = Foreach Item Enumerator  
 You use the Foreach Item enumerator to enumerate items in a collection. You define the items in the collection by specifying columns and column values. The columns in a row define an item. For example, an item that specifies the executables that an Execute Process task runs and the working directory that the task uses has two columns, one that lists the names of executables and one that lists the working directory. The number of rows determines the number of times that the loop is repeated. If the table has 10 rows, the loop repeats 10 times.  
  
 To update the properties of the Execute Process task, you map variables to item columns by using the index of the column. The first column defined in the enumerator item has the index value 0, the second column 1, and so on. The variable values are updated with each repeat of the loop. The `Executable` and `WorkingDirectory` properties of the Execute Process task can then be updated by property expressions that use these variables.  
  
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
  
 **Related Topics:** [For Each Item Columns Dialog Box UI Reference](../../2014/integration-services/for-each-item-columns-dialog-box-ui-reference.md)  
  
### Enumerator = Foreach ADO Enumerator  
 You use the Foreach ADO enumerator to enumerate rows or tables in an ADO or ADO.NET object that is stored in a variable. For example, if the Foreach Loop includes a Script task that writes a dataset to a variable, you can use the Foreach ADO enumerator to enumerate the rows in the dataset. If the variable contains an ADO.NET dataset, the enumerator can be configured to enumerate rows in multiple tables or to enumerate tables.  
  
 **ADO object source variable**  
 Select a user-defined variable in the list, or click \<**New variable...**> to create a new variable.  
  
> [!NOTE]  
>  The variable must have the Object data type, otherwise an error occurs.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md)  
  
 **Rows in first table**  
 Select to enumerate only rows in the first table.  
  
 **Rows in all tables (ADO.NET dataset only)**  
 Select to enumerate rows in all tables. This option is available only if the objects to enumerate are all members of the same ADO.NET dataset.  
  
 **All tables (ADO.NET dataset only)**  
 Select to enumerate tables only.  
  
### Enumerator = Foreach ADO.NET Schema Rowset Enumerator  
 You use the Foreach ADO.NET Schema Rowset enumerator to enumerate a schema for a specified data source. For example, if the Foreach Loop includes an Execute SQL task, you can use the Foreach ADO.NET Schema Rowset enumerator to enumerate schemas such as the columns in the **AdventureWorks** database, and the Execute SQL task to get the schema permissions.  
  
 **Connection**  
 Select an ADO.NET connection manager in the list, or click \<**New connection...**> to create a new ADO.NET connection manager.  
  
> [!IMPORTANT]  
>  The ADO.NET connection manager must use a .NET provider for OLE DB. If connecting to SQL Server, the recommended provider to use is the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Native Client, listed in the **.Net Providers for OleDb** section of the **Connection Manager** dialog box.  
  
 **Related Topics:** [ADO Connection Manager](connection-manager/ado-connection-manager.md), [Configure ADO.NET Connection Manager](configure-ado-net-connection-manager.md)  
  
 **Schema**  
 Select the schema to enumerate.  
  
 **Set Restrictions**  
 Set the restrictions to apply to the specified schema.  
  
 **Related Topics:** [Schema Restrictions Dialog Box](../../2014/integration-services/schema-restrictions-dialog-box.md)  
  
### Enumerator = Foreach From Variable Enumerator  
 You use the Foreach From Variable enumerator to enumerate the enumerable objects in the specified variable. For example, if the Foreach Loop includes an Execute SQL task that runs a query and stores the result in a variable, you can use the Foreach From Variable enumerator to enumerate the query results.  
  
 **Variable**  
 Select a variable in the list, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md)  
  
### Enumerator = Foreach NodeList Enumerator  
 You use the Foreach Nodelist enumerator to enumerate the set of XML nodes that results from applying an XPath expression to an XML file. For example, if the Foreach Loop includes a Script task, you can use the Foreach NodeList enumerator to pass a value that meets the XPath expression criteria from the XML file to the Script task.  
  
 The XPath expression that applies to the XML file is the outer XPath operation, stored in the OuterXPathString property. If the XPath enumeration type is set to `ElementCollection`, the Foreach NodeList enumerator can apply an inner XPath expression, stored in the InnerXPathString property, to a collection of element.  
  
 To learn more about working with XML documents and data, see "[Employing XML in the .NET Framework](https://go.microsoft.com/fwlink/?LinkId=56214)" in the MSDN Library.  
  
 **DocumentSourceType**  
 Select the source type of the XML document. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Direct input**|Set the source to an XML document.|  
|**File connection**|Select a file that contains the XML document.|  
|**Variable**|Set the source to a variable that contains the XML document.|  
  
 **DocumentSource**  
 If **DocumentSourceType** is set to **Direct input**, provide the XML code, or click the ellipsis (...) button to provide XML by using the **Document Source Edito**r dialog box.  
  
 If **DocumentSourceType** is set to **File connection**, select a File connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
 If **DocumentSourceType** is set to **Variable**, select an existing variable, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md).  
  
 **EnumerationType**  
 Select an enumeration type from the list. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Navigator**|Enumerate using an XPathNavigator.|  
|**Node**|Enumerate nodes returned by an XPath operation.|  
|**NodeText**|Enumerate text nodes returned by an XPath operation.|  
|`ElementCollection`|Enumerates element nodes returned by an XPath operation.|  
  
 **OuterXPathStringSourceType**  
 Select the source type of the XPath string. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Direct input**|Set the source to an XML document.|  
|**File connection**|Select a file that contains the XML document.|  
|**Variable**|Set the source to a variable that contains the XML document.|  
  
 `OuterXPathString`  
 If **OuterXPathStringSourceType** is set to **Direct input**, provide the XPath string.  
  
 If **OuterXPathStringSourceType** is set to **File connection**, select a File connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
 If **OuterXPathStringSourceType** is set to **Variable**, select an existing variable, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md).  
  
 **InnerElementType**  
 If **EnumerationType** is set to `ElementCollection`, select the type of inner element in the list.  
  
 **InnerXPathStringSourceType**  
 Select the source type of the inner XPath string. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Direct input**|Set the source to an XML document.|  
|**File connection**|Select a file that contains the XML document.|  
|**Variable**|Set the source to a variable that contains the XML document.|  
  
 `InnerXPathString`  
 If **InnerXPathStringSourceType** is set to **Direct input**, provide the XPath string.  
  
 If **InnerXPathStringSourceType** is set to **File connection**, select a File connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
 If **InnerXPathStringSourceType** is set to **Variable**, select an existing variable, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md).  
  
### Enumerator = Foreach SMO Enumerator  
 You use the Foreach SMO enumerator to enumerate SQL Server Management Object (SMO) objects. For example, if the Foreach Loop includes an Execute SQL task, you can use the Foreach SMO enumerator to enumerate the tables in the **AdventureWorks** database and run queries that counts the number of rows in each table.  
  
 **Connection**  
 Select an existing ADO.NET connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 Related Topics: [ADO.NET Connection Manager](connection-manager/ado-net-connection-manager.md), [Configure ADO.NET Connection Manager](configure-ado-net-connection-manager.md)  
  
 **Enumerate**  
 Specify the SMO object to enumerate.  
  
 **Browse**  
 Select the SMO enumeration.  
  
 **Related Topics:** [Select SMO Enumeration Dialog Box](../../2014/integration-services/select-smo-enumeration-dialog-box.md)  
  
### Enumerator = Foreach Azure Blob Enumerator  
 The  **Azure Blob Enumerator**r enables an SSIS package to enumerate blob files in the specified blob location. The name of enumerated blob file can be stored in a variable and used in tasks inside the Foreach Loop Container.  
  
 **Azure storage connection manager**  
 Select an existing Azure Storage Connection Manager or create a new one that refers to an Azure Storage Account.  
  
 Related Topics: [Azure Storage Connection Manager](connection-manager/azure-storage-connection-manager.md).  
  
 **Blob container name**  
 Specify the name of the blob container that contains the blob files to be enumerated..  
  
 **Blob directory**  
 Specify the specify the blob directory that contains the blob files to be enumerated. The blob directory is a virtual hierarchical structure.  
  
 **Blob name filter**  
 Specify a name filter to enumerate files with a certain name pattern. E.g. MySheet*.xls\* will include files such as MySheet001.xls and MySheetABC.xlsx.  
  
 **Blob time range from/to filter**  
 Specify a time range filter. Files modified after **TimeRangeFrom** and before **TimeRangeTo** will be enumerated.  
### Enumerator = Foreach ADLS File Enumerator  
The **ADLS File Enumerator** enables an SSIS package to enumerate files on ADLS with filters. The slash (`/`)-prefixed full path of enumerated files can be stored in a variable and used in tasks inside the Foreach Loop Container.
  
**AzureDataLakeConnection**  
Specifies an Azure Data Lake connection manager, or creates a new one that refers to an ADLS account.   
  
**AzureDataLakeDirectory**  
Specifies the ADLS directory to search.
  
**FileNamePattern**  
Specifies a file name filter. Only files whose name matches the specified pattern will be enumerated. Wildcards `*` and `?` are supported. 
  
**SearchRecursively**  
Specifies whether to search recursively within the specified directory.  
  
## External Resources  
  
-   Blog entry, [SSIS For Each Node List Enumerator](https://go.microsoft.com/fwlink/?LinkId=220671), on bidn.com.  
  
-   Blog entry, [SSIS-Dynamically set File Mask : FileSpec](https://go.microsoft.com/fwlink/?LinkId=238154), on beyondrelational.com.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Foreach Loop Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)   
 [Foreach Loop Editor &#40;Variable Mappings Page&#41;](../../2014/integration-services/foreach-loop-editor-variable-mappings-page.md)   
 [Expressions Page](expressions/expressions-page.md)   
 [For Loop Container](control-flow/for-loop-container.md)  
  
  
