---
title: "Configure a Foreach Loop Container | Microsoft Docs"
ms.custom: ""
ms.date: "08/22/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "Foreach Loop containers"
  - "containers [Integration Services], Foreach Loop"
ms.assetid: 519c6f96-5e1f-47d2-b96a-d49946948c25
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Configure a Foreach Loop Container
  This procedure describes how to configure a Foreach Loop container, including property expressions at the enumerator and container levels.  
  
### To configure the Foreach Loop container  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  Click the **Control Flow** tab and double-click the Foreach Loop.  
  
3.  In the **Foreach Loop Editor** dialog box, click **General** and, optionally, modify the name and description of the Foreach Loop.  
  
4.  Click **Collection** and select an enumerator type from the **Enumerator** list.  
  
5.  Specify an enumerator and set enumerator options as follows:  
  
    -   To usethe Foreach File enumerator, provide the folder that contains the files to enumerate, specify a filter for the file name and type, and specify whether the fully qualified file name should be returned. Also, indicate whether to recurse through subfolders for more files.  
  
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
  
         If EnumerationType is **ElementCollection**,set OuterXPathStringSourceType and OuterXPathString as described above. Then, click InnerElementType and select an enumeration type for the inner elements, and then click InnerXPathStringSourceType. Depending on the value set for InnerXPathStringSourceType, select a variable or a file connection, create a new variable or file connection, or type the string for the inner XPath expression.  
  
    -   To use the Foreach SMO enumerator, select an existing ADO.NET connection or click **New connection** in the **Connection** list, and then either type the string to use or click **Browse**. If you click **Browse**, in the **Select SMO Enumeration** dialog box, select the object type to enumerate and the enumeration type, and click **OK**.  
  
6.  Optionally, click the browse button **(...)** in the **Expressions** text box on the **Collection** page to create expressions that update property values. For more information, see [Add or Change a Property Expression](expressions/add-or-change-a-property-expression.md).  
  
    > [!NOTE]  
    >  The properties listed in the **Property** list varies by enumerator.  
  
7.  Optionally, click **Variable Mappings** to map object properties to the collection value, and then do the following:  
  
    1.  In the **Variables** list, select a variable or click **\<New Variable>** to create a new variable.  
  
    2.  If you add a new variable, set the variable properties in the **Add Variable** dialog box and click **OK**.  
  
    3.  If you use the For Each Item enumerator, you can update the index value in the **Index** list.  
  
        > [!NOTE]  
        >  The index value indicates which column in the item to map to the variable. Only the For Each Item enumerator can use an index value other than 0.  
  
8.  Optionally, click **Expressions** and, on the **Expressions** page, create property expressions for the properties of the Foreach Loop container. For more information, see [Add or Change a Property Expression](expressions/add-or-change-a-property-expression.md).  
  
9. Click **OK**.  
  
## See Also  
 [Foreach Loop Container](control-flow/foreach-loop-container.md)  
  
  
