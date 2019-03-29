---
title: "XML Task Editor (General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.xmltask.general.f1"
helpviewer_keywords: 
  - "XML Task Editor"
ms.assetid: b9622c48-3243-4408-a1de-9ba20e32ff70
author: janinezhang
ms.author: janinez
manager: craigg
---
# XML Task Editor (General Page)
  Use the **General Node** of the **XML Task Editor** dialog box to specify the operation type and configure the operation.  
  
 To learn about this task, see [XML Task](control-flow/xml-task.md). For information about working with XML documents and data, see "[Employing XML in the .NET Framework](https://go.microsoft.com/fwlink/?LinkId=56214)" in the MSDN Library.  
  
## Static Options  
 **OperationType**  
 Select the operation type from the list. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Validate**|Validates the XML document against a Document Type Definition (DTD) or XML Schema definition (XSD) schema. Selecting this option displays the dynamic options in section, **Validate**.|  
|**XSLT**|Performs XSL transformations on XML documents. Selecting this option displays the dynamic options in section, **XSLT**.|  
|**XPATH**|Performs XPath queries and evaluations. Selecting this option displays the dynamic options in section, **XPATH**.|  
|**Merge**|Merges two XML documents. Selecting this option displays the dynamic options in section, **Merge**.|  
|**Diff**|Compares two XML documents. Selecting this option displays the dynamic options in section, **Diff**.|  
|**Patch**|Applies the output from the Diff operation to create a new document. Selecting this option displays the dynamic options in section, **Patch**.|  
  
 **SourceType**  
 Select the source type of the XML document. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Direct input**|Set the source to an XML document.|  
|**File connection**|Select a file that contains the XML document.|  
|**Variable**|Set the source to a variable that contains the XML document.|  
  
 **Source**  
 If **Source** is set to **Direct input**, provide the XML code or click the ellipsis button **(...)** and then provide the XML by using the **Document Source Editor** dialog box.  
  
 If **Source** is set to **File connection**, select a File connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
 If **Source** is set to **Variable**, select an existing variable, or click **\<New variable...>** to create a new variable.  
  
 **Related Topics**: [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md).  
  
## OperationType Dynamic Options  
  
### OperationType = Validate  
 Specify options for the Validate operation.  
  
 **SaveOperationResult**  
 Specify whether the XML task saves the output of the Validate operation.  
  
 **OverwriteDestination**  
 Specify whether to overwrite the destination file or variable.  
  
 **Destination**  
 Select an existing File connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
 **DestinationType**  
 Select the destination type of the XML document. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**File connection**|Select a file that contains the XML document.|  
|**Variable**|Set the source to a variable that contains the XML document.|  
  
 **ValidationType**  
 Select the validation type. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**DTD**|Use a Document Type Definition (DTD).|  
|**XSD**|Use an XML Schema definition (XSD) schema. Selecting this option displays the dynamic options in section, **ValidationType**.|  
  
 **FailOnValidationFail**  
 Specify whether the operation fails if the document fails to validate.  
  
 **ValidationDetails**  
 Provides rich error output when the value of this property is true. For more info, see [Validate XML with the XML Task](control-flow/validate-xml-with-the-xml-task.md).  
  
### ValidationType Dynamic Options  
  
#### ValidationType = XSD  
 **SecondOperandType**  
 Select the source type of the second XML document. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Direct input**|Set the source to an XML document.|  
|**File connection**|Select a file that contains the XML document.|  
|**Variable**|Set the source to a variable that contains the XML document.|  
  
 **SecondOperand**  
 If **SecondOperandType** is set to **Direct input**, provide the XML code or click the ellipsis button **(...)** and then provide the XML by using the **Source Editor** dialog box.  
  
 If **SecondOperandType** is set to **File connection**, select a File connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
 If **XPathStringSourceType** is set to **Variable**, select an existing variable, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics**: [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md).  
  
### OperationType = XSLT  
 Specify options for the XSLT operation.  
  
 **SaveOperationResult**  
 Specify whether the XML task saves the output of the XSLT operation.  
  
 **OverwriteDestination**  
 Specify whether to overwrite the destination file or variable.  
  
 **Destination**  
 If **DestinationType** is set to **File connection**, select a File connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
 If **DestinationType** is set to **Variable**, select an existing variable, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics**: [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md).  
  
 **DestinationType**  
 Select the destination type of the XML document. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**File connection**|Select a file that contains the XML document.|  
|**Variable**|Set the source to a variable that contains the XML document.|  
  
 **SecondOperandType**  
 Select the source type of the second XML document. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Direct input**|Set the source to an XML document.|  
|**File connection**|Select a file that contains the XML document.|  
|**Variable**|Set the source to a variable that contains the XML document.|  
  
 **SecondOperand**  
 If **SecondOperandType** is set to **Direct input**, provide the XML code or click the ellipsis button **(...)** and then provide the XML by using the **Source Editor** dialog box.  
  
 If **SecondOperandType** is set to **File connection**, select a File connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
 If **XPathStringSourceType** is set to **Variable**, select an existing variable, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics**: [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md).  
  
### OperationType = XPATH  
 Specify options for the XPath operation.  
  
 **SaveOperationResult**  
 Specify whether the XML task saves the output of the XPath operation.  
  
 **OverwriteDestination**  
 Specify whether to overwrite the destination file or variable.  
  
 **Destination**  
 If **DestinationType** is set to **File connection**, select a File connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
 If **DestinationType** is set to **Variable**, select an existing variable, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics**: [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md).  
  
 **DestinationType**  
 Select the destination type of the XML document. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**File connection**|Select a file that contains the XML document.|  
|**Variable**|Set the source to a variable that contains the XML document.|  
  
 **SecondOperandType**  
 Select the source type of the second XML document. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Direct input**|Set the source to an XML document.|  
|**File connection**|Select a file that contains the XML document.|  
|**Variable**|Set the source to a variable that contains the XML document.|  
  
 **SecondOperand**  
 If **SecondOperandType** is set to **Direct input**, provide the XML code or click the ellipsis button **(...)** and then provide the XML by using the **Source Editor** dialog box.  
  
 If **SecondOperandType** is set to **File connection**, select a File connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
 If **XPathStringSourceType** is set to **Variable**, select an existing variable, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics**: [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md).  
  
 **PutResultInOneNode**  
 Specify whether the result is written to a single node.  
  
 **XPathOperation**  
 Select the XPath result type. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Evaluation**|Returns the results of an XPath function.|  
|**Node list**|Return the selected nodes as an XML fragment.|  
|**Values**|Return the inner text value of all selected nodes, concatenated into a string.|  
  
### OperationType = Merge  
 Specify options for the Merge operation.  
  
 **XPathStringSourceType**  
 Select the source type of the XML document. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Direct input**|Set the source to an XML document.|  
|**File connection**|Select a file that contains the XML document.|  
|**Variable**|Set the source to a variable that contains the XML document.|  
  
 **XPathStringSource**  
 If **XPathStringSourceType** is set to **Direct input**, provide the XML code or click the ellipsis button **(...)** and then provide the XML by using the **Document Source Editor** dialog box.  
  
 If **XPathStringSourceType** is set to **File connection**, select a File connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
 If **XPathStringSourceType** is set to **Variable**, select an existing variable, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics**: [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md)  
  
 When you use an XPath statement to identify the merge location in the source document, this statement is expected to return a single node. If the statement returns multiple nodes, only the first node is used. The contents of the second document are merged under the first node that the XPath query returns.  
  
 **SaveOperationResult**  
 Specify whether the XML task saves the output of the Merge operation.  
  
 **OverwriteDestination**  
 Specify whether to overwrite the destination file or variable.  
  
 **Destination**  
 If **DestinationType** is set to **File connection**, select a File connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
 If **DestinationType** is set to **Variable**, select an existing variable, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics**: [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md).  
  
 **DestinationType**  
 Select the destination type of the XML document. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**File connection**|Select a file that contains the XML document.|  
|**Variable**|Set the source to a variable that contains the XML document.|  
  
 **SecondOperandType**  
 Select the destination type of the second XML document. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Direct input**|Set the source to an XML document.|  
|**File connection**|Select a file that contains the XML document.|  
|**Variable**|Set the source to a variable that contains the XML document.|  
  
 **SecondOperand**  
 If **SecondOperandType** is set to **Direct input**, provide the XML code or click the ellipsis button **(...)** and then provide the XML by using the **Document Source Editor** dialog box.  
  
 If **SecondOperandType** is set to **File connection**, select a File connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
 If **SecondOperandType** is set to **Variable**, select an existing variable, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics**: [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md)  
  
### OperationType = Diff  
 Specify options for the Diff operation.  
  
 **DiffAlgorithm**  
 Select the Diff algorithm to use when comparing documents. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Auto**|Let the XML task determine whether to use the fast or precise algorithm.|  
|**Fast**|Use a fast, but less precise Diff algorithm.|  
|**Precise**|Use a precise Diff algorithm.|  
  
 **Diff Options**  
 Set the Diff options to apply to the Diff operation. The options are listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**IgnoreXMLDeclaration**|Specify whether to compare the XML declaration.|  
|**IgnoreDTD**|Specify whether to ignore the document type definition (DTD).|  
|**IgnoreWhite Spaces**|Specify whether to ignore differences in the amount of white space when comparing documents.|  
|**IgnoreNamespaces**|Specify whether to compare the namespace uniform resource identifier (URI) of an element and its attribute names.<br /><br /> Note: If this option is set to `True`, two elements that have the same local name but different namespaces are considered identical.|  
|**IgnoreProcessingInstructions**|Specify whether to compare processing instructions.|  
|**IgnoreOrderOf ChildElements**|Specify whether to compare the order of child elements.<br /><br /> Note: If this option is set to `True`, child elements that differ only in their position in a list of siblings are considered identical.|  
|**IgnoreComments**|Specify whether to compare comment nodes.|  
|**IgnorePrefixes**|Specify whether to compare prefixes of element and attribute names.<br /><br /> Note: If you set this option to `True`, two elements that have the same local name, but different namespace URIs and prefixes, are considered identical.|  
  
 **FailOnDifference**  
 Specify whether the task fails if the Diff operation fails.  
  
 **SaveDiffGram**  
 Specify whether to save the comparison result, a DiffGram document.  
  
 **SaveOperationResult**  
 Specify whether the XML task saves the output of the Diff operation.  
  
 **OverwriteDestination**  
 Specify whether to overwrite the destination file or variable.  
  
 **Destination**  
 If **DestinationType** is set to **File connection**, select a File connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
 If **DestinationType** is set to **Variable**, select an existing variable, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics**: [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md).  
  
 **DestinationType**  
 Select the destination type of the XML document. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**File connection**|Select a file that contains the XML document.|  
|**Variable**|Set the source to a variable that contains the XML document.|  
  
 **SecondOperandType**  
 Select the destination type of the XML document. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Direct input**|Set the source to an XML document.|  
|**File connection**|Select a file that contains the XML document.|  
|**Variable**|Set the source to a variable that contains the XML document.|  
  
 **SecondOperand**  
 If **SecondOperandType** is set to **Direct input**, provide the XML code or click the ellipsis button **(...)** and then provide the XML by using the **Document Source Editor** dialog box.  
  
 If **SecondOperandType** is set to **File connection**, select a File connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
 If **SecondOperandType** is set to **Variable**, select an existing variable, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics**: [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md)  
  
### OperationType = Patch  
 Specify options for the Patch operation.  
  
 **SaveOperationResult**  
 Specify whether the XML task saves the output of the Patch operation.  
  
 **OverwriteDestination**  
 Specify whether to overwrite the destination file or variable.  
  
 **Destination**  
 If **DestinationType** is set to **File connection**, select a File connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
 If **DestinationType** is set to **Variable**, select an existing variable, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics**: [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md).  
  
 **DestinationType**  
 Select the destination type of the XML document. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**File connection**|Select a file that contains the XML document.|  
|**Variable**|Set the source to a variable that contains the XML document.|  
  
 **SecondOperandType**  
 Select the destination type of the XML document. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Direct input**|Set the source to an XML document.|  
|**File connection**|Select a file that contains the XML document.|  
|**Variable**|Set the source to a variable that contains the XML document.|  
  
 **SecondOperand**  
 If **SecondOperandType** is set to **Direct input**, provide the XML code or click the ellipsis button **(...)** and then provide the XML by using the **Document Source Editor** dialog box.  
  
 If **SecondOperandType** is set to **File connection**, select a File connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
 If **SecondOperandType** is set to **Variable**, select an existing variable, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics**: [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md)  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Expressions Page](expressions/expressions-page.md)  
  
  
