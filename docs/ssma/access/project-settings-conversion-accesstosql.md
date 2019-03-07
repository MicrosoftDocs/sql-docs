---
title: "Project Settings (Conversion) (AccessToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "conversion, options described"
  - "Project Settings dialog box, Conversion"
ms.assetid: bcebc635-c638-4ddb-924c-b9ccfef86388
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Project Settings (Conversion) (AccessToSQL)
The Conversion project settings let you configure how objects are converted from Access database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure database objects.  
  
The Conversion pane is available in the **Project Settings** and **Default Project Settings** dialog boxes.  
  
-   Use the **Project Settings** dialog box to set configuration options for the current project. To access the conversion settings, on the **Tools** menu, select **Project Settings**, click **General** at the bottom of the left pane, and then select **Conversion**.  
  
-   Use the **Default Project Settings** dialog box to set configuration options for all projects. To access the conversion settings, on the **Tools** menu, select **Default Project Settings**, select migration project type for which settings are required to be viewed /changed from **Migration Target Version** drop down, click **General** at the bottom of the left pane, and then select **Conversion**.  
  
## Options  
**Add primary key**  
Creates a new primary key in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure table if an Access table has no primary key or unique index.  
  
-   **Default Mode**: False  
  
-   **Optimistic Mode**: False  
  
-   **Full Mode**: True  
  
When connected to SQL Azure, it is by default True.**Add timestamp columns**  
Specifies whether SSMA should create a timestamp value if it is required.  
  
-   **Default Mode**: Let SSMA decide  
  
-   **Optimistic Mode**: Never  
  
-   **Full Mode**: Let SSMA decide  
  
**Include a data assessment report with conversion assessment reports**  
Includes a data assessment in the assessment report.  
  
-   **Default Mode**: True  
  
-   **Optimistic Mode**: False  
  
-   **Full Mode**: True  
  
**Message type when a primary key includes nullable columns**  
Specifies the type of message (Warning, Error, or Nothing) that SSMA shows in the Output pane when it finds primary keys with nullable columns.  
  
-   **Default Mode**: Warning  
  
-   **Optimistic Mode**: No message  
  
-   **Full Mode**: Error  
  
**Message type when foreign key columns are of different sizes**  
Specifies the type of message (Warning, Error, or Nothing) that SSMA shows in the Output pane when it finds an incorrect TEXT foreign key.  
  
-   **Default Mode**: Warning  
  
-   **Optimistic Mode**: No message  
  
-   **Full Mode**: Error  
  
**Message type when memo columns are indexed**  
Specifies the type of message (Warning, Error, or Nothing) that SSMA shows in the Output pane when it finds an index that contains a **memo** column.  
  
-   **Default Mode**: Warning  
  
-   **Optimistic Mode**: No message  
  
-   **Full Mode**: Error  
  
**Warn when a complex query uses a wildcard (\&#42;)**  
Displays a warning in the Output pane and Error List when a column name in a SELECT statement is a wildcard (*).  
  
-   **Default Mode**: True  
  
-   **Optimistic Mode**: False  
  
-   **Full Mode**: True  
  
**Warn when identifier name is changed**  
Displays a message in the assessment report and in the Output pane when an object identifier name is changed by SSMA.  
  
-   **Default Mode**: True  
  
-   **Optimistic Mode**: False  
  
-   **Full Mode**: True  
  
**Warn when identifier will be quoted**  
Displays a message in the assessment report and in the Output pane when an object identifier name will be quoted by SSMA. Quoting identifiers is necessary when the name is a keyword or contains special characters.  
  
-   **Default Mode**: True  
  
-   **Optimistic Mode**: False  
  
-   **Full Mode**: True  
  
## See Also  
[User Interface Reference(Access)](https://msdn.microsoft.com/af24c303-4a41-449b-9c86-d6558a97e839)  
  
