---
title: "Column Pattern Profile Request Options (Data Profiling Task) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Data Profiling Task Editor"
ms.assetid: 9ccb8fc5-f65e-41a2-9511-7fa55586eb8b
author: janinezhang
ms.author: janinez
manager: craigg
---
# Column Pattern Profile Request Options (Data Profiling Task)
  Use the **Request Properties** pane of the **Profile Requests** page to set the options for the **Column Pattern Profile Request** selected in the requests pane. A Column Pattern profile reports a set of regular expressions that cover the specified percentage of values in a string column. This profile can help you identify problems in your data, such as invalid strings, and can suggest regular expressions that can be used in the future to validate new values. For example, a pattern profile of a column of United States Zip Codes might produce the regular expressions \d{5}-\d{4}, \d{5}, and \d{9}. If you see other regular expressions, your data likely contains values that are invalid or in an incorrect format.  
  
> [!NOTE]  
>  The options described in this topic appear on the **Profile Requests page** of the **Data Profiling Task Editor**. For more information about this page of the editor, see [Data Profiling Task Editor &#40;Profile Requests Page&#41;](data-profiling-task-editor-profile-requests-page.md).  
  
 For more information about how to use the Data Profiling Task, see [Setup of the Data Profiling Task](data-profiling-task.md). For more information about how to use the Data Profile Viewer to analyze the output of the Data Profiling Task, see [Data Profile Viewer](data-profile-viewer.md).  
  
## Understanding the Use of Delimiters and Symbols  
 Before computing the patterns for a **Column Pattern Profile Request**, the Data Profiling Task tokenizes the data. That is, the task separates the string values into smaller units known as tokens. The task separates strings into tokens based on the delimiters and symbols that you specify for the **Delimiters** and **Symbols** properties:  
  
-   **Delimiters** By default, the list of delimiters contains the following characters: space, horizontal tab (\t), new line (\n), and carriage return (\r). You can specify additional delimiters, but you cannot remove the default delimiters.  
  
-   **Symbols** By default, the list of **Symbols** contains the following characters: `,.;:-"'`~=&/@!?()<>[]{}|#*^%`. For example, if the symbols are "`()-`", the value "(425) 123-4567" is tokenized as ["(", "425", ")", "123", "-", "4567", ")"].  
  
 A character cannot be both a delimiter and a symbol.  
  
 All delimiters are normalized to a single space as part of the tokenizing process, while symbols are retained.  
  
## Understanding the Use of the Tag Table  
 You can optionally group related tokens with a single tag by storing tags and the related terms in a special table that you create in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. The tag table must have two string columns, one named "Tag" and the other named "Term". These columns can be of type `char`, `nchar`, `varchar`, or `nvarchar`, but not `text` or `ntext`. You can combine multiple tags and the corresponding terms in a single table. A Column Pattern Profile Request can use only one tag table. You can use a separate [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager to connect to the tag table. Therefore, the tag table can be located in a different database or on a different server than the source data.  
  
 For example, you could group the values "East", "West", "North", and "South" that might appear in street addresses by using the single tag, "Direction". The following table is an example of such a tag table.  
  
|Tag|Term|  
|---------|----------|  
|Direction|East|  
|Direction|West|  
|Direction|North|  
|Direction|South|  
  
 You could use another tag to group the different words that express the notion of a "street" in street addresses:  
  
|Tag|Term|  
|---------|----------|  
|Street|Street|  
|Street|Avenue|  
|Street|Place|  
|Street|Way|  
  
 Based on this combination of tags, the resulting pattern for a street address might resemble the following pattern:  
  
 `\d+\ LookupTag=Direction \d+\p{L}+\ LookupTag=Street`  
  
> [!NOTE]  
>  Using a tag table decreases the performance of the Data Profiling task. Do not use more than 10 tags or more than 100 terms per tag.  
  
 The same term can belong to more than one tag.  
  
## Request Properties Options  
 For a **Column Pattern Profile Request**, the **Request Properties** pane displays the following groups of options:  
  
-   **Data**, which includes the **TableOrView** and **Column** options  
  
-   **General**  
  
-   **Options**  
  
### Data Options  
 **ConnectionManager**  
 Select the existing [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager that uses the .NET Data Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (SqlClient) to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database that contains the table or view to be profiled.  
  
 **TableOrView**  
 Select the existing table or view that contains the column to be profiled.  
  
 For more information, see the section, "TableorView Options," in this topic.  
  
 **Column**  
 Select the existing column to be profiled. Select **(\*)** to profile all columns.  
  
 For more information, see the section, "Column Options," in this topic.  
  
#### TableOrView Options  
 **Schema**  
 Specifies the schema to which the selected table belongs. This option is read-only.  
  
 **Table**  
 Displays the name of the selected table. This option is read-only.  
  
#### Column Options  
 **IsWildCard**  
 Specifies whether the **(\*)** wildcard has been selected. This option is set to **True** if you have selected **(\*)** to profile all columns. It is **False** if you have selected an individual column to be profiled. This option is read-only.  
  
 **ColumnName**  
 Displays the name of the selected column. This option is blank if you have selected **(\*)** to profile all columns. This option is read-only.  
  
 **StringCompareOptions**  
 This option does not apply to the Column Pattern Profile.  
  
### General Options  
 **RequestID**  
 Type a descriptive name to identify this profile request. Typically, you do not have to change the autogenerated value.  
  
### Options  
 **MaxNumberOfPatterns**  
 Specify the maximum number of patterns that you want the profile to compute. The default value of this option is 10. The maximum value is 100.  
  
 **PercentageDataCoverageDesired**  
 Specify the percentage of the data that you want the computed patterns to cover. The default value of this option is 95 (percent).  
  
 **CaseSensitive**  
 Indicate whether the patterns should be case-sensitive. The default value of this option is **False**.  
  
 **Delimiters**  
 List the characters that should be treated as the equivalent of spaces between words when tokenizing text. By default, the list of **Delimiters** contains the following characters: the space, horizontal tab (\t), new line (\n), and carriage return (\r). You can specify additional delimiters, but you cannot remove the default delimiters.  
  
 For more information, see "Understanding the Use of Delimiters and Symbols" earlier in this topic.  
  
 **Symbols**  
 List the symbols that should be retained as part of patterns. Examples might include "/" for dates, ":" for times, and "@" for e-mail addresses. By default, the list of **Symbols** contains the following characters: `,.;:-"'`~=&/@!?()<>[]{}|#*^%`.  
  
 For more information, see "Understanding the Use of Delimiters and Symbols" earlier in this topic.  
  
 **TagTableConnectionManager**  
 Select the existing [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager that uses the .NET Data Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (SqlClient) to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database that contains the tag table.  
  
 For more information, see "Understanding the Use of the Tag Table" earlier in this topic.  
  
 **TagTableName**  
 Select the existing tag table, which must have two string columns named Tag and Term.  
  
 For more information, see "Understanding the Use of the Tag Table" earlier in this topic.  
  
## See Also  
 [Data Profiling Task Editor &#40;General Page&#41;](../general-page-of-integration-services-designers-options.md)   
 [Single Table Quick Profile Form &#40;Data Profiling Task&#41;](single-table-quick-profile-form-data-profiling-task.md)  
  
  
