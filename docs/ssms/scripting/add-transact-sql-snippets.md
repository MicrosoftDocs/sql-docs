---
title: "Add Transact-SQL Snippets"
description: Learn how to add your own Transact-SQL code snippets to the set of pre-defined snippets included in SQL Server.
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
ms.assetid: 901c7995-8eb5-4d12-8bb0-de0a922b48f8
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Add Transact-SQL Snippets

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

You can add your own Transact-SQL code snippets to the set of pre-defined snippets included in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  

## Creating a Transact-SQL Snippet File  
 The first part of creating a [!INCLUDE[tsql](../../includes/tsql-md.md)] code snippet is to create an XML file with the text of your code snippet. The file must have a .snippet file extension, and meet the requirements of the [Code Snippets Schema](/previous-versions/visualstudio/visual-studio-2015/ide/code-snippets-schema-reference). Set the snippet language to SQL.  
  
 You can use the pre-defined snippets that ship with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as examples. To find the pre-defined snippets, open SQL Server Management Studio, select the **Tools** menu, and click **Code Snippet Manager**. Select **SQL** in the **Language** list box, the path to the [!INCLUDE[tsql](../../includes/tsql-md.md)] snippets is displayed in the **Location** box.  
  
## Registering the Code Snippet  
 After creating the snippet file, use the Code Snippets Manager to register the snippet with SQL Server Management Studio. You can either add a folder containing multiple snippets, or import individual snippets to the **My Code Snippets** folder.  
  
## Procedures  
  
#### Adding a Snippet Folder  
  
1.  Open SQL Server Management Studio.  
  
2.  Select the **Tools** menu, and click **Code Snippets Manager**.  
  
3.  Click the **Add** button.  
  
4.  Navigate to the folder containing your code snippets, and click the **Select Folder** button.  
  
#### Importing a Snippet  
  
1.  Open SQL Server Management Studio.  
  
2.  Select the **Tools** menu, and click **Code Snippets Manager**.  
  
3.  Click the **Import** button.  
  
4.  Navigate to the folder containing your snippet, click on the .snippet file, and click the **Open** button.  
  
## Examples  
 The following example creates a **TRY-CATCH** surround-with snippet and imports it to SQL Server Management Studio.  
  
1.  Paste the following code into notepad, then save as a file named TryCatch.snippet.  
  
    ```  
    <?xml version="1.0" encoding="utf-8" ?>  
    <CodeSnippets  xmlns="https://schemas.microsoft.com/VisualStudio/2005/CodeSnippet">  
    <_locDefinition xmlns="urn:locstudio">  
        <_locDefault _loc="locNone" />  
        <_locTag _loc="locData">Title</_locTag>  
        <_locTag _loc="locData">Description</_locTag>  
        <_locTag _loc="locData">Author</_locTag>  
        <_locTag _loc="locData">ToolTip</_locTag>  
       <_locTag _loc="locData">Default</_locTag>  
    </_locDefinition>  
    <CodeSnippet Format="1.0.0">  
    <Header>  
    <Title>TryCatch</Title>  
                            <Shortcut></Shortcut>  
    <Description>Example Snippet for Try-Catch.</Description>  
    <Author>SQL Server Books Online Example</Author>  
    <SnippetTypes>  
                                    <SnippetType>SurroundsWith</SnippetType>  
    </SnippetTypes>  
    </Header>  
    <Snippet>  
    <Declarations>  
                                    <Literal>  
                                    <ID>CatchCode</ID>  
                                    <ToolTip>Code to handle the caught error</ToolTip>  
                                    <Default>CatchCode</Default>  
                                    </Literal>  
    </Declarations>  
    <Code Language="SQL"><![CDATA[  
    BEGIN TRY  
  
    $selected$ $end$  
  
    END TRY  
    BEGIN CATCH  
  
    $CatchCode$  
  
    END CATCH;  
    ]]>  
    </Code>  
    </Snippet>  
    </CodeSnippet>  
    </CodeSnippets>  
    ```  
  
2.  Open SQL Server Management Studio.  
  
3.  Select the **Tools** menu, and click **Code Snippets Manager**.  
  
4.  Click the **Import** button.  
  
5.  Navigate to the folder containing TryCatch.snippet, click on the TryCatch.snippet file, and click the **Open** button. You should now have a TryCatch snippet in your **My Code Snippets** folder.  
  
## See Also  
 [Insert Surround-with Transact-SQL snippets](./insert-surround-with-transact-sql-snippets.md)  
